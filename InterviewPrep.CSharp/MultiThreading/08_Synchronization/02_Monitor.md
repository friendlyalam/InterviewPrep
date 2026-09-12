# Monitor in C#

## 1. What is `Monitor`?

`Monitor` is a .NET synchronization mechanism used to provide **mutual exclusion** and **thread coordination**.

It is closely related to the C# `lock` statement.

The simplest mental model is:

```text
Monitor
   ↓
Controls access to a shared critical section
   ↓
Only one thread can enter at a time

Example:

Monitor.Enter(_lockObject);

try
{
    // Critical section
}
finally
{
    Monitor.Exit(_lockObject);
}
2. lock vs Monitor

C#:

lock (_lockObject)
{
    DoWork();
}

is conceptually equivalent to:

Monitor.Enter(_lockObject);

try
{
    DoWork();
}
finally
{
    Monitor.Exit(_lockObject);
}

Therefore:

lock is the simpler C# syntax for monitor-based mutual exclusion.

For normal locking, prefer lock.

Use Monitor when you need its additional coordination capabilities.

3. Why Does Monitor Exist?

lock is intentionally simple.

It gives you:

Acquire lock
    ↓
Execute critical section
    ↓
Release lock

Monitor additionally provides methods for:

Waiting
Signaling
Trying to acquire a lock
Coordinating multiple threads

Important methods:

Monitor.Enter(...)
Monitor.Exit(...)
Monitor.TryEnter(...)
Monitor.Wait(...)
Monitor.Pulse(...)
Monitor.PulseAll(...)
4. Basic Monitor.Enter / Exit
private readonly object _lockObject = new();

private int _counter;

public void Increment()
{
    Monitor.Enter(_lockObject);

    try
    {
        _counter++;
    }
    finally
    {
        Monitor.Exit(_lockObject);
    }
}

The finally block is extremely important.

If an exception occurs:

_counter++;

the lock must still be released.

5. Why Use finally?

Incorrect:

Monitor.Enter(_lockObject);

_counter++;

Monitor.Exit(_lockObject);

Suppose _counter++ throws an exception.

Then:

Monitor.Enter()
      ↓
Exception
      ↓
Monitor.Exit() never executes
      ↓
Lock remains held

This can cause other threads to wait indefinitely.

Correct:

Monitor.Enter(_lockObject);

try
{
    _counter++;
}
finally
{
    Monitor.Exit(_lockObject);
}

Now:

Normal execution → Exit()
Exception        → finally → Exit()
6. Monitor.Enter and lock

Prefer:

lock (_lockObject)
{
    _counter++;
}

instead of manually writing:

Monitor.Enter(_lockObject);

try
{
    _counter++;
}
finally
{
    Monitor.Exit(_lockObject);
}

The lock syntax is:

Shorter
Easier to read
Less error-prone
Appropriate for normal mutual exclusion

Use Monitor when you need functionality that lock does not directly expose.

7. Monitor.TryEnter

TryEnter attempts to acquire the lock without necessarily waiting indefinitely.

Example:

if (Monitor.TryEnter(_lockObject))
{
    try
    {
        DoWork();
    }
    finally
    {
        Monitor.Exit(_lockObject);
    }
}
else
{
    Console.WriteLine("Could not acquire lock.");
}

Mental model:

TryEnter
   ↓
Lock available?
   ├── Yes → Enter
   └── No  → Continue without acquiring

This can be useful when waiting indefinitely is undesirable.

8. TryEnter with Timeout

You can specify a timeout.

if (Monitor.TryEnter(
        _lockObject,
        TimeSpan.FromSeconds(2)))
{
    try
    {
        DoWork();
    }
    finally
    {
        Monitor.Exit(_lockObject);
    }
}
else
{
    Console.WriteLine(
        "Could not acquire lock within timeout.");
}

This means:

Try for up to 2 seconds
        ↓
Lock acquired?
   ├── Yes → Execute
   └── No  → Continue

This can help avoid indefinite waiting.

9. Monitor.Wait

Monitor.Wait is one of the major differences between Monitor and the normal lock syntax.

Wait allows a thread holding the monitor to:

Release the monitor.
Enter a waiting state.
Wait for another thread to signal it.
Reacquire the monitor before continuing.

Example:

lock (_lockObject)
{
    while (!_dataAvailable)
    {
        Monitor.Wait(_lockObject);
    }

    ProcessData();
}

Mental model:

Thread
  ↓
Owns monitor
  ↓
Condition not satisfied
  ↓
Monitor.Wait()
  ↓
Releases monitor
  ↓
Waits
10. Why Does Monitor.Wait Release the Lock?

Suppose:

lock (_lockObject)
{
    while (!_dataAvailable)
    {
        Monitor.Wait(_lockObject);
    }
}

If Wait() did not release the lock:

Consumer → holds lock → waits
Producer → needs same lock → cannot enter

That would create a deadlock-like situation.

Therefore:

Monitor.Wait()
      ↓
Release monitor
      ↓
Wait for notification
      ↓
Reacquire monitor
      ↓
Continue
11. Monitor.Pulse

Pulse wakes one thread waiting on the same monitor.

Example:

lock (_lockObject)
{
    _dataAvailable = true;

    Monitor.Pulse(_lockObject);
}

Mental model:

Waiting threads
      ↓
   Monitor
      ↓
Pulse()
      ↓
Wake one waiting thread

Important:

Pulse() does not directly transfer the lock to the waiting thread.

The awakened thread must still reacquire the monitor.

12. Monitor.PulseAll

PulseAll wakes all threads waiting on the monitor.

lock (_lockObject)
{
    _dataAvailable = true;

    Monitor.PulseAll(_lockObject);
}

Mental model:

Waiting Thread A ─┐
Waiting Thread B ─┼── PulseAll()
Waiting Thread C ─┘
        ↓
All become eligible to continue

They still need to reacquire the monitor before proceeding.

13. Wait, Pulse, and PulseAll

Think of them as a communication mechanism:

Monitor.Wait()
→ "I cannot continue yet."

Monitor.Pulse()
→ "One waiting thread may try again."

Monitor.PulseAll()
→ "All waiting threads may try again."
14. Condition-Based Waiting

A very important pattern is:

lock (_lockObject)
{
    while (!condition)
    {
        Monitor.Wait(_lockObject);
    }

    // Condition is satisfied
}

Why while instead of if?

Because when the thread wakes up, the condition should be checked again.

Correct:

while (!_dataAvailable)
{
    Monitor.Wait(_lockObject);
}

Avoid:

if (!_dataAvailable)
{
    Monitor.Wait(_lockObject);
}

The condition may no longer be true when the thread gets the monitor again.

15. Producer-Consumer with Monitor

A simplified producer-consumer pattern:

Producer
   ↓
Add data
   ↓
Pulse()
   ↓
Consumer wakes
   ↓
Process data

Consumer:

lock (_lockObject)
{
    while (!_dataAvailable)
    {
        Monitor.Wait(_lockObject);
    }

    ConsumeData();
}

Producer:

lock (_lockObject)
{
    ProduceData();

    Monitor.Pulse(_lockObject);
}

This is an important classic synchronization pattern.

16. Example: Simple Producer-Consumer
private readonly object _lockObject = new();

private readonly Queue<int> _queue = new();

public void Produce(int value)
{
    lock (_lockObject)
    {
        _queue.Enqueue(value);

        Monitor.Pulse(_lockObject);
    }
}

public int Consume()
{
    lock (_lockObject)
    {
        while (_queue.Count == 0)
        {
            Monitor.Wait(_lockObject);
        }

        return _queue.Dequeue();
    }
}

Flow:

Producer
   ↓
Queue.Enqueue()
   ↓
Pulse()
   ↓
Consumer wakes
   ↓
Queue.Dequeue()
17. Why while Is Important in Producer-Consumer

Suppose several consumers are waiting:

Consumer A → waiting
Consumer B → waiting
Consumer C → waiting

Producer adds one item:

Queue = [Item]

and signals.

A consumer wakes and eventually consumes the item.

Another consumer may also become eligible later.

Therefore, every consumer must check:

while (_queue.Count == 0)
{
    Monitor.Wait(_lockObject);
}

This protects against continuing when the condition is no longer satisfied.

18. Monitor.Wait Must Be Called While Owning the Monitor

This is important.

Correct:

lock (_lockObject)
{
    Monitor.Wait(_lockObject);
}

or:

Monitor.Enter(_lockObject);

try
{
    Monitor.Wait(_lockObject);
}
finally
{
    Monitor.Exit(_lockObject);
}

Incorrect:

Monitor.Wait(_lockObject);

without owning the monitor.

This results in an exception.

19. Monitor.Pulse Must Also Be Used with the Monitor

Correct:

lock (_lockObject)
{
    Monitor.Pulse(_lockObject);
}

The calling thread must own the monitor associated with the object.

20. Monitor Ownership

A monitor has an owner.

Thread A
   ↓
Monitor.Enter(lockObject)
   ↓
Owns monitor

Other threads attempting to enter:

Thread B → waits
Thread C → waits

When Thread A exits:

Monitor.Exit()
      ↓
Another waiting thread can acquire
21. Monitor Is Reentrant

Like lock, a monitor is reentrant for the same thread.

Example:

private readonly object _lockObject = new();

public void MethodA()
{
    lock (_lockObject)
    {
        MethodB();
    }
}

private void MethodB()
{
    lock (_lockObject)
    {
        // Same thread can re-enter
    }
}

The same thread can acquire the monitor again.

The runtime tracks the recursive ownership.

22. Monitor and Thread Safety

Monitor can protect shared mutable state:

private readonly object _lockObject = new();

private int _balance;

public void Deposit(int amount)
{
    Monitor.Enter(_lockObject);

    try
    {
        _balance += amount;
    }
    finally
    {
        Monitor.Exit(_lockObject);
    }
}

However, just using Monitor somewhere does not automatically make the entire class thread-safe.

All relevant access paths must follow the synchronization strategy.

23. Monitor and Shared Collections

Example:

private readonly List<int> _numbers = new();

private readonly object _lockObject = new();

public void Add(int number)
{
    lock (_lockObject)
    {
        _numbers.Add(number);
    }
}

The same can be written with Monitor:

public void Add(int number)
{
    Monitor.Enter(_lockObject);

    try
    {
        _numbers.Add(number);
    }
    finally
    {
        Monitor.Exit(_lockObject);
    }
}

For normal collection protection, lock is generally easier to read.

For concurrent collection requirements, consider:

ConcurrentDictionary
ConcurrentQueue
ConcurrentBag
ConcurrentStack
24. Monitor and await

A normal monitor is a synchronous synchronization mechanism.

Do not design code like this:

Monitor.Enter(_lockObject);

try
{
    await SaveAsync();
}
finally
{
    Monitor.Exit(_lockObject);
}

Holding a monitor across an asynchronous suspension is problematic and should not be used as an async locking pattern.

For asynchronous mutual exclusion, use an async-compatible primitive such as:

SemaphoreSlim

Example:

await _semaphore.WaitAsync();

try
{
    await SaveAsync();
}
finally
{
    _semaphore.Release();
}

Mental model:

Monitor / lock
→ synchronous coordination

SemaphoreSlim
→ asynchronous waiting
25. Monitor vs SemaphoreSlim
| Feature          | Monitor                       | SemaphoreSlim       |
| ---------------- | ----------------------------- | ------------------- |
| Process-local    | Yes                           | Yes                 |
| Mutual exclusion | Yes                           | Yes                 |
| Multiple permits | No                            | Yes                 |
| `await` support  | No                            | Yes                 |
| `Wait` / `Pulse` | Yes                           | Different API       |
| `TryEnter`       | Yes                           | `Wait` with timeout |
| Typical use      | Synchronous critical sections | Async coordination  |


For:

Synchronous critical section
→ lock / Monitor

Async critical section
→ SemaphoreSlim
26. Monitor vs Mutex

Monitor:

Process-local

Mutex can be used for synchronization involving:

Multiple processes

For example:

Process A
    ↓
Mutex
    ↑
Process B

A normal C# lock or Monitor does not provide this cross-process coordination.

However, for modern distributed systems, neither should automatically be considered a distributed lock.

27. Monitor vs Interlocked

For:

_counter++;

you could use:

Interlocked.Increment(ref _counter);

Instead of:

lock (_lockObject)
{
    _counter++;
}

Use Interlocked for simple atomic operations.

Use Monitor/lock when multiple related operations must be protected together.

Example:

lock (_lockObject)
{
    if (_balance >= amount)
    {
        _balance -= amount;
        _transactionCount++;
    }
}

This involves multiple related operations.

Interlocked.Increment() alone cannot provide the required business-level atomicity.

28. Monitor and TryEnter

TryEnter can be useful when you want to avoid waiting indefinitely.

Example:

if (!Monitor.TryEnter(_lockObject, 1000))
{
    Console.WriteLine("Lock unavailable.");
    return;
}

try
{
    DoWork();
}
finally
{
    Monitor.Exit(_lockObject);
}

Meaning:

Wait up to 1 second
        ↓
Lock acquired?
   ├── Yes → Work
   └── No  → Return
29. Monitor and Timeout

Timeout-based locking can help with:

Avoiding indefinite waits
Detecting contention
Failing fast
Applying fallback behavior

But timeout should not be used blindly.

If you cannot acquire the lock:

What should happen?

The application must have a valid business/technical fallback.

Examples:

Retry
Return cached data
Return failure
Queue the operation
Skip optional work
30. Monitor and Deadlocks

Multiple monitors can still create deadlocks.

Example:

Thread A
   ↓
Monitor.Enter(LockA)
   ↓
Waits for LockB

Thread B
   ↓
Monitor.Enter(LockB)
   ↓
Waits for LockA

Result:

Thread A → waiting
Thread B → waiting

Neither can continue.

Monitor does not eliminate deadlocks.

31. Avoid Nested Locks When Possible

Example:

lock (_lockA)
{
    lock (_lockB)
    {
        DoWork();
    }
}

This increases complexity.

If multiple locks are unavoidable:

Always acquire them in the same order.

For example:

Lock A → Lock B

Every code path should follow:

Lock A → Lock B

and never:

Lock B → Lock A
32. Monitor and Exceptions

Correct:

Monitor.Enter(_lockObject);

try
{
    DoWork();
}
finally
{
    Monitor.Exit(_lockObject);
}

The finally block guarantees release.

This is essential for reliable synchronization.

33. Monitor and Thread.Abort

Modern .NET does not support the old Thread.Abort model used by legacy .NET Framework scenarios.

Do not design synchronization around forcibly aborting threads.

Prefer:

CancellationToken

for cooperative cancellation.

Synchronization and cancellation are separate concerns.

34. Monitor and Cancellation

Monitor.Wait itself is not the preferred modern async cancellation mechanism.

For modern asynchronous application code, prefer:

CancellationToken
+
SemaphoreSlim / Channel / async APIs

depending on the problem.

For synchronous monitor-based designs, use carefully designed timeout/condition logic where appropriate.

35. Monitor and ASP.NET Core

In ASP.NET Core:

Many requests
      ↓
Concurrent execution
      ↓
Shared state
      ↓
Synchronization may be required

Example Singleton:

public class CounterService
{
    private readonly object _lockObject = new();

    private int _count;

    public void Increment()
    {
        Monitor.Enter(_lockObject);

        try
        {
            _count++;
        }
        finally
        {
            Monitor.Exit(_lockObject);
        }
    }
}

However, normal application code would usually prefer:

lock (_lockObject)
{
    _count++;
}

unless monitor-specific functionality is needed.

36. Monitor Is Process-Local

This is extremely important for product-company interviews.

Suppose:

Server A
  └── Monitor A

Server B
  └── Monitor B

They do not coordinate.

Therefore:

Monitor synchronizes threads within the same process; it is not a distributed lock.

For multiple servers/containers/pods, use appropriate distributed/database coordination.

37. Monitor and Database Consistency

This does not provide distributed database protection:

lock (_lockObject)
{
    UpdateDatabase();
}

or:

Monitor.Enter(_lockObject);

try
{
    UpdateDatabase();
}
finally
{
    Monitor.Exit(_lockObject);
}

Another application instance can independently access the database.

Use appropriate database mechanisms:

Transactions
Optimistic concurrency
Pessimistic concurrency
Atomic updates
Unique constraints
Idempotency
38. Wait / Pulse Mental Model

Imagine a restaurant.

Customer
   ↓
"Table not available"
   ↓
Wait

Restaurant staff:

Table becomes available
       ↓
Pulse()
       ↓
Waiting customer is notified

The customer still needs to obtain the monitor before continuing.

This is essentially condition-based thread coordination.

39. Pulse Does Not Mean "Run Immediately"

This is an important interview detail.

Calling:

Monitor.Pulse(_lockObject);

does not mean:

The waiting thread immediately starts executing.

Instead:

Pulse
 ↓
Waiting thread becomes eligible to continue
 ↓
It must reacquire the monitor
 ↓
Then it can continue

The signaling thread may still hold the monitor.

40. PulseAll Does Not Mean All Threads Execute Together

Similarly:

Monitor.PulseAll(_lockObject);

does not mean all waiting threads enter the critical section simultaneously.

Only one thread can own the monitor at a time.

Therefore:

PulseAll()
    ↓
Many threads become eligible
    ↓
Compete for monitor
    ↓
One acquires it
    ↓
Others wait
41. Common Mistakes
Mistake 1: Forgetting finally

Bad:

Monitor.Enter(_lockObject);

DoWork();

Monitor.Exit(_lockObject);

Prefer:

Monitor.Enter(_lockObject);

try
{
    DoWork();
}
finally
{
    Monitor.Exit(_lockObject);
}
Mistake 2: Using Monitor when lock is enough

If you only need:

Mutual exclusion

prefer:

lock (_lockObject)
{
    DoWork();
}
Mistake 3: Calling Wait without owning the monitor

Invalid:

Monitor.Wait(_lockObject);

without acquiring the monitor first.

Mistake 4: Using if instead of while

Prefer:

while (!condition)
{
    Monitor.Wait(_lockObject);
}
Mistake 5: Assuming Pulse releases the lock

It does not.

The signaling thread continues to own the monitor until it exits/releases it.

Mistake 6: Using Monitor for async mutual exclusion

Prefer:

SemaphoreSlim

when asynchronous waiting is required.

Mistake 7: Assuming Monitor works across servers

It does not.

It is process-local.

42. When Should You Use Monitor?

Use Monitor when:

You need synchronous mutual exclusion.
You need TryEnter.
You need Wait.
You need Pulse.
You need PulseAll.
You are implementing condition-based thread coordination.
lock does not expose the control you need.

For ordinary critical sections:

Prefer lock.
43. When Should You NOT Use Monitor?

Avoid Monitor when:

A simple lock is sufficient.
You need asynchronous waiting.
Interlocked is enough.
A concurrent collection is more appropriate.
You need cross-process coordination.
You need distributed locking.
You need durable producer-consumer infrastructure.
44. Choosing the Right Synchronization Mechanism
| Problem                                   | Recommended Mechanism               |
| ----------------------------------------- | ----------------------------------- |
| Simple synchronous critical section       | `lock`                              |
| Advanced synchronous monitor coordination | `Monitor`                           |
| Simple atomic operation                   | `Interlocked`                       |
| Async mutual exclusion                    | `SemaphoreSlim`                     |
| Concurrent dictionary                     | `ConcurrentDictionary`              |
| Producer-consumer                         | `Channel<T>`                        |
| Cross-process synchronization             | `Mutex` or appropriate OS mechanism |
| Distributed coordination                  | Distributed infrastructure          |
| Database consistency                      | Database concurrency mechanisms     |

45. Product Company Interview Questions
Q1. What is Monitor?

Monitor is a .NET synchronization mechanism that provides mutual exclusion and thread coordination through APIs such as Enter, Exit, Wait, Pulse, PulseAll, and TryEnter.

Q2. What is the relationship between lock and Monitor?

lock is the simpler C# construct for monitor-based mutual exclusion.

Conceptually:

lock (_lockObject)
{
    DoWork();
}

behaves like:

Monitor.Enter(_lockObject);

try
{
    DoWork();
}
finally
{
    Monitor.Exit(_lockObject);
}
Q3. Why use Monitor instead of lock?

Use Monitor when you need functionality such as:

TryEnter
Wait
Pulse
PulseAll

For ordinary mutual exclusion, lock is generally preferable.

Q4. What does Monitor.Wait() do?

It causes the current thread to release the monitor and wait for a notification. When awakened, the thread must reacquire the monitor before continuing.

Q5. What is the difference between Pulse and PulseAll?
Pulse
→ Signals one waiting thread.

PulseAll
→ Signals all waiting threads.

They do not directly transfer ownership of the monitor.

Q6. Why should Monitor.Wait() usually be inside a while loop?

Because the thread must re-check the condition after waking and reacquiring the monitor.

Example:

while (!_dataAvailable)
{
    Monitor.Wait(_lockObject);
}
Q7. Can Monitor be used with await?

Monitor is a synchronous synchronization mechanism and should not be used as an async locking primitive.

For asynchronous coordination, use an async-compatible mechanism such as SemaphoreSlim.

Q8. What happens if you forget Monitor.Exit()?

The monitor may remain held, causing other threads to block.

Therefore use:

try
{
    // Work
}
finally
{
    Monitor.Exit(_lockObject);
}
Q9. Is Monitor reentrant?

Yes.

The same thread can enter the same monitor multiple times.

Q10. Is Monitor thread-safe?

Monitor itself provides synchronization, but using it incorrectly does not make an entire application thread-safe.

The synchronization strategy must correctly protect the shared state.

Q11. Is Monitor process-local?

Yes.

It coordinates threads within the same process.

Q12. Can Monitor prevent distributed race conditions?

No.

For multiple servers or application instances, use an appropriate distributed/database concurrency strategy.

46. Scenario-Based Interview Question
Scenario

You have a producer and consumer sharing a queue.

The consumer should wait when the queue is empty.

What could you use?

Answer

A classic synchronous implementation can use:

Monitor.Wait()
Monitor.Pulse()

Consumer:

lock (_lockObject)
{
    while (_queue.Count == 0)
    {
        Monitor.Wait(_lockObject);
    }

    return _queue.Dequeue();
}

Producer:

lock (_lockObject)
{
    _queue.Enqueue(item);

    Monitor.Pulse(_lockObject);
}

For modern asynchronous producer-consumer workloads, however, Channel<T> is often a better abstraction.

47. Scenario-Based Interview Question
Scenario

You have this:

Monitor.Enter(_lockObject);

try
{
    await SaveAsync();
}
finally
{
    Monitor.Exit(_lockObject);
}

Would you recommend it?

Answer

No.

A monitor is a synchronous synchronization primitive and should not be used as an async lock.

Use:

await _semaphore.WaitAsync();

try
{
    await SaveAsync();
}
finally
{
    _semaphore.Release();
}

with SemaphoreSlim.

48. Scenario-Based Interview Question
Scenario

Two threads use:

Monitor.Enter(_lockA);

and:

Monitor.Enter(_lockB);

to protect the same shared object.

Is the object protected?

Answer

No.

The threads are using different monitors/lock objects.

Synchronization only works when all relevant access paths follow the same synchronization strategy.

49. Senior-Level Design Thinking

When considering Monitor, ask:

Is the state shared?
Is it mutable?
Is concurrent access possible?
Is the operation synchronous?
Do I only need mutual exclusion?
If yes, would lock be simpler?
Do I need Wait/Pulse?
Do I need TryEnter?
Would Interlocked be enough?
Would a concurrent collection be better?
Is this actually an async workflow?
If async, should I use SemaphoreSlim?
Is the problem process-local or distributed?
Could multiple locks introduce deadlock?
Can shared mutable state be eliminated?
50. Quick Comparison
lock
→ Simple synchronous mutual exclusion

Monitor
→ lock + advanced synchronous coordination

Interlocked
→ Simple atomic operations

SemaphoreSlim
→ Async-compatible coordination

Concurrent Collections
→ Thread-safe collection operations

Channel<T>
→ Modern producer-consumer communication

Distributed mechanisms
→ Cross-server coordination
51. Quick Revision

Remember:

Monitor is a .NET synchronization mechanism.
lock is the simpler syntax for monitor-based locking.
Monitor.Enter() acquires the monitor.
Monitor.Exit() releases it.
Always use finally with manual Enter/Exit.
Monitor.TryEnter() attempts to acquire the monitor.
Monitor.Wait() releases the monitor and waits.
Monitor.Pulse() signals one waiting thread.
Monitor.PulseAll() signals all waiting threads.
Wait should normally be used inside a while condition loop.
Pulse does not directly transfer monitor ownership.
PulseAll does not make all threads execute simultaneously.
Monitor is reentrant.
Monitor is synchronous.
Do not use Monitor as an async lock.
Use SemaphoreSlim for async mutual exclusion when appropriate.
Monitor is process-local.
Monitor does not provide distributed locking.
Multiple monitors can cause deadlocks.
Prefer lock when advanced Monitor features are unnecessary.
52. Final Mental Model
                    Monitor
                       │
          ┌────────────┼────────────┐
          ↓            ↓            ↓
       Enter         Wait        TryEnter
          │            │            │
       Acquire       Release      Try acquire
       monitor       + wait       monitor
          │            │
          ↓            ↓
       Critical     Pulse/PulseAll
       section          │
                        ↓
                  Thread coordination

The most important relationship:

lock
  ↓
Simple monitor-based mutual exclusion

Monitor
  ↓
More control over synchronous synchronization
  ↓
Enter / Exit / TryEnter / Wait / Pulse / PulseAll
Product Company One-Line Summary

Monitor is the lower-level .NET synchronization primitive behind lock, providing synchronous mutual exclusion plus advanced coordination through Wait, Pulse, PulseAll, and TryEnter; for ordinary locking use lock,
and for async coordination use an async-compatible mechanism such as SemaphoreSlim.
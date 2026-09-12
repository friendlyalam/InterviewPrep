# Lock in C#

## 1. What is `lock`?

`lock` is a C# synchronization mechanism used to ensure that **only one thread at a time** can execute a critical section of code protected by the same lock object.

It is primarily used to protect **shared mutable state** from race conditions.

```csharp
lock (lockObject)
{
    // Critical section
}

Mental model:

Thread A → acquires lock → enters critical section
Thread B → waits

Thread A → releases lock

Thread B → acquires lock → enters critical section
2. Why Do We Need lock?

Consider shared data:

int counter = 0;

Multiple threads execute:

counter++;

It looks like one operation, but conceptually it is:

1. Read counter
2. Add 1
3. Write counter

Two threads can interleave these operations and lose updates.

Example:

Initial counter = 0

Thread A reads 0
Thread B reads 0

Thread A writes 1
Thread B writes 1

Expected = 2
Actual   = 1

This is a race condition.

lock prevents multiple threads from entering the protected critical section simultaneously.

3. Basic lock Example
private int _counter = 0;

private readonly object _lockObject = new();

public void Increment()
{
    lock (_lockObject)
    {
        _counter++;
    }
}

If multiple threads call Increment():

Thread A → lock acquired → counter++
Thread B → waits

Thread A → lock released

Thread B → lock acquired → counter++

Therefore, the read-modify-write operation is protected.

4. Critical Section

A critical section is a section of code where shared state is accessed or modified and must not be executed concurrently by multiple threads.

Example:

lock (_lockObject)
{
    _balance -= amount;
}

The code inside the lock is the critical section.

Keep critical sections:

Small
Focused
Fast
Free from unnecessary blocking
5. How lock Works

Conceptually:

lock (_lockObject)
{
    DoWork();
}

is equivalent to using Monitor:

Monitor.Enter(_lockObject);

try
{
    DoWork();
}
finally
{
    Monitor.Exit(_lockObject);
}

The compiler/runtime ensures that the lock is released even if an exception occurs.

This is one reason lock is preferred over manually calling Monitor.Enter() in simple scenarios.

6. Same Lock Object Is Important

This works:

private readonly object _lockObject = new();

public void Method1()
{
    lock (_lockObject)
    {
        // Access shared state
    }
}

public void Method2()
{
    lock (_lockObject)
    {
        // Access same shared state
    }
}

Both methods use the same synchronization object.

Therefore, only one can enter its critical section at a time.

7. Different Lock Objects Do Not Protect the Same State

This does NOT provide synchronization:

private readonly object _lock1 = new();
private readonly object _lock2 = new();

private int _counter;

public void Method1()
{
    lock (_lock1)
    {
        _counter++;
    }
}

public void Method2()
{
    lock (_lock2)
    {
        _counter++;
    }
}

Why?

Because:

Thread A → locks _lock1
Thread B → locks _lock2

Both can access _counter simultaneously.

Therefore:

The lock object must be shared by all code paths that need mutual exclusion over the same state.

8. What Should You Lock?

Best practice:

private readonly object _lockObject = new();

Then:

lock (_lockObject)
{
    // protected state
}

A private lock object prevents external code from accidentally synchronizing on your internal implementation detail.

9. Why Should the Lock Object Usually Be Private?

Avoid:

public object LockObject { get; } = new();

Another piece of code could do:

lock (service.LockObject)
{
    // Hold your service's lock
}

This creates unwanted coupling.

Prefer:

private readonly object _lockObject = new();

The class controls its own synchronization.

10. Avoid Locking on this

Avoid:

lock (this)
{
    // ...
}

Why?

Because external code can also access the same object:

lock (myObject)
{
    // External code
}

Now external code can interfere with your synchronization.

Prefer:

private readonly object _lockObject = new();
11. Avoid Locking on typeof(...)

Avoid using publicly accessible type objects as synchronization locks:

lock (typeof(MyClass))
{
    // ...
}

Other code can potentially synchronize on the same type object.

A private lock object gives the class control over synchronization.

12. Avoid Locking on Strings

Avoid:

lock ("OrderLock")
{
    // ...
}

Strings can be interned, meaning different parts of an application may unexpectedly reference the same string instance.

Prefer:

private readonly object _lockObject = new();
13. Lock Scope

Good:

lock (_lockObject)
{
    _counter++;
}

Bad:

lock (_lockObject)
{
    CallExternalService();
    Thread.Sleep(5000);
    PerformExpensiveCalculation();
}

Holding a lock for a long time causes other threads to wait.

This can cause:

Reduced throughput
Increased latency
Thread contention
ThreadPool pressure
Potential starvation
14. Do Not Perform Unnecessary I/O Inside lock

Avoid:

lock (_lockObject)
{
    SaveToDatabase();
}

or:

lock (_lockObject)
{
    CallHttpApi();
}

The lock may be held while waiting for external systems.

Better architecture:

Lock
 ↓
Update protected in-memory state
 ↓
Unlock
 ↓
Perform external operation

However, moving work outside the lock is only correct if the required consistency guarantees are still maintained.

15. lock and await

A normal C# lock cannot contain an await.

This is invalid:

lock (_lockObject)
{
    await SaveAsync();
}

Why?

Because the lock is synchronous, while await can suspend the method and resume later.

For asynchronous coordination, consider:

SemaphoreSlim

Example:

private readonly SemaphoreSlim _semaphore = new(1, 1);

public async Task UpdateAsync()
{
    await _semaphore.WaitAsync();

    try
    {
        await SaveAsync();
    }
    finally
    {
        _semaphore.Release();
    }
}

Mental model:

lock
→ synchronous mutual exclusion

SemaphoreSlim
→ can support asynchronous waiting
16. lock Does Not Make Everything Thread-Safe

This is important.

Suppose:

private readonly object _lockObject = new();
private int _balance;

public void Deposit(int amount)
{
    lock (_lockObject)
    {
        _balance += amount;
    }
}

Deposit() is protected.

But:

public int GetBalance()
{
    return _balance;
}

may still violate the intended synchronization strategy.

A consistent synchronization strategy is required for shared state.

For example:

public int GetBalance()
{
    lock (_lockObject)
    {
        return _balance;
    }
}
17. Lock Protects Code, Not Data Automatically

lock does not magically make a variable thread-safe.

It provides mutual exclusion around the code protected by that lock.

For example:

lock (_lockObject)
{
    _counter++;
}

The protection comes from ensuring all relevant accesses use the same synchronization strategy.

Mental model:

Shared mutable data
        ↓
Multiple concurrent accesses
        ↓
Synchronization required
        ↓
lock / Interlocked / concurrent collection /
database concurrency / other appropriate mechanism
18. Lock and Instance Fields

Example:

public class Counter
{
    private int _count;

    private readonly object _lockObject = new();

    public void Increment()
    {
        lock (_lockObject)
        {
            _count++;
        }
    }

    public int GetValue()
    {
        lock (_lockObject)
        {
            return _count;
        }
    }
}

Each Counter instance has its own lock.

Therefore:

Counter A → Lock A → Count A

Counter B → Lock B → Count B

They do not block each other.

19. Lock and Static State

Suppose:

private static int _count;

The state is shared across instances.

Therefore, synchronization must also be shared appropriately.

Example:

private static int _count;

private static readonly object _lockObject = new();

public static void Increment()
{
    lock (_lockObject)
    {
        _count++;
    }
}

Now all instances/classes using these static members coordinate through the same lock.

20. Lock and Singleton

Singleton services are particularly important in ASP.NET Core.

Example:

public class CacheService
{
    private readonly Dictionary<string, string> _cache = new();

    private readonly object _lockObject = new();

    public void Set(string key, string value)
    {
        lock (_lockObject)
        {
            _cache[key] = value;
        }
    }

    public string? Get(string key)
    {
        lock (_lockObject)
        {
            return _cache.TryGetValue(key, out string? value)
                ? value
                : null;
        }
    }
}

If the service is registered as Singleton:

Many HTTP requests
        ↓
Same service instance
        ↓
Same Dictionary
        ↓
Concurrent access
        ↓
Synchronization may be required

Important:

Singleton lifetime does not automatically mean thread-safe.

21. lock and Collections

This is unsafe:

private readonly List<int> _numbers = new();

public void Add(int value)
{
    _numbers.Add(value);
}

If multiple threads modify the list concurrently, synchronization may be required.

One approach:

private readonly object _lockObject = new();

public void Add(int value)
{
    lock (_lockObject)
    {
        _numbers.Add(value);
    }
}

But for highly concurrent scenarios, a concurrent collection may be more appropriate:

ConcurrentBag<int>
ConcurrentQueue<int>
ConcurrentDictionary<TKey, TValue>

Choose based on the required access pattern.

22. lock vs ConcurrentDictionary

lock:

lock (_lockObject)
{
    _dictionary[key] = value;
}

ConcurrentDictionary:

_dictionary[key] = value;

ConcurrentDictionary provides thread-safe collection operations.

However:

A concurrent collection does not automatically make a multi-step business workflow atomic.

Example:

if (!_dictionary.ContainsKey(key))
{
    _dictionary[key] = value;
}

The overall check-then-act workflow needs careful consideration.

Prefer atomic APIs when available:

_dictionary.TryAdd(key, value);
23. Lock and counter++

This:

lock (_lockObject)
{
    counter++;
}

protects the entire read-modify-write operation.

Without the lock:

Read
 ↓
Modify
 ↓
Write

can interleave between threads.

With the lock:

Thread A:
Read → Modify → Write
        ↓
      complete

Thread B:
Read → Modify → Write
24. lock vs Interlocked

For a simple atomic counter:

Interlocked.Increment(ref _counter);

may be preferable to:

lock (_lockObject)
{
    _counter++;
}

Use Interlocked when the operation is simple and supported.

Examples:

Interlocked.Increment(ref counter);

Interlocked.Decrement(ref counter);

Interlocked.Add(ref counter, amount);

Interlocked.Exchange(ref value, newValue);

Interlocked.CompareExchange(
    ref value,
    newValue,
    expectedValue);

Mental model:

Simple atomic operation
        ↓
Interlocked

Multiple related operations / critical section
        ↓
lock
25. lock vs Monitor

lock is the simpler language construct.

Example:

lock (_lockObject)
{
    DoWork();
}

Monitor provides more control.

Example:

Monitor.Enter(_lockObject);

try
{
    DoWork();
}
finally
{
    Monitor.Exit(_lockObject);
}

Monitor also provides APIs such as:

Monitor.Wait(...)
Monitor.Pulse(...)
Monitor.PulseAll(...)
TryEnter(...)

For normal mutual exclusion:

Prefer lock.

Use Monitor when you specifically need its advanced coordination features.

26. Reentrant Locking

C# lock is reentrant for the same thread.

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
        // Allowed for the same thread
    }
}

The same thread can acquire the same lock again.

The runtime tracks the lock ownership/reentrancy.

27. Lock Contention

Lock contention occurs when multiple threads want the same lock and some must wait.

Example:

Thread A ──┐
           ↓
        [ LOCK ]
           ↑
Thread B ──┘ waits

Thread C ──┘ waits

High contention can reduce performance.

Symptoms can include:

Threads spending time waiting
Increased latency
Lower throughput
CPU inefficiency
ThreadPool pressure
28. Reduce Lock Contention

Possible strategies:

1. Keep critical sections small
lock (_lockObject)
{
    UpdateState();
}
2. Avoid unnecessary shared state

Prefer local variables when possible.

3. Use immutability

Immutable state reduces synchronization requirements.

4. Use concurrent collections

When they fit the access pattern.

5. Use Interlocked

For simple atomic operations.

6. Partition state

Instead of one global lock:

One huge lock

consider:

Lock A → Data A
Lock B → Data B
Lock C → Data C

when the design permits it.

29. One Lock vs Multiple Locks

One lock:

lock (_lockObject)
{
    // All shared state
}

is simple but can increase contention.

Multiple locks:

lock (_customerLock)
{
    // Customer state
}

lock (_orderLock)
{
    // Order state
}

can improve concurrency.

But multiple locks introduce another risk:

Deadlocks.

30. Deadlock with Multiple Locks

Example:

Thread A:
Lock A
 ↓
Wait for Lock B

Thread B:
Lock B
 ↓
Wait for Lock A

Neither can continue.

Therefore:

Multiple locks
     ↓
More concurrency
     +
More complexity
     +
Potential deadlock

If multiple locks are necessary, establish a consistent lock acquisition order.

31. Lock Ordering

Bad:

Thread A → Lock Customer → Lock Order

Thread B → Lock Order → Lock Customer

Potential deadlock.

Better:

Always:

Lock Customer
    ↓
Lock Order

Every code path follows the same order.

32. Lock and Exceptions

Consider:

lock (_lockObject)
{
    throw new Exception();
}

The lock is still released when leaving the lock block.

Conceptually, this is why lock behaves like:

Monitor.Enter(_lockObject);

try
{
    throw new Exception();
}
finally
{
    Monitor.Exit(_lockObject);
}

Do not manually release a lock.

33. Lock and Performance

lock is not inherently bad.

The problem is unnecessary or poorly designed synchronization.

Good:

lock (_lockObject)
{
    _counter++;
}

Potentially problematic:

lock (_lockObject)
{
    PerformLargeCalculation();
    CallDatabase();
    CallHttpApi();
    Thread.Sleep(5000);
}

The second example keeps other threads waiting for too long.

34. Lock in ASP.NET Core

ASP.NET Core applications process many requests concurrently.

Example:

Request 1 ──┐
Request 2 ──┤
Request 3 ──┼──> Same Singleton Service
Request 4 ──┤
Request 5 ──┘

If the Singleton contains mutable shared state:

private readonly Dictionary<string, object> _cache;

concurrent access must be designed carefully.

Possible approaches:

lock
ConcurrentDictionary
Interlocked
immutable data
SemaphoreSlim
external distributed cache
database concurrency mechanisms

Choose based on the actual problem.

35. Local lock vs Distributed Systems

This is a critical product-company interview point.

Suppose:

Server A
   ↓
lock

Server B
   ↓
lock

The locks are different because each process has its own memory.

Therefore:

A C# lock only coordinates threads that share the same process and lock object.

It does NOT provide distributed locking across:

Multiple servers
Multiple containers
Multiple Kubernetes pods
Multiple application instances

For distributed coordination, consider appropriate infrastructure such as:

Database transactions/locking
Distributed cache mechanisms
Distributed lock services
Message queues
Optimistic concurrency
Unique constraints
36. Lock and Database Operations

Do not assume:

lock (_lockObject)
{
    UpdateDatabase();
}

provides system-wide database concurrency control.

Another application instance can execute:

Server B → UpdateDatabase()

without acquiring your Server A lock.

Database concurrency must be handled at the database/distributed-system level when required.

Possible techniques:

Transactions
Row/version-based optimistic concurrency
Database locks
Atomic UPDATE statements
Unique constraints
Idempotency
37. Example: Inventory

Suppose:

Stock = 1

Two customers purchase simultaneously.

Bad approach:

if (stock > 0)
{
    stock--;
}

Even with an application-level lock, the lock only protects that particular process.

With multiple application servers:

Customer A → Server A
Customer B → Server B

each server can have its own lock.

A better distributed approach can be an atomic database operation such as:

UPDATE Products
SET Stock = Stock - 1
WHERE ProductId = @ProductId
  AND Stock > 0;

Then check the affected row count.

This allows the database to enforce the atomic stock update.

38. Lock Does Not Solve Business-Level Atomicity Automatically

Consider:

lock (_lockObject)
{
    CheckBalance();
    UpdateBalance();
    CreateTransaction();
}

This may protect in-memory operations.

But if:

UpdateBalance succeeds
CreateTransaction fails

you may still have an inconsistent business operation.

Business atomicity may require:

Database transaction
Outbox pattern
Saga
Idempotency
Compensation
Distributed transaction alternatives

lock is a synchronization primitive, not a replacement for distributed transaction design.

39. Lock and Immutability

One of the best ways to reduce synchronization complexity is to reduce shared mutable state.

Instead of:

shared mutable object

prefer when practical:

immutable object

Mental model:

Mutable shared state
        ↓
Synchronization required

Immutable state
        ↓
Much less synchronization

Immutability is especially useful in concurrent and distributed systems.

40. Common Mistakes
Mistake 1: Locking on this
lock (this)

Avoid.

Mistake 2: Locking on a public object
public object LockObject { get; }

Avoid exposing synchronization objects unnecessarily.

Mistake 3: Using different locks
lock (_lock1)
lock (_lock2)

for the same shared state.

This does not coordinate access.

Mistake 4: Holding locks too long

Avoid:

lock (_lockObject)
{
    Thread.Sleep(5000);
}
Mistake 5: Performing network/database calls inside a lock

Avoid unless there is a very specific reason and the locking strategy is carefully designed.

Mistake 6: Assuming Singleton means thread-safe

It does not.

Mistake 7: Assuming lock solves distributed concurrency

It does not.

Mistake 8: Using lock when Interlocked is enough

For a simple counter:

Interlocked.Increment(ref _counter);

may be simpler and more efficient.

Mistake 9: Using lock around async code

You cannot await inside a normal lock.

Use async-compatible synchronization when needed.

41. When Should You Use lock?

Use lock when:

Multiple threads access shared mutable state.
You need mutual exclusion.
The critical section is synchronous.
Multiple operations must be protected together.
A simple process-local synchronization mechanism is sufficient.

Example:

lock (_lockObject)
{
    _balance += amount;
}
42. When Should You NOT Use lock?

Do not automatically use lock when:

State is immutable.
State is thread-local.
There is no concurrent access.
Interlocked is sufficient.
A concurrent collection is a better fit.
The operation requires await.
Coordination must work across multiple servers.
43. Choosing the Right Tool
| Requirement                            | Possible Choice                  |
| -------------------------------------- | -------------------------------- |
| Simple atomic counter                  | `Interlocked`                    |
| Synchronous critical section           | `lock`                           |
| Advanced synchronous coordination      | `Monitor`                        |
| Async mutual exclusion                 | `SemaphoreSlim`                  |
| Concurrent dictionary operations       | `ConcurrentDictionary`           |
| Concurrent queue                       | `ConcurrentQueue`                |
| Producer/consumer                      | `Channel<T>`                     |
| Cross-process/distributed coordination | Distributed mechanism            |
| Database consistency                   | Transactions/concurrency control |
| Reduce synchronization                 | Immutability                     |


The correct choice depends on the concurrency problem.

44. Enterprise Example

Imagine an in-memory cache:

public class ProductCache
{
    private readonly Dictionary<int, Product> _products = new();

    private readonly object _lockObject = new();

    public void Add(Product product)
    {
        lock (_lockObject)
        {
            _products[product.Id] = product;
        }
    }

    public Product? Get(int id)
    {
        lock (_lockObject)
        {
            return _products.TryGetValue(
                id,
                out Product? product)
                    ? product
                    : null;
        }
    }
}

The lock protects access to the shared dictionary.

However, in a distributed ASP.NET Core application, an in-memory cache exists independently on each application instance.

Therefore:

Instance A → Cache A
Instance B → Cache B
Instance C → Cache C

If shared cache consistency is required, consider a distributed cache.

45. lock Mental Model

Think of a lock as a single key to a room.

             ┌───────────────┐
Thread A ───>│   LOCKED ROOM │
             │ Critical Code │
Thread B ───>│    WAITING    │
Thread C ───>│    WAITING    │
             └───────────────┘

Only the thread holding the key can enter.

When it exits:

Thread A → releases lock
                ↓
Thread B → acquires lock
46. Product Company Interview Questions
Q1. What is lock in C#?

lock provides mutual exclusion so that only one thread at a time can execute a protected critical section for the same lock object.

Q2. What does lock internally use?

lock is implemented using the runtime's monitor synchronization mechanism.

Conceptually:

lock (obj)
{
    DoWork();
}

behaves like:

Monitor.Enter(obj);

try
{
    DoWork();
}
finally
{
    Monitor.Exit(obj);
}
Q3. Why should you avoid lock(this)?

Because external code can also lock the same object, creating unwanted coupling and potential contention/deadlocks.

Use:

private readonly object _lockObject = new();
Q4. Can we use await inside lock?

No.

Use an async-compatible synchronization mechanism such as SemaphoreSlim when asynchronous waiting is required.

Q5. Does lock make code thread-safe?

Not automatically.

All relevant accesses to the shared state must follow a correct synchronization strategy.

Q6. Does lock work across multiple servers?

No.

A normal C# lock is process-local.

Q7. lock vs Interlocked?

Use:

lock
→ multiple related operations / critical section

Interlocked
→ simple atomic operation
Q8. lock vs ConcurrentDictionary?

lock provides general mutual exclusion.

ConcurrentDictionary provides thread-safe dictionary operations designed for concurrent access.

Choose based on whether you need general critical-section protection or concurrent collection semantics.

Q9. Can a Singleton service have race conditions?

Yes.

Singleton means one instance per DI container/process, not automatic thread safety.

Q10. What is lock contention?

When multiple threads compete for the same lock and some must wait.

Q11. How can you reduce lock contention?
Keep critical sections small.
Avoid unnecessary shared state.
Use immutable data.
Use Interlocked where appropriate.
Use concurrent collections where appropriate.
Partition independent state.
Avoid blocking/I/O inside locks.
Q12. Can lock prevent database race conditions across servers?

No.

Database-level concurrency mechanisms are required for distributed/database consistency.

47. Scenario-Based Interview Question
Scenario

An ASP.NET Core application has:

10 application instances
1000 concurrent requests
shared inventory

A developer writes:

lock (_lockObject)
{
    if (stock > 0)
    {
        stock--;
    }
}

Is this sufficient?

Answer

Not necessarily.

The lock only protects threads sharing that particular lock object within the same process.

With 10 application instances:

Server A → Lock A
Server B → Lock B
Server C → Lock C
...
Server J → Lock J

These locks do not coordinate with each other.

For shared inventory, use an appropriate distributed/database concurrency strategy, such as an atomic database update, optimistic concurrency, or another suitable distributed mechanism.

48. Another Scenario
Scenario

You have:

lock (_lockObject)
{
    await SaveToDatabaseAsync();
}

What is wrong?

Answer

A normal C# lock cannot contain await.

For asynchronous coordination, use an async-compatible primitive such as:

SemaphoreSlim

Example:

await _semaphore.WaitAsync();

try
{
    await SaveToDatabaseAsync();
}
finally
{
    _semaphore.Release();
}
49. Senior-Level Design Thinking

When you see:

Shared mutable state
+
Concurrent access

do not immediately say:

"Use lock."

First ask:

Is the state actually shared?
Is it mutable?
Does concurrent access really occur?
Is the operation synchronous or asynchronous?
Is the scope process-local or distributed?
Is the operation simple enough for Interlocked?
Would a concurrent collection fit?
Can the state be immutable?
Is database-level consistency required?
Will locking create contention?
Could multiple locks create deadlock?
Is there a better architecture that avoids shared mutable state?

This is the level of reasoning expected in senior/product-company interviews.

50. Quick Comparison
| Feature             | `lock`        | `Interlocked`                       | `SemaphoreSlim`     |
| ------------------- | ------------- | ----------------------------------- | ------------------- |
| Mutual exclusion    | Yes           | Limited to atomic operations        | Yes                 |
| Multiple operations | Yes           | Usually no                          | Yes                 |
| `await` support     | No            | N/A                                 | Yes                 |
| Simple counter      | Possible      | Excellent fit                       | Usually unnecessary |
| Critical section    | Excellent fit | Not suitable for arbitrary sections | Good                |
| Process-local       | Yes           | Yes                                 | Yes                 |
| Distributed         | No            | No                                  | No                  |


51. Quick Revision

Remember these points:

lock provides mutual exclusion.
It protects a critical section.
Only one thread can hold the same lock at a time.
Use a private lock object.
Avoid lock(this).
Avoid locking on public objects.
All relevant accesses must use the same synchronization strategy.
Keep critical sections small.
Avoid unnecessary I/O inside locks.
lock cannot contain await.
Use SemaphoreSlim for async mutual exclusion when appropriate.
Interlocked is useful for simple atomic operations.
Concurrent collections provide thread-safe collection operations.
Singleton does not automatically mean thread-safe.
lock is process-local.
lock does not solve distributed concurrency.
Database consistency requires database/distributed mechanisms.
Multiple locks can cause deadlocks.
Lock contention can hurt performance.
Immutability can reduce synchronization requirements.
52. Final Mental Model
Shared mutable state
        ↓
Concurrent access
        ↓
Race condition risk
        ↓
Need synchronization/design
        ↓
┌─────────────────────────────────┐
│ Simple atomic operation         │
│        → Interlocked            │
│                                 │
│ Synchronous critical section    │
│        → lock                   │
│                                 │
│ Advanced monitor coordination  │
│        → Monitor                │
│                                 │
│ Async critical section          │
│        → SemaphoreSlim          │
│                                 │
│ Concurrent collection           │
│        → Concurrent*            │
│                                 │
│ Distributed consistency         │
│        → DB/distributed design  │
└─────────────────────────────────┘
Product Company One-Line Summary

lock provides process-local mutual exclusion for a synchronous critical section, but senior-level concurrency design requires choosing the right mechanism based on atomicity, async execution,
contention, shared state, and whether coordination must work across multiple application instances.
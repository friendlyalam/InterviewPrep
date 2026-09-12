# Race Condition and Thread Safety

## 1. Overview

A **race condition** occurs when multiple threads or tasks access shared mutable data concurrently, and the final result depends on the timing or order of execution.

Race conditions are one of the most important concepts in multithreading.

They are especially important in:

- C#/.NET
- ASP.NET Core
- Microservices
- Background processing
- Parallel programming
- Shared caches
- Inventory systems
- Payment systems
- Order processing
- Database concurrency

---

# 2. Simple Definition

A race condition happens when:

```text
Multiple threads
      ↓
Access shared mutable state
      ↓
At least one modifies it
      ↓
Operations overlap
      ↓
Result depends on timing

Example:

counter++;

It looks like one operation, but internally it is approximately:

1. Read counter
2. Add 1
3. Write counter

If two threads perform this simultaneously, updates can be lost.

3. Simple Example

Suppose:

int counter = 0;

Two threads execute:

counter++;

Expected result:

Thread 1 → +1
Thread 2 → +1

Final = 2

But internally:

Thread 1 → Read 0
Thread 2 → Read 0

Thread 1 → Calculate 1
Thread 2 → Calculate 1

Thread 1 → Write 1
Thread 2 → Write 1

Final = 1

Expected:

2

Actual:

1

This is a race condition.

4. Shared State Is the Main Problem

A race condition generally requires shared state.

For example:

int counter = 0;

If multiple threads access the same variable:

Thread 1 ──┐
           ├──→ counter
Thread 2 ──┘

and at least one thread modifies it, synchronization may be required.

5. Shared Mutable State

The dangerous combination is:

Shared
+
Mutable
+
Concurrent access

Example:

class Counter
{
    public int Value;
}

If multiple threads modify:

counter.Value++;

the operation may not be thread-safe.

6. Local Variables Usually Do Not Cause This Problem

Consider:

private static void Calculate()
{
    int result = 0;

    result++;

    Console.WriteLine(result);
}

Each invocation has its own local variable.

Conceptually:

Thread 1 → result = 0
Thread 2 → result = 0
Thread 3 → result = 0

They do not normally share the same local variable.

Therefore, local state is generally safer than shared mutable state.

7. Shared Object Example

Consider:

public class Counter
{
    public int Value { get; set; }
}

Then:

Counter counter = new Counter();

Multiple tasks use the same object:

Task.Run(() => counter.Value++);
Task.Run(() => counter.Value++);
Task.Run(() => counter.Value++);

The object is shared.

Therefore, the update must be considered for thread safety.

8. Why counter++ Is Not Atomic

This:

counter++;

is a read-modify-write operation.

Conceptually:

Read
 ↓
Modify
 ↓
Write

It is not one indivisible operation.

That is why multiple threads can interfere with each other.

9. Atomic Operation

An operation is atomic when it appears to happen as one indivisible operation with respect to competing threads.

For example:

Interlocked.Increment(ref counter);

provides an atomic increment operation.

Instead of:

counter++;

use:

Interlocked.Increment(ref counter);

when the requirement is specifically an atomic numeric update.

10. Fixing a Race Condition with lock

A common solution is:

private readonly object _lock = new();

lock (_lock)
{
    counter++;
}

The lock creates a critical section.

Conceptually:

Thread 1 → LOCK → counter++ → UNLOCK
Thread 2 → WAIT
Thread 3 → WAIT

Only one thread executes the protected section at a time for that lock object.

11. Critical Section

A critical section is code that accesses shared state and must be protected from unsafe concurrent access.

Example:

lock (_lock)
{
    counter++;
}

Here:

counter++

is the critical operation.

12. Lock Scope Matters

Good:

lock (_lock)
{
    counter++;
}

Avoid unnecessarily large critical sections:

lock (_lock)
{
    PerformExpensiveOperation();

    CallExternalService();

    SaveLargeFile();

    counter++;
}

Holding a lock for a long time can cause:

contention
reduced throughput
latency
deadlocks
poor scalability

Keep critical sections as small as reasonably possible.

13. Correct Lock Object

Prefer:

private readonly object _lock = new();

Then:

lock (_lock)
{
    // shared state
}

Avoid locking on publicly accessible objects:

lock (this)
{
}

or:

lock (typeof(MyClass))
{
}

because external code may also acquire the same lock.

A private lock object gives you control over synchronization.

14. Lock Does Not Make Everything Thread-Safe

Consider:

lock (_lock)
{
    counter++;
}

This protects the increment only if all relevant accesses use the same synchronization strategy.

For example:

lock (_lock)
{
    counter++;
}

but somewhere else:

counter++;

The second access bypasses the lock.

Therefore, the shared state is still not safely protected.

15. Thread Safety

Thread-safe code behaves correctly when accessed concurrently by multiple threads.

Example:

public class ThreadSafeCounter
{
    private int _value;
    private readonly object _lock = new();

    public void Increment()
    {
        lock (_lock)
        {
            _value++;
        }
    }

    public int GetValue()
    {
        lock (_lock)
        {
            return _value;
        }
    }
}

The class controls access to its shared mutable state.

16. Race Condition vs Thread Safety

These concepts are related but not identical.

Race condition

A problem caused by timing/order of concurrent operations.

Thread safety

A property of code/design that remains correct under concurrent access.

Think:

Race condition
→ The bug/problem

Thread safety
→ The correctness goal
17. Race Condition vs Data Race

These terms are related but should not be treated as exactly identical.

A data race generally means multiple threads access the same memory location concurrently, with at least one write, without appropriate synchronization.

A race condition is broader.

A race condition can occur when correctness depends on timing/order, even when the operations are not simply a raw unsynchronized memory access.

For interview purposes:

Data race
→ Unsafe concurrent memory access

Race condition
→ Correctness depends on timing/order
18. Example of a Race Condition

Suppose:

if (balance >= 100)
{
    balance -= 100;
}

Two requests execute concurrently.

Initial balance:

100

Both requests may observe:

balance >= 100

Then both withdraw:

Request 1 → -100
Request 2 → -100

The system has allowed two operations based on the same stale state.

This is a business-level race condition.

19. Check-Then-Act Race

A common race-condition pattern is:

Check
 ↓
Act

Example:

if (!items.Contains(id))
{
    items.Add(id);
}

This looks reasonable.

But two threads can do:

Thread 1 → Check → Not found
Thread 2 → Check → Not found

Thread 1 → Add
Thread 2 → Add

The check and add are not one atomic operation.

20. Time-of-Check to Time-of-Use

Another important pattern is:

Check state
   ↓
Something else changes state
   ↓
Use state

Example:

if (account.Balance >= amount)
{
    Withdraw(account, amount);
}

Another thread may modify the balance between the check and withdrawal.

This is often called a TOCTOU problem:

Time Of Check To Time Of Use.

21. Fixing Check-Then-Act

The check and modification may need to be protected together:

lock (_lock)
{
    if (balance >= amount)
    {
        balance -= amount;
    }
}

Now:

Check
+
Act

are inside the same critical section.

22. Race Condition with Collections

This can be unsafe:

List<int> numbers = new();

Parallel.For(
    0,
    1000,
    i =>
    {
        numbers.Add(i);
    });

List<T> is not designed for arbitrary concurrent writes.

Possible solutions depend on the requirement.

For example:

ConcurrentBag<int> numbers =
    new ConcurrentBag<int>();

or synchronize access:

lock (_lock)
{
    numbers.Add(i);
}

The correct choice depends on the workload and required semantics.

23. Thread-Safe Collections

.NET provides concurrent collections such as:

ConcurrentDictionary<TKey,TValue>
ConcurrentQueue<T>
ConcurrentStack<T>
ConcurrentBag<T>
BlockingCollection<T>

These are designed for specific concurrent-access scenarios.

However:

A thread-safe collection does not automatically make the entire application thread-safe.

Higher-level business operations may still contain races.

24. Example: ConcurrentDictionary

This:

ConcurrentDictionary<int, string>

can safely support concurrent operations such as:

dictionary.TryAdd(key, value);

But this pattern may still be problematic:

if (!dictionary.ContainsKey(key))
{
    dictionary.TryAdd(key, value);
}

Even with a concurrent collection, the combined business operation may need an atomic API or a different design.

Prefer APIs that express the intended atomic operation where available.

For example:

dictionary.GetOrAdd(
    key,
    valueFactory);
25. Immutability as a Solution

One powerful way to reduce race conditions is to avoid shared mutable state.

Instead of:

Many threads
     ↓
Shared mutable object
     ↓
Synchronization required

prefer:

Immutable state
     ↓
Less shared mutation
     ↓
Fewer race conditions

Example:

public record User(
    int Id,
    string Name);

If the object is not modified after creation, concurrent reads are much easier to reason about.

26. Thread Confinement

Another strategy is thread confinement.

Instead of allowing many threads to modify the same object:

Thread 1 ──┐
Thread 2 ──┼──→ Shared Object
Thread 3 ──┘

keep state associated with one execution context:

Thread 1 → State 1
Thread 2 → State 2
Thread 3 → State 3

This reduces synchronization requirements.

27. Message Passing

Another strategy is to avoid direct shared mutation.

Instead:

Producer
   ↓
Message / Channel / Queue
   ↓
Consumer
   ↓
Owns state

This is a major idea in concurrent and distributed systems.

Examples:

Channel<T>
message queues
event-driven architecture
actor-style systems

Instead of multiple workers modifying the same state directly, work can be serialized through a controlled owner.

28. Async/Await Does Not Automatically Prevent Race Conditions

This is an important misconception.

Consider:

Task task1 = UpdateAsync();
Task task2 = UpdateAsync();

await Task.WhenAll(task1, task2);

If both operations modify shared mutable state, a race condition can still occur.

async/await solves asynchronous flow.

It does not automatically provide synchronization.

29. ASP.NET Core and Race Conditions

ASP.NET Core applications handle many requests concurrently.

Consider:

public class CounterService
{
    private int _count;

    public void Increment()
    {
        _count++;
    }
}

If this service instance is shared across requests, concurrent requests can access _count.

Therefore, mutable shared state must be carefully designed.

30. Singleton Services and Race Conditions

This is especially important with DI.

Suppose:

services.AddSingleton<MyService>();

The same service instance can be used by many requests.

If it contains mutable fields:

private int _counter;

you must consider thread safety.

A singleton does not automatically mean thread-safe.

31. Scoped Services Do Not Automatically Solve Everything

A scoped service normally has one instance per request scope.

This can reduce sharing between requests.

However, it does not mean all concurrent operations inside that scope are automatically safe.

For example, multiple tasks in the same request can still access the same object concurrently.

Therefore:

Scoped
≠
Automatically thread-safe
32. Static Mutable State Is Dangerous

Example:

private static int _counter;

Static state is shared across instances and can be accessed concurrently.

Therefore:

static + mutable + concurrent access

requires careful synchronization.

Avoid static mutable state unless there is a strong reason.

33. Database Race Conditions

Race conditions are not only in C# memory.

They can happen in databases.

Example:

Application A → Read stock = 1
Application B → Read stock = 1

Application A → Buy
Application B → Buy

Result → Overselling

A local C# lock cannot protect data across multiple application instances.

This is a distributed concurrency problem.

34. Local Lock vs Distributed Lock

Suppose:

Server A
   ↓
lock
   ↓
Database

and:

Server B
   ↓
lock
   ↓
Database

Each server has its own memory.

Therefore:

Server A lock
≠
Server B lock

A local C# lock cannot coordinate both servers.

For distributed systems, consider:

database transactions
optimistic concurrency
pessimistic concurrency
distributed locks
unique constraints
idempotency
message queues
35. Optimistic Concurrency

Optimistic concurrency assumes conflicts are relatively uncommon.

Conceptually:

Read entity
   ↓
Remember version
   ↓
Modify
   ↓
Save only if version is unchanged

Example:

Version = 5

Two clients read:

Client A → Version 5
Client B → Version 5

Client A updates:

Version 5 → Version 6

Client B tries to update using Version 5.

The database detects:

Expected Version = 5
Actual Version = 6

and rejects/conflict-detects the update.

This prevents silent overwrites.

36. Pessimistic Concurrency

Pessimistic concurrency assumes conflicts are likely and protects data during the operation.

Conceptually:

Acquire lock
    ↓
Read
    ↓
Modify
    ↓
Save
    ↓
Release lock

Database mechanisms such as transaction isolation and row-level locking can be used depending on the database and workload.

37. Unique Constraints as a Concurrency Tool

Suppose the business rule is:

Only one account may use a particular email.

Do not rely only on:

if (!repository.Exists(email))
{
    repository.Create(email);
}

Two requests can both observe:

Not exists

and both attempt to insert.

A database unique constraint provides authoritative enforcement.

This is an important product-company design principle:

Enforce critical invariants at the correct ownership boundary.

38. Race Condition in Inventory

Suppose:

Stock = 1

Two users purchase simultaneously.

Without concurrency control:

User A → Read 1
User B → Read 1

User A → Purchase
User B → Purchase

The system may oversell.

Possible solutions:

Database transaction
Optimistic concurrency
Pessimistic locking
Atomic database update
Queue-based serialization

The correct solution depends on the business requirements.

39. Atomic Database Update

Instead of:

SELECT stock
      ↓
Application checks stock
      ↓
UPDATE stock

a database can sometimes perform the business condition atomically:

UPDATE Inventory
SET Stock = Stock - 1
WHERE ProductId = @ProductId
  AND Stock > 0;

Then inspect the affected-row count.

Conceptually:

1 row affected → purchase succeeded
0 rows affected → unavailable/conflict

This can be safer than doing the check and update as separate application operations.

40. Race Conditions and Idempotency

Suppose a payment request is accidentally submitted twice.

Request 1 → Charge ₹100
Request 2 → Charge ₹100

If the operation is not idempotent, the customer could be charged twice.

An idempotency key can help:

Client
  ↓
Idempotency Key
  ↓
Payment Service
  ↓
Check/record request
  ↓
Process once

Idempotency is an important tool for preventing duplicate effects in distributed systems.

41. Synchronization Tools

Common .NET synchronization mechanisms include:

lock
Monitor
Interlocked
SemaphoreSlim
Mutex
ReaderWriterLockSlim
Volatile
Concurrent collections
Channels
CancellationToken

They solve different problems.

Do not choose a synchronization primitive simply because it is familiar.

42. lock vs Interlocked
lock

Useful for:

Multiple operations
+
Shared state
+
Need mutual exclusion

Example:

lock (_lock)
{
    _balance -= amount;
}
Interlocked

Useful for simple atomic operations:

Interlocked.Increment(ref _counter);

Examples:

Increment
Decrement
Exchange
CompareExchange

Use the simplest mechanism that correctly represents the required atomicity.

43. CompareExchange

Interlocked.CompareExchange() is useful for atomic compare-and-update operations.

Conceptually:

If current value == expected value
    ↓
replace it

This enables lock-free algorithms and state transitions in appropriate scenarios.

However, lock-free programming is advanced and easy to get wrong.

Do not use it merely to avoid lock.

44. Memory Visibility

Concurrency is not only about atomicity.

Threads also need appropriate memory visibility guarantees.

For example:

Thread A → writes shared state
Thread B → reads shared state

Without appropriate synchronization, assumptions about when another thread observes changes can be unsafe.

Synchronization mechanisms such as:

lock
Interlocked
Volatile
Task coordination

provide memory-ordering/visibility guarantees appropriate to their semantics.

45. Atomicity vs Visibility

These are different concepts.

Atomicity

An operation cannot be observed as a partially completed update.

Visibility

One thread can reliably observe another thread's update according to the synchronization guarantees.

Ordering

Operations are observed/executed according to the guarantees required by the memory model.

Think:

Thread safety
├── Atomicity
├── Visibility
└── Ordering
46. Race Condition Testing

Race conditions can be difficult to reproduce.

A test may pass:

Run 1 → pass
Run 2 → pass
Run 3 → pass

and fail later:

Run 4 → failure

because the bug depends on timing.

Useful techniques include:

high iteration counts
controlled delays
barriers
concurrent execution
stress testing
deterministic synchronization where possible

Do not rely on one successful test run.

47. Example Testing Strategy

Conceptually:

for (int i = 0; i < 1000; i++)
{
    Task task1 = Task.Run(() => counter++);
    Task task2 = Task.Run(() => counter++);

    await Task.WhenAll(task1, task2);
}

This may expose timing-related problems.

However, a race condition can still remain even if the test does not fail.

48. Race Condition Debugging

When investigating a concurrency bug, ask:

1. What state is shared?
2. Who can modify it?
3. Who can read it?
4. Can operations overlap?
5. Is the operation atomic?
6. What synchronization protects it?
7. Do all accesses use the same synchronization strategy?
8. Is the state local or distributed?
9. Is ordering important?
10. Can the database enforce the invariant?
49. Common Mistakes
Mistake 1: Assuming ++ is atomic
counter++;

It is not a safe atomic increment for concurrent access.

Use:

Interlocked.Increment(ref counter);

when appropriate.

Mistake 2: Locking only the write

Bad design:

if (balance >= amount)
{
    lock (_lock)
    {
        balance -= amount;
    }
}

The check occurs outside the critical section.

The state can change between the check and update.

Better:

lock (_lock)
{
    if (balance >= amount)
    {
        balance -= amount;
    }
}
Mistake 3: Using different lock objects

Bad:

lock (new object())
{
    counter++;
}

Every invocation gets a different lock.

Therefore, it does not provide useful mutual exclusion.

Mistake 4: Locking on this

Avoid:

lock (this)
{
}

because external code can also lock the same object.

Mistake 5: Holding a lock during I/O

Avoid:

lock (_lock)
{
    await CallExternalApiAsync();
}

The C# lock statement cannot contain an await.

More broadly, do not hold synchronous locks around long-running external operations.

Redesign the state transition or use an async-compatible coordination mechanism such as SemaphoreSlim where appropriate.

Mistake 6: Assuming Concurrent Collections Solve Business Races

A ConcurrentDictionary can make individual collection operations safe.

It does not automatically make:

Check
+
Business decision
+
Update

atomic.

Mistake 7: Assuming async code is single-threaded

Async operations can resume on different threads.

Do not assume:

Before await → same thread → After await

is guaranteed.

Mistake 8: Assuming local locks work across servers

They do not.

For distributed systems, use a distributed coordination mechanism or database-level concurrency control.

50. Product Company Scenario
Scenario

You are designing an e-commerce inventory service.

Stock:

10 items

100 customers send requests concurrently.

Question:

How do you prevent overselling?

A strong answer should discuss:

1. Shared inventory state
2. Concurrent requests
3. Atomic update / transaction
4. Optimistic or pessimistic concurrency
5. Database constraints where applicable
6. Retry behavior
7. Idempotency
8. Distributed deployment
9. Queue-based serialization if required
10. Performance trade-offs

Do not simply answer:

Use lock.

A local C# lock is insufficient when multiple application instances are involved.

51. Product Company Scenario: Duplicate Order

Suppose a client retries:

POST /orders

twice because of a network timeout.

You could receive:

Request 1 → Create Order
Request 2 → Create Order

The solution may involve:

Idempotency Key
+
Unique Constraint
+
Transaction

depending on the system design.

52. Product Company Scenario: Singleton Cache

Suppose an ASP.NET Core singleton cache has:

private Dictionary<int, Product> _products;

and multiple requests modify it concurrently.

Questions to ask:

Is Dictionary safe for concurrent writes?
No.

Should we use ConcurrentDictionary?
Possibly.

Are compound operations atomic?
Not automatically.

Can immutable snapshots work?
Possibly.

Is a distributed cache required?
Maybe, if multiple servers need shared cache state.

The correct answer depends on the requirements.

53. Race Condition Prevention Strategies

There is no single solution.

Common strategies:

1. Avoid shared mutable state
2. Use immutability
3. Use lock
4. Use Interlocked
5. Use appropriate concurrent collections
6. Use SemaphoreSlim for async coordination
7. Use channels/message passing
8. Use database transactions
9. Use optimistic concurrency
10. Use pessimistic concurrency
11. Use distributed locks when truly necessary
12. Use idempotency
13. Use database constraints
54. Choosing the Right Strategy
Simple counter
Interlocked
Multiple related fields
lock
Async coordination
SemaphoreSlim
Concurrent key/value access
ConcurrentDictionary
Producer/consumer
Channel<T>
Cross-server data consistency
Database concurrency control
/
Distributed coordination
Duplicate request prevention
Idempotency

The correct tool depends on the problem.

55. Race Condition vs Deadlock

These are different problems.

Race condition
Incorrect result because timing/order affects execution.
Deadlock
Threads/tasks wait indefinitely for each other.

Example:

Race condition
→ Wrong result

Deadlock
→ No progress

A system can have one, the other, or both.

56. Race Condition vs Starvation
Race condition

Correctness problem caused by unsafe concurrent access/timing.

Starvation

A thread/task cannot get enough execution/resource opportunity because other work continuously consumes the resource.

Example:

Thread A waits for lock
Thread B repeatedly gets lock
Thread A keeps waiting

These are different concurrency problems.

57. Thread Safety and Performance

Making everything synchronized is not automatically a good design.

Too much synchronization can cause:

Contention
↓
Waiting
↓
Lower throughput
↓
Higher latency

The goal is:

Correctness
+
Appropriate synchronization
+
Good scalability

Not:

Maximum locking
58. Thread Safety and Scalability

A design should consider:

How many concurrent requests?
How long is the critical section?
How frequently is shared state modified?
Can state be immutable?
Can operations be partitioned?
Can work be serialized?
Can the database enforce the rule?

Good concurrency design often comes from reducing shared mutable state rather than adding more locks.

59. Practical Mental Model

When you see:

sharedState.SomeValue = ...

ask:

Who owns this state?
Who can access it?
Can they access it concurrently?
Can they modify it?
Does ordering matter?
What guarantees do I need?

Then choose the appropriate mechanism.

60. Interview Questions
Q1. What is a race condition?

A race condition occurs when the correctness of a program depends on the timing or order of concurrent operations.

Q2. Why is counter++ unsafe?

Because it is a read-modify-write operation and is not guaranteed to be atomic for concurrent access.

Q3. How can you fix a race condition?

Depending on the problem:

lock
Interlocked
SemaphoreSlim
concurrent collections
immutability
message passing
database transactions
optimistic concurrency
pessimistic concurrency
distributed coordination
Q4. Is lock always the best solution?

No.

The correct mechanism depends on whether the problem involves:

simple atomic updates
multiple related operations
asynchronous coordination
collection access
distributed state
Q5. Is ConcurrentDictionary always thread-safe?

Its supported concurrent operations are designed for concurrent access, but application-level compound operations may still require careful design.

Q6. Does async/await eliminate race conditions?

No.

Async/await does not automatically synchronize shared mutable state.

Q7. Does Task.WhenAll() prevent race conditions?

No.

WhenAll() only coordinates task completion.

Multiple tasks can still concurrently modify shared state.

Q8. Can a Singleton service have race conditions?

Yes.

A singleton is shared by many consumers, so mutable instance state must be thread-safe.

Q9. Can a local lock protect a database across multiple servers?

No.

A C# lock only coordinates threads within the relevant process/object.

Q10. How would you prevent inventory overselling?

Discuss atomic database updates, transactions, optimistic/pessimistic concurrency, idempotency, and potentially queue-based serialization depending on requirements.

61. Quick Revision
| Concept                 | Key Point                                   |
| ----------------------- | ------------------------------------------- |
| Race condition          | Result depends on timing/order              |
| Shared state            | Main source of risk                         |
| Mutable state           | Increases race-condition risk               |
| `counter++`             | Not atomic                                  |
| `lock`                  | Mutual exclusion                            |
| `Interlocked`           | Atomic simple operations                    |
| `SemaphoreSlim`         | Async-compatible coordination               |
| Concurrent collection   | Safe collection-level concurrent operations |
| Immutability            | Reduces shared-state problems               |
| `async/await`           | Does not guarantee thread safety            |
| `WhenAll()`             | Does not prevent races                      |
| Singleton               | Can contain shared mutable state            |
| Local lock              | Process-local                               |
| Database transaction    | Can coordinate shared persistent state      |
| Optimistic concurrency  | Detect conflicts                            |
| Pessimistic concurrency | Prevent conflicts through locking           |
| Idempotency             | Prevents duplicate effects                  |
| Deadlock                | Waiting indefinitely                        |
| Starvation              | Unable to obtain resources/execution        |
| Thread safety           | Correct behavior under concurrent access    |

62. Key Points to Remember
A race condition is fundamentally a correctness problem caused by concurrent operations whose timing/order matters.
The dangerous combination is:
Shared + Mutable + Concurrent Access
counter++ is not a safe atomic increment.
lock protects a critical section when all relevant accesses use the same lock.
Interlocked is useful for simple atomic operations.
Concurrent collections protect collection operations, not arbitrary business workflows.
async/await does not automatically make shared state thread-safe.
Task.WhenAll() does not prevent race conditions.
Avoid unnecessary shared mutable state.
Immutability is often one of the simplest ways to reduce concurrency bugs.
Local C# locks do not coordinate multiple application servers.
Database concurrency mechanisms are often more appropriate for persistent shared state.
Critical business invariants should be enforced at the correct ownership boundary.
Idempotency is important for preventing duplicate effects in distributed systems.
Synchronization improves correctness but excessive synchronization can hurt performance.
63. Final Mental Model

When multiple execution paths access the same mutable state:

             Shared Mutable State
                     ↓
             Concurrent Access
                     ↓
              Race Condition?
                     ↓
          ┌──────────┴──────────┐
          ↓                     ↓
       Local State          Distributed State
          ↓                     ↓
   lock / Interlocked      DB / Transactions
   SemaphoreSlim           Optimistic Concurrency
   Concurrent Collections   Pessimistic Concurrency
   Immutability             Idempotency
   Channels                 Distributed Coordination

The most important question is not:

"Which lock should I use?"

The better question is:

"How can I design the system so that shared mutable state is minimized, and where shared state is unavoidable, what synchronization or concurrency guarantee does the business requirement actually need?"

64. Product Company One-Line Summary

A race condition occurs when concurrent operations access shared mutable state and correctness depends on timing or ordering; robust systems prevent it through appropriate synchronization, immutability,
atomic operations, database concurrency control, idempotency, and careful distributed-system design.
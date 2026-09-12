# Atomicity and Memory Visibility

## 1. Overview

In multithreaded applications, two important concepts are:

- **Atomicity** → whether an operation happens as one indivisible operation.
- **Memory visibility** → whether changes made by one thread are correctly observed by another thread.

They solve different problems.

```text
Atomicity
→ "Can another thread see this operation halfway through?"

Memory Visibility
→ "Can another thread see the latest value?"
2. What is Atomicity?

An operation is atomic when it appears to happen as one indivisible operation.

Another thread cannot observe the operation in a partially completed state.

Example:

int value = 10;

Interlocked.Increment(ref value);

The increment is atomic.

3. Non-Atomic Operation

This operation is NOT atomic:

counter++;

Conceptually:

Read counter
    ↓
Add 1
    ↓
Write counter

Two threads can interfere with each other.

Example:

Initial counter = 10

Thread A reads 10
Thread B reads 10

Thread A writes 11
Thread B writes 11

Expected = 12
Actual   = 11

This is a race condition.

4. Atomic Operations with Interlocked

For simple atomic operations:

Interlocked.Increment(ref counter);

Other useful operations:

Interlocked.Decrement(ref counter);

Interlocked.Add(ref counter, 10);

Interlocked.Exchange(ref value, newValue);

Interlocked.CompareExchange(
    ref value,
    newValue,
    expectedValue);
5. Atomicity Does Not Mean Thread Safety

Making one operation atomic does not automatically make the entire operation thread-safe.

Example:

if (stock > 0)
{
    stock--;
}

The complete business operation contains:

Check stock
    ↓
Update stock

Even if the individual read/write operations were safe, the complete check-and-update operation may still have a race condition.

For complex operations, use an appropriate synchronization mechanism such as:

lock
SemaphoreSlim
Interlocked.CompareExchange
Database transaction
Optimistic concurrency
Atomic database update
6. What is Memory Visibility?

Memory visibility means:

When one thread changes shared data, another thread must be able to correctly observe that change.

Example:

private volatile bool _stopRequested;

Thread A:

_stopRequested = true;

Thread B:

while (!_stopRequested)
{
    DoWork();
}

The synchronization semantics of volatile help Thread B observe the updated value.

7. Why Memory Visibility Matters

Modern CPUs and runtimes use:

CPU caches
Registers
Compiler optimizations
Instruction reordering
CPU memory ordering

Therefore, multithreaded code cannot simply assume that ordinary reads and writes provide all the synchronization guarantees needed between threads.

Synchronization mechanisms establish the required memory-ordering/visibility guarantees.

8. Example of Visibility Problem

Conceptually:

bool ready = false;
int data = 0;

Thread A:

data = 100;
ready = true;

Thread B:

while (!ready)
{
}

Console.WriteLine(data);

Without appropriate synchronization, this is not a correct thread-communication pattern.

The problem is not simply:

"Is data = 100?"

The real question is:

"Does Thread B have a properly synchronized view of Thread A's writes?"

Use an appropriate synchronization mechanism.

9. Volatile and Memory Visibility

volatile is primarily about visibility and ordering.

Example:

private volatile bool _ready;

When one thread writes:

_ready = true;

another thread reading _ready receives the volatile access semantics required by the .NET memory model.

However:

volatile int counter;

does NOT make:

counter++;

atomic.

10. Interlocked and Memory Visibility

Interlocked provides atomic operations along with the required memory-ordering guarantees around those operations.

Example:

Interlocked.Increment(ref counter);

Therefore, Interlocked is useful when you need:

Atomicity
+
Appropriate memory ordering

for a supported operation.

11. Lock and Memory Visibility

lock provides:

Mutual exclusion
Memory synchronization

Example:

lock (_syncObject)
{
    sharedValue++;
}

A lock is therefore useful when multiple operations must be treated as one synchronized critical section.

12. Atomicity vs Visibility

These are different concepts.

Atomicity

Question:

Can another thread observe this operation halfway through?

Example:

Interlocked.Increment(ref counter);
Visibility

Question:

Can another thread correctly observe the latest shared state?

Example:

volatile bool _stopRequested;

A solution may need one, the other, or both depending on the problem.

13. Atomicity Without Solving the Business Problem

Suppose:

Interlocked.Increment(ref stock);

The increment itself is atomic.

But suppose the business requirement is:

Check stock
↓
Reserve stock
↓
Create order
↓
Reduce stock

Making only the final decrement atomic does not make the entire workflow atomic.

This is why enterprise systems often need:

Database transactions
Optimistic concurrency
Pessimistic concurrency
Idempotency
Unique constraints
Atomic database updates
14. Memory Visibility with Lock

Consider:

lock (_syncObject)
{
    sharedValue = 100;
}

Another thread accessing the same shared state through the same synchronization mechanism gets the required synchronization guarantees.

Important:

Both sides must use the same synchronization strategy correctly.

Using a lock on one side and unsynchronized access on the other can still produce incorrect behavior.

15. Memory Visibility and Volatile

A simple flag is a common use case:

private volatile bool _shutdownRequested;

public void Stop()
{
    _shutdownRequested = true;
}

public void Run()
{
    while (!_shutdownRequested)
    {
        ProcessWork();
    }
}

For modern .NET applications, CancellationToken is usually preferred for cooperative cancellation.

16. Atomicity and Interlocked

For a counter:

private int _requestCount;

public void RequestProcessed()
{
    Interlocked.Increment(ref _requestCount);
}

This is appropriate because the requirement is:

Multiple threads
      ↓
Update one counter
      ↓
Each increment must be atomic
17. CompareExchange

CompareExchange is especially important for lock-free programming.

int oldValue = Interlocked.CompareExchange(
    ref value,
    newValue,
    expectedValue);

Conceptually:

if (value == expectedValue)
{
    value = newValue;
}

The comparison and replacement happen atomically.

18. Atomicity vs Memory Visibility in Real Applications
Counter
Interlocked.Increment(ref counter);

Requirement:

Atomic update
Stop flag
volatile bool stopRequested;

Requirement:

Visibility + ordering
Complex shared object
lock (_syncObject)
{
    // Multiple related operations
}

Requirement:

Mutual exclusion
+
Memory synchronization
Async critical section
await semaphore.WaitAsync();

try
{
    // Critical section
}
finally
{
    semaphore.Release();
}

Requirement:

Async-compatible mutual exclusion
19. Common Mistakes
Mistake 1: Assuming ++ is atomic
counter++;

It is not.

Use:

Interlocked.Increment(ref counter);

when appropriate.

Mistake 2: Assuming volatile means atomic
volatile int counter;
counter++;

Still not a safe atomic increment.

Mistake 3: Assuming atomic means everything is thread-safe
Interlocked.Increment(ref counter);

Only that operation is atomic.

Other shared state may still have races.

Mistake 4: Using volatile for complex business logic
if (stock > 0)
{
    stock--;
}

volatile does not make this workflow atomic.

Mistake 5: Using local synchronization for distributed systems
lock (_syncObject)
{
    // update order
}

This only coordinates threads within the relevant process.

If multiple application instances modify the same business data, use database/distributed concurrency mechanisms.

20. Product-Company Mental Model

Think about concurrency in three layers:

Layer 1: Visibility
        ↓
Can another thread see the change correctly?

Layer 2: Atomicity
        ↓
Can the operation be performed indivisibly?

Layer 3: Business Atomicity
        ↓
Can the complete business workflow be made consistent?

Example:

Inventory

Read stock
   ↓
Check stock > 0
   ↓
Reserve item
   ↓
Create order
   ↓
Update stock

This usually cannot be solved simply with volatile or Interlocked.

It may require database transactions, optimistic concurrency, atomic updates, and idempotency.

21. Quick Revision
Atomicity
→ Operation happens indivisibly.

Memory Visibility
→ One thread correctly observes another thread's changes.

volatile
→ Mainly visibility + ordering.

Interlocked
→ Atomic operations + memory-ordering guarantees.

lock
→ Mutual exclusion + synchronization.

SemaphoreSlim
→ Async-compatible coordination.

Database transaction
→ Business/data consistency across multiple operations.
22. Comparisons
Atomicity vs Memory Visibility
| Feature                    | Atomicity                                               | Memory Visibility                            |
| -------------------------- | ------------------------------------------------------- | -------------------------------------------- |
| Main question              | Is the operation indivisible?                           | Can another thread see the change correctly? |
| Main concern               | Partial/interleaved operation                           | Stale/inconsistent observation               |
| Example                    | `Interlocked.Increment()`                               | `volatile` field                             |
| Prevents `counter++` race? | Yes, when replaced with atomic operation                | No                                           |
| Makes data visible?        | Provides ordering guarantees for Interlocked operations | Yes, this is the main purpose                |
| Protects complex logic?    | No                                                      | No                                           |
| Typical solution           | `Interlocked`, `lock`                                   | `volatile`, `lock`, `Interlocked`            |

Volatile vs Interlocked vs Lock
| Feature                          | `volatile`            | `Interlocked`                     | `lock`                             |
| -------------------------------- | --------------------- | --------------------------------- | ---------------------------------- |
| Main purpose                     | Visibility + ordering | Atomic operations                 | Mutual exclusion                   |
| `counter++` safe?                | ❌                     | ✅ with `Increment()`              | ✅                                  |
| Protects multiple statements?    | ❌                     | ❌                                 | ✅                                  |
| Allows only one thread inside?   | ❌                     | Not as a general critical section | ✅                                  |
| Useful for simple flags?         | ✅                     | Sometimes                         | Sometimes                          |
| Useful for counters?             | ❌ for `++`            | ✅                                 | ✅                                  |
| Supports complex business logic? | ❌                     | Usually ❌                         | ✅                                  |
| Async-friendly                   | N/A                   | Synchronous                       | ❌ cannot use `await` inside `lock` |
| Typical example                  | Stop/state flag       | Request counter                   | Shared object/critical section     |

Interlocked vs Lock
| Feature                     | `Interlocked`                   | `lock`                              |
| --------------------------- | ------------------------------- | ----------------------------------- |
| Best for                    | Simple atomic operations        | Complex critical sections           |
| Counter increment           | Excellent                       | Good                                |
| Multiple related operations | Limited                         | Excellent                           |
| Blocking                    | No traditional lock acquisition | Yes                                 |
| Complexity                  | Low                             | Higher but easier for complex logic |
| Example                     | `Interlocked.Increment()`       | `lock (syncObject)`                 |

| Local           | Distributed                |
| --------------- | -------------------------- |
| `lock`          | Database transaction       |
| `Interlocked`   | Optimistic concurrency     |
| `volatile`      | Distributed lock           |
| `SemaphoreSlim` | Idempotency                |
| Process-local   | Multiple servers/instances |
| Shared memory   | Shared external state      |

Final Interview Summary

Atomicity answers "Can this operation be performed indivisibly?", while memory visibility answers "Can another thread correctly observe the change?" Interlocked is primarily used for atomic operations, volatile for visibility and ordering of supported fields, and lock for protecting larger critical sections.
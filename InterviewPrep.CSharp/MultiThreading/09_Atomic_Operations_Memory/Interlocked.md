# Interlocked

## 1. What is Interlocked?

`Interlocked` is a .NET class that provides **atomic operations** on shared variables in multithreaded applications.

It is mainly used when multiple threads/tasks need to safely update a simple shared value without using a `lock`.

```csharp
Interlocked.Increment(ref counter);

Namespace
using System.Threading;
2. Why Do We Need Interlocked?

Consider:

counter++;

This looks like one operation, but internally it is approximately:

Read counter
    ↓
Add 1
    ↓
Write counter

Two threads can execute these steps at the same time and lose an update.

Interlocked makes supported operations atomic.

3. Common Interlocked Operations
| Method              | Purpose                                         |
| ------------------- | ----------------------------------------------- |
| `Increment()`       | Atomically increase value by 1                  |
| `Decrement()`       | Atomically decrease value by 1                  |
| `Add()`             | Atomically add a value                          |
| `Exchange()`        | Atomically replace a value                      |
| `CompareExchange()` | Replace value only if it matches expected value |
| `Read()`            | Atomically read certain values                  |

4. Interlocked.Increment()

Safely increments a shared integer.

int counter = 0;

Interlocked.Increment(ref counter);

If 10 threads increment the same counter:

Interlocked.Increment(ref counter);

each increment is atomic.

5. Interlocked.Decrement()

Safely decreases a value.

int activeUsers = 10;

Interlocked.Decrement(ref activeUsers);

Useful for counters such as:

Active requests
Active workers
Connection count
Reference counters
6. Interlocked.Add()

Atomically adds a value.

int total = 100;

Interlocked.Add(ref total, 50);

Result:

150

Useful when multiple threads update a numeric total.

7. Interlocked.Exchange()

Atomically replaces a value.

int status = 0;

Interlocked.Exchange(ref status, 1);

The operation changes:

0 → 1

Useful when one thread needs to safely publish a new value.

8. Interlocked.CompareExchange()

One of the most important operations for interviews.

It means:

Replace the current value only if it equals the expected value.

int value = 10;

Interlocked.CompareExchange(
    ref value,
    20,
    10);

Meaning:

If value == 10
    value = 20

Otherwise:

Do nothing

This is the foundation of many lock-free algorithms.

9. CompareExchange Example
int status = 0;

int original = Interlocked.CompareExchange(
    ref status,
    1,
    0);

Meaning:

Expected value = 0
New value      = 1

If status is currently 0:

status becomes 1

If status is already 1:

status remains 1

The returned value tells us what the value was before the operation.

10. Interlocked vs Lock
lock
lock (syncObject)
{
    counter++;
}
Interlocked
Interlocked.Increment(ref counter);

| `lock`                           | `Interlocked`                      |
| -------------------------------- | ---------------------------------- |
| Protects a critical section      | Performs atomic operation          |
| Can protect multiple statements  | Best for simple operations         |
| More general                     | More lightweight                   |
| Can protect complex shared state | Mainly simple atomic state changes |
| Cannot contain `await`           | Synchronous atomic operation       |
| Easier for complex logic         | Better for simple counters/state   |

11. When Should You Use Interlocked?

Use Interlocked when:

Updating a simple numeric counter
Incrementing/decrementing values
Atomically replacing a value
Performing compare-and-swap operations
You need lightweight synchronization
The operation can be expressed using an atomic Interlocked operation

Examples:

Request counter
Retry counter
Active connection count
Reference count
Processed message count
Sequence number
Simple state transition
12. When Should You NOT Use Interlocked?

Do not use Interlocked as a replacement for lock in complex business logic.

For example:

if (balance >= amount)
{
    balance -= amount;
}

This is a check + update operation.

Using a simple Interlocked operation does not automatically make the whole business operation atomic.

For complex operations, consider:

lock
SemaphoreSlim
Database transaction
Optimistic concurrency
Atomic database update
13. Important: Interlocked Does Not Make an Object Thread-Safe

For example:

class Account
{
    public decimal Balance { get; set; }
}

Using Interlocked on one unrelated field does not automatically make the entire Account thread-safe.

Thread safety must be considered for the complete shared state and operation.

14. Interlocked and Race Conditions

Without Interlocked:

counter++;

Potential race condition:

Thread A reads 10
Thread B reads 10

Thread A writes 11
Thread B writes 11

Expected = 12
Actual   = 11

With:

Interlocked.Increment(ref counter);

the increment is atomic.

Thread A → 10 → 11
Thread B → 11 → 12
15. Interlocked and Performance

Interlocked is generally cheaper than taking a lock for simple atomic operations.

However:

Do not choose Interlocked only because it is "faster."

Choose it when the operation naturally fits an atomic operation.

For complex operations, correctness is more important than avoiding a lock.

16. Interlocked and Memory Visibility

Interlocked operations also provide the necessary memory-ordering guarantees around the atomic operation.

Therefore, Interlocked is useful not only for atomicity but also for coordinating visibility of shared state between threads.

For simple shared-state synchronization, this is one reason it is preferable to manually trying to implement atomic behavior.

17. Interlocked with Long Values

Interlocked also supports long.

long requestCount = 0;

Interlocked.Increment(ref requestCount);

This is useful for large counters that may exceed the range of int.

18. Important Limitation

Interlocked works on individual atomic operations.

It does not automatically make this sequence atomic:

if (counter < 100)
{
    counter++;
}

The condition and update together form a larger operation.

For such cases, use an appropriate synchronization strategy.

19. Product-Company Example

Suppose an API tracks the number of processed requests.

Multiple requests may execute concurrently:

private int _processedRequests;

public void ProcessRequest()
{
    // Process request

    Interlocked.Increment(ref _processedRequests);
}

This avoids a race condition when many requests update the counter concurrently.

20. Interview Questions
Q1. What is Interlocked?

Interlocked provides atomic operations for shared variables in multithreaded applications.

Q2. Why use Interlocked instead of counter++?

Because counter++ is a read-modify-write operation and is not atomic.

Q3. Interlocked vs lock?

Interlocked is best for simple atomic operations, while lock can protect a larger critical section containing multiple operations.

Q4. What is CompareExchange?

It atomically replaces a value only when its current value equals an expected value.

Q5. Does Interlocked make an entire class thread-safe?

No. It only makes the specific supported operation atomic. Other shared state and operations may still have race conditions.

Q6. Can Interlocked replace all locks?

No. It is not suitable for complex multi-step critical sections.

Q7. Where is Interlocked commonly used?

Counters, sequence numbers, reference counting, simple state transitions, and lock-free algorithms.

21. Quick Revision
Interlocked
    ↓
Atomic operations
    ↓
Shared state
    ↓
Avoid simple race conditions

Most important methods:

Interlocked.Increment(ref value);
Interlocked.Decrement(ref value);
Interlocked.Add(ref value, amount);
Interlocked.Exchange(ref value, newValue);
Interlocked.CompareExchange(ref value, newValue, expectedValue);
Remember
counter++ is not atomic.
Interlocked.Increment() is atomic.
Use Interlocked for simple shared-state operations.
Use lock for complex critical sections.
CompareExchange is important for lock-free algorithms.
Interlocked does not automatically make an entire class thread-safe.
Interlocked is process-local.
It does not solve distributed concurrency.
Database concurrency requires database-level mechanisms.
Correctness comes before micro-optimization.

Product-company one-line summary:
Interlocked provides lightweight atomic operations for shared state and is ideal for simple thread-safe updates such as counters and state transitions, while complex multi-step operations require stronger synchronization or transactional mechanisms.
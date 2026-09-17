# Multithreading Interview Questions

## 1. What is a thread?

A thread is the smallest unit of execution within a process.

A process can contain multiple threads that share the same process memory but have their own execution state and stack.

### Key Points

- Process = application/container of resources.
- Thread = execution unit inside a process.
- Threads share heap/static memory.
- Each thread has its own stack.
- Multiple threads can execute concurrently.
- Multiple threads can execute in parallel on multiple CPU cores.

---

## 2. Process vs Thread

| Process | Thread |
|---|---|
| Independent execution environment | Execution unit inside a process |
| Has its own memory space | Shares process memory |
| More expensive to create | Cheaper than a process |
| Process isolation is stronger | Threads can affect shared state |
| Communication requires IPC mechanisms | Communication can use shared memory |

### Interview Point

Threads are lightweight compared with processes, but shared memory introduces synchronization and thread-safety problems.

---

## 3. What is concurrency?

Concurrency means multiple operations can make progress during overlapping periods.

They do not necessarily execute at exactly the same time.

Example:

```text
Task A → waiting for database
Task B → executing
Task A → resumes

Concurrency is especially useful for I/O-bound applications.

4. What is parallelism?

Parallelism means multiple operations execute simultaneously, usually on multiple CPU cores.

Core 1 → Task A
Core 2 → Task B
Core 3 → Task C

Parallelism is especially useful for CPU-bound workloads.

5. Concurrency vs Parallelism

| Concurrency                       | Parallelism                                |
| --------------------------------- | ------------------------------------------ |
| Multiple operations make progress | Multiple operations execute simultaneously |
| Does not require multiple cores   | Usually benefits from multiple cores       |
| Excellent for I/O-bound work      | Excellent for CPU-bound work               |
| `async/await`, `Task.WhenAll`     | `Parallel`, multiple CPU workers           |
| Focuses on overlapping work       | Focuses on simultaneous execution          |


Interview Trap

Concurrency does not mean parallel execution.

6. Is async/await the same as multithreading?

No.

async/await is primarily an asynchronous programming model.

It allows an application to avoid blocking a thread while waiting for an asynchronous operation such as:

HTTP request
Database call
File I/O
Network operation

Example:

await httpClient.GetAsync(url);

The thread does not need to remain blocked while the I/O operation is pending.

Important
async ≠ new thread
async ≠ parallelism
Task ≠ thread
7. What is a Task?

Task represents an asynchronous operation.

It is an abstraction over work and its eventual completion.

Example:

Task task = Task.Run(() =>
{
    DoWork();
});

A Task does not necessarily mean a dedicated thread exists for that task.

8. Task vs Thread
| Thread                      | Task                                 |
| --------------------------- | ------------------------------------ |
| Actual execution resource   | Abstraction representing work        |
| More expensive              | Lightweight                          |
| Manual lifecycle management | Runtime manages scheduling           |
| `Start()`                   | Usually scheduled automatically      |
| `Join()`                    | `await`                              |
| Can use dedicated thread    | Usually uses ThreadPool or async I/O |
| Low-level                   | Higher-level                         |

Interview Answer

Thread is an execution mechanism, while Task is a higher-level abstraction representing asynchronous work or its completion.

9. What is the ThreadPool?

The .NET ThreadPool is a runtime-managed collection of reusable worker threads.

Instead of creating a new thread for every small operation, applications can reuse ThreadPool threads.

Common users include:

Task.Run
many ThreadPool work items
asynchronous continuations
framework infrastructure
Benefits
Thread reuse
Lower thread-creation overhead
Runtime-managed scheduling
Better scalability for short-lived work
Important

ThreadPool threads are limited resources.

Blocking them unnecessarily can cause ThreadPool starvation.

10. What is ThreadPool starvation?

ThreadPool starvation occurs when available ThreadPool worker threads are occupied with blocking or long-running work, preventing other work from executing promptly.

Common Causes
task.Result;
task.Wait();
Thread.Sleep(...);

Other causes:

Blocking I/O
Excessive CPU work
Too many Task.Run operations
Long-running synchronous operations
Excessive parallelism
ASP.NET Core Example

Bad:

public IActionResult Get()
{
    var result = service.GetDataAsync().Result;

    return Ok(result);
}

Better:

public async Task<IActionResult> Get()
{
    var result = await service.GetDataAsync();

    return Ok(result);
}
11. What is a race condition?

A race condition occurs when the result depends on the timing/interleaving of concurrent operations.

Example:

counter++;

This looks like one operation but conceptually involves:

Read counter
Add 1
Write counter

Two threads can interfere with each other.

12. Why is counter++ not thread-safe?

Because it is not an atomic operation.

Conceptually:

Thread A → Read 10
Thread B → Read 10

Thread A → Write 11
Thread B → Write 11

Expected:

12

Actual:

11
13. How do you make a shared counter thread-safe?
Option 1: lock
private readonly object _lock = new();

private int _counter;

lock (_lock)
{
    _counter++;
}
Option 2: Interlocked
Interlocked.Increment(ref _counter);

For a simple atomic counter, Interlocked is usually preferable.

14. What is thread safety?

Thread safety means code behaves correctly when accessed concurrently by multiple threads.

Thread-safe code correctly handles:

Shared state
Concurrent access
Synchronization
Memory visibility
Atomic operations
Dangerous Combination
Shared
+
Mutable
+
Concurrent access
=
Potential race condition
15. What is a critical section?

A critical section is a portion of code that accesses shared state and must not be executed concurrently by conflicting operations.

Example:

lock (_lock)
{
    balance -= amount;
}

Only one thread can execute the protected section at a time for the same lock object.

16. What is lock in C#?

lock provides mutual exclusion.

Example:

private readonly object _sync = new();

lock (_sync)
{
    _counter++;
}

Conceptually, it is based on Monitor.

Important
Process-local
Synchronous
Protects a critical section
Cannot contain await
Must use the same synchronization object for all relevant accesses
17. Can you use await inside lock?

No.

This is invalid:

lock (_sync)
{
    await DoSomethingAsync();
}
Why?

A lock holds a synchronous monitor across the critical section, while await may suspend execution and resume later.

For async mutual exclusion, use SemaphoreSlim.

Example:

await _semaphore.WaitAsync();

try
{
    await DoSomethingAsync();
}
finally
{
    _semaphore.Release();
}
18. What is Monitor?

Monitor provides synchronization functionality behind the C# lock statement.

Example:

Monitor.Enter(_sync);

try
{
    // Critical section
}
finally
{
    Monitor.Exit(_sync);
}

It also provides:

TryEnter
Wait
Pulse
PulseAll
Interview Point
lock

is simpler syntax for common monitor-based mutual exclusion.

19. What is SemaphoreSlim?

SemaphoreSlim limits the number of concurrent operations.

Example:

private readonly SemaphoreSlim _semaphore =
    new(3, 3);

At most three operations can enter the protected region simultaneously.

await _semaphore.WaitAsync();

try
{
    await ProcessAsync();
}
finally
{
    _semaphore.Release();
}
Common Uses
Limit concurrent API calls
Limit database operations
Async mutual exclusion with capacity 1
Protect limited resources
20. lock vs SemaphoreSlim
l| `lock`                    | `SemaphoreSlim`                   |
| ------------------------- | --------------------------------- |
| Synchronous               | Supports async waiting            |
| Mutual exclusion          | Can allow N concurrent operations |
| Cannot use `await` inside | `WaitAsync()` supports `await`    |
| Process-local             | Process-local                     |
| Simple and fast           | More flexible                     |
| Uses Monitor              | Semaphore-based                   |

21. What is Mutex?

Mutex is an OS-level synchronization primitive.

Unlike lock, a named mutex can coordinate processes on the same machine.

Example:

using Mutex mutex = new(false, "MyApplicationMutex");

mutex.WaitOne();

try
{
    // Critical section
}
finally
{
    mutex.ReleaseMutex();
}
Important
More expensive than lock
Can coordinate processes on the same machine
Not a distributed lock
Not generally async-friendly
22. What is ReaderWriterLockSlim?

It is useful when shared state is:

Many reads
Few writes

Multiple readers can enter simultaneously.

Only one writer can enter at a time.

Readers:
R1 ─┐
R2 ─┼─ allowed together
R3 ─┘

Writer:
W1 ───── exclusive
Important

It is not a general replacement for lock.

It is also not designed for use across await.

23. What is Interlocked?

Interlocked provides atomic operations.

Examples:

Interlocked.Increment(ref counter);

Interlocked.Decrement(ref counter);

Interlocked.Add(ref counter, 10);

Interlocked.Exchange(ref value, 100);

It also provides:

Interlocked.CompareExchange(...)
Best Use

Simple atomic state changes such as:

Counters
Flags
Reference replacement
Lock-free algorithms
24. What is Compare-And-Swap?

CompareExchange performs an atomic conditional update.

Conceptually:

If current value == expected value
    replace it
Else
    do nothing

Example:

Interlocked.CompareExchange(
    ref value,
    newValue,
    expectedValue);

This is commonly used to implement lock-free state transitions.

25. What is volatile?

volatile is related primarily to memory visibility and ordering.

Example:

private volatile bool _stop;

It helps ensure reads/writes are observed appropriately across threads.

Important

volatile does NOT make compound operations atomic.

This is still unsafe:

volatile int counter;

counter++;

Use:

Interlocked.Increment(ref counter);

for atomic increment.

26. volatile vs Interlocked
| volatile                                 | Interlocked                           |
| ---------------------------------------- | ------------------------------------- |
| Visibility/order semantics               | Atomic operations                     |
| Does not make compound operations atomic | Provides atomic read-modify-write     |
| Useful for simple flags/state            | Counters/state transitions            |
| Does not replace locking                 | Can avoid locks for simple operations |


27. What is immutability and why does it help multithreading?

An immutable object cannot change after creation.

Example:

public sealed class Customer
{
    public string Name { get; }
    public int Age { get; }

    public Customer(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

Multiple threads can safely read the same immutable object without coordinating mutations.

Benefits
Less synchronization
Easier reasoning
Safer sharing
Easier testing
Fewer race conditions
Important

Immutability and thread safety are related but not identical concepts.

28. Are C# records immutable?

Not automatically.

A record provides:

Value-based equality
Convenient initialization
with expressions
Concise syntax

But reference-type properties can still refer to mutable objects.

Example:

public record Order(List<string> Items);

The Order record itself has value-like semantics, but the List<string> can still be modified.

29. What is a deadlock?

A deadlock occurs when two or more operations wait indefinitely for resources held by each other.

Example:

Thread A:
Lock A → waits for Lock B

Thread B:
Lock B → waits for Lock A

Neither can continue.

30. What are the four Coffman conditions?

Deadlock requires all four conditions:

1. Mutual Exclusion

A resource can be held by only one operation at a time.

2. Hold and Wait

A thread holds one resource while waiting for another.

3. No Preemption

A resource cannot simply be forcibly taken away.

4. Circular Wait

A circular dependency exists.

A waits for B
B waits for C
C waits for A
31. How do you prevent deadlocks?
1. Consistent lock ordering

Always acquire locks in the same order.

Lock A
Lock B

Never:

Thread 1 → A → B
Thread 2 → B → A
2. Keep critical sections small
3. Avoid nested locks when possible
4. Don't perform external I/O while holding locks
5. Use timeouts where appropriate
6. Reduce shared mutable state
7. Prefer higher-level concurrency primitives
32. Deadlock vs Race Condition
| Deadlock                   | Race Condition                               |
| -------------------------- | -------------------------------------------- |
| Operations become stuck    | Result becomes unpredictable                 |
| Usually involves waiting   | Usually involves unsynchronized shared state |
| No progress                | Incorrect/inconsistent results               |
| Lock ordering can cause it | Missing synchronization can cause it         |


33. What is starvation?

Starvation occurs when a thread/task waits indefinitely or for an excessive amount of time because other work continually gets access to the required resource.

Example:

High-priority work repeatedly acquires resource
↓
Low-priority work rarely gets access
↓
Low-priority work starves
34. Deadlock vs Starvation
| Deadlock                             | Starvation                             |
| ------------------------------------ | -------------------------------------- |
| Circular waiting                     | Unfair/continuous resource competition |
| No involved operation can proceed    | Some operations continue               |
| Usually permanent until intervention | May eventually recover                 |
| Lock dependency problem              | Scheduling/resource allocation problem |

35. What is livelock?

Livelock occurs when threads are active but make no useful progress.

Example:

Thread A detects conflict → backs off
Thread B detects conflict → backs off

Both retry
Both conflict again
Both back off
...

Unlike deadlock, the threads are not blocked; they are actively changing state without progressing.

36. What are concurrent collections?

.NET provides thread-safe collections in System.Collections.Concurrent.

Examples:

ConcurrentDictionary<TKey,TValue>
ConcurrentQueue<T>
ConcurrentStack<T>
ConcurrentBag<T>
BlockingCollection<T>

They are designed for concurrent access.

37. Is ConcurrentDictionary completely thread-safe?

Its supported collection operations are designed to be thread-safe.

But business workflows can still have race conditions.

Unsafe pattern:

if (!dictionary.ContainsKey(key))
{
    dictionary[key] = value;
}

Prefer atomic collection operations such as:

dictionary.TryAdd(key, value);
Important

Thread-safe collection operations do not automatically make an entire business workflow atomic.

38. What is ConcurrentQueue<T>?

A thread-safe FIFO collection.

First In
   ↓
First Out

Useful for concurrent producer-consumer scenarios.

39. What is ConcurrentStack<T>?

A thread-safe LIFO collection.

Last In
   ↓
First Out
40. What is ConcurrentBag<T>?

A thread-safe unordered collection optimized for scenarios where the same threads frequently add and remove items.

It does not guarantee FIFO ordering.

41. What is BlockingCollection<T>?

BlockingCollection<T> provides a producer-consumer abstraction with optional bounded capacity.

It can block synchronous producers/consumers when the collection is full or empty.

For modern asynchronous producer-consumer designs, Channel<T> is often a better choice.

42. What is Channel<T>?

Channel<T> is a process-local asynchronous producer-consumer mechanism.

It supports:

Async read/write
Bounded capacity
Backpressure
Multiple producers
Multiple consumers
Cancellation
Completion

Example:

Channel<int> channel =
    Channel.CreateBounded<int>(10);

await channel.Writer.WriteAsync(100);

int value =
    await channel.Reader.ReadAsync();
43. What is backpressure?

Backpressure means slowing or limiting producers when consumers cannot keep up.

Example:

Producer
   ↓
Bounded Queue
   ↓
Consumer

If the queue is full, the producer must wait or apply a configured overflow strategy.

Why it matters

Without backpressure:

Traffic spike
↓
Unlimited work
↓
Memory growth
↓
CPU/DB/API overload
↓
System failure
44. Task.WhenAll vs Parallel.ForEachAsync
Task.WhenAll

Best when you already have a collection of asynchronous operations.

Task[] tasks =
[
    CallApi1Async(),
    CallApi2Async(),
    CallApi3Async()
];

await Task.WhenAll(tasks);
Parallel.ForEachAsync

Useful when processing many items with controlled parallelism.

await Parallel.ForEachAsync(
    items,
    new ParallelOptions
    {
        MaxDegreeOfParallelism = 5
    },
    async (item, token) =>
    {
        await ProcessAsync(item, token);
    });
Mental Model
WhenAll
→ Coordinate known async operations

Parallel.ForEachAsync
→ Process many items with controlled concurrency
45. When should you use Parallel.For?

Use it primarily for independent CPU-bound work.

Example:

Parallel.For(
    0,
    1000,
    i =>
    {
        ProcessCpuWork(i);
    });

Avoid using it blindly for:

Database calls
External HTTP calls
Unbounded I/O
Work that already has asynchronous APIs
46. Task.Run vs Parallel
Task.Run

Schedules work asynchronously, commonly using ThreadPool threads.

Useful for:

CPU-bound work that should be moved off the caller thread
Certain desktop/UI scenarios
Parallel

Designed specifically for parallel loops/iteration.

Useful for:

CPU-bound batch processing
Data parallelism
ASP.NET Core

Do not use Task.Run simply to make asynchronous I/O "async".

Bad:

await Task.Run(() =>
    httpClient.GetAsync(url));

Prefer:

await httpClient.GetAsync(url);
47. What is CPU-bound work?

CPU-bound work spends most of its time using CPU resources.

Examples:

Image processing
Encryption
Compression
Large calculations
CPU-heavy transformations

Parallelism can improve throughput if enough CPU resources are available.

48. What is I/O-bound work?

I/O-bound work spends significant time waiting for external resources.

Examples:

Database
HTTP API
File system
Network
Cloud storage

Asynchronous programming is usually preferred.

await database.ExecuteAsync(...);
49. Why should you avoid blocking async code?

Bad:

var result = GetDataAsync().Result;

or:

GetDataAsync().Wait();

Potential problems:

ThreadPool starvation
Reduced throughput
Increased latency
Deadlocks in some synchronization-context environments

Prefer:

var result = await GetDataAsync();
50. What is "async all the way"?

If an operation is asynchronous, allow the asynchronous nature to propagate through the call chain.

Prefer:

Controller
   ↓ await
Service
   ↓ await
Repository
   ↓ await
Database

Avoid:

Controller
   ↓ .Result
Service
   ↓ .Wait()
Repository
51. What is CancellationToken?

CancellationToken provides cooperative cancellation.

Example:

public async Task ProcessAsync(
    CancellationToken cancellationToken)
{
    cancellationToken.ThrowIfCancellationRequested();

    await Task.Delay(
        1000,
        cancellationToken);
}

Cancellation does not forcibly kill a thread.

The operation must cooperate.

52. Cancellation vs Timeout
Cancellation

Means:

Stop because the caller/system requested cancellation.

Timeout

Means:

Stop because an operation exceeded an allowed duration.

A timeout can be implemented using a cancellation token.

using CancellationTokenSource cts =
    new(TimeSpan.FromSeconds(5));
53. What is IAsyncEnumerable<T>?

IAsyncEnumerable<T> represents an asynchronous sequence.

Example:

await foreach (var item in GetItemsAsync())
{
    Process(item);
}

It is useful when data becomes available incrementally.

Example

Large database/export stream:

Fetch item
↓
Process item
↓
Fetch next item
↓
Process next item

Instead of loading everything into memory first.

54. Does IAsyncEnumerable<T> mean parallel processing?

No.

It provides asynchronous iteration.

This:

await foreach (var item in items)
{
    await ProcessAsync(item);
}

is generally sequential processing.

Parallelism requires explicit coordination.

55. What is ValueTask<T>?

ValueTask<T> is a lightweight value-type representation of an asynchronous operation.

It can reduce allocations when an operation frequently completes synchronously.

Example:

public ValueTask<int> GetValueAsync()
{
    return ValueTask.FromResult(100);
}
Important

Do not use ValueTask<T> everywhere.

Task<T> is generally the better default.

Use ValueTask<T> when:

Sync completion is common
It is a hot path
Allocation reduction has been measured
API semantics benefit from it
56. What is ThreadPool starvation vs deadlock?
ThreadPool starvation
ThreadPool threads
↓
Blocked/busy
↓
No threads available quickly
↓
Queued work waits
Deadlock
Thread A → waits for B
Thread B → waits for A

Both can cause requests to hang, but their causes are different.

57. Can multiple ASP.NET Core requests execute concurrently?

Yes.

ASP.NET Core is designed to process many requests concurrently.

It does not create one dedicated thread per request.

For I/O-bound operations, asynchronous APIs allow request processing to scale without unnecessarily blocking ThreadPool threads.

58. Is a Singleton service thread-safe automatically?

No.

Dependency Injection lifetime does not automatically make code thread-safe.

A Singleton can be accessed concurrently by many requests.

This is dangerous:

public class CounterService
{
    private int _counter;

    public void Increment()
    {
        _counter++;
    }
}

If registered as Singleton, _counter requires appropriate synchronization.

59. Are Scoped services automatically thread-safe?

No.

Scoped lifetime controls object lifetime within a request scope.

It does not automatically make internal state safe for concurrent access.

If multiple tasks concurrently access the same scoped object, its implementation must still be safe for that usage.

Important Example

EF Core DbContext is not thread-safe.

Do not execute multiple concurrent operations against the same DbContext instance.

60. Is static mutable state dangerous?

Yes.

Example:

private static int _count;

Static state is shared across requests/threads within the process.

It can introduce:

Race conditions
Difficult testing
Unexpected state sharing
Memory retention
Concurrency bugs
61. What is thread confinement?

Thread confinement means mutable state is accessed by only one thread or execution context.

Instead of synchronizing shared mutable state, avoid sharing it.

Conceptually:

Thread A → owns State A
Thread B → owns State B

This can significantly reduce synchronization requirements.

62. What is message passing?

Instead of sharing mutable memory, concurrent components communicate through messages.

Example:

Producer
   ↓
Channel
   ↓
Consumer

Advantages:

Less shared mutable state
Clear ownership
Natural buffering
Backpressure
Easier concurrency reasoning
63. What is optimistic concurrency?

Optimistic concurrency assumes conflicts are relatively uncommon.

A version/concurrency token is checked when updating.

Example:

UPDATE Products
SET Stock = @newStock,
    Version = @newVersion
WHERE Id = @id
  AND Version = @expectedVersion;

If zero rows are updated, another operation changed the record.

Mental Model
Read
↓
Remember version
↓
Update only if version unchanged
↓
Conflict → handle/retry/reload
64. What is pessimistic concurrency?

Pessimistic concurrency assumes conflicts are likely and locks the resource while processing.

Example conceptual flow:

Begin transaction
↓
Lock required row/resource
↓
Read
↓
Modify
↓
Commit
↓
Release lock

It can prevent conflicting operations but introduces lock contention and deadlock risks.

65. Optimistic vs Pessimistic concurrency
| Optimistic                           | Pessimistic                               |
| ------------------------------------ | ----------------------------------------- |
| Detects conflicts                    | Prevents conflicts                        |
| Usually fewer locks                  | Uses locks                                |
| Good when conflicts are uncommon     | Good when conflicts are frequent/critical |
| Better scalability in many workloads | Can create contention                     |
| Conflict handling required           | Deadlock/lock management required         |

66. Why doesn't C# lock solve distributed concurrency?

Because lock is process-local.

Consider:

Server A → lock
Server B → lock

These are two different process-local locks.

They do not coordinate.

For distributed systems, use appropriate mechanisms such as:

Database transactions
Unique constraints
Optimistic concurrency
Distributed locks when justified
Message processing guarantees
Idempotency
67. What is a distributed lock?

A distributed lock coordinates ownership of a resource across multiple application instances.

Example:

Server A ─┐
          ├── Shared Lock Store
Server B ─┤
          └── Resource
Server C ─┘

Common use cases:

Prevent duplicate scheduled jobs
Leader election
Resource allocation
Cache stampede protection
Important

A distributed lock should not automatically be the first choice for enforcing database business invariants.

Prefer authoritative database constraints/transactions where appropriate.

68. What is idempotency?

An operation is idempotent when repeating the same logical operation produces the same business effect.

Example:

POST /payments
Idempotency-Key: PAYMENT-123

If the client retries:

Request 1 → Payment created
Request 2 → Same logical operation
Request 3 → Same result

Only one payment should be created.

Why important?

Distributed systems commonly experience:

Timeouts
Retries
Duplicate messages
Network failures
Client retries
69. Is a database transaction enough for idempotency?

Not necessarily.

You usually need an idempotency strategy such as:

Idempotency key
Unique database constraint
Stored operation/result
Atomic insert/claim
Transaction integration

For example:

IdempotencyKey UNIQUE

can prevent duplicate logical operations.

70. What is a producer-consumer pattern?

Producer creates work.

Consumer processes work.

A queue/buffer sits between them.

Producer
   ↓
Queue / Channel
   ↓
Consumer

Benefits:

Decoupling
Buffering
Controlled concurrency
Backpressure
Background processing
71. In-memory queue vs message broker
| In-memory Channel              | Message Broker                 |
| ------------------------------ | ------------------------------ |
| Process-local                  | Distributed                    |
| Very fast                      | Network-based                  |
| Data lost if process crashes   | Can provide durability         |
| Simple                         | More infrastructure            |
| Good for local background work | Good for distributed workflows |


Examples of message-broker technologies:

Azure Service Bus
RabbitMQ
Kafka
72. What is synchronization overhead?

Synchronization can itself reduce performance.

Examples:

Lock contention
Context switching
Thread scheduling
Atomic operations
Waiting
Queue contention

Therefore:

More synchronization does not automatically mean better correctness/performance.

The goal is correct synchronization with minimal unnecessary contention.

73. What is lock contention?

Lock contention occurs when multiple threads compete for the same lock.

Example:

Thread A → holds lock
Thread B → waiting
Thread C → waiting
Thread D → waiting

Too much contention can reduce throughput.

Reduce contention by:
Keeping critical sections small
Avoiding unnecessary locks
Avoiding external I/O inside locks
Partitioning state
Using immutable data
Using atomic operations where appropriate
74. Why shouldn't you perform HTTP calls while holding a lock?

Bad:

lock (_sync)
{
    awaitSomethingOrBlockingHttpCall();
}

Problems:

Long lock duration
Other threads wait
Reduced throughput
Potential deadlock patterns
External dependency latency becomes lock latency

Better:

Acquire/update required state
↓
Release lock
↓
Call external service
↓
Apply result using appropriate concurrency control
75. What is rate limiting vs concurrency limiting?
Concurrency limiting

Controls how many operations are active simultaneously.

Example:

Maximum 10 concurrent API calls
Rate limiting

Controls how many operations are allowed within a time period.

Example:

Maximum 100 requests/second

They solve different problems.

76. What is Amdahl's Law?

Amdahl's Law explains the theoretical speedup limit of parallelization.

If part of a workload must remain sequential, that portion limits the maximum speedup.

Conceptually:

More CPU cores
       ↓
Parallel portion gets faster
       ↓
Sequential portion remains
       ↓
Maximum speedup is limited
Interview Point

Adding more threads does not guarantee proportional performance improvement.

77. Why can too much parallelism reduce performance?

Because excessive concurrency can cause:

Context switching
ThreadPool pressure
CPU contention
Lock contention
Memory pressure
Database connection exhaustion
External API throttling
Increased GC pressure

Therefore:

Parallelism must be controlled and measured.

78. What happens if you call Task.WhenAll on thousands of API calls?

Potentially problematic.

Example:

await Task.WhenAll(
    thousandsOfApiCalls);

The application may create huge downstream pressure.

Possible problems:

API throttling
Connection exhaustion
Memory usage
Increased latency
Retry storms
Service overload

Use controlled concurrency where necessary.

79. How would you limit concurrent API calls?

Using SemaphoreSlim:

SemaphoreSlim semaphore = new(10);

await semaphore.WaitAsync();

try
{
    await CallApiAsync();
}
finally
{
    semaphore.Release();
}

Now at most 10 operations can execute concurrently through that semaphore.

80. What is the difference between Thread.Sleep and Task.Delay?
Thread.Sleep

Blocks the current thread.

Thread.Sleep(1000);
Task.Delay

Creates an asynchronous delay.

await Task.Delay(1000);

During an asynchronous delay, the ThreadPool thread does not need to remain blocked.

Interview Answer

Thread.Sleep blocks a thread; Task.Delay asynchronously waits without blocking a thread for the delay duration.

81. What happens when an exception occurs inside a Task?

The Task becomes faulted.

Example:

Task task = Task.Run(() =>
{
    throw new InvalidOperationException();
});

try
{
    await task;
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

With await, the exception is observed when awaiting the task.

82. What is AggregateException?

AggregateException can represent one or more exceptions from multiple concurrent operations.

For example, APIs such as synchronous Task.WaitAll can expose exceptions through AggregateException.

With await Task.WhenAll(...), exception observation is generally handled through the awaited operation, while the combined task still represents failures from the coordinated operations.

Interview Point

Understand the difference between:

await

and blocking APIs such as:

Wait()
WaitAll()
Result
83. What is fire-and-forget?

Fire-and-forget means starting work without awaiting its completion.

Example:

_ = ProcessAsync();

This can be dangerous in ASP.NET Core because:

Request scope may end
Exceptions may be missed
Cancellation/lifetime may be wrong
Dependencies may be disposed
Work may be lost during process shutdown

For reliable background work, prefer:

BackgroundService
IHostedService
Channel<T>
Durable message brokers
84. How should background work be handled in ASP.NET Core?

For reliable application-managed background work:

HTTP Request
     ↓
Persist/queue work
     ↓
BackgroundService
     ↓
Process work

For distributed/durable processing:

API
 ↓
Message Broker
 ↓
Worker

Examples:

Azure Service Bus
RabbitMQ
Kafka
85. Is ConcurrentDictionary enough for inventory management?

Not necessarily.

Consider:

Check stock
↓
Stock available?
↓
Decrease stock

These are multiple business operations.

Even if individual dictionary methods are thread-safe, the entire business operation may not be atomic.

Better approaches include:

Atomic database update
Optimistic concurrency
Database transaction
Appropriate locking
Idempotency

Example:

UPDATE Inventory
SET Quantity = Quantity - @requested
WHERE ProductId = @productId
  AND Quantity >= @requested;

Then check affected rows.

86. How would you prevent overselling inventory?

A strong product-company answer:

I would enforce the inventory invariant at the authoritative database layer using an atomic conditional update or optimistic/pessimistic concurrency, rather than relying only on an in-memory lock.

Example:

UPDATE Inventory
SET Quantity = Quantity - @quantity
WHERE ProductId = @productId
  AND Quantity >= @quantity;

If affected rows = 1:

Reservation succeeded

If affected rows = 0:

Insufficient stock or concurrency conflict
87. How would you design concurrent order processing?

A good high-level answer:

Client
  ↓
Order API
  ↓
Validate
  ↓
Idempotency check
  ↓
Create Order
  ↓
Reserve Inventory
  ↓
Process Payment
  ↓
Publish/Queue Events
  ↓
Shipping / Notification

Important mechanisms:

Idempotency
Atomic inventory update
Optimistic/pessimistic concurrency
Database transactions
Outbox pattern
Message broker
Retry with backoff
Cancellation
Timeouts
Circuit breaker
Idempotent consumers
Observability
Scenario-Based Interview Questions
88. Two users buy the last item simultaneously. What happens?

Expected answer:

I would not rely only on an application-level lock, because multiple application instances may process the requests. I would enforce the inventory invariant in the database using an atomic conditional update or appropriate concurrency control.

Example:

UPDATE Inventory
SET Quantity = Quantity - 1
WHERE ProductId = @productId
  AND Quantity > 0;

Only one request can successfully decrement the final quantity.

89. Your API latency suddenly increases under load. What would you investigate?

Check:

ThreadPool starvation
Blocking .Result / .Wait()
Database connection pool exhaustion
External API latency
Lock contention
Excessive concurrency
Retry storms
CPU utilization
GC pressure
ThreadPool queueing
Database locks
p95/p99 latency
Downstream throttling
90. An API calls five independent services. How would you optimize it?

Instead of:

Call A
 ↓
Call B
 ↓
Call C
 ↓
Call D
 ↓
Call E

Start independent operations concurrently:

Task<A> a = GetAAsync();
Task<B> b = GetBAsync();
Task<C> c = GetCAsync();
Task<D> d = GetDAsync();
Task<E> e = GetEAsync();

await Task.WhenAll(a, b, c, d, e);

Then add:

Timeouts
Cancellation
Retry policy
Circuit breaker
Concurrency limits where necessary
Partial failure handling
91. One downstream service is slow. Should you create more threads?

Not necessarily.

If the operation is I/O-bound, adding threads does not solve the underlying latency.

Instead investigate:

Async I/O
Timeout
Connection pool
Downstream latency
Concurrency limits
Caching
Circuit breaker
Retry strategy
92. Your ASP.NET Core application uses .Result everywhere. What problems can it cause?

Potential problems:

ThreadPool starvation
Increased latency
Reduced throughput
Blocking request threads
Deadlocks in some environments
Poor scalability

Refactor to async all the way:

Controller
 ↓ await
Service
 ↓ await
Repository
 ↓ await
Database
93. How would you process 1 million CPU-heavy records?

Do not create one thread per record.

Use controlled parallelism.

Possible approach:

Input
 ↓
Partition/batch
 ↓
Controlled parallel processing
 ↓
Aggregate results

Use:

Parallel.ForEach
Parallel.ForEachAsync when appropriate
bounded workers
partitioning
cancellation
thread-safe aggregation

Measure actual performance.

94. How would you process 1 million database records?

Avoid loading everything into memory.

Consider:

Pagination
Streaming
IAsyncEnumerable<T>
Batching
Controlled concurrency
Database-side filtering
Bulk operations where appropriate
Connection pool limits

The best solution depends on the database and workload.

95. How would you handle duplicate payment requests?

Use idempotency.

Request
 ↓
Idempotency Key
 ↓
Atomic claim/store
 ↓
Process payment once
 ↓
Store result
 ↓
Return same result for retry

Also use a durable shared store/database in a multi-instance production environment.

96. What if the payment API times out after charging the customer?

Do not automatically assume:

Timeout = Payment Failed

The payment may have succeeded but the response was lost.

Use:

Idempotency key
Payment status query
Webhook
Reconciliation
Durable payment state
Idempotent processing
97. How would you prevent duplicate background jobs across multiple servers?

A local lock is insufficient.

Possible approaches:

Distributed lock
Database lease
Scheduler with distributed coordination
Message queue
Unique job constraint

Prefer the simplest mechanism that correctly enforces the business requirement.

98. What would you monitor in a highly concurrent ASP.NET Core application?

Important metrics:

Application
Request rate
Throughput
p50 latency
p95 latency
p99 latency
Error rate
Threading
ThreadPool usage
ThreadPool queueing
ThreadPool starvation indicators
Lock contention
Database
Connection pool usage
Query duration
Blocking
Deadlocks
CPU
External Services
Latency
Timeout rate
Error rate
Rate-limit responses
Runtime
CPU
Memory
GC
Allocation rate
Product-Company Design Questions
99. Design a thread-safe in-memory counter.

Possible solution:

private int _count;

public int Increment()
{
    return Interlocked.Increment(ref _count);
}

public int GetCount()
{
    return Volatile.Read(ref _count);
}

Use Interlocked for atomic modification.

100. Design a thread-safe inventory reservation mechanism.

Requirements:

Initial stock = 100
Multiple concurrent requests
Never allow negative stock

Possible in-memory solution:

public bool TryReserve(int quantity)
{
    while (true)
    {
        int current =
            Volatile.Read(ref _stock);

        if (current < quantity)
        {
            return false;
        }

        int updated =
            current - quantity;

        int original =
            Interlocked.CompareExchange(
                ref _stock,
                updated,
                current);

        if (original == current)
        {
            return true;
        }
    }
}

For a real distributed application, prefer an authoritative database concurrency mechanism.

101. Design a bounded worker system.

Architecture:

Producer
   ↓
Bounded Channel
   ↓
Worker 1
Worker 2
Worker 3

Important concepts:

Bounded capacity
Backpressure
Multiple consumers
Cancellation
Graceful shutdown
Error handling
Retry
Dead-letter handling
Monitoring
102. Design a high-throughput API aggregation service.

Requirements:

Request
 ↓
Call 5 independent APIs
 ↓
Combine results
 ↓
Return response

Solution:

ASP.NET Core
     ↓
Start independent async operations
     ↓
Task.WhenAll
     ↓
Timeout/Cancellation
     ↓
Partial failure strategy
     ↓
Response

Add:

Connection pooling
Concurrency limits
Circuit breaker
Retry with jitter
Caching
Observability
103. How would you improve a multithreaded application that is slower than the single-threaded version?

Do not immediately add more threads.

Investigate:

Is the workload CPU-bound?
Is it actually parallelizable?
Is there lock contention?
Is there excessive synchronization?
Is there ThreadPool pressure?
Are tasks too small?
Is context switching high?
Is memory bandwidth the bottleneck?
Is the database/external API the bottleneck?
Is there excessive allocation?
Is concurrency too high?
Has the workload actually been benchmarked?
Principle

Measure first, optimize second.

Common Interview Traps
Trap 1

"Async means another thread."

Wrong.

Async primarily means non-blocking asynchronous execution.

Trap 2

"Task is a thread."

Wrong.

Task represents asynchronous work/completion.

Trap 3

"More threads always improve performance."

Wrong.

Too many threads can cause:

Context switching
Contention
Memory overhead
ThreadPool pressure
Trap 4

"volatile makes a variable thread-safe."

Wrong.

volatile does not make compound operations atomic.

Trap 5

"ConcurrentDictionary makes the entire application thread-safe."

Wrong.

Only its supported collection operations are thread-safe.

Business workflows may still have race conditions.

Trap 6

"lock works across multiple servers."

Wrong.

lock is process-local.

Trap 7

"Task.WhenAll means parallel CPU execution."

Wrong.

It coordinates asynchronous operations.

The operations may be concurrent, but not necessarily CPU-parallel.

Trap 8

"Task.Run is required for every async method."

Wrong.

Do not wrap normal asynchronous I/O in Task.Run.

Trap 9

"CancellationToken forcibly kills a thread."

Wrong.

Cancellation is cooperative.

Trap 10

"Thread.Sleep is the same as Task.Delay."

Wrong.

Thread.Sleep blocks the thread.

Task.Delay provides asynchronous delay.

Trap 11

"A Singleton is automatically thread-safe."

Wrong.

A Singleton with mutable shared state must still be synchronized correctly.

Trap 12

"Scoped means thread-safe."

Wrong.

DI lifetime and thread safety are different concepts.

Rapid-Fire Questions
104. What is the difference between Thread.Sleep and Task.Delay?

Thread.Sleep blocks the current thread; Task.Delay asynchronously waits.

105. What is the difference between lock and Monitor?

lock is simpler syntax built on monitor-based synchronization.

106. What is the difference between lock and SemaphoreSlim?

lock provides synchronous mutual exclusion; SemaphoreSlim supports async waiting and can allow multiple concurrent entrants.

107. What is the difference between Interlocked and lock?

Interlocked provides atomic operations for simple state changes; lock protects a larger critical section involving multiple operations.

108. What is the difference between volatile and Interlocked?

volatile provides visibility/order semantics; Interlocked provides atomic read-modify-write operations.

109. What is the difference between concurrency and parallelism?

Concurrency is overlapping progress; parallelism is simultaneous execution.

110. What is the difference between CPU-bound and I/O-bound work?

CPU-bound work spends time computing; I/O-bound work spends significant time waiting for external resources.

111. What is the difference between deadlock and starvation?

Deadlock involves circular waiting; starvation occurs when work continually fails to obtain the required resource.

112. What is the difference between Channel and ConcurrentQueue?

ConcurrentQueue<T> is a thread-safe FIFO collection.

Channel<T> provides an asynchronous producer-consumer abstraction with features such as bounded capacity, backpressure, completion, and cancellation.

113. What is the difference between optimistic and pessimistic concurrency?

Optimistic concurrency detects conflicts during update; pessimistic concurrency prevents conflicts by locking resources.

114. What is the difference between local and distributed concurrency control?

Local synchronization coordinates threads/processes within a machine/process.

Distributed concurrency control coordinates multiple application instances through shared infrastructure or authoritative data stores.

Senior-Level Interview Questions
115. Why can async code still have race conditions?

Because await does not automatically synchronize shared state.

Example:

int balance = 100;

async Task WithdrawAsync()
{
    if (balance >= 100)
    {
        await Task.Delay(10);

        balance -= 100;
    }
}

Two concurrent calls can both observe:

balance = 100

before either performs the update.

Lesson
async ≠ thread-safe
116. Why is ConcurrentDictionary.GetOrAdd not always enough for business logic?

Because a business operation may contain multiple steps.

For example:

Check
↓
Calculate
↓
Update
↓
Publish event

The collection's individual atomic operations do not automatically make the entire workflow atomic.

Use appropriate:

Locking
Transactions
Database constraints
Optimistic concurrency
Idempotency
Distributed coordination
117. How do you choose between lock, Interlocked, SemaphoreSlim, Channel, and database concurrency?
lock

Use for:

Short synchronous critical section
Interlocked

Use for:

Simple atomic state transition/counter
SemaphoreSlim

Use for:

Async mutual exclusion or concurrency limiting
Channel<T>

Use for:

Producer-consumer + buffering + backpressure
Database concurrency

Use when:

Business state is authoritative in a shared database
118. What is the most important principle in multithreading?

Avoid unnecessary shared mutable state.

Prefer:

Immutable state
+
Message passing
+
Controlled concurrency
+
Small critical sections

instead of:

Large shared mutable state
+
Many locks
+
Uncontrolled concurrency
Final Product-Company Mental Model

When solving a multithreading problem, think in this order:

1. What work is being performed?
        ↓
2. CPU-bound or I/O-bound?
        ↓
3. Does it need concurrency?
        ↓
4. Does it need parallelism?
        ↓
5. Is state shared?
        ↓
6. Is the shared state mutable?
        ↓
7. What synchronization is required?
        ↓
8. Can shared state be avoided?
        ↓
9. Is concurrency controlled?
        ↓
10. What happens during cancellation?
        ↓
11. What happens when an operation fails?
        ↓
12. What happens when the system is distributed?
        ↓
13. How will performance be measured?
Most Important Concepts to Remember
Thread
    ↓
Execution resource

Task
    ↓
Abstraction representing asynchronous work/completion

async/await
    ↓
Asynchronous programming model

ThreadPool
    ↓
Reusable runtime-managed worker threads

Concurrency
    ↓
Overlapping progress

Parallelism
    ↓
Simultaneous execution

lock
    ↓
Synchronous mutual exclusion

SemaphoreSlim
    ↓
Async synchronization / concurrency limiting

Interlocked
    ↓
Atomic operations

volatile
    ↓
Visibility/order semantics

Concurrent Collections
    ↓
Thread-safe collection operations

Channel
    ↓
Async producer-consumer + backpressure

CancellationToken
    ↓
Cooperative cancellation

Deadlock
    ↓
Circular waiting

Starvation
    ↓
Unable to obtain resources/progress

Immutability
    ↓
Avoid shared mutable state

Optimistic Concurrency
    ↓
Detect conflict

Pessimistic Concurrency
    ↓
Prevent conflict with locking

Distributed Concurrency
    ↓
Coordinate multiple application instances

Idempotency
    ↓
Safe retries / duplicate operations
Interview Answer Formula

For a senior/product-company multithreading question, structure the answer like this:

1. Definition
2. Why it matters
3. How it works
4. C#/.NET mechanism
5. Example
6. Risks/trade-offs
7. ASP.NET Core implication
8. Distributed-system implication
9. Production recommendation

Example:

How would you prevent overselling inventory?

Strong answer:

I would treat inventory as shared mutable business state and enforce the invariant at the authoritative database layer.
For a simple reservation, I could use an atomic conditional update such as UPDATE ... 
WHERE Quantity >= requestedQuantity. If the update affects one row, the reservation succeeded; 
otherwise there was insufficient stock or a concurrency conflict. For more complex workflows,
I would use optimistic or pessimistic concurrency with a transaction. At the API level,
I would also use idempotency to handle retries. I would not rely only on an in-memory C# lock,
because multiple application instances can process requests concurrently.

Final One-Line Summary

Production-quality multithreading is not about creating more threads; it is about choosing the right concurrency model, avoiding unnecessary shared mutable state, synchronizing only where required, controlling concurrency, handling cancellation and failures, and designing correctly for distributed execution.
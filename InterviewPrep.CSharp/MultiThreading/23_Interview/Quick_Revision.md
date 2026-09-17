# Multithreading — Quick Revision

## 1. Core Mental Model

```text
Thread
  ↓
Execution resource

Task
  ↓
Abstraction representing work/completion

async/await
  ↓
Non-blocking asynchronous programming

Concurrency
  ↓
Multiple operations make overlapping progress

Parallelism
  ↓
Multiple operations execute simultaneously

Synchronization
  ↓
Protect shared state

Cancellation
  ↓
Cooperative stopping

Backpressure
  ↓
Prevent producers from overwhelming consumers

Idempotency
  ↓
Safe retries / duplicate requests

Distributed concurrency
  ↓
Coordinate multiple application instances
2. Thread
Definition

A thread is the smallest unit of execution within a process.

Remember
Process has its own memory space.
Threads inside a process share heap/static memory.
Each thread has its own stack.
Threads are relatively expensive compared with tasks.
Too many threads can cause context switching and memory overhead.
Important APIs
Thread.Start();
Thread.Join();
Thread.Sleep();
Thread.CurrentThread.ManagedThreadId;
3. Thread vs Task
Thread	Task
Execution mechanism	Higher-level abstraction
More expensive	Lightweight
Manual lifecycle	Runtime scheduling
Start()	await
Low-level	High-level
Dedicated execution resource	May use ThreadPool or async I/O
Remember

Task is not a Thread.

4. Concurrency vs Parallelism
Concurrency

Multiple operations make progress during overlapping periods.

Parallelism

Multiple operations execute simultaneously.

Concurrency:

A ───────
    B ───────
       C ───────


Parallelism:

Core 1 → A ─────
Core 2 → B ─────
Core 3 → C ─────
Remember

Concurrency = overlapping progress.
Parallelism = simultaneous execution.

5. CPU-Bound vs I/O-Bound
CPU-Bound	I/O-Bound
Computation is bottleneck	Waiting is bottleneck
Image processing	HTTP
Compression	Database
Encryption	File I/O
Complex calculations	Network
Parallelism can help	Async/await usually helps
Rule
CPU-bound → Parallelism
I/O-bound  → Async I/O
6. async/await
Key Facts
async ≠ Thread
async ≠ Parallelism
async ≠ Task.Run

async/await primarily helps avoid blocking while waiting for asynchronous operations.

Example:

var result = await httpClient.GetAsync(url);
Best For
HTTP
Database
File I/O
Network
Cloud services
7. Task
Task task = DoSomethingAsync();

await task;
Common APIs
Task.Run(...)
Task.Delay(...)
Task.WhenAll(...)
Task.WhenAny(...)
Task.CompletedTask
Task.FromResult(...)
Remember

A Task represents asynchronous work or its eventual completion.

8. Task.Run

Use mainly for appropriate CPU-bound work that needs to run away from the current execution context.

await Task.Run(() =>
{
    PerformCpuWork();
});
Don't do this unnecessarily
await Task.Run(() =>
    httpClient.GetAsync(url));

If the API is already asynchronous, call it directly.

await httpClient.GetAsync(url);
9. Task.WhenAll

Use when you need all operations to complete.

Task<A> a = GetAAsync();
Task<B> b = GetBAsync();
Task<C> c = GetCAsync();

await Task.WhenAll(a, b, c);
Typical Use

Independent I/O calls.

Remember

WhenAll does not automatically mean CPU parallelism.

10. Task.WhenAny

Use when you need to know when any one task completes.

Task completed =
    await Task.WhenAny(task1, task2);
Important

WhenAny does not automatically cancel the remaining tasks.

Common Uses
First response
Timeout
Racing redundant services
First completed operation
11. WhenAll vs WhenAny
WhenAll	WhenAny
Wait for all	Wait for first completion
Aggregation	Racing/timeout
All results required	One result may be enough
Doesn't cancel automatically	Doesn't cancel automatically
WhenAll → "I need ALL."

WhenAny → "Tell me when ANY finishes."
12. ThreadPool

.NET maintains reusable worker threads.

Benefits
Thread reuse
Lower creation overhead
Runtime scheduling
Good for short-lived work
Important

ThreadPool is a limited resource.

Blocking ThreadPool threads can cause:

ThreadPool Starvation
13. ThreadPool Starvation

Occurs when ThreadPool worker threads are occupied and new work cannot get a worker promptly.

Common Causes
.Result
.Wait()
Thread.Sleep()

Also:

Blocking I/O
Long CPU operations
Excessive Task.Run
Excessive parallelism
Symptoms
High latency
Requests waiting
Timeouts
Low throughput
ThreadPool queue growth
14. Race Condition

A race condition occurs when concurrent execution produces different/incorrect results depending on timing.

Example:

_counter++;

Conceptually:

Read
 ↓
Modify
 ↓
Write

Two threads can interfere.

Dangerous Combination
Shared
+
Mutable
+
Concurrent
=
Race-condition risk
15. Thread Safety

Thread-safe code behaves correctly when accessed concurrently.

Ways to achieve it
Avoid shared mutable state
Immutability
lock
Monitor
Interlocked
SemaphoreSlim
Concurrent collections
Database concurrency control
Message passing
16. lock
private readonly object _sync = new();

lock (_sync)
{
    _counter++;
}
Use For
Short synchronous critical sections
Shared mutable state
Important
Process-local
Mutual exclusion
Cannot contain await
Same lock object must protect the relevant accesses
17. Monitor

lock uses monitor-based synchronization internally.

Explicit form:

Monitor.Enter(_sync);

try
{
    // Critical section
}
finally
{
    Monitor.Exit(_sync);
}
Remember

lock = convenient syntax for common Monitor-based synchronization.

18. SemaphoreSlim

Useful for:

Async mutual exclusion
Concurrency limiting
Resource throttling

Example:

private readonly SemaphoreSlim _semaphore =
    new(5);

await _semaphore.WaitAsync();

try
{
    await ProcessAsync();
}
finally
{
    _semaphore.Release();
}
Important
SemaphoreSlim(1)
→ Async mutual exclusion

SemaphoreSlim(5)
→ Maximum 5 concurrent operations
19. lock vs SemaphoreSlim
lock	SemaphoreSlim
Synchronous	Async-friendly
One owner	Can allow N
Cannot await inside	WaitAsync()
Short sync critical sections	Async coordination
Process-local	Process-local
20. Mutex

Mutex is an OS-level synchronization primitive.

A named mutex can coordinate processes on the same machine.

Important
Mutex
≠
Distributed Lock

It is generally heavier than lock.

21. ReaderWriterLockSlim

Useful when:

Many Readers
+
Few Writers

Multiple readers can execute concurrently.

Writer requires exclusive access.

Don't use blindly

If the workload is not read-heavy, ordinary lock or immutable snapshots may be simpler.

22. Interlocked

Provides atomic operations.

Interlocked.Increment(ref counter);

Interlocked.Decrement(ref counter);

Interlocked.Add(ref counter, 10);

Interlocked.Exchange(ref value, 100);

Also:

Interlocked.CompareExchange(...);
Best For
Counters
Flags/state transitions
Simple atomic updates
23. CompareExchange

Concept:

If current == expected
    replace with new value
Else
    don't replace

Commonly used for lock-free state transitions.

Example:

Interlocked.CompareExchange(
    ref value,
    newValue,
    expectedValue);
24. volatile

Primarily provides visibility/order semantics.

Example:

private volatile bool _stop;
Important

volatile does NOT make this atomic:

counter++;

Use:

Interlocked.Increment(ref counter);
Remember

Visibility ≠ atomicity.

25. volatile vs Interlocked
volatile	Interlocked
Visibility/order	Atomic operations
Simple state/flags	Counters/state transitions
++ still unsafe	Increment is atomic
Not a locking replacement	Can replace locking for simple operations
26. Deadlock

Deadlock occurs when operations wait indefinitely for each other's resources.

Example:

Thread A:
Lock A
 ↓
Wait for B

Thread B:
Lock B
 ↓
Wait for A
27. Coffman Conditions

All four are required for classic deadlock:

Mutual exclusion
Hold and wait
No preemption
Circular wait
Most Practical Prevention

Use consistent lock ordering.

Always:
A → B

Never:
A → B
B → A
28. Deadlock Prevention
Consistent lock ordering
Small critical sections
Avoid nested locks
Avoid external calls inside locks
Use timeouts where appropriate
Reduce shared state
Prefer higher-level primitives
29. Race vs Deadlock vs Starvation
Race	Deadlock	Starvation
Incorrect/unpredictable result	No progress	Excessive waiting
Shared-state timing	Circular waiting	Resource competition
Synchronization problem	Lock/resource dependency	Scheduling/resource allocation
30. Livelock

Threads are active but make no useful progress.

A detects conflict → backs off
B detects conflict → backs off

A retries → conflict
B retries → conflict
Difference
Deadlock → blocked
Livelock → active but not progressing
31. Concurrent Collections

Important types:

ConcurrentDictionary<TKey,TValue>
ConcurrentQueue<T>
ConcurrentStack<T>
ConcurrentBag<T>
BlockingCollection<T>
Important

Thread-safe collection operations do not automatically make an entire business workflow atomic.

32. ConcurrentDictionary

Useful for concurrent dictionary operations.

Prefer:

dictionary.TryAdd(key, value);

instead of:

if (!dictionary.ContainsKey(key))
{
    dictionary[key] = value;
}

because the second version is check-then-act.

33. ConcurrentQueue

Thread-safe FIFO.

First In
   ↓
First Out
34. ConcurrentStack

Thread-safe LIFO.

Last In
   ↓
First Out
35. ConcurrentBag

Thread-safe unordered collection.

Good for scenarios where ordering is not important.

36. BlockingCollection

Producer-consumer abstraction with blocking behavior and optional bounded capacity.

Useful for synchronous producer-consumer scenarios.

For modern asynchronous pipelines, consider:

Channel<T>
37. Channel<T>

Process-local async producer-consumer mechanism.

Supports:

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

await channel.Writer.WriteAsync(10);

int value =
    await channel.Reader.ReadAsync();
38. Backpressure

Backpressure prevents producers from overwhelming consumers.

Fast Producer
      ↓
Bounded Queue
      ↓
Slow Consumer

When the queue is full, the producer waits or applies the configured overflow behavior.

Remember

Backpressure protects the system from overload.

39. Producer-Consumer
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
40. IAsyncEnumerable

Represents an asynchronous sequence.

await foreach (var item in items)
{
    await ProcessAsync(item);
}
Useful For
Large datasets
Streaming
Incremental processing
Async database/file/network data
Important
IAsyncEnumerable
≠
Parallelism
41. ValueTask

ValueTask<T> can reduce allocations when an operation frequently completes synchronously.

public ValueTask<int> GetValueAsync()
{
    return ValueTask.FromResult(100);
}
Use Selectively

Prefer Task<T> by default.

Use ValueTask<T> when:

Sync completion is common
Hot path
Allocation reduction matters
Measurement justifies it
42. CancellationToken

Cancellation is cooperative.

public async Task ProcessAsync(
    CancellationToken token)
{
    token.ThrowIfCancellationRequested();

    await Task.Delay(1000, token);
}
Remember
CancellationToken
≠
Forcefully kill thread
43. Cancellation vs Timeout
Cancellation

Caller/system asks the operation to stop.

Timeout

Operation exceeds allowed duration.

Timeout can often be implemented through cancellation.

44. Thread.Sleep vs Task.Delay
Thread.Sleep	Task.Delay
Blocks thread	Async delay
Holds execution thread	Does not need to block a thread
Synchronous	Asynchronous
Can contribute to starvation	Better for async workflows
45. Fire-and-Forget

Avoid:

_ = ProcessAsync();

inside ASP.NET Core when reliable completion is required.

Problems:

Request may finish
Exceptions may be lost
Scoped dependencies may be disposed
Application may shut down
Work may be lost

Prefer:

BackgroundService
Channel<T>
Message Broker
46. ASP.NET Core Concurrency

ASP.NET Core handles many requests concurrently.

Important

Do not assume:

1 Request = 1 Dedicated Thread

Async I/O allows threads to be used efficiently.

Good Pattern
Controller
   ↓ await
Service
   ↓ await
Repository
   ↓ await
Database
47. Singleton Thread Safety

A Singleton can be accessed concurrently by many requests.

This is unsafe without synchronization:

private int _counter;

_counter++;

DI lifetime does not automatically provide thread safety.

48. Scoped ≠ Thread-Safe

Scoped means:

Object lifetime is scoped to a request/scope.

It does not mean:

Object can safely be accessed concurrently.

Important example:

EF Core DbContext
→ Not thread-safe

Do not perform concurrent operations on the same DbContext.

49. Static Mutable State

Dangerous:

private static List<Order> _orders;

Possible problems:

Race conditions
Shared state across requests
Memory retention
Testing problems
Concurrency bugs

Prefer avoiding global mutable state.

50. Immutability

Immutable objects cannot change after creation.

Benefits:

Safe sharing
Less synchronization
Easier reasoning
Easier testing
Fewer race conditions
Important
readonly ≠ immutable
record ≠ deeply immutable
IReadOnlyList ≠ immutable
51. Optimistic Concurrency

Assume conflicts are uncommon.

Read
 ↓
Remember Version
 ↓
Update WHERE Version = ExpectedVersion
 ↓
Success / Conflict

Example:

UPDATE Products
SET Stock = @newStock,
    Version = @newVersion
WHERE Id = @id
  AND Version = @expectedVersion;
52. Pessimistic Concurrency

Assume conflicts are likely.

Begin Transaction
 ↓
Lock Resource
 ↓
Read/Modify
 ↓
Commit
 ↓
Release
Risks
Lock contention
Deadlocks
Reduced concurrency
53. Optimistic vs Pessimistic
Optimistic	Pessimistic
Detect conflict	Prevent conflict
Version/token	Lock
Less blocking	More blocking
Good for low conflict	Good for high conflict
Conflict handling required	Deadlock/lock management
54. Local vs Distributed Concurrency
Local
Server A
 ├─ Thread 1
 ├─ Thread 2
 └─ Thread 3

lock can coordinate them.

Distributed
Server A ─┐
Server B ─┼─ Shared Resource
Server C ─┘

A C# lock cannot coordinate all servers.

55. Distributed Lock

Used to coordinate multiple application instances.

Possible mechanisms:

Database-based coordination
Redis-based coordination
Distributed scheduler
Lease-based ownership
Important

Do not automatically use distributed locks for every consistency problem.

Often better:

Atomic DB update
Unique constraint
Optimistic concurrency
Transaction
Idempotency
56. Idempotency

Same logical operation repeated should produce the same business effect.

Example:

Payment
Idempotency-Key: PAY-123

Retries should not create multiple payments.

Useful For
Payments
Orders
Message consumers
HTTP retries
Distributed workflows
57. Inventory Concurrency

Never rely only on:

if (stock >= quantity)
{
    stock -= quantity;
}

For distributed systems, prefer an atomic database operation:

UPDATE Inventory
SET Quantity = Quantity - @quantity
WHERE ProductId = @productId
  AND Quantity >= @quantity;

Check affected rows.

1 → Success
0 → Conflict / insufficient stock
58. Rate Limiting vs Concurrency Limiting
Concurrency Limit

Controls active operations.

Maximum 10 active calls
Rate Limit

Controls operations over time.

Maximum 100 requests/second
Remember

They solve different problems.

59. Too Much Parallelism

More parallelism can make a system slower.

Possible causes:

Context switching
Lock contention
CPU contention
Memory pressure
Database overload
Connection pool exhaustion
API throttling
GC pressure
ThreadPool pressure
Rule

Control and measure concurrency.

60. Amdahl's Law

Parallelization cannot eliminate sequential work.

Sequential portion
       +
Parallel portion
       ↓
Maximum speedup is limited
Remember

More CPU cores do not guarantee proportional speedup.

61. Common Scenario Answers
Scenario: Last inventory item
Atomic DB update
+
Concurrency control
+
Idempotency
Scenario: 5 independent APIs
Task.WhenAll
+
Timeout
+
Cancellation
+
Partial failure handling
Scenario: 10,000 API calls
Bounded concurrency
+
Rate limiting
+
Backpressure
Scenario: Duplicate payment
Idempotency key
+
Durable state
+
Unique constraint
Scenario: Slow background consumer
Bounded Channel
+
Backpressure
+
Controlled consumers
Scenario: .Result in ASP.NET Core
Remove blocking
+
Async all the way
Scenario: Two locks
Consistent lock ordering
Scenario: Multiple servers
Local lock is insufficient
+
Distributed coordination / DB concurrency
62. Production Debugging Checklist

When a concurrent application is slow or incorrect, check:

Correctness
Race conditions
Deadlocks
Starvation
Duplicate processing
Lost updates
Invalid state transitions
Performance
CPU
Memory
GC
ThreadPool
Lock contention
Context switching
Connection pools
External Dependencies
Database latency
HTTP latency
API throttling
Rate limits
Retry storms
Architecture
Unbounded concurrency
Missing backpressure
Missing idempotency
Incorrect background processing
Distributed coordination
63. Senior-Level Decision Tree
Start
  ↓
Is work CPU-bound?
  ├── Yes → Consider controlled parallelism
  └── No
       ↓
     Is work I/O-bound?
       ├── Yes → async/await
       └── No → Analyze workload
       
       ↓
Is work independent?
  ├── Yes → Concurrent execution
  └── No → Respect dependencies

       ↓
Is state shared?
  ├── No → Less synchronization
  └── Yes
       ↓
Is state mutable?
  ├── No → Safe sharing is easier
  └── Yes
       ↓
Can mutation be avoided?
  ├── Yes → Prefer immutability
  └── No → Synchronize appropriately

       ↓
Is application distributed?
  ├── No → Local synchronization may work
  └── Yes → Need distributed/DB coordination

       ↓
Can work be retried?
  ├── Yes → Make operation idempotent
  └── No → Design carefully

       ↓
Can producer overwhelm consumer?
  ├── Yes → Backpressure / bounded concurrency
  └── No → Continue

       ↓
Measure
  ↓
Optimize
64. Most Important Interview Traps
Trap 1

Async creates a new thread.

False.

Trap 2

Task is a thread.

False.

Trap 3

More threads always improve performance.

False.

Trap 4

volatile makes counter++ safe.

False.

Trap 5

ConcurrentDictionary makes business logic atomic.

False.

Trap 6

lock works across servers.

False.

Trap 7

Task.WhenAll means CPU parallelism.

False.

Trap 8

Task.Run is required for async I/O.

False.

Trap 9

CancellationToken kills a thread.

False.

Trap 10

Singleton means thread-safe.

False.

Trap 11

Scoped means thread-safe.

False.

Trap 12

Timeout means the operation failed.

False.

Trap 13

Concurrent collections make workflows atomic.

False.

Trap 14

Distributed lock is always the best solution.

False.

65. One-Minute Revision
Thread
→ Execution resource

Task
→ Work abstraction

async/await
→ Non-blocking async programming

ThreadPool
→ Reusable worker threads

Concurrency
→ Overlapping progress

Parallelism
→ Simultaneous execution

CPU-bound
→ Parallelism

I/O-bound
→ Async

Race condition
→ Timing-dependent incorrect result

Thread safety
→ Correct concurrent behavior

lock
→ Synchronous mutual exclusion

SemaphoreSlim
→ Async synchronization / concurrency limit

Interlocked
→ Atomic operations

volatile
→ Visibility/order, not atomicity

Deadlock
→ Circular waiting

Starvation
→ Excessive waiting for resources

Channel
→ Async producer-consumer

Backpressure
→ Slow producer when consumer can't keep up

CancellationToken
→ Cooperative cancellation

Immutability
→ Avoid shared mutation

Optimistic concurrency
→ Detect conflict

Pessimistic concurrency
→ Prevent conflict with locks

Idempotency
→ Safe repeated logical operation

Distributed lock
→ Cross-instance coordination
66. Final Product-Company Mental Model

When an interviewer gives a multithreading problem, think:

1. What is the workload?
        ↓
2. CPU-bound or I/O-bound?
        ↓
3. Does concurrency help?
        ↓
4. Does parallelism help?
        ↓
5. What state is shared?
        ↓
6. Can shared mutable state be removed?
        ↓
7. What synchronization is actually required?
        ↓
8. What are the resource limits?
        ↓
9. Is concurrency bounded?
        ↓
10. Is backpressure required?
        ↓
11. What happens on cancellation?
        ↓
12. What happens on retry?
        ↓
13. Is the application distributed?
        ↓
14. How is consistency guaranteed?
        ↓
15. How will performance be measured?
Final Rules to Memorize

1. Async is not parallelism.

2. Task is not a thread.

3. CPU-bound → consider parallelism.

4. I/O-bound → prefer async/await.

5. Shared + mutable + concurrent = race-condition risk.

6. Use Interlocked for simple atomic operations.

7. Use lock for short synchronous critical sections.

8. Use SemaphoreSlim for async synchronization/concurrency limits.

9. Never assume ConcurrentDictionary makes an entire workflow atomic.

10. Never assume a local lock protects multiple servers.

11. Use bounded concurrency and backpressure.

12. Cancellation is cooperative.

13. Timeouts do not necessarily mean failure.

14. Retries require idempotency.

15. Database business invariants should be enforced at the authoritative data layer.

16. Measure before optimizing parallelism.

17. Prefer avoiding shared mutable state over adding more locks.

18. Production concurrency is about correctness + scalability + failure handling, not simply creating more threads.
# Parallel Programming

## 1. Definition

Parallel programming means dividing independent work so that multiple operations can execute **simultaneously**, usually on multiple CPU cores.

It is mainly useful for **CPU-bound workloads**.

---

## 2. Parallelism vs Concurrency

| Concept | Meaning |
|---|---|
| Concurrency | Multiple operations make progress during overlapping periods |
| Parallelism | Multiple operations execute at the same time |
| Typical goal | Handle multiple activities |
| Typical goal of parallelism | Improve CPU-bound processing performance |

**Important:**

> Concurrency does not necessarily mean parallel execution.

---

## 3. When Parallelism Is Useful

Good candidates:

- CPU-intensive calculations
- Image/video processing
- Large data transformations
- Mathematical calculations
- Independent data processing
- Batch processing

Example:

```text
Process 1 ──┐
Process 2 ──┤
Process 3 ──┼──> Multiple CPU cores
Process 4 ──┘
4. When Parallelism Is NOT Useful

Avoid unnecessary parallelism when:

Work is very small.
Operations depend on each other.
Ordering is important.
Shared state requires heavy locking.
The workload is primarily I/O-bound.
Parallelization overhead is greater than the benefit.
The machine has limited CPU capacity.
5. Parallel.For

Used when the same operation must be performed for many independent iterations.

Parallel.For(0, 100, i =>
{
    ProcessItem(i);
});

Iterations may execute concurrently and order is not guaranteed.

6. Parallel.ForEach

Used for parallel processing of a collection.

Parallel.ForEach(items, item =>
{
    ProcessItem(item);
});

Best suited for independent CPU-bound operations.

7. Parallel.ForEachAsync

Modern .NET also provides:

await Parallel.ForEachAsync(
    items,
    async (item, cancellationToken) =>
    {
        await ProcessAsync(
            item,
            cancellationToken);
    });

It is useful when each iteration performs asynchronous work and you want controlled concurrency.

8. Degree of Parallelism

Do not assume that maximum parallelism is always best.

Use ParallelOptions:

ParallelOptions options = new()
{
    MaxDegreeOfParallelism = 4
};

Parallel.ForEach(
    items,
    options,
    item =>
    {
        ProcessItem(item);
    });

MaxDegreeOfParallelism limits how many operations can execute concurrently.

9. Why Unlimited Parallelism Is Dangerous

Too much parallelism can cause:

CPU saturation
ThreadPool pressure
Context switching
Memory pressure
Lock contention
Lower throughput
Increased latency

More parallelism ≠ always better performance.

10. Shared Mutable State

Parallel operations accessing shared mutable data can cause race conditions.

Unsafe:

int counter = 0;

Parallel.For(0, 1000, _ =>
{
    counter++;
});

counter++ is not atomic.

Possible solutions:

Interlocked.Increment(ref counter);

or:

lock (lockObject)
{
    counter++;
}

Better design:

Avoid shared mutable state whenever possible.

11. Thread-Safe Collections

When parallel operations need shared collections, consider:

ConcurrentDictionary
ConcurrentQueue
ConcurrentBag
ConcurrentStack

Example:

ConcurrentBag<int> results = new();

Parallel.For(
    0,
    100,
    i =>
    {
        results.Add(i * i);
    });

However:

A thread-safe collection does not automatically make a multi-step business operation thread-safe.

12. Thread-Local State

Sometimes each parallel worker can maintain its own state and combine the results later.

This reduces contention.

Conceptually:

Worker 1 → Local Result
Worker 2 → Local Result
Worker 3 → Local Result
Worker 4 → Local Result

             ↓

        Combine Results

This is often better than updating one shared variable from every worker.

13. Parallel.For with Local State

For aggregation scenarios, Parallel.For supports:

localInit
body
localFinally

This allows each worker to maintain local state before combining results.

The principle is:

Local state first, shared aggregation later.

14. Cancellation

Parallel operations should support cancellation when the operation may be long-running.

CancellationTokenSource cts = new();

ParallelOptions options = new()
{
    CancellationToken = cts.Token
};

Parallel.For(
    0,
    100_000,
    options,
    i =>
    {
        ProcessItem(i);
    });

Cancellation is cooperative.

15. Exceptions

Exceptions from parallel operations must be handled correctly.

try
{
    Parallel.ForEach(
        items,
        item => ProcessItem(item));
}
catch (AggregateException ex)
{
    foreach (Exception error in ex.InnerExceptions)
    {
        Console.WriteLine(error.Message);
    }
}

Important:

Parallel work can produce multiple failures.

16. Ordering

Parallel execution does not guarantee processing order.

Parallel.For(
    0,
    10,
    i =>
    {
        Console.WriteLine(i);
    });

Output might be:

0
3
1
5
2
...

If ordering is required, parallel execution may require a different design.

17. Partitioning

Parallel processing divides work into partitions.

Example:

Input:
1 2 3 4 5 6 7 8

Partition 1 → 1 2
Partition 2 → 3 4
Partition 3 → 5 6
Partition 4 → 7 8

Each partition can be processed independently.

Good partitioning helps reduce overhead and improve CPU utilization.

18. Parallel Overhead

Parallelism itself has a cost:

Scheduling
Partitioning
Synchronization
Context switching
Coordination
Combining results

For very small operations:

Parallel overhead > Actual work

Sequential execution may therefore be faster.

19. CPU-Bound vs I/O-Bound
CPU-bound

Examples:

Image processing
Compression
Encryption
Large calculations
Data transformation

Parallelism can help.

I/O-bound

Examples:

HTTP API calls
Database calls
File operations

Usually prefer:

async/await
Task.WhenAll()

rather than CPU parallelism.

20. Task.WhenAll vs Parallel

| `Task.WhenAll`                            | `Parallel`                                |
| ----------------------------------------- | ----------------------------------------- |
| Async/concurrent operations               | Parallel processing                       |
| Excellent for I/O-bound work              | Excellent for CPU-bound work              |
| Works naturally with async APIs           | Designed around parallel iterations       |
| Does not imply simultaneous CPU execution | Designed to use available CPU parallelism |


Example I/O:

Task a = GetUserAsync();
Task b = GetOrderAsync();

await Task.WhenAll(a, b);

Example CPU:

Parallel.ForEach(
    items,
    item => Calculate(item));
21. Task.Run vs Parallel

Task.Run is commonly used to offload CPU-bound synchronous work.

int result = await Task.Run(
    () => Calculate());

Parallel.For/ForEach is designed specifically for parallel iteration.

Do not use Task.Run automatically for every operation.

22. Parallel.ForEachAsync vs Task.WhenAll

Use Task.WhenAll when you already have a known set of independent asynchronous operations.

await Task.WhenAll(
    GetUserAsync(),
    GetOrderAsync(),
    GetProductAsync());

Use Parallel.ForEachAsync when processing a collection with controlled asynchronous concurrency.

await Parallel.ForEachAsync(
    items,
    options,
    async (item, token) =>
    {
        await ProcessAsync(item, token);
    });
23. Backpressure and Concurrency Limits

In real systems, unlimited parallel operations can overload:

Database
External APIs
CPU
Memory
ThreadPool
Downstream services

Use controlled concurrency.

Common mechanisms:

MaxDegreeOfParallelism
SemaphoreSlim
Parallel.ForEachAsync
Queues
Channels
24. Parallelism in ASP.NET Core

Be careful when introducing parallel CPU work inside web requests.

Consider:

Number of incoming requests
Available CPU cores
ThreadPool usage
Request latency
Downstream capacity
Cancellation
Maximum concurrency

Example:

100 HTTP requests
        ↓
Each starts 20 parallel operations
        ↓
Potentially 2000 operations
        ↓
CPU / ThreadPool pressure

Parallelism must therefore be controlled.

25. Parallelism and ThreadPool

Parallel.For and related APIs use the .NET ThreadPool infrastructure.

If too much work is submitted:

ThreadPool resources become busy.
Other application work may experience delays.
Excessive blocking can contribute to ThreadPool starvation.

Avoid blocking inside parallel operations:

Parallel.ForEach(
    items,
    item =>
    {
        Thread.Sleep(1000); // Avoid unnecessary blocking
    });
26. Avoid Locks When Possible

This design:

Parallel.ForEach(
    items,
    item =>
    {
        lock (lockObject)
        {
            UpdateSharedState(item);
        }
    });

may serialize much of the work.

Prefer:

Independent processing
        ↓
Local results
        ↓
Final aggregation

when possible.

27. Parallelism and Immutability

Immutable data is easier to use safely in parallel operations because workers cannot modify the same object state.

Good parallel design often follows:

Read shared immutable data → perform independent work → produce independent results.

28. Parallelism and Database Operations

Do not blindly execute thousands of database operations in parallel.

Possible problems:

Connection pool exhaustion
Database overload
Lock contention
Increased latency
Transaction conflicts

Control concurrency and consider batching.

29. Parallelism and External APIs

The same principle applies to external APIs.

Bad:

10,000 items
   ↓
10,000 simultaneous API calls

Better:

10,000 items
   ↓
Controlled concurrency
   ↓
API

Respect:

Rate limits
Timeouts
Cancellation
Retries
Circuit breakers
30. Parallelism vs ThreadPool Starvation

Parallelism itself is not starvation.

Starvation can occur when parallel work is:

Excessive
Blocking
Long-running
Consuming too many ThreadPool workers

Therefore:

Controlled parallelism is important in production systems.

31. Performance Measurement

Never assume parallel code is faster.

Compare:

Sequential execution
        vs
Parallel execution

Measure:

Execution time
CPU utilization
Memory
Throughput
Latency
Contention

Use realistic workloads.

32. Common Mistakes
Mistake 1

Using parallelism for tiny workloads.

Mistake 2

Using parallelism for dependent operations.

Mistake 3

Updating shared mutable state without synchronization.

Mistake 4

Creating unlimited parallel operations.

Mistake 5

Blocking inside parallel work.

Mistake 6

Assuming execution order.

Mistake 7

Using parallelism for I/O when async concurrency is more appropriate.

Mistake 8

Assuming more CPU utilization automatically means better performance.

33. Product Company Scenario
Scenario

You need to process 10 million independent records.

A good approach:

10 million records
        ↓
Partition data
        ↓
Controlled parallel processing
        ↓
Local results
        ↓
Aggregate results

Consider:

CPU cores
Partition size
Memory
Cancellation
Error handling
Degree of parallelism
Ordering requirements
Downstream dependencies

34. Quick Comparison
| Requirement                   | Preferred Approach                  |
| ----------------------------- | ----------------------------------- |
| CPU-heavy loop                | `Parallel.For`                      |
| CPU-heavy collection          | `Parallel.ForEach`                  |
| Async collection processing   | `Parallel.ForEachAsync`             |
| Several independent API calls | `Task.WhenAll`                      |
| Simple CPU-bound offloading   | `Task.Run`                          |
| Limit concurrent operations   | `ParallelOptions` / `SemaphoreSlim` |
| Producer-consumer             | `Channel<T>`                        |
| Shared concurrent dictionary  | `ConcurrentDictionary`              |

35. Interview Questions
Q1. What is parallel programming?

Executing independent operations simultaneously, usually across multiple CPU cores.

Q2. When should you use parallelism?

Primarily for sufficiently large CPU-bound workloads that can be executed independently.

Q3. Does parallelism always improve performance?

No. Scheduling, synchronization, partitioning, and context-switching overhead can make it slower.

Q4. Parallel.ForEach vs Task.WhenAll?

Parallel.ForEach is primarily for parallel iteration, especially CPU-bound work. Task.WhenAll coordinates multiple asynchronous operations and is commonly used for I/O-bound work.

Q5. How do you control parallelism?

Use mechanisms such as MaxDegreeOfParallelism, SemaphoreSlim, or controlled batching.

Q6. Is Parallel.ForEach thread-safe?

The API coordinates parallel execution, but your code inside the loop must still safely handle shared state.

Q7. Does parallel execution preserve order?

No.

Q8. What is the biggest risk with shared state?

Race conditions and synchronization contention.

36. Key Points to Remember
Parallelism = simultaneous execution.
Mainly useful for CPU-bound work.
Parallel.For → parallel numeric iterations.
Parallel.ForEach → parallel collection processing.
Parallel.ForEachAsync → controlled async collection processing.
Use MaxDegreeOfParallelism to control concurrency.
Avoid unnecessary shared mutable state.
Use thread-safe collections when appropriate.
Parallel execution does not guarantee ordering.
Parallelism has overhead.
More parallelism does not always mean better performance.
Task.WhenAll is usually better for independent I/O operations.
Avoid blocking inside parallel operations.
Consider CPU, memory, ThreadPool, database, and downstream capacity.
Always benchmark before claiming a performance improvement.
Final Mental Model
Independent CPU Work
        ↓
Can it be parallelized?
        ↓
Yes
        ↓
Partition Work
        ↓
Control Parallelism
        ↓
Process in Parallel
        ↓
Avoid Shared Mutable State
        ↓
Aggregate Results
        ↓
Measure Performance

Product-company one-line summary:
Parallel programming improves CPU-bound workloads by executing independent work simultaneously, 
but production-quality parallelism requires controlled concurrency, safe shared-state handling, cancellation, proper error handling,
and measurement of the actual performance benefit.
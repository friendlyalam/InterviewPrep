# 02 - Concurrency vs Parallelism

## 1. Overview

Concurrency and parallelism are closely related but they are NOT the same thing.

Understanding the difference is very important for:

- Multithreading
- Task-based programming
- Async/await
- Parallel programming
- ASP.NET Core
- Distributed systems
- Product-company interviews


### Simple Mental Model

Concurrency = Multiple tasks are in progress during the same period.

Parallelism = Multiple tasks are executing at the same time.


Think:

Concurrency → "How do I handle multiple tasks?"

Parallelism → "How do I execute multiple tasks simultaneously?"


---

# 2. What is Concurrency?

Concurrency means that multiple tasks can make progress during overlapping periods.

The tasks do not necessarily execute at exactly the same time.


Example:

Task A:  ───────────────
Task B:       ───────────────
Task C:             ───────────────


Their execution periods overlap.


A system can achieve concurrency even with a single CPU core by switching between tasks.


Example:

CPU:

    Task A
       ↓
    Task B
       ↓
    Task A
       ↓
    Task C
       ↓
    Task B


The CPU switches between tasks.

This allows multiple tasks to make progress.


---

# 3. Real-World Example of Concurrency

Consider an e-commerce application.

Three independent operations are happening:

    Order Processing
    Payment Processing
    Notification Processing


They may all be in progress at the same time.

For example:

    Order Service
        ↓
    Save Order

    Payment Service
        ↓
    Process Payment

    Notification Service
        ↓
    Send Email


The system is dealing with multiple activities concurrently.


---

# 4. What is Parallelism?

Parallelism means executing multiple operations simultaneously.

This normally happens when multiple CPU cores execute different work at the same time.


Example:

    CPU Core 1 → Task A
    CPU Core 2 → Task B
    CPU Core 3 → Task C
    CPU Core 4 → Task D


Here multiple tasks are actually executing simultaneously.


---

# 5. Concurrency vs Parallelism

| Concurrency | Parallelism |
|---|---|
| Multiple tasks are in progress | Multiple tasks execute simultaneously |
| Can work on a single CPU core | Usually uses multiple CPU cores |
| Focuses on managing multiple tasks | Focuses on simultaneous execution |
| Useful for I/O-heavy applications | Useful for CPU-intensive workloads |
| Does not require simultaneous execution | Requires simultaneous execution |
| Often involves task switching | Often involves multiple cores |

### Interview Definition

Concurrency is the ability to deal with multiple tasks during overlapping periods.

Parallelism is the ability to execute multiple tasks simultaneously.


---

# 6. Concurrency on a Single CPU Core

Suppose the machine has only one CPU core.

There are three tasks:

    Task A
    Task B
    Task C


Only one task can execute at a particular instant.

The CPU can switch between them:

    Time →
    
    A A A | B B | A A | C C | B B | C C


This is concurrency.

The tasks are making progress during overlapping periods, but they are not executing simultaneously.


---

# 7. Parallelism on Multiple CPU Cores

Suppose there are four CPU cores.

    Core 1 → Task A
    Core 2 → Task B
    Core 3 → Task C
    Core 4 → Task D


At approximately the same time:

    Task A → executing
    Task B → executing
    Task C → executing
    Task D → executing


This is parallelism.


---

# 8. Important Relationship

Parallelism is one way of achieving concurrent execution.

But concurrency does not require parallelism.


Think:

    Concurrency
        │
        ├── Single-core concurrency
        │
        └── Multi-core concurrency
                  │
                  └── Parallel execution


Therefore:

    Concurrency ≠ Parallelism


---

# 9. CPU-Bound Work

CPU-bound work spends most of its time performing calculations.

Examples:

- Image processing
- Video encoding
- Compression
- Encryption
- Large mathematical calculations
- Data transformation
- Complex algorithms


Example:

    for (int i = 0; i < 1_000_000_000; i++)
    {
        // CPU-intensive calculation
    }


The CPU is the bottleneck.


Parallelism can improve performance if the work can safely be divided across CPU cores.


---

# 10. I/O-Bound Work

I/O-bound work spends significant time waiting for external resources.

Examples:

- HTTP API calls
- Database queries
- File operations
- Network operations
- Cloud storage
- Message brokers


Example:

    await httpClient.GetAsync(url);


The application is waiting for an external system to respond.


The CPU does not need to continuously execute instructions during the wait.


For I/O-bound workloads, asynchronous programming is usually more important than creating more threads.


---

# 11. CPU-Bound vs I/O-Bound

| CPU-Bound | I/O-Bound |
|---|---|
| CPU is the bottleneck | External resource/wait is the bottleneck |
| Heavy computation | Waiting for I/O |
| Image processing | HTTP request |
| Encryption | Database query |
| Compression | File I/O |
| Mathematical calculations | Network operation |
| Parallelism can help | Async/await can help |

### Important

Do not automatically use `Task.Run()` for every operation.

Choose the concurrency model based on the workload.


---

# 12. Concurrency Example

Suppose we have three independent operations:

    Download File A
    Download File B
    Download File C


Sequential approach:

    Download A
        ↓
    Download B
        ↓
    Download C


This takes roughly:

    A time + B time + C time


Concurrent approach:

    Start A
    Start B
    Start C

    A ─────────────
    B ───────────────
    C ──────────


The operations can overlap while waiting for I/O.


This can significantly reduce total waiting time.


---

# 13. Parallel Example

Suppose we have a large array and need to perform CPU-intensive calculations.

Sequential:

    Core
     │
     ├── Item 1
     ├── Item 2
     ├── Item 3
     └── Item 4


Parallel:

    Core 1 → Item 1
    Core 2 → Item 2
    Core 3 → Item 3
    Core 4 → Item 4


Independent CPU work can execute simultaneously.


---

# 14. Concurrency Does Not Mean "Many Threads"

Concurrency can be implemented using:

- Threads
- Tasks
- async/await
- ThreadPool
- Channels
- Message queues
- Actors
- Event-driven architecture


Modern .NET applications often use higher-level abstractions rather than manually creating threads.


---

# 15. Parallelism Does Not Mean "Create Many Threads"

Parallelism is about simultaneous execution.

Creating a large number of threads does not guarantee better parallelism.


For example:

    1,000 threads
          ↓
    8 CPU cores


The system cannot execute all 1,000 CPU-bound threads simultaneously on 8 cores.


The extra threads may create:

- Context switching
- Scheduling overhead
- Memory usage
- Contention


---

# 16. Thread vs Concurrency vs Parallelism

These are different concepts.


### Thread

A thread is an execution mechanism.


### Concurrency

Concurrency is a way of organizing/managing multiple tasks that can make progress during overlapping periods.


### Parallelism

Parallelism is simultaneous execution of multiple tasks, usually across multiple CPU cores.


Mental model:

    Thread
       ↓
    Execution mechanism

    Concurrency
       ↓
    Multiple activities in progress

    Parallelism
       ↓
    Multiple activities executing simultaneously


---

# 17. Task and Concurrency

Modern .NET uses `Task` extensively for concurrent operations.

Example:

    Task task1 = DoWorkAsync();
    Task task2 = DoWorkAsync();

    await Task.WhenAll(task1, task2);


Both operations can be in progress concurrently.


Important:

    Task != Thread


A Task does not necessarily represent a dedicated thread.


---

# 18. async/await and Concurrency

Consider:

    async Task<string> GetCustomerAsync()
    {
        return await httpClient.GetStringAsync(url);
    }


You can start multiple independent I/O operations:

    Task<string> customerTask = GetCustomerAsync();
    Task<string> orderTask = GetOrdersAsync();

    await Task.WhenAll(customerTask, orderTask);


The requests can be in progress concurrently.


This does not mean that two dedicated threads are sitting and waiting for the network responses.


---

# 19. Task.WhenAll

`Task.WhenAll()` is commonly used when multiple independent asynchronous operations should complete before continuing.


Example:

    Task task1 = Operation1Async();
    Task task2 = Operation2Async();
    Task task3 = Operation3Async();

    await Task.WhenAll(task1, task2, task3);


Conceptually:

    Operation 1 ─────────
    Operation 2 ─────────────
    Operation 3 ────────
                    ↓
              WhenAll completes


`WhenAll` completes when all supplied tasks complete.


---

# 20. Sequential vs Concurrent Async Operations

## Sequential

    var customer = await GetCustomerAsync();

    var orders = await GetOrdersAsync();

    var payments = await GetPaymentsAsync();


If these operations are independent, the total time can be approximately:

    Customer + Orders + Payments


## Concurrent

    var customerTask = GetCustomerAsync();
    var ordersTask = GetOrdersAsync();
    var paymentsTask = GetPaymentsAsync();

    await Task.WhenAll(
        customerTask,
        ordersTask,
        paymentsTask);


Now the operations can overlap.

Total time can approach roughly:

    Maximum(Customer, Orders, Payments)


This is useful when operations are independent and the external systems can handle the concurrent load.


---

# 21. Important Condition for Concurrency

Do not make operations concurrent just because you can.

Concurrency is appropriate when:

1. Operations are independent.
2. There is no required ordering.
3. The downstream systems can handle the load.
4. Shared state is properly handled.
5. The increased concurrency is beneficial.


Example:

    GetCustomer()
    GetOrders()
    GetRecommendations()


If they are independent, they may be good candidates for concurrent execution.


But:

    CreateOrder()
        ↓
    Payment()
        ↓
    ConfirmOrder()


If the next operation depends on the previous result, they may need to remain sequential.


---

# 22. Parallel.For

.NET provides APIs for CPU-bound parallel work.

Example:

    Parallel.For(0, 10, i =>
    {
        Console.WriteLine($"Processing {i}");
    });


Different iterations may execute concurrently/parallel depending on available resources and scheduler decisions.


`Parallel.For` is primarily intended for CPU-bound parallel work.


---

# 23. Parallel.ForEach

Example:

    Parallel.ForEach(items, item =>
    {
        ProcessItem(item);
    });


This can execute independent iterations in parallel.


Good candidate:

    Process 1,000 independent images


Potentially bad candidate:

    Update shared database state without proper synchronization


---

# 24. Task.Run and Parallelism

For CPU-bound synchronous work, `Task.Run()` can move work to the ThreadPool.

Example:

    Task task = Task.Run(() =>
    {
        PerformCpuHeavyCalculation();
    });


This does not magically make the algorithm parallel.

It schedules the work asynchronously, generally on a ThreadPool thread.


For actual parallel processing of many independent CPU operations, APIs such as:

    Parallel.For
    Parallel.ForEach


may be more appropriate.


---

# 25. Concurrency and Shared State

Concurrency becomes difficult when multiple operations modify shared mutable state.


Example:

    int counter = 0;


Multiple concurrent operations:

    Operation A → counter++
    Operation B → counter++
    Operation C → counter++


Without proper synchronization, updates can be lost.


This leads to:

    Race Condition


We will study this in detail in:

    07_Race_Condition_Thread_Safety


---

# 26. Concurrency and Thread Safety

Whenever multiple operations access shared mutable data, ask:

1. Is the data shared?
2. Can multiple operations access it concurrently?
3. Can they modify it?
4. Is the operation atomic?
5. Is synchronization required?


Possible solutions include:

- lock
- Interlocked
- ConcurrentDictionary
- Immutable objects
- Message passing
- Database constraints
- Optimistic concurrency


---

# 27. Concurrency and Ordering

Concurrent operations do not automatically execute in a predictable order.


Example:

    Task A
    Task B
    Task C


Possible execution:

    B → A → C


Another execution:

    C → B → A


Another:

    A → C → B


If ordering is required, the design must explicitly enforce it.


---

# 28. Concurrency and Dependencies

Consider:

    Step 1
       ↓
    Step 2
       ↓
    Step 3


If Step 2 depends on Step 1, they cannot simply be executed independently.


But:

    Step A ─────┐
                ├──→ Step C
    Step B ─────┘


A and B can potentially execute concurrently because C only requires both to complete.


This dependency analysis is an important part of designing concurrent systems.


---

# 29. Concurrency in ASP.NET Core

ASP.NET Core applications naturally handle multiple requests concurrently.

Example:

    Client A ──→ API
    Client B ──→ API
    Client C ──→ API
    Client D ──→ API


The server handles multiple requests without creating one dedicated manually managed Thread for every request.


Modern ASP.NET Core relies heavily on asynchronous programming and the ThreadPool.


Therefore, blocking request threads unnecessarily can reduce scalability.


---

# 30. Blocking vs Non-Blocking

### Blocking

A thread waits while doing nothing useful.

Example:

    Thread.Sleep(5000);


The thread is blocked.


### Non-blocking asynchronous waiting

Example:

    await Task.Delay(5000);


The operation waits asynchronously without blocking a thread for the entire delay.


For scalable server applications, unnecessary blocking should be avoided.


---

# 31. Concurrency Limits

Unlimited concurrency is usually a bad idea.


Suppose:

    100,000 requests
          ↓
    Start 100,000 operations


The application may overload:

- CPU
- Memory
- Database
- HTTP services
- APIs
- Network
- Message brokers


Instead, concurrency may need to be limited.


Example concept:

    100,000 operations
          ↓
    Maximum 100 concurrent operations
          ↓
    Remaining operations wait


Tools such as `SemaphoreSlim` can be used for concurrency limiting.

This will be studied in the synchronization section.


---

# 32. Backpressure

Backpressure means slowing down producers when consumers cannot keep up.


Example:

    Producer
       ↓
    Producer
       ↓
    Queue
       ↓
    Consumer


If producers generate work faster than consumers process it, the system needs a strategy.


Possible approaches:

- Bounded queues
- Channels
- SemaphoreSlim
- Rate limiting
- Message broker limits
- Load shedding


Backpressure is very important in production systems.


---

# 33. Concurrency in Distributed Systems

Concurrency is not limited to threads inside one process.


Consider:

    Server 1 ──┐
    Server 2 ──┼──→ Database
    Server 3 ──┘


Multiple servers can update the same data concurrently.


A local:

    lock


on Server 1 does NOT protect data from Server 2 or Server 3.


Distributed concurrency may require:

- Database transactions
- Optimistic concurrency
- Pessimistic concurrency
- Unique constraints
- Idempotency
- Distributed locks
- Version numbers
- Atomic database operations


These concepts become important in HLD and distributed systems.


---

# 34. Real-World Example: Product Search

Suppose an API needs:

    Product details
    Reviews
    Recommendations


If these are independent:

    Product API ─────────┐
    Reviews API ─────────┼──→ Response
    Recommendation API ─┘


They can potentially be called concurrently.


Sequential:

    Product → Reviews → Recommendations


Concurrent:

    Product ────────
    Reviews ───────────
    Recommendations ──


The concurrent design can reduce total latency.


However, you must consider:

- Downstream capacity
- Failure handling
- Timeouts
- Cancellation
- Partial failures
- Rate limits


---

# 35. Latency Example

Suppose:

    Product API = 100 ms
    Reviews API = 200 ms
    Recommendation API = 300 ms


Sequential:

    100 + 200 + 300
    = 600 ms approximately


Concurrent:

    max(100, 200, 300)
    = 300 ms approximately


This is an example of why concurrency can improve I/O-bound application latency.


Actual production latency will also include:

- Network overhead
- Serialization
- Queuing
- Scheduling
- Connection pooling
- Retries
- Server processing


---

# 36. Parallelism Example

Suppose processing 4 large images takes:

    Image 1 = 1 second
    Image 2 = 1 second
    Image 3 = 1 second
    Image 4 = 1 second


Sequential:

    1 + 1 + 1 + 1
    = approximately 4 seconds


With enough CPU cores and effective parallelization:

    Image 1 → Core 1
    Image 2 → Core 2
    Image 3 → Core 3
    Image 4 → Core 4


The total can approach approximately:

    1 second


Actual performance depends on:

- Number of CPU cores
- Algorithm
- Memory bandwidth
- Synchronization
- Scheduling overhead
- Work distribution


---

# 37. When to Prefer Concurrency

Concurrency is useful when:

- Multiple operations are independent.
- Operations spend time waiting.
- You have multiple I/O operations.
- You need high server throughput.
- You want to keep applications responsive.
- Multiple requests need to be handled simultaneously.


Common examples:

- Multiple HTTP calls
- Multiple database queries
- File operations
- Message processing
- API aggregation


---

# 38. When to Prefer Parallelism

Parallelism is useful when:

- Work is CPU-bound.
- Work can be divided into independent pieces.
- Multiple CPU cores are available.
- Parallel execution provides measurable benefit.
- Synchronization overhead is manageable.


Examples:

- Image processing
- Large numerical calculations
- Data transformation
- CPU-intensive algorithms
- Batch computation


---

# 39. When NOT to Use Parallelism

Avoid unnecessary parallelism when:

- Work is tiny.
- Operations depend on each other.
- Synchronization dominates the work.
- The downstream system is the bottleneck.
- Database operations are being blindly parallelized.
- The application already has sufficient concurrency.
- Parallel overhead exceeds the performance benefit.


Parallelism has overhead.


---

# 40. Common Interview Traps

### Trap 1

"Concurrency means multiple threads."

Not necessarily.


### Trap 2

"Parallelism means multiple tasks."

Not enough.

Parallelism means simultaneous execution.


### Trap 3

"async/await means parallel execution."

Not necessarily.


### Trap 4

"Task.Run creates parallelism."

Not automatically.

It schedules work, usually on the ThreadPool.


### Trap 5

"More threads improve performance."

Not necessarily.


### Trap 6

"All concurrent operations should use locks."

Not necessarily.

Better designs may use:

- Immutability
- Interlocked
- Concurrent collections
- Message passing
- Atomic operations
- Database concurrency mechanisms


---

# 41. Product-Company Design Thinking

When you see a concurrency problem, ask these questions:

### Step 1

What work needs to happen?


### Step 2

Which operations are independent?


### Step 3

Which operations have dependencies?


### Step 4

Is the workload CPU-bound or I/O-bound?


### Step 5

Can the work execute concurrently?


### Step 6

Can it execute in parallel?


### Step 7

Is shared mutable state involved?


### Step 8

Do we need synchronization?


### Step 9

What is the maximum safe concurrency?


### Step 10

What happens if an operation fails?


### Step 11

What happens if an external service is slow?


### Step 12

Can the downstream system handle the concurrency?


### Step 13

How will cancellation work?


### Step 14

How will we measure performance?


This thinking is more important than simply knowing threading APIs.


---

# 42. Modern .NET Perspective

For modern .NET applications, think in terms of:

    Task
    async/await
    ThreadPool
    Parallel
    CancellationToken
    Channels
    Concurrent Collections
    Synchronization primitives


rather than:

    Create Thread
    Create Thread
    Create Thread
    Create Thread


Manual `Thread` usage is a lower-level tool.

Higher-level abstractions are usually preferred when they match the problem.


---

# 43. Key Points to Remember

1. Concurrency and parallelism are different.
2. Concurrency means multiple tasks can make progress during overlapping periods.
3. Parallelism means multiple tasks execute simultaneously.
4. Concurrency does not require multiple CPU cores.
5. Parallelism usually benefits from multiple CPU cores.
6. CPU-bound workloads can benefit from parallelism.
7. I/O-bound workloads often benefit from asynchronous concurrency.
8. `Task` does not mean a dedicated thread.
9. `async` does not automatically create a new thread.
10. `Task.WhenAll()` is useful for independent asynchronous operations.
11. `Parallel.For` and `Parallel.ForEach` are useful for suitable CPU-bound workloads.
12. More threads do not automatically mean more performance.
13. More parallelism does not automatically mean better performance.
14. Shared mutable state creates race-condition risks.
15. Concurrent execution does not guarantee execution order.
16. Independent operations are good candidates for concurrency.
17. Dependent operations may need sequential execution.
18. Unlimited concurrency can overload downstream systems.
19. Concurrency limits and backpressure are important in production systems.
20. A local `lock` does not solve distributed concurrency across multiple servers.
21. Always consider failure, cancellation, timeout, and resource limits.
22. Choose concurrency based on workload and requirements, not simply because an API exists.


---

# 44. Interview Quick Answers

## Q1. What is concurrency?

Concurrency is the ability to manage multiple tasks that can make progress during overlapping periods.


## Q2. What is parallelism?

Parallelism is the simultaneous execution of multiple tasks, typically across multiple CPU cores.


## Q3. Can concurrency exist on a single CPU core?

Yes.

The CPU can switch between tasks, allowing them to make progress during overlapping periods.


## Q4. Can parallelism exist on multiple CPU cores?

Yes.

Different cores can execute different tasks simultaneously.


## Q5. Is async/await parallelism?

No.

Async/await is primarily an asynchronous programming model. It does not inherently mean parallel execution.


## Q6. Is Task the same as Thread?

No.

Task is a higher-level abstraction representing work or an asynchronous operation.


## Q7. CPU-bound or I/O-bound: which benefits from parallelism?

CPU-bound work is generally the stronger candidate for parallelism.


## Q8. Which approach is commonly useful for I/O-bound operations?

Asynchronous programming using async/await.


## Q9. What does Task.WhenAll do?

It creates a Task that completes when all supplied tasks have completed.


## Q10. Does Task.WhenAll create threads?

No.

It coordinates tasks; it does not itself mean that a new thread is created for every task.


## Q11. Why shouldn't we create thousands of threads?

Because threads consume resources and excessive threads cause scheduling and context-switching overhead.


## Q12. Can concurrency cause race conditions?

Yes, when concurrent operations access shared mutable state without appropriate synchronization.


## Q13. Does lock work across multiple servers?

No.

A normal in-process lock only coordinates threads within the same process.


## Q14. What is backpressure?

Backpressure is a mechanism for preventing producers from overwhelming consumers when work arrives faster than it can be processed.


---

# 45. Final Mental Model

Remember:

    CONCURRENCY
        ↓
    Multiple tasks in progress
        ↓
    May or may not execute simultaneously


    PARALLELISM
        ↓
    Multiple tasks executing simultaneously
        ↓
    Usually multiple CPU cores


    CPU-BOUND
        ↓
    Consider parallelism


    I/O-BOUND
        ↓
    Consider async concurrency


    SHARED MUTABLE STATE
        ↓
    Think thread safety


    TOO MUCH CONCURRENCY
        ↓
    Think resource limits + backpressure


    MULTIPLE SERVERS
        ↓
    Think distributed concurrency


The goal is not:

    "Use as many threads as possible."


The goal is:

    "Choose the right concurrency model for the workload."
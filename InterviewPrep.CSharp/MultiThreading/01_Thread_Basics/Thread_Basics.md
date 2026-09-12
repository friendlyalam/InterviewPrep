# 01 - Thread Basics

## 1. What is a Thread?

A thread is the smallest unit of execution within a process.

A process represents a running application, while a thread represents a path of execution inside that process.

Example:

Application / Process
│
├── Main Thread
├── Worker Thread
└── Worker Thread


### Simple Mental Model

Process = Container

Thread = Worker executing inside the container


Multiple threads can exist inside the same process.

Threads belonging to the same process generally share the process's memory and resources.


---

# 2. Process vs Thread

| Process | Thread |
|---|---|
| Running application/program | Execution path inside a process |
| Has its own address space | Shares process address space |
| More expensive to create | Less expensive than a process |
| Strong isolation from other processes | Threads share resources |
| Inter-process communication is relatively expensive | Communication through shared memory is easier |
| Process failure is generally isolated | A serious thread failure can affect the process |

### Mental Model

Process = Application

Thread = Worker inside the application


Example:

Order Management Application
│
├── Thread 1 → Process requests
├── Thread 2 → Background work
└── Thread 3 → Other work


---

# 3. Why Do We Need Multiple Threads?

A single thread can execute only one instruction path at a time.

Suppose an application has:

Task A
↓
Task B
↓
Task C
↓
Task D

Everything happens sequentially.

Multiple threads allow independent work to make progress concurrently.

Example:

Thread 1 → Task A ─────────→ Task C

Thread 2 → Task B ─────────→ Task D


Potential benefits:

- Better responsiveness
- Better throughput
- Better CPU utilization
- Ability to execute independent work concurrently
- Useful for CPU-intensive parallel workloads


However:

More threads != automatically better performance.


Too many threads can cause:

- Context switching overhead
- CPU overhead
- Memory consumption
- Lock contention
- ThreadPool starvation
- Synchronization problems


---

# 4. Creating a Thread

C# provides the `Thread` class in the `System.Threading` namespace.

Basic syntax:

    Thread thread = new Thread(SomeMethod);
    thread.Start();


Example:

    static void DoWork()
    {
        Console.WriteLine("Worker thread is running");
    }

    Thread worker = new Thread(DoWork);

    worker.Start();


Important:

`new Thread(...)` creates the Thread object.

`Start()` starts the thread.

Creating the object does not mean the thread has started executing.


---

# 5. Thread.Start()

`Start()` starts the execution of a thread.

Example:

    Thread worker = new Thread(() =>
    {
        Console.WriteLine("Worker executing");
    });

    worker.Start();


Conceptually:

    new Thread(...)
          ↓
    Thread object created
          ↓
    Start()
          ↓
    Thread becomes runnable
          ↓
    Operating system schedules it
          ↓
    Thread executes


Important:

The exact time at which the thread executes is controlled by the operating system/runtime scheduler.

Therefore, you should not assume exact execution ordering between independent threads.


---

# 6. Main Thread

Every normal application begins execution on an initial thread.

For example:

    static void Main()
    {
        Console.WriteLine("Application started");
    }


The code inside `Main()` initially executes on the main thread.

You can identify the current thread:

    Thread.CurrentThread.ManagedThreadId


Example:

    Console.WriteLine(
        $"Thread ID: {Thread.CurrentThread.ManagedThreadId}");


The actual thread ID is runtime-dependent.

Do not assume that a worker thread will always have a particular ID.


---

# 7. Thread.ManagedThreadId

Each managed thread has an identifier.

Syntax:

    Thread.CurrentThread.ManagedThreadId


Example:

    Console.WriteLine(
        $"Current Thread: {Thread.CurrentThread.ManagedThreadId}");


This is useful for:

- Debugging
- Logging
- Understanding which thread is executing code
- Troubleshooting concurrency problems


Do not use thread IDs as business identifiers.

They are primarily useful for diagnostics.


---

# 8. Thread.Join()

`Join()` causes the calling thread to wait until another thread finishes.

Example:

    Thread worker = new Thread(() =>
    {
        Thread.Sleep(1000);
        Console.WriteLine("Worker completed");
    });

    worker.Start();

    worker.Join();

    Console.WriteLine("Main completed");


Execution concept:

    Main Thread
         │
         ├── Start Worker
         │
         ├── Join()
         │      │
         │      └── Wait
         │
         │
         └── Worker completes
                ↓
           Main continues


Without `Join()`:

The main thread may continue before the worker thread finishes.


### Important

`Join()` blocks the calling thread.

It should therefore be used carefully in modern asynchronous applications.


---

# 9. Thread.Sleep()

`Thread.Sleep()` blocks the current thread for a specified amount of time.

Example:

    Thread.Sleep(1000);


This approximately means:

Pause the current thread for 1 second.


Example:

    Console.WriteLine("Started");

    Thread.Sleep(2000);

    Console.WriteLine("Finished");


The current thread cannot perform other work during the sleep.


### Important Product-Company Point

Do not confuse:

    Thread.Sleep()

with:

    await Task.Delay()


`Thread.Sleep()` blocks a thread.

`Task.Delay()` can asynchronously wait without blocking a thread while waiting.

This distinction becomes important when learning async/await.


---

# 10. ThreadState

A thread can move through different execution states.

Conceptually:

    Unstarted
        ↓
    Runnable
        ↓
    Running
        ↓
    Waiting / Blocked
        ↓
    Running
        ↓
    Stopped


You can inspect:

    thread.ThreadState


Example:

    Console.WriteLine(thread.ThreadState);


### Important

Do not build application logic around exact thread-state timing.

Thread scheduling is dynamic.

Thread state information is mainly useful for understanding and debugging.


---

# 11. Foreground Thread

A foreground thread can keep the process alive while it is running.

Conceptually:

    Application
        │
        └── Foreground Thread
                 │
                 └── Still running
                         ↓
                   Process remains alive


If foreground threads are still running, the process generally does not terminate simply because the main thread has completed.


---

# 12. Background Thread

A background thread does not keep the process alive.

You can configure a thread:

    thread.IsBackground = true;


Example:

    Thread worker = new Thread(() =>
    {
        Thread.Sleep(5000);
        Console.WriteLine("Worker completed");
    });

    worker.IsBackground = true;

    worker.Start();


If the application exits before the background thread finishes, the background work may not complete.


### Product-Company Point

Do not use manually created background threads as the normal solution for modern application background processing.

Depending on the requirement, modern .NET applications commonly use:

- Task
- ThreadPool
- BackgroundService
- Channels
- Queues
- Message brokers


---

# 13. Thread vs Task

This is one of the most important interview concepts.

## Thread

A `Thread` represents an actual execution thread.

Example:

    Thread thread = new Thread(DoWork);
    thread.Start();


## Task

A `Task` represents an asynchronous operation or unit of work.

Example:

    Task task = Task.Run(DoWork);


### Mental Model

Thread = Execution resource

Task = Abstraction representing work


A Task does NOT automatically mean:

"Create a new thread."


This is extremely important.

---

# 14. Task != Thread

Consider:

    await httpClient.GetAsync(url);


This does not mean:

"Create a new thread and wait for the HTTP request."


For asynchronous I/O:

    Application
        ↓
    Start I/O operation
        ↓
    Thread is not required to sit blocked waiting
        ↓
    I/O completes
        ↓
    Continuation resumes


This is one reason asynchronous programming is important for scalable server applications.


---

# 15. Thread vs ThreadPool

A manually created Thread:

    new Thread(...)


creates a dedicated OS thread for that work.

The ThreadPool maintains a pool of reusable worker threads.

Instead of repeatedly creating dedicated threads, applications can use ThreadPool-based abstractions.

Examples:

- Task
- Task.Run
- ThreadPool.QueueUserWorkItem
- Parallel APIs
- Many framework APIs internally using ThreadPool threads


### Why ThreadPool?

Creating and destroying threads has overhead.

ThreadPool allows threads to be reused.


Mental model:

Without ThreadPool:

    Work → Create Thread → Work → Destroy Thread
    Work → Create Thread → Work → Destroy Thread


With ThreadPool:

    ThreadPool
    ├── Worker 1
    ├── Worker 2
    ├── Worker 3
    └── Worker 4

    Work → Reuse available worker


ThreadPool behavior and starvation will be studied separately.


---

# 16. Concurrency

Concurrency means multiple tasks can make progress during overlapping periods.

Example:

    Task A ─────────────
          Task B ─────────────


The tasks overlap in time.


Concurrency is about:

"Dealing with multiple tasks."


It does not necessarily mean they execute at exactly the same instant.


---

# 17. Parallelism

Parallelism means multiple operations execute simultaneously, typically using multiple CPU cores.

Example:

    CPU Core 1 → Task A
    CPU Core 2 → Task B
    CPU Core 3 → Task C
    CPU Core 4 → Task D


Parallelism is about:

"Executing multiple tasks at the same time."


---

# 18. Concurrency vs Parallelism

| Concurrency | Parallelism |
|---|---|
| Multiple tasks make progress during overlapping periods | Multiple tasks execute simultaneously |
| Does not require multiple CPU cores | Usually benefits from multiple CPU cores |
| Mainly about managing multiple activities | Mainly about simultaneous execution |
| Useful for I/O workloads | Very useful for CPU-intensive workloads |

### Interview Answer

Concurrency is about handling multiple tasks at the same time period.

Parallelism is about actually executing multiple tasks simultaneously.


---

# 19. Context Switching

A CPU can switch execution between threads.

Example:

    Thread A
       ↓
    Thread B
       ↓
    Thread C
       ↓
    Thread A
       ↓
    Thread B


The CPU/runtime/OS must save and restore execution state when switching between threads.

This is called context switching.


Context switching has overhead.


Therefore:

Too many threads can reduce performance.


---

# 20. More Threads != More Performance

A common beginner assumption is:

    More Threads
         ↓
    More Performance


This is not always true.


Too many threads can cause:

    More threads
        ↓
    More context switching
        ↓
    More CPU overhead
        ↓
    More contention
        ↓
    Potentially worse performance


The correct number of concurrent operations depends on:

- CPU cores
- Workload
- CPU-bound vs I/O-bound work
- Blocking
- Synchronization
- Memory
- External system capacity


---

# 21. Shared Memory

Threads within the same process generally share process memory.

Example:

    int counter = 0;


Multiple threads can access the same variable:

    Thread A ──┐
               │
               ├── counter
               │
    Thread B ──┘


Shared memory makes communication between threads relatively easy.

But shared mutable state creates concurrency problems.


---

# 22. Race Condition

A race condition occurs when the result of a program depends on the timing/interleaving of concurrent operations.

Example:

    int counter = 0;

    counter++;


`counter++` looks like one operation.

Conceptually it involves:

    Read counter
        ↓
    Add 1
        ↓
    Write counter


Suppose two threads execute it:

    Thread A → Read 0
    Thread B → Read 0
    Thread A → Write 1
    Thread B → Write 1


Expected:

    2


Actual:

    1


This is a race condition.


We will study race conditions and thread safety in detail in:

    07_Race_Condition_Thread_Safety


---

# 23. Thread Safety

Code is thread-safe when it behaves correctly when accessed concurrently by multiple threads.

Example of potentially unsafe shared state:

    class Counter
    {
        private int _count;

        public void Increment()
        {
            _count++;
        }
    }


Multiple threads calling `Increment()` can cause lost updates.


Thread safety can be achieved using appropriate techniques such as:

- lock
- Monitor
- Interlocked
- Concurrent collections
- Immutability
- Message passing
- Proper synchronization design


These will be covered in later folders.


---

# 24. Thread Scheduling

Thread scheduling determines which runnable thread gets CPU time.

Example:

    Thread A
    Thread B
    Thread C


You should not assume:

    A always finishes before B.


Even if:

    A.Start();
    B.Start();


the actual execution order can vary.


Possible execution:

    A → B → A → C → B


Another run might produce:

    B → A → C → A → B


Therefore concurrent code should not depend on accidental execution order.


---

# 25. CPU-Bound vs I/O-Bound Work

This distinction is critical for modern .NET.


## CPU-Bound Work

The CPU is doing significant computation.

Examples:

- Image processing
- Compression
- Encryption
- Complex calculations
- Large data transformations


Parallelism can be useful for CPU-bound work.


## I/O-Bound Work

The application spends time waiting for an external operation.

Examples:

- Database calls
- HTTP requests
- File I/O
- Network operations
- Cloud service calls


Async/await is usually more appropriate for scalable I/O-bound workloads than creating dedicated threads.


### Mental Model

CPU-bound:

    Application → CPU → CPU → CPU


I/O-bound:

    Application → Request → WAIT → Response


During asynchronous I/O waiting, we generally do not need to block a thread.


---

# 26. Thread.Sleep vs Async Waiting

## Thread.Sleep

    Thread.Sleep(5000);


The current thread is blocked for approximately 5 seconds.


## Async waiting

    await Task.Delay(5000);


The operation can asynchronously wait without occupying a thread for the entire delay.


### Interview Point

`Thread.Sleep()` blocks.

`Task.Delay()` is asynchronous and does not block the current thread while waiting.


---

# 27. Thread Abort

Older .NET Framework code may contain concepts such as:

    Thread.Abort()


Modern .NET does not support `Thread.Abort()` as the normal thread-cancellation mechanism.

Modern applications should use cooperative cancellation with:

    CancellationToken


Example:

    CancellationToken cancellationToken


The thread/task periodically observes the cancellation request and stops cooperatively.


Cancellation will be studied in:

    11_Cancellation


---

# 28. Thread Priority

C# exposes:

    Thread.Priority


with values such as:

- Lowest
- BelowNormal
- Normal
- AboveNormal
- Highest


However, application code generally should not rely on thread priority for business correctness or performance design.

The operating system scheduler controls actual scheduling behavior.


For modern product applications, focus more on:

- Correct concurrency model
- Async I/O
- ThreadPool usage
- CPU utilization
- Synchronization
- Backpressure
- Cancellation
- Scalability


---

# 29. Thread Local State

Sometimes each thread needs its own independent value.

.NET provides mechanisms such as:

    ThreadLocal<T>


Example concept:

    ThreadLocal<int> value;


Each thread gets its own value instead of sharing the same value.


Thread-local state can be useful in specific performance/concurrency scenarios.

However, it should not be used simply to avoid designing proper shared-state synchronization.


---

# 30. Important Thread Concepts

You should understand these terms:

- Process
- Thread
- Main thread
- Worker thread
- Thread ID
- Thread.Start()
- Thread.Join()
- Thread.Sleep()
- ThreadState
- Foreground thread
- Background thread
- Thread scheduling
- Context switching
- Shared memory
- Race condition
- Thread safety
- Concurrency
- Parallelism
- CPU-bound work
- I/O-bound work
- ThreadPool
- Task
- Async/await
- Cancellation


---

# 31. Real-World Example

Consider an e-commerce application.

A request arrives:

    Customer places order


The application may need to:

    Validate order
         ↓
    Save order
         ↓
    Reserve inventory
         ↓
    Process payment
         ↓
    Send notification


Not every operation should necessarily execute on a separate manually created Thread.

Modern architecture may use:

    HTTP Request
         ↓
    ASP.NET Core
         ↓
    Async database operation
         ↓
    Async payment API
         ↓
    Message/Queue
         ↓
    Background processing


The correct concurrency model depends on the workload.


---

# 32. When Should You Use Thread?

Direct `Thread` usage is relatively uncommon in modern application development.

It can be appropriate when you genuinely need:

- Dedicated thread semantics
- Long-running dedicated execution
- Specialized thread configuration
- Low-level threading control
- Specific legacy/infrastructure scenarios


For ordinary application work, prefer higher-level abstractions when appropriate:

- Task
- async/await
- ThreadPool
- Parallel
- Channels
- BackgroundService
- Message queues


---

# 33. When Should You NOT Create a New Thread?

Avoid creating a new thread just because:

"I want this operation to run asynchronously."


For example, this is usually unnecessary for an async HTTP call:

    new Thread(async () =>
    {
        await httpClient.GetAsync(url);
    });


Prefer proper async programming:

    await httpClient.GetAsync(url);


Similarly, do not create hundreds or thousands of dedicated threads for ordinary server requests.


Use appropriate framework abstractions.


---

# 34. Common Mistakes

## Mistake 1

Thinking:

    Task = Thread


Wrong.

Task is an abstraction for work/operation.

A Task may execute using a ThreadPool thread, an existing thread, or represent asynchronous I/O without occupying a thread while waiting.


---

## Mistake 2

Thinking:

    async = new thread


Wrong.

`async` does not automatically create a new thread.


---

## Mistake 3

Thinking:

    More threads = faster


Wrong.

Too many threads can create significant overhead.


---

## Mistake 4

Thinking:

    counter++ is atomic


Wrong.

A normal increment operation is not automatically thread-safe.


---

## Mistake 5

Using:

    Thread.Sleep()


inside scalable server code to wait for I/O.


Prefer asynchronous APIs and:

    await


when the operation is naturally asynchronous.


---

## Mistake 6

Assuming thread execution order.

Example:

    thread1.Start();
    thread2.Start();


does NOT guarantee:

    thread1 finishes first.


---

# 35. Product-Company Interview Focus

For interviews at companies such as Microsoft, Amazon, and Google, understand the concepts rather than memorizing APIs.

You should be able to explain:

### Fundamentals

- What is a thread?
- What is a process?
- Process vs thread
- Why use multiple threads?
- How threads share memory
- Thread lifecycle
- Thread scheduling


### Execution

- Thread.Start()
- Thread.Join()
- Thread.Sleep()
- Foreground vs background threads
- ThreadPool
- Context switching


### Concurrency

- Concurrency
- Parallelism
- Race condition
- Thread safety
- Shared mutable state


### Modern .NET

- Thread vs Task
- Task vs ThreadPool
- async/await
- CPU-bound vs I/O-bound
- Why async doesn't necessarily create a thread
- Why Task.Run should not simply wrap every async operation


### Design

- When to create a Thread
- When to use Task
- When to use async/await
- When to use Parallel
- How to avoid excessive threads
- How to design thread-safe components


---

# 36. Key Interview Questions and Short Answers

## Q1. What is a thread?

A thread is an execution path within a process.


## Q2. What is a process?

A process is a running program with its own address space and resources.


## Q3. Process vs thread?

A process provides isolation and owns resources; threads execute within a process and share its process-level resources.


## Q4. How do you create a thread in C#?

    Thread thread = new Thread(DoWork);
    thread.Start();


## Q5. Does creating a Thread start it?

No.

`new Thread(...)` creates the thread object.

`Start()` begins execution.


## Q6. What does Join() do?

`Join()` blocks the calling thread until the target thread completes.


## Q7. What does Thread.Sleep() do?

It blocks the current thread for approximately the specified duration.


## Q8. What is a race condition?

A race condition occurs when concurrent operations access shared state and the result depends on their timing/interleaving.


## Q9. What is thread safety?

Thread safety means code behaves correctly when accessed concurrently.


## Q10. Thread vs Task?

Thread is an execution thread.

Task is a higher-level abstraction representing asynchronous work/operation.


## Q11. Does every Task create a new thread?

No.


## Q12. Does async create a new thread?

No. Async programming does not inherently require creating a new thread.


## Q13. Concurrency vs parallelism?

Concurrency means multiple tasks make progress during overlapping periods.

Parallelism means multiple tasks execute simultaneously.


## Q14. Why can too many threads reduce performance?

Because of context switching, scheduling overhead, memory usage, and synchronization contention.


## Q15. Is `counter++` thread-safe?

No, not for shared mutable state.


## Q16. Should we create a Thread for every HTTP request?

No.

Use asynchronous I/O and framework-managed concurrency.


## Q17. What is ThreadPool?

ThreadPool is a managed pool of reusable worker threads used by .NET for many types of asynchronous/background work.


## Q18. CPU-bound vs I/O-bound?

CPU-bound work spends significant time using CPU.

I/O-bound work spends significant time waiting for external resources.


---

# 37. Mental Model

Remember this hierarchy:

    Process
       ↓
    Threads
       ↓
    Execution


For modern .NET:

    Application
       ↓
    Task / async operation
       ↓
    ThreadPool / I/O completion mechanisms
       ↓
    Actual execution


Do not assume:

    Task = Thread

Do not assume:

    async = Thread


---

# 38. Most Important Points to Remember

1. A process is a running application.
2. A thread is an execution path inside a process.
3. Multiple threads in the same process generally share process memory/resources.
4. `Thread.Start()` starts a thread.
5. `Thread.Join()` waits for a thread.
6. `Thread.Sleep()` blocks the current thread.
7. Thread scheduling is not deterministic.
8. Thread IDs are mainly useful for diagnostics.
9. Foreground/background behavior affects process lifetime.
10. Context switching has overhead.
11. More threads do not automatically mean better performance.
12. Shared mutable state can cause race conditions.
13. `counter++` is not automatically atomic.
14. Thread safety must be designed explicitly.
15. Concurrency and parallelism are different concepts.
16. CPU-bound and I/O-bound workloads need different approaches.
17. Task is not the same as Thread.
18. Async does not automatically mean a new thread.
19. ThreadPool provides reusable worker threads.
20. Modern .NET generally prefers higher-level concurrency abstractions over manually creating threads for ordinary work.
21. Use asynchronous APIs for asynchronous I/O.
22. Avoid blocking threads unnecessarily.
23. Do not depend on accidental thread execution order.
24. Do not use Thread.Sleep() as a normal way to wait for I/O.
25. Cancellation should normally be cooperative using CancellationToken.


---

# 39. What Comes Next?

This folder establishes the foundation.

Next topics build on it:

    02_Concurrency_Parallelism
           ↓
    03_ThreadPool
           ↓
    04_Task
           ↓
    05_Async_Await
           ↓
    06_Task_Coordination
           ↓
    07_Race_Condition_Thread_Safety
           ↓
    08_Synchronization
           ↓
    ...


The most important foundation from this folder is:

    Thread
       ↓
    Execution

    Task
       ↓
    Work / Operation

    async/await
       ↓
    Asynchronous programming

    ThreadPool
       ↓
    Reusable worker threads

    Concurrency
       ↓
    Managing multiple activities

    Parallelism
       ↓
    Simultaneous execution

    Shared mutable state
       ↓
    Race conditions
       ↓
    Synchronization / thread safety
## 1. What is a Task?

`Task` represents an asynchronous operation or unit of work.

It is a higher-level abstraction than `Thread`.

A Task can represent:

- CPU-bound work
- I/O-bound asynchronous work
- Work that completes in the future
- An operation that returns a result

Example:

Task task = Task.Run(() =>
{
    Console.WriteLine("Work running...");
});

| Thread                              | Task                                       |
| ----------------------------------- | ------------------------------------------ |
| Low-level execution mechanism       | Higher-level abstraction                   |
| More expensive to create/manage     | Easier to manage                           |
| Direct control over thread          | Runtime manages execution                  |
| Manual thread management            | Built-in task coordination                 |
| `Thread.Start()`                    | `Task.Run()` / async APIs                  |
| `Join()`                            | `await`                                    |
| Not ideal for most application work | Preferred for most asynchronous operations |


Important:

Task is NOT the same thing as Thread.

A Task represents work.

A Thread is an execution mechanism.

3. Creating a Task
Using Task.Run
Task task = Task.Run(() =>
{
    Console.WriteLine("Task is running...");
});

Wait for completion:

await task;
4. Task with a Return Value

Use Task<T> when the operation returns a result.

Task<int> task = Task.Run(() =>
{
    return 100;
});

int result = await task;

Console.WriteLine(result);

Here:

Task<int>
   ↓
Eventually produces
   ↓
int
5. Task.Run

Task.Run() is commonly used to run synchronous CPU-bound work on ThreadPool infrastructure.

Example:

Task task = Task.Run(() =>
{
    PerformCpuWork();
});

Good use:

CPU-bound synchronous work
        ↓
Task.Run()
        ↓
ThreadPool

Do not use Task.Run() unnecessarily around naturally asynchronous I/O.

Avoid:

Task.Run(async () =>
{
    await httpClient.GetAsync(url);
});

Prefer:

await httpClient.GetAsync(url);
6. Task and async/await

async and await make asynchronous code easier to write.

Example:

public async Task ProcessAsync()
{
    await SomeOperationAsync();
}

Important:

async does not automatically create a new thread.

For I/O operations, the thread can be released while waiting.

7. Task.Delay

Task.Delay() creates an asynchronous delay.

await Task.Delay(1000);

It does not block the current thread like:

Thread.Sleep(1000);

Prefer Task.Delay() in asynchronous code.

8. Task.WhenAll

Use Task.WhenAll() when multiple independent operations can run concurrently.

Task task1 = Operation1Async();
Task task2 = Operation2Async();
Task task3 = Operation3Async();

await Task.WhenAll(task1, task2, task3);

Conceptually:

Task 1 ────────────┐
Task 2 ────────────┼──→ WhenAll
Task 3 ────────────┘

Useful for:

Multiple API calls
Multiple independent database operations
Parallel asynchronous workflows
9. Task.WhenAny

Task.WhenAny() completes when any one of the supplied tasks completes.

Task completedTask =
    await Task.WhenAny(task1, task2, task3);

Useful for:

First response wins
Timeout patterns
Racing independent operations
10. Task Status

A Task has a status.

Common statuses include:

Created
WaitingForActivation
WaitingToRun
Running
RanToCompletion
Canceled
Faulted

Example:

Task task = Task.Run(() =>
{
    Thread.Sleep(1000);
});

Console.WriteLine(task.Status);

await task;

Console.WriteLine(task.Status);
11. Task Exceptions

Exceptions from a Task are observed when the Task is awaited.

try
{
    await Task.Run(() =>
    {
        throw new InvalidOperationException("Something failed.");
    });
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Important:

Always handle Task exceptions appropriately.

Do not silently ignore failed Tasks.

12. Task Cancellation

Tasks can support cooperative cancellation using CancellationToken.

CancellationTokenSource cts = new();

Task task = Task.Run(() =>
{
    while (!cts.Token.IsCancellationRequested)
    {
        // Work
    }
}, cts.Token);

Request cancellation:

cts.Cancel();

Cancellation is cooperative.

The running operation must observe the token and stop appropriately.

13. Task.Wait vs await
Blocking
task.Wait();

The current thread is blocked while waiting.

Asynchronous
await task;

The method can yield while waiting.

Prefer:

await

over:

.Wait()
.Result

in modern asynchronous application code.

14. Task.Result

Example:

Task<int> task = CalculateAsync();

int result = task.Result;

Although this may work, .Result blocks the current thread.

Prefer:

int result = await task;
15. Task.Factory.StartNew

Older/advanced API:

Task task = Task.Factory.StartNew(() =>
{
    Console.WriteLine("Work");
});

For most application code, prefer:

Task.Run(() =>
{
    Console.WriteLine("Work");
});

Task.Factory.StartNew() has more configuration options but can have surprising behavior, especially with async delegates.

16. Task Scheduling

Tasks are scheduled by a Task Scheduler.

For typical Task.Run() usage:

Task
 ↓
TaskScheduler
 ↓
ThreadPool
 ↓
Worker Thread
 ↓
Execution

But:

Do not assume every Task always runs on a ThreadPool thread.

Some Tasks represent asynchronous I/O and may not require a worker thread to remain blocked while waiting.

17. CPU-Bound vs I/O-Bound
CPU-bound

Examples:

Image processing
Large calculations
Encryption
Compression
Data processing

Possible approach:

await Task.Run(() =>
{
    PerformCpuWork();
});
I/O-bound

Examples:

HTTP API call
Database call
File I/O
Network operation

Prefer naturally asynchronous APIs:

await httpClient.GetAsync(url);

Do not wrap every I/O operation in Task.Run().

18. Task Does Not Automatically Mean Parallelism

This:

Task task1 = Operation1Async();
Task task2 = Operation2Async();

await Task.WhenAll(task1, task2);

can provide concurrency.

But concurrency does not always mean simultaneous CPU execution.

Remember:

Concurrency
    ≠
Parallelism
19. Multiple Tasks and Shared State

Multiple tasks accessing shared mutable data can cause race conditions.

Example:

int counter = 0;

Task task1 = Task.Run(() =>
{
    counter++;
});

Task task2 = Task.Run(() =>
{
    counter++;
});

The result is not something you should rely on without proper synchronization.

Possible solutions include:

lock
Interlocked
SemaphoreSlim
Concurrent collections
Immutable data
20. Task and ThreadPool

Typical relationship:

Task.Run()
   ↓
Task
   ↓
TaskScheduler
   ↓
ThreadPool
   ↓
Worker Thread

But asynchronous I/O is different:

Async API
   ↓
Start I/O
   ↓
Thread does not wait/block
   ↓
I/O completes
   ↓
Continuation resumes
21. Task in ASP.NET Core

ASP.NET Core applications heavily use Tasks.

Example:

public async Task<IActionResult> GetDataAsync()
{
    var data = await service.GetDataAsync();

    return Ok(data);
}

This allows the server to handle other work while waiting for asynchronous I/O.

Important:

Do not block ASP.NET Core request threads unnecessarily.

Avoid:

var result = service.GetDataAsync().Result;

Prefer:

var result = await service.GetDataAsync();
22. Common Task Methods
| Method                 | Purpose                                          |
| ---------------------- | ------------------------------------------------ |
| `Task.Run()`           | Run work asynchronously, commonly CPU-bound work |
| `Task.Delay()`         | Asynchronous delay                               |
| `Task.WhenAll()`       | Wait for all tasks                               |
| `Task.WhenAny()`       | Wait for first completed task                    |
| `Task.FromResult()`    | Create an already-completed Task with a result   |
| `Task.CompletedTask`   | Represent completed Task                         |
| `Task.FromException()` | Create faulted Task                              |
| `Task.FromCanceled()`  | Create canceled Task                             |

23. Task.CompletedTask

Useful when a method returns Task but has no asynchronous work.

public Task DoSomethingAsync()
{
    return Task.CompletedTask;
}

Do not make a method async unnecessarily if it has nothing to await.

24. Task.FromResult

Useful when a result is already available.

Task<int> task = Task.FromResult(100);

int result = await task;
25. Task.Run vs Task.Delay
Task.Run

Used to schedule work.

Task.Run(() =>
{
    // Work
});
Task.Delay

Used to asynchronously wait.

await Task.Delay(1000);

They solve different problems.

26. Task vs async/await

They are related but different.

Task

Represents an operation.

async

Allows a method to use await.

await

Asynchronously waits for a Task.

Example:

public async Task<int> GetValueAsync()
{
    Task<int> task = GetDataAsync();

    int result = await task;

    return result;
}
Product Company Interview Points
Question: What is a Task?

Answer:

A Task is a higher-level abstraction that represents an asynchronous operation or unit of work. It can represent CPU-bound work, I/O-bound operations, or an operation that eventually produces a result.

Question: Task vs Thread?

Key answer:

A Thread is a low-level execution mechanism, while a Task is a higher-level abstraction for representing work and asynchronous operations.

Question: Does Task create a new thread?

Answer:

No. A Task does not necessarily create a new thread. Task.Run() commonly schedules synchronous work to the ThreadPool, while naturally asynchronous I/O can complete without keeping a worker thread blocked.

Question: Does async create a new thread?

Answer:

No. async does not inherently create a new thread.

Question: Task.Run vs await?

Task.Run():

Commonly used to offload synchronous CPU-bound work.

await:

Used to asynchronously wait for an asynchronous operation.

Question: Task.WhenAll vs Task.WhenAny?

WhenAll:

Waits until all supplied tasks complete.

WhenAny:

Completes when any supplied task completes.

Question: Why avoid .Result and .Wait()?

Because they synchronously block the current thread and can reduce scalability. In asynchronous application code, prefer await.

Question: How are exceptions handled in Tasks?

Usually:

try
{
    await task;
}
catch (Exception ex)
{
    // Handle or propagate appropriately
}
Key Points to Remember
Task is a higher-level abstraction than Thread.
Task does not automatically mean a new thread.
Task.Run() commonly uses ThreadPool infrastructure.
async does not automatically create a thread.
await is preferred over blocking with .Wait() or .Result.
Use Task.WhenAll() for independent concurrent operations.
Use Task.WhenAny() when the first completed operation matters.
CPU-bound work and I/O-bound work should be treated differently.
Do not wrap naturally asynchronous I/O in Task.Run() unnecessarily.
Tasks can be canceled using CancellationToken.
Task exceptions must be observed and handled appropriately.
Shared mutable state can cause race conditions.
ASP.NET Core code should generally remain asynchronous end-to-end.
Concurrency and parallelism are different concepts.
ThreadPool and Task are related but are not the same thing.
Mental Model
Thread
    ↓
Low-level execution mechanism

Task
    ↓
Represents work / asynchronous operation

Task.Run()
    ↓
Commonly schedules synchronous work
    ↓
ThreadPool

async/await
    ↓
Asynchronous programming model
    ↓
Avoid unnecessary blocking
Interview-Level Summary

The most important distinction is:

Thread = execution mechanism

Task = abstraction representing work

async/await = asynchronous programming model

ThreadPool = runtime-managed pool of reusable threads
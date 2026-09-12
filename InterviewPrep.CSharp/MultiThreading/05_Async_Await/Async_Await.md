## 1. What is Asynchronous Programming?

Asynchronous programming allows an application to start an operation and continue doing other work instead of blocking while waiting for that operation to complete.

It is especially useful for I/O-bound operations such as:

- HTTP/API calls
- Database calls
- File operations
- Network operations
- Cloud service calls

Example:

await httpClient.GetAsync(url);

Important:

Asynchronous programming is mainly about avoiding unnecessary blocking while waiting.

2. What is async?

async is a C# keyword used to mark a method that can use await.

Example:

public async Task ProcessAsync()
{
    await SomeOperationAsync();
}

Important:

async does not automatically create a new thread.

3. What is await?

await asynchronously waits for a Task to complete.

Example:

public async Task ProcessAsync()
{
    await Task.Delay(1000);

    Console.WriteLine("Completed");
}

The method can pause at await without synchronously blocking the current thread.

4. Basic Async Flow
Method starts
     ↓
Start asynchronous operation
     ↓
await
     ↓
Method yields while operation is incomplete
     ↓
Other work can execute
     ↓
Operation completes
     ↓
Method continues
5. Async Method Return Types

Common return types are:

Task
Task<T>
ValueTask
ValueTask<T>

Most application code commonly uses:

Task
Task<T>

Example:

public async Task SaveAsync()
{
    await SaveToDatabaseAsync();
}

With a result:

public async Task<int> GetCountAsync()
{
    return await GetCountFromDatabaseAsync();
}
6. Task vs Task<T>
Task

Use Task when the asynchronous operation does not return a value.

public async Task ProcessAsync()
{
    await Task.Delay(1000);
}
Task<T>

Use Task<T> when the asynchronous operation returns a value.

public async Task<int> GetNumberAsync()
{
    await Task.Delay(1000);

    return 100;
}

Usage:

int number = await GetNumberAsync();

Mental model:

Task
  ↓
No result

Task<T>
  ↓
Returns T
7. async Does Not Mean Parallel

Async programming:

Avoid unnecessary blocking

Parallel programming:

Execute multiple operations simultaneously

They are different concepts.

Example:

await GetDataAsync();

This is asynchronous.

Multiple independent operations:

Task task1 = GetData1Async();
Task task2 = GetData2Async();

await Task.WhenAll(task1, task2);

This provides concurrent execution.

Remember:

Async ≠ Parallel
8. Async is Most Useful for I/O-Bound Work

I/O-bound operations spend significant time waiting for external resources.

Examples:

HTTP request
Database query
File operation
Network operation
Cloud API call

Example:

var response = await httpClient.GetAsync(url);

While waiting for the response, the application does not need to keep a thread synchronously blocked.

9. CPU-Bound Work

CPU-bound work uses the processor heavily.

Examples:

Large calculations
Image processing
Encryption
Compression
Complex data processing

For synchronous CPU-bound work, Task.Run() can be useful when appropriate.

Example:

int result = await Task.Run(() =>
{
    return Calculate();
});

Important:

Do not use Task.Run() automatically for every asynchronous operation.

10. Task.Run() vs async/await
Task.Run

Commonly used to offload synchronous CPU-bound work.

await Task.Run(() =>
{
    PerformCpuWork();
});
async/await

Used to compose and asynchronously wait for asynchronous operations.

await httpClient.GetAsync(url);

Mental model:

CPU-bound synchronous work
        ↓
     Task.Run()

I/O-bound asynchronous operation
        ↓
      await
11. Do Not Wrap Async I/O in Task.Run() Unnecessarily

Avoid:

await Task.Run(async () =>
{
    await httpClient.GetAsync(url);
});

Prefer:

await httpClient.GetAsync(url);

Why?

Because HttpClient.GetAsync() is already asynchronous.

Adding Task.Run() does not make the network operation more asynchronous.

12. Thread.Sleep() vs Task.Delay()
Thread.Sleep
Thread.Sleep(1000);

Blocks the current thread.

Task.Delay
await Task.Delay(1000);

Asynchronously waits without synchronously blocking the current thread.

For asynchronous application code, prefer:

await Task.Delay(...);
13. Sequential Async Operations

Consider:

await GetUserAsync();

await GetOrdersAsync();

await GetProductsAsync();

These operations are performed one after another.

Conceptually:

GetUser
   ↓
GetOrders
   ↓
GetProducts

If these operations are independent, this may take longer than necessary.

14. Concurrent Async Operations

If operations are independent:

Task userTask = GetUserAsync();
Task ordersTask = GetOrdersAsync();
Task productsTask = GetProductsAsync();

await Task.WhenAll(
    userTask,
    ordersTask,
    productsTask);

Conceptually:

GetUser      ───────────┐
GetOrders    ───────────┼──→ WhenAll
GetProducts  ───────────┘

This can reduce overall waiting time for independent I/O operations.

15. Task.WhenAll()

Task.WhenAll() waits for multiple Tasks to complete.

Example:

Task task1 = Operation1Async();
Task task2 = Operation2Async();
Task task3 = Operation3Async();

await Task.WhenAll(task1, task2, task3);

Useful for:

Multiple API calls
Multiple independent database operations
Multiple independent asynchronous operations

Important:

Start independent operations first, then await them together.

16. Task.WhenAny()

Task.WhenAny() completes when any one of the supplied Tasks completes.

Example:

Task task1 = Operation1Async();
Task task2 = Operation2Async();
Task task3 = Operation3Async();

Task completedTask =
    await Task.WhenAny(task1, task2, task3);

Useful for:

First response wins
Timeout patterns
Racing independent operations
Selecting the first completed operation
17. Async Method Execution

Example:

public async Task ProcessAsync()
{
    Console.WriteLine("Before await");

    await Task.Delay(1000);

    Console.WriteLine("After await");
}

Conceptually:

Before await
     ↓
await
     ↓
Method can yield
     ↓
Delay completes
     ↓
After await

Important:

await is not the same as blocking with .Wait().

18. Task Scheduling

Tasks are scheduled by a Task Scheduler.

For typical Task.Run() usage:

Task.Run()
    ↓
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

Do not assume every Task always represents a ThreadPool thread.

Some Tasks represent asynchronous I/O operations and do not require a worker thread to remain blocked while waiting.

19. Async I/O Execution Model

For an asynchronous I/O operation:

Application
     ↓
Start I/O
     ↓
Thread does not synchronously wait
     ↓
I/O operation continues
     ↓
I/O completes
     ↓
Continuation resumes

This is one of the main reasons async programming improves scalability for I/O-heavy applications.

20. Async Exception Handling

Exceptions from asynchronous operations are normally observed when the Task is awaited.

Example:

try
{
    await GetDataAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Avoid silently swallowing exceptions:

try
{
    await GetDataAsync();
}
catch
{
}

Handle an exception only when you can meaningfully handle, translate, log, or recover from it.

21. Async Exception Propagation

Consider:

Repository
    ↓
throws exception
    ↓
Service
    ↓
Controller
    ↓
Global exception handler

An exception can propagate through awaited asynchronous methods.

Example:

public async Task ServiceAsync()
{
    await RepositoryAsync();
}

If RepositoryAsync() fails, the exception can propagate to the caller.

22. Cancellation with Async/Await

Use CancellationToken for cooperative cancellation.

Example:

public async Task ProcessAsync(
    CancellationToken cancellationToken)
{
    await Task.Delay(
        5000,
        cancellationToken);
}

Caller:

using CancellationTokenSource cts = new();

Task task = ProcessAsync(cts.Token);

cts.Cancel();

await task;

Cancellation is cooperative.

The operation must observe and respond to the token.

23. Cancellation in HTTP Calls

Example:

HttpResponseMessage response =
    await httpClient.GetAsync(
        url,
        cancellationToken);

This allows the HTTP operation to respond to cancellation.

In ASP.NET Core, request cancellation can also be propagated using the request's cancellation token.

24. Async Database Operations

Modern database libraries commonly provide asynchronous methods.

Example with EF Core:

var users = await dbContext.Users
    .ToListAsync(cancellationToken);

Prefer the database provider's native async API.

Avoid unnecessarily doing this:

await Task.Run(() =>
{
    return dbContext.Users.ToList();
});

Prefer:

await dbContext.Users.ToListAsync();
25. Async File Operations

Modern .NET provides asynchronous file APIs.

Example:

string content =
    await File.ReadAllTextAsync(filePath);

This is preferable to artificially wrapping synchronous file I/O in Task.Run().

26. Async HTTP Operations

Example:

HttpClient client = new();

HttpResponseMessage response =
    await client.GetAsync(url);

string content =
    await response.Content.ReadAsStringAsync();

This is a typical I/O-bound asynchronous workflow.

In production applications, HttpClient should normally be managed through the appropriate .NET HTTP client infrastructure rather than creating a new instance for every request.

27. Async ASP.NET Core

ASP.NET Core heavily uses asynchronous APIs.

Example:

[HttpGet]
public async Task<IActionResult> GetUsersAsync()
{
    var users = await service.GetUsersAsync();

    return Ok(users);
}

Service:

public async Task<List<User>> GetUsersAsync()
{
    return await repository.GetUsersAsync();
}

Repository:

public async Task<List<User>> GetUsersAsync()
{
    return await dbContext.Users.ToListAsync();
}

The async flow continues through the application layers.

28. Async All the Way

Prefer:

Controller
    ↓ await
Service
    ↓ await
Repository
    ↓ await
Database

Avoid unnecessarily mixing asynchronous code with blocking calls.

Bad:

var result = service.GetDataAsync().Result;

Better:

var result = await service.GetDataAsync();
29. Why Blocking is Dangerous in ASP.NET Core

Bad:

var result = service.GetDataAsync().Result;

or:

service.GetDataAsync().Wait();

Problems can include:

Thread blocking
Reduced scalability
ThreadPool pressure
Increased latency
Poor resource utilization
Potential deadlocks in environments with synchronization contexts

Prefer:

var result = await service.GetDataAsync();
30. Task.Result

Example:

Task<int> task = GetNumberAsync();

int result = task.Result;

.Result synchronously blocks while waiting.

Prefer:

int result = await GetNumberAsync();

Important:

In asynchronous application code, prefer await over .Result.

31. Task.Wait()

Example:

task.Wait();

This blocks the current thread.

Prefer:

await task;

Especially in:

ASP.NET Core
UI applications
High-throughput services
Library code
32. Async Does Not Automatically Make Code Faster

Async primarily improves resource utilization and scalability for operations that spend time waiting.

For example:

Database request
      ↓
Waiting...
      ↓
Database response

Async allows the application to avoid unnecessarily blocking a thread during the waiting period.

It does not make the database itself faster.

33. Async vs Parallelism
Async	Parallelism
Focuses on non-blocking operations	Focuses on simultaneous execution
Excellent for I/O-bound work	Useful for suitable CPU-bound work
Does not require multiple threads	Often uses multiple threads/CPU cores
Uses async/await	Can use Parallel, Tasks, etc.

Remember:

Async ≠ Parallel
34. Async vs Concurrency

Concurrency means multiple operations can make progress during overlapping periods.

Example:

Task task1 = GetUserAsync();
Task task2 = GetOrdersAsync();

await Task.WhenAll(task1, task2);

This is asynchronous concurrency.

Remember:

Concurrency
    ≠
Parallelism
35. async void

Avoid async void for normal application methods.

Bad:

public async void ProcessAsync()
{
    await Task.Delay(1000);
}

Prefer:

public async Task ProcessAsync()
{
    await Task.Delay(1000);
}

Main exception:

async void is appropriate for certain event handlers.

Example:

private async void Button_Click(
    object sender,
    EventArgs e)
{
    await ProcessAsync();
}
36. async Task vs async void
async Task	async void
Caller can await	Caller cannot await
Exceptions flow through the Task	Exception handling is different
Composable	Not easily composable
Preferred for application methods	Mainly used for event handlers
37. Async Lambda

Example:

Func<Task> operation = async () =>
{
    await Task.Delay(1000);
};

Execute:

await operation();

Be careful with APIs that accept Action instead of Func<Task>.

An async lambda passed to an Action can become async void.

Prefer APIs that support asynchronous delegates when asynchronous work is required.

38. Async Streams

C# supports asynchronous streams using:

IAsyncEnumerable<T>
await foreach

Example:

await foreach (var item in GetItemsAsync())
{
    Console.WriteLine(item);
}

Useful when data arrives asynchronously over time.

Examples:

Streaming API results
Large data processing
Network streams
Database streams
39. IAsyncEnumerable<T>

Example:

public async IAsyncEnumerable<int> GetNumbersAsync()
{
    for (int i = 1; i <= 5; i++)
    {
        await Task.Delay(500);

        yield return i;
    }
}

Consume it:

await foreach (int number in GetNumbersAsync())
{
    Console.WriteLine(number);
}

Important:

IAsyncEnumerable<T> allows asynchronous iteration over a sequence.

40. ValueTask

ValueTask<T> can sometimes reduce allocations when an asynchronous operation frequently completes synchronously.

Example:

public ValueTask<int> GetValueAsync()
{
    return ValueTask.FromResult(100);
}

Important:

Do not automatically replace every Task<T> with ValueTask<T>.

ValueTask is an advanced performance optimization and should generally be used when there is a measurable reason.

41. Async Deadlock Concept

A common problematic pattern in environments with a synchronization context is:

var result = GetDataAsync().Result;

while GetDataAsync() awaits an operation that tries to resume on the blocked context.

Conceptually:

Thread
  ↓
.Result blocks
  ↓
Async operation completes
  ↓
Continuation wants blocked context
  ↓
Deadlock

Modern ASP.NET Core does not have the classic ASP.NET synchronization context, but blocking is still undesirable because it wastes threads and hurts scalability.

42. Synchronization Context

A synchronization context can determine where an async continuation resumes in some application environments.

Examples:

WPF
Windows Forms
Classic ASP.NET

Modern ASP.NET Core does not have the classic ASP.NET synchronization context.

43. ConfigureAwait

ConfigureAwait(false) can be used when code does not need to resume on a captured synchronization context.

Example:

await SomeOperationAsync()
    .ConfigureAwait(false);

It is especially relevant in:

Libraries
Reusable components
Code where a specific context is not required

In ASP.NET Core, there is generally no classic synchronization context to capture.

44. Async and ThreadPool

For I/O-bound asynchronous operations:

Start I/O
    ↓
Thread does not synchronously block
    ↓
I/O continues
    ↓
I/O completes
    ↓
Continuation resumes

For CPU-bound synchronous work using Task.Run():

Task.Run()
    ↓
ThreadPool
    ↓
Worker thread
    ↓
CPU work

These are different execution models.

45. Task.CompletedTask

Useful when a method returns Task but there is no asynchronous work to perform.

Example:

public Task DoSomethingAsync()
{
    return Task.CompletedTask;
}

Do not make a method async unnecessarily if it has nothing to await.

46. Task.FromResult()

Useful when a result is already available.

Example:

Task<int> task =
    Task.FromResult(100);

int result = await task;
47. Task.FromException()

Creates a faulted Task.

Example:

Task task =
    Task.FromException(
        new InvalidOperationException("Failed."));

Useful when implementing APIs that need to return a failed Task.

48. Task.FromCanceled()

Creates a canceled Task.

Example:

using CancellationTokenSource cts = new();

cts.Cancel();

Task task =
    Task.FromCanceled(cts.Token);

This is useful when an API needs to return a canceled Task.

49. Async Method Naming Convention

Async methods should normally end with Async.

Good:

GetUserAsync()
SaveOrderAsync()
ProcessPaymentAsync()

Avoid:

GetUser()
SaveOrder()
ProcessPayment()

when the method is actually asynchronous.

The Async suffix makes the API intention clear.

50. Do Not Use async Without await Unnecessarily

Example:

public async Task ProcessAsync()
{
    Console.WriteLine("Processing");
}

If there is no asynchronous operation, async may be unnecessary.

Better:

public Task ProcessAsync()
{
    Console.WriteLine("Processing");

    return Task.CompletedTask;
}

However, do not artificially use Task.CompletedTask if the method will soon contain real asynchronous work.

51. Returning a Task Directly

This:

public Task<User> GetUserAsync()
{
    return repository.GetUserAsync();
}

can be preferable to:

public async Task<User> GetUserAsync()
{
    return await repository.GetUserAsync();
}

when the method has no additional asynchronous logic.

If additional processing, exception handling, or other logic is required, async/await may be appropriate.

52. Async Method with Multiple Awaits

Example:

public async Task ProcessOrderAsync()
{
    var user = await GetUserAsync();

    var order = await GetOrderAsync(user.Id);

    await SaveOrderAsync(order);
}

This is sequential because each operation depends on the previous result.

Conceptually:

GetUser
   ↓
GetOrder
   ↓
SaveOrder

Do not use WhenAll() when operations have dependencies.

53. Async Method with Independent Operations

If operations are independent:

public async Task ProcessAsync()
{
    Task userTask = GetUserAsync();
    Task productTask = GetProductAsync();

    await Task.WhenAll(
        userTask,
        productTask);
}

This allows both operations to be started before waiting for completion.

54. Async Concurrency Limits

Do not start thousands of operations at once without considering system capacity.

Example:

100,000 requests
       ↓
Too much concurrency
       ↓
Database overloaded
       ↓
Latency increases
       ↓
Failures increase

Use appropriate concurrency limits when necessary.

Possible tools:

SemaphoreSlim
Bounded channels
Rate limiting
Connection pool limits
Service-specific concurrency controls
55. Async and Timeouts

Long-running asynchronous operations should often have appropriate timeouts.

Example:

using CancellationTokenSource cts =
    new(TimeSpan.FromSeconds(5));

await ProcessAsync(cts.Token);

Timeouts prevent operations from waiting indefinitely.

In real applications, use the timeout mechanisms appropriate to the API being called.

56. Async and Retries

Retries should not blindly be applied to every failure.

Retry may be appropriate for transient failures such as:

Temporary network failures
Temporary service unavailability
Some transient database failures

Do not automatically retry:

Validation failures
Authentication failures
Business rule failures
Permanent errors

Retries should normally use:

Maximum retry count
Timeout
Backoff
Jitter where appropriate
57. Async in Microservices

Async programming is commonly used in microservices for:

Calling other services
Database access
Message processing
External APIs
Cloud services

Example:

Order Service
     ↓ await
Payment Service
     ↓
Database

For long-running workflows, asynchronous messaging may be more appropriate than keeping an HTTP request open.

58. Async vs Message Queue

Async/await:

Application
   ↓
Wait asynchronously
   ↓
Operation completes

Message queue:

Service A
   ↓
Message Queue
   ↓
Service B

A message queue provides decoupling and durable communication capabilities that async/await alone does not provide.

Examples:

RabbitMQ
Azure Service Bus
Kafka
59. Common Async Mistakes
Mistake 1: Using .Result
var result = GetDataAsync().Result;

Prefer:

var result = await GetDataAsync();
Mistake 2: Using .Wait()
GetDataAsync().Wait();

Prefer:

await GetDataAsync();
Mistake 3: Using Task.Run() for every operation

Avoid:

await Task.Run(async () =>
{
    await httpClient.GetAsync(url);
});

Prefer:

await httpClient.GetAsync(url);
Mistake 4: Using async void

Avoid:

public async void ProcessAsync()
{
}

Prefer:

public async Task ProcessAsync()
{
}
Mistake 5: Running dependent operations concurrently

Do not do:

Task task1 = GetUserAsync();
Task task2 = GetOrderUsingUserAsync();

await Task.WhenAll(task1, task2);

if GetOrderUsingUserAsync() requires the user result.

60. Product Company Interview Questions
Q1. What is async/await?

Answer:

async and await provide a programming model for asynchronous operations. async allows a method to use await, and await asynchronously waits for a Task without synchronously blocking the current thread.

Q2. Does async create a new thread?

Answer:

No. async itself does not create a new thread. For I/O-bound operations, asynchronous APIs can allow the current thread to be released while the operation is waiting.

Q3. Does await create a new thread?

Answer:

No. await does not inherently create a new thread. It asynchronously waits for an operation represented by a Task.

Q4. What is the difference between Task and Thread?

Answer:

Thread is a low-level execution mechanism, while Task is a higher-level abstraction representing work or an asynchronous operation.

Q5. What is Task.Run used for?

Answer:

Task.Run() is commonly used to offload synchronous CPU-bound work to the ThreadPool. It should not be unnecessarily used around APIs that are already asynchronous.

Q6. What is Task.WhenAll?

Answer:

Task.WhenAll() creates a Task that completes when all supplied Tasks have completed. It is useful for independent concurrent asynchronous operations.

Q7. What is Task.WhenAny?

Answer:

Task.WhenAny() completes when any supplied Task completes.

Q8. Why should we avoid .Result and .Wait()?

Answer:

They synchronously block the current thread, which can reduce scalability and cause deadlocks in some synchronization-context environments.

Q9. What is the difference between Task and Task<T>?

Answer:

Task represents an asynchronous operation without a result, while Task<T> represents an asynchronous operation that eventually produces a value of type T.

Q10. What is async void?

Answer:

async void is generally avoided because the caller cannot await it and exception handling/composition is different. It is mainly appropriate for event handlers.

Q11. What is ConfigureAwait(false)?

Answer:

ConfigureAwait(false) tells the await operation not to require resuming on a captured synchronization context. It is particularly relevant in library code where a specific context is not required.

Q12. Can async code run sequentially?

Answer:

Yes. If multiple asynchronous operations are awaited one after another, they execute sequentially from the caller's perspective.

Q13. How do you execute independent async operations concurrently?

Answer:

Task task1 = Operation1Async();
Task task2 = Operation2Async();

await Task.WhenAll(task1, task2);
Q14. Does asynchronous programming always improve performance?

Answer:

No. Async mainly improves scalability and resource utilization for operations that spend time waiting, especially I/O-bound operations. It does not automatically make the underlying operation faster.

Q15. What is async all the way?

Answer:

It means keeping asynchronous operations asynchronous through the call chain instead of starting async work and then blocking with .Result or .Wait().

61. Enterprise Example

Consider an e-commerce order API.

Controller
     ↓
Order Service
     ↓
 ┌───────────────┬───────────────┐
 ↓               ↓               ↓
Customer API   Product API    Inventory API

If these operations are independent:

Task customerTask = GetCustomerAsync();
Task productTask = GetProductAsync();
Task inventoryTask = GetInventoryAsync();

await Task.WhenAll(
    customerTask,
    productTask,
    inventoryTask);

Then process the results.

This can reduce total waiting time compared with calling each service sequentially.

62. Important Design Considerations

When designing asynchronous code, consider:

Is the operation CPU-bound or I/O-bound?
Is the operation already asynchronous?
Are operations independent?
Can they safely run concurrently?
What happens if one operation fails?
How should cancellation work?
Is a timeout required?
Could unlimited concurrency overload a dependency?
Does the caller need the result?
Does the operation need to preserve ordering?
63. Async Mental Model
                ASYNC PROGRAMMING
                       |
          +------------+------------+
          |                         |
       I/O-bound                CPU-bound
          |                         |
     Native async API          Synchronous work
          |                         |
        await                   Task.Run()
          |                         |
   Avoid blocking              ThreadPool
Key Points to Remember
async does not create a new thread.
await does not automatically create a new thread.
Task represents an operation or unit of work.
Task<T> represents an operation that returns a result.
Async is especially useful for I/O-bound operations.
Task.Run() is commonly useful for synchronous CPU-bound work.
Do not unnecessarily wrap asynchronous I/O inside Task.Run().
Prefer await over .Result and .Wait().
Task.WhenAll() waits for all Tasks.
Task.WhenAny() waits for the first completed Task.
Async does not automatically mean parallel.
Concurrency does not automatically mean parallelism.
Use CancellationToken for cooperative cancellation.
Prefer async Task over async void.
async void is mainly appropriate for event handlers.
Use native async database APIs such as EF Core's ToListAsync().
Use native async file APIs such as ReadAllTextAsync().
ASP.NET Core applications should generally use async all the way.
Avoid blocking asynchronous operations in web applications.
Do not use unlimited concurrency against databases or external services.
Use timeouts for operations that should not wait indefinitely.
Retry only appropriate transient failures.
IAsyncEnumerable<T> supports asynchronous streaming.
ValueTask is an advanced optimization and should not replace Task everywhere.
Async improves scalability/resource utilization; it does not magically make operations faster.
Final Mental Model
async
  ↓
Allows await inside the method

await
  ↓
Asynchronously waits for a Task

Task
  ↓
Represents an asynchronous operation

Task<T>
  ↓
Represents an asynchronous operation with a result

Task.Run()
  ↓
Commonly offloads synchronous CPU-bound work
  ↓
ThreadPool

Task.WhenAll()
  ↓
Wait for all independent operations

Task.WhenAny()
  ↓
Wait for first completed operation

CancellationToken
  ↓
Cooperative cancellation

IAsyncEnumerable<T>
  ↓
Asynchronous streaming
Product Company One-Line Summary

Async/await is primarily an asynchronous programming model that helps applications avoid unnecessary thread blocking, especially during I/O-bound operations,
while Task provides the abstraction used to represent those asynchronous operations.
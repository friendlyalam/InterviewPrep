# CancellationToken

## 1. What is CancellationToken?

`CancellationToken` is a .NET mechanism used to **request cancellation of an ongoing operation**.

It allows long-running or asynchronous operations to stop cooperatively instead of being forcefully terminated.

```text
CancellationTokenSource
        |
        | Cancel()
        v
CancellationToken
        |
        v
Running Operation
        |
        | observes cancellation
        v
Stops gracefully
Important

Cancellation is cooperative.

The token does not forcibly kill a thread or task.

The running operation must observe the token and decide how to stop.

2. Why Do We Need CancellationToken?

Real applications frequently need to stop work before it naturally finishes.

Examples:

User cancels an HTTP request.
User closes a page.
API request times out.
Application is shutting down.
Background job is stopped.
A dependent service fails.
A long-running calculation is no longer required.
A request is cancelled because the client disconnected.

Without cancellation, unnecessary work may continue.

3. CancellationTokenSource

CancellationTokenSource is responsible for requesting cancellation.

using CancellationTokenSource cts = new();

cts.Cancel();

It provides the token:

CancellationToken token = cts.Token;

Mental model:

CancellationTokenSource
        |
        +---- Cancel()
        |
        v
CancellationToken
        |
        v
Operation observes cancellation
4. CancellationToken

CancellationToken is the value passed to the operation.

Example:

public async Task ProcessAsync(
    CancellationToken cancellationToken)
{
    ...
}

The method can check:

cancellationToken.IsCancellationRequested

or:

cancellationToken.ThrowIfCancellationRequested();
5. Basic Cancellation Example
using CancellationTokenSource cts = new();

Task task = ProcessAsync(cts.Token);

await Task.Delay(1000);

cts.Cancel();

await task;

The important flow is:

Start operation
      ↓
Pass token
      ↓
Operation starts
      ↓
Cancel requested
      ↓
Token becomes cancelled
      ↓
Operation observes token
      ↓
Operation stops
6. IsCancellationRequested

The simplest way to check cancellation is:

if (cancellationToken.IsCancellationRequested)
{
    return;
}

Example:

private static async Task ProcessAsync(
    CancellationToken cancellationToken)
{
    for (int i = 1; i <= 10; i++)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("Cancellation requested.");
            return;
        }

        Console.WriteLine($"Processing item {i}");

        await Task.Delay(500);
    }
}
7. ThrowIfCancellationRequested

A more common pattern for Task-based APIs is:

cancellationToken.ThrowIfCancellationRequested();

Example:

private static async Task ProcessAsync(
    CancellationToken cancellationToken)
{
    for (int i = 1; i <= 10; i++)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Console.WriteLine($"Processing item {i}");

        await Task.Delay(
            500,
            cancellationToken);
    }
}

If cancellation has been requested, it throws:

OperationCanceledException
8. OperationCanceledException

Cancellation normally results in:

OperationCanceledException

Example:

try
{
    await ProcessAsync(cancellationToken);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation cancelled.");
}

This is different from an unexpected application failure.

Cancellation
    → expected control flow

Exception
    → unexpected failure

Cancellation should generally not be logged as a system error.

9. TaskCanceledException

TaskCanceledException derives from OperationCanceledException.

Exception
   |
   └── SystemException
          |
          └── OperationCanceledException
                  |
                  └── TaskCanceledException

In most application code, catching:

OperationCanceledException

is preferable when you want to handle cancellation generally.

10. CancellationToken with Task.Delay

Always prefer the overload that accepts the token when cancellation should interrupt the delay.

await Task.Delay(
    5000,
    cancellationToken);

Without the token:

await Task.Delay(5000);

cancelling the operation does not cancel that delay.

With the token:

await Task.Delay(
    5000,
    cancellationToken);

the delay itself observes cancellation.

11. CancellationToken with HTTP Calls

Modern .NET APIs commonly accept CancellationToken.

Example:

HttpResponseMessage response =
    await httpClient.GetAsync(
        url,
        cancellationToken);

If the request is cancelled:

Client request
      ↓
ASP.NET Core
      ↓
HttpClient
      ↓
CancellationToken
      ↓
HTTP operation cancelled

This is extremely important in ASP.NET Core applications.

12. CancellationToken with Database Operations

Many EF Core operations accept a cancellation token.

Example:

var users = await dbContext.Users
    .ToListAsync(cancellationToken);

If the request is cancelled, EF Core can propagate the cancellation to the database operation.

This avoids unnecessarily continuing work that the caller no longer needs.

13. CancellationToken Propagation

One of the most important enterprise practices is:

Receive the token at the boundary and propagate it through the call chain.

Example:

public async Task<Order> GetOrderAsync(
    int orderId,
    CancellationToken cancellationToken)
{
    return await repository.GetOrderAsync(
        orderId,
        cancellationToken);
}

Repository:

public async Task<Order> GetOrderAsync(
    int orderId,
    CancellationToken cancellationToken)
{
    return await dbContext.Orders
        .FirstAsync(
            x => x.Id == orderId,
            cancellationToken);
}

Flow:

HTTP Request
     ↓
Controller
     ↓
Service
     ↓
Repository
     ↓
EF Core
     ↓
Database

The cancellation token should travel through the chain.

14. CancellationToken Should Not Be Created Everywhere

Bad design:

public async Task ProcessAsync()
{
    using CancellationTokenSource cts = new();

    ...
}

If the caller already owns the cancellation request, creating another unrelated token breaks cancellation propagation.

Better:

public async Task ProcessAsync(
    CancellationToken cancellationToken)
{
    ...
}

The caller controls cancellation.

15. CancellationToken.None

If cancellation is not required:

CancellationToken.None

can be used.

Example:

await ProcessAsync(
    CancellationToken.None);

It represents a token that will never be cancelled.

16. Default CancellationToken

You may see:

public Task ProcessAsync(
    CancellationToken cancellationToken = default)
{
    ...
}

This makes cancellation optional.

Example:

await ProcessAsync();

or:

await ProcessAsync(cancellationToken);

For reusable application services, this can sometimes be convenient, but cancellation should be propagated when available.

17. CancellationTokenSource.Cancel()

Calling:

cts.Cancel();

requests cancellation.

Important:

Cancel()
≠
Kill thread

It simply signals:

CancellationToken.IsCancellationRequested == true

The operation must observe the signal.

18. CancellationTokenSource.CancelAfter()

You can automatically request cancellation after a specified period.

using CancellationTokenSource cts =
    new();

cts.CancelAfter(TimeSpan.FromSeconds(5));

await ProcessAsync(cts.Token);

This is useful for time limits.

Example:

Start operation
      ↓
Allow 5 seconds
      ↓
CancelAfter()
      ↓
Cancellation requested
      ↓
Operation stops
19. Timeout vs Cancellation

These concepts are related but not identical.

Timeout

Means:

The operation did not finish within the allowed time.

Cancellation

Means:

Someone requested that the operation stop.

A timeout can be implemented by triggering cancellation.

Example:

using CancellationTokenSource cts =
    new(TimeSpan.FromSeconds(5));

await ProcessAsync(cts.Token);
20. CancellationTokenSource.CreateLinkedTokenSource

Sometimes an operation should stop when any of multiple cancellation sources requests cancellation.

Example:

using CancellationTokenSource linkedCts =
    CancellationTokenSource.CreateLinkedTokenSource(
        requestToken,
        shutdownToken);

Now:

Request cancellation
       ↓
       ┐
       │
       ├── Linked token → Operation stops
       │
Shutdown cancellation
       ↓

This is very useful in ASP.NET Core and background processing.

21. Real-World Linked Cancellation Example

Suppose a background operation should stop when:

The user cancels the request.
The application shuts down.
using CancellationTokenSource linkedCts =
    CancellationTokenSource.CreateLinkedTokenSource(
        requestCancellationToken,
        applicationStoppingToken);

await ProcessAsync(
    linkedCts.Token);

Now either source can stop the operation.

22. Cancellation in ASP.NET Core

ASP.NET Core provides request cancellation through:

HttpContext.RequestAborted

Example:

public async Task<IActionResult> GetOrders(
    CancellationToken cancellationToken)
{
    var orders = await service.GetOrdersAsync(
        cancellationToken);

    return Ok(orders);
}

ASP.NET Core can bind the request cancellation token automatically.

The token can then flow:

HTTP Client
     ↓
ASP.NET Core Request
     ↓
CancellationToken
     ↓
Service
     ↓
Repository
     ↓
Database
23. Why Request Cancellation Matters

Suppose a client requests:

GET /orders

The server starts:

Database query
      ↓
Processing
      ↓
External API

But the user closes the browser.

Continuing all the work may waste:

CPU
database connections
network resources
memory
ThreadPool resources
downstream API capacity

Proper cancellation allows the server to stop unnecessary work.

24. Cancellation in BackgroundService

ASP.NET Core BackgroundService provides a stopping token.

Example:

protected override async Task ExecuteAsync(
    CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        await ProcessWorkAsync(
            stoppingToken);
    }
}

When the application shuts down:

Application shutdown
       ↓
stoppingToken cancelled
       ↓
BackgroundService observes token
       ↓
Loop exits
       ↓
Graceful shutdown
25. Cancellation in Loops

For long-running loops:

while (!cancellationToken.IsCancellationRequested)
{
    await ProcessAsync(cancellationToken);
}

Better when appropriate:

while (true)
{
    cancellationToken.ThrowIfCancellationRequested();

    await ProcessAsync(cancellationToken);
}
26. Cancellation Does Not Automatically Stop Synchronous Work

Consider:

public void Process()
{
    for (int i = 0; i < 1_000_000_000; i++)
    {
        Calculate(i);
    }
}

Passing a token somewhere else does not magically stop this method.

The method must periodically observe cancellation.

Example:

public void Process(
    CancellationToken cancellationToken)
{
    for (int i = 0; i < 1_000_000_000; i++)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Calculate(i);
    }
}
27. Cancellation with CPU-Bound Work

For CPU-bound work:

await Task.Run(
    () => Calculate(cancellationToken),
    cancellationToken);

Inside:

private static int Calculate(
    CancellationToken cancellationToken)
{
    int result = 0;

    for (int i = 0; i < 1_000_000; i++)
    {
        cancellationToken.ThrowIfCancellationRequested();

        result += i;
    }

    return result;
}

Important:

Task.Run(..., cancellationToken) does not magically terminate already-running CPU work.

The delegate itself should observe the token.

28. Cancellation and Task.Run

This:

Task.Run(
    () => Work(),
    cancellationToken);

allows cancellation before the Task starts.

But once Work() is already running, the token does not forcibly terminate it.

Therefore:

Task.Run(
    () => Work(cancellationToken),
    cancellationToken);

is better when the work itself supports cancellation.

29. CancellationToken Registration

You can register a callback:

using CancellationTokenRegistration registration =
    cancellationToken.Register(
        () => Console.WriteLine(
            "Cancellation requested."));

When cancellation occurs, the callback runs.

Example:

using CancellationTokenSource cts = new();

cts.Token.Register(
    () => Console.WriteLine(
        "Cancellation callback executed."));

cts.Cancel();

Use registration carefully.

Do not put expensive or complicated business logic inside cancellation callbacks.

30. CancellationToken Is Struct

CancellationToken is a value type (struct).

CancellationTokenSource is the object that manages the cancellation state.

Mental model:

CancellationTokenSource
    |
    +-- cancellation state
    |
    +-- Cancel()
    |
    +-- Token
          |
          v
   CancellationToken
31. CancellationToken Is Safe to Pass Around

The token is designed to be passed through multiple layers.

Example:

Controller
    ↓
Service(token)
    ↓
Repository(token)
    ↓
HttpClient(token)

This is the normal enterprise pattern.

32. Do Not Swallow Cancellation

Bad:

try
{
    await ProcessAsync(token);
}
catch (Exception)
{
    return;
}

This catches cancellation as well.

Better:

try
{
    await ProcessAsync(token);
}
catch (OperationCanceledException)
{
    throw;
}
catch (Exception ex)
{
    // Handle actual failure.
}

Depending on the application boundary, cancellation may instead be intentionally handled and converted to an appropriate response. The important point is not to accidentally treat cancellation as an unexpected error.

33. Cancellation and finally

finally still executes when cancellation causes an exception.

Example:

try
{
    await ProcessAsync(token);
}
finally
{
    ReleaseResource();
}

This is important for:

locks
semaphores
database resources
temporary files
connections
cleanup
34. Cancellation with SemaphoreSlim

Correct pattern:

await semaphore.WaitAsync(
    cancellationToken);

try
{
    await ProcessAsync(
        cancellationToken);
}
finally
{
    semaphore.Release();
}

This combines:

async waiting
cancellation
synchronization
guaranteed release
35. Cancellation and Parallel Operations

When multiple operations are running:

Task task1 =
    ProcessAsync(token);

Task task2 =
    ProcessAsync(token);

Task task3 =
    ProcessAsync(token);

await Task.WhenAll(
    task1,
    task2,
    task3);

Cancelling the token requests cancellation from all operations that observe that token.

Token
  |
  +---- Task 1
  |
  +---- Task 2
  |
  +---- Task 3
36. Cancellation with Task.WhenAny

A common pattern is:

Operation
      +
Timeout
      ↓
Task.WhenAny()
      ↓
Which completed first?

For example:

using CancellationTokenSource cts =
    new();

Task operation =
    ProcessAsync(cts.Token);

Task timeout =
    Task.Delay(5000);

Task completed =
    await Task.WhenAny(
        operation,
        timeout);

if (completed == timeout)
{
    cts.Cancel();
}

The important point:

Task.WhenAny() does not automatically cancel the remaining operation.

You must explicitly cancel it.

37. Cancellation and Retry

Be careful when combining cancellation with retries.

Example:

Retry attempt 1
     ↓
fails
     ↓
Retry attempt 2
     ↓
cancellation requested
     ↓
STOP

Cancellation should normally take priority over continuing retries.

A retry policy should observe the same cancellation token.

38. Cancellation and Idempotency

Cancellation does not guarantee that work never happened.

For example:

Send payment request
       ↓
Payment succeeds
       ↓
Client disconnects
       ↓
Server observes cancellation

The client may think:

Payment failed

while the payment actually succeeded.

Therefore distributed systems often need:

idempotency keys
transaction design
durable state
proper retry handling

Cancellation is not a replacement for idempotency.

39. Cancellation and Database Transactions

Cancellation can stop a database operation, but business consistency is a separate concern.

Example:

Create Order
    ↓
Reserve Inventory
    ↓
Charge Payment

If cancellation occurs between these operations, the system still needs appropriate transactional or compensating behavior.

Cancellation ≠ transaction rollback of arbitrary completed work.

40. Cancellation vs Thread.Abort

Do not think of CancellationToken as a modern version of killing a thread.

Conceptually:

Thread.Abort
    → forceful interruption concept

CancellationToken
    → cooperative cancellation

Cooperative cancellation is safer and fits modern async programming.

41. CancellationToken vs Thread.Sleep

This:

Thread.Sleep(5000);

does not accept a cancellation token.

Prefer:

await Task.Delay(
    5000,
    cancellationToken);

when working asynchronously.

Now the delay can be cancelled.

42. CancellationToken vs Task.Delay
| Feature                 | `Task.Delay()` | `Task.Delay(..., token)` |
| ----------------------- | -------------- | ------------------------ |
| Async                   | Yes            | Yes                      |
| Cancellation            | No             | Yes                      |
| Blocks thread           | No             | No                       |
| Suitable for async code | Yes            | Yes                      |
| Can stop waiting early  | No             | Yes                      |

43. CancellationToken vs CancellationTokenSource
| Feature           | CancellationToken             | CancellationTokenSource |
| ----------------- | ----------------------------- | ----------------------- |
| Purpose           | Represents cancellation state | Requests cancellation   |
| Calls `Cancel()`  | No                            | Yes                     |
| Passed to methods | Yes                           | Usually no              |
| Can be cancelled  | No                            | Yes                     |
| Main role         | Observe                       | Control                 |


Mental model:

CTS = controller

Token = signal
44. CancellationToken vs Timeout
| Cancellation             | Timeout                               |
| ------------------------ | ------------------------------------- |
| Explicit request to stop | Time limit exceeded                   |
| Can come from user       | Usually time-based                    |
| Can come from shutdown   | Automatically triggered               |
| Can be linked            | Can be implemented using cancellation |
| Cooperative              | Cooperative                           |

45. CancellationToken vs Exception

Cancellation normally uses:

OperationCanceledException

But cancellation is generally not treated as an unexpected application failure.

Business/technical failure
        ↓
Exception handling

Expected cancellation
        ↓
Cancellation handling
46. Common Cancellation Mistakes
Mistake 1: Creating a new token inside every method
new CancellationTokenSource()

inside every layer breaks propagation.

Mistake 2: Not passing the token to async APIs

Bad:

await Task.Delay(5000);

Better:

await Task.Delay(
    5000,
    cancellationToken);
Mistake 3: Assuming Cancel() kills the operation
cts.Cancel();

does not forcibly terminate running code.

Mistake 4: Ignoring cancellation in CPU loops

Long CPU operations should periodically observe the token.

Mistake 5: Swallowing OperationCanceledException

Do not accidentally convert cancellation into a successful result.

Mistake 6: Not propagating request cancellation

In ASP.NET Core:

Request
  ↓
Service
  ↓
Repository

The token should normally flow through the chain.

Mistake 7: Treating cancellation as an error

Cancellation is often expected application behavior.

Mistake 8: Forgetting to dispose CancellationTokenSource

Prefer:

using CancellationTokenSource cts =
    new();

when the source owns disposable resources and its lifetime is local.

47. CancellationToken and Thread Safety

CancellationToken is designed to be safely shared among multiple operations.

Example:

Task task1 = ProcessAsync(token);
Task task2 = ProcessAsync(token);
Task task3 = ProcessAsync(token);

All operations can observe the same cancellation request.

The important distinction is:

Shared cancellation signal
        ≠
Shared mutable application state
48. CancellationToken and ThreadPool

Cancellation can help prevent unnecessary ThreadPool work.

For example:

HTTP request
     ↓
CPU/IO operation
     ↓
Client disconnects
     ↓
Cancellation requested
     ↓
Operation stops
     ↓
Resources become available

This can improve application efficiency under load.

Cancellation does not automatically solve ThreadPool starvation, but proper cancellation can prevent unnecessary work from continuing.

49. Cancellation in Microservices

Consider:

Order Service
      ↓
Payment Service
      ↓
Inventory Service

If the original request is cancelled, downstream calls should normally receive a suitable cancellation token.

Request Token
      ↓
Order Service
      ↓
Payment API
      ↓
Inventory API

This prevents unnecessary downstream work.

However, cancellation across distributed systems must be designed carefully because remote work may already have completed.

50. Cancellation Is Not Distributed Transaction Management

This is an important interview distinction.

Cancellation can tell your current operation:

STOP

It cannot guarantee:

ROLL BACK EVERYTHING EVERYWHERE

For distributed business workflows, you may need:

transactions
Saga
compensating actions
idempotency
durable messaging
state tracking
51. Product-Company Example: Search API

Imagine:

Search request
      ↓
Product API
      ↓
Database
      ↓
Recommendation API

The user closes the page.

A good design:

Client disconnect
      ↓
RequestAborted
      ↓
CancellationToken
      ↓
Database query cancelled
      ↓
Recommendation call cancelled
      ↓
Unnecessary work stops

This improves resource utilization.

52. Product-Company Example: File Processing
public async Task ProcessFileAsync(
    string filePath,
    CancellationToken cancellationToken)
{
    foreach (string line in File.ReadLines(filePath))
    {
        cancellationToken.ThrowIfCancellationRequested();

        await ProcessLineAsync(
            line,
            cancellationToken);
    }
}

Benefits:

long processing can stop
resources are not wasted
caller controls cancellation
cancellation propagates to lower layers
53. Product-Company Example: Background Worker
protected override async Task ExecuteAsync(
    CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        await ProcessQueueAsync(
            stoppingToken);
    }
}

When application shutdown begins:

Host shutdown
     ↓
stoppingToken cancelled
     ↓
Worker stops accepting new work
     ↓
Current operation observes cancellation
     ↓
Worker exits gracefully
54. Cancellation Design Guidelines
Guideline 1

Accept CancellationToken in long-running or asynchronous methods.

Guideline 2

Propagate the token to lower layers.

Guideline 3

Pass the token to APIs that support cancellation.

Guideline 4

Use ThrowIfCancellationRequested() for cooperative checks.

Guideline 5

Do not forcibly terminate threads.

Guideline 6

Do not create unrelated cancellation sources in every layer.

Guideline 7

Use linked tokens when multiple cancellation conditions exist.

Guideline 8

Treat cancellation separately from unexpected failures.

Guideline 9

Use cancellation with timeouts and graceful shutdown.

Guideline 10

Remember that cancellation does not undo already completed distributed work.

55. When Should You Use CancellationToken?

Use it for:

HTTP requests
database operations
file processing
long-running calculations
background services
queue processing
external API calls
polling loops
asynchronous workflows
application shutdown
request timeout handling
56. When CancellationToken Is Less Important

A token may not be necessary for very small, instantaneous operations.

Example:

public int Add(
    int a,
    int b)
{
    return a + b;
}

There is normally no meaningful cancellation point.

Do not add cancellation parameters everywhere without reason.

57. Cancellation and Resource Cleanup

Cancellation should always be considered together with cleanup.

Example:

try
{
    await ProcessAsync(
        cancellationToken);
}
catch (OperationCanceledException)
{
    Console.WriteLine(
        "Operation cancelled.");
}
finally
{
    Cleanup();
}

Cancellation should leave the application in a valid state.

58. Cancellation Mental Model

Remember:

CancellationTokenSource
        |
        | Cancel()
        v
CancellationToken
        |
        | observed by
        v
Operation
        |
        | stops cooperatively
        v
OperationCanceledException

The most important word is:

Cooperative

59. Interview Questions
Q1. What is CancellationToken?

A mechanism for cooperative cancellation of asynchronous or long-running operations in .NET.

Q2. Does CancellationToken kill a thread?

No.

It only requests cancellation. The running operation must observe the token and stop cooperatively.

Q3. What is CancellationTokenSource?

It manages cancellation state and provides the CancellationToken.

It can request cancellation using:

cts.Cancel();
Q4. Difference between CancellationToken and CancellationTokenSource?
CancellationTokenSource
→ requests cancellation

CancellationToken
→ observes cancellation
Q5. What does ThrowIfCancellationRequested() do?

It checks whether cancellation was requested and throws OperationCanceledException if so.

Q6. Why pass CancellationToken through service and repository layers?

To propagate the original cancellation request and stop unnecessary downstream work.

Q7. What is CreateLinkedTokenSource?

It combines multiple cancellation tokens into one linked token.

Cancellation of any linked source can cancel the combined token.

Q8. Does Task.Run(token) stop already-running work?

No.

It can prevent work from starting if cancellation occurs before execution begins, but already-running code must observe the token itself.

Q9. Does Task.WhenAny cancel remaining tasks?

No.

WhenAny() only tells you which task completed first.

You must explicitly cancel the remaining operations.

Q10. Is cancellation the same as timeout?

No.

A timeout is a time-based condition; cancellation is a request to stop. A timeout can trigger cancellation.

Q11. Should OperationCanceledException be logged as an error?

Usually no. Expected cancellation is normally control flow rather than an unexpected application error.

Q12. Can CancellationToken undo completed work?

No.

If a remote operation already completed, cancellation cannot magically roll it back.

60. Common Product-Company Interview Scenario
Question

A user starts downloading a large report. The user closes the browser. What should the server do?

Strong answer

The request cancellation token should propagate through the application layers and into the underlying asynchronous operations. When the client disconnects, ASP.NET Core can signal request cancellation. The report generation, database query, file processing, and downstream calls should observe the token and stop unnecessary work where supported.

Also, cancellation should be distinguished from rollback because some external work may already have completed.

61. CancellationToken vs Other Concepts
| Concept                   | Main Purpose                       |
| ------------------------- | ---------------------------------- |
| `CancellationToken`       | Cooperative cancellation           |
| `CancellationTokenSource` | Request cancellation               |
| `Task`                    | Represents asynchronous work       |
| `async/await`             | Asynchronous programming model     |
| `SemaphoreSlim`           | Limit/coordinate concurrent access |
| `lock`                    | Synchronous mutual exclusion       |
| `Interlocked`             | Atomic operations                  |
| Timeout                   | Limit maximum allowed time         |
| Retry                     | Repeat failed operations           |
| Circuit Breaker           | Stop calls to failing dependency   |
| Idempotency               | Safely handle repeated requests    |
| Transaction               | Maintain transactional consistency |


62. Important Comparison: Cancellation vs Abort vs Timeout
| Feature                      | CancellationToken | Forceful Abort        | Timeout                           |
| ---------------------------- | ----------------- | --------------------- | --------------------------------- |
| Cooperative                  | Yes               | No                    | Usually implemented cooperatively |
| Safe for async workflows     | Yes               | Generally undesirable | Yes                               |
| Stops thread forcibly        | No                | Conceptually yes      | No                                |
| Caller can request           | Yes               | Depends               | Usually indirectly                |
| Can propagate through layers | Yes               | No                    | Yes                               |
| Suitable for modern .NET     | Yes               | Avoid                 | Yes                               |


63. Important Comparison: IsCancellationRequested vs ThrowIfCancellationRequested
F| Feature                | `IsCancellationRequested` | `ThrowIfCancellationRequested()` |
| ---------------------- | ------------------------- | -------------------------------- |
| Checks cancellation    | Yes                       | Yes                              |
| Throws exception       | No                        | Yes                              |
| Allows custom handling | Yes                       | Yes, through exception           |
| Common use             | Conditional cleanup/exit  | Task-based cancellation          |
| Returns                | `bool`                    | `void` or throws                 |


Example:

if (token.IsCancellationRequested)
{
    return;
}

versus:

token.ThrowIfCancellationRequested();
64. Important Comparison: CancellationToken vs CancellationTokenSource
	|                            | CancellationToken | CancellationTokenSource |
| ----------------------------- | ----------------- | ----------------------- |
| Represents cancellation state | Yes               | Manages it              |
| Requests cancellation         | No                | Yes                     |
| Passed to operations          | Yes               | Usually no              |
| `Cancel()`                    | No                | Yes                     |
| `CancelAfter()`               | No                | Yes                     |
| Linked cancellation           | No                | Yes                     |

65. Key Points to Remember
CancellationToken provides cooperative cancellation.
CancellationTokenSource requests cancellation.
cts.Cancel() does not kill a thread.
Operations must observe the token.
ThrowIfCancellationRequested() throws OperationCanceledException.
Pass the token through service and repository layers.
Pass the token to APIs that support cancellation.
Task.Delay supports cancellation through its token overload.
HTTP and EF Core operations commonly support cancellation tokens.
ASP.NET Core exposes request cancellation through RequestAborted.
BackgroundService receives a stopping token.
CreateLinkedTokenSource() combines cancellation sources.
Cancellation does not automatically cancel other Tasks after WhenAny.
Cancellation does not undo completed distributed work.
Cancellation is not a transaction or rollback mechanism.
Cancellation should not normally be treated as an unexpected error.
CPU-bound operations must explicitly check the token.
Cancellation is cooperative, not forceful.
Use cancellation to reduce wasted resources.
In distributed systems, combine cancellation with idempotency, retries, timeouts, and appropriate consistency mechanisms.
66. Final Mental Model
                 CancellationTokenSource
                          |
                          |
                      Cancel()
                          |
                          v
                 CancellationToken
                          |
          +---------------+---------------+
          |               |               |
          v               v               v
       HTTP Call       DB Query       Background Job
          |               |               |
          v               v               v
       observes        observes        observes
          |               |               |
          +---------------+---------------+
                          |
                          v
             Cooperative Cancellation
                          |
                          v
             OperationCanceledException

The key idea:

CancellationToken does not force work to stop; it provides a cooperative signal that well-designed operations observe so they can stop safely and release resources.

67. Product Company One-Line Summary

CancellationToken is .NET's cooperative cancellation mechanism that allows cancellation requests to propagate through asynchronous and long-running operations, helping applications stop unnecessary work gracefully without forcibly terminating threads.
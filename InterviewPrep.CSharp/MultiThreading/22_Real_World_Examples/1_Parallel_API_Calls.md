# Parallel API Calls

## 1. Definition

**Parallel API calls** means starting multiple independent API calls so they can execute concurrently instead of waiting for each call to complete sequentially.

Example:

```text
Sequential

API 1 ──────────→ 2 sec
                   ↓
API 2 ──────────→ 3 sec
                   ↓
API 3 ──────────→ 1 sec

Total ≈ 6 sec

With concurrent asynchronous calls:

API 1 ──────────→ 2 sec
API 2 ─────────────────→ 3 sec
API 3 ─────→ 1 sec

Total ≈ 3 sec

The key idea is:

Start independent I/O operations without unnecessarily waiting for each one before starting the next.

2. Why Parallel API Calls Matter

Modern applications frequently need data from multiple services.

Example:

Order API
   |
   ├── Customer Service
   ├── Inventory Service
   ├── Payment Service
   ├── Shipping Service
   └── Recommendation Service

If these calls are independent, waiting sequentially increases latency.

Concurrent asynchronous calls can reduce the overall response time.

3. Important Terminology

Do not confuse these concepts:
| Concept                 | Meaning                                                             |
| ----------------------- | ------------------------------------------------------------------- |
| Concurrency             | Multiple operations are in progress during overlapping periods      |
| Parallelism             | Multiple operations execute simultaneously                          |
| Async                   | Allows an operation to progress without blocking the calling thread |
| `Task`                  | Represents an asynchronous operation                                |
| `Task.WhenAll`          | Waits for multiple tasks to complete                                |
| `Task.Run`              | Schedules CPU-bound work, usually on ThreadPool                     |
| `Parallel.ForEachAsync` | Controlled concurrent processing of many items                      |


For API calls:

Prefer asynchronous concurrency, not Task.Run, for normal HTTP I/O.

4. Sequential API Calls

Example:

Customer customer = await GetCustomerAsync();
Inventory inventory = await GetInventoryAsync();
Payment payment = await GetPaymentAsync();

Execution:

GetCustomer
     ↓
GetInventory
     ↓
GetPayment

If:

Customer   = 2 sec
Inventory  = 3 sec
Payment    = 1 sec

Approximate latency:

2 + 3 + 1 = 6 seconds

This is appropriate when later calls depend on earlier results.

5. Concurrent API Calls with Task.WhenAll

If calls are independent:

Task<Customer> customerTask = GetCustomerAsync();
Task<Inventory> inventoryTask = GetInventoryAsync();
Task<Payment> paymentTask = GetPaymentAsync();

await Task.WhenAll(
    customerTask,
    inventoryTask,
    paymentTask);

Customer customer = await customerTask;
Inventory inventory = await inventoryTask;
Payment payment = await paymentTask;

Conceptually:

                 ┌── Customer API
                 │
Request ─────────┼── Inventory API
                 │
                 └── Payment API
                         ↓
                    WhenAll
                         ↓
                      Response

Approximate latency:

max(2, 3, 1) = 3 seconds

instead of:

2 + 3 + 1 = 6 seconds
6. Important: Creating the Task Starts the Operation

Consider:

Task<Customer> customerTask = GetCustomerAsync();
Task<Inventory> inventoryTask = GetInventoryAsync();
Task<Payment> paymentTask = GetPaymentAsync();

The calls are initiated before:

await Task.WhenAll(...);

This is different from:

Customer customer = await GetCustomerAsync();
Inventory inventory = await GetInventoryAsync();
Payment payment = await GetPaymentAsync();

The second version is sequential.

7. Best Basic Pattern

For independent API calls:

Task<Customer> customerTask = GetCustomerAsync();
Task<Inventory> inventoryTask = GetInventoryAsync();
Task<Payment> paymentTask = GetPaymentAsync();

await Task.WhenAll(
    customerTask,
    inventoryTask,
    paymentTask);

return new OrderSummary(
    await customerTask,
    await inventoryTask,
    await paymentTask);

This is one of the most important patterns for ASP.NET Core.

8. Why Task.Run Is Usually Wrong for API Calls

Avoid:

Task.Run(() => httpClient.GetAsync(url));

for normal HTTP I/O.

HttpClient already supports asynchronous I/O.

Prefer:

Task<HttpResponseMessage> task =
    httpClient.GetAsync(url);

Then:

await task;

The goal is to avoid blocking a ThreadPool thread while waiting for network I/O.

9. Parallel API Calls Are Usually Async Concurrency

The phrase "parallel API calls" is commonly used in application development, but technically:

HTTP API calls
      ↓
I/O-bound operations
      ↓
async concurrency

rather than:

CPU-bound work
      ↓
parallel CPU execution

For API calls, think:

Concurrent asynchronous I/O.

10. When Calls Can Be Parallelized

Calls can usually run concurrently when there is no dependency.

Example:

GetCustomer()
GetOrders()
GetRecommendations()

If each operation only needs:

CustomerId

they may be independent.

Therefore:

Task<Customer> customerTask = GetCustomerAsync(id);
Task<Order[]> ordersTask = GetOrdersAsync(id);
Task<Product[]> recommendationsTask =
    GetRecommendationsAsync(id);

await Task.WhenAll(
    customerTask,
    ordersTask,
    recommendationsTask);
11. When Calls Should NOT Be Parallelized

Suppose:

Create Customer
       ↓
Get Customer ID
       ↓
Create Order
       ↓
Create Payment

There are dependencies.

You cannot safely do:

Create Customer
Create Order
Create Payment

all at the same time if each operation requires the previous result.

Correct:

Customer customer = await CreateCustomerAsync();

Order order =
    await CreateOrderAsync(customer.Id);

Payment payment =
    await CreatePaymentAsync(order.Id);

Mental model:

Independent → concurrent. Dependent → sequential.

12. Mixed Dependencies

Real systems often have a mixture.

Example:

Get Customer
      |
      ├── Get Orders
      ├── Get Recommendations
      └── Get Loyalty Points

After obtaining the customer ID:

Customer customer =
    await GetCustomerAsync(customerId);

Task<Order[]> ordersTask =
    GetOrdersAsync(customer.Id);

Task<Product[]> recommendationsTask =
    GetRecommendationsAsync(customer.Id);

Task<LoyaltyPoints> pointsTask =
    GetLoyaltyPointsAsync(customer.Id);

await Task.WhenAll(
    ordersTask,
    recommendationsTask,
    pointsTask);

This creates a dependency graph rather than simply making everything sequential.

13. Task.WhenAll vs Task.WhenAny
Task.WhenAll

Use when:

I need all operations to complete.

await Task.WhenAll(
    task1,
    task2,
    task3);
Task.WhenAny

Use when:

I need the first completed operation.

Task completed =
    await Task.WhenAny(
        task1,
        task2,
        task3);

Example:

Service A ─────── 3 sec
Service B ──→ 1 sec
Service C ─────── 4 sec

WhenAny → Service B

Important:

WhenAny does not automatically cancel the remaining operations.

14. First Successful Response

WhenAny means:

first task to complete

not:

first successful task

Example:

Service A → fails in 500 ms
Service B → succeeds in 1 sec

WhenAny returns A because A completed first.

If you need the first successful result, you need additional logic to inspect failures and continue waiting.

15. Failure Handling with WhenAll

Suppose:

API 1 → Success
API 2 → Success
API 3 → Failure

Then:

await Task.WhenAll(
    api1,
    api2,
    api3);

completes faulted.

You should design explicitly whether:

Any failure → entire request fails

or:

Partial failure → return partial result
16. Partial Failure

Suppose an order dashboard needs:

Customer
Orders
Recommendations

Recommendations may be optional.

Business rule:

Customer failure → request fails
Orders failure → request fails
Recommendations failure → return empty recommendations

Do not treat all failures equally.

Possible design:

Task<Customer> customerTask =
    GetCustomerAsync();

Task<Order[]> ordersTask =
    GetOrdersAsync();

Task<Product[]> recommendationsTask =
    GetRecommendationsAsync();

await Task.WhenAll(
    customerTask,
    ordersTask,
    recommendationsTask);

For optional calls, you can instead handle their exceptions separately and return a fallback.

The exact strategy depends on the business requirement.

17. Timeout

External services can become slow.

Never assume:

API always responds quickly

Use appropriate timeout policies.

Conceptually:

Order order =
    await GetOrderAsync()
        .WaitAsync(TimeSpan.FromSeconds(3));

Timeout should be considered separately from cancellation.

Example:

Request timeout
       ↓
Cancel outstanding work where appropriate
       ↓
Return controlled error
18. Cancellation

ASP.NET Core requests provide cancellation through the request lifecycle.

Example:

public async Task<OrderSummary> GetSummaryAsync(
    int orderId,
    CancellationToken cancellationToken)
{
    Task<Order> orderTask =
        GetOrderAsync(orderId, cancellationToken);

    Task<Customer> customerTask =
        GetCustomerAsync(orderId, cancellationToken);

    await Task.WhenAll(
        orderTask,
        customerTask);

    return new OrderSummary(
        await orderTask,
        await customerTask);
}

Cancellation should propagate to downstream HTTP calls.

19. Don't Ignore Request Cancellation

Bad:

await httpClient.GetAsync(url);

when your application already has:

CancellationToken cancellationToken

Prefer:

await httpClient.GetAsync(
    url,
    cancellationToken);

This prevents unnecessary work when the client has already disconnected or the operation is no longer needed.

20. Unlimited Parallel API Calls

This is dangerous:

List<Task<Response>> tasks = new();

foreach (var item in items)
{
    tasks.Add(CallApiAsync(item));
}

await Task.WhenAll(tasks);

If there are:

100,000 items

you may create an enormous amount of concurrent work.

Possible consequences:

Too many outbound connections
Too much memory
Downstream overload
Rate-limit responses
ThreadPool pressure
Higher latency
Cascading failures

Therefore:

Concurrency should be controlled.

21. Controlled Concurrency

For many independent API calls, use a concurrency limit.

Example:

SemaphoreSlim semaphore =
    new(10);

async Task<Response> CallWithLimitAsync(
    Item item,
    CancellationToken cancellationToken)
{
    await semaphore.WaitAsync(cancellationToken);

    try
    {
        return await CallApiAsync(
            item,
            cancellationToken);
    }
    finally
    {
        semaphore.Release();
    }
}

Now at most:

10

operations are in progress at once.

22. Parallel.ForEachAsync

For large collections, Parallel.ForEachAsync can provide controlled concurrency.

Example:

ParallelOptions options = new()
{
    MaxDegreeOfParallelism = 10,
    CancellationToken = cancellationToken
};

await Parallel.ForEachAsync(
    items,
    options,
    async (item, token) =>
    {
        await CallApiAsync(item, token);
    });

Important:

Parallel.ForEachAsync is not automatically better than Task.WhenAll.

Choose based on workload and required control.

23. Task.WhenAll vs Parallel.ForEachAsync

| Scenario                                | Preferred                                  |
| --------------------------------------- | ------------------------------------------ |
| 3 independent API calls                 | `Task.WhenAll`                             |
| 5 independent service calls             | `Task.WhenAll`                             |
| 100,000 API requests                    | Controlled concurrency                     |
| Large collection with concurrency limit | `Parallel.ForEachAsync` or `SemaphoreSlim` |
| Need all results                        | `Task.WhenAll`                             |
| Need streaming/controlled processing    | `Parallel.ForEachAsync`                    |
| CPU-bound loop                          | `Parallel.For` / appropriate parallelism   |
| Normal HTTP I/O                         | `async/await`                              |


24. Concurrency Limit vs Rate Limit

These are different.

Concurrency limit

Controls:

How many operations are in progress at once?

Example:

Maximum 10 simultaneous API calls
Rate limit

Controls:

How many requests are allowed over a time period?

Example:

100 requests per second

You may need both.

Example:

Concurrency = 10
Rate = 100 requests/second
25. Respect Downstream API Limits

Suppose an external API allows:

100 requests/second

Your application should not blindly send:

10,000 requests/second

Use:

Concurrency control
Rate limiting
Retry policy
Backoff
Timeout
Circuit breaker

depending on the architecture.

26. Retry and Parallel API Calls

Retries can amplify load.

Example:

100 API calls
   ↓
20 fail
   ↓
20 retries

If retries happen repeatedly:

100
 ↓
20 retries
 ↓
10 retries
 ↓
...

This can become a retry storm.

Use:

Limited retries
Exponential backoff
Jitter
Timeout
Circuit breaker
Idempotency where required

Only retry failures that are actually retryable.

27. Idempotency

Parallel calls and retries can create duplicate business effects.

Example:

POST /payment

If the request times out and is retried:

Payment #1 created
Payment #2 created

Use an idempotency mechanism where appropriate:

Idempotency-Key
+
Unique constraint
+
Atomic business operation

This is especially important for:

Payments
Orders
Reservations
Inventory
Message processing
28. HttpClient Best Practice

Do not create a new HttpClient for every request:

using HttpClient client = new();

inside frequently executed application code.

In ASP.NET Core, prefer:

IHttpClientFactory

or properly managed long-lived HttpClient instances.

Benefits include:

Connection reuse
Handler lifetime management
DNS refresh behavior
Centralized configuration
Resilience policies
Observability
29. Typed/Named HTTP Clients

For enterprise applications, typed clients can separate external-service communication.

Example:

public interface IInventoryClient
{
    Task<Inventory> GetInventoryAsync(
        int productId,
        CancellationToken cancellationToken);
}

Implementation:

public sealed class InventoryClient
    : IInventoryClient
{
    private readonly HttpClient _httpClient;

    public InventoryClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Inventory> GetInventoryAsync(
        int productId,
        CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<Inventory>(
            $"/api/inventory/{productId}",
            cancellationToken)
            ?? throw new InvalidOperationException(
                "Inventory response was empty.");
    }
}

Business services then depend on:

IInventoryClient

rather than constructing HTTP infrastructure themselves.

30. Dependency Injection

Enterprise code should generally use dependency injection.

Example:

public sealed class OrderSummaryService
{
    private readonly ICustomerClient _customerClient;
    private readonly IInventoryClient _inventoryClient;
    private readonly IPaymentClient _paymentClient;

    public OrderSummaryService(
        ICustomerClient customerClient,
        IInventoryClient inventoryClient,
        IPaymentClient paymentClient)
    {
        _customerClient = customerClient;
        _inventoryClient = inventoryClient;
        _paymentClient = paymentClient;
    }
}

Then:

Task<Customer> customerTask =
    _customerClient.GetCustomerAsync(
        customerId,
        cancellationToken);

Task<Inventory> inventoryTask =
    _inventoryClient.GetInventoryAsync(
        productId,
        cancellationToken);

Task<Payment> paymentTask =
    _paymentClient.GetPaymentAsync(
        orderId,
        cancellationToken);

await Task.WhenAll(
    customerTask,
    inventoryTask,
    paymentTask);
31. Don't Hold Locks During API Calls

Avoid:

lock (_lock)
{
    await CallExternalApiAsync();
}

This is invalid because C# lock cannot contain an await.

Also avoid holding synchronous locks around slow external operations.

Why?

Lock acquired
     ↓
Network call
     ↓
5 seconds
     ↓
Lock released

Other operations wait unnecessarily.

Better:

Keep critical sections small
+
Avoid external calls under locks
+
Use async-friendly coordination when needed
32. Database Calls and API Calls

Parallelizing everything can still overload resources.

Example:

20 API calls
+
20 DB queries
+
20 cache calls

can create significant resource pressure.

Consider:

DB connection pool
HTTP connection limits
External API limits
CPU
Memory
ThreadPool

Concurrency should be controlled at the resource boundary.

33. ASP.NET Core Example

Suppose an endpoint needs:

Customer Service
Order Service
Recommendation Service

Controller:

[HttpGet("{customerId}")]
public async Task<ActionResult<CustomerDashboard>> GetDashboard(
    int customerId,
    CancellationToken cancellationToken)
{
    CustomerDashboard dashboard =
        await _service.GetDashboardAsync(
            customerId,
            cancellationToken);

    return Ok(dashboard);
}

Service:

public async Task<CustomerDashboard> GetDashboardAsync(
    int customerId,
    CancellationToken cancellationToken)
{
    Task<Customer> customerTask =
        _customerClient.GetAsync(
            customerId,
            cancellationToken);

    Task<Order[]> ordersTask =
        _orderClient.GetOrdersAsync(
            customerId,
            cancellationToken);

    Task<Product[]> recommendationsTask =
        _recommendationClient.GetAsync(
            customerId,
            cancellationToken);

    await Task.WhenAll(
        customerTask,
        ordersTask,
        recommendationsTask);

    return new CustomerDashboard
    {
        Customer = await customerTask,
        Orders = await ordersTask,
        Recommendations =
            await recommendationsTask
    };
}

This is a clean enterprise pattern for independent I/O operations.

34. Avoid .Result and .Wait()

Bad:

Customer customer =
    customerTask.Result;

or:

customerTask.Wait();

These can:

Block threads
Increase latency
Cause ThreadPool starvation
Create deadlock risks in some environments

Prefer:

Customer customer =
    await customerTask;
35. Exception Handling

Do not hide failures:

try
{
    await Task.WhenAll(
        task1,
        task2,
        task3);
}
catch
{
    return null;
}

This loses useful information.

Instead:

Log meaningful context
Classify the failure
Apply business rules
Retry only appropriate transient failures
Return an appropriate response
36. Observability

When making parallel calls, track:

Total request latency
Individual API latency
p50
p95
p99
Error rate
Timeout rate
Retry count
Concurrency
Downstream status codes
Connection pool usage

Example:

Dashboard API = 850 ms

Customer = 150 ms
Orders = 820 ms
Recommendations = 200 ms

The slowest dependency dominates the critical path.

37. Critical Path

For independent operations:

Total latency ≈ max(individual latencies)

For sequential operations:

Total latency ≈ sum(individual latencies)

Example:

API A = 100 ms
API B = 500 ms
API C = 200 ms

Sequential:

100 + 500 + 200 = 800 ms

Concurrent:

max(100, 500, 200) = 500 ms

Real systems include:

Network overhead
Serialization
Connection acquisition
CPU processing
Retries
Queuing

so this is a conceptual model, not an exact latency guarantee.

38. Cascading Failure

Suppose:

Frontend
   ↓
Order API
   ↓
10 downstream APIs

If the Order API makes unlimited concurrent calls and a downstream service slows down:

Slow downstream
      ↓
More requests remain in progress
      ↓
More resources consumed
      ↓
Higher latency
      ↓
Timeouts
      ↓
Retries
      ↓
More load

This can become a cascading failure.

Therefore:

Timeout
+
Concurrency limit
+
Rate limit
+
Retry/backoff
+
Circuit breaker

may be required.

39. Fan-Out / Fan-In

A common architecture pattern is:

                 ┌── Service A
                 │
Request ─────────┼── Service B
                 │
                 └── Service C
                         ↓
                      Fan-In
                         ↓
                      Response

This is called:

Fan-out / fan-in

Fan-out:

One request → multiple downstream operations

Fan-in:

Multiple results → one combined response

Task.WhenAll is commonly used to implement the fan-out/fan-in pattern.

40. API Gateway / Aggregator Scenario

An API Gateway or Backend-for-Frontend may aggregate:

Customer API
Order API
Payment API
Inventory API

Instead of the frontend making four requests:

Frontend → Gateway
             ├── Customer
             ├── Orders
             ├── Payment
             └── Inventory

The gateway can call independent services concurrently.

Benefits:

Reduced frontend round trips
Centralized aggregation
Potentially lower latency
Simpler client

But the gateway becomes sensitive to downstream latency and failures.

41. Dependency Graph

A good system design question is:

"Which operations actually depend on each other?"

Example:

Get Order
    |
    ├── Get Customer
    │
    ├── Get Inventory
    │
    └── Get Shipping

These can potentially run concurrently after the required input is available.

Another example:

Create Order
     ↓
Payment
     ↓
Shipment

These are dependent.

Do not parallelize them just because parallelism appears faster.

42. Performance Considerations

Parallel API calls can reduce latency, but they can increase:

Concurrency
Memory usage
Network traffic
Connection usage
Downstream load
Failure surface

Therefore:

Faster for one request does not necessarily mean better for the whole system.

You must consider system-wide throughput and downstream capacity.

43. Bulk API Calls

Suppose you need data for:

10,000 customers

Do not blindly do:

Task.WhenAll(
    customers.Select(
        customer => GetCustomerAsync(customer.Id)));

This could create 10,000 concurrent operations.

Better options:

Batch API
Controlled concurrency
Pagination
Queue
Background processing
Rate limiting

If the external API supports:

POST /customers/batch

then batching may be significantly better than thousands of individual calls.

44. Parallel Calls vs Batch API

| Approach                  | Advantage                         | Risk                          |
| ------------------------- | --------------------------------- | ----------------------------- |
| Individual parallel calls | Simple, lower per-call dependency | Can create huge concurrency   |
| Batch API                 | Fewer network round trips         | Larger payload / batch limits |
| Sequential calls          | Simple                            | High latency                  |
| Queue                     | Good for background workloads     | Not immediate response        |
| Controlled concurrency    | Protects resources                | Adds coordination             |


45. When NOT to Use Parallel API Calls

Avoid or reconsider parallel calls when:

Calls depend on each other
Downstream service has strict limits
Database connection pool is small
Operation has ordering requirements
Side effects must happen sequentially
The calls are extremely cheap
Concurrency would overload the system
Business consistency requires a transaction/workflow
46. Production Checklist

Before parallelizing API calls, ask:

1. Are the calls independent?
2. Are they I/O-bound?
3. Can they safely execute concurrently?
4. What is the downstream concurrency limit?
5. What is the API rate limit?
6. What timeout should be used?
7. How is cancellation propagated?
8. What happens if one call fails?
9. Is partial failure acceptable?
10. Are retries required?
11. Are retries safe?
12. Does the operation need idempotency?
13. Can the downstream service handle the load?
14. Are connection pools sufficient?
15. Is concurrency bounded?
16. Do we need batching?
17. Do we need a queue instead?
18. How are latency and errors monitored?
19. What happens during downstream degradation?
20. Could this cause cascading failure?
47. Interview Questions
Q1. How do you make multiple API calls concurrently in C#?

Use asynchronous methods to start the independent operations and then await them together with Task.WhenAll.

Q2. Why is this faster?

Because independent I/O operations can overlap rather than waiting for each previous operation to complete.

Q3. Should you use Task.Run for HTTP calls?

Generally no. HTTP I/O is already asynchronous. Task.Run is primarily useful for moving CPU-bound work away from the current context.

Q4. What is the difference between Task.WhenAll and Parallel.ForEachAsync?

Task.WhenAll is excellent for a known set of independent tasks, while Parallel.ForEachAsync is useful for processing many items with controlled concurrency.

Q5. What happens if one task fails in Task.WhenAll?

The combined task completes faulted. The application should apply an appropriate failure-handling strategy based on the business requirement.

Q6. Does Task.WhenAll limit concurrency?

No.

If you create 10,000 tasks and pass them to WhenAll, they may all be in flight.

Use controlled concurrency when necessary.

Q7. How do you limit concurrent API calls?

Common approaches include:

SemaphoreSlim
Parallel.ForEachAsync + MaxDegreeOfParallelism
Rate/concurrency limiting
Queue/Channel

Choose based on workload.

Q8. What is fan-out/fan-in?

Fan-out sends work to multiple independent operations, and fan-in combines their results into one response.

Q9. What is the main risk of excessive parallel API calls?

Overloading your application or downstream services, causing latency, throttling, failures, and potentially cascading failures.

Q10. How do retries affect parallel API calls?

Retries can multiply downstream traffic and create retry storms. Use bounded retries, backoff, jitter, timeouts, and idempotency where appropriate.

Q11. How should cancellation be handled?

Accept a CancellationToken, propagate it to downstream async operations, and stop unnecessary work when cancellation is requested.

Q12. How do you handle partial failures?

Define business criticality for each dependency. Some failures may fail the entire request, while optional dependencies may return a fallback.

Q13. What if the external API has a rate limit?

Respect the provider's limits using rate limiting, bounded concurrency, backoff, and batching where appropriate.

Q14. Why shouldn't you use .Result or .Wait()?

They block threads and can contribute to ThreadPool starvation and latency; use await instead.

Q15. How would you design an API aggregator?

Identify independent downstream calls, fan them out asynchronously, apply timeouts/cancellation/concurrency controls, handle partial failures, combine results, and instrument each dependency.

48. Key Points to Remember
Parallel API calls usually mean concurrent asynchronous I/O.

Independent calls → start concurrently.

Dependent calls → keep the dependency order.

Task.WhenAll is the primary pattern for a small known set of independent calls.

Task.WhenAny returns the first completed task, not necessarily the first successful task.

Task.WhenAll does NOT limit concurrency.

Do not use Task.Run just to make HTTP calls parallel.

Avoid .Result and .Wait() in async application code.

Propagate CancellationToken.

Use timeouts for external dependencies.

Do not create unlimited concurrent API calls.

Use SemaphoreSlim or Parallel.ForEachAsync for controlled concurrency.

Concurrency limit != rate limit.

Respect downstream API and connection-pool limits.

Retries can create retry storms.

Use exponential backoff and jitter where appropriate.

Non-idempotent operations require careful retry handling.

Use idempotency for operations such as payments and orders when retries can duplicate effects.

Use IHttpClientFactory/managed HttpClient rather than creating HttpClient per request.

Do not hold locks while performing external I/O.

Monitor p95/p99 latency, failures, timeouts, retries, and downstream latency.

The slowest dependency often determines the fan-out request's critical-path latency.

Consider batching for large numbers of API calls.

Consider queues/background processing for work that does not need an immediate response.

Parallelism can improve latency but can reduce overall system stability if concurrency is uncontrolled.
49. Mental Model

Think about parallel API calls as:

                 REQUEST
                    |
                    ↓
             Identify dependencies
                    |
          ┌─────────┴─────────┐
          ↓                   ↓
     Independent          Dependent
          |                   |
          ↓                   ↓
    Start concurrently    Run sequentially
          |
          ↓
    Apply concurrency
       controls
          |
          ↓
   Timeout + cancellation
          |
          ↓
   Handle failures/retries
          |
          ↓
      Fan-in results
          |
          ↓
       RESPONSE

The core production principle is:

Parallelize independent I/O operations to reduce latency, but always control concurrency and protect downstream systems from overload.

One-Line Definition

Parallel API calls are the concurrent execution of independent asynchronous API operations, typically using Task.WhenAll, 
to reduce latency while requiring proper control of concurrency, timeouts, cancellation, retries, failures, and downstream capacity.
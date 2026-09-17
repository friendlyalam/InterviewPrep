# ASP.NET Core Concurrency

## 1. Definition

ASP.NET Core is designed to handle many HTTP requests concurrently.

A web application may have:

- hundreds of simultaneous users
- multiple requests for the same resource
- database operations
- HTTP calls to other services
- background processing
- shared application state

Concurrency becomes important because multiple requests can execute at overlapping times and may access the same resources.

> **ASP.NET Core concurrency means safely and efficiently handling multiple requests and asynchronous operations at the same time without unnecessary blocking or unsafe shared state.**

---

# 2. Why Concurrency Matters in ASP.NET Core

Consider:

```text
User A ──────── Request ────────┐
                                │
User B ──────── Request ────────┼──> ASP.NET Core
                                │
User C ──────── Request ────────┘

ASP.NET Core does not normally process all requests one-by-one.

Multiple requests can be in progress simultaneously.

For example:

Request A → Database
Request B → HTTP API
Request C → Cache
Request D → CPU processing

While A waits for the database, the application should avoid unnecessarily blocking a worker thread.

3. ASP.NET Core Request Processing

A simplified flow:

HTTP Request
     ↓
Kestrel
     ↓
Middleware Pipeline
     ↓
Routing
     ↓
Controller / Minimal API
     ↓
Service
     ↓
Repository / External API / Database
     ↓
Response

Multiple requests can pass through this pipeline concurrently.

4. ASP.NET Core Does Not Create One Thread Per Request

A common misconception is:

"Every HTTP request gets its own dedicated thread."

That is not the right mental model.

ASP.NET Core uses asynchronous programming and ThreadPool infrastructure.

For an I/O-bound operation:

Request
   ↓
Start database operation
   ↓
await
   ↓
Thread is not unnecessarily blocked
   ↓
Database completes
   ↓
Continuation resumes
   ↓
Response

The same ThreadPool can therefore serve many concurrent requests.

5. async/await in ASP.NET Core

Prefer asynchronous APIs:

public async Task<IActionResult> GetOrdersAsync()
{
    List<Order> orders =
        await _orderService.GetOrdersAsync();

    return Ok(orders);
}

Instead of:

public IActionResult GetOrders()
{
    List<Order> orders =
        _orderService.GetOrdersAsync()
            .Result;

    return Ok(orders);
}

The second approach blocks the request thread while waiting.

6. Why Blocking Is Dangerous

Consider:

var result = SomeAsyncOperation().Result;

or:

SomeAsyncOperation().Wait();

The ThreadPool thread is blocked.

Under high traffic:

1000 requests
      ↓
Many blocked worker threads
      ↓
ThreadPool pressure
      ↓
Requests wait for available threads
      ↓
Latency increases
      ↓
Throughput decreases

This can contribute to ThreadPool starvation.

7. Async All the Way

Prefer:

Controller
    ↓
Service
    ↓
Repository
    ↓
Database

with asynchronous methods throughout:

Controller
    ↓ await
Service
    ↓ await
Repository
    ↓ await
Database

Example:

public async Task<Order?> GetOrderAsync(
    int orderId)
{
    return await _repository
        .GetOrderAsync(orderId);
}

Avoid converting asynchronous operations into synchronous blocking operations.

8. CPU-Bound vs I/O-Bound Operations

This distinction is critical.

I/O-bound

Examples:

SQL query
HTTP request
Redis operation
file I/O
Azure Storage
message broker operation

Use asynchronous APIs:

await repository.GetAsync();
CPU-bound

Examples:

image processing
encryption
large calculations
CPU-heavy data transformation

Depending on the workload, CPU-bound work may use controlled parallelism or background processing.

9. Do Not Use Task.Run for Normal ASP.NET Core I/O

Avoid:

public async Task<IActionResult> GetAsync()
{
    var result = await Task.Run(
        () => _repository.GetAsync());

    return Ok(result);
}

This does not make normal I/O better.

Prefer:

public async Task<IActionResult> GetAsync()
{
    var result =
        await _repository.GetAsync();

    return Ok(result);
}

Task.Run is primarily useful for explicitly offloading CPU-bound work when appropriate.

10. Concurrent Independent Operations

Suppose an API needs:

Product
Inventory
Reviews

and they are independent.

Sequential:

var product =
    await _productService.GetAsync();

var inventory =
    await _inventoryService.GetAsync();

var reviews =
    await _reviewService.GetAsync();

This waits for each operation before starting the next.

Better:

Task<Product> productTask =
    _productService.GetAsync();

Task<Inventory> inventoryTask =
    _inventoryService.GetAsync();

Task<List<Review>> reviewsTask =
    _reviewService.GetAsync();

await Task.WhenAll(
    productTask,
    inventoryTask,
    reviewsTask);

Product product = await productTask;
Inventory inventory = await inventoryTask;
List<Review> reviews = await reviewsTask;

Conceptually:

Sequential:

Product ────────>
                Inventory ────────>
                                  Reviews ─────>

Concurrent:

Product ────────>
Inventory ─────────────>
Reviews ─────────>

Use concurrency only when the operations are independent.

11. Do Not Create Unlimited Concurrency

This is dangerous:

var tasks = orders
    .Select(order => ProcessOrderAsync(order));

await Task.WhenAll(tasks);

If there are:

1,000,000 orders

you could create an enormous number of concurrent operations.

Potential problems:

database overload
connection pool exhaustion
external API throttling
memory pressure
ThreadPool pressure
increased latency
rate-limit errors
12. Limit Concurrency

Modern .NET provides:

Parallel.ForEachAsync(...)

for suitable workloads.

Example:

await Parallel.ForEachAsync(
    orders,
    new ParallelOptions
    {
        MaxDegreeOfParallelism = 10
    },
    async (order, cancellationToken) =>
    {
        await ProcessOrderAsync(
            order,
            cancellationToken);
    });

Conceptually:

1000 orders
     ↓
Maximum 10 operations concurrently
     ↓
Controlled load

Another option is:

SemaphoreSlim

for custom concurrency limits.

13. SemaphoreSlim for Request-Level Concurrency

Example:

private readonly SemaphoreSlim _semaphore =
    new(10, 10);

Use:

await _semaphore.WaitAsync(
    cancellationToken);

try
{
    await ProcessAsync(
        cancellationToken);
}
finally
{
    _semaphore.Release();
}

This limits the number of concurrent operations.

Important

SemaphoreSlim is:

process-local
not distributed
useful for concurrency limiting

It does not coordinate across multiple application instances.

14. ASP.NET Core and Shared State

This is one of the most important concurrency topics.

Suppose:

public class OrderService
{
    private int _counter;
}

If the service is registered as Singleton:

services.AddSingleton<OrderService>();

multiple requests may access the same instance concurrently.

Therefore:

Singleton
    +
Mutable state
    +
Concurrent requests
    =
Potential race condition
15. Singleton Services Must Be Thread-Safe

Example of dangerous code:

public class CounterService
{
    private int _count;

    public void Increment()
    {
        _count++;
    }
}

_count++ is not an atomic operation.

Multiple requests can execute it concurrently.

Use:

Interlocked.Increment(
    ref _count);

when the requirement is simply an atomic counter.

16. Scoped Services

ASP.NET Core commonly uses:

services.AddScoped<IOrderService, OrderService>();

A scoped service generally has one instance per request scope.

This reduces accidental cross-request shared state.

However:

Scoped does not automatically make code thread-safe.

If the same scoped object is deliberately accessed concurrently from multiple tasks, it can still have concurrency issues.

17. Transient Services

Transient:

services.AddTransient<IEmailService, EmailService>();

A new instance is normally created whenever requested from DI.

This can reduce shared instance state, but it does not automatically solve all concurrency problems.

Thread safety still depends on:

shared static state
shared database data
external resources
cached objects
referenced mutable objects
18. Static Mutable State

Avoid request-specific mutable state such as:

private static List<Order> _orders =
    new();

Multiple requests can access the same collection concurrently.

Potential result:

race conditions
inconsistent state
collection exceptions
memory growth
difficult debugging

If shared state is genuinely required, choose an appropriate thread-safe design.

19. Concurrent Collections

For process-local concurrent state:

ConcurrentDictionary<TKey, TValue>

can be useful.

Example:

private readonly ConcurrentDictionary<int, Order>
    _orders = new();

But remember:

A thread-safe collection does not automatically make a multi-step business operation thread-safe.

For example:

if (!_orders.ContainsKey(id))
{
    _orders[id] = order;
}

This check-then-act workflow is problematic.

Prefer an atomic collection operation such as:

_orders.TryAdd(id, order);

when that matches the requirement.

20. Async Does Not Eliminate Race Conditions

This is a common interview trap.

Code such as:

await Task.Delay(100);

does not automatically make code thread-safe.

Consider:

if (stock > 0)
{
    await ProcessPaymentAsync();

    stock--;
}

Two concurrent requests could both observe:

stock = 1

before either decrements it.

Result:

Request A → sees 1
Request B → sees 1

Both continue

Request A → decrement
Request B → decrement

Overselling

Async programming and concurrency control solve different problems.

21. Database Concurrency

In real applications, concurrency often crosses application boundaries.

Suppose two application servers process the same product:

Server A ──┐
           ├── Database
Server B ──┘

A local:

lock

on Server A does not protect Server B.

Therefore database concurrency controls are often required.

Examples:

optimistic concurrency
row version
transactions
atomic UPDATE
unique constraints
pessimistic locking
idempotency
22. Optimistic Concurrency

Optimistic concurrency assumes conflicts are relatively uncommon.

Example:

Product
Id = 101
Stock = 10
Version = 5

Request A reads:

Stock = 10
Version = 5

Request B reads:

Stock = 10
Version = 5

A updates successfully:

Version = 6

B attempts:

UPDATE ...
WHERE Id = 101
AND Version = 5

No row is updated.

The application detects the conflict.

23. Pessimistic Concurrency

Pessimistic concurrency locks the resource while it is being modified.

Conceptually:

Request A
    ↓
Acquire DB lock
    ↓
Modify
    ↓
Commit
    ↓
Release

Request B
    ↓
Wait

Useful when conflicts are frequent or strict serialization is required.

However, locks can reduce concurrency and increase deadlock risk.

24. Idempotency

Concurrency becomes especially important with retries.

Suppose a payment request is sent twice:

Request 1 → Payment
Request 2 → Payment retry

Without idempotency:

Customer charged twice

Use an idempotency key:

Idempotency-Key = ABC123

The server ensures the same logical operation is processed only once.

This is critical for:

payments
orders
bookings
inventory
message processing
25. ASP.NET Core Request Cancellation

ASP.NET Core exposes request cancellation through:

HttpContext.RequestAborted

Example:

public async Task<IActionResult> GetAsync(
    CancellationToken cancellationToken)
{
    var result =
        await _service.GetAsync(
            cancellationToken);

    return Ok(result);
}

ASP.NET Core can bind the request cancellation token automatically.

The cancellation should be propagated:

HTTP Request
     ↓
Controller
     ↓
Service
     ↓
Repository
     ↓
Database
26. Why Cancellation Matters

Suppose a user closes the browser while an expensive query is running.

Without cancellation:

Client disconnected
      ↓
Server continues expensive work
      ↓
Database continues processing
      ↓
Resources wasted

With cancellation:

Client disconnects
      ↓
Cancellation requested
      ↓
Operation stops when supported
      ↓
Resources released
27. Timeouts vs Cancellation

They are related but different.

Timeout

The operation took too long.

await operation.WaitAsync(
    TimeSpan.FromSeconds(5));
Cancellation

The operation should stop because the caller no longer wants it or application shutdown occurred.

Example:

await operation.WaitAsync(
    cancellationToken);

Production applications often use both.

28. Thread Safety of HttpContext

HttpContext is request-specific.

Do not intentionally access the same HttpContext concurrently from multiple threads/tasks unless the specific API usage is safe.

Avoid capturing request-specific mutable state into long-running background work.

For example, don't do:

_ = Task.Run(async () =>
{
    await DoSomethingAsync(
        HttpContext.User);
});

and assume the request context remains valid after the request ends.

29. Background Work

Do not use an HTTP request as a container for long-running background work.

Bad pattern:

HTTP Request
     ↓
Start huge processing
     ↓
Keep request open for minutes

Better:

HTTP Request
     ↓
Validate
     ↓
Queue message
     ↓
Return response
     ↓
Background Worker
     ↓
Process

Examples:

BackgroundService
IHostedService
Channel<T>
Azure Service Bus
RabbitMQ
Kafka

For distributed production systems, a durable message broker is usually preferred when work must survive application restarts.

30. Producer-Consumer in ASP.NET Core

A common architecture:

HTTP Requests
     |
     v
Producer
     |
     v
Queue / Channel
     |
     v
Consumers
     |
     v
Database / External Service

Benefits:

controlled concurrency
buffering
backpressure
decoupling
background processing

For process-local work:

Channel<T>

is a strong modern .NET option.

For durable distributed work:

Azure Service Bus
RabbitMQ
Kafka

may be more appropriate depending on requirements.

31. ThreadPool Starvation

ThreadPool starvation happens when ThreadPool workers are unavailable because they are blocked or occupied for too long.

Common causes:

.Result
.Wait()
Thread.Sleep()

blocking I/O, excessive CPU work, or uncontrolled parallelism.

Symptoms:

increasing latency
request timeouts
low throughput
queued ThreadPool work
slow recovery under load
32. Bad Example
public IActionResult Get()
{
    var result =
        _service.GetAsync().Result;

    return Ok(result);
}

Problem:

Request thread
     ↓
Blocks
     ↓
Waits for async operation
     ↓
ThreadPool pressure

Better:

public async Task<IActionResult> GetAsync()
{
    var result =
        await _service.GetAsync();

    return Ok(result);
}
33. Lock Cannot Contain await

This is invalid:

lock (_lock)
{
    await DoSomethingAsync();
}

C# does not allow await inside a lock body.

If asynchronous mutual exclusion is required, consider:

SemaphoreSlim

Example:

await _semaphore.WaitAsync(
    cancellationToken);

try
{
    await DoSomethingAsync(
        cancellationToken);
}
finally
{
    _semaphore.Release();
}
34. Do Not Hold Locks During External Calls

Avoid:

lock (_lock)
{
    awaitDatabaseOperation();
}

Conceptually, don't hold synchronization around:

database calls
HTTP calls
message broker calls
long-running operations

External operations can take unpredictable amounts of time.

Prefer designs that minimize the critical section.

35. Caching and Concurrency

Caching introduces concurrency considerations.

Suppose 100 requests miss the cache simultaneously:

100 requests
     ↓
Cache miss
     ↓
100 database calls

This is sometimes called a cache stampede.

Possible solutions include:

request coalescing
SemaphoreSlim
distributed locking
cache-aside strategies
randomized expiration
distributed cache
background refresh

For distributed systems, local synchronization alone may not be sufficient.

36. Lazy Initialization and Concurrency

If expensive initialization should happen once, use appropriate thread-safe mechanisms.

Examples:

Lazy<T>

or:

ConcurrentDictionary<TKey, TValue>

with the correct atomic operation.

Avoid manually implementing:

if (_instance == null)
{
    _instance = new Something();
}

without understanding concurrent access.

37. Database Connection Pooling

High concurrency does not mean:

1 request = 1 unlimited database connection

Database providers normally use connection pooling.

But if the application generates excessive concurrent database operations:

Too many requests
      ↓
Too many DB operations
      ↓
Connection pool exhaustion
      ↓
Requests wait
      ↓
Latency increases

Therefore concurrency should be controlled at the application and database levels.

38. External API Concurrency

Suppose an external service allows:

100 requests/second

but your application sends:

10,000 concurrent requests

You may get:

HTTP 429
throttling
timeouts
retries
cascading failures

Use:

concurrency limits
rate limiting
timeouts
retries with backoff
circuit breaker
caching
queues
39. Concurrency + Retry Danger

A common production failure:

Request
   ↓
External API
   ↓
Timeout
   ↓
Retry
   ↓
Timeout
   ↓
Retry

With thousands of requests, retries can multiply traffic.

This can create a retry storm.

Therefore:

Retry
+
Timeout
+
Circuit Breaker
+
Backoff
+
Concurrency Limit

should be designed together.

40. Circuit Breaker

A circuit breaker protects your service when a downstream dependency is failing.

Conceptually:

Healthy
   ↓
Requests allowed
   ↓
Failures increase
   ↓
Circuit opens
   ↓
Requests fail fast
   ↓
Wait
   ↓
Half-open
   ↓
Test request
   ↓
Healthy → Closed
Failed  → Open

This reduces pressure on failing dependencies.

41. ASP.NET Core Rate Limiting

ASP.NET Core supports rate-limiting middleware.

Rate limiting can protect:

APIs
login endpoints
expensive operations
downstream dependencies

Important distinction:

Concurrency limit
    ↓
How many operations execute at once

Rate limit
    ↓
How many operations are allowed over time

They solve different problems.

42. Concurrency vs Rate Limiting

Example:

Concurrency limit = 20

means:

Maximum 20 active operations

Rate limit:

100 requests/second

means:

Maximum 100 requests during a time window

A system may need both.

43. Thread Safety vs Process Safety

A lock protects code within the same process:

Server A
   |
   +-- lock

It does not automatically coordinate:

Server A ──┐
           ├── Database
Server B ──┘

For distributed systems use appropriate mechanisms such as:

database transactions
optimistic concurrency
distributed locks
unique constraints
idempotency
distributed coordination
44. ASP.NET Core and Distributed Systems

Consider:

                Load Balancer
                 /        \
                /          \
        Server A          Server B
           |                  |
           +--------+---------+
                    |
                 Database

A local:

lock

on Server A cannot protect the same resource on Server B.

This is one of the most important differences between:

In-process concurrency

and:

Distributed concurrency
45. EF Core and Concurrency

EF Core DbContext is generally not thread-safe.

Do not do:

Task.WhenAll(
    context.Orders.ToListAsync(),
    context.Products.ToListAsync());

using the same DbContext instance concurrently.

Instead, use:

separate contexts
sequential operations
properly designed data access

Also remember that a scoped DbContext does not mean it can safely execute multiple operations concurrently.

46. Parallel Queries

If independent database operations genuinely need to run concurrently, use separate DbContext instances when supported by the architecture.

Conceptually:

Context A → Orders
Context B → Products
Context C → Customers

But don't assume parallel database queries are automatically faster.

Consider:

DB connection pool
database CPU
query cost
locking
network latency
transaction requirements

Always measure.

47. ASP.NET Core and Immutability

Immutable objects are useful for safely sharing data.

For example:

public sealed record ProductSnapshot(
    int Id,
    string Name,
    decimal Price);

Once created, the object's own properties cannot be reassigned.

Immutable data reduces the need for synchronization when shared across concurrent operations.

Remember:

record does not automatically guarantee deep immutability.

If a record contains a mutable reference-type property, that referenced object can still change.

48. Request Data Should Usually Be Local

Prefer:

public async Task<IActionResult> GetAsync()
{
    var orders =
        await _service.GetOrdersAsync();

    var result =
        orders.Select(...);

    return Ok(result);
}

over storing request-specific mutable data in:

static fields
singleton state
global collections

Local variables naturally have request-local ownership.

49. Thread-Local State vs Shared State

Concurrency becomes easier when state is isolated.

Prefer:

Request A → local state
Request B → local state
Request C → local state

over:

Request A ─┐
Request B ─┼── Shared mutable state
Request C ─┘

A useful design principle is:

Reduce shared mutable state before adding synchronization.

50. Concurrency and Caching Architecture

For a product system:

Client
  ↓
Load Balancer
  ↓
ASP.NET Core instances
  ↓
Distributed Cache
  ↓
Database

If cache state must be shared between instances, use a distributed cache such as Redis rather than an in-memory dictionary.

In-memory cache:

Server A → Cache A
Server B → Cache B

Distributed cache:

Server A ──┐
Server B ──┼── Redis
Server C ──┘
51. Concurrency and Session State

Avoid storing important mutable session state only in process memory when the application is scaled horizontally.

Example:

Load Balancer
     |
     +---- Server A
     |
     +---- Server B

A request may go to different servers.

For distributed session/state, consider:

distributed cache
database
token-based state
external state store

depending on requirements.

52. Concurrency and Transactions

A transaction provides atomicity for a set of database operations.

Example:

Create Order
     +
Reserve Inventory
     +
Create Payment Record

If these must satisfy a consistency boundary, database transactions may be required.

But a database transaction does not automatically coordinate external services.

For distributed workflows, consider:

Saga
outbox pattern
idempotency
message-based processing
53. Outbox Pattern

A common microservices concurrency/reliability pattern:

Database Transaction
       |
       +---- Order
       |
       +---- Outbox Event
                 |
                 ↓
             Publisher
                 |
                 ↓
          Message Broker

The order and event record are committed atomically in the same database transaction.

This avoids the dangerous sequence:

Save order
   ↓
Publish event
   ↓
Application crashes

where the order exists but the event was never published.

54. BackgroundService Concurrency

A BackgroundService may run independently of HTTP requests.

Example:

public sealed class OrderWorker
    : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessOrdersAsync(
                stoppingToken);
        }
    }
}

Important considerations:

cancellation
graceful shutdown
concurrency limits
retries
idempotency
failure handling
queue backpressure
observability
55. Do Not Fire-and-Forget Request Work Carelessly

Avoid:

_ = ProcessOrderAsync();

inside a request simply because you don't want to await it.

Problems include:

request may finish
scoped services may be disposed
exceptions may be lost
application may shut down
operation may never complete

Better:

HTTP Request
    ↓
Durable Queue
    ↓
Background Worker

for work that must continue independently.

56. Observability for Concurrency

Production systems should monitor:

Application
request latency
throughput
active requests
ThreadPool usage
queue length
Database
connection pool
query latency
locks
deadlocks
CPU
External APIs
latency
timeout rate
HTTP 429
HTTP 5xx
retry count
Queue
queue depth
consumer lag
processing time
failed messages
dead-letter count
57. Correlation ID

Concurrent systems make debugging difficult because many requests execute simultaneously.

Use a correlation ID:

Request
CorrelationId = ABC123
     ↓
Service A
     ↓
Service B
     ↓
Database / Queue

Logs can then be correlated across components.

For distributed systems, also propagate tracing information such as distributed trace/span context.

58. Common Concurrency Problems in ASP.NET Core
| Problem                      | Typical Cause                      | Solution                           |
| ---------------------------- | ---------------------------------- | ---------------------------------- |
| ThreadPool starvation        | `.Result`, `.Wait()`, blocking I/O | Async all the way                  |
| Race condition               | Shared mutable state               | Lock/atomic operation/immutability |
| DB overload                  | Unlimited concurrency              | Concurrency limits                 |
| Connection pool exhaustion   | Too many DB operations             | Limit concurrency                  |
| Cache stampede               | Many simultaneous cache misses     | Request coalescing/locking         |
| Duplicate payment            | Retries without idempotency        | Idempotency key                    |
| Duplicate processing         | Message redelivery                 | Idempotent consumer                |
| Deadlock                     | Circular resource waiting          | Consistent lock ordering           |
| Retry storm                  | Aggressive retries                 | Backoff + circuit breaker          |
| Lost background work         | Fire-and-forget                    | Durable queue                      |
| Cross-server race            | Local lock                         | Distributed/database coordination  |
| Request cancellation ignored | Token not propagated               | CancellationToken                  |


59. Common Interview Traps
Trap 1

Does async/await create a new thread?

No.

It is primarily an asynchronous programming model.

Trap 2

Does every HTTP request get a dedicated thread?

No.

ASP.NET Core uses ThreadPool and asynchronous I/O.

Trap 3

Does async code automatically become thread-safe?

No.

Shared mutable state can still have race conditions.

Trap 4

Can lock protect a distributed application?

No.

lock is process-local.

Trap 5

Is Task.WhenAll always better?

No.

It is useful for independent operations, but unlimited concurrency can overload dependencies.

Trap 6

Can you use the same EF Core DbContext concurrently?

Generally no.

DbContext is not thread-safe.

Trap 7

Does Scoped mean thread-safe?

No.

Scoped means lifetime within a request scope; it does not guarantee thread safety.

Trap 8

Does ConcurrentDictionary make every business operation thread-safe?

No.

Its individual operations are designed for concurrent use, but multi-step workflows still require appropriate synchronization or atomic operations.

Trap 9

Should Task.Run be used for database calls?

Generally no.

Use the database provider's asynchronous API directly.

Trap 10

Can a request safely start arbitrary background work?

No.

Use a proper background processing mechanism for work that must outlive the request.

60. Product-Company Scenario
Scenario

An e-commerce API receives:

10,000 requests/minute

Each request:

validates the order
checks inventory
calls payment service
saves order
publishes event

A naive implementation could create:

Request
 ↓
Inventory
 ↓
Payment
 ↓
Database
 ↓
Event

with unlimited concurrency.

Potential failures:

Traffic spike
    ↓
Too many concurrent requests
    ↓
Database overload
    ↓
Connection pool exhaustion
    ↓
Timeouts
    ↓
Retries
    ↓
More traffic
    ↓
Cascading failure

A production design may include:

Client
  ↓
Load Balancer
  ↓
ASP.NET Core
  ↓
Rate Limiting
  ↓
Validation
  ↓
Concurrency Control
  ↓
Database / Cache / Payment
  ↓
Outbox
  ↓
Message Broker
  ↓
Background Consumers

With:

async I/O
bounded concurrency
cancellation
timeout
retry with backoff
circuit breaker
idempotency
optimistic concurrency
distributed cache
queue backpressure
observability
61. Recommended ASP.NET Core Concurrency Principles
Principle 1

Use asynchronous APIs for I/O.

await repository.GetAsync();
Principle 2

Avoid blocking:

.Result
.Wait()
Thread.Sleep()
Principle 3

Use Task.WhenAll for independent operations.

Principle 4

Control concurrency.

Do not create unlimited tasks.

Principle 5

Minimize shared mutable state.

Principle 6

Use thread-safe primitives when shared state is unavoidable.

Examples:

lock
Interlocked
ConcurrentDictionary
SemaphoreSlim
Principle 7

Remember process boundaries.

lock
SemaphoreSlim
ConcurrentDictionary

are generally process-local.

Principle 8

Use database-level concurrency controls for shared persistent data.

Principle 9

Propagate CancellationToken.

Principle 10

Use queues for long-running background work.

Principle 11

Make distributed operations idempotent.

Principle 12

Measure before optimizing.

62. Concurrency Decision Guide
Do I have shared mutable state?
        |
       Yes
        ↓
Can I eliminate the shared state?
        |
     Yes → Prefer local/immutable state
        |
       No
        ↓
Is it process-local?
        |
     Yes
        ↓
Choose:
lock
Interlocked
ConcurrentDictionary
SemaphoreSlim
        |
       No
        ↓
Distributed coordination
        |
        +-- Database transaction
        +-- Optimistic concurrency
        +-- Unique constraint
        +-- Distributed lock
        +-- Idempotency
        +-- Queue/message broker

For asynchronous I/O:

Independent operations?
       |
      Yes
       ↓
Task.WhenAll
       |
       ↓
Too many operations?
       |
      Yes
       ↓
Limit concurrency
       |
       ↓
Parallel.ForEachAsync /
SemaphoreSlim /
Channel
63. ASP.NET Core Concurrency Mental Model

Think about concurrency at four levels:

Level 1
Request Concurrency
        ↓
Many HTTP requests

Level 2
Application Concurrency
        ↓
Tasks / async / parallel work

Level 3
Resource Concurrency
        ↓
Database / cache / external APIs

Level 4
Distributed Concurrency
        ↓
Multiple application instances

A solution at one level may not solve another level.

For example:

lock

can solve a process-local problem but not a distributed one.

64. Key Interview Points
ASP.NET Core is designed to handle many concurrent requests.
Kestrel handles HTTP requests, while asynchronous operations help avoid unnecessary thread blocking.
async/await does not mean parallel execution.
Task does not equal Thread.
Avoid .Result, .Wait(), and other unnecessary blocking.
Use async database/HTTP APIs directly.
Use Task.WhenAll for independent operations.
Do not create unlimited concurrency.
Use Parallel.ForEachAsync, SemaphoreSlim, or queues when concurrency must be controlled.
Singleton services containing mutable state must be thread-safe.
Scoped lifetime does not automatically make code thread-safe.
DbContext is not thread-safe.
ConcurrentDictionary protects collection operations, not arbitrary business workflows.
lock is process-local.
SemaphoreSlim is also process-local.
Distributed concurrency requires database or distributed coordination.
CancellationToken should be propagated through asynchronous operations.
Avoid fire-and-forget work inside HTTP requests.
Use BackgroundService or durable messaging for independent background processing.
Retries must be combined with timeouts, backoff, and often circuit breakers.
Idempotency is essential for retryable operations such as payments and order processing.
Database concurrency needs transactions, optimistic concurrency, atomic updates, or appropriate locking.
Reduce shared mutable state before adding synchronization.
Measure concurrency and performance under realistic load.
65. Final Mental Model
                    ASP.NET Core
                         |
              Multiple Requests
                         |
                 Async I/O First
                         |
              +----------+----------+
              |                     |
         Independent            Shared State
          Operations                 |
              |                     |
         Task.WhenAll          Thread Safety
              |                     |
       Limit Concurrency      +------+------+------+
              |               |      |      |
        SemaphoreSlim       lock Interlocked Concurrent*
              |               |
          Backpressure       Process-local
              |
       External Resources
              |
       +------+------+------+
       |      |      |
       DB    HTTP   Queue
       |      |      |
    Limits  Rate   Consumers
    /Locks  Limit  /Backpressure
       |
   Distributed System
       |
   +---+---+---+---+
   |       |       |
Transactions Idempotency
Optimistic   Distributed
Concurrency  Coordination
Final One-Line Definition

ASP.NET Core concurrency is the safe and efficient management of overlapping HTTP requests and asynchronous operations, using non-blocking I/O, controlled concurrency, thread-safe state management, cancellation,
and appropriate database or distributed coordination to maintain performance and correctness under load.
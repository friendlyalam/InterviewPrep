# ValueTask<T>

## 1. Definition

`ValueTask<T>` is a lightweight value-type (`struct`) used to represent an asynchronous operation that may complete **synchronously or asynchronously**.

It is mainly useful when an operation **frequently completes synchronously** and we want to reduce unnecessary `Task<T>` allocations.

```csharp
ValueTask<int> GetValueAsync()

Mental model:

Task<T>
    ↓
Usually represents an async operation
    ↓
Reference type → allocation may be required

ValueTask<T>
    ↓
Can represent:
    ├── An already available result
    └── A Task<T> for an operation that is still running
2. Why ValueTask Exists

Consider a cache lookup.

Many requests may find the value immediately:

public async Task<string?> GetAsync(string key)
{
    if (_cache.TryGetValue(key, out string? value))
        return value;

    return await LoadFromDatabaseAsync(key);
}

If the cache hit happens very frequently, returning a Task<string?> can introduce unnecessary task allocations.

ValueTask<T> can represent the already available result without requiring a new Task<T>.

public ValueTask<string?> GetAsync(string key)
{
    if (_cache.TryGetValue(key, out string? value))
        return ValueTask.FromResult(value);

    return new ValueTask<string?>(LoadFromDatabaseAsync(key));
}

The important idea is:

ValueTask<T> is useful when synchronous completion is common enough that avoiding Task<T> allocations matters.

3. Task<T> vs ValueTask<T>
| Feature                        | Task<T>                    | ValueTask<T>                                    |
| ------------------------------ | -------------------------- | ----------------------------------------------- |
| Type                           | Reference type             | Value type (`struct`)                           |
| Can represent async operation  | Yes                        | Yes                                             |
| Can represent immediate result | Yes                        | Yes                                             |
| Common default                 | Yes                        | No                                              |
| Allocation for async operation | Usually task object exists | Can avoid allocation for synchronous completion |
| Multiple awaits                | Supported                  | Should generally be avoided                     |
| `.AsTask()`                    | Not needed                 | Converts to `Task<T>`                           |
| API complexity                 | Simple                     | More complex                                    |
| Performance optimization       | General-purpose            | Specialized optimization                        |
| Recommended for most APIs      | Yes                        | Only when justified                             |

Key rule

Use Task<T> by default. Use ValueTask<T> when profiling or API design shows that it provides a meaningful benefit.


4. ValueTask Without Generic Result

There is also:

ValueTask

and:

ValueTask<T>

Example:

public ValueTask SaveAsync()
{
    return ValueTask.CompletedTask;
}

For a result:

public ValueTask<int> GetCountAsync()
{
    return ValueTask.FromResult(10);
}
5. Synchronous Completion

One of the main reasons to use ValueTask<T> is synchronous completion.

public ValueTask<int> GetCachedValueAsync()
{
    return ValueTask.FromResult(100);
}

The operation is already complete.

The caller can still use:

int value = await GetCachedValueAsync();

There is no special calling syntax.

6. Asynchronous Completion

ValueTask<T> can also wrap an actual Task<T>.

public ValueTask<int> GetValueAsync()
{
    Task<int> task = LoadFromDatabaseAsync();

    return new ValueTask<int>(task);
}

private async Task<int> LoadFromDatabaseAsync()
{
    await Task.Delay(100);

    return 100;
}

Conceptually:

ValueTask<int>
      |
      +---- immediate result
      |
      +---- Task<int>
7. Awaiting ValueTask

The normal way to consume a ValueTask<T> is:

int value = await GetValueAsync();

Example:

public async Task ProcessAsync()
{
    int value = await GetValueAsync();

    Console.WriteLine(value);
}

From the caller's perspective, it behaves similarly to awaiting a Task<T>.

8. ValueTask Is Not a Replacement for Task

A common interview mistake is:

"ValueTask is a faster version of Task."

That is incorrect.

ValueTask<T> exists for a specific optimization scenario.

It has additional usage restrictions and complexity.

Therefore:

Task<T>
    ↓
Default async abstraction

ValueTask<T>
    ↓
Specialized optimization
9. Important Usage Restriction

A ValueTask should generally be awaited once.

Avoid:

ValueTask<int> valueTask = GetValueAsync();

int first = await valueTask;
int second = await valueTask;   // Avoid

A ValueTask is not designed to provide the same reusable semantics as a Task.

The recommended approach is:

int value = await GetValueAsync();

If the result needs to be awaited multiple times, convert it to a Task:

Task<int> task = GetValueAsync().AsTask();

int first = await task;
int second = await task;

Now the Task<int> can be reused.

10. Do Not Store ValueTask for Long Periods

Avoid treating a ValueTask like a long-lived task object.

For example:

private ValueTask<int> _operation;

This can make lifecycle and consumption semantics harder to reason about.

Prefer:

Task<int> _operation;

when the operation needs to be stored, shared, or awaited multiple times.

11. AsTask()

ValueTask<T> provides:

.AsTask()

Example:

ValueTask<int> valueTask = GetValueAsync();

Task<int> task = valueTask.AsTask();

int result = await task;

This is useful when an API requires a Task<T>.

For example:

Task<int> task = SomeLibraryMethod(GetValueAsync().AsTask());
Important

Don't repeatedly call AsTask() on the same ValueTask without understanding its semantics.

Convert once when a reusable Task is actually required.

12. ValueTask.CompletedTask

For operations without a result:

public ValueTask SaveAsync()
{
    return ValueTask.CompletedTask;
}

This represents an already-completed operation.

For Task:

return Task.CompletedTask;

For ValueTask:

return ValueTask.CompletedTask;
13. ValueTask.FromResult()

For an already available result:

public ValueTask<int> GetNumberAsync()
{
    return ValueTask.FromResult(100);
}

Equivalent conceptual operation:

Result already available
        ↓
ValueTask<int>
        ↓
await
        ↓
100
14. Example: Cache + Database

A realistic use case is a high-performance cache.

public sealed class ProductService
{
    private readonly Dictionary<int, string> _cache = new();

    public ValueTask<string?> GetProductAsync(int productId)
    {
        if (_cache.TryGetValue(productId, out string? product))
        {
            return ValueTask.FromResult<string?>(product);
        }

        return new ValueTask<string?>(
            LoadFromDatabaseAsync(productId));
    }

    private async Task<string?> LoadFromDatabaseAsync(int productId)
    {
        await Task.Delay(100);

        return $"Product-{productId}";
    }
}

Flow:

GetProductAsync()
       |
       +---- Cache hit
       |       ↓
       |   immediate result
       |       ↓
       |   ValueTask
       |
       +---- Cache miss
               ↓
          Database call
               ↓
             Task
               ↓
          ValueTask wrapping Task

This is one of the scenarios where ValueTask<T> can make architectural sense.

15. ValueTask and async Methods

You can write:

public async ValueTask<int> GetValueAsync()
{
    return 100;
}

However, don't automatically change every:

async Task<T>

to:

async ValueTask<T>

just because ValueTask can sometimes reduce allocations.

The optimization should have a reason.

16. Important Performance Consideration

ValueTask<T> itself is a struct.

That does not mean it is automatically faster.

There are trade-offs.

For example:

Task<T>
    ↓
Simple
Reusable
Widely supported
Easy to compose

ValueTask<T>
    ↓
Potentially fewer allocations
But
More complex consumption semantics
Larger value type
Possible conversion to Task

If the operation is usually asynchronous, Task<T> may be the better choice.

17. When ValueTask Is a Good Choice

Consider ValueTask<T> when:

1. Synchronous completion is common

Example:

Cache lookup
Memory lookup
Already-buffered data
Already-computed result
2. The API is performance-sensitive

Examples:

High-throughput networking
High-frequency parsing
Low-allocation libraries
Hot paths
3. Allocation reduction has been measured

Use profiling/benchmarking to confirm that allocations matter.

18. When Task Is Better

Prefer Task<T> when:

1. The operation normally completes asynchronously

For example:

Database call
HTTP request
Remote microservice call
File I/O
2. The operation needs to be awaited multiple times
Task<int> task = GetValueAsync();

await task;
await task;
3. The task needs to be stored
private Task<Product> _productTask;
4. The API requires Task

Avoid unnecessary conversions:

GetValueAsync().AsTask();
5. Performance benefit has not been demonstrated

Don't optimize based only on theory.

19. ValueTask and I/O

A common misconception:

"ValueTask should be used for I/O because I/O is asynchronous."

Not necessarily.

The important question is:

Does the operation frequently complete synchronously?

For example:

HTTP request
    ↓
Usually waits for network
    ↓
Task<T> is normally appropriate

Whereas:

Cache lookup
    ↓
Frequently already available
    ↓
ValueTask<T> may be useful
20. ValueTask and IAsyncEnumerable

ValueTask is especially important in modern asynchronous APIs.

For example, IAsyncEnumerator<T> uses:

ValueTask<bool> MoveNextAsync()

Conceptually:

await foreach
      ↓
MoveNextAsync()
      ↓
ValueTask<bool>
      ↓
Next item available?

This allows implementations to avoid unnecessary allocations when the next item is already available.

Example:

await foreach (var item in GetItemsAsync())
{
    Console.WriteLine(item);
}

The async iterator infrastructure can use ValueTask<bool> for efficient asynchronous iteration.

21. ValueTask and I/O Completion

A high-performance API may have operations where the result is sometimes immediately available.

For example:

Read from buffer
       |
       +---- Data already available
       |          ↓
       |     synchronous completion
       |
       +---- Data not available
                  ↓
             asynchronous wait

ValueTask<T> can represent both cases efficiently.

This is one reason it appears in performance-sensitive .NET APIs.

22. ValueTask and Cancellation

ValueTask<T> supports asynchronous operations that may also observe cancellation.

Example:

public ValueTask<int> GetValueAsync(
    CancellationToken cancellationToken)
{
    cancellationToken.ThrowIfCancellationRequested();

    return ValueTask.FromResult(100);
}

The important concept is:

Cancellation behavior belongs to the operation; ValueTask only represents that operation's completion.

23. Exception Handling

Exceptions can be observed normally through await.

public async ValueTask<int> GetValueAsync()
{
    await Task.Delay(100);

    throw new InvalidOperationException(
        "Operation failed.");
}

Caller:

try
{
    int result = await GetValueAsync();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

The exception is observed when the ValueTask is awaited.

24. ValueTask vs Task.WhenAll

Task.WhenAll works naturally with Task.

For example:

Task<int> task1 = GetTaskAsync();
Task<int> task2 = GetTaskAsync();

int[] results =
    await Task.WhenAll(task1, task2);

With ValueTask<T>, you may need to convert:

Task<int> task1 =
    GetValueAsync().AsTask();

Task<int> task2 =
    GetValueAsync().AsTask();

int[] results =
    await Task.WhenAll(task1, task2);

This demonstrates an important trade-off:

ValueTask is not always more convenient for task composition.

25. ValueTask vs Parallelism

ValueTask does not create parallel execution.

This:

ValueTask<int> operation = GetValueAsync();

does not mean:

Parallel execution

It only represents completion of an asynchronous operation.

Remember:

ValueTask
    ↓
Async operation representation

Parallelism
    ↓
Multiple operations executing simultaneously

They are different concepts.

26. ValueTask vs async/await

These are also different concepts.

async/await
    ↓
Programming model for asynchronous code

Task
    ↓
Common representation of asynchronous operation

ValueTask
    ↓
Alternative representation optimized for certain scenarios

Example:

public async ValueTask<int> GetValueAsync()
{
    return 100;
}

Here:

async
    → asynchronous method implementation

await
    → asynchronous control flow

ValueTask<int>
    → return representation
27. Common Mistake: Using ValueTask Everywhere

Bad design:

public ValueTask<Product> GetProductAsync(...)

for every service method simply because:

"ValueTask is faster."

This can make the application harder to understand without providing measurable benefit.

Better:

public Task<Product> GetProductAsync(...)

for ordinary application/service code.

Use:

ValueTask<Product>

when the API has a strong reason for it.

28. Common Mistake: Multiple Awaits

Avoid:

ValueTask<int> operation = GetValueAsync();

await operation;
await operation;

Prefer:

int result = await GetValueAsync();

If repeated consumption is required:

Task<int> task = GetValueAsync().AsTask();

int first = await task;
int second = await task;
29. Common Mistake: Assuming ValueTask Means Zero Allocation

Incorrect:

"ValueTask guarantees zero allocations."

Not necessarily.

If ValueTask<T> wraps a Task<T>, the underlying task may still be allocated.

The main benefit is that synchronous completion can sometimes avoid creating a task object.

Therefore:

ValueTask
≠
Guaranteed zero allocation
30. Common Mistake: Ignoring Struct Costs

ValueTask<T> is a struct.

Large structs can have their own copying/usage costs.

Therefore, "struct = faster" is not a universal rule.

Performance should be measured.

31. ValueTask in High-Performance Libraries

You are more likely to encounter ValueTask in:

.NET runtime APIs
Networking APIs
IAsyncEnumerable
high-throughput libraries
low-allocation libraries
custom protocol implementations
high-frequency asynchronous operations

It is less commonly necessary in ordinary business-service methods.

32. Enterprise Application Guidance

For a normal ASP.NET Core application:

Controller
   ↓
Service
   ↓
Repository
   ↓
Database

Generally prefer:

Task<T>

for:

HTTP calls
Database calls
Microservice calls
Repository operations
Business services

Use ValueTask<T> selectively when:

Operation frequently completes synchronously
        +
High call frequency
        +
Allocation/performance is measurable
        +
API design benefits from it
33. Practical Example
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ValueTaskDemo;

public sealed class ProductCache
{
    private readonly Dictionary<int, string> _cache = new();

    public ValueTask<string?> GetProductAsync(int productId)
    {
        if (_cache.TryGetValue(productId, out string? product))
        {
            return ValueTask.FromResult<string?>(product);
        }

        return new ValueTask<string?>(
            LoadProductAsync(productId));
    }

    private async Task<string?> LoadProductAsync(int productId)
    {
        await Task.Delay(100);

        string product = $"Product-{productId}";

        _cache[productId] = product;

        return product;
    }
}

public static class Program
{
    public static async Task Main()
    {
        ProductCache cache = new();

        string? first =
            await cache.GetProductAsync(101);

        Console.WriteLine($"First call: {first}");

        string? second =
            await cache.GetProductAsync(101);

        Console.WriteLine($"Second call: {second}");
    }
}

Conceptually:

First request
     ↓
Cache miss
     ↓
Database/remote operation
     ↓
Async completion
     ↓
ValueTask wraps Task

Second request
     ↓
Cache hit
     ↓
Immediate result
     ↓
ValueTask.FromResult

This demonstrates the primary motivation for ValueTask.

34. ValueTask Decision Guide

Use this decision process:

Do I need an asynchronous return type?
                |
                Yes
                |
        Is synchronous completion
        frequent?
          /             \
        No               Yes
        |                 |
     Task<T>       Is this a hot/high-
                   performance path?
                    /          \
                  No            Yes
                  |              |
               Task<T>      Benchmark/
                            profiling
                                |
                           Benefit exists?
                           /          \
                         No            Yes
                         |              |
                      Task<T>       ValueTask<T>
35. Interview Questions
Q1. What is ValueTask?

ValueTask<T> is a value-type abstraction representing an asynchronous operation that can either complete synchronously with a result or asynchronously, potentially reducing allocations in suitable scenarios.

Q2. Is ValueTask always faster than Task?

No.

ValueTask is an optimization for specific scenarios, particularly operations that frequently complete synchronously.

Q3. Why is ValueTask a struct?

It allows the completed result to sometimes be represented directly without allocating a separate Task<T> object.

Q4. Can ValueTask wrap a Task?

Yes.

return new ValueTask<int>(
    SomeTaskReturningMethod());
Q5. Can ValueTask be awaited multiple times?

It should generally be treated as a single-consumption operation.

If multiple awaits or reusable semantics are required, convert it to a Task using:

.AsTask()
Q6. When should you use Task instead?

Use Task<T> when:

operation normally completes asynchronously
task needs to be stored
task needs multiple consumers
task composition is important
performance benefit from ValueTask is not demonstrated
API simplicity is more important
Q7. What is ValueTask<T>.AsTask()?

It converts a ValueTask<T> into a Task<T> so it can be used where normal task semantics are required.

Q8. Does ValueTask provide parallelism?

No.

It only represents an asynchronous operation.

Q9. Does ValueTask guarantee zero allocations?

No.

If it wraps a Task<T>, that task may still have been allocated.

Q10. Why does IAsyncEnumerable use ValueTask?

IAsyncEnumerator<T>.MoveNextAsync() uses ValueTask<bool> because the next item may already be available synchronously, allowing efficient iteration without unnecessary task allocations.

36. Product-Company Interview Scenario
Scenario

You are building a high-throughput in-memory cache.

90% of requests are cache hits and complete immediately.

10% require an asynchronous database lookup.

Which return type could be considered?

ValueTask<Product?>

Why?

Because the operation frequently completes synchronously.

However, the final decision should be supported by:

profiling
benchmarks
allocation measurements
throughput measurements
API complexity analysis

Do not choose ValueTask simply because it sounds faster.

37. Task vs ValueTask — Interview Summary
| Question                | Task              | ValueTask                              |
| ----------------------- | ----------------- | -------------------------------------- |
| Default choice          | Yes               | No                                     |
| Reference type          | Yes               | No                                     |
| Struct                  | No                | Yes                                    |
| Async operation         | Yes               | Yes                                    |
| Immediate result        | Yes               | Yes                                    |
| Reusable                | Yes               | Not generally                          |
| Multiple awaits         | Yes               | Avoid                                  |
| `AsTask()`              | N/A               | Yes                                    |
| Simple API              | Yes               | Less simple                            |
| Allocation optimization | General           | Potentially better for sync completion |
| High-performance APIs   | Yes               | Often useful                           |
| Normal business code    | Usually preferred | Selective                              |
| Requires benchmarking   | Not usually       | Strongly recommended                   |

38. Key Points to Remember
ValueTask<T> is a struct representing an asynchronous result.
It can represent:
an immediately available result
an asynchronous operation backed by a Task<T>
Its primary purpose is reducing unnecessary allocations when synchronous completion is common.
Task<T> should remain the default choice for most application code.
Do not assume:
ValueTask = faster Task
Do not assume:
ValueTask = zero allocation
Avoid multiple awaits on the same ValueTask.
Use:
.AsTask()

when normal reusable Task semantics are needed.

ValueTask does not create parallelism.
ValueTask is particularly relevant in:
high-performance APIs
networking
async streams
IAsyncEnumerable
low-allocation libraries
frequently synchronously completing operations
For ordinary ASP.NET Core business services, repositories, HTTP calls, and database calls, prefer Task<T> unless there is a measured reason to use ValueTask<T>.
The product-company interview mental model is:
Task<T>
    ↓
Default asynchronous abstraction

ValueTask<T>
    ↓
Specialized performance optimization
    ↓
Useful when synchronous completion is frequent
    ↓
Use only when the trade-off is justified
Final One-Line Definition

ValueTask<T> is a lightweight value-type representation of an asynchronous operation that can
avoid unnecessary Task<T> allocations when the operation frequently completes synchronously, but it should be used selectively rather than as a general replacement for Task<T>.
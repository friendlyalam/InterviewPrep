# IAsyncEnumerable<T>

## 1. Definition

`IAsyncEnumerable<T>` represents a sequence of values that can be produced **asynchronously over time**.

It is useful when data is:

- Large
- Generated gradually
- Retrieved from an external source
- Streamed from a database/API/file
- Not required all at once

Namespace:

```csharp
using System.Collections.Generic;
2. Why Use IAsyncEnumerable<T>?

Traditional:

Get all data
    ↓
Load into memory
    ↓
Process

IAsyncEnumerable<T>:

Get item
   ↓
Process item
   ↓
Get next item
   ↓
Process next item

Benefits:

Lower memory usage
Start processing earlier
Natural async streaming
Supports cancellation
Good for large datasets
3. Basic Syntax

Producer:

public async IAsyncEnumerable<int> GetNumbersAsync()
{
    for (int i = 1; i <= 5; i++)
    {
        await Task.Delay(500);

        yield return i;
    }
}

Consumer:

await foreach (int number in GetNumbersAsync())
{
    Console.WriteLine(number);
}
4. yield return

yield return produces one item at a time.

yield return item;

The method does not need to create the complete collection first.

5. IEnumerable<T> vs IAsyncEnumerable<T>
| `IEnumerable<T>`                              | `IAsyncEnumerable<T>`         |
| --------------------------------------------- | ----------------------------- |
| Synchronous iteration                         | Asynchronous iteration        |
| `foreach`                                     | `await foreach`               |
| `MoveNext()`                                  | `MoveNextAsync()`             |
| Good for in-memory data                       | Good for async/streaming data |
| Blocks if underlying operation is synchronous | Supports asynchronous waiting |


6. Task<List<T>> vs IAsyncEnumerable<T>
Task<List<T>>
Database
   ↓
Fetch all records
   ↓
List<T>
   ↓
Return

All results are materialized before processing.

IAsyncEnumerable<T>
Database
   ↓
Item 1 → Process
Item 2 → Process
Item 3 → Process
...

Data can be processed incrementally.

7. Cancellation

Long-running streams should support cancellation.

public async IAsyncEnumerable<int> GetNumbersAsync(
    [EnumeratorCancellation]
    CancellationToken cancellationToken)
{
    for (int i = 1; i <= 100; i++)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await Task.Delay(
            100,
            cancellationToken);

        yield return i;
    }
}

Consumer:

await foreach (
    int number in GetNumbersAsync(cts.Token))
{
    Console.WriteLine(number);
}
8. [EnumeratorCancellation]

For async iterators, use:

[EnumeratorCancellation]
CancellationToken cancellationToken

This allows the cancellation token supplied by await foreach to be propagated to the iterator.

Namespace:

using System.Runtime.CompilerServices;
9. WithCancellation

Another common pattern:

await foreach (
    int number in GetNumbersAsync()
        .WithCancellation(cancellationToken))
{
    Console.WriteLine(number);
}

This passes cancellation to the async enumeration.

10. ASP.NET Core Streaming

IAsyncEnumerable<T> can be useful when an API needs to stream results instead of loading everything into memory first.

Conceptually:

Client
  ↓
ASP.NET Core API
  ↓
IAsyncEnumerable<T>
  ↓
Database / External Source

The exact HTTP response behavior depends on the API/formatter and framework configuration.

11. EF Core

IAsyncEnumerable<T> can be useful for streaming database results.

Example concept:

await foreach (
    Order order in dbContext.Orders
        .AsAsyncEnumerable())
{
    Process(order);
}

Be careful with:

DbContext lifetime
Connection lifetime
Query execution
Cancellation
Transaction boundaries

Do not assume streaming always improves performance.

12. Important Difference: Async Does Not Mean Parallel

This:

await foreach (var item in GetItemsAsync())
{
    await ProcessAsync(item);
}

processes items sequentially.

It is asynchronous, but not automatically parallel.

Item 1 → Process
          ↓
Item 2 → Process
          ↓
Item 3 → Process
13. Streaming vs Parallel Processing

If items are independent and need concurrent processing, concurrency must be designed explicitly.

For example:

Producer
   ↓
IAsyncEnumerable
   ↓
Bounded Channel
   ↓
Multiple Consumers

This provides both:

Streaming
Controlled concurrency
14. Exception Handling

Exceptions can be handled during enumeration:

try
{
    await foreach (
        int item in GetNumbersAsync())
    {
        Process(item);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

An exception can occur while producing or consuming the sequence.

15. Deferred Execution

Async iterators generally use deferred execution.

The producer does not execute the complete sequence when the method is called.

Execution happens as the consumer requests items.

GetNumbersAsync()
      ↓
No complete execution yet
      ↓
await foreach
      ↓
Produce next item
16. Memory Benefit

For a large dataset:

Task<List<Order>>
1,000,000 orders
        ↓
Large memory allocation

With streaming:

IAsyncEnumerable<Order>
        ↓
Read a manageable amount
        ↓
Process
        ↓
Continue

This can reduce memory pressure.

17. When to Use

Use IAsyncEnumerable<T> when:

Results are large.
Results arrive gradually.
Processing can start before all data is available.
The source supports asynchronous enumeration.
Streaming provides a meaningful benefit.
18. When NOT to Use

Avoid it when:

The dataset is small.
All results are needed immediately.
A normal List<T> is simpler.
The underlying source cannot benefit from streaming.
Streaming complicates transaction/resource lifetime unnecessarily.
19. Common Mistakes
Mistake 1

Assuming IAsyncEnumerable<T> means parallel processing.

Mistake 2

Loading everything into memory before returning the async stream.

Mistake 3

Ignoring cancellation.

Mistake 4

Keeping database resources open unnecessarily long.

Mistake 5

Creating too many concurrent consumers without limits.

20. Product Company Scenario
Large Order Export

Suppose an application needs to export millions of orders.

Instead of:

Database
   ↓
List<Order>
   ↓
Export

Use:

Database
   ↓
IAsyncEnumerable<Order>
   ↓
Process Order
   ↓
Write Export

This allows incremental processing and can reduce memory usage.

21. Interview Questions
Q1. What is IAsyncEnumerable<T>?

An asynchronous sequence that produces elements over time and can be consumed using await foreach.

Q2. IEnumerable<T> vs IAsyncEnumerable<T>?

IEnumerable<T> is synchronous; IAsyncEnumerable<T> supports asynchronous iteration.

Q3. Does IAsyncEnumerable<T> mean parallel processing?

No. It provides asynchronous streaming, not automatic parallelism.

Q4. Why use it instead of Task<List<T>>?

It can process results incrementally instead of materializing the entire collection in memory.

Q5. How do you cancel async enumeration?

Use CancellationToken, often with [EnumeratorCancellation] or WithCancellation().

Q6. Can IAsyncEnumerable<T> be used with EF Core?

Yes, AsAsyncEnumerable() can expose query results as an async stream, but resource lifetime and query behavior must be considered.

22. Key Points
IAsyncEnumerable<T> = asynchronous sequence.
Consume with await foreach.
Produce values using yield return.
Useful for large/streaming datasets.
Can reduce memory usage.
Supports cancellation.
Async does not mean parallel.
Use controlled concurrency when parallel processing is required.
Be careful with database/resource lifetimes.
Do not use it just because it is modern; use it when streaming provides value.
Final Mental Model
IEnumerable<T>
    ↓
foreach
    ↓
Synchronous sequence


IAsyncEnumerable<T>
    ↓
await foreach
    ↓
Asynchronous sequence
    ↓
Produce → Process → Produce → Process

Product-company one-line summary:
IAsyncEnumerable<T> provides asynchronous, incremental iteration over a sequence, 
allowing applications to process data as it becomes available without requiring the entire result set to be materialized first.
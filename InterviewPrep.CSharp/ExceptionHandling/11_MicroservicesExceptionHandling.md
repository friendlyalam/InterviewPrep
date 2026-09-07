Exception Handling in Microservices

For your backend preparation, this is a must-know topic.

Imagine:

Order Service
      |
      ↓
Payment Service
      |
      ↓
Inventory Service

Suppose Payment Service fails.

You need to think about:

Should Order Service retry?
Is the failure transient?
How many retries?
Exponential backoff?
Circuit breaker?
Idempotency?
What HTTP status should be returned?
Should the operation be placed on a queue?
Should a compensating action occur?
How should the failure be logged?
How do we correlate the request across services?

Exception handling therefore becomes part of distributed-system reliability, not merely try/catch.

27. Exception Handling + Messaging

For RabbitMQ/Azure Service Bus-style systems, you should understand:

Consumer
   ↓
Process Message
   ↓
Exception
   ↓
Retry
   ↓
Still failing
   ↓
Dead Letter Queue

You should know the concepts:

retry
delayed retry
dead-letter queue
poison message
idempotency
acknowledgement
reprocessing

These are highly relevant to microservices interviews.

28. Domain/Business Exceptions

A useful architecture can distinguish business failures from technical failures.

Example:

public sealed class InsufficientInventoryException
    : Exception
{
    public InsufficientInventoryException(string productId)
        : base($"Insufficient inventory for product {productId}.")
    {
    }
}

Then your API layer can translate it:

InsufficientInventoryException
             ↓
           409
       Conflict

while an unexpected infrastructure exception might become:

DatabaseException
      ↓
    500

The exact mapping depends on your API contract and architecture.

29. Don't Expose Internal Exceptions to Clients

Never return:

{
    "error": "SqlException: Login failed for user..."
}

or:

{
    "stackTrace": "..."
}

to production clients.

Instead:

{
    "title": "An unexpected error occurred.",
    "status": 500,
    "traceId": "..."
}

Log the detailed exception internally.

30. Correlation ID / Trace ID

For microservices, this is extremely valuable.

Imagine:

Client
  |
  | TraceId = ABC123
  ↓
API Gateway
  |
  ↓
Order Service
  |
  ↓
Payment Service
  |
  ↓
Inventory Service

If Payment Service fails, you should be able to search logs using:

ABC123

and see the complete request journey.

Modern distributed systems often use distributed tracing and standards such as OpenTelemetry.

31. Exception Handling Architecture

A strong production architecture might look like:

                    Client
                       |
                       ↓
                 ASP.NET Core
                       |
              Global Exception
                   Handler
                       |
        ┌──────────────┴──────────────┐
        ↓                             ↓
  Expected Exception          Unexpected Exception
        ↓                             ↓
  Appropriate 4xx               Log + 500
        |
        ↓
   ProblemDetails

Meanwhile, lower layers:

Controller
    ↓
Application Service
    ↓
Domain
    ↓
Repository
    ↓
Database

Each layer should generally handle exceptions only when it has meaningful responsibility to do so.

32. Important Exceptions You Should Know

| Exception                     | Typical Meaning                     |
| ----------------------------- | ----------------------------------- |
| `ArgumentException`           | Invalid argument                    |
| `ArgumentNullException`       | Null argument                       |
| `ArgumentOutOfRangeException` | Argument outside valid range        |
| `InvalidOperationException`   | Operation invalid for current state |
| `NullReferenceException`      | Null reference was dereferenced     |
| `IndexOutOfRangeException`    | Invalid array index                 |
| `KeyNotFoundException`        | Dictionary key missing              |
| `FormatException`             | Invalid format                      |
| `OverflowException`           | Numeric overflow                    |
| `DivideByZeroException`       | Division by zero                    |
| `IOException`                 | I/O failure                         |
| `FileNotFoundException`       | File doesn't exist                  |
| `UnauthorizedAccessException` | Access denied                       |
| `TimeoutException`            | Operation timed out                 |
| `OperationCanceledException`  | Operation was cancelled             |



33. OperationCanceledException vs Exception

This is particularly important with modern async applications.

try
{
    await service.ProcessAsync(cancellationToken);
}
catch (OperationCanceledException)
{
    // Operation was cancelled
}

Cancellation is not necessarily an application failure.

For example:

HTTP Request
     ↓
Client disconnects
     ↓
CancellationToken
     ↓
Database/API operation cancelled

A good application should understand cancellation instead of treating every cancellation as a system error.

34. Exception.Data

An exception can contain additional diagnostic information:

var exception = new Exception("Order processing failed.");

exception.Data["OrderId"] = orderId;
exception.Data["CustomerId"] = customerId;

throw exception;

Useful in some specialized scenarios, but structured logging is often a better approach for application diagnostics.

35. AggregateException

You should recognize it.

It can contain multiple exceptions, particularly in task-based APIs and parallel operations.

try
{
    Task.WaitAll(tasks);
}
catch (AggregateException ex)
{
    foreach (var inner in ex.InnerExceptions)
    {
        Console.WriteLine(inner.Message);
    }
}

However, in modern async code, prefer:

await Task.WhenAll(tasks);

and understand how task failures propagate through await.

36. Exception Performance

A common interview discussion:

Are exceptions expensive?

Yes, throwing exceptions is relatively expensive compared with normal branching.

Therefore don't write:

try
{
    var item = dictionary[key];
}
catch (KeyNotFoundException)
{
    // normal missing-key scenario
}

if absence is expected.

Prefer:

if (dictionary.TryGetValue(key, out var item))
{
    // found
}
else
{
    // not found
}

The principle is more important than memorizing benchmark numbers.

37. Exception Handling Anti-Patterns

You should be able to identify these immediately in an interview.

❌ Empty catch
catch
{
}
❌ Catch and rethrow incorrectly
catch (Exception ex)
{
    throw ex;
}
❌ Catch everything unnecessarily
catch (Exception ex)
{
    return null;
}
❌ Using exceptions for normal logic
try
{
    return dictionary[key];
}
catch
{
    return null;
}
❌ Exposing internal details
return BadRequest(ex.ToString());
❌ Logging and silently continuing
catch (Exception ex)
{
    logger.LogError(ex, "Failed");
}

when the operation actually needs to fail.

❌ Logging sensitive information
logger.LogError("Token: {Token}", token);


20. Logging Exceptions

This is one of the most important production concepts.

Bad:

catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Better:

catch (Exception ex)
{
    logger.LogError(
        ex,
        "Failed to process order {OrderId}",
        orderId);

    throw;
}

Notice:

logger.LogError(ex, ...)

not merely:

logger.LogError(ex.Message)

You want the exception object because it contains valuable diagnostic information such as:

stack trace
exception type
inner exception
additional exception data


21. Never Log Sensitive Data

Very important for real-world systems.

Avoid:

logger.LogError(
    ex,
    "Payment failed for card {CardNumber}",
    cardNumber);

Don't log:

passwords
access tokens
authentication credentials
full payment-card information
secrets
sensitive personal information

Use identifiers that are safe and useful for tracing instead.

22. Don't Swallow Exceptions

Bad:

try
{
    ProcessPayment();
}
catch (Exception)
{
}

This is called swallowing the exception.

It makes failures invisible.

At minimum:

catch (Exception ex)
{
    logger.LogError(ex, "Payment processing failed.");
    throw;
}

Or handle the exception if you have a legitimate recovery strategy.

23. Catch Only What You Can Handle

This is one of the most important principles.

Bad:

try
{
    ProcessOrder();
}
catch (Exception ex)
{
    logger.LogError(ex, "Error");
}

If you cannot recover from it, blindly catching it can actually make the application less reliable.

Instead:

try
{
    ProcessOrder();
}
catch (TimeoutException ex)
{
    logger.LogWarning(ex, "Order service timed out.");
    // retry/fallback if appropriate
}

Unexpected exceptions can propagate to a centralized handler.

24. Retryable vs Non-Retryable Exceptions

This becomes very important in microservices.

Example:
Example:

Payment Service
      ↓
Network timeout
      ↓
Potentially transient
      ↓
Retry

But:

Invalid customer ID
      ↓
Bad request/business error
      ↓
Retry won't help

So you need to distinguish:

Transient failures

Examples:

network timeout
temporary service unavailable
transient database connectivity problem

Potentially retry.

Permanent failures

Examples:

invalid input
authorization failure
entity doesn't exist
business rule violation

Usually don't retry.

25. Retry + Exception Handling

In production .NET systems, resilience libraries/patterns such as Polly are commonly used for retry, timeout, circuit-breaker and related resilience strategies.

Conceptually:

Request
   ↓
External Service
   ↓
Timeout
   ↓
Retry
   ↓
Timeout
   ↓
Retry
   ↓
Failure
   ↓
Circuit Breaker / Fallback

But:

Never blindly retry every exception.

Retries can make problems worse, especially for non-idempotent operations such as some payment or order operations.



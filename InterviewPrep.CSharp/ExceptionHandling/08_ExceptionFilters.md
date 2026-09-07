Exception Filters — when

Advanced but useful.

C# allows conditional exception handling.

catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
{
    Console.WriteLine("Resource not found.");
}

Another example:

catch (Exception ex) when (ex is TimeoutException)
{
    Console.WriteLine("Operation timed out.");
}

This is called an exception filter.


catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
{
    // Handle 404
}

Another example:

catch (Exception ex) when (ex is TimeoutException)
{
    // Handle timeout
}

Exception filters allow you to handle an exception only when a condition is satisfied.

14. when vs Catch + If

Instead of:

catch (HttpRequestException ex)
{
    if (ex.StatusCode == HttpStatusCode.NotFound)
    {
        // ...
    }
}

you can use:

catch (HttpRequestException ex)
    when (ex.StatusCode == HttpStatusCode.NotFound)
{
    // ...
}

This is cleaner when the condition determines whether the catch block should apply.

15. ExceptionDispatchInfo

This is an advanced topic.

It becomes useful when an exception needs to be captured and later rethrown while preserving its original stack trace.

ExceptionDispatchInfo.Capture(ex).Throw();

You won't use this every day, but knowing why it exists is useful for senior-level .NET discussions.
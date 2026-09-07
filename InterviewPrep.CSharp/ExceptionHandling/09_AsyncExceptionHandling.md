Async/Await Exception Handling

Very important for modern .NET.

try
{
    await ProcessOrderAsync();
}
catch (TimeoutException ex)
{
    // Handle timeout
}
Important

An exception thrown by an awaited task is propagated through await.

await SomeOperationAsync();

So don't unnecessarily do:

try
{
    await SomeOperationAsync();
}
catch (Exception ex)
{
    throw ex;
}

If you aren't adding meaningful behavior, the catch block is unnecessary.

17. Task.WhenAll

Important for product-company interviews.

var tasks = new[]
{
    GetCustomerAsync(),
    GetOrdersAsync(),
    GetPaymentsAsync()
};

await Task.WhenAll(tasks);

If one or more operations fail, you need to understand how exceptions from the tasks are propagated and how to inspect individual task failures when required.

For example:

var tasks = new[]
{
    GetCustomerAsync(),
    GetOrdersAsync()
};

try
{
    await Task.WhenAll(tasks);
}
catch
{
    foreach (var task in tasks)
    {
        if (task.IsFaulted)
        {
            Console.WriteLine(task.Exception);
        }
    }

    throw;
}
18. Exception Handling in ASP.NET Core

This is extremely important for your .NET backend interviews.

You generally don't want:

try
{
    // every controller action
}
catch (Exception ex)
{
    return StatusCode(500);
}

in every controller.

Instead, use centralized exception handling.

Modern ASP.NET Core provides mechanisms such as:

app.UseExceptionHandler();

and the IExceptionHandler abstraction.

Conceptually:

Request
   ↓
Controller
   ↓
Service
   ↓
Repository
   ↓
Exception
   ↓
Central Exception Handler
   ↓
HTTP Response

For example:

OrderNotFoundException
        ↓
       404

Validation problem
        ↓
       400

Unauthorized
        ↓
       401

Forbidden
        ↓
       403

Unexpected exception
        ↓
       500
19. ProblemDetails

For modern ASP.NET Core APIs, you should understand Problem Details.

Instead of returning an inconsistent response such as:

{
    "error": "something went wrong"
}

your API can use a standardized problem-details response.

Example:

{
    "type": "https://example.com/problems/order-not-found",
    "title": "Order not found",
    "status": 404,
    "detail": "Order 123 was not found."
}

This is particularly useful for consistent API contracts.
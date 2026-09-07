Custom Exceptions:
In C#, exceptions are used to handle unexpected situations in a program.
The .NET Framework provides many built-in exception classes such as DivideByZeroException,
NullReferenceException or IndexOutOfRangeException.
However, sometimes predefined exceptions are not sufficient. In such cases, 
developers can create their own custom exceptions to represent specific error conditions.

What are Custom Exceptions?
Custom exceptions are user-defined exception classes created by inheriting from the base System.Exception class. They allow developers to:

Provide meaningful exception names.
Add custom messages.
Include additional properties for error details.

Steps to Create a Custom Exception
Create a class that inherits from System.Exception.
Provide one or more constructors (default, parameterized or with inner exceptions).
(Optional) Add custom properties or methods to hold additional error information.

You should know how and when to create them.

Example:

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException(string message)
        : base(message)
    {
    }

    public OrderNotFoundException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}

Usage:

if (order is null)
{
    throw new OrderNotFoundException(
        $"Order {orderId} was not found.");
}
But don't overuse custom exceptions.

Don't create:

CustomerException
CustomerNameException
CustomerEmailException
CustomerAddressException

just for every possible validation problem.

Exceptions should represent exceptional situations, not ordinary control flow.


Exceptions vs Validation

This distinction is extremely important.

Bad design
try
{
    if (string.IsNullOrWhiteSpace(request.Email))
        throw new Exception();

    if (request.Age < 18)
        throw new Exception();
}
catch
{
    // validation
}

Validation failures are generally expected input problems, not exceptional system failures.

Instead:

if (string.IsNullOrWhiteSpace(request.Email))
{
    return ValidationResult.Invalid("Email is required.");
}

In ASP.NET Core, validation can be handled using mechanisms such as model validation and appropriate HTTP responses.

10. Exceptions Should Not Be Used for Normal Control Flow

Avoid:

try
{
    var customer = customers.Single(c => c.Id == id);
}
catch (InvalidOperationException)
{
    // customer doesn't exist
}

Prefer:

var customer = customers.SingleOrDefault(c => c.Id == id);

if (customer is null)
{
    // Handle not found
}

Exceptions are relatively expensive and, more importantly, this makes the code's intent much clearer.

11. finally

finally is primarily used for cleanup.

FileStream? stream = null;

try
{
    stream = File.OpenRead("data.txt");
}
finally
{
    stream?.Dispose();
}

However, modern C# normally prefers:

using FileStream stream = File.OpenRead("data.txt");

or:

using var stream = File.OpenRead("data.txt");

The compiler generates appropriate cleanup behavior.

12. using and IDisposable

You need to understand this relationship.

using var connection = new SqlConnection(connectionString);

Conceptually:

var connection = new SqlConnection(connectionString);

try
{
    // use connection
}
finally
{
    connection.Dispose();
}

Therefore:

using is primarily about deterministic resource cleanup, while exception handling determines how failures are handled.



Exception Propagation:

Suppose:

void MethodA()
{
    MethodB();
}

void MethodB()
{
    MethodC();
}

void MethodC()
{
    throw new Exception("Something went wrong");
}

The exception travels upward:

MethodC()
   ↓
MethodB()
   ↓
MethodA()
   ↓
Caller

If no method catches it, it eventually reaches the application's unhandled-exception boundary.

This is called exception propagation.
Multiple Catch Blocks:
n C#, the main purpose of a catch block is to handle exceptions raised in the try block.
A catch block is executed only when an exception occurs in the program. We can use multiple 
catch blocks with a single try block to handle different types of exceptions. 
Each catch block is designed to handle a specific type of exception.

Note: C# does not allow multiple catch blocks for the same exception type, because it will cause a compile-time error.
The catch blocks are evaluated in the order they appear. If an exception matches the first catch block, the remaining catch blocks are ignored.  

Very important.

try
{
    // code
}
catch (ArgumentNullException ex)
{
    // specific exception
}
catch (ArgumentException ex)
{
    // broader argument exception
}
catch (Exception ex)
{
    // final fallback
}

The order matters.

This is wrong:

catch (Exception ex)
{
}
catch (ArgumentException ex)
{
}

Because ArgumentException will never be reached.

Rule

Specific → General


Exception vs Specific Exceptions

Avoid this:

try
{
    ProcessOrder();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

when you actually know the possible failure.

Prefer:

try
{
    ProcessOrder();
}
catch (InvalidOperationException ex)
{
    // Handle expected business/state problem
}
catch (TimeoutException ex)
{
    // Handle timeout
}

Why?

Because different exceptions often require different recovery strategies.
Basic structure
try
{
    // Code that may throw
    //The try block contains the code that might throw an exception. Only the code inside the try block is monitored for exceptions.
}
catch (Exception ex)
{
    // Handle exception
    //The catch block handles exceptions thrown in the try block. You can have multiple catch blocks to handle different exception types.
}
finally
{
    // Cleanup
    //The finally block is mainly used for cleanup tasks such as closing files, releasing unmanaged resources, or freeing external connections.
    //Even if no exception occurs, the code in finally runs.
    //However, in modern C#, prefer using / await using for disposable resources where applicable.
}

Example:

try
{
    int number = int.Parse("abc");
}
catch (FormatException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("Execution completed.");
}
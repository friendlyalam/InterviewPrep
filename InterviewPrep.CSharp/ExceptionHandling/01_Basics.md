What is an Exception?

An exception is an object representing an abnormal condition that interrupts the normal flow of program execution.

What is Exception Handling?

Exception handling is the mechanism in C# used to detect, handle, and propagate unexpected errors that occur while a program is running.

Example:

int result = 10 / 0;

This causes:

DivideByZeroException

Without handling it, the application can terminate or the request can fail unexpectedly.


Example:

int a = 10;
int b = 0;

int result = a / b;

This throws:

System.DivideByZeroException

Instead of allowing the application to crash unexpectedly, we can handle it:

try
{
    int result = a / b;
}
catch (DivideByZeroException)
{
    Console.WriteLine("Cannot divide by zero.");
}


The core architecture to remember:

                    HTTP Request
                         │
                         ▼
                    Controller
                         │
                         ▼
                     Service
                         │
                         ▼
                   Repository
                         │
                         ▼
                  Exception occurs
                         │
                         ▼
             Global Exception Handler
                    ┌────┴────┐
                    ▼         ▼
                  Log      Translate
                              │
                              ▼
                        HTTP Response

This is the level of C# exception handling worth knowing for a senior .NET/product-company interview.
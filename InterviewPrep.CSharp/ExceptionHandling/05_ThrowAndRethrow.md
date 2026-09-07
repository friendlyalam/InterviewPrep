throw

You must understand the difference between:

throw;

and:

throw ex;

This is a very common interview question.

Correct
catch (Exception ex)
{
    Log(ex);
    throw;
}
Avoid
catch (Exception ex)
{
    Log(ex);
    throw ex;
}

throw; preserves the original stack trace.

throw ex; resets the stack-trace location to the current throw point.

Remember
throw;     → rethrow original exception ✅
throw ex;  → rethrow but damages stack-trace information ❌


The throw keyword is used to manually raise an exception. It can be used to signal that an error or invalid condition has occurred.


int age = 15;

if (age < 18)
{
    throw new ArgumentException("Age must be 18 or older.");
}
Explanation:

throw creates an exception object and passes it up the call stack.
The exception can be caught by a try-catch block in the calling code.
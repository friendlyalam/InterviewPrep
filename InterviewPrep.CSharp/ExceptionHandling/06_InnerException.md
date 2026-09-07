Exception Wrapping / InnerException

Very important in production applications.

Suppose your database layer throws:

SqlException

Your application layer may want to expose a domain/application-specific exception.

try
{
    repository.Save(order);
}
catch (SqlException ex)
{
    throw new OrderPersistenceException(
        "Unable to save the order.",
        ex);
}

The original exception is preserved as:

ex.InnerException

This is called exception chaining/wrapping.
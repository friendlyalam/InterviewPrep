Exception Handling Best Practices:

Do
✔ Catch only when you can handle/recover/translate
✔ Use specific exception types
✔ Preserve stack trace with `throw;`
✔ Log exceptions with structured logging
✔ Use centralized exception handling in Web APIs
✔ Preserve inner exceptions when wrapping
✔ Use custom exceptions for meaningful application conditions
✔ Use using/await using for disposable resources
✔ Return safe error information to clients
✔ Consider retry only for transient failures

Don't
✘ Don't use empty catch blocks
✘ Don't catch Exception everywhere
✘ Don't use exceptions for normal control flow
✘ Don't use throw ex
✘ Don't expose stack traces to API clients
✘ Don't blindly retry every exception
✘ Don't duplicate global error handling in every controller
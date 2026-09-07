38. Product-Company Interview Questions

You should be able to answer these confidently:

Basic
What is exception handling?
What is the difference between throw and throw ex?
What is finally?
Can finally execute when an exception occurs?
Can you have multiple catch blocks?
Why should specific exceptions be caught before general exceptions?
What is an inner exception?
What is a custom exception?
What is exception propagation?
What happens when an exception isn't caught?
Intermediate
When should you create a custom exception?
Why shouldn't exceptions be used for normal control flow?
What is an exception filter?
How does exception handling work with async/await?
What is AggregateException?
How does using relate to finally?
What happens if finally itself throws?
How do you preserve a stack trace?
When should you catch Exception?
How do you log exceptions correctly?
Senior/Product Company
How would you implement global exception handling in ASP.NET Core?
How would you map domain exceptions to HTTP status codes?
How would you prevent internal exception details from reaching clients?
How would you handle transient exceptions in microservices?
When should you retry an exception?
When should you NOT retry?
How does exponential backoff work?
What is a circuit breaker?
How do retries interact with idempotency?
How would you handle failures in RabbitMQ consumers?
What is a poison message?
What is a dead-letter queue?
How do you trace an exception across multiple microservices?
How would you design a centralized exception-handling mechanism?
How would you distinguish business exceptions from infrastructure exceptions?
39. What You Actually Need to Master

For your 10+ years .NET / Microservices / Technical Architect-level preparation, I would divide this topic like this:

🟢 Level 1 — Must Master
Exception fundamentals
Exception hierarchy
try/catch/finally
Multiple catch blocks
throw
throw vs throw ex
Inner exceptions
Custom exceptions
Exception propagation
using / IDisposable
Common .NET exceptions
🟡 Level 2 — Strongly Recommended
Exception filters
Async exception handling
Task.WhenAll
OperationCanceledException
AggregateException
Exception performance
Logging
Structured logging
Sensitive-data protection
Exception anti-patterns
🔴 Level 3 — Product Company / Senior
ASP.NET Core global exception handling
IExceptionHandler
ProblemDetails
Domain vs infrastructure exceptions
HTTP status mapping
Retry
Exponential backoff
Circuit breaker
Timeout
Cancellation
Idempotency
Microservice failure handling
RabbitMQ retry/DLQ
Poison messages
Distributed tracing
Correlation/trace IDs
Observability
Resilience architecture
One principle to remember

A strong engineer doesn't ask:

"Where should I put try/catch?"

They ask:

"Which layer can meaningfully recover from this failure, and how should the failure propagate if it cannot?"
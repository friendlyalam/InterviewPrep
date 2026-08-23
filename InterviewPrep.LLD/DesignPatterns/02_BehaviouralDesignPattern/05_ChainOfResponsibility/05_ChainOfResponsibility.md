1. Category

Chain of Responsibility is a Behavioral Design Pattern.

-------------------------------------------------------------------------------------------------
2. Definition

Chain of Responsibility passes a request through a sequence of handlers, where each handler can either process the request or pass it to the next handler.

The basic structure is:

Request
   ↓
Handler A
   ↓
Handler B
   ↓
Handler C
   ↓
Handler D

Each handler decides:

Can I handle this?
     │
   ┌─┴─┐
  Yes  No
   │    │
Process  ↓
       Next Handler

-------------------------------------------------------------------------------------------------

3. Real-Life Example
Customer Support

Suppose you contact a company's support system.

Customer
   ↓
Level 1 Support
   ↓
Level 2 Support
   ↓
Technical Expert
   ↓
Manager

If Level 1 can solve the problem:

Customer
   ↓
Level 1
   ↓
Solved

If not:

Customer
   ↓
Level 1
   ↓
Level 2
   ↓
Solved

The request moves through the chain.

-------------------------------------------------------------------------------------------------

4. Another Real-Life Example
Approval Process

Imagine an expense approval system:

Employee
   ↓
Team Lead
   ↓
Manager
   ↓
Director
   ↓
VP

A ₹2,000 expense might be approved by the Team Lead.

A large expense might continue:

Team Lead
   ↓
Manager
   ↓
Director

Each handler has a responsibility and a threshold.

-------------------------------------------------------------------------------------------------

5. Product-Company Example

A very useful software example is HTTP middleware.

For example:

HTTP Request
     ↓
Authentication
     ↓
Authorization
     ↓
Logging
     ↓
Validation
     ↓
Controller

Each component can:

process the request
reject it
or pass it forward.

This is why Chain of Responsibility is particularly relevant to ASP.NET Core.

-------------------------------------------------------------------------------------------------

6. Project We'll Build

We'll create:

Expense Approval Pipeline

An employee submits an expense.

Depending on the amount, different approval levels handle it.

Expense Request
       ↓
TeamLeadHandler
       ↓
ManagerHandler
       ↓
DirectorHandler

For example:

₹2,000
   ↓
Team Lead
   ↓
Approved

But:

₹75,000
   ↓
Team Lead
   ↓
Manager
   ↓
Director
   ↓
Approved

This demonstrates the pattern very clearly without creating unnecessary services.

-------------------------------------------------------------------------------------------------

7. Project Structure

We'll keep it compact:

12_ChainOfResponsibility
│
├── Handlers
│   ├── ExpenseHandler.cs
│   ├── TeamLeadHandler.cs
│   ├── ManagerHandler.cs
│   └── DirectorHandler.cs
│
├── Models
│   └── ExpenseRequest.cs
│
├── DependencyInjection
│   └── ServiceCollectionExtensions.cs
│
└── Program.cs

8 files.

No unnecessary:

PaymentService
NotificationService
DatabaseService
Repository
Factory

The pattern itself is the focus.

-------------------------------------------------------------------------------------------------


8. Architecture
                   ExpenseRequest
                         │
                         ▼
                TeamLeadHandler
                    /        \
              handles?       next
                 │             │
                 ▼             ▼
              ManagerHandler
                    /        \
              handles?       next
                 │             │
                 ▼             ▼
             DirectorHandler
                    │
                    ▼
                  Result

  -------------------------------------------------------------------------------------------------
9. Core Concept

Every handler has two responsibilities:

1. Process the request

If it is responsible for that request.

2. Pass it forward

If it cannot process it.

Conceptually:

if (CanHandle(request))
{
    Handle(request);
}
else
{
    _next.Handle(request);
}

That's essentially the heart of the pattern.

-------------------------------------------------------------------------------------------------

10. Advantages
✅ Reduced Coupling

The sender doesn't need to know which handler will process the request.

✅ Flexible Chain

Handlers can be added or reordered.

✅ Single Responsibility

Each handler has one responsibility.

✅ Good for Pipelines

Excellent for:

middleware
validation
authorization
approval workflows
request processing

-------------------------------------------------------------------------------------------------

11. Disadvantages
❌ Request May Go Through Many Handlers

This can increase processing time.

❌ Handler Might Not Handle It

You need a strategy for an unhandled request.

❌ Debugging Can Be Harder

You may need to trace the entire chain.

❌ Chain Ordering Matters

For example:

Authorization
   ↓
Validation

is different from:

Validation
   ↓
Authorization
12. When to Use

Use it when:

✅ Multiple objects can handle a request
✅ Handler selection should be dynamic
✅ Processing order matters
✅ You want a pipeline
✅ You want to add/remove handlers easily

-------------------------------------------------------------------------------------------------
13. When NOT to Use

Avoid it when:

❌ Only one class can ever handle the request
❌ The processing order doesn't matter
❌ A simple method call is enough
❌ The chain becomes unnecessarily complicated

-------------------------------------------------------------------------------------------------
14. Chain of Responsibility vs Mediator

This is important because we just completed Mediator.

Chain of Responsibility
A → B → C → D

The request moves sequentially through handlers.

Mediator
Request
   ↓
Mediator
   ↓
Specific Handler

The mediator determines which handler should receive the request.

Simple interview answer:

Chain of Responsibility passes a request through a chain of possible handlers,
whereas Mediator centralizes communication and dispatches requests between components.

-------------------------------------------------------------------------------------------------

15. Chain of Responsibility vs Strategy
Strategy

Choose one algorithm.

Payment
 ↓
Strategy A
OR
Strategy B
OR
Strategy C
Chain of Responsibility

Try handlers sequentially.

Request
 ↓
A
 ↓
B
 ↓
C

This distinction is frequently useful in design discussions.

-------------------------------------------------------------------------------------------------

16. Chain of Responsibility in ASP.NET Core

One of the best practical examples for you is middleware:

Request
   ↓
Exception Middleware
   ↓
Authentication Middleware
   ↓
Authorization Middleware
   ↓
Logging Middleware
   ↓
Endpoint

Each middleware can:

Process
   ↓
Call next

or stop the pipeline.

So when asked:

"Where have you seen Chain of Responsibility in .NET?"

A strong answer is:

"ASP.NET Core middleware is a practical pipeline-style example of Chain of Responsibility. 
Each middleware can process the request and decide whether to invoke the next middleware."

That's much better than just giving a textbook definition.

Implementation Plan

We'll implement it in this order:

Step 1
ExpenseRequest
ExpenseHandler base abstraction

        ↓

Step 2
TeamLeadHandler
ManagerHandler
DirectorHandler

        ↓

Step 3
Build the chain

        ↓

Step 4
DI registration

        ↓

Step 5
Program.cs

        ↓

Step 6
Run multiple expense scenarios

        ↓

Step 7
Interview questions + senior-level tips

Next step: ExpenseRequest and the base ExpenseHandler abstraction.
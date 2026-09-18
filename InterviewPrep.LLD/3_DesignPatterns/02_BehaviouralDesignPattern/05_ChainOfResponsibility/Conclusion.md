2. What Happens for ₹5,000?
Expense ₹5,000
      ↓
Team Lead
      ↓
Can approve?
      ↓
YES
      ↓
APPROVED

The request stops immediately.

Manager and Director aren't called.

3. What Happens for ₹30,000?
Expense ₹30,000
      ↓
Team Lead
      │
      └── Cannot approve
              ↓
          Manager
              │
              └── Can approve
                      ↓
                   APPROVED
4. What Happens for ₹80,000?
Expense ₹80,000
      ↓
Team Lead
      ↓
Cannot approve
      ↓
Manager
      ↓
Cannot approve
      ↓
Director
      ↓
Can approve
      ↓
APPROVED
5. What Happens for ₹150,000?
Expense ₹150,000
      ↓
Team Lead
      ↓
Cannot approve
      ↓
Manager
      ↓
Cannot approve
      ↓
Director
      ↓
Cannot approve
      ↓
REJECTED
6. Expected Output

You should get output similar to:

==========================================
     CHAIN OF RESPONSIBILITY DEMO
==========================================

------------------------------------------
Employee    : 101
Amount      : ₹5,000
Description : Office supplies
------------------------------------------
Result      : Team Lead approved expense of ₹5,000.

------------------------------------------
Employee    : 102
Amount      : ₹30,000
Description : Business travel
------------------------------------------
Result      : Manager approved expense of ₹30,000.

------------------------------------------
Employee    : 103
Amount      : ₹80,000
Description : Client event
------------------------------------------
Result      : Director approved expense of ₹80,000.

------------------------------------------
Employee    : 104
Amount      : ₹150,000
Description : Conference
------------------------------------------
Result      : Expense rejected because it exceeds the approval limit.
7. Final Architecture

This is the most important diagram to remember for the interview:

                         Program
                            │
                            ▼
                     ExpenseHandler
                            │
                            ▼
                    TeamLeadHandler
                    Limit = ₹10K
                            │
                      cannot handle
                            │
                            ▼
                     ManagerHandler
                    Limit = ₹50K
                            │
                      cannot handle
                            │
                            ▼
                    DirectorHandler
                   Limit = ₹100K
                            │
                            ▼
                         Result

The caller knows only:

ExpenseHandler

It does not need to know:

TeamLeadHandler
ManagerHandler
DirectorHandler

That's the decoupling provided by the pattern.

8. Final Project — 8 Files ✅
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

Exactly 8 files, as requested.

Chain of Responsibility — Implementation Complete ✅

Before moving to the next pattern, you should understand this one key sentence:

Chain of Responsibility allows a request to pass through a sequence of handlers until one handler processes it or the chain ends.

And for your .NET interviews, remember:

ASP.NET Core Middleware
        ↓
Pipeline
        ↓
Chain of Responsibility concept



-----------------------------------------------------------------------------------------------

Chain of Responsibility — Interview & Product-Company Section

The implementation is complete. Now let's focus on what you should actually be able to explain in a Microsoft/Amazon/Google-level interview.

1. Best Interview Definition

If the interviewer asks:

What is Chain of Responsibility?

Say:

"Chain of Responsibility is a behavioral design pattern where a request is passed through a sequence of handlers. Each handler decides whether it can process the request; if it cannot, it forwards the request to the next handler in the chain."

That's the core definition.

2. What Problem Does It Solve?

Without the pattern, you might write:

if (amount <= 10000)
{
    // Team Lead
}
else if (amount <= 50000)
{
    // Manager
}
else if (amount <= 100000)
{
    // Director
}
else
{
    // Reject
}

This works.

But imagine the rules grow:

Team Lead
Manager
Senior Manager
Director
VP
CFO
Compliance
Regional Approval

Your if/else becomes difficult to maintain.

With Chain of Responsibility:

Request
   ↓
Handler 1
   ↓
Handler 2
   ↓
Handler 3
   ↓
Handler 4

Each handler owns its own rule.

3. The Most Important Advantage
Open/Closed Principle

Suppose we add:

SeniorDirectorHandler

We don't necessarily need to modify existing handlers.

We can insert:

Team Lead
    ↓
Manager
    ↓
Senior Director
    ↓
Director

This supports the Open/Closed Principle:

Software entities should be open for extension but closed for modification.

4. Chain of Responsibility + SOLID
Single Responsibility

Each handler has one approval responsibility.

TeamLeadHandler
    ↓
Team Lead approval
Open/Closed

New handlers can be added.

Dependency Inversion

The caller depends on:

ExpenseHandler

rather than concrete handlers.

5. Chain of Responsibility vs Strategy

This is a very important interview comparison.

Strategy

You choose one strategy.

             Request
                │
        ┌───────┼───────┐
        ▼       ▼       ▼
    Strategy A B      C

Example:

Payment calculation
   ↓
CreditCardStrategy
OR
UPIStrategy
OR
BankTransferStrategy
Chain

You can execute multiple handlers sequentially.

Request
   ↓
Handler A
   ↓
Handler B
   ↓
Handler C
Easy way to remember

Strategy = choose one.
Chain = try/pass through multiple.

6. Chain of Responsibility vs Mediator

You just learned Mediator, so this comparison is especially important.

Chain

Communication follows a chain:

A → B → C → D

The request moves from one handler to another.

Mediator

Communication goes through a central coordinator:

A ──┐
B ──┼──► Mediator
C ──┘
Interview answer

"Chain of Responsibility passes a request sequentially through potential handlers, while Mediator centralizes communication and coordinates interactions between participants."

7. Chain of Responsibility vs Command

These are also frequently confused.

Command

Encapsulates an operation as an object.

CreateOrderCommand
       ↓
CreateOrderHandler
Chain

Determines who gets the opportunity to handle a request.

Request
 ↓
Handler A
 ↓
Handler B
 ↓
Handler C

They can actually be combined.

For example:

Command
   ↓
Mediator
   ↓
Validation Chain
   ↓
Command Handler
8. Real .NET Example — Middleware

This is the example I want you to remember for interviews.

ASP.NET Core request pipeline:

HTTP Request
      ↓
Exception Middleware
      ↓
Logging Middleware
      ↓
Authentication Middleware
      ↓
Authorization Middleware
      ↓
Endpoint

Each middleware can:

Process request
      ↓
Call next()

or terminate the request.

For example:

public async Task InvokeAsync(
    HttpContext context,
    RequestDelegate next)
{
    // Process request

    await next(context);
}

The next delegate represents the next component in the pipeline.

9. Interview Question
"Is ASP.NET Core middleware exactly the GoF Chain of Responsibility pattern?"

A strong answer:

"It's a very close pipeline-style implementation of the Chain of Responsibility idea, although ASP.NET Core middleware has its own pipeline-specific abstractions and execution model. I would describe it as a practical example of the pattern rather than claiming the framework is simply a textbook GoF implementation."

That's a more precise senior-level answer.

10. What Happens If Nobody Handles the Request?

This is an important design question.

Possible approaches:

Option 1 — Return failure
Request
 ↓
A
 ↓
B
 ↓
C
 ↓
Not handled
Option 2 — Throw an exception

Useful when an unhandled request indicates a programming/configuration error.

Option 3 — Default handler
A → B → C → DefaultHandler

For our project, we chose a controlled rejection.

11. What If the Chain Becomes Very Long?

Potential problem:

A → B → C → D → E → F → G → H → I

Every request might traverse many handlers.

Possible solutions:

Keep the chain focused.
Stop processing as soon as possible.
Avoid unnecessary handlers.
Use a more appropriate architecture if routing becomes complex.
Consider parallel/event-based processing if sequential handling isn't required.
12. Can Handlers Execute More Than Once?

Yes, depending on implementation.

For example:

Handler A
   ↓
does something
   ↓
Handler B
   ↓
does something
   ↓
Handler C

Unlike a strict "one handler wins" implementation, a pipeline can intentionally allow multiple handlers to process the same request.

This is common in middleware-style designs.

So Chain of Responsibility has two common styles:

One handler handles
A → B → C
    ↑
  stops here
Multiple handlers participate
A → B → C → D

Each performs processing and forwards the request.

13. When Should You Use It?

Use it when:

✅ Multiple handlers may handle a request
✅ Handler selection should be dynamic
✅ Processing order matters
✅ You have a pipeline
✅ You want handlers to remain independent

Typical examples:

ASP.NET middleware
Validation pipeline
Authorization
Approval workflow
Exception processing
Request processing
Logging pipeline
14. When Should You NOT Use It?

Don't use it just because you have several if statements.

Avoid it when:

❌ There is only one possible handler
❌ The chain is always fixed and trivial
❌ Routing is better represented by a simple map/dictionary
❌ The chain becomes difficult to understand
❌ The added abstraction provides no benefit

A senior engineer should be able to say:

"A design pattern isn't automatically better than straightforward code."

15. Product-Company Scenario Question
Interviewer:

"We have 20 validation rules. Would you use Chain of Responsibility?"

Good answer:

"Possibly, if the validation rules form an ordered pipeline and each rule can independently decide whether to continue or terminate processing. However, I would first evaluate whether a simple validation pipeline or composable validators would be clearer. I wouldn't introduce Chain of Responsibility solely because there are many rules."

That's the type of answer that demonstrates design judgment, not pattern memorization.

16. Another Scenario
Interviewer:

"We have authentication, authorization, logging and rate limiting. Which pattern?"

Don't immediately say:

"Chain of Responsibility."

Instead say:

"A pipeline is a natural fit because each component can process the request and decide whether to continue. In ASP.NET Core, middleware provides exactly this kind of pipeline. Conceptually, it aligns closely with Chain of Responsibility."

Much stronger.

17. Whiteboard Explanation

If you have only 2 minutes, draw this:

              Request
                 │
                 ▼
          ┌─────────────┐
          │  Handler A  │
          └──────┬──────┘
                 │ can't handle
                 ▼
          ┌─────────────┐
          │  Handler B  │
          └──────┬──────┘
                 │ can't handle
                 ▼
          ┌─────────────┐
          │  Handler C  │
          └──────┬──────┘
                 │
                 ▼
              Result

Then say:

"Each handler either processes the request or forwards it to the next handler."

That's enough to establish the concept.

18. Questions You Should Be Able to Answer

Before considering this pattern fully learned, make sure you can answer:

What is Chain of Responsibility?
What problem does it solve?
Why is it behavioral?
How is the chain constructed?
What happens if no handler handles the request?
Can multiple handlers process the request?
Chain vs Strategy?
Chain vs Mediator?
Chain vs Command?
How does ASP.NET Core middleware relate to it?
What are its disadvantages?
How does it support Open/Closed Principle?
When would you avoid it?
Can a chain become a performance problem?
How would you dynamically configure the chain?

If you can answer those and explain our 8-file project on a whiteboard, you're in good shape for this pattern.
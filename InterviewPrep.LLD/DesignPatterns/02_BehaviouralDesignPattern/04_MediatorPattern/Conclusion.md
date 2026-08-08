2. Expected Output

You should see something similar to:

======================================
       MEDIATOR PATTERN DEMO
======================================

Submitting valid leave request...
--------------------------------------
Approved : True
Message  : Leave approved for employee 101.

Submitting invalid leave request...
--------------------------------------
Approved : False
Message  : Leave request cannot exceed 10 days.

======================================
          DEMO COMPLETED
======================================
3. What Actually Happened?

When we execute:

LeaveResult validResult =
    await mediator.SendAsync(validRequest);

Program.cs doesn't know:

LeaveRequestHandler

It only knows:

IMediator

The mediator receives:

LeaveRequest

and determines:

LeaveRequest
       ↓
IRequestHandler<LeaveRequest, LeaveResult>
       ↓
LeaveRequestHandler

Then the handler executes:

HandleAsync(...)

and returns:

LeaveResult
4. Final Architecture
                       Program.cs
                           │
                           │
                           ▼
                      IMediator
                           │
                           ▼
                        Mediator
                           │
                           │ resolves
                           ▼
             IRequestHandler<LeaveRequest,
                              LeaveResult>
                           │
                           ▼
                 LeaveRequestHandler
                           │
                           ▼
                     LeaveResult

This is the core Mediator Pattern.

5. Final Project Structure
11_MediatorPattern
│
├── Requests
│   └── LeaveRequest.cs
│
├── Handlers
│   └── LeaveRequestHandler.cs
│
├── Interfaces
│   ├── IRequest.cs
│   ├── IRequestHandler.cs
│   └── IMediator.cs
│
├── Mediator
│   └── Mediator.cs
│
├── Models
│   └── LeaveResult.cs
│
├── DependencyInjection
│   └── ServiceCollectionExtensions.cs
│
└── Program.cs

Only 8 files. This is the size we should target for your future pattern demonstrations unless a pattern genuinely requires more.

6. Complete Flow in One Diagram
              LeaveRequest
                    │
                    ▼
             ┌─────────────┐
             │  Mediator   │
             └──────┬──────┘
                    │
                    ▼
        ┌──────────────────────┐
        │ LeaveRequestHandler   │
        └──────────┬───────────┘
                   │
                   ▼
              LeaveResult

The key benefit:

Program
   │
   │ does NOT know
   ▼
LeaveRequestHandler

It only knows:

Program
   ↓
IMediator

That's the decoupling we're demonstrating.

🎯 Mediator Pattern — Interview Section

Now, as you requested, all interview-focused material comes after Program.cs.

1. Strong Interview Definition

If Microsoft/Amazon/Google asks:

What is the Mediator Pattern?

Say:

"Mediator is a behavioral design pattern that encapsulates communication between multiple objects through a central mediator, 
reducing direct dependencies between those objects. In a request-handler architecture, the mediator receives a request and dispatches it to the appropriate handler."

That's a much stronger answer than simply saying:

"Mediator reduces coupling."

2. The Problem It Solves

Without Mediator:

A ──► B
A ──► C
A ──► D

B ──► C
B ──► D

C ──► D

Dependencies grow rapidly.

With Mediator:

A ──┐
B ──┤
C ──┼──► Mediator
D ──┘

Communication becomes centralized.

3. Main Components

Remember these four:

Component	Responsibility
Request	Represents an operation/request
Handler	Executes the request
Mediator	Routes request to handler
Participant	Communicates through mediator

In our project:

LeaveRequest
LeaveRequestHandler
Mediator
Program
4. Advantages
Loose coupling

Participants don't directly depend on each other.

Single responsibility

Communication is separated from individual components.

Testability

Handlers can be tested independently.

Extensibility

Adding another request can be done without modifying existing callers.

CQRS compatibility

Mediator works naturally with command/query handlers.

5. Disadvantages
Additional abstraction

A simple call becomes:

Caller
 ↓
Mediator
 ↓
Handler
More classes

Every request can introduce another handler.

Runtime indirection

The handler may be resolved dynamically.

God Mediator risk

If business logic is placed inside the mediator, the mediator can become a central bottleneck.

6. When Should You Use It?

Good scenarios:

✔ CQRS
✔ Request/handler architecture
✔ Complex object communication
✔ Many interacting components
✔ Decoupling application components
✔ Large applications with many operations
7. When Should You NOT Use It?

Don't use it simply because:

"It's an enterprise pattern."

Avoid it when:

✘ Application is tiny
✘ Communication is already simple
✘ Only two objects interact
✘ The abstraction doesn't solve a real problem

This is an important senior-level answer.

8. Mediator vs Command

This is very likely to come up because you just studied Command.

Command
"What should we do?"

Example:

CreateOrderCommand
Mediator
"Who should handle this request?"

Example:

Mediator
   ↓
CreateOrderCommandHandler

They can be used together:

API
 ↓
Mediator
 ↓
Command
 ↓
CommandHandler
9. Mediator vs Observer
Mediator
A → Mediator → B

Central coordination.

Observer
Publisher
    ↓
 Event
 ┌──┼──┐
 ▼  ▼  ▼
 A  B  C

One-to-many notification.

Interview answer

"Mediator centralizes communication and coordination, whereas Observer establishes a publish-subscribe relationship where subscribers are notified when an event occurs."

10. Mediator vs Facade
Facade

Simplifies access to a complex subsystem.

Client
  ↓
Facade
  ↓
Subsystem
Mediator

Coordinates communication between participants.

A
 ↓
Mediator
 ↓
B
11. Mediator vs Dependency Injection

These are not alternatives.

DI answers:

How do I provide dependencies?

Mediator answers:

How do components communicate without directly depending on each other?

Our project uses both:

DI
 ↓
provides Mediator + Handler

Mediator
 ↓
routes Request → Handler
12. Mediator + CQRS

This is particularly important for your product-company preparation.

                   API
                    │
                    ▼
                 Mediator
                 /      \
                /        \
               ▼          ▼
          Command        Query
             │              │
             ▼              ▼
          Handler         Handler
             │              │
             ▼              ▼
          Write DB        Read DB

For example:

CreateOrderCommand
GetOrderQuery
CancelOrderCommand

Each can have its own handler.

13. What Is MediatR?

A common interview question:

"Have you used MediatR?"

Correct conceptual answer:

"MediatR is a .NET library that provides mediator-style request and notification dispatching. The underlying architectural idea is the Mediator pattern."

Don't confuse:

Mediator = design pattern
MediatR = library
14. Why Did We Build Our Own Mediator?

Because simply doing:

await _mediator.Send(request);

using a library doesn't demonstrate that you understand the pattern.

Our implementation shows:

Request
   ↓
Mediator
   ↓
Handler resolution
   ↓
Handler execution
   ↓
Result

That's what you should be able to draw on a whiteboard.

15. Important Product-Company Question
"Would you build your own mediator in production?"

A strong answer:

"Usually no. In a .NET application, I would generally use an established library such as MediatR or implement a simpler application-specific dispatcher if the requirements are limited. I would avoid building custom reflection-based infrastructure unless there is a clear requirement for it."

That's a much better senior-level answer.

16. Another Important Question
"Can Mediator become a bottleneck?"

Answer:

"The mediator itself should generally remain a thin dispatcher. The bigger risk is turning it into a God object by putting business logic and orchestration for unrelated operations inside it. Keeping request handling in dedicated handlers avoids that problem."

17. Your 20-Minute Interview Explanation

If asked to explain Mediator with an example:

Step 1 — Problem
Many objects directly communicate
        ↓
High coupling
Step 2 — Solution
Introduce Mediator
Step 3 — Architecture
Request
   ↓
Mediator
   ↓
Handler
   ↓
Result
Step 4 — Explain our example

"I created an employee leave request. The caller only depends on IMediator. The mediator resolves the appropriate 
IRequestHandler<LeaveRequest, LeaveResult> through dependency injection and dispatches the request to LeaveRequestHandler. This keeps the caller decoupled from the concrete handler."

Step 5 — Mention production

"In a real .NET application, this can be implemented using MediatR, particularly when combined with CQRS."
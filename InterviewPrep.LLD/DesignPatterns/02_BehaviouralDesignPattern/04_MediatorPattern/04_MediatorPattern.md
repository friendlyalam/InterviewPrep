2. Definition

Mediator Pattern defines a central object that encapsulates how multiple objects communicate with each other, reducing direct coupling between them.

The Mediator design pattern is a behavioral pattern that reduces coupling between 
components by forcing them to communicate indirectly through a central mediator object.
Instead of classes talking to each other directly (creating complex dependencies),
they send messages to the mediator, which routes them appropriately.

The key idea is:

Without Mediator

Object A ─────► Object B
Object A ─────► Object C
Object B ─────► Object C
Object C ─────► Object A

As the application grows, objects become tightly coupled.

With Mediator:

             Mediator
            /   |   \
           ▼    ▼    ▼
        Object A B   C

Objects communicate through the mediator instead of directly communicating with each other.

-----------------------------------------------------------------------------------------------------

3. Real-Life Example
Air Traffic Controller

Planes don't individually coordinate with every other plane.

Instead:

Plane A ──┐
Plane B ──┼──► Air Traffic Controller
Plane C ──┤
Plane D ──┘

The controller coordinates the interaction.

The Air Traffic Controller = Mediator.

-----------------------------------------------------------------------------------------------------

4. Software Example

Imagine several UI components:

Login Form
   ↓
User Service
   ↓
Dashboard
   ↓
Notification

If every component directly communicates with every other component:

Login → Dashboard
Login → Notification
Dashboard → Login
Dashboard → User
Notification → Dashboard
...

the dependencies grow rapidly.

Mediator centralizes that communication.

-----------------------------------------------------------------------------------------------------

5. Why Do We Need Mediator?

Suppose we have:

Employee
Manager
HR

Without Mediator:

Employee → Manager
Employee → HR
Manager → Employee
Manager → HR
HR → Employee
HR → Manager

This creates many dependencies.

With Mediator:

Employee ──┐
Manager  ──┼──► Mediator
HR       ──┘

Now the participants don't need to know about each other directly.

-----------------------------------------------------------------------------------------------------

6. Our Project

We'll build:

Employee Leave Approval System

An employee submits a leave request.

The request goes through a mediator to the appropriate handler.

Employee
   ↓
Leave Request
   ↓
Mediator
   ↓
Leave Request Handler
   ↓
Leave Result

That's all.

We do not need:

PaymentService ❌
NotificationService ❌
InventoryService ❌
ShippingService ❌
Database ❌

Those don't help us understand Mediator.

-----------------------------------------------------------------------------------------------------

7. Project Goal

Our application should allow:

Employee submits leave request
             ↓
         Mediator
             ↓
     LeaveRequestHandler
             ↓
       Validate request
             ↓
        Approve/Reject
             ↓
          Result

For example:

EmployeeId: 101
Days: 3
Reason: Family function

Result:

Leave approved

-----------------------------------------------------------------------------------------------------

8. Project Structure

We'll keep it small:

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

Only 8 files.

-----------------------------------------------------------------------------------------------------

9. Architecture
                    Program.cs
                        │
                        ▼
                     IMediator
                        │
                        ▼
                  LeaveRequest
                        │
                        ▼
                 LeaveRequestHandler
                        │
                        ▼
                   LeaveResult

The important relationship is:

Program
   │
   │ knows
   ▼
IMediator

Program
   │
   │ does NOT directly know
   ▼
LeaveRequestHandler

The mediator handles the routing.

-----------------------------------------------------------------------------------------------------

10. Main Components

We have four important concepts.

1. Request

Represents what we want to do.

LeaveRequest

2. Handler

Contains the logic to process the request.

LeaveRequestHandler
3. Mediator

Finds the appropriate handler and invokes it.

Mediator
4. Result

Represents the outcome.

LeaveResult

-----------------------------------------------------------------------------------------------------
11. Advantages
1. Loose Coupling

Objects don't need direct references to each other.

2. Centralized Communication

Communication is coordinated through one abstraction.

3. Easier Maintenance

Adding another request doesn't require modifying every participant.

4. Better Testability

Handlers can be tested independently.

5. Works Well With CQRS

A very common architecture is:

Controller
    ↓
Mediator
    ↓
Command
    ↓
Handler
6. Single Responsibility

The request, mediator, and handler have separate responsibilities.

-----------------------------------------------------------------------------------------------------

12. Disadvantages
1. Additional Complexity

Instead of:

Service.Method()

you may have:

Mediator
 ↓
Request
 ↓
Handler
2. More Classes

Each request may have its own handler.

3. Debugging Can Be Less Direct

You have to follow the mediator's dispatch path.

4. Mediator Can Become a God Object

If you put business logic inside the mediator, you've defeated the purpose.

The mediator should primarily coordinate/dispatch, not become the application's business layer.

-----------------------------------------------------------------------------------------------------

13. When Should We Use It?

Use Mediator when:

✅ Many objects communicate with each other
✅ Direct dependencies are becoming complicated
✅ You want request/handler architecture
✅ You are using CQRS
✅ You have many independent operations
✅ You want centralized request dispatching

-----------------------------------------------------------------------------------------------------
14. When Should We NOT Use It?

Don't introduce Mediator simply because it is a design pattern.

Avoid it when:

❌ Very small application
❌ Only two objects communicate
❌ Direct dependency is simple and clear
❌ Mediator adds more code than value

For example:

customer.GetName();

doesn't need:

GetCustomerNameRequest
GetCustomerNameHandler
Mediator

That would be overengineering.

-----------------------------------------------------------------------------------------------------

15. Mediator vs Command

Since we just completed Command Pattern, this distinction is very important.

Command

Represents the request.

CreateOrderCommand

It answers:

What operation do we want to perform?

Mediator

Routes the request to its handler.

Mediator
   ↓
CreateOrderCommandHandler

It answers:

Who should handle this request?

They are often used together.

Controller
    ↓
Mediator
    ↓
Command
    ↓
CommandHandler

-----------------------------------------------------------------------------------------------------
16. Mediator vs Observer
Mediator

Communication is generally:

A → Mediator → B

The mediator actively coordinates the interaction.

Observer

Communication is:

Publisher
   ↓
Event
   ↓
Subscriber A
Subscriber B
Subscriber C

Observers are notified when something happens.

Simple distinction:

Mediator coordinates communication. Observer broadcasts notifications.

-----------------------------------------------------------------------------------------------------

17. Mediator vs Facade

Another important interview distinction.

Facade

Provides a simplified interface to a complex subsystem.

Client
  ↓
Facade
  ↓
Subsystem A
Subsystem B
Subsystem C


Mediator

Coordinates communication between participants.

Participant A
      ↓
   Mediator
      ↓
Participant B

-----------------------------------------------------------------------------------------------------
18. Mediator in .NET

In real .NET applications, you may encounter:

MediatR

The important distinction:

Mediator
   ↓
Design Pattern

while:

MediatR
   ↓
.NET library implementing mediator-style request/notification
      handling

Don't say:

"Mediator Pattern is MediatR."

Instead say:

"MediatR is a library commonly used to implement mediator-style communication in .NET applications."

-----------------------------------------------------------------------------------------------------

19. Mediator + CQRS

This is one of the most important enterprise connections.

                 API
                  │
             ┌────┴────┐
             ▼         ▼
          Command     Query
             │         │
             ▼         ▼
          Handler    Handler
             │         │
             ▼         ▼
         Write DB    Read DB

Mediator can sit between the API and these handlers:

API
 │
 ▼
Mediator
 │
 ├──► CreateOrderCommandHandler
 │
 ├──► CancelOrderCommandHandler
 │
 └──► GetOrderQueryHandler

This is why Mediator is relevant to product-company architecture discussions.

-----------------------------------------------------------------------------------------------------

20. Our 20-Minute Interview Project

We will demonstrate:

Employee
   ↓
LeaveRequest
   ↓
IMediator
   ↓
Mediator
   ↓
IRequestHandler
   ↓
LeaveRequestHandler
   ↓
LeaveResult

The project will contain:

IRequest

Generic request contract.

IRequestHandler

Generic handler contract.

IMediator

Mediator contract.

Mediator

Actual request-to-handler dispatch.

LeaveRequest

Request object.

LeaveRequestHandler

Business operation.

LeaveResult

Result.

ServiceCollectionExtensions

DI registration.

Program.cs

Complete execution.

-----------------------------------------------------------------------------------------------------

21. Implementation Sequence

We will now build it in this exact order:

STEP 1
IRequest
IRequestHandler
IMediator
LeaveRequest
LeaveResult

        ↓

STEP 2
LeaveRequestHandler

        ↓

STEP 3
Mediator

        ↓

STEP 4
Dependency Injection

        ↓

STEP 5
Program.cs

        ↓

STEP 6
Run complete project
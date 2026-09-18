Expected Output

You should get something similar to:

======================================
      COMMAND PATTERN DEMO
======================================

Creating Order...
--------------------------------------
Success : True
Message : Order created successfully.
Order ID : 4b2...
Amount   : ₹2,998.00
Status   : Created

Cancelling Order...
--------------------------------------
Success : True
Message : Order cancelled successfully.
Order ID : 4b2...
Status   : Cancelled

Processing Refund...
--------------------------------------
Success : True
Message : Refund of ₹2,998.00 processed successfully.

======================================
          DEMO COMPLETED
======================================

The actual Guid will obviously be different.

Complete Flow

Now you have the complete Command Pattern:

                         Program.cs
                             │
                             ▼
                    CreateOrderCommand
                             │
                             ▼
              CreateOrderCommandHandler
                             │
                             ▼
                       IOrderService
                             │
                             ▼
                       OrderService
                             │
                             ▼
                    IOrderRepository
                             │
                             ▼
                    OrderRepository

Then:

CancelOrderCommand
        ↓
CancelOrderCommandHandler
        ↓
IOrderService
        ↓
OrderService
        ↓
IOrderRepository

And:

RefundOrderCommand
        ↓
RefundOrderCommandHandler
        ↓
IPaymentService
        ↓
PaymentService
One Important Improvement

For a real ASP.NET Core application, you normally wouldn't manually call:

GetRequiredService<ICommandHandler<...>>()

from application code.

The controller/application endpoint would receive the appropriate abstraction, or a mediator such as MediatR would dispatch the command.

We're doing it here because this is a standalone 20-minute console demonstration of the pattern.

✅ Command Pattern Project Completed

Your final structure is:

10_CommandPattern
│
├── Commands
│   ├── CreateOrderCommand.cs
│   ├── CancelOrderCommand.cs
│   └── RefundOrderCommand.cs
│
├── Handlers
│   ├── CreateOrderCommandHandler.cs
│   ├── CancelOrderCommandHandler.cs
│   └── RefundOrderCommandHandler.cs
│
├── Interfaces
│   ├── ICommand.cs
│   ├── ICommandHandler.cs
│   ├── IOrderService.cs
│   ├── IOrderRepository.cs
│   └── IPaymentService.cs
│
├── Models
│   ├── Order.cs
│   └── CommandResult.cs
│
├── Services
│   ├── OrderService.cs
│   └── PaymentService.cs
│
├── Repositories
│   └── OrderRepository.cs
│
├── DependencyInjection
│   └── ServiceCollectionExtensions.cs
│
└── Program.cs
🎯 Now — Interview Preparation

You asked me to provide the interview material after Program.cs, so here is the complete interview section.

1. Definition

Command Pattern encapsulates a request as an object, allowing the sender of the request to be decoupled from the object that executes it.

Our implementation:

Command
   ↓
Handler
   ↓
Service
2. What Problem Does It Solve?

Without Command:

Controller
   ↓
OrderService

As operations increase:

Controller
 ├── CreateOrder()
 ├── CancelOrder()
 ├── RefundOrder()
 ├── ShipOrder()
 ├── UpdateOrder()
 └── ...

Command gives us:

CreateOrderCommand
CancelOrderCommand
RefundOrderCommand
ShipOrderCommand

Each request becomes a separate object.

3. Advantages
1. Loose Coupling

Sender doesn't know implementation details.

2. Supports Queuing

Commands can be sent to:

RabbitMQ
Kafka
Azure Service Bus
Amazon SQS
3. Supports Retry

A failed command can be retried.

4. Supports Logging/Auditing

You can record:

Command
User
Timestamp
CorrelationId
Result
5. Supports Undo/Redo

Commands can optionally carry information needed to reverse operations.

6. Testability

Handlers can be tested independently.

7. CQRS Compatibility

Commands naturally fit the CQRS model.

4. Disadvantages
1. More Classes

Instead of one method:

Command
Handler
Service
2. More Indirection
Controller
 ↓
Command
 ↓
Handler
 ↓
Service
3. Overengineering

For:

customer.GetName();

creating:

GetCustomerNameCommand
GetCustomerNameHandler

would be excessive.

4. Debugging Can Be More Difficult

The execution path is longer.

5. When to Use

Use Command when you have:

✅ CQRS
✅ Complex operations
✅ Background processing
✅ Queue-based processing
✅ Retry requirements
✅ Audit requirements
✅ Scheduling
✅ Undo/Redo
✅ Workflow processing
6. When NOT to Use

Avoid it for:

❌ Simple CRUD operations
❌ Tiny applications
❌ Simple getters
❌ Trivial business logic
❌ Situations where abstraction provides no benefit
7. Command vs Strategy

This is an important interview question.

Command	Strategy
Represents a request	Represents an algorithm
What should happen?	How should it happen?
Can be queued	Usually executed directly
RefundOrderCommand	CreditCardRefundStrategy

Example:

RefundOrderCommand
       ↓
RefundHandler
       ↓
RefundStrategy
       ↓
Payment Gateway

They can work together.

8. Command vs Observer
Command
→ "Do something."

Observer/Event
→ "Something happened."

Example:

CancelOrderCommand
       ↓
CancelOrderHandler
       ↓
Order cancelled
       ↓
OrderCancelledEvent
       ↓
Email
Inventory
Audit

This combination is very common conceptually in enterprise systems.

9. Command vs Mediator

These are not the same pattern.

Command

Represents the request:

CreateOrderCommand
Mediator

Coordinates communication between the sender and handler:

Controller
   ↓
Mediator
   ↓
CreateOrderCommandHandler

MediatR is a popular .NET library implementing mediator-style request dispatching.

10. Command + CQRS

This is probably your most important interview connection.

                CQRS
                 │
        ┌────────┴────────┐
        ▼                 ▼
     Command            Query
        │                 │
        ▼                 ▼
    Handler            Handler
        │                 │
        ▼                 ▼
   Write Model        Read Model

Command:

CreateOrderCommand

Query:

GetOrderQuery

Commands change state.

Queries retrieve state.

11. Senior Interview Question
"Would you use Command Pattern everywhere?"

Strong answer:

No. I would use it when the request needs independent handling, queuing, retry, auditing, scheduling, CQRS, or complex workflows. For simple operations, a direct service call is often cleaner and avoids unnecessary abstraction.

That's a much stronger answer than saying:

"Command Pattern improves flexibility."

12. Product-Company Architecture

A mature architecture can evolve from:

API
 ↓
Command
 ↓
Handler
 ↓
Domain Service
 ↓
Repository

to:

API
 ↓
MediatR
 ↓
Command
 ↓
Handler
 ↓
Domain
 ↓
Repository
 ↓
Database

and for asynchronous processing:

API
 ↓
Command
 ↓
Message Broker
 ↓
Worker
 ↓
Command Handler
 ↓
Domain
 ↓
Database
13. Common Mistakes
❌ Business logic inside Command

Command should represent the request.

❌ Handler becoming a God class

Keep business rules in appropriate domain/service components.

❌ Creating dependencies with new

Use DI.

❌ Using Command for every method

Avoid unnecessary abstraction.

❌ Confusing Command with Event

Command:

"Please do this."

Event:

"This already happened."

14. Your 20-Minute Interview Explanation

If the interviewer says:

"Explain a practical implementation of Command Pattern."

You can draw:

                   API
                    │
                    ▼
           CreateOrderCommand
                    │
                    ▼
        CreateOrderCommandHandler
                    │
                    ▼
              OrderService
                    │
                    ▼
           OrderRepository
                    │
                    ▼
                Database

Then say:

"I use the Command Pattern to encapsulate state-changing requests as objects. Each command has a dedicated handler. The handler depends on abstractions such as the order service and repository through dependency injection. This separates request representation from execution and makes the operation easier to test, queue, retry, audit, and integrate with CQRS."

That's the level I want you to reach for your product-company interviews.
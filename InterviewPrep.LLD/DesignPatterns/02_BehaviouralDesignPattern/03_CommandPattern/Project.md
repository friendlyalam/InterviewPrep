1. Project We Will Build
Order Management Command System

We'll build a system where different business operations are represented as commands.

For example:

Create Order
Cancel Order
Refund Order

Each operation will have its own:

Command
   ↓
Command Handler
   ↓
Business Service
   ↓
Repository
2. Why This Project?

A simple example like:

Remote → TurnOnCommand → TV

is good for learning the pattern, but not good enough for your interview preparation.

Our project demonstrates concepts you can discuss in a senior product-company interview:

Command Pattern
Command Handler
Dependency Injection
SOLID
CQRS relationship
Async programming
Validation
Repository abstraction
Result objects
Exception handling
CancellationToken
Logging
Extensibility
Unit-testability
3. Business Scenario

Imagine an e-commerce application.

A customer wants to create an order.

The API receives:

CustomerId
ProductId
Quantity
Price

Instead of directly calling:

_orderService.CreateOrder(...)

we create:

CreateOrderCommand

Then:

CreateOrderCommand
        ↓
CreateOrderCommandHandler
        ↓
OrderService
        ↓
OrderRepository
4. Complete Architecture
                         API / Program
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
                    IOrderRepository
                              │
                              ▼
                         Database

For cancellation:

API
 │
 ▼
CancelOrderCommand
 │
 ▼
CancelOrderCommandHandler
 │
 ▼
OrderService
 │
 ▼
Repository

For refund:

API
 │
 ▼
RefundOrderCommand
 │
 ▼
RefundOrderCommandHandler
 │
 ▼
PaymentService
 │
 ▼
Payment Gateway
5. Project Structure

We'll use this structure:

10_CommandPattern
│
├── Commands
│   │
│   ├── CreateOrderCommand.cs
│   ├── CancelOrderCommand.cs
│   └── RefundOrderCommand.cs
│
├── Handlers
│   │
│   ├── CreateOrderCommandHandler.cs
│   ├── CancelOrderCommandHandler.cs
│   └── RefundOrderCommandHandler.cs
│
├── Interfaces
│   │
│   ├── ICommand.cs
│   ├── ICommandHandler.cs
│   ├── IOrderService.cs
│   ├── ICommandOrderRepository.cs
│   └── IPaymentService.cs
│
├── Models
│   │
│   ├── CommandOrder.cs
│   └── CommandResult.cs
│
├── Services
│   │
│   ├── OrderService.cs
│   └── PaymentService.cs
│
├── Repositories
│   │
│   └── CommandOrderRepository.cs
│
├── DependencyInjection
│   │
│   └── ServiceCollectionExtensions.cs
│
└── Program.cs

This is enough to demonstrate the pattern without turning it into a 2,000-line application.

6. Commands We'll Implement
CreateOrderCommand

Represents:

Create a new order.

CreateOrderCommand
        ↓
CreateOrderCommandHandler
CancelOrderCommand

Represents:

Cancel an existing order.

CancelOrderCommand
        ↓
CancelOrderCommandHandler
RefundOrderCommand

Represents:

Refund an order.

RefundOrderCommand
        ↓
RefundOrderCommandHandler
7. Why Three Commands?

One command would demonstrate the basic pattern.

Three commands demonstrate why Command is useful.

We'll show:

                  ICommand
                     │
        ┌────────────┼─────────────┐
        ▼            ▼             ▼
CreateOrder       CancelOrder    RefundOrder
Command           Command        Command
        │            │             │
        ▼            ▼             ▼
CreateHandler     CancelHandler  RefundHandler

Adding a new operation becomes straightforward:

ShipOrderCommand
ShipOrderCommandHandler

without modifying existing commands.

That's a strong demonstration of Open/Closed Principle.

8. Command Responsibilities

A command should primarily represent the request/data.

Example:

CreateOrderCommand

CustomerId
ProductId
Quantity
Price

It should not contain:

SQL
Database calls
Payment logic
Business workflow

Those belong elsewhere.

9. Handler Responsibility

The handler receives the command and coordinates the operation.

CreateOrderCommand
        ↓
Handler
        ↓
Validate
        ↓
Service
        ↓
Repository

The handler is the bridge between:

Request

and

Business operation
10. Service Responsibility

The service contains actual business rules.

For example:

OrderService

- Check quantity
- Check order status
- Calculate total
- Create order
- Cancel order

This prevents the handler from becoming a God class.

11. Repository Responsibility

The repository handles persistence.

OrderRepository

Create
Get
Update

We'll use an in-memory implementation for the demo.

Why?

Because our goal is to demonstrate Command Pattern, not spend 20 minutes configuring SQL Server.

In a real application:

IOrderRepository
       ↓
EF Core
       ↓
SQL Server/PostgreSQL
12. Payment Service

Refund introduces another real-world dependency:

RefundOrderCommand
       ↓
RefundHandler
       ↓
PaymentService
       ↓
Payment Gateway

For the demo we'll simulate the gateway.

Production could be:

Stripe
Razorpay
Adyen
Payment Service
13. Dependency Injection

We will not do this:

new CreateOrderCommandHandler(...)

inside Program.cs.

Instead:

Program
   ↓
DI Container
   ↓
ICommandHandler
   ↓
Implementation

We'll have:

ServiceCollectionExtensions.cs

with registrations such as:

IOrderService
      ↓
OrderService

IOrderRepository
      ↓
OrderRepository

This matches the enterprise approach you've been asking for in our pattern projects.

14. Program.cs

Program.cs will remain very small.

Something conceptually like:

Build DI container
       ↓
Resolve command handler
       ↓
Create command
       ↓
Execute handler
       ↓
Display result

The actual implementation will use DI rather than manually constructing the dependency graph.

15. Async

We'll use:

Task<T>

and:

CancellationToken

where appropriate.

Why?

Because modern .NET enterprise applications commonly perform:

Database calls
HTTP calls
Message broker calls
Payment calls

asynchronously.

16. Result Pattern

Instead of simply:

bool

we'll return something like:

CommandResult

Success
Message
Data

Example:

Success: true
Message: Order created successfully
OrderId: ORD-1001

This makes the demo more realistic.

17. Validation

For CreateOrderCommand we'll demonstrate basic validation:

CustomerId > 0
ProductId > 0
Quantity > 0
Price > 0

The goal is to demonstrate where validation belongs rather than building a complete validation framework.

18. Error Handling

We'll demonstrate:

Invalid request
Order not found
Invalid order status
Payment failure

The handler/service should return meaningful results rather than allowing random exceptions to leak everywhere.

19. Command Lifecycle

The complete lifecycle will be:

                 Client
                   │
                   ▼
          CreateOrderCommand
                   │
                   ▼
       CreateOrderCommandHandler
                   │
                   ▼
             Validation
                   │
                   ▼
             OrderService
                   │
                   ▼
          OrderRepository
                   │
                   ▼
               Result
                   │
                   ▼
                Client
20. Enterprise Extension

After the basic implementation, I'll show how the same design can evolve into:

Command
   ↓
Handler
   ↓
Domain
   ↓
Repository
   ↓
Database

and eventually:

API
 ↓
Command
 ↓
Mediator
 ↓
Handler
 ↓
Domain
 ↓
Repository
 ↓
Database

This connects directly to CQRS + MediatR, which is much more relevant to your .NET product-company preparation.

21. Command + Observer Combination

Since we just completed Observer, I want you to understand how these patterns work together.

After creating an order:

CreateOrderCommand
        ↓
Handler
        ↓
Order Created
        ↓
OrderCreatedEvent
        ↓
 ┌──────┼──────────┐
 ▼      ▼          ▼
Email  Inventory  Analytics

So:

Command = "Do something."

Observer/Event = "Something happened."

This distinction is very important in senior interviews.

22. What We Will NOT Build

To keep this a 15–20 minute interview project, we won't unnecessarily add:

❌ Real SQL Server

❌ Real payment gateway

❌ Authentication

❌ JWT

❌ Full ASP.NET Web API

❌ Kafka

❌ RabbitMQ

❌ Complex domain model

❌ 20+ classes

Those are separate topics.

We'll demonstrate the design pattern, not build an entire production application.

23. What You Should Be Able to Explain in Interview

After completing this project, you should be able to draw:

Controller
    ↓
Command
    ↓
Handler
    ↓
Service
    ↓
Repository

and explain:

"The Command Pattern encapsulates a request as an object. The handler executes that request, 
while the business service owns the business rules. This separation allows commands to be queued, logged, retried, audited, and used naturally with CQRS."

That is a much stronger answer than simply saying:

"Command pattern is used for undo/redo."

24. Final Project Flow

Our finished project will support:

CREATE ORDER
     ↓
CreateOrderCommand
     ↓
CreateOrderCommandHandler
     ↓
OrderService
     ↓
Repository


CANCEL ORDER
     ↓
CancelOrderCommand
     ↓
CancelOrderCommandHandler
     ↓
OrderService
     ↓
Repository


REFUND ORDER
     ↓
RefundOrderCommand
     ↓
RefundOrderCommandHandler
     ↓
PaymentService

Then DI:

ServiceCollectionExtensions
          ↓
      DI Container
          ↓
      Program.cs
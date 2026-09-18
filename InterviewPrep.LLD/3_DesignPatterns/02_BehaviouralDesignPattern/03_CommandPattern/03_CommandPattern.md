Command Pattern — Complete Theory
1. Definition

Command Pattern encapsulates a request or action as an object, allowing the request to be stored, queued,
logged, retried, or executed independently from the object that performs the operation.

Simple meaning

Instead of:

Client → Service → Execute

we use:

Client
  ↓
Command
  ↓
Handler
  ↓
Service
  ↓
Execute

For example:

CancelOrderCommand

represents:

"Cancel order 1001."

The command represents the request; it doesn't need to perform the business operation itself.

------------------------------------------------------------------------------------------------------------
2. Intent

The main goal is to separate the sender of a request from the object that actually performs it.

For example:

Controller
     ↓
CreateOrderCommand
     ↓
CreateOrderCommandHandler
     ↓
OrderService

The controller doesn't need to know how an order is created.

----------------------------------------------------------------------------------------

3. Real-Life Examples
Example 1 — Restaurant Order

You tell the waiter:

"I want a chicken biryani."

The waiter creates an order/request.

Customer
   ↓
Order Request
   ↓
Kitchen
   ↓
Prepare Food

The request can be processed later.


Example 2 — ATM

You request:

Withdraw ₹10,000.

Conceptually:

Withdraw Request
       ↓
ATM System
       ↓
Bank Account
       ↓
Withdraw Money

The withdrawal request is a command.

-----------------------------------------------------------

Example 3 — Remote Control

A remote button represents:

TurnOnCommand
TurnOffCommand
IncreaseVolumeCommand

The remote doesn't implement television logic.

Remote
  ↓
Command
  ↓
TV

-------------------------------------------------------------

4. Real Product-Company Examples
E-commerce
CreateOrderCommand
CancelOrderCommand
ShipOrderCommand
RefundOrderCommand
Banking
TransferMoneyCommand
WithdrawMoneyCommand
DepositMoneyCommand
Microsoft/Azure-style systems

A request such as:

CreateResourceCommand

can represent:

Create VM
Create Storage
Create Database

The request can then be processed by an appropriate handler/service.


Background Jobs

This is especially important.

GenerateInvoiceCommand
        ↓
Queue
        ↓
Worker
        ↓
Generate Invoice

Because the command is an object, it can be:

queued
serialized
retried
logged

--------------------------------------------------------------
5. Command + CQRS ⭐⭐⭐⭐⭐

This is probably the most important connection for your interviews.

CQRS separates:

Command → Change State
Query   → Read State

Example:

POST /orders

       ↓

CreateOrderCommand

       ↓

CreateOrderCommandHandler

       ↓

OrderService

       ↓

Database

For a query:

GET /orders/1001

       ↓

GetOrderQuery

       ↓

GetOrderQueryHandler

       ↓

Database

So you will frequently see:

Command
+
Handler
+
CQRS
+
MediatR

together in .NET applications.

-----------------------------------------------------------------

6. Advantages
✅ 1. Loose Coupling

Sender doesn't need to know implementation details.

Controller
   ↓
Command

instead of directly depending on many services.

✅ 2. Supports Queuing

Commands can be placed into:

RabbitMQ
Kafka
Azure Service Bus
SQS

and processed later.

✅ 3. Supports Retry

If processing fails:

Command
   ↓
Handler
   ↓
Failure
   ↓
Retry
✅ 4. Supports Logging/Auditing

You can record:

Who
What command
When
Correlation ID
Result
✅ 5. Supports Undo

Some commands can store enough information to reverse an operation.

DeleteCommand
     ↓
Undo
     ↓
Restore
✅ 6. Easy Testing

You can test:

CreateOrderCommandHandler

independently from the controller.

✅ 7. Easy Extension

Today:

CreateOrderCommand

Tomorrow:

CancelOrderCommand
RefundOrderCommand
ShipOrderCommand

Existing commands don't need modification.

----------------------------------------------------------

7. Disadvantages
❌ 1. More Classes

A simple operation may require:

Command
Handler
Service

instead of one method.

❌ 2. More Complexity

For a small CRUD application:

Controller → Service

may be perfectly sufficient.

Adding Command + Handler may be unnecessary.

❌ 3. Debugging Can Become More Difficult

Instead of:

Controller → Service

you may have:

Controller
 ↓
Mediator
 ↓
Command
 ↓
Handler
 ↓
Service
 ↓
Repository
❌ 4. Overengineering Risk

Not every method needs a command.

----------------------------------------------------------------------

8. When to Use Command

Use it when you need:

⭐ Complex business operations
PlaceOrder
CancelOrder
RefundPayment
TransferMoney
⭐ CQRS
Command → Handler
⭐ Background processing
Command → Queue → Worker
⭐ Retry
Command → Handler → Retry
⭐ Auditing
Command → Audit → Execute
⭐ Scheduling
Command → Scheduler → Execute Later
⭐ Undo/Redo
Command → Execute
Command → Undo

---------------------------------------------------------------

9. When NOT to Use Command

Don't use it for every simple method.

For example:

customer.GetName();

Creating:

GetCustomerNameCommand
GetCustomerNameCommandHandler

would probably be unnecessary.

Also avoid it when:

Application is very small.
Operation is trivial.
No queuing/retry/auditing/CQRS requirement exists.
Additional abstraction doesn't provide value.

------------------------------------------------------

10. Command vs Strategy

Very important interview question.

| Command                 | Strategy                                      |
| ----------------------- | --------------------------------------------- |
| Represents a request    | Represents an algorithm                       |
| **What should happen?** | **How should it happen?**                     |
| Can be queued           | Usually selected/executed directly            |
| Can support undo        | Usually doesn't represent an undoable request |
| `RefundOrderCommand`    | `CreditCardRefundStrategy`                    |

They can work together:

RefundOrderCommand
       ↓
RefundCommandHandler
       ↓
RefundStrategy
       ↓
Payment Gateway

----------------------------------------------

11. Command vs Observer

| Command              | Observer              |
| -------------------- | --------------------- |
| Request              | Notification          |
| Usually one handler  | Multiple subscribers  |
| "Do this"            | "This happened"       |
| `CancelOrderCommand` | `OrderCancelledEvent` |


Enterprise example:

CancelOrderCommand
        ↓
CancelOrderHandler
        ↓
Order Cancelled
        ↓
OrderCancelledEvent
        ↓
 ┌──────┼─────────┐
Email  Inventory  Audit

This combination is extremely useful to understand.

-------------------------------------------------------

12. Command vs Facade

| Command                   | Facade                        |
| ------------------------- | ----------------------------- |
| Encapsulates a request    | Simplifies complex subsystem  |
| Represents an action      | Coordinates multiple services |
| `CreateOrderCommand`      | `OrderFacade`                 |
| Often paired with Handler | Often paired with services    |


They can also work together:

Controller
    ↓
CreateOrderCommand
    ↓
Handler
    ↓
OrderFacade
    ↓
Payment + Inventory + Shipping

----------------------------------------------------------------

13. Command Structure

The architecture we'll implement is:

                    Controller / Program
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
                       Repository
                           │
                           ▼
                       Database

And with DI:

ICommandHandler<TCommand, TResult>

will be injected rather than created using new.

-------------------------------------------------------------------------

14. Enterprise Version We'll Build

Our project will demonstrate:

Command
Handler
Service
Repository abstraction
Dependency Injection
Result object
Validation
Async execution

And we'll keep it 20-minute interview-demo size, not a huge project.

------------------------------------------------------------------------------------

15. Interview Questions You Must Know
Basic

Q: What is Command Pattern?

It encapsulates a request as an object and separates the sender from the receiver.

Intermediate

Q: Why use Command instead of directly calling a service?

It provides a clear request abstraction and enables capabilities such as queuing, retry, logging, auditing, scheduling, and CQRS.

Advanced

Q: How is Command related to CQRS?

In CQRS, commands represent state-changing operations and are typically processed by dedicated command handlers.

Senior-level

Q: Would you use Command for every application operation?

No. I'd use it when the additional abstraction provides value such as CQRS, complex workflows, asynchronous processing, 
retryability, auditing, or scheduling. For trivial operations, direct service calls are simpler.

That last answer is particularly important in a product-company interview because it demonstrates that you understand trade-offs rather than blindly applying patterns.


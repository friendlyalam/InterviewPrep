# Real-World Delegates and Events in C#

This file connects delegates and events to product-company scenarios.

---

# 1. Order Processing

Imagine an order service:

```text
Create Order
     ↓
Validate
     ↓
Save Order
     ↓
Order Created
     ↓
Notify interested components

A simple in-process event:

public event EventHandler<OrderCreatedEventArgs>?
    OrderCreated;

Subscribers:

OrderCreated += SendEmail;
OrderCreated += UpdateAudit;
OrderCreated += SendNotification;
2. Delegate for Configurable Behavior

Suppose a pricing service accepts a pricing rule:

public decimal Calculate(
    decimal amount,
    Func<decimal, decimal> pricingRule)
{
    return pricingRule(amount);
}

Usage:

decimal result =
    Calculate(
        1000,
        amount => amount * 0.90m);

This is useful when the behavior is small and local.

3. Strategy Using Delegate

Simple strategy:

Func<decimal, decimal> discount =
    amount => amount * 0.90m;

Another:

Func<decimal, decimal> discount =
    amount => amount * 0.80m;

The same processing code can use different strategies.

For complex strategies, prefer an interface:

public interface IDiscountStrategy
{
    decimal Calculate(decimal amount);
}
4. Validation Pipeline

Delegates can represent validation rules:

Func<Order, bool> isValidCustomer;
Func<Order, bool> hasValidAmount;
Func<Order, bool> hasInventory;

Then:

bool valid =
    isValidCustomer(order)
    && hasValidAmount(order)
    && hasInventory(order);

For larger systems, validation frameworks or explicit services may be more appropriate.

5. Notification System

Simple in-process notification:

public event EventHandler<OrderCreatedEventArgs>?
    OrderCreated;

Subscribers:

OrderCreated
    |
    +-- Email
    +-- SMS
    +-- Audit
    +-- Metrics

This provides loose coupling between the publisher and subscribers.

6. Event-Driven Microservices

Do not confuse C# events with distributed events.

In-process
OrderService
     ↓
C# Event
     ↓
Subscriber
Distributed
Order Service
     ↓
RabbitMQ / Kafka / Azure Service Bus
     ↓
Consumers

Distributed messaging is appropriate when independent services need reliable communication.

7. File Processing

Suppose a file has been uploaded:

Upload
  ↓
File saved
  ↓
FileUploaded
  ↓
 ┌───────┬─────────┐
 ↓       ↓         ↓
Scan   Metadata   Audit

An in-process event can notify local components.

For large/critical processing:

File uploaded
      ↓
Message Broker
      ↓
Background worker
      ↓
Virus scan / processing
8. Payment Processing

A simple local event:

PaymentCompleted
       ↓
Audit

But don't assume that:

PaymentCompleted?.Invoke(...);

means the payment notification is durable.

If payment processing is business-critical:

Payment Service
      ↓
Transaction
      ↓
Outbox
      ↓
Message Broker
      ↓
Consumers
9. Delegate for Logging

A small component may accept:

Action<string> logger

Example:

public void Process(
    Action<string> logger)
{
    logger("Processing started");

    // Work

    logger("Processing completed");
}

Usage:

Process(
    message => Console.WriteLine(message));

For real applications, use a proper logging abstraction such as ILogger<T> rather than inventing delegates everywhere.

10. Delegate for Transformation
public List<TResult> Transform<T, TResult>(
    IEnumerable<T> items,
    Func<T, TResult> selector)
{
    return items
        .Select(selector)
        .ToList();
}

Usage:

var names =
    Transform(
        customers,
        customer => customer.Name);

This is conceptually the same kind of behavior passed to LINQ.

11. ASP.NET Core Example

Suppose a service performs a business operation:

public async Task ProcessAsync(
    Order order,
    Func<Order, Task> afterProcessed)
{
    // Business operation

    await afterProcessed(order);
}

Usage:

await ProcessAsync(
    order,
    async order =>
    {
        await notificationService
            .SendAsync(order);
    });

Use this style when the callback is truly local to the operation.

Don't use callbacks to hide major business workflows.

12. Event Memory/Lifetime Problem

Suppose:

Singleton publisher
       ↓
event subscription
       ↓
short-lived object

The publisher may keep the subscriber reachable through the event subscription.

Therefore:

Long-lived publisher
+
Long-lived event subscription
+
Short-lived subscriber

can contribute to unwanted object retention.

Unsubscribe when appropriate.

13. Event Handler Failure

Suppose:

OrderCreated
   |
   +-- Email
   +-- Audit
   +-- Notification

What if Email throws?

You need to define:

Should Audit still run?
Should Notification still run?
Should the order operation fail?
Should the error be retried?
Should the failure be logged?

For critical workflows, explicit messaging or orchestration often provides clearer semantics.

14. Event vs Message Broker
| Feature                  | C# Event              | Message Broker             |
| ------------------------ | --------------------- | -------------------------- |
| Scope                    | Usually process-local | Distributed                |
| Durable                  | No                    | Can be                     |
| Survives process restart | No                    | Depending on configuration |
| Retry                    | Manual                | Broker/consumer mechanisms |
| Independent services     | No                    | Yes                        |
| Scaling consumers        | Limited/local         | Designed for it            |
| Example                  | `OrderCreated`        | Kafka/RabbitMQ/Service Bus |

15. Product-Company Decision Rule

Use a delegate when:

I need to pass behavior.

Use an event when:

Something happened and local subscribers
may need to react.

Use a message broker when:

Independent processes/services need
reliable distributed communication.

Use an interface/service when:

The behavior is a substantial business
capability with dependencies/state.
16. Real-World Architecture

A mature order system may look like:

Client
  ↓
Order API
  ↓
Order Service
  ↓
Database
  ↓
Outbox
  ↓
Message Broker
  ↓
 ┌────────────┬────────────┬─────────────┐
 ↓            ↓            ↓
Payment    Inventory    Notification
Service     Service        Service

Inside one service, delegates/events may still be useful.

Across services, durable messaging is usually the appropriate mechanism.

17. Key Product-Company Principle

Do not ask:

"Can I use an event here?"

Ask:

"What communication semantics does this requirement need?"

Then consider:

Local behavior
→ Delegate

Local notification
→ Event

Business capability
→ Interface / Service

Distributed reliable communication
→ Message Broker
18. Key Points
Delegates are excellent for small behavior injection.
Events are useful for in-process notifications.
Interfaces are better for substantial business capabilities.
C# events are not distributed messaging.
Events are not automatically asynchronous.
Async callbacks should normally be awaitable.
Long-lived event publishers require lifetime consideration.
Critical workflows need explicit failure handling.
Distributed business events generally require durable messaging.
Don't use delegates/events merely because they are available.
Interview Mental Model
Delegate
→ "What behavior should I execute?"

Event
→ "Something happened. Who is interested?"

Interface
→ "What capability does this object provide?"

Message Broker
→ "How do independent services communicate reliably?"

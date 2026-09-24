# Events in C#

---

# 1. What is an Event?

An event is a delegate-based mechanism used to notify interested subscribers that something happened.

Examples:

```text
OrderCreated
PaymentCompleted
UserRegistered
FileUploaded
InventoryUpdated
PaymentFailed

Mental model:

Something happened
       ↓
     Event
       ↓
Subscribers react
2. Publisher and Subscriber

An event has two main sides.

Publisher

The class that raises the event.

Example:

OrderService
Subscriber

The class that subscribes to the event.

Example:

NotificationService

Architecture:

OrderService
     |
     | OrderCreated
     ↓
NotificationService
3. Basic Event Example
public class OrderService
{
    public event Action<int>? OrderCreated;

    public void CreateOrder(int orderId)
    {
        Console.WriteLine("Order created");

        OrderCreated?.Invoke(orderId);
    }
}

Subscriber:

var service = new OrderService();

service.OrderCreated +=
    orderId =>
    {
        Console.WriteLine(
            $"Notification for order {orderId}");
    };

service.CreateOrder(1001);
4. += and -=

Subscribe:

service.OrderCreated += HandleOrderCreated;

Unsubscribe:

service.OrderCreated -= HandleOrderCreated;

These operations modify the event's subscriber list.

5. Why Use event?

Consider:

public Action<int>? OrderCreated;

External code could potentially:

service.OrderCreated = null;

or invoke/replace the delegate.

Instead:

public event Action<int>? OrderCreated;

External code can subscribe/unsubscribe but cannot normally raise or replace the event.

This provides encapsulation.

6. Delegate vs Event

Delegate:

public Action<int>? OrderCreated;

Event:

public event Action<int>? OrderCreated;

Important difference:

Delegate
→ general method reference

Event
→ controlled publish/subscribe

The event publisher controls when the notification is raised.

7. Multiple Subscribers
service.OrderCreated += SendEmail;
service.OrderCreated += SendSms;
service.OrderCreated += WriteAudit;

When raised:

OrderCreated
     |
     +-- SendEmail
     +-- SendSms
     +-- WriteAudit
8. Standard Event Pattern

.NET commonly uses:

EventHandler

Example:

public event EventHandler? OrderCreated;

Raise:

OrderCreated?.Invoke(
    this,
    EventArgs.Empty);
9. EventHandler Pattern

Example:

public class OrderService
{
    public event EventHandler? OrderCreated;

    public void CreateOrder()
    {
        Console.WriteLine("Order created");

        OrderCreated?.Invoke(
            this,
            EventArgs.Empty);
    }
}

Subscriber:

service.OrderCreated +=
    HandleOrderCreated;

static void HandleOrderCreated(
    object? sender,
    EventArgs e)
{
    Console.WriteLine(
        "Order notification received");
}
10. Why sender?

sender identifies the object that raised the event.

Example:

OrderCreated?.Invoke(this, EventArgs.Empty);

Here:

sender = this

The subscriber can inspect the publisher if necessary.

11. Why EventArgs?

EventArgs is the standard .NET base type for event data.

If no data is required:

EventArgs.Empty

If data is required, create custom event arguments.

12. Events Are Usually Synchronous

Consider:

OrderCreated?.Invoke(
    this,
    EventArgs.Empty);

The event handlers normally execute synchronously as part of this invocation.

Therefore:

An event does not automatically mean asynchronous execution.

If asynchronous work is required, design an async-compatible approach.

13. Events and Thread Safety

If multiple threads can subscribe/unsubscribe or raise events, concurrency needs to be considered.

Events do not automatically make the underlying business state thread-safe.

For example:

Event thread safety
≠
Business state thread safety

You still need appropriate synchronization where shared mutable state exists.

14. Events and Memory Leaks

A long-lived publisher holding a reference to a subscriber can keep that subscriber alive.

Example:

Long-lived publisher
       ↓
event subscription
       ↓
short-lived subscriber

If the subscriber should be collected but remains subscribed, the event subscription can contribute to unwanted object retention.

Important especially with:

UI applications
long-lived services
static events
global event aggregators

Unsubscribe when the subscriber's lifetime ends when appropriate.

15. Event vs Message Broker

C# event:

Process
  ↓
Publisher
  ↓
Event
  ↓
Subscriber

Usually:

in-memory
process-local
not durable
lost when process terminates

Message broker:

Service A
   ↓
RabbitMQ / Kafka / Azure Service Bus
   ↓
Service B

Designed for distributed communication and durable/reliable messaging patterns.

16. Product-Company Example

Simple monolith:

OrderService
      |
      | OrderCreated event
      |
 ┌────┼───────────────┐
 ↓    ↓               ↓
Email Audit       Notification

Microservices:

Order Service
      |
      ↓
Message Broker
      |
 ┌────┼───────────────┐
 ↓    ↓               ↓
Email Inventory   Analytics

Do not confuse the two designs.

17. Key Points
Event
→ notification mechanism

Publisher
→ raises event

Subscriber
→ listens to event

+=
→ subscribe

-=
→ unsubscribe

event
→ external code cannot normally invoke it

EventHandler
→ standard .NET event pattern

EventArgs
→ event data

C# event
→ normally in-process

Event
≠
message broker

Event
≠
automatically asynchronous
Interview Definition

An event is a delegate-based publish/subscribe mechanism that allows a type to notify subscribers
when something happens while restricting external code from directly raising the event.
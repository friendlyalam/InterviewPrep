Output
===== Publishing Order Event =====

[Email] Confirmation email sent to customer@company.com

[SMS] SMS sent for Order ORD-1001

[Inventory] Stock reserved for Order ORD-1001

[Analytics] Order value recorded : ₹4,999.00

[Audit] Audit log created for Order ORD-1001

===== Event Published Successfully =====
Enterprise Improvement #1 ⭐⭐⭐⭐⭐

The current implementation has a weakness.

Suppose

Email Subscriber

↓

Throws Exception

Then

SMS

Inventory

Analytics

Audit

will never execute.

Better Enterprise Version
public void Publish(OrderPlacedEvent orderEvent)
{
    foreach (IEventSubscriber subscriber in _subscribers)
    {
        try
        {
            subscriber.Handle(orderEvent);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Subscriber {subscriber.GetType().Name} failed: {ex.Message}");
        }
    }
}

Now

If Email fails

Inventory still works.

Analytics still works.

Audit still works.

This is how resilient event publishing is often implemented.

Enterprise Improvement #2 ⭐⭐⭐⭐⭐

Our implementation is

Synchronous

Every subscriber waits.

Email

↓

SMS

↓

Inventory

↓

Analytics

Real systems often publish asynchronously.

Order Created

↓

Kafka

↓

Email

SMS

Inventory

Analytics

Audit

Every subscriber runs independently.

Enterprise Improvement #3 ⭐⭐⭐⭐⭐

In ASP.NET Core

Instead of

publisher.Publish(...)

you'll often see

await mediator.Publish(orderPlacedEvent);

using libraries such as MediatR, or events flowing through a message broker.

The underlying design idea is still Observer.

SOLID Principles
Principle	Usage
SRP	Publisher only publishes events.
OCP	Add new subscribers without modifying the publisher.
LSP	Every subscriber is interchangeable through IEventSubscriber.
ISP	Small Handle() contract.
DIP	Publisher depends only on the subscriber abstraction.
Product Company Examples
Company	Event
Amazon	Order Placed
Microsoft	User Registered
Google	File Uploaded
Uber	Ride Completed
Walmart	Inventory Updated
Common Mistakes
❌ Mistake 1

Publisher calling concrete subscribers.

emailSubscriber.Handle(...);

smsSubscriber.Handle(...);

Wrong.

❌ Mistake 2

Subscribers calling each other.

Email

↓

SMS

↓

Inventory

Wrong.

Subscribers should be independent.

❌ Mistake 3

Putting business logic inside the publisher.

Publisher should only publish.

Subscribers perform the work.

Interview Questions
Q1. Observer vs Pub/Sub?
Observer is an in-memory design pattern where the publisher holds references to subscribers.
Publish/Subscribe is an architectural style where a broker (Kafka, RabbitMQ, Azure Service Bus, etc.) decouples publishers and subscribers.
Q2. Can Observer be asynchronous?

Yes. The pattern is independent of transport. Notifications can be synchronous or asynchronous.

Q3. How do you prevent one subscriber from breaking all others?

Catch exceptions per subscriber (or isolate processing through asynchronous messaging).

Q4. Where is Observer used in .NET?
Events and delegates (event, EventHandler)
INotifyPropertyChanged
MediatR notifications
Domain events
SignalR event notifications (conceptually)
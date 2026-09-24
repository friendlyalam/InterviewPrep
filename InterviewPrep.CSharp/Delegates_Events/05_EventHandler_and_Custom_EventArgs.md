# EventHandler and Custom EventArgs in C#

---

# 1. EventHandler

.NET provides the standard:

```csharp
EventHandler

delegate.

Its conceptual signature is:

void Handler(
    object? sender,
    EventArgs e);

Example:

public event EventHandler? OrderCreated;

Raise:

OrderCreated?.Invoke(
    this,
    EventArgs.Empty);
2. Why Use EventHandler?

It provides a standard .NET event pattern.

It makes events familiar and consistent.

Typical structure:

Publisher
    |
    | event
    ↓
EventHandler
    |
    ↓
Subscriber
3. EventHandler<TEventArgs>

When the event needs data:

public event EventHandler<OrderCreatedEventArgs>?
    OrderCreated;

The custom event data class should generally derive from:

EventArgs
4. Custom EventArgs

Example:

public sealed class OrderCreatedEventArgs
    : EventArgs
{
    public int OrderId { get; }

    public decimal Amount { get; }

    public OrderCreatedEventArgs(
        int orderId,
        decimal amount)
    {
        OrderId = orderId;
        Amount = amount;
    }
}
5. Complete Example
using System;

public sealed class OrderCreatedEventArgs
    : EventArgs
{
    public int OrderId { get; }

    public decimal Amount { get; }

    public OrderCreatedEventArgs(
        int orderId,
        decimal amount)
    {
        OrderId = orderId;
        Amount = amount;
    }
}

public class OrderService
{
    public event EventHandler<OrderCreatedEventArgs>?
        OrderCreated;

    public void CreateOrder(
        int orderId,
        decimal amount)
    {
        Console.WriteLine("Order created.");

        OnOrderCreated(
            new OrderCreatedEventArgs(
                orderId,
                amount));
    }

    protected virtual void OnOrderCreated(
        OrderCreatedEventArgs e)
    {
        OrderCreated?.Invoke(this, e);
    }
}

Subscriber:

var service = new OrderService();

service.OrderCreated += HandleOrderCreated;

service.CreateOrder(1001, 5000m);

static void HandleOrderCreated(
    object? sender,
    OrderCreatedEventArgs e)
{
    Console.WriteLine(
        $"Order: {e.OrderId}");

    Console.WriteLine(
        $"Amount: {e.Amount}");
}
6. Why OnOrderCreated()?

Instead of directly writing:

OrderCreated?.Invoke(this, args);

everywhere, use:

protected virtual void OnOrderCreated(
    OrderCreatedEventArgs e)
{
    OrderCreated?.Invoke(this, e);
}

Benefits:

centralizes event raising
easier to override in inheritance scenarios
cleaner business methods
follows common .NET event patterns
7. Event Data Should Usually Be Read-Only

Prefer:

public int OrderId { get; }

rather than:

public int OrderId { get; set; }

The publisher creates the event data and subscribers consume it.

This reduces accidental mutation.

8. Event Subscriber
service.OrderCreated +=
    HandleOrderCreated;

Handler:

static void HandleOrderCreated(
    object? sender,
    OrderCreatedEventArgs e)
{
    Console.WriteLine(
        $"Order {e.OrderId} created");
}
9. Multiple Subscribers
service.OrderCreated += SendEmail;
service.OrderCreated += UpdateAudit;
service.OrderCreated += SendNotification;

All subscribed handlers participate in the event invocation.

10. Unsubscribe
service.OrderCreated -= SendEmail;

Important for long-lived publishers/subscribers.

Especially consider unsubscription when:

subscriber has a shorter lifetime
publisher is long-lived
UI components are involved
static/global events are used
11. Custom EventArgs vs Custom Delegate

You could define:

delegate void OrderCreatedHandler(
    int orderId,
    decimal amount);

But standard .NET code often prefers:

EventHandler<OrderCreatedEventArgs>

because it follows the established event pattern.

12. EventHandler vs Action Event

You may see:

public event Action<int>? OrderCreated;

This is valid for many application-level cases.

You may also see:

public event EventHandler<OrderCreatedEventArgs>?
    OrderCreated;

The second follows the standard .NET event pattern and provides:

sender
standard EventArgs
familiar conventions
extensible event data
13. Async Event Consideration

Standard:

EventHandler<TEventArgs>

returns:

void

Therefore it is not naturally awaitable.

If an application requires asynchronous handlers, carefully design an async event abstraction rather than blindly using:

async void

For simple callback-based async behavior, prefer:

Func<T, Task>

where appropriate.

14. Product-Company Scenario

Suppose:

OrderService
     |
     ↓
OrderCreated
     |
 ┌───┼──────────┐
 ↓   ↓          ↓
Email Audit  Analytics

For an in-process application, an event can be appropriate.

For independent microservices requiring durability:

OrderService
     |
     ↓
Message Broker
     |
 ┌───┼───────────┐
 ↓   ↓           ↓
Email Inventory Analytics

Use the appropriate mechanism based on system boundaries and reliability requirements.

15. Interview Questions
Why use EventHandler?

It follows the standard .NET event pattern.

What is EventArgs?

The standard base type used for event data.

Why inherit custom event arguments from EventArgs?

To follow the established .NET event convention and make event data strongly typed.

What is sender?

The object that raised the event.

Why use OnOrderCreated()?

It centralizes event raising and follows a common .NET pattern.

Can EventHandler be awaited?

No. Its normal signature returns void.

16. Key Points
EventHandler
→ standard .NET event delegate

EventHandler<TEventArgs>
→ event with strongly typed data

EventArgs
→ base type for event data

sender
→ publisher

e
→ event data

OnXxx()
→ common event-raising pattern

Custom EventArgs
→ strongly typed event information

Do not blindly use async void
→ for asynchronous workflows use an awaitable design
Interview Definition

EventHandler<TEventArgs> is the standard .NET delegate pattern for events,
where sender identifies the publisher and TEventArgs provides strongly typed event data.
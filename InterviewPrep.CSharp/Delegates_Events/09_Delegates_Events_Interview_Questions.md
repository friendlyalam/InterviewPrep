# Delegates and Events — Product-Company Interview Questions

---

# 1. What is a delegate?

A delegate is a type-safe reference to one or more compatible methods.

It allows methods to be:

- stored
- passed as parameters
- returned
- invoked later

---

# 2. Why use delegates?

Common uses:

- callbacks
- behavior injection
- LINQ
- sorting
- filtering
- transformations
- events
- async callbacks

---

# 3. Delegate vs Method

A method is executable code.

A delegate is an object/type-safe reference representing compatible method(s).

```text
Method
→ implementation

Delegate
→ reference to behavior
4. What is Action?

Action is a built-in delegate that returns void.

Action<string> log =
    message => Console.WriteLine(message);
5. What is Func?

Func is a built-in delegate that returns a value.

Func<int, int, int> add =
    (a, b) => a + b;

The last generic parameter is the return type.

6. What is Predicate?

Predicate<T> represents:

T → bool

Example:

Predicate<int> isEven =
    x => x % 2 == 0;
7. Action vs Func
Action
→ void

Func
→ returns value

Example:

Action<int> print;
Func<int, bool> validate;
8. What is a lambda?

A lambda is a concise anonymous function.

Example:

x => x * 2

It can be converted to a compatible delegate.

9. Is a lambda a delegate?

Not exactly.

A lambda expression can be converted to a compatible delegate or expression tree.

10. What is a callback?

A callback is behavior supplied to another method so that the receiving method can invoke it at an appropriate time.

Example:

ProcessOrder(
    orderId,
    id => Console.WriteLine(id));
11. What is a multicast delegate?

A delegate containing multiple method references.

Example:

handler += SendEmail;
handler += SendSms;
handler += WriteAudit;
12. What does += mean for a delegate?

It adds a method to the invocation list.

13. What does -= mean?

It removes a method from the invocation list.

14. What is an event?

An event is a delegate-based publish/subscribe mechanism that allows a type to notify subscribers.

15. Why use event instead of exposing a delegate?

Because event provides encapsulation.

External code can normally:

event += handler;
event -= handler;

but cannot directly raise or replace the event.

16. Who can raise an event?

The declaring type normally controls event invocation.

Subscribers only subscribe/unsubscribe.

17. Can an event have multiple subscribers?

Yes.

OrderCreated += SendEmail;
OrderCreated += SendSms;
OrderCreated += WriteAudit;
18. Are events asynchronous?

No.

Normal C# event invocation is synchronous.

If handlers perform asynchronous operations, the design must explicitly account for asynchronous execution.

19. What is EventHandler?

EventHandler is the standard .NET event delegate pattern.

Conceptually:

void Handler(
    object? sender,
    EventArgs e);
20. What is EventHandler<TEventArgs>?

It provides strongly typed event data.

Example:

public event EventHandler<OrderCreatedEventArgs>?
    OrderCreated;
21. What is EventArgs?

EventArgs is the standard base class for event data.

Custom event data can inherit from it.

22. What is sender?

The object that raised the event.

Example:

OrderCreated?.Invoke(this, args);

Here:

sender = this
23. What is a closure?

A closure occurs when a lambda or local function captures variables from its surrounding scope.

Example:

int discount = 10;

Func<int, int> calculate =
    price => price - discount;

The lambda captures discount.

24. Delegate vs Event

| Delegate                 | Event                                |
| ------------------------ | ------------------------------------ |
| General method reference | Notification mechanism               |
| Can invoke directly      | Publisher controls invocation        |
| Can be assigned          | External code cannot normally assign |
| Can represent callback   | Represents publish/subscribe         |
| General-purpose          | Event-specific                       |


Mental model:

Delegate
→ Pass behavior

Event
→ Notify subscribers
25. Delegate vs Interface

Use a delegate for small/simple behavior:

Func<Order, bool>

Use an interface for a substantial business capability:

IPaymentProcessor
26. Delegate vs Strategy Pattern

A simple strategy can use:

Func<decimal, decimal>

For complex strategies with dependencies/state/multiple operations, use an interface/class.

27. Action vs Func<Task>

For synchronous:

Action

For awaitable async callbacks:

Func<Task>

Avoid general-purpose:

async void
28. Why is async void problematic?

Because the caller cannot await it.

Problems:

difficult exception handling
no completion tracking
difficult composition
difficult cancellation coordination

Event handlers are one framework scenario where async void may be required by the API shape.

29. Can delegates point to instance methods?

Yes.

Example:

var service = new OrderService();

Action action =
    service.Process;
30. Can delegates point to static methods?

Yes.

Action action =
    OrderService.Process;
31. Can delegates return values?

Yes.

Example:

Func<int, int> square =
    x => x * x;
32. What happens when a multicast delegate returns a value?

Multiple methods may execute, but normal delegate invocation exposes the return value from the last invoked method.

Therefore multicast delegates are generally more suitable for notification-style void methods.

33. What happens if one event handler throws?

Normal multicast invocation can stop subsequent handlers from executing.

For critical workflows, explicitly define failure handling.

34. Are C# events distributed?

No.

Normal C# events are process-local and in-memory.

35. Event vs Message Broker

C# event:

Process
 ↓
Event
 ↓
Subscriber

Message broker:

Service
 ↓
Broker
 ↓
Other service

A broker is appropriate for distributed reliable communication.

36. Product-Company Scenario
Scenario

An order is created.

Requirements:

audit
notification
analytics
payment processing

Possible design:

Order Service
      ↓
OrderCreated

For local in-process behavior:

C# event

For independent microservices:

Message Broker

The correct answer depends on the communication boundary and reliability requirements.

37. Event Memory Retention

A long-lived publisher may hold references to subscribers through event subscriptions.

This can keep objects alive longer than expected.

Be careful with:

singleton publishers
static events
global event aggregators
UI components
short-lived subscribers
38. When Should You NOT Use an Event?

Avoid events when:

the caller needs a direct result
the operation must be explicitly awaited
failure semantics are critical
the workflow needs durable delivery
communication crosses service boundaries

Consider:

direct method call
interface/service
async method
message broker
39. Most Important Interview Traps
Trap 1

"Event means asynchronous."

Wrong.

Trap 2

"Event means distributed messaging."

Wrong.

Trap 3

"Action is good for async callbacks."

Generally avoid it for async operations.

Prefer:

Func<Task>
Trap 4

"Lambda is exactly the same thing as delegate."

Not exactly.

Lambda can be converted to a delegate.

Trap 5

"Delegate and event are the same."

No.

An event uses delegate-based mechanics but adds subscription/invocation restrictions.

40. Senior-Level Scenario Question
Question

You have:

OrderService
PaymentService
NotificationService
InventoryService

Should you use C# events between them?

Answer

If these components are inside the same process, C# events can be useful for local notifications.

If they are independent microservices, normal C# events cannot communicate across processes.

Use a distributed messaging mechanism such as:

RabbitMQ
Kafka
Azure Service Bus

depending on the architecture and reliability requirements.

41. Quick Interview Definitions
Delegate

A type-safe reference to one or more compatible methods.

Action

A built-in delegate returning void.

Func

A built-in delegate returning a value.

Predicate

A delegate returning bool.

Lambda

A concise anonymous function that can be converted to a delegate or expression tree.

Callback

Behavior passed to another component to be invoked later.

Event

A delegate-based publish/subscribe notification mechanism.

EventHandler

The standard .NET delegate pattern for events.

Multicast Delegate

A delegate containing multiple method references.

42. Final Interview Mental Model
                    Delegates & Events
                           |
          ┌────────────────┼────────────────┐
          ↓                ↓                ↓
       Delegate           Event          Lambda
          |                |                |
     Pass behavior     Notify others    Short behavior
          |
    ┌─────┼─────┐
    ↓     ↓     ↓
 Action  Func Predicate

And at architecture level:

Small behavior
     ↓
Delegate

Local notification
     ↓
Event

Business capability
     ↓
Interface / Service

Distributed reliable communication
     ↓
Message Broker

---

## 10 — `Quick_Revision.md`

```markdown
# Delegates and Events — Quick Revision

---

# Delegate

> A type-safe reference to one or more compatible methods.

```csharp
Action<string> handler = SendEmail;

handler("Hello");

Mental model:

Delegate
→ Pass behavior
Action

Returns void.

Action<string> print =
    message => Console.WriteLine(message);
T → void
Func

Returns a value.

Func<int, int> square =
    x => x * x;

Last generic parameter = return type.

T → TResult
Predicate

Returns bool.

Predicate<int> isEven =
    x => x % 2 == 0;
T → bool
Lambda

Short anonymous function.

x => x * 2

Can be converted to a compatible delegate.

Callback

Pass behavior to another method.

ProcessOrder(
    orderId,
    id => Console.WriteLine(id));

Mental model:

Give me behavior
→ I will call it later
Multicast Delegate

Multiple methods in one delegate.

handler += SendEmail;
handler += SendSms;
handler += WriteAudit;

Remove:

handler -= SendSms;
Event

Delegate-based notification mechanism.

public event EventHandler? OrderCreated;

Subscribe:

OrderCreated += Handler;

Unsubscribe:

OrderCreated -= Handler;

Mental model:

Something happened
→ Notify interested subscribers
Why event instead of delegate?

Event provides encapsulation.

External code can normally:

+=
-=

but cannot directly raise or replace the event.

EventHandler

Standard .NET event pattern.

public event EventHandler? OrderCreated;

Raise:

OrderCreated?.Invoke(
    this,
    EventArgs.Empty);
Custom EventArgs
public sealed class OrderCreatedEventArgs
    : EventArgs
{
    public int OrderId { get; }

    public OrderCreatedEventArgs(int orderId)
    {
        OrderId = orderId;
    }
}

Event:

public event EventHandler<OrderCreatedEventArgs>?
    OrderCreated;
Async Delegate

Prefer:

Func<Task>

or:

Func<T, Task>

Example:

Func<int, Task> process =
    async id =>
    {
        await ProcessAsync(id);
    };

Avoid general-purpose:

async void
Important
async
≠
parallel

Async delegate:

Func<Task>

does not automatically mean parallel execution.

Delegate vs Event
Delegate
→ method reference / behavior

Event
→ notification / publish-subscribe
Delegate vs Interface
Delegate
→ small behavior

Interface
→ substantial object contract/business capability
Event vs Message Broker
C# Event
→ process-local
→ in-memory
→ not durable

Message Broker
→ distributed
→ can be durable
→ independent services
→ retry/acknowledgement patterns

Examples:

RabbitMQ
Kafka
Azure Service Bus
Lambda vs Expression Tree

Delegate:

Func<Order, bool>

means:

Execute behavior

Expression:

Expression<Func<Order, bool>>

means:

Represent/inspect expression

Important for:

Entity Framework Core
LINQ providers
SQL translation
Closure

A lambda can capture an outer variable.

int discount = 10;

Func<int, int> calculate =
    price => price - discount;

discount is captured.

Product-Company Decision Tree
Need to pass small behavior?
        ↓
     Delegate

Something happened and
local components need notification?
        ↓
      Event

Need a substantial business capability?
        ↓
   Interface / Service

Need communication between services?
        ↓
   Message Broker
Most Important Traps
Event
≠
automatic async

Event
≠
message broker

Delegate
≠
interface

Lambda
≠
exactly a delegate

Action
≠
good general async callback

C# event
≠
distributed event
One-Minute Revision
Delegate
→ type-safe method reference

Action
→ void

Func
→ returns value

Predicate
→ bool

Lambda
→ concise anonymous function

Callback
→ pass behavior for later invocation

Multicast
→ multiple methods

Event
→ notification mechanism

EventHandler
→ standard .NET event pattern

EventArgs
→ event data

+=
→ subscribe/add

-=
→ unsubscribe/remove

Func<Task>
→ async callback

C# event
→ process-local

Message broker
→ distributed communication
Final Mental Model
                 C# Delegates & Events
                          |
          ┌───────────────┼───────────────┐
          ↓               ↓               ↓
       Delegate          Event          Lambda
          |               |               |
    Pass behavior    Notify subscribers   |
          |                               |
    ┌─────┼─────┐                         |
    ↓     ↓     ↓                         |
 Action  Func Predicate                   |
                                          |
                                     LINQ / Callbacks

Architecture:

Small behavior
→ Delegate

Local notification
→ Event

Business capability
→ Interface / Service

Distributed communication
→ Message Broker
Final Rule to Remember

Delegate = pass behavior. Event = notify subscribers. Interface = define a capability. Message broker = communicate reliably across distributed components.
# Multicast Delegates and Callbacks in C#

---

# 1. What is a Callback?

A callback is a method supplied to another method so that it can be invoked later.

Example:

```csharp
public void ProcessOrder(
    int orderId,
    Action<int> callback)
{
    Console.WriteLine(
        $"Processing order {orderId}");

    callback(orderId);
}

Usage:

ProcessOrder(
    101,
    id => Console.WriteLine(
        $"Order {id} completed"));

Mental model:

Caller
   ↓
Pass callback
   ↓
ProcessOrder
   ↓
Operation completes
   ↓
Callback invoked
2. Why Callbacks Are Useful?

Callbacks allow the caller to define what should happen next without modifying the processing method.

Useful for:

notifications
logging
transformations
extensibility
asynchronous operations
framework hooks
3. Multicast Delegate

A delegate can contain multiple methods.

Action<string> handler = SendEmail;

handler += SendSms;
handler += WriteAuditLog;

Invocation:

handler("Order created");

Conceptually:

handler
   |
   +-- SendEmail
   |
   +-- SendSms
   |
   +-- WriteAuditLog
4. Removing Handlers
handler -= SendSms;

Now:

SendEmail
WriteAuditLog

remain.

5. Invocation Order

For a multicast delegate:

handler += First;
handler += Second;
handler += Third;

Invocation normally follows the invocation list order:

First
  ↓
Second
  ↓
Third

However, business logic should not generally rely on subscriber ordering unless it is explicitly designed that way.

6. Return Values of Multicast Delegates

Consider:

Func<int> handler = First;
handler += Second;
handler += Third;

When invoked, each method executes, but only the return value of the last invoked method is normally observed through the delegate invocation.

Therefore multicast delegates are much more suitable for:

void notifications

than for aggregating return values.

7. Exceptions in Multicast Delegates

Suppose:

handler += SendEmail;
handler += SendSms;
handler += WriteAudit;

If one handler throws during normal invocation, later handlers may not execute.

Therefore:

Don't assume every subscriber will always execute successfully.

For important workflows, explicitly design failure handling.

8. Delegate Invocation List

You can inspect the invocation list:

foreach (Delegate handler
         in myDelegate.GetInvocationList())
{
    Console.WriteLine(handler.Method.Name);
}

This is useful for advanced diagnostics, although normal application code rarely needs to inspect it.

9. Callback with Result

A callback can return a value:

public int Process(
    int value,
    Func<int, int> operation)
{
    return operation(value);
}

Usage:

int result =
    Process(10, x => x * 2);

Result:

20
10. Async Callback

For asynchronous operations, prefer:

Func<Task>

or:

Func<T, Task>

Example:

public async Task ProcessAsync(
    Func<Task> callback)
{
    Console.WriteLine("Processing...");

    await Task.Delay(100);

    await callback();
}

Usage:

await ProcessAsync(
    async () =>
    {
        await Task.Delay(100);

        Console.WriteLine("Callback completed");
    });
11. Why Not async Action?

Avoid:

Action action = async () =>
{
    await Task.Delay(1000);
};

This becomes an async void callback.

Problems include:

caller cannot await it
exception handling is harder
completion cannot be observed
composition becomes difficult

Prefer:

Func<Task> action = async () =>
{
    await Task.Delay(1000);
};

await action();
12. Callback vs Event
Callback

Usually:

one operation
    ↓
one supplied behavior

Example:

ProcessOrder(callback);
Event

Usually:

something happened
    ↓
zero/many subscribers

Example:

OrderCreated += SendEmail;
OrderCreated += Audit;
13. Callback vs Strategy

A simple strategy can often be represented using a delegate.

Func<decimal, decimal> discountStrategy =
    amount => amount * 0.90m;

For complex strategies, prefer an interface/class.

public interface IDiscountStrategy
{
    decimal Calculate(decimal amount);
}

Use delegates for small behavior.

Use classes/interfaces for complex business behavior.

14. Product-Company Scenario

Suppose an order processor supports different completion actions.

public async Task ProcessAsync(
    int orderId,
    Func<int, Task> onCompleted)
{
    // Business processing

    await Task.Delay(100);

    await onCompleted(orderId);
}

Usage:

await processor.ProcessAsync(
    1001,
    async orderId =>
    {
        await notificationService
            .SendAsync(orderId);
    });

This is a clean callback design.

15. Important Production Considerations

Callbacks can become problematic if:

they perform expensive work synchronously
they block threads
they have hidden side effects
they throw unexpected exceptions
they capture large object graphs
they create lifetime issues
they are retained for too long

For large workflows, explicit services/message queues are often easier to reason about.

16. Key Points
Callback
→ pass behavior to another method

Multicast delegate
→ multiple methods in one invocation list

+=
→ add handler

-=
→ remove handler

Delegate invocation
→ synchronous unless the delegate itself represents async work

Async callback
→ Func<Task> / Func<T, Task>

Avoid
→ async void callbacks

Simple behavior
→ delegate

Complex business behavior
→ interface/class

Distributed reliable workflow
→ message broker, not normal C# delegate
Interview Definition

A callback is a method supplied to another component so that it can be invoked at a defined point,
while a multicast delegate allows multiple compatible methods to be invoked through a single delegate instance.
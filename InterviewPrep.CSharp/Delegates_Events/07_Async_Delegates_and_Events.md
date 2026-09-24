# Async Delegates and Events in C#

---

# 1. Delegates Can Represent Async Methods

A delegate can point to a method returning `Task`.

Example:

```csharp
Func<Task> operation =
    ProcessAsync;

Method:

static async Task ProcessAsync()
{
    await Task.Delay(1000);

    Console.WriteLine("Completed");
}

Usage:

await operation();
2. Async Callback

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

        Console.WriteLine(
            "Callback completed");
    });
3. Generic Async Callback
Func<int, Task> processOrderAsync =
    async orderId =>
    {
        await Task.Delay(100);

        Console.WriteLine(
            $"Order {orderId} processed");
    };

Usage:

await processOrderAsync(1001);
4. Why Avoid async Action?

Avoid:

Action action = async () =>
{
    await Task.Delay(1000);
};

Because Action represents:

() → void

This creates an async void delegate.

Problems:

caller cannot await completion
exception handling is difficult
composition is difficult
cancellation/result handling is poor

Prefer:

Func<Task> action = async () =>
{
    await Task.Delay(1000);
};

await action();
5. Async Delegate with Result
Func<int, Task<string>> getOrderAsync =
    async orderId =>
    {
        await Task.Delay(100);

        return $"Order-{orderId}";
    };

Usage:

string result =
    await getOrderAsync(1001);
6. Async Delegate + WhenAll

Suppose several operations are independent:

Func<int, Task<string>> getDataAsync =
    async id =>
    {
        await Task.Delay(100);

        return $"Data-{id}";
    };

Start operations:

Task<string> task1 = getDataAsync(1);
Task<string> task2 = getDataAsync(2);
Task<string> task3 = getDataAsync(3);

string[] results =
    await Task.WhenAll(
        task1,
        task2,
        task3);

This allows independent async I/O operations to overlap.

7. Async Delegate Mental Model
Func<T, Task<TResult>>
          ↓
Input
          ↓
Async operation
          ↓
Task<TResult>
          ↓
await
          ↓
Result
8. Events Are Different

A normal event:

public event EventHandler? OrderCreated;

uses a void-returning delegate.

Therefore:

OrderCreated?.Invoke(
    this,
    EventArgs.Empty);

does not provide a task to await.

9. Why async void Event Handlers Are Tricky

Example:

service.OrderCreated +=
    async (sender, e) =>
    {
        await SendEmailAsync();
    };

Because EventHandler returns void, the handler becomes effectively:

async void

This means the publisher cannot simply:

await OrderCreated;

There is no Task representing all subscribers.

10. When Is async void Acceptable?

async void is primarily intended for event handlers in framework event models where the framework expects a void handler.

Example:

private async void Button_Click(
    object sender,
    EventArgs e)
{
    await SaveAsync();
}

For general application methods, prefer:

Task
Task<T>
11. Async Event Design

If the application truly needs awaitable event handlers, you can design an explicit async notification abstraction.

For example:

public delegate Task AsyncEventHandler<TEventArgs>(
    object sender,
    TEventArgs args);

Then:

private event AsyncEventHandler<OrderCreatedEventArgs>?
    OrderCreatedAsync;

Invocation:

var handlers =
    OrderCreatedAsync?
        .GetInvocationList();

A production implementation should carefully define:

execution order
parallel vs sequential handlers
exception behavior
cancellation
timeout
duplicate execution
handler lifetime

Do not create an async event abstraction unless there is a real requirement for it.

12. Better Alternative for Business Workflows

For important business operations, consider explicit service calls or messaging rather than complicated in-process async events.

Example:

Order created
      ↓
Persist order
      ↓
Publish durable event/message
      ↓
Consumers process independently

This gives clearer failure and retry semantics.

13. Async Delegates vs Parallelism

Important:

Func<Task>

does not automatically mean parallel execution.

Async allows an operation to yield while waiting.

To run independent operations concurrently:

Task task1 = operation1();
Task task2 = operation2();

await Task.WhenAll(task1, task2);
14. Async Delegate and Cancellation

Prefer passing cancellation when the operation supports it:

Func<CancellationToken, Task> operation;

Example:

Func<CancellationToken, Task> operation =
    async token =>
    {
        await Task.Delay(
            1000,
            token);
    };

Usage:

await operation(cancellationToken);

This makes cancellation explicit.

15. Product-Company Example

Suppose an API needs to call multiple independent services.

Func<CancellationToken, Task<Customer>> getCustomer;
Func<CancellationToken, Task<Order>> getOrder;
Func<CancellationToken, Task<Payment>> getPayment;

These can be started concurrently:

Task<Customer> customerTask =
    getCustomer(token);

Task<Order> orderTask =
    getOrder(token);

Task<Payment> paymentTask =
    getPayment(token);

await Task.WhenAll(
    customerTask,
    orderTask,
    paymentTask);

This is a realistic use of async delegates.

16. Key Points
Async delegate
→ Func<Task> / Func<T, Task>

Async result
→ Func<T, Task<TResult>>

Avoid general async void
→ prefer Task

EventHandler
→ void-returning delegate

async event handler
→ often becomes async void

async
≠
parallel

Concurrent async operations
→ Task.WhenAll

Cancellation
→ include CancellationToken when needed
Interview Definition

For asynchronous callbacks, prefer awaitable delegate types such as Func<Task> or Func<T, Task>; 
standard EventHandler is void-returning, so asynchronous event handlers require special care because they behave as async void.
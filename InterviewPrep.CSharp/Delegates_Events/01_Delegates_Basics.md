# Delegates in C#

## 1. What is a Delegate?

A delegate is a **type-safe reference to a method**.

In simple words:

> A delegate allows us to store a method in a variable and invoke that method later.

Normal method call:

```csharp
PrintMessage("Hello");

Using a delegate:

Action<string> action = PrintMessage;

action("Hello");

Mental model:

Method
   ↓
Delegate reference
   ↓
Invoke later
2. Why Do We Need Delegates?

Delegates allow us to pass behavior as a parameter.

Example:

public void ProcessOrder(Action<string> callback)
{
    Console.WriteLine("Order processed");

    callback("Order123");
}

Usage:

ProcessOrder(SendEmail);

static void SendEmail(string orderId)
{
    Console.WriteLine($"Email sent for {orderId}");
}

The caller decides what should happen after the operation.

Mental model:

ProcessOrder()
      ↓
Business operation
      ↓
Callback
      ↓
Caller-defined behavior
3. Creating a Custom Delegate

Syntax:

public delegate void NotificationHandler(string message);

Compatible method:

static void SendEmail(string message)
{
    Console.WriteLine($"Email: {message}");
}

Assign:

NotificationHandler handler = SendEmail;

Invoke:

handler("Order created");

Complete example:

using System;

public delegate void NotificationHandler(string message);

public class Program
{
    public static void Main()
    {
        NotificationHandler handler = SendEmail;

        handler("Order created");
    }

    static void SendEmail(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}
4. Delegate Signature

A delegate defines the signature that compatible methods must follow.

delegate int Calculator(int a, int b);

Compatible:

static int Add(int a, int b)
{
    return a + b;
}

Usage:

Calculator calculator = Add;

int result = calculator(10, 20);

Result:

30
5. Type Safety

Delegates are type-safe.

For:

delegate int Calculator(int a, int b);

this is valid:

int Add(int a, int b)
{
    return a + b;
}

But this is not compatible:

string GetName()
{
    return "John";
}

The method signature must be compatible with the delegate.

6. Delegate as a Parameter

Delegates are especially useful when a method needs configurable behavior.

public static void Process(
    int value,
    Func<int, int> operation)
{
    int result = operation(value);

    Console.WriteLine(result);
}

Usage:

Process(10, x => x * 2);
Process(10, x => x + 5);

Output:

20
15

The same method can execute different behavior.

7. Multicast Delegates

A delegate can contain multiple method references.

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
8. Removing a Handler

Use -=:

handler -= SendSms;

Now only:

SendEmail
WriteAuditLog

remain.

9. Delegate Invocation

These are equivalent:

handler("Hello");

and:

handler.Invoke("Hello");

For nullable delegates:

handler?.Invoke("Hello");

This safely invokes only when the delegate is not null.

10. Delegates and Callbacks

A callback means:

Give me a method and I will call it when my operation reaches a particular point.

Example:

public void ProcessOrder(
    int orderId,
    Action<int> onCompleted)
{
    Console.WriteLine($"Processing order {orderId}");

    onCompleted(orderId);
}

Usage:

ProcessOrder(
    101,
    orderId =>
    {
        Console.WriteLine(
            $"Order {orderId} completed");
    });
11. Delegate vs Method Call

Normal:

SendEmail("Order created");

Delegate:

Action<string> handler = SendEmail;

handler("Order created");

The important difference is that a delegate allows the method reference itself to be passed around.

12. Where Delegates Are Used

Delegates are heavily used in .NET:

LINQ
callbacks
events
sorting
filtering
transformations
dependency/behavior injection
asynchronous callbacks
functional-style programming

Example:

numbers.Where(x => x > 10);

The lambda is converted to a compatible delegate.

13. Delegate vs Interface
Delegate

Best when you need to pass a relatively small piece of behavior.

Func<Order, bool>
Interface

Better when you need a larger object contract.

public interface IPaymentProcessor
{
    Task ProcessAsync(Order order);

    Task RefundAsync(Order order);
}

Mental model:

Delegate
→ one behavior

Interface
→ object contract / multiple behaviors
14. Product-Company Example

Suppose an order service needs customizable post-processing:

public class OrderProcessor
{
    public void Process(
        int orderId,
        Action<int> afterProcessing)
    {
        Console.WriteLine(
            $"Order {orderId} processed");

        afterProcessing(orderId);
    }
}

Possible behaviors:

OrderProcessor
      |
      +-- SendEmail
      +-- SendSMS
      +-- Audit
      +-- Notification

This avoids hard-coding one specific behavior.

15. Key Points
Delegate = type-safe method reference.
Delegate can be stored in a variable.
Delegate can be passed as a parameter.
Delegate can be returned from a method.
Delegate can reference static or instance methods.
Delegate can be multicast.
+= adds a handler.
-= removes a handler.
Delegate invocation can be used for callbacks.
Events are built on delegates.
LINQ heavily uses delegates.
Delegates are useful for small behavior injection.

Interview Definition

A delegate is a type-safe reference to one or more compatible methods and is 

commonly used for callbacks, behavior injection, LINQ operations, and event handling.
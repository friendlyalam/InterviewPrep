1. Product Company Definition

Composition over Inheritance is an object-oriented design principle that recommends building classes by combining smaller, 
reusable objects (composition) instead of relying heavily on inheritance.

2. Simple Definition

Instead of asking:

"What does this class inherit?"

Ask:

"What objects does this class need?"

Modern applications answer the second question.

-------------------------------------
3. Why Was This Principle Introduced?

Inheritance seems easy.

Vehicle
    │
    ▼
Car
    │
    ▼
ElectricCar
    │
    ▼
SelfDrivingElectricCar

As projects grow:

Deep inheritance trees
Hard-to-maintain code
Changes in base classes affect all derived classes
Difficult testing

This is known as the Fragile Base Class Problem.

--------------------------------------------------------------------------

4. The Problem with Inheritance

Suppose we're building a notification system.

Using Inheritance (Bad Design)
Notification
      │
      ▼
EmailNotification
      │
      ▼
PromotionalEmailNotification
      │
      ▼
FestivalPromotionalEmailNotification

Now imagine adding:

SMS
WhatsApp
Push Notification
Slack
Teams

The hierarchy grows rapidly.

-------------------------------------------------------------------------------

5. Better Design Using Composition

Instead of inheritance:

NotificationService
        │
        ├────────► EmailSender
        ├────────► SmsSender
        ├────────► PushSender
        └────────► WhatsAppSender

Each object has one responsibility.

The NotificationService composes the behavior it needs.

----------------------------------------------------------------------------------

6. Enterprise Example
Requirement

Build an Order Service.

After an order is placed, we need to:

Send Email
Write Log

Should OrderService inherit from EmailService?

❌ No.

Read it:

OrderService is an EmailService.

That doesn't make sense.

Instead:

OrderService
      │
      ├────────► EmailService
      └────────► Logger

This is composition.

-------------------------------------------------------------------------------

7. Complete C# Example
EmailService
using System;

public class EmailService
{
    public void SendEmail(string email)
    {
        Console.WriteLine($"Email sent to {email}");
    }
}
Logger
using System;

public class Logger
{
    public void Log(string message)
    {
        Console.WriteLine($"LOG : {message}");
    }
}
OrderService
using System;

public class OrderService
{
    private readonly EmailService _emailService;
    private readonly Logger _logger;

    public OrderService()
    {
        _emailService = new EmailService();
        _logger = new Logger();
    }

    public void PlaceOrder(string customerEmail)
    {
        Console.WriteLine("Order placed.");

        _logger.Log("Order Created");

        _emailService.SendEmail(customerEmail);
    }
}
Main
using System;

class Program
{
    static void Main()
    {
        OrderService service = new OrderService();

        service.PlaceOrder("alam@example.com");
    }
}
Output
Order placed.

LOG : Order Created

Email sent to alam@example.com
Why Is This Composition?

Because:

OrderService

HAS

EmailService

HAS

Logger

It is not:

OrderService IS-A EmailService

----------------------------------------------------------------------------------------

Real Enterprise Example

Imagine Microsoft Outlook.

It has:

Outlook

↓

Mail Service

↓

Calendar

↓

Contacts

↓

Notification

↓

Search

Outlook doesn't inherit from all these classes.

It uses them.

-----------------------------------------------------------------------------------

Memory Representation
Stack
────────────────────────────

OrderService
      │
      ▼

Heap

OrderService Object

      │
      ├────────► EmailService Object

      │
      └────────► Logger Object

Multiple independent objects collaborate.

-------------------------------------------------------------------------------------

Advantages
1. Low Coupling

Each class knows only what it needs.

2. Easy Testing

Replace:

EmailService

with

FakeEmailService

during unit testing.

3. Reusability

EmailService can be reused by:

OrderService
PaymentService
UserService
InvoiceService
4. Flexibility

Tomorrow,

replace Email with SMS.

No inheritance changes required.

5. Better Maintainability

Each class has one responsibility.
-----------------------------------------------------
Disadvantages
More classes
Slightly more wiring
Requires good design

For enterprise software, these trade-offs are usually worthwhile.

------------------------------------------------------------------------------------------------------

| Inheritance                  | Composition                     |
| ---------------------------- | ------------------------------- |
| IS-A                         | HAS-A                           |
| Tight coupling               | Loose coupling                  |
| Less flexible                | More flexible                   |
| Base changes affect children | Components evolve independently |
| Deep hierarchies possible    | Small collaborating objects     |
| Harder to modify             | Easier to extend                |


--------------------------------------------------------------------------------------------------

Product Company Examples

Instead of:

PaymentService : EmailService

Use:

PaymentService

↓

EmailService

Instead of:

UserService : Logger

Use:

UserService

↓

Logger

Instead of:

InvoiceService : PdfGenerator

Use:

InvoiceService

↓

PdfGenerator

--------------------------------------------------------------------------------

Common Interview Questions
Q1. Why is Composition preferred?

Because it provides:

Loose coupling
Better maintainability
Higher flexibility
Easier testing
Better code reuse
Q2. Does this mean inheritance is bad?

No.

Inheritance is appropriate when there is a genuine IS-A relationship.

Examples:

Vehicle

↓

Car
Animal

↓

Dog

Those are good uses of inheritance.

Q3. Can we use both together?

Yes.

Enterprise applications often combine them.

Example:

Employee

↓

Manager

Inheritance.

Manager also has:

Logger

EmailService

NotificationService

Composition.

Q4. Which is used more in ASP.NET Core?

Composition.

Examples include:

Dependency Injection
Logging
Configuration
Repositories
Services
Middleware

These are composed together rather than inherited.

Best Practices

Use Inheritance when:

There is a true IS-A relationship.
The base class represents a stable abstraction.
Derived classes naturally extend the base behavior.

Use Composition when:

A class needs functionality from other classes.
Behavior may change over time.
You want flexibility, reuse, and easier testing.
Interview Summary
Need specialization?

        │
      YES
        │
Use Inheritance

----------------------------

Need functionality?

        │
      YES
        │
Use Composition
Product Company Insight

One refinement to the example above: in modern ASP.NET Core, you typically don't instantiate dependencies with new inside
OrderService. Instead, you inject them through the constructor:

public class OrderService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IEmailService emailService,
        ILogger<OrderService> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }
}

This still follows Composition over Inheritance, while also applying Dependency Injection and the Dependency Inversion Principle.
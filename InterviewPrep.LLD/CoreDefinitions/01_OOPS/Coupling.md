1. Product Company Definition

Coupling is the degree of dependency between two classes, modules, or components. It measures how much one piece of code knows about or relies on another piece of code.

Interview Definition

Coupling describes how strongly two software components are connected. Lower coupling is generally preferred because changes in one component have less impact on others.

---------------------------------------------------------------

2. Simple Definition

Think of two electrical appliances.

Tightly Coupled
Bulb permanently soldered to the wire

If the bulb fails, replacing it is difficult.

Loosely Coupled
Bulb connected through a socket

You can replace the bulb without changing the wiring.

That socket is exactly what an interface does in software.

---------------------------------------------------------------------
High Coupling

A custom-built machine where every part is welded together.

Part A ──welded── Part B ──welded── Part C

Change Part B?

You may need to cut and rebuild the entire machine.

Low Coupling

A USB device.

Laptop  ⇄  USB Device

You can replace the USB device without changing the laptop.

That is the goal of good software design.

---------------------------------------------------------------------------------

4. Why Is Coupling Important?

Imagine an e-commerce application.

When an order is placed:

Save order

Send email

Send SMS

Update inventory

Create invoice

Publish analytics event

If OrderService directly creates every dependency, it becomes tightly coupled.

-------------------------------------------------------------------------------------------

5. High Coupling (Bad Design)
C# Example
using System;

public class EmailService
{
    public void Send(string email)
    {
        Console.WriteLine($"Email sent to {email}");
    }
}

public class OrderService
{
    private EmailService _emailService;

    public OrderService()
    {
        _emailService = new EmailService();
    }

    public void PlaceOrder(string email)
    {
        Console.WriteLine("Order placed");

        _emailService.Send(email);
    }
}

------------------------------------------------------------------------------

Why Is This Tightly Coupled?

Look at this line:

_emailService = new EmailService();

OrderService knows:

The exact class (EmailService)

How to create it

Which constructor it uses

Now imagine the business says:

"Replace EmailService with SendGrid."

You must modify OrderService.

That is tight coupling.

----------------------------------------------------------------------------------

Problems with Tight Coupling

Hard to test

Hard to replace implementations

Hard to maintain

Violates the Dependency Inversion Principle

Changes ripple through the system

----------------------------------------------------------------------------------

6. Low Coupling (Good Design)
Step 1 - Interface
public interface IEmailService
{
    void Send(string email);
}
Step 2 - Implementation
using System;

public class EmailService : IEmailService
{
    public void Send(string email)
    {
        Console.WriteLine($"Email sent to {email}");
    }
}
Step 3 - OrderService
using System;

public class OrderService
{
    private readonly IEmailService _emailService;

    public OrderService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void PlaceOrder(string email)
    {
        Console.WriteLine("Order placed");

        _emailService.Send(email);
    }
}
Step 4 - Main
using System;

class Program
{
    static void Main()
    {
        IEmailService emailService = new EmailService();

        OrderService orderService =
            new OrderService(emailService);

        orderService.PlaceOrder("alam@example.com");
    }
}
Output
Order placed
Email sent to alam@example.com

------------------------------------------------------------------------------

What Changed?

Before:

OrderService
      ↓
EmailService

After:

OrderService
      ↓
IEmailService
      ↓
EmailService

Now OrderService depends on an abstraction, not a concrete class.

This is low coupling.

-----------------------------------------------------------------------------

Memory Representation

Low-coupling object graph

OrderService

IEmailService

EmailService

The important point:

OrderService only knows IEmailService.

The actual object may be EmailService, SendGridEmailService, AwsSesEmailService, etc.

--------------------------------------------------------------------------------

Where Product Companies Care Most
Unit Testing

With tight coupling:

OrderService service = new OrderService();

You cannot easily replace the email sender.

With loose coupling:

IEmailService fake = new FakeEmailService();
OrderService service = new OrderService(fake);

Now you can test OrderService without sending real emails.

----------------------------------------------------------
How ASP.NET Core Reduces Coupling

You write:

builder.Services.AddScoped<IEmailService, EmailService>();

Then:

public class OrderService
{
    public OrderService(IEmailService emailService)
    {
    }
}

The framework provides the implementation.

Your class remains loosely coupled.

------------------------------------------------
Common Interview Questions
Q1. What is coupling?

The degree of dependency between software components.

Q2. Which is better?

Usually low coupling.

Q3. How do we reduce coupling in C#?

Interfaces

Dependency Injection

Events

Message queues

Composition over Inheritance

Q4. Is zero coupling possible?

Not in a useful application.

Some level of dependency is always necessary.

The goal is manageable coupling.

Q5. Why do interfaces reduce coupling?

Because the consumer depends on a contract rather than a specific implementation.

-----------------------------------------------------------------------------------

Best Practices

Best practices

Depend on interfaces, not concrete classes.

Use constructor injection for required dependencies.

Keep classes focused on a single responsibility.

Avoid creating dependencies with new inside business logic.

Prefer composition over inheritance when sharing functionality.

----------------------------------------------------------------------------------

| Feature                    | Tight Coupling                              | Loose Coupling                                 |
| -------------------------- | ------------------------------------------- | ---------------------------------------------- |
| Definition                 | Classes are highly dependent on each other. | Classes have minimal dependency on each other. |
| Dependency                 | Depends on concrete classes.                | Depends on abstractions (interfaces).          |
| Flexibility                | Low                                         | High                                           |
| Maintainability            | Difficult                                   | Easy                                           |
| Testing                    | Hard to unit test                           | Easy to mock and test                          |
| Code Reusability           | Low                                         | High                                           |
| Impact of Changes          | Changes in one class affect many classes.   | Changes are mostly isolated.                   |
| Dependency Injection       | Usually not used                            | Commonly used                                  |
| SOLID Principle            | Often violates DIP                          | Follows DIP                                    |
| Product Company Preference | ❌ Avoid when possible                       | ✅ Preferred                                    |


------------------------------------------------------------------------------------------------------------------------
Real life examples

| Scenario        | Tight Coupling                                           | Loose Coupling                                             |
| --------------- | -------------------------------------------------------- | ---------------------------------------------------------- |
| Mobile Charger  | Phone supports only one proprietary charger.             | Phone supports USB-C chargers from different brands.       |
| Car Tyres       | Tyres fit only one specific car model.                   | Standard tyre sizes fit many compatible cars.              |
| Television      | Remote works only with one TV model.                     | Universal remote works with many TVs.                      |
| Printer         | Printer accepts only one manufacturer's cartridges.      | Printer supports standard cartridges.                      |
| Computer        | Components are permanently soldered and hard to replace. | RAM, SSD, and GPU can be replaced independently.           |
| Home Appliances | Bulb fixed permanently into the fixture.                 | Standard E27 bulb can be replaced by any compatible brand. |
| Railway Track   | Train designed for only one track gauge.                 | Train designed to operate on standardized tracks.          |
| Power Adapter   | Device requires one specific adapter.                    | Device charges with any USB-C PD compatible charger.       |


--------------------------------------------------------------------------------------------------------------------------------------------

Software examples

| Tight Coupling                                              | Loose Coupling                                          |
| ----------------------------------------------------------- | ------------------------------------------------------- |
| `OrderService` directly creates `EmailService` using `new`. | `OrderService` depends on `IEmailService`.              |
| `PaymentService` directly creates `StripeGateway`.          | `PaymentService` depends on `IPaymentGateway`.          |
| `ReportService` directly creates `PdfGenerator`.            | `ReportService` depends on `IReportGenerator`.          |
| `NotificationService` directly creates `SmsSender`.         | `NotificationService` depends on `INotificationSender`. |
| `InvoiceService` directly creates `SqlRepository`.          | `InvoiceService` depends on `IInvoiceRepository`.       |


-----------------------------------------------------------------------------------------------------------------------------

Product Company Examples

| Company       | Loose Coupling Example                                                                       |
| ------------- | -------------------------------------------------------------------------------------------- |
| Microsoft     | ASP.NET Core Dependency Injection                                                            |
| Amazon        | Different payment gateway implementations behind a common interface                          |
| Netflix       | Microservices communicating through APIs/events instead of direct implementation knowledge   |
| Uber          | Multiple map providers hidden behind a common abstraction                                    |
| Swiggy/Zomato | Different payment providers (Razorpay, Stripe, Paytm) implementing the same payment contract |

--------------------------------------------------------------
Product Company Summary
| Tight Coupling                   | Loose Coupling                       |
| -------------------------------- | ------------------------------------ |
| Concrete Class                   | Interface/Abstraction                |
| `new EmailService()`             | `IEmailService`                      |
| Hard to Replace                  | Easy to Replace                      |
| Hard to Test                     | Easy to Mock                         |
| High Dependency                  | Low Dependency                       |
| Less Flexible                    | Highly Flexible                      |
| Avoid in Enterprise Applications | Preferred in Enterprise Applications |



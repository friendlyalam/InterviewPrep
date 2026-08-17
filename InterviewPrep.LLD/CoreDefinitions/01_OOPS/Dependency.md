1. Product Company Definition

Dependency is a relationship where one class temporarily uses another class to perform a specific task but does not own or permanently maintain it.

Interview Definition

Dependency is a "uses" relationship in which one object depends on another object to complete an operation. 
The dependency is usually passed as a method parameter, local variable, or injected through a constructor.

2. Simple Definition

Imagine an Order Service.

To send an order confirmation, it needs an Email Service.

OrderService
      │
      ▼
EmailService

OrderService cannot send an email by itself.

It depends on EmailService.

----------------------------------------------------------
3. Why Do We Need Dependency?

Suppose you're building an e-commerce application.

When an order is placed, you need to:

Send email
Send SMS
Generate invoice
Write logs

Should OrderService perform all of these tasks?

❌ No.

Each responsibility belongs to another class.

So OrderService depends on these services.

-------------------------------------------------------------------
Real Product Company Examples
| Class                 | Depends On        |
| --------------------- | ----------------- |
| OrderService          | EmailService      |
| PaymentService        | BankGateway       |
| InvoiceService        | PdfGenerator      |
| NotificationService   | SmsProvider       |
| AuthenticationService | JwtTokenGenerator |
| ReportService         | ExcelExporter     |

------------------------------------------------------------------------
Dependency vs Association

Many developers ask:

Aren't they both "uses" relationships?

Yes, but there is a subtle difference.

Association
Doctor -------- Patient

The relationship is part of the business model.

A doctor and patient are related entities.

Dependency
ReportService
       │
       ▼
PdfGenerator

The service only needs another class to perform a task.

It is not modelling a business relationship.

------------------------------------------------------------------------------------
Dependency vs Aggregation

Aggregation:

Company
     │
     ▼
Employees

The company stores references to employees.

Dependency:

OrderService
      │
      ▼
EmailService

The service simply uses another object.

-----------------------------------------------------------------------------

Dependency vs Composition

Composition:

Order
     │
     ▼
ShippingAddress

The parent owns the child.

Dependency:

OrderService
      │
      ▼
EmailService

No ownership exists.

------------------------------------------------------------------------------------------
Common Interview Questions
Q1. What is Dependency?

A relationship where one class temporarily uses another class to perform a task.

Q2. Is Dependency a HAS-A relationship?

No.

It is primarily a uses relationship.

Q3. How is Dependency implemented?

Usually by:

Method parameters
Constructor injection
Property injection
Q4. Why is Dependency important?

Because it separates responsibilities and makes classes easier to maintain, test, and replace.

Q5. How does this relate to Dependency Injection?

Dependency Injection is a technique for providing dependencies to a class instead of having the class create them itself.

Example:

public class OrderService
{
    private readonly EmailService _emailService;

    public OrderService(EmailService emailService)
    {
        _emailService = emailService;
    }
}

The relationship is still a dependency, but the dependency is supplied from the outside.

---------------------------------------------------------------------------------------------------------

Best Practices

✅ A class should depend only on the services it actually needs.

✅ Avoid creating dependencies inside business methods:

// Less flexible
EmailService emailService = new EmailService();

Prefer supplying them from outside, especially in enterprise applications.

------------------------------------------------------------------------------------------------

Interview Summary
Dependency
      │
      ▼
Uses Relationship
      │
      ▼
Temporary Collaboration
      │
      ▼
No Ownership
      │
      ▼
Usually Passed as Parameter or Injected

-----------------------------------------------------------------------------------

Product Company Insight

There's one important point that experienced interviewers often explore.

In modern .NET applications, you rarely see this:

var emailService = new EmailService();

inside OrderService.

Instead, you'll usually see an interface and constructor injection:

public class OrderService
{
    private readonly IEmailService _emailService;

    public OrderService(IEmailService emailService)
    {
        _emailService = emailService;
    }
}

This still represents a dependency, but now the class depends on an abstraction rather than a concrete implementation.
That design is the foundation of Dependency Injection (DI) and the Dependency Inversion Principle (DIP), both of which are heavily used in product-company codebases.


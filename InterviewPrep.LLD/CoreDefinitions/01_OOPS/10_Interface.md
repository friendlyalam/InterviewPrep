1.Product Company Definition

An interface is a contract that defines what an object must do without specifying how it should do it. 
Any class implementing the interface is responsible for providing the implementation.

2. Simple Definition

Think of an interface as a promise.

If a class says:

I implement IStorageService

it is promising:

"I will implement every method defined in IStorageService."

----------------------------------------------------------------------------------
3. Why was Interface Introduced?

Imagine an e-commerce application.

Initially, the company supports only email notifications.

OrderService

↓

EmailNotification

Your code might look like:

EmailNotification notification = new EmailNotification();
notification.Send();

Everything works.

Six months later, the business says:

We also need:

SMS
Push Notifications
WhatsApp
Slack
Microsoft Teams

Without interfaces:

OrderService

↓

EmailNotification

↓

SmsNotification

↓

PushNotification

↓

WhatsAppNotification

Your OrderService becomes tightly coupled to concrete classes and must change whenever a new notification type is introduced.

This violates the Open/Closed Principle (OCP).

-----------------------------------------------------------------------------------
4. Solution using an Interface

Instead of depending on a specific class:

OrderService

↓

INotificationService

↓

EmailNotification

SmsNotification

PushNotification

WhatsAppNotification

Now OrderService doesn't care which implementation it receives.

It only knows:

Send()

--------------------------------------------------------------------
5. Real-Life Analogy

A mobile charger.

The wall socket defines:

Voltage

Frequency

Pin layout

It does not care whether you connect:

Samsung charger
Apple charger
OnePlus charger

As long as the charger follows the socket's requirements.

The socket acts like an interface.

---------------------------------------------------------------------------------
6. Another Real-Life Example

A payment terminal accepts:

Visa
Mastercard
RuPay

The machine expects a standard way to process a payment.

Each card implements that standard differently.

--------------------------------------------------------------------
7. Interface Syntax
public interface INotificationService
{
    void Send(string message);
}

Notice:

No implementation.
Only the contract.

----------------------------------------------------------------
8. Enterprise-Level Example
Step 1 - Interface
public interface IStorageProvider
{
    void Upload(string fileName);
}
Step 2 - Azure Storage
public class AzureStorageProvider : IStorageProvider
{
    public void Upload(string fileName)
    {
        Console.WriteLine($"Uploading {fileName} to Azure Blob Storage");
    }
}
Step 3 - AWS Storage
public class AwsStorageProvider : IStorageProvider
{
    public void Upload(string fileName)
    {
        Console.WriteLine($"Uploading {fileName} to Amazon S3");
    }
}
Step 4 - Google Cloud Storage
public class GoogleCloudStorageProvider : IStorageProvider
{
    public void Upload(string fileName)
    {
        Console.WriteLine($"Uploading {fileName} to Google Cloud Storage");
    }
}
Step 5 - Business Layer
public class FileManager
{
    private readonly IStorageProvider _storage;

    public FileManager(IStorageProvider storage)
    {
        _storage = storage;
    }

    public void Save(string fileName)
    {
        _storage.Upload(fileName);
    }
}
Step 6 - Usage
class Program
{
    static void Main()
    {
        IStorageProvider storage =
            new AzureStorageProvider();

        FileManager manager =
            new FileManager(storage);

        manager.Save("Resume.pdf");
    }
}

Output

Uploading Resume.pdf to Azure Blob Storage

Tomorrow:

IStorageProvider storage =
    new AwsStorageProvider();

No changes inside FileManager.

--------------------------------------------------------------------------
9. Memory Representation
IStorageProvider storage =
    new AzureStorageProvider();

Memory

Stack
────────────────────────────

storage

        │

        ▼

Heap
────────────────────────────

AzureStorageProvider Object

Upload()

Notice:

There is no interface object.

An interface cannot be instantiated.

The interface reference points to the implementing object's memory.

-------------------------------------------------------------------------------------

10. Why Can't We Create an Interface Object?

This is invalid:

IStorageProvider storage =
    new IStorageProvider();

Why?

Because an interface only defines what must exist.

It does not provide an implementation.

The CLR cannot create an object without implementation.

------------------------------------------------------------------------------------------

11. Characteristics of an Interface
Defines a contract.
Cannot be instantiated.
Supports runtime polymorphism.
A class can implement multiple interfaces.
An interface can inherit from other interfaces.
Promotes loose coupling.
Encourages dependency inversion.
Ideal for Dependency Injection.

-------------------------------------------------------------------------------------
12. Advantages
Loose Coupling

Business logic depends on abstractions instead of concrete classes.

Easier Testing

You can replace a real implementation with a fake or mock implementation.

Better Maintainability

New implementations can often be added without changing existing business logic.

Better Extensibility

Adding a new storage provider usually means creating a new class rather than modifying existing code.

Supports SOLID

Interfaces play a central role in:

Interface Segregation Principle (ISP)
Dependency Inversion Principle (DIP)
Open/Closed Principle (OCP)

-------------------------------------------------------------------------------------------------

13. Disadvantages
Too many interfaces can make a project harder to navigate.
Creating an interface for every tiny class without a real need can overcomplicate the design.
Choosing poor interface names can make APIs confusing.

-----------------------------------------------------------------------------------------------
| Interface                              | Abstract Class                       |
| -------------------------------------- | ------------------------------------ |
| Defines a contract                     | Defines a partial implementation     |
| No instance state (traditionally)      | Can contain fields and state         |
| Multiple interfaces can be implemented | Only one base class can be inherited |
| Best for capabilities/roles            | Best for sharing common behaviour    |


------------------------------------------------------------------------------------------------
A common interview question:

When should I use an interface instead of an abstract class?

Answer:

Use an interface when unrelated classes need the same capability or when you want loose coupling.
Use an abstract class when related classes share common state or implementation.

Imagine your system has different payment methods:

Payment Methods
│
├── UPI
├── Credit Card
└── Debit Card

1. Interface — common capability

Suppose the business requirement is:

Every payment method must be able to process a payment.

public interface IPaymentProcessor
{
    Task ProcessPaymentAsync(decimal amount);
}

Now:

public class UpiPaymentProcessor : IPaymentProcessor
{
    public async Task ProcessPaymentAsync(decimal amount)
    {
        // UPI-specific implementation
    }
}
public class CreditCardPaymentProcessor : IPaymentProcessor
{
    public async Task ProcessPaymentAsync(decimal amount)
    {
        // Credit-card-specific implementation
    }
}

Why interface?

Because UPI and Credit Card processors don't need to share a parent implementation.

The interface simply says:

"If you are a payment processor, you MUST be able to process a payment."

And your business service can depend only on the abstraction:

public class PaymentService
{
    private readonly IPaymentProcessor _processor;


    public PaymentService(IPaymentProcessor processor)
    {
        _processor = processor;
    }


    public Task PayAsync(decimal amount)
    {
        return _processor.ProcessPaymentAsync(amount);
    }
}

This is loose coupling.


2. Abstract class — shared enterprise implementation

Now suppose all your payment processors need the same common functionality:

PaymentProcessor
│
├── ValidateAmount()
├── LogTransaction()
├── GenerateTransactionId()
│
├── UPI
├── Credit Card
└── Debit Card

Then an abstract class can provide the shared implementation:

public abstract class PaymentProcessor
{
    protected void ValidateAmount(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Invalid amount.");
    }


    protected void LogTransaction()
    {
        Console.WriteLine("Transaction logged.");
    }


    public abstract Task ProcessPaymentAsync(decimal amount);
}

Then:

public class UpiPaymentProcessor : PaymentProcessor
{
    public override async Task ProcessPaymentAsync(decimal amount)
    {
        ValidateAmount(amount);


        // UPI-specific logic


        LogTransaction();
    }
}
public class CreditCardPaymentProcessor : PaymentProcessor
{
    public override async Task ProcessPaymentAsync(decimal amount)
    {
        ValidateAmount(amount);


        // Credit-card-specific logic


        LogTransaction();
    }
}

Now the children share actual implementation.

That's when an abstract class becomes useful.

The real difference
Interface
IPaymentProcessor
       │
       ├── UPI
       ├── Credit Card
       └── Debit Card

Meaning:

"These classes have the same capability/contract."

Abstract class
PaymentProcessor
       │
       ├── common validation
       ├── common logging
       ├── common state
       │
       ├── UPI
       ├── Credit Card
       └── Insurance

Meaning:

"These classes belong to the same conceptual family AND should reuse common code/state."

⭐ Enterprise interview answer

If Microsoft/interviewer asks:

"When would you use an interface vs abstract class?"

Say:

I use an interface when I need to define a capability or contract and want loose coupling between consumers and implementations.
I use an abstract class when closely related classes need to share common state or implementation while still allowing specialized behavior through abstract or virtual members.

And don't think:

Interface = unrelated classes always
Abstract class = related classes always

That's too simplistic.

The real deciding factor is:

Do I mainly need a contract/capability? → Interface

Do I need shared state or reusable implementation? → Abstract class

----------------------------------------------------------------------------------------------------
15. Interface vs Inheritance

Inheritance

Vehicle

↓

Car

Means:

Car is a Vehicle.

Interface

IPrintable

↓

Invoice

↓

Report

↓

Prescription

Means:

These classes can be printed.

An interface usually models a capability, not an "is-a" relationship.

-------------------------------------------------------------------------------------------
16. Common Mistakes
Mistake 1

Creating interfaces for every class without a reason.

Not every class needs an interface.

Mistake 2

Using inheritance where an interface is more appropriate.

Mistake 3

Creating very large interfaces.

Example:

IUserService

↓

Login()

Logout()

Register()

Delete()

ResetPassword()

GenerateReport()

Export()

Import()

SendEmail()

CreateInvoice()

This violates the Interface Segregation Principle (ISP).

-----------------------------------------------------------------------------------------
17. Product Company Interview Questions
Q1. What is an interface?

A contract that defines what a class must implement.

Q2. Why do we use interfaces?

To achieve loose coupling, runtime polymorphism, and extensibility.

Q3. Can an interface have constructors?

No.

Interfaces cannot be instantiated.

Q4. Can an interface contain fields?

No instance fields.

Q5. Can a class implement multiple interfaces?

Yes.

public class FileService :
    IStorageProvider,
    ILogger,
    IAuditable
{
}
Q6. Why do product companies prefer interfaces?

Because they reduce coupling and make applications easier to test, extend, and maintain.

Q7. Is an interface faster than an abstract class?

Performance differences are usually negligible in business applications. The choice should be based on design, not micro-optimisation.

Q8. Which design patterns heavily use interfaces?
Strategy
Factory
Repository
Decorator
Adapter
Command
Mediator
Observer

Q9. Which enterprise technologies rely on interfaces?
Dependency Injection
ASP.NET Core
Entity Framework Core
Logging (ILogger)
Caching
Authentication
Messaging

----------------------------------------------------------------------------------------------
18. Interview-Ready Definition

An interface is a contract that specifies a set of operations a class must implement. It enables loose coupling, runtime polymorphism, 
and extensible designs by allowing software to depend on abstractions rather than concrete implementations.

-----------------------------------------------------------------------------------------------------------------------------

19. One Important Product-Company Insight

Many developers say:

"An interface is used for multiple inheritance."

That answer is incomplete.

The primary reason modern product companies use interfaces is not multiple inheritance.

They use interfaces because they enable:

Loose coupling
Dependency Injection
Unit testing
Runtime polymorphism
Clean Architecture
SOLID principles
Easily replaceable implementations

Those are the reasons you'll encounter in real enterprise applications.

---------------------------------------------------------------------------------

| Feature               | Class                                              | Interface                                                                                                            |
| --------------------- | -------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------- |
| Definition            | A blueprint that contains data and implementation. | A contract that defines what a class must do.                                                                        |
| Purpose               | Represents an object with state and behavior.      | Defines capabilities/behavior without implementation (except default/static members in modern C#).                   |
| Object Creation       | ✅ Object can be created (if not abstract).         | ❌ Cannot create an object directly.                                                                                  |
| Constructors          | ✅ Supported                                        | ❌ Not supported (interfaces cannot have instance constructors).                                                      |
| Fields                | ✅ Allowed                                          | ❌ Instance fields are not allowed.                                                                                   |
| Properties            | ✅ Allowed                                          | ✅ Allowed (typically declarations; modern C# also supports default implementations in some cases).                   |
| Methods               | ✅ Can have implemented methods                     | ✅ Method declarations; can also have default implementations in modern C#.                                           |
| Access Modifiers      | public, private, protected, internal, etc.         | Members are public by contract (with newer C# allowing additional modifiers in specific scenarios).                  |
| Inheritance           | Single class inheritance                           | Multiple interface inheritance                                                                                       |
| Multiple Inheritance  | ❌ Not supported for classes                        | ✅ Supported                                                                                                          |
| State (Data)          | ✅ Can store data in fields                         | ❌ Cannot store instance state                                                                                        |
| Static Members        | ✅ Supported                                        | ✅ Supported (modern C#)                                                                                              |
| Abstract Members      | ✅ Supported in abstract classes                    | ✅ Implicitly abstract unless a default implementation is provided                                                    |
| Implementation        | Complete or partial                                | Contract-focused; implementation is provided by implementing classes (or default interface members where applicable) |
| Memory Allocation     | Object stored on heap                              | No object exists for the interface itself                                                                            |
| Relationship          | IS-A                                               | CAN-DO / Capability                                                                                                  |
| Product Company Usage | Represents entities                                | Defines contracts for flexibility and loose coupling                                                                 |

--------------------------------------------------

When Should You Use a Class?

Use a class when:

You need to store data.
You need constructors.
You need fields.
You need an object.
You are modelling a real entity.

Examples:

Employee
Product
Order
Customer
Invoice
When Should You Use an Interface?

Use an interface when:

Multiple implementations are possible.
You want loose coupling.
You want Dependency Injection.
You want easier testing.
You want a contract.

Examples:

ILogger
IRepository
INotificationService
IPaymentGateway
ICacheService

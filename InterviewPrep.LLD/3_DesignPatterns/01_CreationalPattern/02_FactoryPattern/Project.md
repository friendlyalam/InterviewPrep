Enterprise Project

Payment Gateway Platform

Business Requirement

An e-commerce platform allows customers to pay using multiple payment methods.

Currently supported:

Credit Card
UPI
Wallet
Net Banking

Future payment methods may include:

Stripe
PayPal
Apple Pay
Google Pay
Amazon Pay

The Checkout Service should not know how each payment provider works. It should simply ask for a payment processor and process the payment.

This is exactly the problem that Factory Method solves.

Final Folder Structure
02_FactoryMethodPattern
│
├── Models
│      PaymentRequest.cs
│      PaymentResponse.cs
│
├── Enums
│      PaymentMethod.cs
│
├── Interfaces
│      IPaymentProcessor.cs
│
├── Factories
│      PaymentProcessorFactory.cs
│
├── Processors
│      CreditCardPaymentProcessor.cs
│      UpiPaymentProcessor.cs
│      WalletPaymentProcessor.cs
│      NetBankingPaymentProcessor.cs
│
└── Program.cs
Step 1 : PaymentRequest
Why do we need it?

In real applications, a payment processor requires information such as:

Amount
Currency
Customer
Order
Payment Method

Instead of passing five or six parameters to every method, we create a request model.

Models/PaymentRequest.cs
namespace FactoryMethodPattern.Models;

public class PaymentRequest
{
    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public PaymentMethod PaymentMethod { get; set; }
}
Why Guid for OrderId?

Because enterprise systems generally use unique identifiers instead of integers.

Example:

8fdad418-07c2-40e2-b0d2-38d2f23fd65f

This avoids collisions across distributed systems.

Why decimal for Amount?

Never use

double

or

float

for money.

Always use

decimal

because it provides higher precision for financial calculations.

Step 2 : PaymentResponse

Every payment gateway returns some response.

Instead of returning

true

or

false

we return a proper response object.

Models/PaymentResponse.cs
namespace FactoryMethodPattern.Models;

public class PaymentResponse
{
    public bool IsSuccess { get; set; }

    public string TransactionId { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}
Why not return bool?

Imagine this:

bool result = ProcessPayment();

If

false

is returned,

Can we answer?

Why did it fail?
Which transaction failed?
What should be shown to the customer?

No.

A response object is much more informative.

Step 3 : PaymentMethod Enum

Instead of using strings like

"UPI"

or

"CreditCard"

we use an enum.

Enums/PaymentMethod.cs
namespace FactoryMethodPattern.Enums;

public enum PaymentMethod
{
    CreditCard,

    Upi,

    Wallet,

    NetBanking
}
Why Enum?

Suppose a developer writes

"creditcard"

Another writes

"Credit Card"

Another writes

"CARD"

All are different strings.

Enums eliminate spelling mistakes and improve type safety.

Current Class Diagram
                    PaymentRequest
                    -----------------------
                    OrderId
                    Amount
                    Currency
                    CustomerEmail
                    PaymentMethod
                           │
                           │
                           ▼
                    PaymentMethod (Enum)
                    --------------------
                    CreditCard
                    Upi
                    Wallet
                    NetBanking


                    PaymentResponse
                    -----------------------
                    IsSuccess
                    TransactionId
                    Message
Why We Haven't Written the Factory Yet

Many beginners start with this:

new PaymentProcessorFactory()

But what is the factory going to create?

The concrete payment processors don't exist yet.

In enterprise development, we first define:

Domain models ✅
Contracts (interfaces)
Implementations
Factory
Client

This keeps dependencies flowing in the right direction.

Product Company Discussion

Notice that none of these classes know anything about:

Credit Card
UPI
Wallet
Factory

These are domain models.

They simply represent the data flowing through the system.

This separation of concerns is a hallmark of clean architecture and enterprise design.
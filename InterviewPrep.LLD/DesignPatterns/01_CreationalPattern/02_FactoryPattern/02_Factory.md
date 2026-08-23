1. Product Company Definition

Factory Method is a Creational Design Pattern that defines an interface (or method) for creating objects but allows subclasses 
or factory classes to decide which concrete object should be created.

Read it carefully.

The important part is

The client asks for an object without knowing which concrete class is being created.

This is the heart of Factory Method.

--------------------------------------------------------------------------------------------------------------------------------------------

2. Simple Definition

Suppose you go to a coffee shop.

You ask for

Coffee

The cashier asks

Which one?
Cappuccino
Latte
Espresso

You don't make the coffee yourself.

The coffee machine creates it.

The coffee machine acts like a Factory.

--------------------------------------------------------------------------------------------------------------------------------------------

3. Why was Factory Method Introduced?

Suppose we don't use Factory Method.

if(paymentType == "CreditCard")
{
    payment = new CreditCardPayment();
}
else if(paymentType == "UPI")
{
    payment = new UpiPayment();
}
else if(paymentType == "Wallet")
{
    payment = new WalletPayment();
}
else if(paymentType == "NetBanking")
{
    payment = new NetBankingPayment();
}

Now imagine tomorrow business adds

Apple Pay
Google Pay
PayPal

Again we modify this code.

Next year

Amazon Pay
Samsung Pay

Again we modify.

Every new payment provider forces changes to existing code.

This violates

OCP
Maintainability
Scalability

-------------------------------------------------------------------------------

4. Problem Without Factory Method

Imagine an E-commerce Platform.

Checkout

↓

if()

↓

else if()

↓

else if()

↓

else if()

↓

Payment

Every new payment method

↓

Developer modifies Checkout Service.

Soon

CheckoutService

5000 lines

Very common problem in legacy systems.

--------------------------------------------------------------------------------------------------------------------------------------------

5. Real-Life Example 1
Restaurant

Customer says

Pizza

Kitchen decides

Veg Pizza
Cheese Pizza
Paneer Pizza

Customer never cooks.

Kitchen creates.

Kitchen = Factory.

--------------------------------------------------------------------------------------------------------------------------------------------

6. Real-Life Example 2
Car Showroom

Customer says

SUV

Showroom decides

Hyundai Creta
Tata Harrier
Mahindra XUV700

Customer never creates the car.

Showroom creates.

Showroom = Factory.

-----------------------------------------------------------------------------------

7. Enterprise Scenario

We'll build

Payment Gateway Platform

Supported providers

Credit Card
UPI
Wallet
Net Banking

Business says

Tomorrow

Stripe

Next month

Razorpay

Next year

PayPal

Business logic should not change.

Only Factory should know how to create the correct payment processor.

--------------------------------------------------------------------------------------------------------------------------------------------

8. Why Payment Gateway?

Because almost every enterprise application has multiple payment providers.

Examples

Amazon
Flipkart
Swiggy
Zomato
Azure Marketplace
Google Play
Microsoft Store

Factory Method fits this scenario perfectly.

--------------------------------------------------------------------------------------------------------------------------------------------

9. Characteristics
Encapsulates object creation.
Hides concrete implementation.
Client depends on abstraction.
Easy to extend.
Supports OCP.
Reduces coupling.

--------------------------------------------------------------------------------------------------------------------------------------------
10. Advantages

| Advantage              | Explanation                                       |
| ---------------------- | ------------------------------------------------- |
| Loose Coupling         | Client doesn't know concrete class.               |
| Easy Extension         | Add new provider without changing business logic. |
| Better Maintainability | Creation logic stays in one place.                |
| Better Testing         | Factory can be mocked or replaced.                |
| Follows OCP            | Existing code remains unchanged.                  |


--------------------------------------------------------------------------------------------------------------------------------------------

11. Disadvantages

| Disadvantage                | Explanation                                     |
| --------------------------- | ----------------------------------------------- |
| More Classes                | Requires factory classes and interfaces.        |
| Slightly More Complex       | More abstraction than direct object creation.   |
| Overkill for Small Projects | Not needed when only one implementation exists. |


--------------------------------------------------------------------------------------------------------------------------------------------

12. When to Use

Use Factory Method when:

Multiple implementations exist.
Object creation depends on business rules.
Future implementations are expected.
Client should not know concrete classes.
Creation logic is becoming repetitive.

Examples

Payment Gateway
Notification Provider
Storage Provider
Authentication Provider
Report Generator

--------------------------------------------------------------------------------------------------------------------------------------------
13. When NOT to Use

Don't use Factory Method when

Only one implementation exists.
Object creation is simple.
No future extensibility is expected.

Example

Customer

Invoice

Product

Address

Simply using

new Customer()

is perfectly fine.

--------------------------------------------------------------------------------------------------------------------------------------------

14. Bad Design
Checkout Service

↓

if

↓

Credit Card

↓

else if

↓

UPI

↓

else if

↓

Wallet

↓

else if

↓

Net Banking

Every provider change

↓

Checkout changes.

Bad architecture.

--------------------------------------------------------------------------------------------------------------------------------------------

15. Good Design
Checkout Service

↓

Payment Factory

↓

IPaymentProcessor

↓

Credit Card

UPI

Wallet

Net Banking

Checkout only talks to the factory and the interface.

--------------------------------------------------------------------------------------------------------------------------------------------

16. Characteristics Compared to Singleton

| Singleton                                 | Factory Method                       |
| ----------------------------------------- | ------------------------------------ |
| Controls **how many** objects are created | Controls **which** object is created |
| One shared instance                       | Multiple implementations             |
| Focuses on object lifetime                | Focuses on object creation           |
| One object                                | Many possible objects                |

--------------------------------------------------------------------------------------------------------------------------------------------

17. Product Company Interview Questions
Q1

Why Factory Method?

Answer

To encapsulate object creation and avoid creating concrete objects directly inside business logic.

Q2

Which SOLID principle does Factory Method support?

Open/Closed Principle (OCP)
Dependency Inversion Principle (DIP)


Q3

What problem does it solve?

Removing large

if

else if

switch

blocks used only to decide which object to create.

Q4

Can Factory Method return different implementations?

Yes.

That is its primary purpose.

Q5

Does Factory Method improve testing?

Yes.

Business logic depends on abstractions instead of concrete classes.

--------------------------------------------------------------------------------------------------------------------------------------------

18. Product Company Discussion

One of the biggest misconceptions is:

"Factory Method removes all if-else statements."

This is not true.

A Factory Method often contains some selection logic (an if, switch, dictionary lookup, or configuration-based mapping).

The key idea is where that logic lives.

Instead of scattering object creation across many business services, we centralize it in one place—the factory.

That gives us:

Clean business logic
Easier maintenance
Better extensibility
Consistent object creation




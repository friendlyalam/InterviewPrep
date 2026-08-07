Output
Original Price : ₹200,000.00
Final Price    : ₹160,000.00
Now Let's Add a New Strategy

Suppose tomorrow Amazon introduces

Corporate Pricing
CorporatePricingStrategy.cs
using StrategyPattern.Interfaces;
using StrategyPattern.Models;

namespace StrategyPattern.Strategies;

public sealed class CorporatePricingStrategy : IPricingStrategy
{
    public string StrategyName => "Corporate";

    public decimal CalculatePrice(PricingContext context)
    {
        decimal corporateDiscount = 25;

        return context.Product.BasePrice
             - (context.Product.BasePrice * corporateDiscount / 100);
    }
}
Register in DI
services.AddTransient<
    IPricingStrategy,
    CorporatePricingStrategy>();
Program.cs
CustomerType = "Corporate";

That's it.

Nothing else changes.

Not

PricingService

Not

Existing Strategies

Not

Interfaces

Not

Models

This is Open/Closed Principle.

Complete Execution Flow
Program.cs

        │

        ▼

PricingService

        │

        ▼

IEnumerable<IPricingStrategy>

        │

 ┌──────┼───────────┬────────────┐
 │      │           │            │
 ▼      ▼           ▼            ▼

Regular Festival Premium Corporate

        │

        ▼

Selected Strategy

        │

        ▼

CalculatePrice()

        │

        ▼

Return Final Price
Strategy Pattern in Microsoft
Authentication
Authentication

↓

JWT

↓

Google

↓

Azure AD

↓

Windows

Same interface.

Different algorithms.

ASP.NET Core Compression
Compression

↓

GZip

↓

Brotli
Logging
ILogger

↓

Console

↓

Debug

↓

Azure Monitor
Serialization
Serializer

↓

JSON

↓

XML

↓

ProtoBuf
Strategy Pattern in Amazon
Pricing

↓

Regular

↓

Prime

↓

Festival

↓

Corporate

↓

Bulk Purchase
Strategy Pattern in Uber
Fare Calculation

↓

Normal

↓

Surge

↓

Airport

↓

Shared Ride
Strategy Pattern in Google
Maps

↓

Fastest Route

↓

Shortest Route

↓

Avoid Tolls

↓

Avoid Highways
Advantages

✅ Removes huge if-else

✅ Easy to test

✅ Runtime algorithm selection

✅ Open for extension

✅ Cleaner code

✅ High cohesion

✅ Low coupling

Disadvantages

❌ More classes

❌ Requires DI configuration

❌ Slight learning curve

Interview Questions
Q1 What problem does Strategy solve?

It encapsulates multiple algorithms behind a common interface, allowing the application to choose one at runtime without changing the client code.

Q2 Why not use switch?

Because every new algorithm requires modifying the same method, violating the Open/Closed Principle.

Q3 Why use IEnumerable<IPricingStrategy>?

Because .NET Dependency Injection automatically provides all registered implementations, making it easy to add new strategies without changing the service.

Q4 Strategy vs Factory?
Strategy	Factory
Chooses behavior	Creates objects
Runtime algorithm	Runtime object creation
Behavioral	Creational
Q5 Strategy vs State?

This is a favorite interview question.

Strategy	State
Client chooses the algorithm.	The object's internal state determines behavior.
Behavior is selected externally.	Behavior changes automatically as state changes.
Focus: interchangeable algorithms.	Focus: state transitions.

Example:

Strategy: The customer chooses Festival Pricing or Corporate Pricing.
State: An order automatically moves from Created → Paid → Shipped → Delivered, and its behavior changes based on its current state.
Common Mistakes
❌ Huge switch
switch(customerType)

Avoid it.

❌ Creating strategies manually
new FestivalPricingStrategy();

Prefer DI in enterprise applications.

❌ Database calls inside strategies

Strategies should focus only on the algorithm.

❌ Logging inside every strategy

Keep cross-cutting concerns in decorators, middleware, or services.

Final Project Rating
Criteria	Rating
Enterprise Relevance	⭐⭐⭐⭐⭐
Microsoft Interview	⭐⭐⭐⭐⭐
Amazon Interview	⭐⭐⭐⭐⭐
Google Interview	⭐⭐⭐⭐⭐
DI Usage	⭐⭐⭐⭐⭐
SOLID Principles	⭐⭐⭐⭐⭐
Runtime Selection	⭐⭐⭐⭐⭐
Difficulty	Intermediate
Strategy Pattern Status
✅ Theory

✅ Real-Life Examples

✅ Enterprise Example

✅ Complete Project

✅ Dependency Injection

✅ Program.cs

✅ Runtime Strategy Selection

✅ Interview Questions

✅ Product Company Discussion
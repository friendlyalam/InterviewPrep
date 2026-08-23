Definition

Strategy Pattern defines a family of algorithms, encapsulates each one in a separate class, and makes them interchangeable at runtime.

This allows an application to dynamically select or change an algorithm at runtime without altering the context class that uses it.
It heavily promotes the Open/Closed Principle—allowing you to add new behaviors without modifying existing

Simple Definition

Instead of writing

if(...)
{

}
else if(...)
{

}
else if(...)
{

}

We write

One Interface

↓

Multiple Implementations

↓

Choose one at runtime
Intent

Separate different algorithms from the client so they can be changed without modifying existing code.

--------------------------------------------------------------------------------------------------------
Problem

Suppose Amazon calculates product prices.

Today they have

Regular Price

Tomorrow

Festival Sale

Next month

Prime Member Discount

Then

Corporate Customer Discount

Then

Flash Sale

Most beginners write

if(customer == "Regular")
{
   ...
}
else if(customer == "Prime")
{
   ...
}
else if(customer == "Corporate")
{
   ...
}
else if(customer == "Festival")
{
   ...
}

Imagine after 2 years...

25 Discount Types

One class becomes

600 Lines

Every new discount requires modifying the same class.

This violates the Open/Closed Principle (OCP).

--------------------------------------------------------------------------------------------------------

Strategy Solution

Instead

Regular Strategy

Festival Strategy

Prime Strategy

Corporate Strategy

Each algorithm lives in its own class.

The application chooses the correct one at runtime.


--------------------------------------------------------------------------------------------------------
Real-Life Example 1
Google Maps

You enter a destination.

Possible route strategies:

Fastest Route

Shortest Route

Avoid Highways

Avoid Tolls

Same destination.

Different algorithms.

Google Maps selects one based on your choice.


Real-Life Example 2
Payment Fraud Detection

Suppose a payment platform has different fraud detection strategies.

Low Risk Customer

↓

Simple Validation
High Risk Customer

↓

Advanced Validation
International Payment

↓

Cross Border Validation

The payment flow stays the same.

Only the validation algorithm changes.

--------------------------------------------------------------------------------------------------------
Product Company Example

We'll build

Dynamic Pricing Engine

Used in companies like:

Amazon
Uber
Airbnb
Walmart
Booking.com
Flipkart

Different pricing algorithms:

Regular Pricing

Festival Pricing

Premium Member Pricing

Corporate Pricing

Each pricing algorithm is a separate strategy.

Why This Project?

Because pricing is one of the most common Strategy Pattern examples in enterprise systems.

Changing prices should not require changing the pricing service.

Instead, new pricing strategies are added independently.

--------------------------------------------------------------------------------------------------------
Advantages

✅ Follows Open/Closed Principle

✅ Removes long if-else chains

✅ Easy to test each algorithm

✅ Easy to extend

✅ Runtime algorithm selection

✅ Improves readability

✅ Promotes Single Responsibility Principle

--------------------------------------------------------------------------------------------------------
Disadvantages

❌ More classes

❌ Slightly higher complexity

❌ Choosing the correct strategy requires some coordination (often via DI or a resolver)

--------------------------------------------------------------------------------------------------------
When to Use

Use Strategy when:

Multiple algorithms solve the same problem.
Algorithms change independently.
Runtime selection is required.
You want to eliminate large conditional blocks.

--------------------------------------------------------------------------------------------------------
When NOT to Use

Don't use Strategy when:

There is only one algorithm.
Algorithms are unlikely to change.
A simple method is enough.

--------------------------------------------------------------------------------------------------------
Strategy vs Factory

Many developers confuse them.

| Strategy                   | Factory                     |
| -------------------------- | --------------------------- |
| Chooses **behavior**       | Chooses **object creation** |
| Multiple algorithms        | Multiple object types       |
| Executes different logic   | Creates different objects   |
| Runtime behavior selection | Runtime object selection    |

Easy Memory Trick

Factory answers:

Which object should I create?

Strategy answers:

Which algorithm should I execute?

--------------------------------------------------------------------------------------------------------
Strategy vs Builder

| Builder                   | Strategy                      |
| ------------------------- | ----------------------------- |
| Builds complex objects    | Executes different algorithms |
| Step-by-step construction | Runtime behavior selection    |
| Ends with `Build()`       | Ends with algorithm execution |


--------------------------------------------------------------------------------------------------------
| Principle Used  | How It's Used                                                            |
| --------- | ------------------------------------------------------------------------ |
| SRP       | Each strategy contains one pricing algorithm.                            |
| OCP       | Add a new pricing strategy without modifying existing ones.              |
| LSP       | Any `IPricingStrategy` implementation can replace another.               |
| ISP       | Small, focused strategy interface.                                       |
| DIP       | `PricingService` depends on `IPricingStrategy`, not concrete strategies. |

--------------------------------------------------------------------------------------------------------

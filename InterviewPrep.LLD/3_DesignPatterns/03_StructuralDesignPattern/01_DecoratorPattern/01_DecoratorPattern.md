Definition

Decorator Pattern dynamically adds new behavior to an object without modifying its original class.

Simple definition:

Wrap an existing object and add extra functionality before or after calling it.

--------------------------------------------------------------------------------------------------------
Intent

Instead of modifying an existing class every time a new feature is needed,

we wrap it with another object.

Original Object

↓

Logging Decorator

↓

Caching Decorator

↓

Retry Decorator

↓

Client

Each decorator adds one responsibility.

--------------------------------------------------------------------------------------------------------
Real-Life Example 1
Gift Wrapping 🎁

Imagine you buy a mobile phone.

Original object:

Mobile Phone

You can add

Gift Wrap

Still the same phone.

Then

Greeting Card

Still the same phone.

Then

Premium Packaging

Still the same phone.

Every wrapper adds functionality.

The object itself never changes.


Real-Life Example 2
Coffee Shop

Base coffee

↓

Milk

↓

Chocolate

↓

Whipped Cream

↓

Caramel

Every topping wraps the previous coffee.

Exactly how Decorator works.


--------------------------------------------------------------------------------------------------------
Product Company Example

We'll build

Product Notification Pipeline

Imagine Amazon.

When an order is shipped

Original notification

↓

Email

↓

Logging

↓

Retry

↓

Performance Monitoring

Every feature is optional.

--------------------------------------------------------------------------------------------------------
Without Decorator
public class NotificationService
{
    public void Send()
    {
        SendEmail();

        Log();

        Retry();

        Metrics();

        Audit();

        Security();

        Cache();

        ...
    }
}

After 2 years

1500 Lines

Impossible to maintain.

With Decorator
Notification

↓

Logging

↓

Retry

↓

Metrics

↓

Email

Every feature becomes an independent class.

--------------------------------------------------------------------------------------------------------
Why Product Companies Love Decorator

Because ASP.NET Core Middleware is essentially Decorator.

Request

↓

Authentication

↓

Authorization

↓

Logging

↓

Caching

↓

Exception Handling

↓

Controller

Every middleware wraps the next one.

--------------------------------------------------------------------------------------------------------
Advantages

✅ Open/Closed Principle

✅ Add features without changing existing classes

✅ Very flexible

✅ Small classes

✅ Easy testing

✅ Reusable decorators

--------------------------------------------------------------------------------------------------------
Disadvantages

❌ More classes

❌ Harder debugging if there are many decorators

❌ Decorator order matters

--------------------------------------------------------------------------------------------------------
When to Use

Use Decorator when:

Features are optional.
Features can be combined.
You don't want inheritance explosion.
Behavior should be added dynamically.

--------------------------------------------------------------------------------------------------------
When NOT to Use

Don't use Decorator when:

Behavior never changes.
Only one implementation exists.
Simplicity is more important than flexibility.

--------------------------------------------------------------------------------------------------------
Decorator vs Strategy

This is one of Microsoft's favorite interview questions.
| Strategy                 | Decorator                          |
| ------------------------ | ---------------------------------- |
| Changes the algorithm    | Adds responsibilities              |
| One strategy is selected | Multiple decorators can be stacked |
| Behavioral Pattern       | Structural Pattern                 |

Strategy
Festival Pricing

OR

Premium Pricing

Only one executes.

Decorator
Notification

↓

Logging

↓

Retry

↓

Metrics

All execute.

--------------------------------------------------------------------------------------------------------
Decorator vs Adapter
| Decorator          | Adapter                      |
| ------------------ | ---------------------------- |
| Adds functionality | Converts interfaces          |
| Same interface     | Different interface          |
| Wraps object       | Bridges incompatible objects |


--------------------------------------------------------------------------------------------------------
Decorator Pattern - Final Part
Approach 1 (Manual Wrapping)

This is the easiest way to understand the pattern.

Program.cs
INotificationService service =
    new PerformanceDecorator(
        new RetryDecorator(
            new LoggingDecorator(
                new EmailNotificationService())));

Then

await service.SendAsync(message);
Object Graph
PerformanceDecorator

↓

RetryDecorator

↓

LoggingDecorator

↓

EmailNotificationService
Output
Execution Started

Attempt 1

========== LOG START ==========

Recipient : abc@gmail.com

Email Sent Successfully

=========== LOG END ===========

Execution Time : 503 ms
Problem

Would Microsoft write this?

new PerformanceDecorator(
    new RetryDecorator(
        new LoggingDecorator(
            new EmailNotificationService())))

No.

Enterprise Approach

We let Dependency Injection build the object graph.

Problem with Default Microsoft DI

The built-in DI container cannot decorate services automatically.

This is why many enterprise projects use Scrutor.

What is Scrutor?

Scrutor is a small library built on top of Microsoft's DI container that adds support for decorators.

Instead of manually wrapping services, you register them declaratively.

For example:

services.AddTransient<INotificationService, EmailNotificationService>();

services.Decorate<INotificationService, LoggingDecorator>();
services.Decorate<INotificationService, RetryDecorator>();
services.Decorate<INotificationService, PerformanceDecorator>();

The container automatically creates:

Performance

↓

Retry

↓

Logging

↓

Email

without nested new expressions.

Which Should You Learn?
Method	Learn?	Interview?	Production?
Manual Decorator	✅ Yes	✅ Yes	Rare
Scrutor Decorator	✅ Yes	⭐⭐⭐⭐⭐	⭐⭐⭐⭐⭐
For Our Course

We'll use:

Manual wrapping to understand the pattern.
Scrutor to understand enterprise implementation.
Program.cs (Course Version)
using DecoratorPattern.Models;
using DecoratorPattern.Services;
using DecoratorPattern.Decorators;
using DecoratorPattern.Interfaces;

NotificationMessage message = new()
{
    Recipient = "customer@company.com",
    Subject = "Order Shipped",
    Body = "Your order has been shipped."
};

INotificationService service =
    new PerformanceDecorator(
        new RetryDecorator(
            new LoggingDecorator(
                new EmailNotificationService())));

await service.SendAsync(message);
Execution Flow
Program

↓

Performance

↓

Retry

↓

Logging

↓

Email Service
Why This Order?

Suppose Email throws an exception.

Flow:

Performance Start

↓

Retry Start

↓

Logging Start

↓

Email

↓

Logging End

↓

Retry End

↓

Performance End

Every decorator wraps the next.

If Order Changes

Suppose

Logging

↓

Performance

↓

Retry

↓

Email

Now the logs include the performance timing differently.

Decorator order changes behavior.

This is an important interview point.

ASP.NET Core Middleware

This is the same concept:

Exception Middleware

↓

Authentication

↓

Authorization

↓

Logging

↓

Controller

Each middleware wraps the next middleware.

That is why middleware is often described as an implementation of the Decorator (or Pipeline) pattern.

Interview Questions
Q1 Why not inheritance?

Without Decorator:

EmailService

↓

LoggingEmailService

↓

RetryLoggingEmailService

↓

CachingRetryLoggingEmailService

You quickly get a combinatorial explosion of subclasses.

Decorator avoids that.

Q2 Decorator vs Middleware?

Middleware is essentially a pipeline of decorators around request processing.

Q3 Decorator vs Proxy?
Decorator	Proxy
Adds responsibilities	Controls access
Enhances behavior	Restricts or manages access
Logging, Retry, Metrics	Lazy loading, Security, Remote access
Q4 Why use a Base Decorator?

It centralizes the wrapped service reference and forwarding logic, reducing duplication across decorators.

Q5 Can Decorators be removed?

Yes.

That's one of their biggest advantages.

Performance

↓

Logging

↓

Email

or

Retry

↓

Email

No changes are needed in EmailNotificationService.

Project Summary
NotificationMessage

↓

EmailNotificationService

↓

LoggingDecorator

↓

RetryDecorator

↓

PerformanceDecorator

↓

Program
Product Company Rating
Company	Usage
Microsoft	⭐⭐⭐⭐⭐
Amazon	⭐⭐⭐⭐⭐
Google	⭐⭐⭐⭐
Uber	⭐⭐⭐⭐⭐
Walmart	⭐⭐⭐⭐
One Improvement to Our Course

Earlier, we used DI extensively for Factory, Builder, and Strategy because those patterns naturally fit dependency injection.

For Decorator, the reality is a bit different:

If you're teaching the pattern itself, manual wrapping (new LoggingDecorator(new EmailNotificationService())) is the clearest way to show how it works.
In production, developers usually rely on middleware pipelines, libraries like Scrutor, or framework features to compose decorators.

So from now on, we'll use the approach that best reflects how the pattern is actually used in enterprise code, rather than forcing every pattern into the same DI style.
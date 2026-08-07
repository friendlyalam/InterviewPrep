Enterprise Project

We'll build

Notification Pipeline

Exactly like ASP.NET Core Middleware.

Project Structure
06_DecoratorPattern
│
├── Models
│      NotificationMessage.cs
│
├── Interfaces
│      INotificationService.cs
│
├── Services
│      EmailNotificationService.cs
│
├── Decorators
│      NotificationDecorator.cs
│      LoggingDecorator.cs
│      RetryDecorator.cs
│      PerformanceDecorator.cs
│
├── DependencyInjection
│      ServiceCollectionExtensions.cs
│
└── Program.cs

Only 8 classes.

Architecture
Program

↓

PerformanceDecorator

↓

RetryDecorator

↓

LoggingDecorator

↓

EmailNotificationService

Notice something important.

Unlike Strategy,

where only one algorithm executes,

Decorator executes every wrapper.

Product Company Examples
Company	Example
Microsoft	ASP.NET Core Middleware
Amazon	Logging + Retry + Metrics around services
Google	Request interceptors
Uber	API Gateway Filters
Walmart	Auditing + Monitoring + Retry
SOLID Principles
Principle	Usage
SRP	Each decorator has one responsibility
OCP	Add decorators without modifying existing classes
LSP	Every decorator implements the same interface
ISP	Small notification interface
DIP	Depend on INotificationService
Interview Questions
What problem does Decorator solve?
Decorator vs Strategy?
Decorator vs Adapter?
Why is ASP.NET Core Middleware considered a Decorator?
Why prefer Decorator over inheritance?
Why does decorator order matter?
What We'll Build
Program

↓

PerformanceDecorator

↓

RetryDecorator

↓

LoggingDecorator

↓

EmailNotificationService

↓

Console Output

This is almost identical to how ASP.NET Core builds its middleware pipeline.
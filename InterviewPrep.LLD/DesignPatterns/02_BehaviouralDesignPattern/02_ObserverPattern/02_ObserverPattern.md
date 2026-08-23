Definition

Observer Pattern defines a one-to-many dependency between objects so that when one object changes state, all its dependents are automatically notified.

Simple definition

One object publishes events. Multiple objects subscribe and react independently.

------------------------

Intent

Decouple the publisher from its subscribers.

Publisher doesn't know who is listening.

Real Life Example 1
YouTube
MrBeast uploads video

        │

        ▼

Subscribers receive notification

MrBeast never calls each subscriber.

He simply publishes.

YouTube handles notifying everyone.

----------------------------------

Real Life Example 2
Newspaper
Newspaper Company

        │

        ▼

Thousands of Subscribers

Company doesn't care

Who you are
Where you live

It simply publishes today's newspaper.

-----------------------------------------------------------------------
Product Company Example

We'll build

Order Event Notification System

Exactly how Amazon works.

Customer places an order.

What happens?

Order Created

↓

Email

↓

SMS

↓

Analytics

↓

Inventory Update

↓

Audit Log

↓

Loyalty Points

Every service reacts independently.

Without Observer
OrderService

↓

EmailService

↓

SmsService

↓

AnalyticsService

↓

InventoryService

↓

AuditService

Every time you add a service

You modify

OrderService

Violation of

OCP
SRP
With Observer
OrderService

↓

OrderPlaced Event

↓

Email Subscriber

↓

SMS Subscriber

↓

Analytics Subscriber

↓

Inventory Subscriber

↓

Audit Subscriber

OrderService never changes.

----------------------------------------------------------

Why Product Companies Love Observer

Because modern applications are event-driven.

Examples

Microsoft

User Registered

↓

Azure Event Grid

↓

Email

↓

Billing

↓

CRM

↓

Audit



Amazon

Order Placed

↓

SNS

↓

Inventory

↓

Shipment

↓

Recommendation Engine

↓

Analytics



Google

New File Uploaded

↓

Pub/Sub

↓

Virus Scan

↓

Thumbnail Generation

↓

Search Index

↓

Logging

----------------------------------------------------

Advantages

✅ Loose Coupling

✅ Open/Closed Principle

✅ Easy to Add Subscribers

✅ Event Driven

✅ Highly Scalable

-----------------------------------------------------

Disadvantages

❌ Event ordering can become complex.

❌ Debugging asynchronous flows is harder.

❌ Too many subscribers may affect performance.

-------------------------------------------------------------------

When to Use

Use Observer when

Events occur.
Multiple components need notification.
Publisher shouldn't know subscribers.
Extensibility is important.

------------------------------------------------------------
When NOT to Use

Don't use Observer when

Only one consumer exists.
Tight execution order is required.
Strong synchronous dependencies exist.

---------------------------------------------------------------
Observer vs Facade

| Observer         | Facade                 |
| ---------------- | ---------------------- |
| Publishes events | Simplifies subsystem   |
| One → Many       | One → One              |
| Event-driven     | Workflow orchestration |


--------------------------------------------------------
Observer vs Strategy

| Observer           | Strategy               |
| ------------------ | ---------------------- |
| Event notification | Algorithm selection    |
| Many listeners     | One selected algorithm |


-------------------------------------------------------


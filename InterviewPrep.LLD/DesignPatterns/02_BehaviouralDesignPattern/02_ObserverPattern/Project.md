Enterprise Project
Order Event System
Folder Structure
09_ObserverPattern
│
├── Models
│      Order.cs
│      OrderPlacedEvent.cs
│
├── Interfaces
│      IEventSubscriber.cs
│      IOrderPublisher.cs
│
├── Subscribers
│      EmailSubscriber.cs
│      SmsSubscriber.cs
│      InventorySubscriber.cs
│      AnalyticsSubscriber.cs
│      AuditSubscriber.cs
│
├── Publishers
│      OrderPublisher.cs
│
└── Program.cs
Architecture
OrderPublisher

       │

       ▼

OrderPlaced Event

       │

 ┌─────┼──────────┬───────────┬────────────┐
 ▼     ▼          ▼           ▼            ▼

Email  SMS   Inventory   Analytics    Audit

Publisher knows nothing.

Subscribers know nothing about each other.

SOLID Principles

| Principle | Usage                                           |
| --------- | ----------------------------------------------- |
| SRP       | Each subscriber has one responsibility.         |
| OCP       | Add subscribers without changing publisher.     |
| LSP       | Every subscriber implements the same interface. |
| ISP       | Small subscriber interface.                     |
| DIP       | Publisher depends on subscriber abstraction.    |


Product Company Examples

| Company   | Example               |
| --------- | --------------------- |
| Amazon    | Order Events          |
| Microsoft | Azure Event Grid      |
| Google    | Cloud Pub/Sub         |
| Uber      | Trip Completed Events |
| Walmart   | Inventory Events      |


Interview Questions
Observer vs Pub/Sub?
Observer vs Event Bus?
Observer vs Mediator?
Why is Observer good for microservices?
Can Observer be asynchronous?
What are the drawbacks of synchronous observers?
Enterprise Improvement

One important thing:

The project we'll build demonstrates the Observer Pattern in-memory using objects.

In real enterprise systems, the same idea is often implemented with infrastructure such as:

Azure Event Grid
Azure Service Bus
RabbitMQ
Apache Kafka
Google Pub/Sub
Amazon SNS/SQS

The design pattern remains the same—the transport mechanism changes.

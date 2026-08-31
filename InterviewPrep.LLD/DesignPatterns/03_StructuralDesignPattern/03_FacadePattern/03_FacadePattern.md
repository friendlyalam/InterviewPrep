Definition

Facade Pattern provides a unified, simplified interface to a complex subsystem.

Simple definition:

Instead of talking to 10 different services, the client talks to one Facade.


Intent

Hide the complexity of multiple subsystems behind one easy-to-use interface.

Real-Life Example 1
Restaurant Waiter

Without Facade

Customer

↓

Chef

↓

Cashier

↓

Kitchen

↓

Manager

↓

Billing

The customer has to coordinate everything.

With Facade

Customer

↓

Waiter

↓

Kitchen

↓

Cashier

↓

Chef

↓

Billing

The waiter acts as the Facade.

Real-Life Example 2
Travel Agency

Without Facade

Customer

↓

Flight Booking

↓

Hotel Booking

↓

Taxi Booking

↓

Insurance

The customer coordinates everything.

With Facade

Customer

↓

Travel Agency

↓

Flight

↓

Hotel

↓

Taxi

↓

Insurance

One contact point.



-
Product Company Example

We'll build an

Enterprise Order Processing System

Exactly like Amazon.

Customer clicks
Place Order

What happens internally?

Inventory

↓

Payment

↓

Invoice

↓

Shipping

↓

Notification

↓

Loyalty Points

↓

Audit

↓

Analytics

Imagine exposing all these services to the UI.

It becomes a nightmare.

Instead

CheckoutController

↓

OrderFacade

↓

Inventory

↓

Payment

↓

Shipping

↓

Invoice

↓

Notification

The UI only knows one class.

Without Facade
_inventoryService.CheckStock();

_paymentService.ProcessPayment();

_invoiceService.GenerateInvoice();

_shippingService.ScheduleDelivery();

_notificationService.SendEmail();

Imagine this in

Web API
Mobile App
Admin Portal
Batch Job

Duplicate code everywhere.

With Facade
_orderFacade.PlaceOrder(order);

That's all.

-
Why Product Companies Love Facade

Because microservices often expose

Inventory

Payment

Shipping

Notification

Invoice

But clients don't call every service individually.

Instead they call

Checkout Service

or

Order Orchestrator

That orchestrator is a Facade.

-

Advantages

✅ Simplifies complex systems

✅ Reduces coupling

✅ Hides implementation details

✅ Easier maintenance

✅ Better readability

--
Disadvantages

❌ Can become a "God Class" if it grows too much

❌ May hide too much flexibility


When to Use

Use Facade when:

Multiple services must be called together.
Clients shouldn't know subsystem details.
You want a clean API over a complex system.

When NOT to Use
Don't use Facade when:

There's only one service.
The client genuinely needs fine-grained control over each subsystem.


Facade vs Adapter

A common interview question.

| Facade                 | Adapter                             |
| ---------------------- | ----------------------------------- |
| Simplifies a subsystem | Converts one interface into another |
| Same functionality     | Different interface                 |
| Hides complexity       | Solves incompatibility              |


Adapter
Azure SDK

↓

Adapter

↓

Application

-
Facade
Inventory

↓

Payment

↓

Shipping

↓

Notification

↓

Facade

↓

Application


---
Facade vs Mediator

| Facade                     | Mediator                                    |
| -------------------------- | ------------------------------------------- |
| Client talks to one object | Objects communicate through a mediator      |
| Simplifies usage           | Coordinates interactions between colleagues |
| One-way orchestration      | Two-way collaboration                       |


-
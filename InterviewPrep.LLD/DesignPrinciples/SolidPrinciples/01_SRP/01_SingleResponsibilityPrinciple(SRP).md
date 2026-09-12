1. Product Company Definition

A class should have only one responsibility and therefore only one reason to change.

This is the official definition by Robert C. Martin (Uncle Bob).

------------------------------------------------------------------------

2. What does "One Reason to Change" mean?

Many beginners misunderstand this.

It does not mean:

One method.

It does not mean:

One property.

It means:

One business responsibility.

For example,

An OrderService is responsible for:

Managing Orders

NOT

Managing Orders
Sending Emails
Generating PDF
Writing Logs
Updating Inventory
Sending SMS

Each of these is a different responsibility.

-----------------------------------------------------------------

3. Simple Definition

Think of SRP as:

One Class = One Job

Example:

Hospital

Doctor

Job?

Treat patients.

Doctor should NOT

Prepare salary
Cook food
Drive ambulance
Manage pharmacy

---------------------------------------------------------------------------

Another example

Restaurant

Chef

Job?

Cook food.

Chef should NOT

Collect payment
Clean tables
Deliver food

Each person has one responsibility.

Classes should also have one responsibility.

-----------------------------------------------------------------------------

4. Why Was SRP Introduced?

Suppose Microsoft develops an Order Management System.

Initially,

OrderService

does:

Save Order

Everything works.

After six months,

Business says:

Add:

Email
SMS
Invoice
Inventory
Analytics
Logging

Developer writes everything inside OrderService.

Now OrderService becomes

5000 Lines

Then

10000 Lines

Now imagine a production bug.

Finding the issue becomes very difficult.

This is exactly why SRP exists.

---------------------------------------------------------------------------------

5. Bad Design (Violation of SRP)

Imagine this class.

public class OrderService
{
    public void PlaceOrder()
    {
        SaveOrder();

        SendEmail();

        SendSms();

        GenerateInvoice();

        UpdateInventory();

        LogInformation();

        PublishAnalytics();
    }

    private void SaveOrder() { }

    private void SendEmail() { }

    private void SendSms() { }

    private void GenerateInvoice() { }

    private void UpdateInventory() { }

    private void LogInformation() { }

    private void PublishAnalytics() { }
}

Looks simple.

But it violates SRP.

Why Is This Bad?

Ask yourself:

Why can this class change?

Answer:

Because of

Order changes

Email changes

SMS changes

Inventory changes

Invoice changes

Logging changes

Analytics changes

That means

Seven different reasons to change.

SRP says

One reason only.

-------------------------------------------------------------

Real Enterprise Problem

Imagine

Amazon changes email provider

SMTP

↓

Amazon SES

Should OrderService change?

No.

But because email logic is inside it,

you must modify OrderService.

Tomorrow

Business changes Invoice.

Again

Modify OrderService.

Tomorrow

Inventory changes.

Again

Modify OrderService.

This creates a ripple effect.

------------------------------------------------------------------------

Enterprise Example (Microsoft / Amazon Style)

Imagine an Online Shopping Application.

Instead of

ShoppingService

↓

Everything

Professional architecture looks like

CheckoutService
        │
        ├────────► PaymentService

        ├────────► InventoryService

        ├────────► NotificationService

        ├────────► InvoiceService

        ├────────► LoyaltyService

        └────────► AuditService

Each service has one business responsibility.

---------------------------------------------------------------

Characteristics of SRP
One responsibility
One reason to change
High cohesion
Low coupling
Easier maintenance
Easier testing
Better readability
Better scalability

-----------------------------------------------------

| Benefit                   | Explanation                                          |
| ------------------------- | ---------------------------------------------------- |
| Easy Maintenance          | Smaller classes are easier to update.                |
| Easy Testing              | Each class can be tested independently.              |
| High Cohesion             | Related logic stays together.                        |
| Low Coupling              | Changes don't spread unnecessarily.                  |
| Better Reuse              | Services can be reused by other modules.             |
| Easier Debugging          | Bugs are easier to isolate.                          |
| Better Team Collaboration | Different developers can work on different services. |


-------------------------------------------------------------------------------

Common Interview Questions
Q1. What is SRP?

A class should have only one responsibility and therefore only one reason to change.

Q2. What does "one reason to change" mean?

One business responsibility, not one method or one property.

Q3. Is SRP about the number of methods?

No.

A class can have many methods as long as they all support the same responsibility.

Example:

CustomerService

AddCustomer()

UpdateCustomer()

DeleteCustomer()

GetCustomer()

All methods belong to customer management, so SRP is not violated.

Q4. Does a large class always violate SRP?

No.

A class can be large if all its members contribute to a single responsibility.

Q5. Which OOP concept is most closely related to SRP?

High Cohesion.

SRP encourages classes to be focused on one business purpose.

----------------------------------------------------------------------------------------------------------------

Common Mistakes

❌ "One class should have one method."

Wrong.

❌ "One class should have one property."

Wrong.

❌ "A large class always violates SRP."

Wrong.

The correct focus is:

One business responsibility.

------------------------------------------------------------------------------------
Product Company Best Practices
Group methods by business capability, not by technical convenience.
Keep services focused.
Avoid "God Classes."
Use Dependency Injection to collaborate with other services rather than placing all logic in one class.
If you can identify multiple independent reasons for a class to change, consider splitting it.

-----------------------------------------------------

Product Company Interview Tip

A question that often follows is:

"If OrderService no longer sends emails, who coordinates the workflow?"

In enterprise applications, the answer is often an application service, workflow/orchestration service,
or an event-driven mechanism. For example, OrderService saves the order, then publishes an OrderPlaced event. 
Separate handlers such as EmailService, InventoryService, and AnalyticsService react to that event.
This keeps each service focused while still allowing the overall business process to work together.


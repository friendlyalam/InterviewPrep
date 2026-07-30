1. Product Company Definition

Cohesion is the degree to which the responsibilities of a class, module, or component are closely related and focused on a single purpose.

Interview Definition

Cohesion measures how well the methods and data of a class belong together. A highly cohesive class has one clear responsibility.

2. Simple Definition

Think of cohesion as answering this question:

"Does this class have one job, or is it trying to do everything?"

One job → High Cohesion ✅
Many unrelated jobs → Low Cohesion ❌

----------------------------------------------------------
3. Why Is Cohesion Important?

Imagine an e-commerce application.

Suppose someone writes this class:

OrderService

✔ Place Order
✔ Send Email
✔ Generate PDF
✔ Send SMS
✔ Calculate Salary
✔ Export Excel
✔ Authenticate User
✔ Print Barcode

This class is doing everything.

That is Low Cohesion.

----------------------------------------------------------

A better design is:

OrderService
    │
    ├── Place Order

EmailService
    ├── Send Email

PdfService
    ├── Generate PDF

SmsService
    ├── Send SMS

AuthenticationService
    ├── Authenticate User

Each class has one responsibility.

That is High Cohesion.

-----------------------------------------------------------------------

4. Real-Life Analogy
Low Cohesion

Imagine a person who is:

Doctor
Teacher
Lawyer
Pilot
Chef

All at the same time.

Possible?

Maybe.

Efficient?

No.

------------------------------------------------------------------

High Cohesion

Doctor

Only treats patients.

Teacher

Only teaches.

Chef

Only cooks.

Each has one responsibility.

-----------------------------------------------------------------

5. Bad Design (Low Cohesion)
public class EmployeeService
{
    public void AddEmployee() { }

    public void DeleteEmployee() { }

    public void SendEmail() { }

    public void GenerateInvoice() { }

    public void ExportExcel() { }

    public void PrintBarcode() { }

    public void CalculateTax() { }
}
Problems
Too many responsibilities
Difficult to maintain
Difficult to test
Violates the Single Responsibility Principle (SRP)

----------------------------------------------------------------------------

6. Good Design (High Cohesion)
EmployeeService
public class EmployeeService
{
    public void AddEmployee() { }

    public void DeleteEmployee() { }

    public void UpdateEmployee() { }
}

---------------------------
EmailService
public class EmailService
{
    public void SendEmail() { }
}

--------------------------------
InvoiceService
public class InvoiceService
{
    public void GenerateInvoice() { }
}

---------------------------------
ExcelExportService
public class ExcelExportService
{
    public void Export() { }
}

Now every class has a single responsibility.

----------------------------------------------------------
7. Enterprise Example

Imagine Amazon Order Processing.

Instead of this:

OrderService

↓

Everything

Amazon would typically design something like:

OrderService
        │
        ├────────► InventoryService
        ├────────► PaymentService
        ├────────► EmailService
        ├────────► ShippingService
        ├────────► NotificationService
        └────────► InvoiceService

Each service focuses on one business capability.

------------------------------------------------------

8. Characteristics
High Cohesion
One responsibility
Easy to understand
Easy to test
Easy to maintain
High reusability
Small focused classes

----------------------------------------------------
Low Cohesion
Multiple unrelated responsibilities
Difficult maintenance
Difficult debugging
Large "God Classes"
High chance of bugs

------------------------------------------------------------

| High Cohesion        | Low Cohesion            |
| -------------------- | ----------------------- |
| One responsibility   | Many responsibilities   |
| Small classes        | Large classes           |
| Easy testing         | Difficult testing       |
| Easy maintenance     | Difficult maintenance   |
| Easy reuse           | Hard reuse              |
| Easier to understand | Difficult to understand |
| Preferred            | Avoid                   |

----------------------------------------------------------------
10. Real-Life Examples

| High Cohesion                             | Low Cohesion                                     |
| ----------------------------------------- | ------------------------------------------------ |
| Calculator only performs calculations     | Calculator also sends emails and prints invoices |
| Washing Machine washes clothes            | Washing Machine also cooks food                  |
| Camera captures photos                    | Camera also acts as a refrigerator               |
| Email Service only sends emails           | Email Service manages payroll and inventory      |
| Bank ATM only performs banking operations | ATM also books movie tickets and edits photos    |


---------------------------------------------------------------------------------------------------
11. Product Company Examples
Good Design:
UserService

↓

User Operations

---------------
PaymentService

↓

Payment Operations

----------------------------
NotificationService

↓

Notifications

Each service has one clear responsibility.

-----------------------------------------------------------

Bad Design
ApplicationService

↓

Users

↓

Payments

↓

Invoices

↓

Emails

↓

Reports

↓

Logging

↓

Authentication

This is often called a God Object or God Class.

-----------------------------------------------------------------
| Cohesion                          | Coupling                         |
| --------------------------------- | -------------------------------- |
| Relationship **inside one class** | Relationship **between classes** |
| Measures focus                    | Measures dependency              |
| High is good                      | Low is good                      |
| One responsibility                | Minimal dependency               |
| Concerned with class design       | Concerned with collaboration     |

-----------------------------------------

13. Common Interview Questions
Q1. What is Cohesion?

Cohesion measures how closely related the responsibilities within a class are.

Q2. Which is better?

High Cohesion.

Q3. How can we increase cohesion?
Split large classes.
Give each class one responsibility.
Follow the Single Responsibility Principle (SRP).
Q4. Can a project have high cohesion and high coupling?

Yes, but it is not ideal.

For example, each class may have one responsibility (high cohesion), but if every class directly depends on many concrete classes, the system still has high coupling.

The goal is:

High Cohesion
Low Coupling
Q5. Which SOLID principle is related to cohesion?

Single Responsibility Principle (SRP) is the principle that most directly promotes high cohesion.

--------------------------------------------------

14. Best Practices
One class → One responsibility.
Avoid "God Classes."
Keep methods related to the same business purpose.
Move unrelated functionality into separate classes.
Design classes that are easy to understand and test.

----------------------------------------------------------------
Product Company Summary
| Aspect                     | High Cohesion | Low Cohesion |
| -------------------------- | ------------- | ------------ |
| Responsibility             | Single        | Multiple     |
| Readability                | High          | Low          |
| Maintainability            | Easy          | Difficult    |
| Testing                    | Easy          | Difficult    |
| Reusability                | High          | Low          |
| Product Company Preference | ✅ Yes         | ❌ No         |

----------------------------------------------------------------------

Interview Summary
Good Design

↓

High Cohesion

+

Low Coupling

↓

Easy Maintenance

↓

Easy Testing

↓

Scalable Software

-----------------------------------------------------------------------
Product Company Insight

Many candidates answer:

"Cohesion means related methods."

A stronger answer is:

Cohesion measures how well a class is focused on a single responsibility. Highly cohesive classes are easier to understand, test, reuse,
and maintain. In enterprise software, the goal is to achieve high cohesion within classes while maintaining low coupling between classes.



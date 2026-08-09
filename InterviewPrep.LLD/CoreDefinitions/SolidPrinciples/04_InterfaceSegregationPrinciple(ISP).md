1. Product Company Definition

Clients should not be forced to depend on interfaces they do not use.

This principle was introduced by Robert C. Martin.

2. Simple Definition

Instead of creating one large interface, create multiple small, focused interfaces.

Or:

A class should implement only the methods it actually needs.

-----------------------------------------------------------------------------

3. Why Was ISP Introduced?

Suppose we have one interface:

public interface IEmployee
{
    void ApproveLeave();
    void AssignTask();
    void PickItem();
    void PackItem();
    void DeliverPackage();
}

Now imagine these employees:

Admin
Warehouse Picker
Warehouse Packer
Delivery Partner

Does every employee perform all five operations?

No.

Yet every class is forced to implement them.

-------------------------------------------------------------------------------

Bad Design
                IEmployee
                    │
     ┌──────────────┼──────────────┐
     │              │              │
     ▼              ▼              ▼
   Admin         Picker        Delivery

The DeliveryPartner is forced to implement:

ApproveLeave();
AssignTask();

even though those methods don't make sense.

Developers often write:

throw new NotImplementedException();

This is exactly what ISP tries to prevent.

------------------------------------------------------------------
3. Why was ISP introduced?

Before ISP, developers created huge interfaces like this:

public interface IEmployee
{
    void RegisterPatient();
    void ScheduleAppointment();
    void DiagnosePatient();
    void WritePrescription();
    void DispenseMedicine();
    void GenerateBill();
}

Now imagine these employees:

Receptionist
Doctor
Pharmacist
Cashier

The Receptionist doesn't diagnose patients.

The Doctor doesn't generate bills.

The Cashier doesn't dispense medicines.

Yet every class must implement every method.

Developers usually write:

throw new NotImplementedException();

or

// Do Nothing

This is exactly why ISP exists.

------------------------------------------------------------------------------

4. Real-Life Example

Think about a TV Remote.

A simple TV remote has:

Power
Volume
Channel

A smart TV remote has:

Netflix
YouTube
Voice Search
Bluetooth

Should every TV be forced to implement all smart features?

No.

Different remotes expose different capabilities.

------------------------------------------------------------------------------

5. Enterprise Project

We'll build a completely different project.

Smart Warehouse Management System

This is a realistic enterprise domain used in companies like:

Amazon
Flipkart
Walmart
IKEA
DHL
Business Scenario

A warehouse has four types of users:

Admin

Responsibilities:

Approve leave
Assign tasks
Picker

Responsibilities:

Pick products
Packer

Responsibilities:

Pack products
Delivery Partner

Responsibilities:

Deliver packages

Notice:

No employee performs every operation.

Bad Design
                IWarehouseEmployee
                       │
      ┌────────┬────────┼────────┬────────┐
      ▼        ▼        ▼        ▼
   Admin    Picker   Packer   Delivery

Everyone implements every method.

Lots of empty implementations.

Lots of exceptions.

This violates ISP.

-----------------------------------------------------------------

Good Design

Instead, create small interfaces.

                  ILeaveApprover
                         ▲
                         │
                      Admin

                  ITaskAssigner
                         ▲
                         │
                      Admin

                  IItemPicker
                         ▲
                         │
                     Picker

                  IItemPacker
                         ▲
                         │
                     Packer

               IDeliveryExecutor
                         ▲
                         │
                  DeliveryPartner

Now every class implements only what it needs.

--------------------------------------------------------------------------

5. Enterprise Scenario

Project

Hospital Management System

Suppose a hospital has four employees.

Receptionist

Responsibilities

Register Patient
Schedule Appointment
Doctor

Responsibilities

Diagnose Patient
Write Prescription
Pharmacist

Responsibilities

Dispense Medicine
Cashier

Responsibilities

Generate Bill

Notice

Nobody performs all operations.

6. Bad Design
                  IHospitalEmployee
                          │
     ┌────────────┬────────────┬────────────┬────────────┐
     ▼            ▼            ▼            ▼
Receptionist    Doctor    Pharmacist    Cashier

Every class gets

RegisterPatient()

ScheduleAppointment()

DiagnosePatient()

WritePrescription()

DispenseMedicine()

GenerateBill()

Most methods become

throw new NotImplementedException();

This is a textbook ISP violation.

7. Good Design
                  IPatientRegistration
                           ▲
                           │
                    Receptionist

               IAppointmentScheduler
                           ▲
                           │
                    Receptionist

                  IDiagnosisService
                           ▲
                           │
                         Doctor

                IPrescriptionWriter
                           ▲
                           │
                         Doctor

                IMedicineDispensing
                           ▲
                           │
                      Pharmacist

                    IBillingService
                           ▲
                           │
                         Cashier

Each interface represents one business capability.

Each class implements only the capabilities it actually provides.

---------------------------------------------------------------------------------------

6. Characteristics
Small interfaces
High cohesion
Low coupling
No unnecessary methods
Easy to extend
Easy to maintain
Easy to test
Clear business responsibilities
---------------------------------------------------------------------

| Advantage            | Explanation                           |
| -------------------- | ------------------------------------- |
| Easier to understand | Interfaces stay focused.              |
| Less coupling        | Classes depend only on what they use. |
| Better testing       | Smaller contracts are easier to mock. |
| Better maintenance   | Changes affect fewer classes.         |
| Cleaner architecture | No unused methods.                    |


-------------------------------------------------------------------------

10. Disadvantages

If overused,

you may end up creating

50 interfaces

50 implementations

for a very small application.

Balance is important.

-----------------------------------------------------------------------------

8. Common ISP Violations
Fat Interface
interface IEmployee
{
    15 methods...
}
Empty Methods
public void ApproveLeave()
{
}
Throwing Exceptions
public void ApproveLeave()
{
    throw new NotSupportedException();
}
Copy-Paste Implementations

Developers often copy methods just to satisfy the compiler.

--------------------------------------------------------------------------------
9. Relationship with Previous SOLID Principles

| Principle | Focus                        |
| --------- | ---------------------------- |
| SRP       | One responsibility per class |
| OCP       | Extend without modifying     |
| LSP       | Safe substitution            |
| ISP       | Small, focused interfaces    |


----------------------------------------------------------------------------------

10. Interview Questions
Q1. What is ISP?

Clients should not be forced to depend on interfaces they do not use.

Q2. What is a fat interface?

An interface containing many unrelated methods that force implementers to support functionality they don't need.

Q3. What is the biggest sign of an ISP violation?

Methods that:

throw new NotSupportedException();

or

throw new NotImplementedException();

or remain empty.

Q4. Does ISP mean every interface should have only one method?

No.

An interface can have multiple methods as long as they belong to one cohesive responsibility.

For example:

public interface IStorageProvider
{
    Upload();
    Download();
    Delete();
}

These methods all belong to the single responsibility of storage management, so this does not violate ISP.

---------------------------------------------------------------------------------------------------------------

13. Interview Questions
Basic
What is Interface Segregation Principle?

Clients should not be forced to depend on methods they don't use.

Why is ISP required?

To avoid large interfaces that force unnecessary implementations.

What is a Fat Interface?

An interface containing unrelated responsibilities.

Intermediate
Which is better?
IEmployee

or

ILeaveApprover

IBillingService

IPatientRegistration

Answer

Second approach.

What indicates ISP violation?

Methods like

throw new NotImplementedException();
Does every interface need one method?

No.

An interface may have multiple methods if they all belong to the same business capability.

Example

public interface IPatientRegistration
{
    void RegisterPatient();
    void UpdatePatient();
}

Both methods belong to patient registration.

ISP is not violated.

Senior-Level
Difference between SRP and ISP

SRP focuses on

Class Design.

ISP focuses on

Interface Design.

Difference between OCP and ISP

OCP

Extending behavior.

ISP

Designing better contracts.

Which design patterns benefit from ISP?
Strategy
Adapter
Decorator
Repository
Dependency Injection

-----------------------------------------------------------------------
14. Product Company Insight

Many developers think

"One interface should contain every operation related to a module."

This is incorrect.

For example,

don't write:

IHospitalService

with 25 methods.

Instead,

split it into meaningful business contracts.

This leads to:

Better dependency injection
Smaller units for testing
Better scalability
Easier onboarding for new developers
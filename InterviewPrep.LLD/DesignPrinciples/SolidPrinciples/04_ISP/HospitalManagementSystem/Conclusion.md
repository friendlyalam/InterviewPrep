Why this is a better ISP design

Instead of

10 interfaces

↓

1 method each

we have

4 interfaces

↓

Each represents one business capability

This is much closer to what you'll see in enterprise .NET applications.

Object Diagram
                IReceptionService
                        ▲
                        │
                  Receptionist

                 IDoctorService
                        ▲
                        │
                      Doctor

                IPharmacyService
                        ▲
                        │
                   Pharmacist

                 IBillingService
                        ▲
                        │
                     Cashier
Interview Question
Why didn't we create an interface for every method?

Because ISP is not about one-method interfaces.

It's about cohesive interfaces.

Methods that belong to the same business capability should stay together.

For example:

RegisterPatient()

ScheduleAppointment()

Both belong to Reception.

Keeping them together improves readability and reduces unnecessary abstractions.

Product Company Review

This design reflects what you'd commonly see in enterprise applications:

Interfaces model business capabilities, not individual methods.
Each role implements only the interfaces relevant to that role.
Responsibilities remain cohesive.
The design is ready for constructor injection and dependency inversion in later layers.

------------------------------------------------------------------------------------------------------

Console Output
--------------------------------
Patient Registration
--------------------------------
Patient registered successfully.

--------------------------------
Appointment Scheduling
--------------------------------
Appointment scheduled successfully.

--------------------------------
Doctor Consultation
--------------------------------
Diagnosis completed.

--------------------------------
Pharmacy
--------------------------------
Medicines dispensed successfully.

--------------------------------
Billing
--------------------------------
Bill generated successfully.

--------------------------------
Hospital Visit Completed
--------------------------------
Bill Amount : 1500
Object Interaction
Program
   │
   ▼
HospitalManagementService
   │
   ├────────► IReceptionService
   │               │
   │               ▼
   │         Receptionist
   │
   ├────────► IDoctorService
   │               │
   │               ▼
   │            Doctor
   │
   ├────────► IPharmacyService
   │               │
   │               ▼
   │          Pharmacist
   │
   └────────► IBillingService
                   │
                   ▼
                Cashier

Notice that HospitalManagementService communicates only with interfaces, not concrete classes.

Where is ISP?

Each class implements only the interface relevant to its business responsibility:

Class	Interface
Receptionist	IReceptionService
Doctor	IDoctorService
Pharmacist	IPharmacyService
Cashier	IBillingService

No class is forced to implement unrelated methods.

What would violate ISP?

A bad design would be:

public interface IHospitalEmployee
{
    void RegisterPatient();
    void ScheduleAppointment();
    Prescription DiagnosePatient();
    void DispenseMedicine();
    Bill GenerateBill();
}

Then Cashier would be forced to implement:

public void DiagnosePatient()
{
    throw new NotImplementedException();
}

and Doctor would need to implement billing methods it never uses.

That is a classic Interface Segregation Principle violation.

Product Company Review

Overall, this is a good ISP example because it demonstrates:

✅ Cohesive interfaces grouped by business capability.
✅ Constructor Injection.
✅ Dependency Injection through abstractions.
✅ Clear separation of responsibilities.
✅ An orchestration service coordinating specialized components.

One improvement you would commonly see in production systems is introducing repositories (e.g., IPatientRepository, IAppointmentRepository)
so services persist data instead of only writing to the console. We intentionally avoided that here because the focus is Interface Segregation Principle, 
not data access. Once we study repositories and clean architecture, we'll evolve this project further.
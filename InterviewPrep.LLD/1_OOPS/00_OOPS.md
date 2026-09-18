Definition

Object-Oriented Programming (OOP) is a programming paradigm (style of programming) in which software is designed using
objects that contain both data (properties/fields) and behaviour (methods).

Simple Definition

OOP is a way of writing programs by representing real-world entities as objects that contain data and behaviour.

What Does "Object-Oriented" Mean?

Everything in the real world can be thought of as an object.

Examples:

Car

Mobile

Employee

Doctor

Hospital

Student

Bank Account

Each object has:

Data (Characteristics)
Behaviour (Actions)

Example: Car

Data (Properties)

Brand = BMW

Colour = Black

Speed = 180

--------------------------

Behaviour (Methods)

Start()

Stop()

Accelerate()

Brake()


----------------------------------------------------
In C#, we represent this using a class and create an object.

Before OOP (Procedural Programming)

Imagine writing a Hospital Management System.

Without OOP:

RegisterPatient()

CalculateBill()

GenerateReport()

SendSMS()

UpdatePatient()

Everything is just functions.

As the project grows:

Thousands of functions
Global variables
Difficult maintenance
Hard to reuse code
With OOP

We organise code into classes.

Patient

Doctor

Appointment

Billing

Pharmacy

Each class manages its own data and behaviour.

------------------------------------

Real-Life Example

Imagine a hospital.

Instead of one person doing everything:

Reception

Doctor

Nurse

Pharmacy

Billing

Each department has its own responsibility.

OOP works the same way.

OOP in C#

Suppose we create an Employee class.

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public void Work()
    {
        Console.WriteLine($"{Name} is working.");
    }
}

Creating an object:

Employee emp = new Employee();

emp.Id = 101;
emp.Name = "Mohd Alam";

emp.Work();

Output:

Mohd Alam is working.
Why Was OOP Introduced?

Large applications became difficult to maintain.

Problems included:

Duplicate code
Poor code organisation
Difficult testing
Difficult maintenance
Hard to extend

OOP solves these problems.

---------------------------------------------------------

Benefits of OOP
1. Code Reusability

Write once.

Use many times.

Example:

Person

↓

Employee

↓

Manager

↓

Developer
2. Better Organisation

Instead of:

1000 Functions

You organise code like:

Employee

Customer

Product

Order
3. Easier Maintenance

Bug in Employee?

Open only Employee class.

4. Better Security

Hide sensitive data.

Example:

Salary

Password

Bank Balance

Users cannot directly modify them.

5. Scalability

Need a new feature?

Create a new class.

Existing classes remain unchanged.

Four Pillars of OOP

These are the most important concepts.

                OOP

        ┌────────┼────────┐

        │        │        │

Encapsulation

Inheritance

Polymorphism

Abstraction

Let's understand each one.

1. Encapsulation
Definition

Wrapping data and methods together inside a class and controlling access to the data.

Example:

public class Employee
{
    private decimal salary;

    public decimal Salary
    {
        get => salary;
        set
        {
            if (value > 0)
                salary = value;
        }
    }
}

User cannot directly modify the field.

Real-life example:

ATM Machine

↓

You press buttons.

↓

Machine performs work.

↓

You cannot access internal circuits.
2. Inheritance
Definition

One class acquires properties and methods of another class.

Example:

Person

↓

Employee

↓

Manager

Code:

class Person
{
    public string Name { get; set; }
}

class Employee : Person
{
}

Employee automatically gets Name.

Real-life example:

Father

↓

Son

Some characteristics are inherited.

3. Polymorphism
Definition

One interface, many implementations.

Example:

Payment

↓

UPI

↓

Credit Card

↓

Net Banking

Each payment processes differently.

Code:

public abstract class Payment
{
    public abstract void Pay();
}

Each child class overrides Pay().

Real-life example:

Remote Control

↓

TV

↓

AC

↓

Projector

Same button.

Different behaviour.

4. Abstraction
Definition

Showing only essential information while hiding unnecessary implementation details.

Example:

Car

↓

Steering

Brake

Accelerator

You drive the car.

You don't need to know how the engine works internally.

Code:

public abstract class Shape
{
    public abstract double Area();
}

Each shape implements its own area calculation.

OOP Relationships
                    Person
                       │
          ┌────────────┴────────────┐
          ▼                         ▼
      Employee                  Customer
          │
     ┌────┴────┐
     ▼         ▼
Developer   Manager

This hierarchy makes the code organised and reusable.

OOP in a Hospital Management System
Hospital

│

├── Patient

├── Doctor

├── Nurse

├── Appointment

├── Billing

├── Pharmacy

├── Inventory

Each class has:

Data
Behaviour

For example:

Patient

Data

PatientId

Name

Age

----------------

Behaviour

Register()

Update()

Discharge()

-------------------------------------------
Advantages of OOP
| Advantage       | Description                                                              |
| --------------- | ------------------------------------------------------------------------ |
| Reusability     | Reuse existing code through inheritance and composition.                 |
| Maintainability | Easier to fix and update code.                                           |
| Scalability     | Easier to add new features.                                              |
| Security        | Encapsulation protects data.                                             |
| Flexibility     | Polymorphism allows different implementations behind a common interface. |
| Modularity      | Code is divided into logical classes.                                    |

-------------------------------------------------------------------------------------------------------------
Disadvantages of OOP
More planning is required.
Can be overkill for very small programs.
Too many classes can make navigation harder if the design is poor.

----------------------------------------------------------------------------------------------
Real Product Company Example

Consider an e-commerce application.

E-Commerce

│

├── Product

├── Customer

├── Order

├── Payment

├── Cart

├── Shipment

Each class has its own responsibility.

Instead of one file with 10,000 lines of code, the application is organised into manageable components.
------------------------------------------------------------------------------------------------------------------------------------------
Microsoft Interview Questions
1. What is OOP?

A programming paradigm that organises software around objects containing both data and behaviour.

2. What are the four pillars of OOP?
Encapsulation
Inheritance
Polymorphism
Abstraction
3. Why is OOP used?

To create software that is reusable, maintainable, scalable, modular, and easier to understand.

-----------------------------------------------------------------------------------------------------------------

One-Line Interview Answer

Object-Oriented Programming (OOP) is a programming paradigm that models software as interacting objects containing 
both data and behaviour. 
Its main goals are code reuse, maintainability, scalability, modularity, and flexibility through the principles of 
encapsulation, inheritance, polymorphism, and abstraction.

--------------------------------------------------------------------------------------------------------------------
Easy Way to Remember

Think of building a city.

City

│

├── Houses

├── Schools

├── Hospitals

├── Banks

├── Roads

Each building has:

Its own data
Its own responsibilities

Together, they form a complete city.

Similarly, in OOP:

Application

│

├── Employee

├── Customer

├── Order

├── Payment

├── Product

Each class has its own responsibility, and together they build a complete, maintainable application. 
This is why OOP is the foundation of modern enterprise applications in C# and .NET.
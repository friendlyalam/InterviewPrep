IS-A vs HAS-A Relationship

This question is asked in almost every product company because it tests whether you know when to use Inheritance and when to use Composition/Aggregation.

------------------------------------------------------------

Why is this Important?

Many developers write code like this:

Car → Engine

using inheritance.

class Car : Engine
{
}

This is wrong.

Why?

Because:

A Car is NOT an Engine.

It has an Engine.

This is exactly why interviewers ask this question.

--------------------------------------------------------------------------------------------
Product Company Definition

IS-A Relationship:

IS-A represents inheritance. A derived class is a specialised version of its base class.

Example:

Car IS-A Vehicle

Dog IS-A Animal

SavingsAccount IS-A BankAccount


HAS-A Relationship:

HAS-A represents object relationships such as Composition or Aggregation, where one object contains or uses another object.

Example:

Car HAS-A Engine

House HAS-A Rooms

Order HAS-A Address

-----------------------------------------------------------
Simple Real-life Examples
IS-A
Vehicle

↓

Car

A Car is a Vehicle.

✔ Correct.

Animal

↓

Dog

Dog is an Animal.

✔ Correct.

HAS-A
Car

↓

Engine

Car has an Engine.

✔ Correct.

Laptop

↓

Battery

Laptop has a Battery.

✔ Correct.

--------------------------------------------------------------------------------
How to Identify?

Whenever you design classes, ask yourself one question.

Can I naturally say:

A ______ is a ______.

Example:

A Car is a Vehicle.

✔ Makes sense.

Use Inheritance.

Now ask:

A Car is an Engine.

❌ Doesn't make sense.

Now try:

A Car has an Engine.

✔ Makes sense.

Use Composition (or Aggregation, depending on ownership).

-------------------------------------------------------------------------------------

Enterprise Example

Imagine you're building an e-commerce platform.

Wrong Design
public class Address
{
}

public class Order : Address
{
}

Read it:

Order is an Address.

Completely wrong.

Correct Design
public class Address
{
}

public class Order
{
    private Address _address;
}

Read it:

Order has an Address.

Perfect.

----------------------------------------------------------------------------------

Another Enterprise Example
Banking System
IS-A
Account

↓

SavingsAccount

↓

CurrentAccount

A SavingsAccount is an Account.

HAS-A
Customer

↓

Address

A Customer has an Address.

Customer

↓

Debit Card

A Customer has a Debit Card.

-----------------------------------------------------------------------------------

C# Example
IS-A
using System;

public class Employee
{
    public void Work()
    {
        Console.WriteLine("Employee is working.");
    }
}

public class SoftwareEngineer : Employee
{
    public void WriteCode()
    {
        Console.WriteLine("Writing C# code.");
    }
}

class Program
{
    static void Main()
    {
        SoftwareEngineer engineer = new SoftwareEngineer();

        engineer.Work();
        engineer.WriteCode();
    }
}

Output

Employee is working.
Writing C# code.

---------------------------------------------------------------------------------

HAS-A
using System;

public class Laptop
{
    public void Start()
    {
        Console.WriteLine("Laptop Started");
    }
}

public class Employee
{
    private Laptop _laptop;

    public Employee()
    {
        _laptop = new Laptop();
    }

    public void StartWorking()
    {
        _laptop.Start();

        Console.WriteLine("Employee started working.");
    }
}

class Program
{
    static void Main()
    {
        Employee employee = new Employee();

        employee.StartWorking();
    }
}

Output

Laptop Started
Employee started working.

-----------------------------------------------------------------------------------------------
Memory Representation
IS-A
SoftwareEngineer Object

↓

Employee Members

+

SoftwareEngineer Members

One object contains both base and derived members because of inheritance.

-----------------------------------------------------------------------------------------------
HAS-A
Employee Object

↓

Laptop Object

Two separate objects exist.

One references the other.

-------------------------------------------------------------------------------------------------------

Product Company Examples

IS-A
| Base Class | Derived Class  |
| ---------- | -------------- |
| Vehicle    | Car            |
| Animal     | Dog            |
| Employee   | Manager        |
| User       | Administrator  |
| Account    | SavingsAccount |


HAS-A

| Parent   | Child    |
| -------- | -------- |
| Order    | Address  |
| Car      | Engine   |
| House    | Room     |
| Company  | Employee |
| Airline  | Pilot    |
| Computer | CPU      |


-------------------------------------------------------------------------------------
Interview Questions
Q1. What is IS-A?

An inheritance relationship where the derived class is a specialised version of the base class.

Q2. What is HAS-A?

A relationship where one object contains or uses another object.

Q3. Which OOP concept represents IS-A?

Inheritance.

Q4. Which OOP concepts represent HAS-A?
Association
Aggregation
Composition

The exact relationship depends on ownership and lifecycle.

Q5. Which is preferred in modern software?

Usually:

HAS-A (Composition)

because it provides greater flexibility and lower coupling than deep inheritance hierarchies.

-----------------------------------------------------------------------------------------------
Common Mistakes
Wrong
Car

↓

Engine

using inheritance.

Wrong
Order

↓

Address

using inheritance.

Correct
Order

↓

Address

using Composition.

Correct
Employee

↓

Manager

using inheritance.

-----------------------------------------------------------------------------------
Decision Flow
Does "A is a B" make sense?
          │
     Yes ─────► Use Inheritance (IS-A)
          │
          No
          │
Does "A has a B" make sense?
          │
     Yes ─────► Use HAS-A
                     │
      ┌──────────────┼──────────────┐
      │              │              │
Association   Aggregation   Composition

----------------------------------------------------------------------------

Product Company Interview Answer

If an interviewer asks:

When should we use Inheritance?

A strong answer is:

Use inheritance only when there is a true IS-A relationship and the derived class is a specialised form of the base class. 
If the relationship is instead HAS-A, prefer composition or aggregation because they usually provide better flexibility,
lower coupling, and easier maintenance.




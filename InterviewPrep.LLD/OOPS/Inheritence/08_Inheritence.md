Best Product Company Definition

Inheritance is an OOP mechanism that allows one class (derived class) to acquire and reuse the properties and behaviour of another class (base class),
while also allowing the derived class to extend or modify that behaviour.

Simple Definition

Inheritance means creating a new class using an existing class.

Instead of writing the same code again,

we reuse it.

--------------------------------------------------------------------
Why Was Inheritance Introduced?

Suppose you're building an HR Management System.

You have:

Developer

Manager

Tester

HR

Admin

Every employee has:

EmployeeId
Name
Email
DateOfJoining
Login()
Logout()

Without inheritance:

Developer

EmployeeId

Name

Email

DateOfJoining

--------------------

Manager

EmployeeId

Name

Email

DateOfJoining

--------------------

Tester

EmployeeId

Name

Email

DateOfJoining

The same code is repeated.

This is called code duplication.

With Inheritance
                 Employee

EmployeeId

Name

Email

DateOfJoining

Role

Login()

Logout()

                 ▲

      ┌──────────┼───────────┐

      │          │           │

Developer     Manager      Tester

Now common code exists only once.

---------------------------------------------------------------------------------------------------------------

Real-Life Example

Think of a family.

Father

↓

Son

The son inherits:

Eye colour
Height tendency
Family name

but also has his own characteristics.

Similarly,

Employee

↓

Developer

Developer inherits Employee members and adds new ones.

------------------------------------------------------------------------------------------------------------------------------
Syntax

public class Employee
{

}

public class Developer : Employee
{

}

The : means inherits from.

---------------------------------------------------------------------------------------------------------------------------

Constructor Execution

This is one of the favourite interview questions.

public class Employee
{
    public Employee()
    {
        Console.WriteLine("Employee Constructor");
    }
}

public class Developer : Employee
{
    public Developer()
    {
        Console.WriteLine("Developer Constructor");
    }
}

Output

Employee Constructor

Developer Constructor

Why?

Because the base class must initialise itself before the derived class.

Constructor Memory Flow
new Developer()

↓

Allocate Memory

↓

Employee Constructor

↓

Developer Constructor

↓

Object Ready

------------------------------------------------------------------------------------------

Types of Inheritance in C#
1. Single Inheritance
Employee

↓

Developer

Supported in C#.

2. Multilevel Inheritance
Person

↓

Employee

↓

Developer

Supported.

3. Hierarchical Inheritance
           Employee

      ┌────────┼────────┐

      ▼        ▼        ▼

Developer   Tester   Manager

Supported.

4. Multiple Inheritance (Classes)
Employee

Project

↓

Developer

❌ Not supported for classes.

Reason:

Diamond Problem.

5. Hybrid Inheritance

Combination of multiple types.

Only possible using interfaces.

Accessing Base Members

Using base.

public class Employee
{
    public virtual void Work()
    {
        Console.WriteLine("Employee Working");
    }
}

public class Developer : Employee
{
    public override void Work()
    {
        base.Work();

        Console.WriteLine("Developer Writing Code");
    }
}

Output

Employee Working

Developer Writing Code
Why Use base?

To call the base class implementation before or after adding derived behaviour.

---------------------------------------------------------------------------------------------------

Method Overriding
public class Employee
{
    public virtual void Work()
    {
        Console.WriteLine("Employee Work");
    }
}

public class Developer : Employee
{
    public override void Work()
    {
        Console.WriteLine("Developer Coding");
    }
}

Runtime chooses the correct implementation.

Method Hiding
public class Employee
{
    public void Work()
    {
        Console.WriteLine("Employee Work");
    }
}

public class Developer : Employee
{
    public new void Work()
    {
        Console.WriteLine("Developer Work");
    }
}

new hides the base method instead of overriding it.

---------------------------------------------------------------------------------------------------

Advantages
Code Reuse

Write common code once.

Easier Maintenance

Bug fix in Employee benefits all derived classes.

Extensibility

Add new behaviour without changing the base class.

Runtime Polymorphism

Derived classes can override behaviour.

------------------------------------------------------------------------------------------------
Disadvantages

Tight Coupling

Derived classes depend heavily on the base class.

Fragile Base Class Problem

Changing the base class can unintentionally affect all derived classes.

Deep Hierarchies

Too many inheritance levels make code difficult to understand.

----------------------------------------------------------------------------------------
Inheritance vs Composition

This is one of the most common product-company questions.

Inheritance:

Car

↓

ElectricCar

Composition:

Car

↓

Engine

↓

Battery

↓

GPS

↓

Music System

Inheritance = IS-A

Composition = HAS-A

Examples:

Developer IS AN Employee

Car HAS AN Engine

Laptop HAS A Keyboard

Hospital HAS Doctors

Modern product companies often prefer composition over inheritance unless there is a true IS-A relationship.

-----------------------------------------------------------------------------------------------------------------------
Product Company Interview Questions
1. What is Inheritance?

Mechanism that allows a derived class to reuse and extend the functionality of a base class.

2. Why use Inheritance?
Code reuse
Maintainability
Extensibility
Polymorphism
3. Which keyword is used?
:

Example:

class Developer : Employee
4. Does C# support multiple inheritance?

For classes: ❌ No

For interfaces: ✅ Yes

5. Why doesn't C# support multiple inheritance for classes?

To avoid the Diamond Problem, where a derived class inherits conflicting implementations from multiple base classes.

6. What is the difference between base and this?
base	                    this
Refers to the base class	Refers to the current object
Calls base constructor or methods	Accesses current class members
7. Constructor execution order?

Base constructor executes first, then the derived constructor.

8. What is the difference between overriding and hiding?
| Overriding                    | Hiding                                         |
| ----------------------------- | ---------------------------------------------- |
| Uses `virtual` and `override` | Uses `new`                                     |
| Runtime polymorphism          | Compile-time behaviour based on reference type |
| Replaces base implementation  | Hides base implementation                      |

9. When should you avoid inheritance?
When there isn't a true IS-A relationship.
When the hierarchy becomes deep and difficult to maintain.
When composition models the relationship better.

10. What principle is related to inheritance?

The Liskov Substitution Principle (LSP).

Any derived class should be usable wherever the base class is expected without breaking program behaviour.

Example:

Document document = new PdfDocument("Design.pdf", 1024, false);
document.Preview();

This works correctly because PdfDocument is a valid substitute for Document.

Interview-Ready Definition

Inheritance is an object-oriented mechanism that enables a derived class to reuse, extend, and specialise the data and behaviour of a base class,
promoting code reuse, maintainability, and runtime polymorphism while modelling a true "IS-A" relationship.

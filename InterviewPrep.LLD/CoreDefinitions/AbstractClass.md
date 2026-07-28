Abstract Class in C#
Definition

An abstract class is a class that cannot be instantiated (you cannot create its object).

It is used as a base class to provide:

Common implementation (implemented methods)
Common data (fields/properties)
A contract (abstract methods) that derived classes must implement.
An abstract class cannot be instantiated directly.
It can contain both abstract methods (without implementation) and non-abstract methods (with implementation).
Abstract methods must be implemented in derived classes using the override keyword.
Abstract classes can have fields, properties, constructors, and methods.
An abstract class cannot be declared as sealed.
A class cannot be both abstract and static.
Abstract methods can only be declared inside abstract classes.

Simple Definition

An abstract class is an incomplete class that serves as a blueprint for other classes.

-------------------------------------------------------------------------------------------------------
Why Do We Need an Abstract Class?

Imagine you are developing a Hospital Management System.

Every employee has:

Id
Name
Salary

Every employee also calculates salary differently.

For example:

Doctor
Nurse
Receptionist

All have salary calculation, but the logic differs.

Instead of repeating common properties, create one base class.

--------------------------------------------------------------------------------------------------
Real-Life Example

Suppose you own a vehicle company.

Every vehicle has:

Engine
Wheels
Start()

But every vehicle moves differently.

Car

↓

Drives

--------------------

Bike

↓

Rides

--------------------

Boat

↓

Sails

The common things belong in the abstract class.

Specific behaviour belongs in derived classes.

------------------------------------------------------------------------------------
Syntax
public abstract class Employee
{

}

Notice:

Employee emp = new Employee();

❌ Compile Error

Because abstract classes cannot be instantiated.

-------------------------------------------------------------------------------------------------
Example 1 (Basic)
using System;

public abstract class Employee
{
    public void Display()
    {
        Console.WriteLine("Employee Details");
    }
}

public class Developer : Employee
{

}

class Program
{
    static void Main()
    {
        Developer dev = new Developer();

        dev.Display();
    }
}

Output

Employee Details

---------------------------------------------------------------------------------

Example 2 (Abstract Method)
using System;

public abstract class Employee
{
    public abstract void CalculateSalary();
}

public class Developer : Employee
{
    public override void CalculateSalary()
    {
        Console.WriteLine("Developer Salary Calculated");
    }
}

class Program
{
    static void Main()
    {
        Developer dev = new Developer();

        dev.CalculateSalary();
    }
}

Output

Developer Salary Calculated

---------------------------------------------------------------------------------

What is an Abstract Method?

An abstract method has:

No implementation
Only declaration

Example

public abstract void CalculateSalary();

Notice:

No { }

The child class must implement it.

----------------------------------------------------------------------------------

Memory Representation
Developer dev = new Developer();

Memory

Stack

dev
 │
 ▼

Heap

Developer Object

Id

Name

Salary

Display()

CalculateSalary()

Notice:

There is no Employee object.

Only the Developer object exists.

----------------------------------------------------------------------------
Characteristics of an Abstract Class

1. Cannot Create Object
Employee emp = new Employee();

❌ Not allowed

2. Can Have Constructors
public abstract class Employee
{
    public Employee()
    {
        Console.WriteLine("Employee Constructor");
    }
}

The constructor runs when a derived object is created.

3. Can Have Fields
protected int id;
4. Can Have Properties
public string Name
{
    get;
    set;
}
5. Can Have Methods
public void Display()
{

}
6. Can Have Abstract Methods
public abstract void Save();
7. Can Have Static Methods
public static void CompanyPolicy()
{

}
8. Can Have Events
public event Action Saved;
9. Can Have Indexers
public string this[int index]
{
    get;
    set;
}
10. Can Have Nested Classes
public class Address
{

}

----------------------------------------------------------------------------------

Real-World Examples
Payment System
Payment (Abstract)

↓

Credit Card

↓

UPI

↓

Net Banking

Each payment type processes payments differently.

Shape
Shape

↓

Circle

↓

Rectangle

↓

Triangle

Every shape calculates area differently.

Employee
Employee

↓

Developer

↓

Manager

↓

Tester

↓

HR

Every employee calculates salary differently.

----------------------------------------------------------------------------------------------
| Abstract Class                       | Interface                                                                     |
| ------------------------------------ | ----------------------------------------------------------------------------- |
| Can have implementation              | Traditionally only contract (modern C# also allows default interface methods) |
| Can have fields                      | Cannot have instance fields                                                   |
| Constructors allowed                 | No instance constructors                                                      |
| Supports single inheritance          | A class can implement multiple interfaces                                     |
| Can have abstract and normal methods | Primarily defines a contract                                                  |

------------------------------------------------------------------------------------------------------------------------------------------
| Normal Class      | Abstract Class               |
| ----------------- | ---------------------------- |
| Can create object | Cannot create object         |
| Methods optional  | Can contain abstract methods |
| Used directly     | Used as a base class         |


---------------------------------------------------------------------------------------------------------------------------------

| Abstract             | Sealed              |
| -------------------- | ------------------- |
| Cannot create object | Can create object   |
| Must be inherited    | Cannot be inherited |
| Incomplete class     | Final class         |

------------------------------------------------------------------------------------------------------------------------------------

| Abstract                     | Static                                |
| ---------------------------- | ------------------------------------- |
| Cannot create object         | Cannot create object                  |
| Can be inherited             | Cannot be inherited                   |
| Can contain instance members | Only static members                   |
| Used as a base class         | Used for utility/helper functionality |


-------------------------------------------------------------------------------------------------------------------------
Advantages

✅ Promotes code reuse.

✅ Provides a common base for related classes.

✅ Enforces implementation of required methods.

✅ Reduces duplicate code.


Disadvantages

❌ Supports only single inheritance.

❌ Cannot create objects directly.

--------------------------------------------------------------------------------------------------------------------------------
Microsoft Interview Questions
1. What is an abstract class?

A class that cannot be instantiated and can contain both implemented methods and abstract methods.

2. Why do we use an abstract class?

To share common code while forcing derived classes to implement required behaviour.

3. Can an abstract class have constructors?

✅ Yes.

4. Can an abstract class have static methods?

✅ Yes.

5. Can an abstract class have fields?

✅ Yes.

6. Can an abstract class contain only normal methods?

✅ Yes.

Even if it has no abstract methods, it can still be declared abstract to prevent direct instantiation.

7. Can an abstract class implement interfaces?

✅ Yes.

interface ILogger
{
    void Log();
}

public abstract class Employee : ILogger
{
    public abstract void Log();
}
8. Can an abstract class inherit another class?

✅ Yes.

class Person
{
}

abstract class Employee : Person
{
}
9. Can an abstract class be sealed?

❌ No.

Reason:

abstract means must be inherited.
sealed means cannot be inherited.

These are opposite concepts.

10. Can an abstract method have a body?

❌ No.

public abstract void Display();

No implementation is allowed in an abstract method.

----------------------------------------------------------------------------------------------
Product Company Best Practice

Use an abstract class when:

Multiple related classes share common data and behaviour.
You want to provide a default implementation.
You also want to force derived classes to implement specific methods.

Use an interface when you want to define a capability or contract that unrelated classes can implement.

--------------------------------------------------------------------------------------------------------------------------------
Summary

| Question                   | Answer                                       |
| -------------------------- | -------------------------------------------- |
| Can create object?         | ❌ No                                         |
| Can have constructor?      | ✅ Yes                                        |
| Can have fields?           | ✅ Yes                                        |
| Can have properties?       | ✅ Yes                                        |
| Can have normal methods?   | ✅ Yes                                        |
| Can have abstract methods? | ✅ Yes                                        |
| Can inherit another class? | ✅ Yes                                        |
| Can implement interfaces?  | ✅ Yes                                        |
| Can be sealed?             | ❌ No                                         |
| Purpose                    | Share common code and enforce implementation |


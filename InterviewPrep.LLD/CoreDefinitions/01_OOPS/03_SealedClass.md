Sealed Class in C#
Definition

A sealed class is a class that cannot be inherited.

It is used when you want to stop other developers from creating a derived class.

The sealed keyword tells the compiler:

"This class is final. No one can inherit it."
A class can be sealed by using the sealed keyword. 
The keyword tells the compiler that the class is sealed and therefore, cannot be extended.
No class can be derived from a sealed class.

===================================================================
Syntax
public sealed class Employee
{

}
========================================
Example 1 (Basic)
using System;

public sealed class Employee
{
    public void Display()
    {
        Console.WriteLine("Employee Class");
    }
}

class Program
{
    static void Main()
    {
        Employee emp = new Employee();

        emp.Display();
    }
}

Output

Employee Class
===============================================================

What Happens if We Inherit?
public sealed class Employee
{

}

public class Manager : Employee
{

}

Compile Error

Cannot derive from sealed type 'Employee'
===========================================================
Memory
Employee (Sealed)
      |
      |
Object

emp = new Employee()

A sealed class behaves like any other class regarding object creation and memory.

The only difference is:

Inheritance

Employee
   ↑

Manager

❌ Not Allowed
=========================================================================

Why Do We Need a Sealed Class?

Suppose you wrote a payment gateway.

public class PaymentGateway
{
    public virtual void ProcessPayment()
    {

    }
}

Someone writes

class HackerPayment : PaymentGateway
{
    public override void ProcessPayment()
    {
        Console.WriteLine("Always Approved");
    }
}

Now your business rules are broken.

Instead

public sealed class PaymentGateway
{

}

No one can inherit and change the behavior.
========================================================================
Real World Example

Imagine an ATM.

ATM Machine

✔ Withdraw

✔ Deposit

✔ Check Balance

Can customers modify the ATM software by inheriting from it?

No.

The manufacturer locks it.

That is similar to a sealed class.
===================================================================================

Example 2 – Bank
using System;

public sealed class Bank
{
    public void Deposit()
    {
        Console.WriteLine("Money Deposited");
    }

    public void Withdraw()
    {
        Console.WriteLine("Money Withdrawn");
    }
}

class Program
{
    static void Main()
    {
        Bank bank = new Bank();

        bank.Deposit();

        bank.Withdraw();
    }
}

Output

Money Deposited

Money Withdrawn

=====================================================================================
Example 3 – Hospital Project
public sealed class HospitalConfiguration
{
    public string HospitalName
    {
        get;
        set;
    }

    public string City
    {
        get;
        set;
    }
}

If every hospital must use the same configuration rules, sealing the class can prevent unintended inheritance.

======================================================================================================================
Example 4 – License Validation
public sealed class LicenseValidator
{
    public bool IsValid(string license)
    {
        return true;
    }
}

No one can create

class FakeLicense : LicenseValidator
{

}
==================================================================================================
Characteristics of Sealed Class
1. Cannot Be Inherited
sealed class A
{

}

class B : A
{

}

Compile Error

2. Can Create Objects
Employee emp = new Employee();

Allowed.

Unlike a static class.

3. Can Have Constructors
public sealed class Employee
{
    public Employee()
    {

    }
}

Perfectly valid.

4. Can Have Fields
private int id;
5. Can Have Properties
public string Name
{
    get;
    set;
}
6. Can Have Methods
public void Display()
{

}
7. Can Have Events
public event Action Saved;
8. Can Have Indexers
private string[] skills = new string[5];

public string this[int index]
{
    get
    {
        return skills[index];
    }

    set
    {
        skills[index] = value;
    }
}
9. Can Have Nested Types
public sealed class Employee
{
    public class Address
    {

    }
}
Complete Example
using System;

public sealed class Employee
{
    private int id;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public event Action Saved;

    public Employee()
    {
        Console.WriteLine("Constructor Called");
    }

    public void Save()
    {
        Console.WriteLine("Employee Saved");

        Saved?.Invoke();
    }

    private string[] skills = new string[5];

    public string this[int index]
    {
        get
        {
            return skills[index];
        }

        set
        {
            skills[index] = value;
        }
    }

    public class Address
    {
        public string City
        {
            get;
            set;
        }
    }
}
===========================================================================
| Sealed Class                                         | Static Class                    |
| ---------------------------------------------------- | ------------------------------- |
| Can create object                                    | Cannot create object            |
| Can contain instance members                         | Only static members             |
| Can have constructors                                | Only static constructor         |
| Cannot be inherited                                  | Cannot be inherited             |
| Used for business objects that shouldn't be extended | Used for utility/helper classes |
| can contain indexers, events, and nested types       | Cannot contain indexers

========================================================================================================
Example

sealed class Employee
{

}
Employee emp = new Employee();

Allowed.

static class Logger
{

}
Logger logger = new Logger();

Compile Error.

================================================================================================
| Normal Class         | Sealed Class         |
| -------------------- | -------------------- |
| Can be inherited     | Cannot be inherited  |
| Can create objects   | Can create objects   |
| Constructors allowed | Constructors allowed |
| Methods allowed      | Methods allowed      |
| Properties allowed   | Properties allowed   |

=================================================================================================
Sealed Method

You can also seal an overridden method.

using System;

class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal");
    }
}

class Dog : Animal
{
    public sealed override void Speak()
    {
        Console.WriteLine("Dog");
    }
}

class Puppy : Dog
{
    // Compile Error
    // Cannot override Speak()
}

This allows inheritance of Dog, but prevents further overriding of Speak().

====================================================================================
Microsoft Product Company Examples

Examples of classes that are sealed or commonly designed to be sealed:

Immutable value objects.
Security-related classes.
License validation.
Cryptographic helpers.
Configuration objects that should not be extended.
Domain classes whose behavior must remain fixed.

A class may also be sealed as a performance optimization because the runtime knows no derived class can override its virtual methods.

===========================================================================================================================================
Interview Questions
1. What is a sealed class?

A class that cannot be inherited.

2. Why do we use a sealed class?

To prevent inheritance and protect the implementation from being changed through derived classes.

3. Can we create an object of a sealed class?

Yes.

Employee emp = new Employee();
4. Can a sealed class have constructors?

Yes.

5. Can a sealed class have static methods?

Yes.

public sealed class Employee
{
    public static void DisplayCompany()
    {

    }
}
6. Can a sealed class have instance methods?

Yes.

7. Can a sealed class implement an interface?

Yes.

interface IPrintable
{
    void Print();
}

public sealed class Invoice : IPrintable
{
    public void Print()
    {
        Console.WriteLine("Printing Invoice");
    }
}
8. Can a sealed class inherit another class?

Yes.

class Person
{

}

sealed class Employee : Person
{

}

This is valid.

The important point is that nothing can inherit from Employee.

9. Can an abstract class be sealed?

No.

Why?

An abstract class is meant to be inherited, while a sealed class prevents inheritance.

These two concepts contradict each other.

10. Can a sealed class be abstract?

No.

Same reason.
===================================================================================================
Best Practices
Seal a class only when you intentionally want to prevent inheritance.
Avoid sealing classes "just in case." Leave classes extensible unless there is a clear design reason.
Consider sealing immutable classes or classes with security-sensitive logic.
If you only want to stop overriding a specific method, use a sealed override instead of sealing the entire class.
==================================================================================================================
Microsoft Interview Summary
| Question                            | Answer                                         |
| ----------------------------------- | ---------------------------------------------- |
| Can a sealed class be inherited?    | ❌ No                                           |
| Can we create an object?            | ✅ Yes                                          |
| Can it have constructors?           | ✅ Yes                                          |
| Can it have properties and methods? | ✅ Yes                                          |
| Can it implement interfaces?        | ✅ Yes                                          |
| Can it inherit another class?       | ✅ Yes                                          |
| Can it be abstract?                 | ❌ No                                           |
| Can it contain static members?      | ✅ Yes                                          |
| Main purpose                        | Prevent inheritance and protect implementation |
================================================================================================
Easy Way to Remember
| Type             | Object Creation | Inheritance |
| ---------------- | --------------- | ----------- |
| **Normal Class** | ✅ Yes           | ✅ Yes       |
| **Sealed Class** | ✅ Yes           | ❌ No        |
| **Static Class** | ❌ No            | ❌ No        |

Memory Perspective
Normal Class → Memory is allocated when you create an object with new.
Sealed Class → Exactly the same as a normal class. The sealed keyword does not affect memory allocation; it only restricts inheritance.
Static Class → No objects are created. Static fields are allocated when the CLR loads and initializes the type.

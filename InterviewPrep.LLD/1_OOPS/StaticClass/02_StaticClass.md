In C#, a static class is a class declared using the static keyword.
A static class is a class that cannot be instantiated (you cannot create an object of it).
Static classes are commonly used to group utility or helper methods that do not depend on object state.
All members inside a static class must be static.
It cannot be instantiated using the new keyword.
It is implicitly sealed, so it cannot be inherited.
A static class can only have the public or internal access modifiers (it cannot be private, protected, etc.).

Syntax: 
static class Class_Name
{
      // static data members 
     // static method
}

=======================================================================
Declaring a Static Class
A static class is defined like a normal class but prefixed with the static keyword.
static class Utility
{
    public static void PrintMessage(string msg)
    {
        Console.WriteLine(msg);
    }
}

class Program
{
    static void Main()
    {
        Utility.PrintMessage("Hello World");
    }
}
Output
Hello World
Explanation:

Utility is static, so no objects can be created.
Its method is called directly with the class name.
====================================================================================
Real World Example

1. Calculator (Best ⭐⭐⭐⭐⭐)

Interviewer: Can you give a real-world example of a static class?

Answer:

A calculator is a good analogy. A calculator performs operations like addition, subtraction, multiplication, and division,
but it doesn't maintain any user-specific state. Whether one person or a thousand people use it, the logic remains the same. Similarly, 
the .NET Math class is static because it provides common utility methods that don't require creating an object.

2. Company Configuration (Enterprise Example ⭐⭐⭐⭐⭐)

Answer:

In an enterprise application, company-wide configuration is a good example. 
Information such as the company name, support email, or application version is shared across the entire application.
Every module reads the same values, so creating multiple objects would be unnecessary.

3. Logger (Product Company Example ⭐⭐⭐⭐⭐)

Answer:

A logging utility is another good example. Every module in an application may need to log information,
but logging itself doesn't require maintaining object-specific state. Instead of creating a new logger object everywhere,
a shared utility can expose common logging methods.

Therefore it should be static.
=====================================================================================
Microsoft Example

The .NET Framework itself contains many static classes.

Examples:

Math.Sqrt(25);

Console.WriteLine("Hello");

Convert.ToInt32("10");

Environment.MachineName;

You never write

Math math = new Math();

because Math is a static class.
======================================================================================
Example 1 – Utility Class
using System;

public static class Calculator
{
    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine(Calculator.Add(5, 6));

        Console.WriteLine(Calculator.Multiply(3, 4));
    }
}
=======================================================================================
Example 2 – Logger
using System;

public static class Logger
{
    public static void Log(string message)
    {
        Console.WriteLine($"{DateTime.Now} : {message}");
    }
}

class Program
{
    static void Main()
    {
        Logger.Log("Application Started");

        Logger.Log("Employee Saved");
    }
}
=========================================================================================
Example 3 – Hospital Project
public static class HospitalSettings
{
    public static string HospitalName = "ABC Hospital";

    public static string Country = "India";
}

Usage

Console.WriteLine(HospitalSettings.HospitalName);

No object required.

================================================================================================
Example 4 – Configuration
public static class AppConfig
{
    public static string ConnectionString =
        "Server=.;Database=Hospital;Trusted_Connection=True;";
}

Anywhere:

Console.WriteLine(AppConfig.ConnectionString);
==========================================================================================================================

Characteristics of Static Class:
1. Cannot Create Object

❌

Calculator c = new Calculator();

✔

Calculator.Add(10,20);

2. Only Static Members Allowed

Wrong

public static class Test
{
    public int Age;
}

Compile Error

Correct

public static class Test
{
    public static int Age;
}

3. Automatically Sealed

You cannot inherit from a static class.

public static class A
{

}

class B : A
{

}

Compile Error.

4. Cannot be Instantiated

No constructor call like

new Calculator();

5. Can Have Static Constructor
public static class Settings
{
    static Settings()
    {
        Console.WriteLine("Loaded");
    }

    public static string AppName = "Hospital";
}

The static constructor runs automatically once, before the first use of the class.

=============================================================================================================
Static Constructor Example
using System;

public static class Config
{
    static Config()
    {
        Console.WriteLine("Static Constructor");
    }

    public static string Name = "Hospital";
}

class Program
{
    static void Main()
    {
        Console.WriteLine(Config.Name);
    }
}

Output

Static Constructor
Hospital
==================================================================
Memory
Application Starts

          |
          V

CLR

          |

Loads Static Class

          |

One Copy Exists

          |

All Threads Use It

Static data is associated with the type(class), not with an object. There is one copy per application domain/process (depending on runtime context), rather than one copy per instance.

=========================================================================================================
| Static Class                          | Normal Class                          |
| ------------------------------------- | ------------------------------------- |
| Cannot create object                  | Object can be created                 |
| Only static members                   | Static and instance members allowed   |
| Automatically sealed                  | Can be inherited unless sealed        |
| No instance constructor               | Can have instance constructors        |
| One type-level copy of static members | Each object has its own instance data |
| Used for utilities and helpers        | Used to model real-world entities     |


| Instance Class                                | Static Class                                                      |
| --------------------------------------------- | ----------------------------------------------------------------- |
| Memory allocated when `new` creates an object | Static field storage allocated when the CLR loads the type        |
| One copy per object                           | One shared copy per type                                          |
| Requires object                               | No object required                                                |
| Lives on the managed heap as an object        | Static members live in type-associated storage managed by the CLR |

========================================================================================================
Static Method vs Instance Method
public class Employee
{
    public void Work()
    {
    }

    public static void CompanyPolicy()
    {
    }
}

Usage

Employee.CompanyPolicy();

Employee emp = new Employee();

emp.Work();

=============================================================
When Should We Use Static Class?

Use it when

Utility methods
Helper methods
Common calculations
Logging
Configuration
Extension methods (defined in static classes)
Constants
Global application settings

Examples

Math

Convert

Console

Environment

Path

File

Directory
===============================================================================================
When Should We NOT Use Static Class?

Don't use static when every object has its own data.

Wrong

static class Employee
{
    public static string Name;
}

There are many employees.

Each employee has a different name.

This should be

class Employee
{
    public string Name;
}
Real Product Company Example

Employee

class Employee
{
    public int Id;
    public string Name;
}

Logger

static class Logger
{
    public static void Log(string message)
    {

    }
}

Configuration

static class Configuration
{
    public static string ConnectionString;
}

Validation

static class Validator
{
    public static bool IsEmail(string email)
    {

    }
}

=====================================================================================================
Common Interview Questions
1. What is a static class?

A static class cannot be instantiated and contains only static members. It groups functionality that belongs to the type(class) rather than to individual objects.

2. Why use a static class?

Because the functionality has no object-specific state and doesn't require object creation.

3. Can a static class have a constructor?

Yes, but only a static constructor.

It cannot have an instance constructor.

4. Can a static class inherit another class?

No.

Static classes are implicitly sealed and cannot be inherited.

5. Can a normal class inherit a static class?

No.

6. Can a static class implement an interface?

No.

Interfaces describe instance behaviour to be implemented by objects. Since a static class cannot be instantiated, it cannot implement an interface.

7. Can a static class contain instance methods?

No.

Everything inside must be static.

8. Can a normal class contain static methods?

Yes.

class Employee
{
    public static void ShowPolicy()
    {

    }

    public void Work()
    {

    }
}
9. Can we overload static methods?

Yes.

public static class MathUtil
{
    public static int Add(int a, int b) => a + b;

    public static double Add(double a, double b) => a + b;
}
10. Can we override static methods?

No.

Static methods belong to the type, not an instance, so they cannot participate in runtime polymorphism.

11. Are static methods polymorphic?

No.

They are resolved at compile time.

12. Can static methods access instance members?

No, because there is no object.

Wrong

public class Employee
{
    public string Name;

    public static void Print()
    {
        Console.WriteLine(Name);
    }
}

Correct

public class Employee
{
    public string Name;

    public static void Print(Employee employee)
    {
        Console.WriteLine(employee.Name);
    }
}
13. Can instance methods access static members?

Yes.

class Employee
{
    public static string Company = "ABC";

    public void Show()
    {
        Console.WriteLine(Company);
    }
}

14: Where is a static variable stored?

A good answer is:

A static variable belongs to the type, not to an object. Memory for static fields is allocated when the CLR initializes the type. 
There is only one copy of a static field for the entire application (or more precisely, one per loaded type in the relevant runtime context),
and all instances share it.

15: When is memory allocated for a static class?

Answer:

Memory for the static fields of a static class is allocated when the CLR loads and initializes that type, typically before the type is first used.
It is not allocated when an object is created because a static class cannot be instantiated.

Microsoft-Level Tip

Interviewers often ask:

"If no object exists, who owns the static variable?"

The correct answer is:

The type owns it. Static members belong to the class/type itself, not to any object.
The CLR maintains one shared copy for that type, which is why you access it using the class name:

Logger.Count++;
Math.Sqrt(25);
Console.WriteLine("Hello");

No object is needed because the member belongs to the type, not to an instance.

16.Why would you make Logger static(or Configuration,Utility etc. )?
Because logging does not require object-specific state. There should be one shared logging service used throughout the application.

17. Why shouldn't Employee be static?
    Because every employee has different data. If Employee were static, there would only be one shared Name and Id, which is incorrect.

    18.Can a Static Class Have an Indexer?
❌ No

Example:

public static class MyClass
{
    public string this[int index]   // ❌ Compile-time error
    {
        get { return ""; }
        set { }
    }
}
Why?

An indexer is used on an object.

19.Can a Static Class Have Events?
✅ Yes

But the event itself must be static.

=================================================================================
Product Company Best Practices
Keep static classes stateless whenever possible.
Avoid storing mutable global state in static fields because it complicates testing and concurrency.
Use dependency injection for services that may need to be mocked or replaced.
Prefer const or static readonly for values that should not change.
Use static classes for helper methods, extension methods, and application-wide utilities—not for business entities like Customer, Order, or Employee.
Use a static class when the functionality belongs to the application or the type itself, not to individual objects. Examples include Logger, Math, Convert, Path, File,
Directory, and application configuration helpers.

=================================================================================
Microsoft-Level Interview Summary
| Question                                             | Answer |
| ---------------------------------------------------- | ------ |
| Can we create an object of a static class?           | No     |
| Can a static class contain instance members?         | No     |
| Can a static class inherit another class?            | No     |
| Can another class inherit a static class?            | No     |
| Can a static class have a static constructor?        | Yes    |
| Can a static class have an instance constructor?     | No     |
| Can static methods be overloaded?                    | Yes    |
| Can static methods be overridden?                    | No     |
| Can instance methods access static members?          | Yes    |
| Can static methods access instance members directly? | No     |
| Is `Math` a static class?                            | Yes    |
| Is `Console` a static class?                         | Yes    |

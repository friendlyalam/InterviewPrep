Definition

A partial class allows you to split a single class into multiple files.

The compiler combines all the parts into one class during compilation.
Declared using the partial keyword.
A class can be split across multiple files.
All parts must use the same name and namespace.
At compile time, the compiler merges all parts into a single class.
Useful for separating auto-generated and user-defined code.
Supports defining fields, methods, properties, events and nested classes.

Simple Definition:

A partial class is one class whose code is written in multiple files.

===========================================================================================
Syntax
public partial class Employee
{

}

Another file:

public partial class Employee
{

}

The compiler combines them into:

Employee
│
├── Part 1
├── Part 2
└── Part 3

↓

One Employee Class

==================================================================================
Why Do We Need Partial Classes?

Imagine a class with 5000 lines of code.

Employee.cs

---------------------------------

Properties

Methods

Validation

Database Code

Logging

Business Logic

Events

---------------------------------

5000+ Lines

Very difficult to maintain.

Instead, split it.

Employee.Properties.cs

Employee.Methods.cs

Employee.Validation.cs

Employee.Events.cs

All belong to the same class.

======================================================================================
Example 1 (Basic)
Employee.Part1.cs
using System;

public partial class Employee
{
    public int Id;

    public string Name;
}
Employee.Part2.cs
using System;

public partial class Employee
{
    public void Display()
    {
        Console.WriteLine($"{Id} {Name}");
    }
}
Program.cs
using System;

class Program
{
    static void Main()
    {
        Employee emp = new Employee();

        emp.Id = 101;
        emp.Name = "Mohd Alam";

        emp.Display();
    }
}

Output

101 Mohd Alam

Although the class is split across two files, the compiler sees it as one class.

--------------------------------------------------------------------------------------------------------------

Visual Representation
Employee.Part1.cs

partial class Employee
{
    Id
    Name
}

          +

Employee.Part2.cs

partial class Employee
{
    Display()
}

          +

Employee.Part3.cs

partial class Employee
{
    Save()
}

          |

          V

Compiler

          |

          V

One Employee Class

-----------------------------------------------------------------------------------------------
Real-World Example

Suppose you're building a Hospital Management System.

Instead of one huge file:

Patient.cs

5000 Lines

You split it into:

Patient.Properties.cs

Patient.Validation.cs

Patient.Database.cs

Patient.Events.cs

Patient.Helper.cs

Each file focuses on one responsibility, making maintenance much easier.

---------------------------------------------------------------------------------------------------
Example 2 (Production Style)
Employee.Properties.cs
public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Salary { get; set; }
}
Employee.Methods.cs
using System;

public partial class Employee
{
    public void Display()
    {
        Console.WriteLine($"{Id} {Name} {Salary}");
    }
}
Employee.Validation.cs
using System;

public partial class Employee
{
    public bool ValidateSalary()
    {
        return Salary > 0;
    }
}
Program.cs
using System;

class Program
{
    static void Main()
    {
        Employee emp = new Employee();

        emp.Id = 1;
        emp.Name = "John";
        emp.Salary = 70000;

        emp.Display();

        Console.WriteLine(emp.ValidateSalary());
    }
}

--------------------------------------------------------------------------------------------------
Memory

Many beginners think:

"Does partial create multiple objects?"

No.

Employee emp = new Employee();

Memory

Stack

emp --------------------+

                         |

                         V

Heap

Employee Object

Id

Name

Salary

Display()

ValidateSalary()

Only one object is created.

The compiler merged all the partial files before the program ran.

---------------------------------------------------------------------------------------------

Characteristics of Partial Class
1. Same Class in Multiple Files
Employee.cs

Employee1.cs

Employee2.cs

All represent one class.

2. Compiler Combines Them

You never get multiple classes.

The compiler creates only one class definition.

3. Must Use the partial Keyword Everywhere

Wrong

public partial class Employee
{

}

public class Employee
{

}

Compile Error.

Correct

public partial class Employee
{

}

public partial class Employee
{

}
4. Same Namespace Required

Wrong

namespace HR
{
    public partial class Employee
    {

    }
}

namespace Sales
{
    public partial class Employee
    {

    }
}

These are different classes because they're in different namespaces.

5. Same Assembly Required

All parts of a partial class must be compiled into the same assembly.

Partial Class with Constructor
File 1
public partial class Employee
{
    public Employee()
    {
        Console.WriteLine("Constructor");
    }
}
File 2
public partial class Employee
{
    public void Display()
    {

    }
}

Perfectly valid.

Partial Class with Fields
public partial class Employee
{
    private int id;
}

Another file

public partial class Employee
{
    public void Show()
    {
        Console.WriteLine(id);
    }
}

Notice:

id is accessible because it is still the same class.

Partial Class with Event
public partial class Employee
{
    public event Action Saved;
}

Another file

public partial class Employee
{
    public void Save()
    {
        Saved?.Invoke();
    }
}
Partial Class with Nested Class
public partial class Employee
{
    public class Address
    {

    }
}
Partial Method

Partial methods are designed to allow one part of a partial class to declare a method and another part to implement it.

File 1
public partial class Employee
{
    partial void Validate();

    public void Save()
    {
        Validate();

        Console.WriteLine("Saved");
    }
}
File 2
using System;

public partial class Employee
{
    partial void Validate()
    {
        Console.WriteLine("Validation Successful");
    }
}

Output

Validation Successful
Saved

--------------------------------------------------------------------------------------------
| Partial Class                        | Normal Class              |
| ------------------------------------ | ------------------------- |
| Split into multiple files            | Usually one file          |
| Easier maintenance for large classes | Simpler for small classes |
| Compiler merges all parts            | No merging required       |
| One class after compilation          | One class                 |

--------------------------------------------------------------------------------------
| Partial Class                 | Inheritance                            |
| ----------------------------- | -------------------------------------- |
| Same class split across files | Parent and child are different classes |
| Shares all members directly   | Child inherits parent members          |
| One object                    | Separate types                         |

----------------------------------------------------------------------------------------------
| Partial                              | Abstract                     |
| ------------------------------------ | ---------------------------- |
| Organizes source code                | Defines incomplete behaviour |
| Object creation depends on the class | Cannot instantiate directly  |
| About file organisation              | About inheritance and design |

-------------------------------------------------------------------------------

Real Microsoft Examples

Partial classes are used extensively in:

Windows Forms
Form1.cs

Form1.Designer.cs

The designer-generated code is kept separate from your handwritten code.

Entity Framework
Customer.cs

Customer.Extensions.cs

Generated entity code stays separate from custom logic.

WPF
MainWindow.xaml

MainWindow.xaml.cs

The generated code and your code work together through partial classes.

Source Generators

Modern C# source generators also generate partial class code so they can add members without modifying your handwritten files.

--------------------------------------------------------------------------------------------------------------
Advantages
Improves readability.
Makes large classes easier to maintain.
Separates generated code from custom code.
Allows multiple developers to work on the same class with fewer merge conflicts.
Commonly used by code generation tools.


Disadvantages
Too many files can make navigation harder.
Splitting small classes unnecessarily adds complexity.
Doesn't reduce coupling or improve object-oriented design by itself.

----------------------------------------------------------------------------------------------------------------

Microsoft Interview Questions
1. What is a partial class?

A class whose definition is split across multiple files. The compiler combines all parts into a single class.

2. Why do we use partial classes?

To improve maintainability, separate generated code from handwritten code, and allow multiple developers or tools to work on the same class.

3. Does a partial class create multiple objects?

No.

It is still one class.

Employee emp = new Employee();

Creates one object.

4. Can partial classes have constructors?

Yes.

5. Can partial classes inherit another class?

Yes.

public partial class Employee : Person
{

}

Only one part should specify the base class.

6. Can a partial class implement interfaces?

Yes.

public partial class Employee : IDisposable
{

}

Again, declare the interface in one part.

7. Can partial classes contain static members?

Yes.

8. Can a partial class be sealed?

Yes.

public sealed partial class Employee
{

}
9. Can a partial class be abstract?

Yes.

public abstract partial class Employee
{

}
10. Is a partial class a runtime feature?

No.

It is a compile-time feature.

The compiler merges all parts before the application runs.

---------------------------------------------------------------------------------------------------
| Feature         | Supported |
| --------------- | --------- |
| Fields          | ✅         |
| Properties      | ✅         |
| Methods         | ✅         |
| Constructors    | ✅         |
| Events          | ✅         |
| Indexers        | ✅         |
| Nested Classes  | ✅         |
| Static Members  | ✅         |
| Inheritance     | ✅         |
| Interfaces      | ✅         |
| `sealed`        | ✅         |
| `abstract`      | ✅         |
| Generic Classes | ✅         |
| Partial Methods | ✅         |


A partial class is not an OOP concept like inheritance or polymorphism. It is a code organization feature provided by the C# compiler.
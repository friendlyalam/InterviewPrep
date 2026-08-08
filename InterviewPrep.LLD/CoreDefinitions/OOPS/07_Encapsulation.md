Best Definition (Interview Definition)

Encapsulation is the process of bundling data (fields/properties) and the methods that operate on that data into a single unit (class),
while restricting direct access to the internal data and exposing only controlled access through a public interface.

Simple Definition

Encapsulation means wrapping data and behaviour together inside a class and protecting the data from unauthorized or invalid access.

Think of it as:

Data + Methods + Access Control = Encapsulation

Encapsulation hides the internal representation of an object and exposes only necessary operations.
Fields are often kept private while access is provided through public properties or methods.
It improves data security, code maintainability and flexibility.
Access modifiers (private, public, protected, internal) control visibility of members.

----------------------------------------------------------------------------------------

Why Was Encapsulation Introduced?

Imagine there is no encapsulation.

public class Employee
{
    public decimal Salary;
}

Anywhere in the application:

Employee emp = new Employee();

emp.Salary = -50000;

This is allowed.

But a negative salary doesn't make business sense.

Without encapsulation:

Invalid data can enter the system.
Business rules are ignored.
The application becomes unreliable.
With Encapsulation
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
            else
                Console.WriteLine("Salary cannot be negative.");
        }
    }
}

Now:

Employee emp = new Employee();

emp.Salary = -50000;

Output:

Salary cannot be negative.

The object protects its own state.

----------------------------------------------------------------------------------------
Real-Life Example 1 – ATM (Best Example)

An ATM machine has:

Cash

PIN

Account Balance

Can you directly change your account balance inside the ATM?

No.

You can only use the provided operations:

Withdraw()

Deposit()

CheckBalance()

You cannot access the internal circuits or database.

This is encapsulation.

               ATM

      ----------------------

      Private

      Balance

      PIN

      Cash

      ----------------------

      Public

      Withdraw()

      Deposit()

      CheckBalance()

      ----------------------------------------------------------------------------------------------------------------------------------------------

      Real-Life Example 2 – Car

A car contains:

Engine

Gearbox

Fuel Injection

Battery

As a driver, you use:

Start()

Brake()

Accelerate()

Steering

You don't manually control the fuel injectors every second.

The internal implementation is hidden.

-----------------------------------------------------------------------------------------------------------------------------
Real-Life Example 3 – Mobile Phone

You tap:

Call

Camera

Gallery

Messages

You don't directly access:

CPU registers
Memory management
Camera drivers

The operating system hides those implementation details.

---------------------------------------------------------------------------------------------------------------------------

Encapsulation in C#

A class contains:

Fields (Data)

+

Properties

+

Methods

+

Access Modifiers

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

    public void DisplaySalary()
    {
        Console.WriteLine(salary);
    }
}

-----------------------------------------------------------------------------------------------
Memory Representation
Employee emp = new Employee();

Memory:

Stack
──────────────────────────

emp
 │
 ▼

Heap

Employee Object

-----------------------

private salary

Property Salary

DisplaySalary()

-----------------------

Only methods and properties of the class can directly access the private field.

-------------------------------------------------------------------------------------------------------
What Does Encapsulation Achieve?

It protects the object's internal state.

Instead of this:

User

↓

Salary = -10000

We get:

User

↓

Property

↓

Validation

↓

Private Field

The property acts as a security gate.

------------------------------------------------------------------------------------------------

Components of Encapsulation

1. Private Fields
private decimal salary;

The field cannot be accessed directly from outside the class.

2. Public Properties
public decimal Salary
{
    get => salary;
    set => salary = value;
}

Properties provide controlled access.

3. Methods
public void IncreaseSalary(decimal amount)
{
    if (amount > 0)
        salary += amount;
}

Methods can enforce business rules.

4. Access Modifiers

Encapsulation relies heavily on access modifiers.
| Modifier           | Accessible From                      |
| ------------------ | ------------------------------------ |
| private            | Same class only                      |
| protected          | Same class + derived classes         |
| internal           | Same assembly                        |
| public             | Anywhere                             |
| protected internal | Derived classes or same assembly     |
| private protected  | Derived classes in the same assembly |

-----------------------------------------------------------------------------------------------
Advantages
1. Data Security

Sensitive information remains protected.

Examples:

Password
Salary
Bank Balance
PIN
2. Validation

You can prevent invalid data.

Example:

Salary > 0

Age >= 18

Balance >= 0
3. Maintainability

All business rules are kept in one place.

4. Flexibility

Internal implementation can change without affecting callers.

5. Loose Coupling

External code depends on public behaviour rather than internal implementation.

Disadvantages
Slightly more code.
Over-encapsulation can make simple code unnecessarily complex.

--------------------------------------------------------------------------------------------
Encapsulation vs Data Hiding

Many developers think these are the same.

They are related, but different.

Data Hiding

Hiding internal data.

Example:

private decimal salary;
Encapsulation

Wrapping:

Data
Methods
Validation
Access Control

inside one class.

Data Hiding is one technique used to achieve Encapsulation.

----------------------------------------------------------------------------------------------
| Encapsulation                                 | Abstraction                                    |
| --------------------------------------------- | ---------------------------------------------- |
| Protects data                                 | Hides implementation details                   |
| Achieved using classes and access modifiers   | Achieved using abstract classes and interfaces |
| Focuses on "How can data be safely accessed?" | Focuses on "What should be exposed?"           |

Example:

ATM:

Abstraction → Customer sees Withdraw, Deposit, CheckBalance.
Encapsulation → Account balance and PIN are protected and can only be changed through approved operations.

----------------------------------------------------------------------------------------------------------------------------

Microsoft Interview Questions
1. What is Encapsulation?

Wrapping data and methods into a single unit while controlling access to the internal state through access modifiers and a public interface.

2. Why do we use Encapsulation?

To protect data, enforce business rules, improve maintainability, and reduce coupling.

3. Which C# features implement Encapsulation?
Classes
Access Modifiers
Properties
Methods
4. Is private enough for Encapsulation?

No.

private helps hide data, but encapsulation also includes exposing safe operations (properties and methods) that enforce business rules.

5. Can Encapsulation exist without Properties?

Yes.

Methods alone can provide controlled access.

Example:

public void Deposit(decimal amount)
{
    if (amount > 0)
        balance += amount;
}
6. Can public fields provide Encapsulation?

No.

public decimal Balance;

Anyone can modify it directly, so business rules cannot be enforced.

Product Company Best Practices

Instead of:

public decimal Salary;

Prefer:

private decimal salary;

public decimal Salary
{
    get => salary;
    private set => salary = value;
}

public void IncreaseSalary(decimal increment)
{
    if (increment <= 0)
        throw new ArgumentException("Increment must be positive.");

    Salary += increment;
}

This keeps the object in a valid state and centralises business logic.

--------------------------------------------------------------------------------

One-Line Interview Answer

Encapsulation is the OOP principle of bundling data and the operations on that data within a class while restricting direct access to the internal state and exposing controlled,
validated access through a well-defined public interface.
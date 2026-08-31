Product Company Definition

Polymorphism is an OOP principle that allows a single interface, 
reference, or method call to represent multiple behaviours depending on the actual object being used.

Another interview-ready definition:

Polymorphism enables the same operation to behave differently for different objects.


Simple Definition

The word Polymorphism comes from two Greek words:

Poly = Many

Morph = Forms

Meaning:

One thing can have many forms.

------------------------------------------------------------------
Simple Real-Life Example

Suppose you have a remote control.

                Remote

                    │

      ┌─────────────┼─────────────┐

      ▼             ▼             ▼

Sony TV       Samsung TV      LG TV

You press:

Power Button

The button is the same.

But every TV responds differently.

This is Polymorphism.

----------------------------------------------------------------------
Real-Life Example 2

A person behaves differently based on the situation.

Person

↓

At Office

↓

Software Engineer

--------------------

At Home

↓

Father

--------------------

At Hospital

↓

Patient

Same person.

Different behaviour.

--------------------------------------------------------------------------------

Real-Life Example 3

Google Maps

You press:

Navigate

Depending on your choice:

Car Route

Bike Route

Walking Route

Public Transport

Same action.

Different implementation.

---------------------------------------------------------------------------------------
Why Was Polymorphism Introduced?

Imagine there is no polymorphism.

Suppose our application supports:

Email Notification
SMS Notification
Push Notification

Without polymorphism:

if(type == "Email")
{
    // Email logic
}
else if(type == "SMS")
{
    // SMS logic
}
else if(type == "Push")
{
    // Push logic
}

Now the company adds:

WhatsApp

Slack

Teams

Telegram

The code keeps growing.

This violates the Open/Closed Principle (OCP).

--------------------------------------------------------------------------------------

With Polymorphism

Instead of checking every type:

notification.Send();

The runtime automatically calls:

EmailNotification.Send()

or

SmsNotification.Send()

or

PushNotification.Send()

The calling code never changes.

---------------------------------------------------------------------------------------

What Problem Does Polymorphism Solve?

Without polymorphism:

Caller

↓

if

↓

else if

↓

else

↓

else if

↓

else

Every new type requires modifying existing code.

With polymorphism:

Caller

↓

Send()

↓

Runtime chooses

↓

Correct object implementation

Much cleaner.

-----------------------------------------------------------------------------------------------------

Types of Polymorphism in C#

There are two types.

               Polymorphism

          ┌─────────┴─────────┐

          ▼                   ▼

Compile-Time          Runtime

(Method Overloading)  (Method Overriding)

-------------------------------------------------------------------------------------------------------

1. Compile-Time Polymorphism

Also called:

Static Polymorphism
Early Binding

Achieved using:

Method Overloading
Operator Overloading

The compiler decides which method to call.

Method Overloading

Same method name.

Different parameters.

Example:

using System;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}

class Program
{
    static void Main()
    {
        Calculator calculator = new Calculator();

        Console.WriteLine(calculator.Add(10, 20));

        Console.WriteLine(calculator.Add(10, 20, 30));

        Console.WriteLine(calculator.Add(10.5, 20.5));
    }
}

Output:

30

60

31

The compiler knows exactly which method to call before the program runs.

----------------------------------------------------------------------------------------
Memory Representation
Calculator calculator = new Calculator();
Stack

calculator

      │

      ▼

Heap

Calculator Object

-------------------------

Add(int,int)

Add(int,int,int)

Add(double,double)

The object contains all overloaded methods.

The compiler chooses the correct one based on the method signature.

-----------------------------------------------------------------------------------------

Rules for Method Overloading

Allowed:

Display(int)

Display(string)

Display(int,string)

Display(double)

Not allowed:

int Display(int)

string Display(int)

Return type alone cannot distinguish overloaded methods.

------------------------------------------------------------------------------------------------

2. Runtime Polymorphism

Also called:

Dynamic Polymorphism
Late Binding

Achieved using:

Inheritance
Method Overriding
virtual
override

The runtime decides which method to execute.

Example
using System;

public class Notification
{
    public virtual void Send()
    {
        Console.WriteLine("Sending generic notification.");
    }
}

public class EmailNotification : Notification
{
    public override void Send()
    {
        Console.WriteLine("Sending Email.");
    }
}

class Program
{
    static void Main()
    {
        Notification notification =
            new EmailNotification();

        notification.Send();
    }
}

Output:

Sending Email.

Notice:

Reference type:

Notification

Actual object:

EmailNotification

Runtime chooses:

EmailNotification.Send()

-------------------------------------------------------------------------------------

Memory Representation
Notification notification =
    new EmailNotification();
Stack

notification

      │

      ▼

Heap

EmailNotification Object

↓

Inherited Members

↓

Overridden Send()

There is no separate Notification object.

Only the derived object exists.

-------------------------------------------------------
Compile-Time vs Runtime Polymorphism
| Compile-Time             | Runtime                         |
| ------------------------ | ------------------------------- |
| Method Overloading       | Method Overriding               |
| Early Binding            | Late Binding                    |
| Compiler decides         | Runtime decides                 |
| Same class is common     | Requires inheritance            |
| Faster method resolution | Small runtime dispatch overhead |

-----------------------------------------------------------------------------------------
virtual

Marks a method that can be overridden.

public virtual void Send()
{
}

--------------------------------
override

Replaces the base implementation.

public override void Send()
{
}

-----------------------------------------------------------
new

Hides the base method instead of overriding it.

public new void Send()
{
}

This is method hiding, not runtime polymorphism.

---------------------------------------------------------------------
When Does Runtime Polymorphism Happen?

When all of these are true:

A base class defines a virtual (or abstract) method.
A derived class overrides that method.
A base-class reference points to a derived-class object.

Example:

Notification notification = new EmailNotification();

notification.Send();

This is where runtime dispatch occurs.

----------------------------------------------------------------

Advantages
1. Extensibility

Adding a new implementation usually doesn't require changing existing calling code.

2. Cleaner Code

Instead of many if-else blocks:

notification.Send();
3. Better Maintainability

New behaviour can often be introduced by creating a new derived class.

4. Supports SOLID

Especially:

Open/Closed Principle (OCP)
Liskov Substitution Principle (LSP)

------------------------------------------------------------------------------------------------
Disadvantages
Requires good object-oriented design.
Excessive inheritance can make code harder to understand.
Runtime polymorphism has a very small method-dispatch cost (usually negligible in business applications).

------------------------------------------------------------------------------------------------------------

Common Mistakes
Mistake 1

Thinking overloading and overriding are the same.

They are different.

Mistake 2

Forgetting virtual.

public void Send()
{
}

Cannot be overridden.

Mistake 3

Using new instead of override.

new hides the method.

It does not provide runtime polymorphism.

-----------------------------------------------------------------------------------------------------
Product Company Interview Questions
1. What is Polymorphism?

Allowing the same interface or method call to exhibit different behaviour depending on the actual object.

2. Why do we need Polymorphism?

To write flexible, extensible, and maintainable code without repeatedly modifying existing logic for new types.

3. How many types of polymorphism exist in C#?
Compile-Time (Method Overloading)
Runtime (Method Overriding)
4. Does method overloading require inheritance?

No.

It usually happens within the same class.

5. Does runtime polymorphism require inheritance?

Yes.

It requires a base type and a derived type (or an interface and its implementation).

6. Which is faster?

Compile-time polymorphism.

Because the compiler already knows which method to call.

7. Which keywords are required for runtime polymorphism?
virtual or abstract
override
8. Is method hiding (new) polymorphism?

No.

It hides the base implementation instead of participating in runtime polymorphism.

9. Can abstract methods participate in polymorphism?

Yes.

Every implementation of an abstract method is resolved through runtime polymorphism.

10. Which design principles benefit from polymorphism?
Open/Closed Principle (OCP)
Liskov Substitution Principle (LSP)
Dependency Inversion Principle (DIP)

--------------------------------------------------------------------------------------------------
Interview-Ready Definition

Polymorphism is the object-oriented principle that allows a single interface, reference, or method call to invoke different
implementations based on the actual object at runtime or the method signature at compile time, resulting in flexible, extensible, and maintainable software.
1. Upcasting
Product Company Definition

Upcasting is the process of assigning a derived-class object to a base-class reference.

Example:

Notification notification = new EmailNotification();

Here:

Reference Type  : Notification

Actual Object   : EmailNotification

------------------------------------------------------------------------
Why is it called Upcasting?

Think of the inheritance hierarchy.

              Notification
                    ▲
                    │
            EmailNotification

Moving up the hierarchy:

EmailNotification

↓

Notification

Hence the name Upcasting.

--------------------------------------------------------------------------------

Real-Life Example
Vehicle

▲

Car

A Car is a Vehicle.

So this is valid:

Vehicle vehicle = new Car();

---------------------------------------------------------------------------
Memory Representation
Notification notification =
        new EmailNotification();

Memory

Stack
──────────────────────────

notification

      │

      ▼

Heap
──────────────────────────

EmailNotification Object

↓

Inherited Members

↓

Overridden Methods

Notice carefully:

There is only one object.

There is NO Notification object.

----------------------------------------------------------
Why do Product Companies Prefer Upcasting?

Because business code should depend on abstractions, not concrete classes.

Bad

EmailNotification email =
    new EmailNotification();

Good

Notification notification =
    new EmailNotification();

Now tomorrow you can replace:

new EmailNotification()

with

new SmsNotification()

without changing most calling code.

---------------------------------------------------------------------------------------

2. Downcasting
Definition

Converting a base reference back into a derived type.

Example

Notification notification =
    new EmailNotification();

EmailNotification email =
    (EmailNotification)notification;

Now you can access child-specific members.

--------------------------------------------------------------------------------------

Why do we need Downcasting?

Suppose:

public class EmailNotification
{
    public void AttachPdf()
    {

    }
}

The base class doesn't know about:

AttachPdf()

So

notification.AttachPdf();

❌ Not allowed

After downcasting:

EmailNotification email =
    (EmailNotification)notification;

email.AttachPdf();

Now it works.


--------------------------------------------------------------------------------------
Memory Representation
Stack

notification

↓

EmailNotification Object

------------------------

email

↓

Same Object

Both variables reference the same object.

--------------------------------------------------------------------------------
Invalid Downcasting
Notification notification =
    new SmsNotification();

EmailNotification email =
    (EmailNotification)notification;

Runtime Error

InvalidCastException

Because the object is actually an SmsNotification.

-------------------------------------------------------------------------------------
Safe Downcasting using is

Instead of:

EmailNotification email =
    (EmailNotification)notification;

Do:

if(notification is EmailNotification)
{
    EmailNotification email =
        (EmailNotification)notification;
}

Now the cast is safe.

--------------------------------------------------------------------------------------------

Better Approach (Pattern Matching)

Modern C#

if(notification is EmailNotification email)
{
    email.AttachPdf();
}

This is cleaner.

as Operator

Instead of throwing an exception:

EmailNotification email =
    notification as EmailNotification;

If conversion fails:

email == null

No exception.

Example

EmailNotification email =
    notification as EmailNotification;

if(email != null)
{
    email.AttachPdf();
}

----------------------------------------------------------------------------------------------------

| `is`                             | `as`                                    |
| -------------------------------- | --------------------------------------- |
| Checks the type                  | Attempts the conversion                 |
| Returns `true` or `false`        | Returns object or `null`                |
| Good for conditions              | Good when you need the converted object |
| Cannot replace casting by itself | Eliminates an extra explicit cast       |

---------------------------------------------------------------------------------------------------------
Dynamic Dispatch

One of the most important interview topics.

Suppose

Notification notification =
    new EmailNotification();

notification.Send();

Question:

Which Send() method executes?

Answer:

Runtime checks

↓

Actual Object

↓

EmailNotification

↓

Calls EmailNotification.Send()

This is called

Dynamic Dispatch

----------------------------------------------------------------------------------
Visual Flow
notification.Send()

↓

CLR

↓

What object?

↓

EmailNotification

↓

Execute

EmailNotification.Send()

Not

Notification.Send()

-------------------------------------------------------------------------------------------
Why Does This Happen?

Because:

virtual

override

enable runtime method resolution.

--------------------------------------------------------------------------------------------

Method Hiding vs Overriding

Very common interview question.

Method Hiding
using System;

class Animal
{
    public void Speak()
    {
        Console.WriteLine("Animal");
    }
}

class Dog : Animal
{
    public new void Speak()
    {
        Console.WriteLine("Dog");
    }
}

class Program
{
    static void Main()
    {
        Animal animal =
            new Dog();

        animal.Speak();
    }
}

Output

Animal

Why?

Because:

new

↓

Hide

↓

Compile-Time Decision
Method Overriding
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
    public override void Speak()
    {
        Console.WriteLine("Dog");
    }
}

class Program
{
    static void Main()
    {
        Animal animal =
            new Dog();

        animal.Speak();
    }
}

Output

Dog

Because runtime chooses the actual object.

------------------------------------------------------------------------------------------------------------
| `new`                   | `override`                       |
| ----------------------- | -------------------------------- |
| Hides the base method   | Replaces the base implementation |
| Compile-time resolution | Runtime resolution               |
| No runtime polymorphism | Supports runtime polymorphism    |
| Uses method hiding      | Uses dynamic dispatch            |

-----------------------------------------------------------------------------------------------------------------

Can Base Reference Access Child Members?

Example

Animal animal =
    new Dog();

Can we do:

animal.Fetch();

No.

Why?

The reference type is:

Animal

The compiler only allows members defined on Animal.

After downcasting:

Dog dog = (Dog)animal;

dog.Fetch();

Now it works.

---------------------------------------------------------------------------------------------------------------------

Common Product Company Questions
Can runtime polymorphism exist without inheritance?

No.

It requires a base type (or interface) and a derived implementation.

Can constructors be overridden?

No.

Constructors are not inherited.

Can static methods be overridden?

No.

Static methods belong to the type, not the object.

Can private methods be overridden?

No.

Private members are not accessible to derived classes.

Why is overriding faster than an if-else chain?

It's not necessarily faster. The main advantage is maintainability and extensibility. Runtime dispatch has a very small overhead,
but it avoids growing conditional logic and keeps code open for extension.

Which design patterns rely heavily on polymorphism?
Strategy Pattern
Factory Pattern
Command Pattern
State Pattern
Template Method Pattern
Visitor Pattern


Which SOLID principles benefit?
Open/Closed Principle
Liskov Substitution Principle
Dependency Inversion Principle

-------------------------------------------------------------------------------------
Best Practices

✅ Program against base classes or interfaces.

Notification notification =
    new EmailNotification();

instead of

EmailNotification email =
    new EmailNotification();

✅ Prefer override over new when the goal is polymorphic behaviour.

✅ Use pattern matching instead of manual casting where possible.

if (notification is EmailNotification email)
{
    email.AttachPdf();
}

✅ Avoid unnecessary downcasting.

If you frequently need to downcast, it may indicate that the base abstraction is missing behaviour or the design can be improved.

Summary
Polymorphism

│

├── Compile-Time

│      └── Method Overloading

│

└── Runtime

       ├── virtual

       ├── override

       ├── Upcasting

       ├── Downcasting

       ├── Dynamic Dispatch

       ├── is

       ├── as

       └── Pattern Matching
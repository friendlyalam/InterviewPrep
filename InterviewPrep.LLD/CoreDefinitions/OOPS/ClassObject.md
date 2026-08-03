In Object-Oriented Programming, classes and objects are fundamental concepts used to represent real-world concepts and entities.

A class is a blueprint (template) used to create objects.

It defines:
Properties (Data)
Methods (Behavior)
Constructors
Fields
Events
Indexers
Nested Types

Explanation of Each Member
| Member           | Purpose                                                                            | Example                            |
| ---------------- | ---------------------------------------------------------------------------------- | ---------------------------------- |
| **Fields**       | Store the internal state of the object. Usually `private`.                         | `_salary`                          |
| **Properties**   | Provide controlled access to fields using `get`/`set`.                             | `Salary`                           |
| **Constructors** | Initialize an object when it is created.                                           | `new Employee(101, "John", 75000)` |
| **Methods**      | Define the actions or behaviour of the object.                                     | `Work()`, `Display()`              |
| **Events**       | Notify other objects that something has happened.                                  | `SalaryChanged`                    |
| **Indexers**     | Allow an object to be accessed like an array.                                      | `emp[0] = "C#"`                    |
| **Nested Types** | Define a class (or other type) inside another class when they are closely related. | `Employee.Address`                 |


A class itself does not occupy memory for its instance members until an object is created.
For example, Dog is a class, while a specific dog like Tommy is an object of that class.
A class is a user-defined data type that encapsulates data and behavior.

Syntax:

class ClassName{
    // Fields
    // Properties
    // Methods
}

=============================
Declaration of Class
A class declaration begins with the class keyword followed by the class name. However, some optional attributes can be used with
class declaration according to the application requirement. Class declarations can include these components, in order:

Modifiers: Define the accessibility of a class. By default, a class is internal.
Keyword class: Used to declare a class.
Class Identifier: The name of the class, conventionally starting with a capital letter.
Base Class (Optional): Specifies a parent class to inherit from, using the : symbol.
Interfaces (Optional): A comma-separated list of interfaces implemented by the class, also preceded by : A class can implement multiple interfaces.
Body: Enclosed within { }, containing members like fields, properties, methods, constructors and events.
=============================================================================================================
Real-life Example

Think of a House Blueprint.

The blueprint contains:

Number of rooms
Color
Door
Windows

But you cannot live in a blueprint.

You need to build the actual house.

The blueprint = Class

The actual house = Object
=========================================================================
Another Real-world Example

Consider a Car.

Every car has

Brand
Model
Color
Speed

Every car can

Start()
Stop()
Accelerate()

These common characteristics become a class.

class Car
{
    public string Brand;
    public string Color;

    public void Start()
    {
        Console.WriteLine("Car Started");
    }

    public void Stop()
    {
        Console.WriteLine("Car Stopped");
    }
}
=========================================================

An object is an instance of a class.

Once an object is created with New Keyword, memory is allocated.
A class can have multiple objects, each with its own set of data.

Car car1 = new Car();
Here
Car is the class.
car1 is the object.
==========================================================
Memory Representation
Class

Car
------------------------
Brand
Color
Start()
Stop()

        |
        | new
        V

Heap Memory

-----------------------
Object (car1)

Brand = Honda
Color = White
-----------------------

=========================================================================
Complete Program Example
using System;

class Car
{
    public string Brand;
    public string Color;

    public void Display()
    {
        Console.WriteLine($"Brand : {Brand}");
        Console.WriteLine($"Color : {Color}");
    }

    public void Start()
    {
        Console.WriteLine($"{Brand} Started");
    }
}

class Program
{
    static void Main()
    {
        Car car1 = new Car();

        car1.Brand = "Honda";
        car1.Color = "White";

        car1.Display();
        car1.Start();
    }
}

========================================================================

Multiple Objects

One class can create many objects.

Car car1 = new Car();
Car car2 = new Car();
Car car3 = new Car();

Each object has its own data.

car1.Brand = "BMW";
car2.Brand = "Audi";
car3.Brand = "Mercedes";

Memory

Heap

car1
Brand = BMW

car2
Brand = Audi

car3
Brand = Mercedes

==================================================================
Advantages

✔ Code Reusability

✔ Easy Maintenance

✔ Better Organization

✔ Encapsulation Support

✔ Inheritance Support

✔ Polymorphism Support

✔ Abstraction Support

============================================================================
Class vs Object
| Class                                    | Object                 |
| ---------------------------------------- | ---------------------- |
| Blueprint                                | Real instance          |
| Logical entity                           | Physical entity        |
| No instance memory until object creation | Occupies memory        |
| Defines data and behavior                | Uses data and behavior |
| Can create many objects                  | Belongs to one class   |


===============================================================================================
Important Interview Questions
1. What is a class?

A class is a user-defined reference type that acts as a blueprint for creating objects. It contains data (fields/properties) and behavior (methods).

2. What is an object?

An object is an instance of a class. It represents a real-world entity and occupies memory when created.

3. Can we create an object without a class?

No (except some runtime-generated types). In normal C#, every object is an instance of some class (or another reference/value type).

4. Where is an object stored?

For a normal class instance:

The object data is typically allocated on the managed heap.
A local variable like car1 holds a reference to that object (the reference itself is stored where the variable lives, such as the stack for a local variable).

5. Does a class occupy memory?

The class definition (metadata) exists in the program, but instance data is not allocated until you create an object with new.

6. Can one class create multiple objects?

Yes.

Car car1 = new Car();
Car car2 = new Car();
Car car3 = new Car();

Each object has its own independent state.

7. What keyword creates an object?
new

Example

Car car = new Car();
8. Is a class a reference type?

Yes.

All normal classes are reference types in C#.

Common Mistakes Beginners Make

❌ Forgetting to create an object before accessing instance members:

Car.Start(); // Error: Start is not static

✔ Correct:

Car car = new Car();
car.Start();

❌ Assuming all objects share the same field values:

Car car1 = new Car();
Car car2 = new Car();

car1.Brand = "Honda";
car2.Brand = "BMW";

Console.WriteLine(car1.Brand); // Honda
Console.WriteLine(car2.Brand); // BMW

Each object has its own copy of instance fields.

Best Practices
Use properties instead of public fields in production code.
Keep related data and behavior together in the same class.
Give classes meaningful names (Customer, Invoice, Patient).
Follow PascalCase naming for classes and properties.
Make fields private unless there's a good reason to expose them directly.


=================================================================================================

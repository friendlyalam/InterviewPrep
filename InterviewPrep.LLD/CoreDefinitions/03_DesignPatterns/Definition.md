Product Company Definition

A Design Pattern is a proven, reusable solution to a commonly occurring software design problem.

Notice the wording carefully.

A design pattern is not code.

It is not a framework.

It is not a library.

It is a solution template that helps developers solve recurring design problems.

Design patterns are defined as reusable solutions to the common problems that arise during software design and development. 
They are general templates or best practices that guide developers in creating well-structured, maintainable, and efficient code.

--------------------------
Simple Definition

Imagine you are building houses.

Whenever you build a kitchen, you don't invent a new layout every time.

You reuse a layout that has worked well before.

That reusable solution is a pattern.

Software design patterns work the same way.

------------------------------------------------------------------
Why were Design Patterns introduced?

Early software projects often had problems like:

Tight coupling
Duplicate code
Difficult maintenance
Hard testing
Difficult extension
Fragile architecture

Experienced developers noticed these problems repeated across projects.

Instead of solving them from scratch every time, they documented proven solutions.

Those solutions became Design Patterns.

------------------------------------------------

Real-Life Example

Suppose you build a hospital.

Every room has:

Doors
Windows
Electrical wiring
Plumbing

You don't redesign the plumbing system for every hospital.

You reuse a proven blueprint.

That blueprint is similar to a design pattern.

------------------------------------------------------

Are Design Patterns mandatory?

No.

Use a pattern only when it solves a real problem.

A common interview saying is:

Don't use a Design Pattern because you know it. Use it because the problem requires it.

-----------------------------------------------------------------

Categories of Design Patterns

The classic Gang of Four (GoF) book defines 23 Design Patterns, grouped into three categories.

1. Creational Patterns

Focus: Object Creation

These answer:

How should objects be created?

Creational design patterns abstract the instantiation process. They help make a system independent of how its objects are created, 
composed, and represented. A class creational pattern uses inheritance to vary the class that’s instantiated, 
whereas an object creational pattern will delegate instantiation to another object. 
Creational patterns give a lot of flexibility in what gets created, who creates it, how it gets created, and, when. 

There are two recurring themes in these patterns: 

They all encapsulate knowledge about which concrete class the system uses. 
They hide how instances of these classes are created and put together.

Patterns:

| Pattern          | Purpose                                      |
| ---------------- | -------------------------------------------- |
| Singleton        | Only one object should exist                 |
| Factory Method   | Let subclasses decide which object to create |
| Abstract Factory | Create related families of objects           |
| Builder          | Build complex objects step by step           |
| Prototype        | Clone existing objects                       |



2. Structural Patterns

Focus: Object Structure

These answer:

How should classes and objects be organized?

Structural Design Patterns are concerned with how classes and objects are composed to form larger structures.
Structural class patterns use inheritance to compose interfaces or implementations.
Consider how multiple inheritances mix two or more classes into one. The result is a class that combines the properties of its parent classes.

There are two recurring themes in these patterns: 

This pattern is particularly useful for making independently developed class libraries work together. 
Structural Design Patterns describe ways to compose objects to realize new functionality.
The added flexibility of object composition comes from the ability to change the composition at run-time, which is impossible with static class composition. 

Patterns:
| Pattern   | Purpose                                           |
| --------- | ------------------------------------------------- |
| Adapter   | Make incompatible interfaces work together        |
| Bridge    | Separate abstraction from implementation          |
| Composite | Tree structures (folders, menus)                  |
| Decorator | Add behavior without modifying code               |
| Facade    | Provide a simple interface to a complex subsystem |
| Flyweight | Share objects to reduce memory usage              |
| Proxy     | Control access to another object                  |



3. Behavioral Patterns

Focus: Communication and Behavior

These answer:

How should objects interact?

Behavioral Patterns are concerned with algorithms and the assignment of responsibilities between objects.
Behavioral patterns describe not just patterns of objects or classes but also the patterns of communication between them. 
These patterns characterize complex control flow that’s difficult to follow at run-time.

There are three recurring themes in these patterns:

Behavioral class patterns use inheritance to distribute behavior between classes. 
Behavioral object patterns use object composition rather than inheritance.
Behavioral object patterns are concerned with encapsulating behavior in an object and delegating requests to it. 

Patterns:
| Pattern                 | Purpose                                  |
| ----------------------- | ---------------------------------------- |
| Strategy                | Choose algorithms at runtime             |
| Observer                | Publish/Subscribe notifications          |
| Command                 | Encapsulate requests as objects          |
| State                   | Change behavior based on state           |
| Chain of Responsibility | Pass requests through handlers           |
| Mediator                | Centralize communication                 |
| Memento                 | Save and restore state                   |
| Template Method         | Define algorithm skeleton                |
| Visitor                 | Add operations without modifying classes |
| Iterator                | Traverse collections                     |
| Interpreter             | Interpret expressions                    |

----------------------------------
Which Patterns are Most Important for Product Companies?

Not all 23 are asked equally.

Tier 1 (Must Know)

| Pattern          | Importance |
| ---------------- | ---------- |
| Factory Method   | ⭐⭐⭐⭐⭐      |
| Abstract Factory | ⭐⭐⭐⭐⭐      |
| Singleton        | ⭐⭐⭐⭐⭐      |
| Builder          | ⭐⭐⭐⭐⭐      |
| Strategy         | ⭐⭐⭐⭐⭐      |
| Observer         | ⭐⭐⭐⭐⭐      |
| Decorator        | ⭐⭐⭐⭐⭐      |
| Adapter          | ⭐⭐⭐⭐⭐      |


These are commonly used in enterprise .NET applications.

-------------------------------------------------------
Tier 2 (Very Useful)

| Pattern                 | Importance |
| ----------------------- | ---------- |
| Facade                  | ⭐⭐⭐⭐       |
| Command                 | ⭐⭐⭐⭐       |
| State                   | ⭐⭐⭐⭐       |
| Chain of Responsibility | ⭐⭐⭐⭐       |
| Proxy                   | ⭐⭐⭐⭐       |
| Composite               | ⭐⭐⭐⭐       |


---------------------------------------------
Tier 3 (Good to Know)
| Pattern         | Importance |
| --------------- | ---------- |
| Prototype       | ⭐⭐⭐        |
| Bridge          | ⭐⭐⭐        |
| Flyweight       | ⭐⭐⭐        |
| Mediator        | ⭐⭐⭐        |
| Template Method | ⭐⭐⭐        |
| Iterator        | ⭐⭐⭐        |
| Visitor         | ⭐⭐         |
| Interpreter     | ⭐          |




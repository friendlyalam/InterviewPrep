1. Product Company Definition

Aggregation is a weak "HAS-A" relationship in which one class contains references to one or more objects of another class,
but those objects have their own independent lifecycle.

Interview Definition

Aggregation represents a whole-part relationship where the parent object uses child objects,
but does not own their lifetime. The child objects can exist even if the parent object is destroyed.

--------------------------------------------------------------------------------------------------------
2. Simple Definition

Aggregation means:

One object has another object, but does not own it.

Example:

Company HAS Employees

If the company closes,

employees still exist.

They can join another company.

-----------------------------------------------------------------------------------------------------------

3. Why Aggregation Was Introduced?

Suppose we're developing an enterprise application.

Initially,

Company

needs employees.

Should the company create employees itself?

public Company()
{
    _employees.Add(new Employee(...));
}

❌ Bad design.

Why?

Because employees already exist in HR.

The company should receive employees instead of creating them.

Aggregation solves this problem.

----------------------------------------------------------------------------------------------------------------
Characteristics
Weak ownership
HAS-A relationship
Whole-part relationship
Child objects exist independently
Parent stores references
Child objects are usually created outside the parent
Child objects can be shared or moved to another parent

-----------------------------------------------------------------------------------------------------------------

Real Product Company Examples

| Parent            | Child            | Why Aggregation?                    |
| ----------------- | ---------------- | ----------------------------------- |
| Airline           | Pilot            | Pilot can join another airline      |
| Company           | Employee         | Employee can switch companies       |
| Hospital          | Doctor           | Doctor can work elsewhere           |
| University        | Professor        | Professor can move universities     |
| Logistics Company | Delivery Vehicle | Vehicle can be sold or reassigned   |
| Sports Team       | Player           | Player can transfer to another team |

---------------------------------------------------------------------------------------------------------------------

| Association                               | Aggregation                      |
| ----------------------------------------- | -------------------------------- |
| Uses relationship                         | Weak HAS-A relationship          |
| Collaboration between independent objects | Whole-part relationship          |
| Parent may not store the reference        | Parent usually stores references |
| Example: Customer places Order            | Example: Airline has Pilots      |


----------------------------------------------------------------------------------------------------------------

| Aggregation                   | Composition                         |
| ----------------------------- | ----------------------------------- |
| Weak ownership                | Strong ownership                    |
| Child exists independently    | Child depends on parent             |
| Child usually created outside | Child usually created inside parent |
| Parent references child       | Parent owns child                   |
| Airline → Pilot               | House → Room                        |


------------------------------------------------------------------------------------------------------------

Common Interview Questions
Q1. What is Aggregation?

Aggregation is a weak HAS-A relationship where a parent object references child objects, but the child objects have an independent lifecycle.

Q2. Why is it called weak ownership?

Because the parent uses the child but does not control when the child is created or destroyed.

Q3. Who creates the child object?

Typically, another part of the application creates the child object first, and then passes it to the parent.

Example:

Pilot pilot = new Pilot(...);

Airline airline = new Airline("Sky Wings",
                              new List<Pilot> { pilot });
Q4. Can the same child object belong to another parent?

Yes.

For example:

Airline airline1 =
    new Airline("Sky Wings",
        new List<Pilot> { pilot1 });

Airline airline2 =
    new Airline("Global Air",
        new List<Pilot> { pilot1 });

From an OOP perspective, this is possible because the Pilot object has an independent lifecycle. 
Whether it makes sense for the business domain is a separate rule that your application would enforce.

Q5. Is Aggregation implemented using inheritance?

No.

Aggregation is implemented using object references, not inheritance.

Q6. Why do product companies use Aggregation?

Because many business entities already exist independently.

Examples:

Employees are hired before being assigned to departments.
Doctors are assigned to hospitals.
Drivers are assigned to delivery hubs.
Pilots are assigned to airlines.

Aggregation models these relationships naturally while keeping the design flexible.

Best Practices
Use Aggregation when the child object has its own lifecycle.
Pass child objects through constructors or methods rather than creating them inside the parent.
Keep ownership separate from collaboration.
Avoid using Composition when the child should outlive the parent.

-------------------------------------------------------------------------------------------------------------------
Interview Summary
Aggregation
      │
      ▼
Weak HAS-A Relationship
      │
      ▼
Whole-Part Relationship
      │
      ▼
Parent References Child
      │
      ▼
Child Has Independent Lifecycle
      │
      ▼
Child Usually Created Outside Parent
Product Company Insight

Many developers say:

"Aggregation means HAS-A relationship."

That answer is incomplete.

----------------------------------------------------------------------------------------------------

A stronger interview answer is:

Aggregation is a weak HAS-A relationship where the parent references child objects but does not own their lifecycle.
The child objects are typically created independently and can continue to exist or be associated with another parent even after the original parent is destroyed.
Product Company Definition

Association is a relationship between two independent classes where one object can use or interact with another object to perform a business operation, while both objects have independent lifecycles.

Interview Definition

Association represents a "uses" or "works with" relationship between objects. Both objects can exist independently of each other.

2. Simple Definition

Association means:

Two objects know about each other and collaborate, but neither owns the other.

Example:

Customer

        books

Flight

A customer can book a flight.

The flight exists without that customer.

The customer also exists without that flight.

-----------------------------------------------------------------------------------

3. Why Do We Need Association?

Imagine a flight booking application.

Without association:

Customer

(No way to book a flight)

There is no relationship.

Now introduce:

Customer -------- Flight

Now the business rule becomes possible:

A customer books a flight.

Neither object owns the other.

They simply collaborate.

------------------------------------------------------------------------------

4. Real-Life Examples
Example 1
Doctor -------- Patient

A doctor treats patients.

If the doctor resigns,

patients still exist.

If a patient changes hospitals,

the doctor still exists.

Independent lifecycles.

----------------------------------------------------------------------------

Example 2
Teacher -------- Student

Teacher leaves school.

Students still exist.

Student transfers school.

Teacher still exists.

---------------------------------------------------------------------

5. Enterprise Example

Let's build an Airline Reservation System.

Flight Class
public class Flight
{
    public string FlightNumber { get; }

    public Flight(string flightNumber)
    {
        FlightNumber = flightNumber;
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Flight : {FlightNumber}");
    }
}
Customer Class
public class Customer
{
    public string Name { get; }

    public Customer(string name)
    {
        Name = name;
    }

    public void BookFlight(Flight flight)
    {
        Console.WriteLine(
            $"{Name} booked {flight.FlightNumber}");
    }
}
Main Program
using System;

class Program
{
    static void Main()
    {
        Flight flight =
            new Flight("AI-202");

        Customer customer =
            new Customer("Mohd Alam");

        customer.BookFlight(flight);
    }
}

Output

Mohd Alam booked AI-202
6. Why Is This Association?

Notice carefully.

Customer

↓

uses

↓

Flight

The customer does not own the flight object.

The flight object was created independently.

It was simply passed to:

BookFlight(flight);

-----------------------------------------------------------------
7. Memory Representation
Flight flight =
    new Flight("AI-202");

Customer customer =
    new Customer("Mohd Alam");

 --------------------------------------------------------------------------------------------

Memory

Stack
────────────────────

flight
customer

      │        │
      ▼        ▼

Heap
────────────────────

Flight Object

FlightNumber


Customer Object

Name

When:

customer.BookFlight(flight);

During the method call:

Customer

↓

Temporary reference

↓

Flight

No ownership is created.

No new object is created.

The existing Flight object is simply used.

--------------------------------------------------------------------

8. Life Cycle

Suppose:

Customer customer =
    null;

The customer object becomes eligible for garbage collection (assuming no other references).

The flight object still exists.

Suppose:

Flight flight =
    null;

The customer still exists.

This is the key property of association.

Both objects have independent lifecycles.

-----------------------------------------------------------------------------

9. UML Representation
+-----------+           +-----------+

| Customer  | --------> |  Flight   |

+-----------+           +-----------+

A plain line indicates an association.

-------------------------------------------------------------------------------

10. Why Product Companies Use Association

Consider a food delivery application.

Customer

↓

Restaurant

↓

DeliveryPartner

↓

Coupon

↓

Payment

None of these objects own one another.

They simply collaborate to complete an order.

Association models these interactions cleanly.

----------------------------------------------------------------------------------

11. Common Mistakes
Mistake 1

Thinking every relationship is inheritance.

Wrong:

Customer

↓

Flight

A customer is not a flight.

Inheritance doesn't fit.

Mistake 2

Thinking association means ownership.

Ownership belongs to:

Composition
Aggregation

Association is simply collaboration.

--------------------------------------------------------
12. Association vs Inheritance

Association

Customer

↓

uses

↓

Flight

Inheritance

Vehicle

↓

Car

One models interaction.

The other models an is-a relationship.

---------------------------------------------------------------------------------

13. Association vs Interface

Interface

IAirline

↓

AirIndia

Defines a capability.

Association

Customer

↓

AirIndia

Defines collaboration between objects.

-------------------------------------------------------------------
14. Common Product Company Questions
Q1. What is association?

A relationship where two independent objects collaborate without owning each other.

Q2. Can associated objects exist independently?

Yes.

That is the defining characteristic of association.

Q3. Does association imply ownership?

No.

Association represents collaboration, not ownership.

Q4. Can one object be associated with many objects?

Yes.

Example:

Customer

↓

Many Flights

or

Doctor

↓

Many Patients
Q5. Is association a compile-time or runtime concept?

Association is an object relationship in object-oriented design. At runtime, objects collaborate by holding references or by passing references to methods.

15. Best Practices
Use association when one object uses another but does not own it.
Avoid forcing inheritance where there is no true "is-a" relationship.
Keep associations focused on business interactions.
16. Interview Summary
Association

↓

Objects Collaborate

↓

No Ownership

↓

Independent Lifecycles

↓

Uses Relationship

-----------------------------------------------------------------------------------------------
Important Interview Insight

Many tutorials define association as:

"Association is a relationship between two classes."

A stronger answer is:

Association models collaboration between independent objects. Neither object owns the other, and each can exist without the other.
They simply work together to accomplish a business task.

This explanation demonstrates both the concept and its practical purpose, which is what product-company interviewers typically look for.
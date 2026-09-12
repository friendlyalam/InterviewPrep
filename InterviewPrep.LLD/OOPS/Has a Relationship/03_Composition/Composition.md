Composition is a strong "HAS-A" relationship where one object owns one or more child objects.
The parent is responsible for creating and managing the lifecycle of those child objects.
If the parent object is destroyed, the child objects are also destroyed conceptually as part of the parent's object graph.

Interview Definition

Composition represents strong ownership between objects. The child object cannot meaningfully exist as part of that relationship without its parent.

--------------------------------------------------------------------------------------------------------------------------------------------------------
2. Simple Definition

Composition means:

One object is made up of another object and owns it.

Unlike Aggregation,

the parent is responsible for creating the child.

------------------------------------------------------------------------------------------------------------------------------
3. Why Composition?

Imagine you are building an E-Commerce Order System.

Every order must have:

Shipping Address
Billing Address

Can an Address exist independently in your business model?

Usually No.

The address belongs to a specific order.

If the order is deleted, that address (as part of the order) is also removed.

This is Composition.

---------------------------------------------------------------------------------------------------------------------------
4. Real-Life Examples

| Parent   | Child            | Composition? | Why                                 |
| -------- | ---------------- | ------------ | ----------------------------------- |
| House    | Room             | ✅            | Room belongs to the house           |
| Car      | Engine           | ✅            | Engine is part of the car           |
| Order    | Shipping Address | ✅            | Address belongs to that order       |
| Computer | Motherboard      | ✅            | Motherboard is part of the computer |
| Book     | Chapters         | ✅            | Chapters belong to the book         |

Notice:

The child is a part of the parent.

-------------------------------------------------------------------------------------------------------------------------------

Characteristics
Strong ownership
Strong HAS-A relationship
Whole-part relationship
Parent creates child
Shared lifecycle
Child usually isn't shared with other parents
Parent controls child lifetime

------------------------------------------------------------------------------------------------

| Feature                    | Association      | Aggregation        | Composition             |
| -------------------------- | ---------------- | ------------------ | ----------------------- |
| Relationship               | Uses             | Weak HAS-A         | Strong HAS-A            |
| Ownership                  | None             | Weak               | Strong                  |
| Parent Creates Child       | No               | Usually No         | Usually Yes             |
| Child Lifetime Independent | ✅                | ✅                  | ❌                       |
| Child Shared               | Possible         | Yes                | Usually No              |
| Example                    | Customer → Order | Company → Employee | Order → ShippingAddress |


-------------------------------------------------------------------------------------------------------------

Real Product Company Examples

| Parent   | Child                                  |
| -------- | -------------------------------------- |
| Order    | Shipping Address                       |
| Invoice  | Invoice Line                           |
| Computer | Motherboard                            |
| Car      | Engine                                 |
| Book     | Chapters                               |
| House    | Rooms                                  |
| Email    | Attachment Metadata (domain-dependent) |
| Report   | Report Sections                        |


----------------------------------------------------------------------------------------------------------------

Common Interview Questions
Q1. What is Composition?

A strong HAS-A relationship where the parent owns the child and controls its lifecycle.

Q2. Who creates the child object?

Normally the parent class.

Example:

_shippingAddress =
    new ShippingAddress(...);
Q3. Can the child exist independently?

In the context of the designed relationship, typically no.

The child is intended to be part of the parent.

Q4. Why is Composition called strong ownership?

Because the parent is responsible for creating and managing the child object.

Q5. Is Composition implemented using inheritance?

No.

It is implemented using object references where the parent owns the child object.

Q6. When should I use Composition?

Use Composition when:

the child logically belongs to the parent,
the parent manages the child's lifecycle,
and the child should not be shared between unrelated parent objects.
Best Practices

----------------------------------------------------------------------------------------

✅ Use Composition when:

Objects are tightly related.
The child has no meaningful role outside the parent.
The parent is responsible for creating and managing the child.

Avoid Composition if:

The child is shared across multiple parents.
The child has its own independent lifecycle.

--------------------------------------------------------------------------------------------------
Product Company Insight

A common interview answer is:

"Composition means HAS-A."

That answer is incomplete.

A stronger answer is:

Composition is a strong HAS-A relationship where the parent owns the child object, creates it, manages its lifecycle, and the child is considered part of the parent's object graph.

That explanation shows you understand ownership, lifecycle, and object design, which are the key ideas interviewers look for.

-----------------------------------------------------------------------------------------------------

One Important Clarification

You'll often hear:

"Destroy the parent, and the child is destroyed."

This is a design concept, not an immediate CLR behaviour.
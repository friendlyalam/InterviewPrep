1. Product Company Definition

Objects of a derived class should be able to replace objects of their base class without affecting the correctness of the program.

This principle was introduced by Barbara Liskov.

2. Simple Definition

If Class B inherits from Class A, then anywhere Class A is expected, Class B should work correctly without breaking the application.

Or even simpler:

A child class should behave like its parent promises.

---------------------------------------------------

3. Why Was LSP Introduced?

Many developers misuse inheritance.

They think:

If two classes look similar,
inheritance is always correct.

But inheritance is only correct if the child can truly replace the parent.

---------------------------------------------------------------------------------

4. Real-Life Example
Good Example

Vehicle

↓

Car

↓

Electric Car

Every electric car is still a car.

If someone asks for a car,

an electric car works perfectly.

LSP satisfied.

---------------------------------------------
Bad Example

Bird

↓

Penguin

Suppose the parent class contains:

Fly()

Penguin cannot fly.

Developers often do this:

public override void Fly()
{
    throw new NotSupportedException();
}

The program compiles.

But the behavior is broken.

This violates LSP.

-----------------------------------------------------
5. What Does "Substitution" Mean?

Imagine a method:

ProcessDocument(IDocumentProcessor processor)

The caller doesn't care whether the processor is:

PDF
Word
Excel

Every implementation should process the document correctly.

The caller should never need to ask:

if(processor is PdfProcessor)

or

if(processor is WordProcessor)

If it does, the design is usually wrong.

--------------------------------------------------------------------------------------

8. Characteristics
Proper inheritance
Proper abstraction
No unexpected behavior
No overridden methods throwing exceptions
Child strengthens functionality, not weakens it
Consumers treat all implementations uniformly

------------------------------------------------------------------

9. Advantages

| Benefit              | Explanation                                          |
| -------------------- | ---------------------------------------------------- |
| Safe inheritance     | Child classes behave correctly.                      |
| Better polymorphism  | Consumers don't care about concrete implementations. |
| Easier maintenance   | Fewer special cases.                                 |
| Better extensibility | New implementations integrate cleanly.               |
| Cleaner code         | Eliminates unnecessary type checks.                  |


---------------------------------------------------------------------------------------

10. Common LSP Violations

Throwing exceptions
public override void Process()
{
    throw new NotSupportedException();
}


Returning invalid values
public override decimal CalculateDiscount()
{
    return -1;
}


Doing nothing
public override void Save()
{
}


Changing expected behavior

Parent promises:

Returns processed document

Child returns:

null

--------------------------------------------------------
11. Relationship with Previous Principles

| Principle | Relationship                                          |
| --------- | ----------------------------------------------------- |
| SRP       | Keeps classes focused.                                |
| OCP       | Allows adding new implementations.                    |
| LSP       | Ensures those implementations are valid replacements. |


----------------------------------------------------------------------

Think of it like this:

OCP asks: Can I add a new implementation?
LSP asks: Will that new implementation behave correctly everywhere the base type is expected?

-------------------------------------------------------------------------------------------
12. Interview Questions
Q1. What is LSP?

Derived objects should be replaceable for base objects without changing the correctness of the application.

Q2. What is substitution?

Replacing a base-class reference with a derived-class object while preserving expected behavior.

Q3. What is the most common LSP violation?

Throwing NotSupportedException because the child cannot support the parent's contract.

Q4. Is every inheritance relationship valid?

No.

Only when the child fully satisfies the parent's contract.

Q5. Which OOP concept is most related?

Polymorphism.

LSP ensures polymorphism is safe and reliable.

Product Company Insight

Many developers answer:

"LSP means child class should inherit parent."

That is not the principle.

A stronger interview answer is:

LSP ensures that any implementation of an abstraction can replace another implementation without breaking business logic,
changing expected behavior, or forcing consumers to add special-case checks. It validates whether an inheritance or interface 
implementation truly represents the same contract.
2. Method Overriding (Runtime Polymorphism)
Product Company Definition

Method overriding allows a derived class to provide its own implementation of a virtual or abstract method defined in the base class.
The runtime chooses which implementation to execute.
Its also called runtime polymorphism or dynamic polymorphism because the method to be executed is determined at runtime.

--------------------------------------------------------------------------
Memory Behaviour
Runtime

↓

Checks Actual Object

↓

Calls Correct Method

-------------------------------------------------------------------------
Use Method Overriding

When every derived class performs the same operation differently.

Examples:

Payment processing
Notifications
Tax calculation
Shipping cost calculation
File preview

--------------------------------------------------------------------------
 1.Does overriding require virtual?

Yes. The base method must be virtual, abstract, or already override.

2.Can private methods be overridden?

No.

Private methods are not inherited by derived classes.

3.Which demonstrates runtime polymorphism?

Only method overriding (and interface implementations resolved at runtime).
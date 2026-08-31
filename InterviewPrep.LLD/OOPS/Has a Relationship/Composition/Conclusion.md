Why Is This Composition?

Look carefully.

We never wrote:

ShippingAddress address =
    new ShippingAddress(...);

outside the Order.

Instead,

the Order creates the ShippingAddress.

That is the defining characteristic of Composition.

Memory Representation

When this executes:

Order order =
    new Order(...);

Memory:

Stack
────────────────────────────

order
   │
   ▼

Heap
────────────────────────────

Order Object

OrderId = 1001

↓

ShippingAddress Object

Street

City

Country

Notice:

The ShippingAddress object is part of the Order.

Object Lifetime

Suppose:

order = null;

The Order object becomes eligible for garbage collection (assuming no other references).

Since the ShippingAddress is only reachable through the Order, it also becomes eligible for garbage collection.

The parent owns the child.
Why Is This Dependency?

Look carefully:

orderService.PlaceOrder(
    "Mohd Alam",
    "alam@example.com",
    emailService);

OrderService:

does not own EmailService
does not permanently store it
only needs it while placing the order

This is a dependency relationship.

Memory Representation
Stack
──────────────────────────

emailService

orderService

        │

        ▼

Heap
──────────────────────────

EmailService Object


OrderService Object

During the method call:

OrderService

↓

temporarily uses

↓

EmailService

After the method returns, the dependency is over.
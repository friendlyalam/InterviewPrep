Object Relationship
Program
   │
   ▼
OrderService
   │
   ├────────► IOrderRepository
   │              │
   │              ▼
   │       OrderRepository
   │
   ├────────► IInventoryService
   │              │
   │              ▼
   │      InventoryService
   │
   ├────────► IInvoiceService
   │              │
   │              ▼
   │      InvoiceService
   │
   ├────────► IEmailService
   │              │
   │              ▼
   │       EmailService
   │
   └────────► IAuditService
                  │
                  ▼
            AuditService

            ---------------------------------------------------------

            How SRP Is Applied

            | Class              | Responsibility                | Reason to Change                   |
| ------------------ | ----------------------------- | ---------------------------------- |
| `Order`            | Order data                    | Order fields change                |
| `OrderRepository`  | Save orders                   | Database logic changes             |
| `InventoryService` | Inventory updates             | Stock management rules change      |
| `InvoiceService`   | Invoice generation            | Invoice format or tax rules change |
| `EmailService`     | Email notifications           | Email provider/template changes    |
| `AuditService`     | Audit logging                 | Audit requirements change          |
| `OrderService`     | Coordinate the order workflow | Order business process changes     |



---------------------------------------------------------------

Interview Questions
Why does OrderService have many dependencies? Doesn't that violate SRP?

Answer:
No. SRP is about responsibilities, not the number of dependencies. OrderService has a single responsibility: orchestrating the order processing workflow. It delegates specialized work (inventory, email, invoice, audit, persistence) to dedicated services instead of implementing those responsibilities itself.

Why use interfaces instead of concrete classes?

To achieve:

Loose Coupling
Dependency Injection
Easier Unit Testing
Extensibility
Compliance with the Dependency Inversion Principle (covered later)
Why Constructor Injection?

Because:

Required dependencies are guaranteed to be available.
The class is immutable with respect to its collaborators (readonly fields).
Dependencies are explicit and easy to mock during testing.
It is the recommended approach in ASP.NET Core.
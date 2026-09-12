1. Definition

DRY = Don't Repeat Yourself.

It means:

Every piece of knowledge or business logic should have a single, authoritative representation in the codebase.

The goal is not simply to reduce duplicate lines of code; it is mainly to avoid duplicating the same logic or knowledge in multiple places.

2. Usage

Use DRY when:

The same business logic appears in multiple places.
The same validation rules are repeated.
The same calculation/transformation is repeated.
The same configuration/value is duplicated.
Multiple classes perform the same responsibility.
Utility and helper classes centralize commonly used functions like string handling, date formatting, or calculations.
Validation logic and business rules are reused across multiple modules to ensure consistent behavior.
Logging, exception handling, and configuration management are shared to avoid duplicated setup and inconsistent handling.

Example:

decimal CalculateDiscount(decimal price)
{
    return price * 0.10m;
}

Instead of writing the same calculation in 5 different services, keep it in one reusable place.

3. Key Features
Single source of truth
Reduces code duplication
Easier maintenance
Reduces inconsistent behavior
Changes need to be made in fewer places
Improves readability when abstraction is meaningful
Helps maintain business rules consistently


4. Real-World Example
Imagine a company calculates shipping charges.

Without DRY:

Order Service       → Shipping = ₹100
Cart Service        → Shipping = ₹100
Checkout Service    → Shipping = ₹100
Invoice Service     → Shipping = ₹120   ❌

Someone changes the shipping charge from ₹100 to ₹120 but forgets one service.

Now the system gives inconsistent results.

With DRY:

             Shipping Rule
                  ↓
       ┌──────────┼──────────┐
       ↓          ↓          ↓
     Order       Cart     Checkout

One centralized rule becomes the single source of truth.

5. Program Example — Violation
❌ Without DRY
public class OrderService
{
    public decimal CalculateDiscount(decimal price)
    {
        return price * 0.10m;
    }
}

public class CartService
{
    public decimal CalculateDiscount(decimal price)
    {
        return price * 0.10m;
    }
}

The same business logic is duplicated.

If the discount changes from 10% to 15%, multiple places must be changed.

6. Program Example — Following DRY
public class DiscountService
{
    public decimal CalculateDiscount(decimal price)
    {
        return price * 0.10m;
    }
}

Other services can depend on it:

public class OrderService
{
    private readonly DiscountService _discountService;

    public OrderService(DiscountService discountService)
    {
        _discountService = discountService;
    }

    public decimal GetDiscount(decimal price)
    {
        return _discountService.CalculateDiscount(price);
    }
}

Now the discount rule has one authoritative implementation.

7. Common DRY Violations
1. Duplicate business logic
price * 0.10m

repeated throughout the application.

2. Duplicate validation
if (email == null || email == "")

repeated in multiple services.

3. Duplicate constants
const int MaxRetry = 3;

defined independently in multiple classes.

4. Copy-paste code

Two methods contain almost identical logic with minor changes.

5. Duplicate mapping/transformation logic

Converting the same entity to DTO differently in multiple places.

8. DRY Does NOT Mean "Remove Every Duplicate Line"

This is an important interview point.

Sometimes two pieces of code look similar but represent different business concepts.

For example:

CalculateEmployeeSalary()
CalculateProductPrice()

Both may contain:

amount * tax

That doesn't automatically mean they should share one method.

Their business rules may evolve independently.

DRY applies primarily to duplicated knowledge/logic, not merely similar-looking syntax.

9. Limitations / Risks

Over-applying DRY can create problems.

❌ Excessive abstraction

You may create:

GenericService
BaseService
CommonService
UtilityService
HelperService

just to eliminate a few duplicate lines.

This can make the code harder to understand.

❌ Wrong abstraction

Two pieces of code may currently look identical but may have different reasons to change.

Combining them can create tight coupling.

❌ Difficult debugging

Highly generic reusable code can sometimes make debugging and understanding execution flow harder.

10. DRY vs Readability

Good DRY:

CalculateTax(order);

instead of repeating complex tax logic everywhere.

Bad DRY:

Process<T>(T item, Func<T, bool> condition, ...)

when a simple, readable method would be clearer.

Principle:

Don't sacrifice readability just to eliminate a small amount of duplication.

11. DRY in LLD

For LLD interviews, DRY commonly works together with:

DRY
 ↓
Single Source of Truth
 ↓
Reusable Components
 ↓
Less Duplication
 ↓
Easier Maintenance

It often complements:

SOLID
Single Responsibility Principle
Composition
Dependency Injection
Design Patterns

But DRY and SRP are not the same principle.

12. Interview Points to Remember

Q: What is DRY?

DRY means Don't Repeat Yourself. It aims to ensure that each piece of knowledge or business logic has a single authoritative representation.

Q: Is DRY only about duplicate code?

No. It is mainly about avoiding duplication of knowledge, business rules, and logic. Two pieces of code can look similar without necessarily violating DRY.

Q: Can DRY be overused?

Yes. Excessive abstraction can make code more complex and tightly coupled. Duplication is sometimes preferable when two concepts have different reasons to change.

One-line takeaway

DRY means "one business rule, one authoritative place," not "zero duplicate lines at any cost."
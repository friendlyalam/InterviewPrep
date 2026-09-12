1. Definition

YAGNI = You Aren't Gonna Need It.

It means:

Don't build a feature, abstraction, or functionality until there is an actual requirement for it.

The goal is to avoid speculative development and unnecessary complexity.

2. Usage

Use YAGNI when:

A feature is not currently required.
You are adding functionality "just in case".
You are creating abstractions for future possibilities without a real requirement.
You are adding configuration that isn't currently needed.
You are designing for hypothetical use cases.
You are implementing features that nobody has requested.
3. Key Features
Focuses on current requirements
Avoids speculative development
Reduces unnecessary code
Reduces maintenance
Reduces testing effort
Reduces bugs
Speeds up development
Keeps the design simpler
4. Real-World Example

Suppose your product currently supports:

India

A developer says:

"Maybe we'll support 20 countries next year, so let's build a complete multi-country currency and tax framework now."

That's unnecessary if there is no current requirement.

YAGNI approach

Build:

India
 ↓
INR
 ↓
Current tax requirements

When the business actually requires:

USA → USD
UK  → GBP
UAE → AED

then extend the design based on the actual requirements.

5. Program Example — YAGNI Violation

Suppose the current requirement is:

Calculate salary for Indian employees.

Developer creates:

public interface ISalaryCalculator
{
    decimal Calculate();
}

public class IndiaSalaryCalculator : ISalaryCalculator
{
    public decimal Calculate()
    {
        // Current requirement
        return 50000;
    }
}

public class USSalaryCalculator : ISalaryCalculator
{
    public decimal Calculate()
    {
        // Maybe needed in future
        return 7000;
    }
}

public class UKSalaryCalculator : ISalaryCalculator
{
    public decimal Calculate()
    {
        // Maybe needed in future
        return 5000;
    }
}

If the product currently has no US or UK requirement, those implementations are unnecessary.

6. YAGNI Approach

Implement only what is required:

public class SalaryCalculator
{
    public decimal Calculate(decimal basicSalary)
    {
        return basicSalary;
    }
}

When a real requirement appears for different countries or salary rules, extend the design then.

7. Another Example — API

Current requirement:

POST /orders
GET  /orders/{id}

Don't automatically build:

POST   /orders
GET    /orders/{id}
PUT    /orders/{id}
PATCH  /orders/{id}
DELETE /orders/{id}
POST   /orders/{id}/archive
POST   /orders/{id}/duplicate
POST   /orders/{id}/export

just because these might be needed later.

Build the APIs required by the current product requirements.

8. YAGNI Violations
1. Future-proofing without requirements

"We may need this someday."

2. Unused interfaces

Creating interfaces only because "good architecture requires interfaces."

3. Unused configuration

Adding dozens of configuration options that currently have no purpose.

4. Unused features

Implementing functionality before the business asks for it.

5. Premature optimization

Optimizing code for millions of users when the current requirement and actual load don't justify it.

6. Over-generalization

Creating a highly generic framework for a problem that currently has only one use case.

9. Limitations / Risks

YAGNI does not mean:

"Never think about the future."

You should still consider known and reasonably expected requirements.

For example, if the approved product requirement explicitly says:

"We will launch in India and UAE next quarter."

Then designing the system with appropriate extensibility may be justified.

The key difference is:

YAGNI:
"Maybe we'll need it someday."

Valid requirement:
"We know we'll need it."
10. YAGNI vs KISS

They are closely related but focus on different things.
| Principle | Focus                                              |
| --------- | -------------------------------------------------- |
| **KISS**  | Keep the solution as simple as possible            |
| **YAGNI** | Don't build functionality you don't currently need |

Example:

KISS
↓
Don't create a complex solution for a simple requirement.

YAGNI
↓
Don't create functionality for requirements that don't exist yet.
11. YAGNI vs DRY
| Principle | Focus                            |
| --------- | -------------------------------- |
| **DRY**   | Avoid duplicated knowledge/logic |
| **KISS**  | Avoid unnecessary complexity     |
| **YAGNI** | Avoid unnecessary functionality  |

hey work together:

DRY
 ↓
Don't duplicate

KISS
 ↓
Don't over-complicate

YAGNI
 ↓
Don't build unnecessary things
12. YAGNI in LLD Interviews

When designing an LLD, don't immediately create:

10 interfaces
15 design patterns
20 classes
Multiple factories
Multiple strategies
Future country support
Future payment methods
Future databases

Instead:

Requirement
     ↓
Identify current use cases
     ↓
Identify required responsibilities
     ↓
Design required classes/interfaces
     ↓
Apply patterns only where justified

Then explain:

"I'm keeping the current design simple. If a new requirement such as multiple payment providers is introduced, I can introduce Strategy/Factory at that point."

That demonstrates good engineering judgment.

13. Interview Points to Remember

Q: What is YAGNI?

YAGNI means "You Aren't Gonna Need It." It says we should not implement functionality or abstractions until there is a real requirement for them.

Q: Does YAGNI mean we should never design for extensibility?

No. We should support known and justified future requirements, but avoid speculative features that have no concrete need.

Q: How is YAGNI different from KISS?

KISS focuses on keeping the solution simple, while YAGNI focuses on not building functionality that isn't currently required.

Q: Can YAGNI conflict with DRY?

Yes. Sometimes creating a reusable abstraction just to eliminate a small amount of duplication can violate YAGNI if there is no actual need for that abstraction.

One-line takeaway

YAGNI means: Don't build today what you only imagine you might need tomorrow.
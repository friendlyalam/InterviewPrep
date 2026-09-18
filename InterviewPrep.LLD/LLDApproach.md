# LLD Learning Approach — C#

For every LLD problem, we will follow a consistent process.

## 1. Understand the Requirements

- Functional requirements
- Non-functional/design considerations
- Identify what is in scope
- Identify what is out of scope
- Clarify assumptions where requirements are ambiguous

## 2. Identify the Core Objects

Identify the main building blocks of the system:

- Entities
- Services
- Interfaces
- Enums
- Value Objects
- Repositories
- External dependencies, where applicable

For every class, clearly understand:

- What responsibility does it have?
- Why does it exist?
- What should it NOT be responsible for?

## 3. Identify Relationships

Understand how the objects interact:

- Association
- Composition
- Aggregation
- Inheritance
- Dependency

Prefer composition over inheritance when appropriate.

## 4. Apply OOP Principles

Use the four core OOP principles:

- Encapsulation
- Abstraction
- Inheritance
- Polymorphism

Explain where and why each principle is being used.

## 5. Apply SOLID Principles

Focus especially on:

- SRP — Single Responsibility Principle
- OCP — Open/Closed Principle
- LSP — Liskov Substitution Principle
- ISP — Interface Segregation Principle
- DIP — Dependency Inversion Principle

Do not apply SOLID mechanically.

For every important principle, explain:

- What problem does it solve?
- Why is it useful here?
- What would happen if we ignored it?

## 6. Identify Suitable Design Patterns

Use design patterns only when they solve an actual design problem.

Common patterns we will consider:

- Strategy
- Factory
- Abstract Factory
- Builder
- Observer
- Command
- State
- Decorator
- Adapter
- Facade
- Composite
- Mediator
- Singleton — only where genuinely justified
- Other patterns when required

For every pattern, explain:

- Why we need it
- What problem it solves
- Why this pattern fits
- Why a simpler approach may or may not be sufficient

## 7. Design the Class / Interface Structure

First design the system conceptually.

Then convert the design into C#.

We will identify:

- Classes
- Interfaces
- Methods
- Properties
- Relationships
- Dependencies
- Responsibilities

Before writing code, understand the design.

## 8. Write Production-Style C#

The implementation should follow clean, enterprise-style C#.

Guidelines:

- Interfaces + Dependency Injection
- Constructor injection for business dependencies
- Avoid unnecessary `new` inside business logic
- Use `new` mainly for DTOs/models/entities and composition-root setup
- Clean naming
- Appropriate access modifiers
- Encapsulation
- Small and focused classes
- Meaningful interfaces
- Low coupling
- High cohesion
- Extensibility without over-engineering
- Idiomatic C#
- Code should be complete and copy-paste-ready

The final implementation should be provided as complete runnable code rather than disconnected code fragments.

## 9. Walk Through the Design

Explain the runtime flow step by step.

For example:

Request
   ↓
Controller / Entry Point
   ↓
Service
   ↓
Domain Object / Strategy / Factory
   ↓
Repository / External Dependency
   ↓
Response

Explain:

- Which object is created
- Which dependency is injected
- Which method is called
- How objects communicate
- How the final result is produced

## 10. Review and Improve

After implementing the first version, review the design.

Ask:

- What is good about the design?
- What can be improved?
- Does it follow SOLID?
- Is there unnecessary coupling?
- Are responsibilities properly separated?
- Is inheritance really required?
- Can composition be used instead?
- Are interfaces meaningful?
- Is there unnecessary abstraction?
- Is there over-engineering?
- What happens when a new requirement arrives?
- How easily can the system be extended?
- What would we change in an LLD interview?

Then refactor where necessary.

## 11. Interview Discussion

Finally, discuss interview-level follow-ups.

### Requirement Changes

Examples:

- Add a new payment method
- Add a new notification channel
- Add a new pricing strategy
- Add a new user type
- Add a new business rule

### Edge Cases

Consider:

- Null input
- Empty input
- Invalid input
- Duplicate operations
- Object state
- Failure scenarios
- Boundary conditions

### Concurrency / Thread Safety

Where relevant, discuss:

- Multiple users accessing the same resource
- Race conditions
- Locks
- Thread safety
- Concurrent collections
- Immutable objects
- Synchronization

### Persistence / API Boundaries

Where relevant, discuss:

- Repository abstraction
- Database interaction
- External APIs
- Third-party services
- DTOs
- Domain models
- Mapping
- Failure handling

### Extensibility

Ask:

> "If a new requirement is added tomorrow, how much existing code needs to change?"

The goal is to design systems where new behavior can often be added with minimal modification to existing code.

---

# LLD Problem-Solving Mindset

We will NOT memorize patterns and force them into every problem.

Instead, follow this sequence:

Requirement
   ↓
Identify responsibilities
   ↓
Identify objects
   ↓
Identify relationships
   ↓
Identify changing behavior
   ↓
Create appropriate abstractions
   ↓
Apply OOP
   ↓
Apply SOLID
   ↓
Choose patterns where they solve a real problem
   ↓
Design classes/interfaces
   ↓
Implement in C#
   ↓
Review
   ↓
Handle changing requirements
   ↓
Interview discussion

## Primary Goal

The goal is not simply to solve individual LLD problems.

The goal is to develop the ability to look at an unfamiliar real-world requirement and independently:

1. Break it into responsibilities
2. Identify appropriate objects
3. Design relationships
4. Identify changing behavior
5. Choose suitable abstractions
6. Apply SOLID appropriately
7. Select design patterns when justified
8. Write clean, maintainable C#
9. Explain the design clearly
10. Modify the design when requirements change

This is the skill required to become proficient in the LLD/System Design part of product-company interviews.
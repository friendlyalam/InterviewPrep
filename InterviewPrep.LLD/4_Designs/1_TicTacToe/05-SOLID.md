# `05-SOLID.md`

```markdown
# Tic-Tac-Toe — Step 5: SOLID Principles

## Objective

Evaluate the design against SOLID principles.

The goal is not to force every principle into the solution.

---

# 1. Single Responsibility Principle — SRP

A class should have one primary reason to change.

## Player

Responsible for:

- Player identity
- Player symbol

## Cell

Responsible for:

- One board position
- Its occupied state

## Board

Responsible for:

- Board state
- Cell operations
- Board-related checks

## Game

Responsible for:

- Game flow
- Turn management
- Game lifecycle

This keeps responsibilities separated.

---

# 2. Open/Closed Principle — OCP

The design should be open for extension and closed for unnecessary modification.

Consider a future requirement:

```text
Support different winning rules.

Instead of putting every possible rule into:

Game

we could introduce:

IWinCondition

and create different implementations.

However, for the current fixed 3 × 3 game, this abstraction is not necessary.

3. Liskov Substitution Principle — LSP

There is no meaningful inheritance hierarchy in our initial solution.

Therefore LSP does not require special implementation.

This is preferable to creating artificial inheritance.

4. Interface Segregation Principle — ISP

We should not create a large interface such as:

ITicTacToeSystem
{
    StartGame();
    MakeMove();
    ResetGame();
    SaveGame();
    LoadGame();
    CalculateScore();
    SendNotification();
    GenerateReport();
}

This would mix unrelated responsibilities.

Instead, if interfaces are required later, they should be small and focused.

5. Dependency Inversion Principle — DIP

High-level business logic should not become tightly coupled to replaceable infrastructure.

For the current in-memory game, there are no major infrastructure dependencies.

Therefore we should NOT create interfaces just for the sake of DIP.

If persistence is added later:

Game
 ↓
IGameRepository
 ↓
DatabaseGameRepository

Then DIP becomes valuable.

SOLID Summary
SRP → Clear responsibilities
OCP → Allow future extensibility
LSP → No artificial inheritance
ISP → Keep interfaces focused
DIP → Abstract replaceable dependencies
Important Interview Point

If an interviewer asks:

"Where are your interfaces?"

A good answer is not:

"Every class must have an interface."

Instead:

"I introduce interfaces where behavior or dependencies need to be replaced. The current in-memory game does not have enough variability to justify interfaces everywhere."

This demonstrates design judgment rather than pattern memorization.
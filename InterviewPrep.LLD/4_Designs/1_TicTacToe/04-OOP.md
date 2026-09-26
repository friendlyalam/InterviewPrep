# `04-OOP.md`

```markdown
# Tic-Tac-Toe — Step 4: OOP Principles

## Objective

Apply the four fundamental OOP principles appropriately.

---

# 1. Encapsulation

Encapsulation means an object controls its own state.

## Example: Cell

A Cell should not allow arbitrary code to directly change its symbol.

Instead of exposing:

```csharp
cell.Symbol = PlayerSymbol.X;

we can provide:

cell.Mark(PlayerSymbol.X);

The Cell can then protect itself from being marked twice.

Example:

public void Mark(PlayerSymbol symbol)
{
    if (!IsEmpty)
        throw new InvalidOperationException();

    Symbol = symbol;
}

Therefore the Cell controls its own state.

2. Abstraction

The caller should not need to know how the board internally stores cells.

The caller can simply use:

board.Place(position, symbol);

The Board internally handles:

Finding the cell
Validating the position
Marking the cell

The internal implementation remains hidden.

3. Polymorphism

The current Tic-Tac-Toe requirements do not require significant runtime polymorphism.

This is important.

We should NOT introduce multiple classes simply to demonstrate polymorphism.

For example, this is unnecessary:

Player
 ├── XPlayer
 └── OPlayer

There is no meaningful behavioral difference.

4. Inheritance

Inheritance is not required for the current problem.

There is no strong:

IS-A

relationship that needs inheritance.

For example:

XPlayer IS-A Player

does not provide useful behavior.

Instead:

Player HAS-A Symbol

is sufficient.

OOP Summary

| Principle     | Application                                           |
| ------------- | ----------------------------------------------------- |
| Encapsulation | Cell and Board protect their state                    |
| Abstraction   | Board exposes operations rather than internal storage |
| Polymorphism  | Used only if future requirements need it              |
| Inheritance   | Not required in the initial design                    |

Key Learning

Good OOP does NOT mean using every OOP feature.

A good design uses the appropriate concept only when it solves a real problem.
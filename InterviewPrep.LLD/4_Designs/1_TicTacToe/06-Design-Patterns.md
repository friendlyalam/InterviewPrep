# `06-Design-Patterns.md`

```markdown
# Tic-Tac-Toe — Step 6: Design Patterns

## Objective

Determine whether a design pattern actually solves a problem.

---

# Important Principle

We should NOT start with:

> "Which design pattern can I use?"

We start with:

> "What design problem do I have?"

Then select a pattern if appropriate.

---

# 1. Strategy Pattern

### Current requirement

The winning rule is fixed:

```text
3 × 3
3 symbols in a line

Therefore Strategy is NOT required.

Future requirement

Suppose we support:

3 × 3
5 × 5
N × N

or:

3-in-a-row
4-in-a-row
5-in-a-row

Then a Strategy could be useful.

Example:

IWinCondition
       │
       ├── StandardWinCondition
       └── NInARowWinCondition
Decision
Current version → No Strategy
Future variable rules → Strategy may be justified
2. Factory Pattern

We don't have complicated object creation.

Creating:

new Player("Player 1", PlayerSymbol.X);

is simple.

A Factory would add unnecessary abstraction.

Decision
Factory → Not required
3. Builder Pattern

Objects such as Player and Position are simple.

There are not enough optional construction parameters to justify Builder.

Decision
Builder → Not required
4. State Pattern

The game has states:

InProgress
Won
Draw

But the behavior associated with each state is small.

An enum is enough.

Decision
Current version → GameStatus enum
Complex future state behavior → State Pattern may be considered
5. Observer Pattern

Observer could become useful if multiple components need to react to game events.

For example:

Game
  │
  ├── UI
  ├── Scoreboard
  ├── Game History
  └── Analytics

Then:

GameEvent
     ↓
Observers

could be useful.

Decision
Current version → Not required
Future event-driven requirements → Observer may be useful
6. Command Pattern

Command could be useful if we later add:

Undo
Redo
Move history
Replay

Example:

MoveCommand
     ↓
Execute()
Undo()
Decision
Current version → Not required
Undo/Redo → Command becomes useful

Pattern Summary

| Pattern   | Current Version | Future Use                      |
| --------- | --------------- | ------------------------------- |
| Strategy  | No              | Variable winning rules          |
| Factory   | No              | Complex object creation         |
| Builder   | No              | Complex object creation         |
| State     | No              | Complex state-specific behavior |
| Observer  | No              | Multiple event listeners        |
| Command   | No              | Undo/redo/replay                |
| Singleton | No              | No genuine need                 |

Final Decision

The initial Tic-Tac-Toe implementation intentionally uses very few patterns.

This is a good design decision.

A simple problem should have a simple design.

Patterns can be introduced when requirements create the problem they solve.


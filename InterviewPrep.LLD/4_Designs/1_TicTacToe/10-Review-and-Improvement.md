# `10-Review-and-Improvement.md`

```markdown
# Tic-Tac-Toe — Step 10: Review and Improvement

## 1. Responsibility Review

### Player

Good:

- Contains player information.
- Does not manage game behavior.

### Cell

Good:

- Owns its occupied state.
- Prevents double marking.

### Board

Good:

- Owns cells.
- Handles board-related operations.

### Game

Good:

- Coordinates the complete game.
- Manages turns.
- Determines when the game ends.

---

# 2. Coupling Review

The main dependency is:

```text
Game → Board → Cell

This is reasonable for the current problem.

The Game does not directly manipulate individual Cell objects.

3. SOLID Review

The design has reasonable separation of responsibilities.

We have deliberately avoided unnecessary abstractions.

The following would currently be over-engineering:

IGame
IBoard
ICell
IPlayer
IGameManager
IBoardManager

There is no requirement that justifies all of these interfaces.

4. What Could Be Improved?
Improvement 1 — Configurable Board

Current:

3 × 3

Future:

N × N

The Board can be modified to accept a size.

Improvement 2 — Win Condition Strategy

Current:

3 symbols in a row

Future:

N symbols in a row

At that point:

public interface IWinCondition
{
    bool IsWinner(Board board, PlayerSymbol symbol);
}

could become useful.

Improvement 3 — Move Strategy

If a computer player is introduced:

IMoveStrategy

could be used.

Example:

HumanMoveStrategy
RandomMoveStrategy
MinimaxMoveStrategy
Improvement 4 — Persistence

If games need to be saved:

IGameRepository

could abstract persistence.

Improvement 5 — Events

If UI, scoreboard, and analytics need to react to moves:

Observer / Events

could be introduced.

5. Avoid Over-Engineering

We should NOT immediately implement all future abstractions.

A common LLD mistake is:

Requirement
   ↓
20 interfaces
   ↓
10 design patterns
   ↓
Huge architecture

Instead:

Requirement
   ↓
Simple design
   ↓
Identify actual variation
   ↓
Introduce abstraction
6. Interview-Level Improvement

If an interviewer says:

"Now support 5 × 5 board."

We should not rewrite the entire design.

We should identify which responsibility changes.

Likely:

Board
Win condition

need extension.

The Game flow should remain mostly unchanged.

Final Review

The initial design is intentionally simple.

The important design quality is not the number of classes.

It is:

Clear responsibility
+
Low coupling
+
Good encapsulation
+
Easy extension
+
No unnecessary abstraction
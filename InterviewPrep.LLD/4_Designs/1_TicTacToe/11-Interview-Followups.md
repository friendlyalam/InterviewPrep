# `11-Interview-Followups.md`

```markdown
# Tic-Tac-Toe — Step 11: Interview Follow-ups

## Objective

Practice changing the design when requirements change.

This is one of the most important LLD interview skills.

---

# Follow-up 1 — Support Different Board Sizes

### Requirement

Support:

```text
3 × 3
5 × 5
10 × 10
Design Change

Make Board size configurable.

Board
 └── Size

The Game should not need to know how the Board internally stores cells.

Follow-up 2 — Support N-in-a-Row
Requirement

A player wins when they get N symbols in a row.

Example:

5 × 5 board
4 symbols required to win
Design

Introduce:

IWinCondition

Example:

IWinCondition
      │
      ├── ThreeInARowWinCondition
      └── NInARowWinCondition

This is a suitable use case for Strategy.

Follow-up 3 — Computer Player
Requirement

Support:

Human vs Computer
Design

Introduce:

IMoveStrategy

Possible implementations:

HumanMoveStrategy
RandomMoveStrategy
MinimaxMoveStrategy

The Game should not need to know which strategy is being used.

Follow-up 4 — Undo Move
Requirement

Allow a player to undo the previous move.

Design

Consider Command Pattern:

MoveCommand
     │
     ├── Execute()
     └── Undo()

Maintain move history:

Stack<MoveCommand>
Follow-up 5 — Save Game
Requirement

Save and resume games.

Design

Introduce:

IGameRepository

Implementations could include:

InMemoryGameRepository
DatabaseGameRepository
FileGameRepository

The Game/Application layer should depend on the abstraction rather than a concrete database.

Follow-up 6 — Online Multiplayer
Requirement

Players can play from different machines.

Additional Concerns

Now we need:

Client
   ↓
API
   ↓
Game Service
   ↓
Game State

We must consider:

Concurrent moves
Authentication
Game/session ID
Network failures
Reconnects
State synchronization

This goes beyond basic LLD and starts touching system design.

Follow-up 7 — Multiple Games
Requirement

The system supports thousands of simultaneous games.

Design Concern

Do not use:

static Game CurrentGame;

Each game must have independent state.

Conceptually:

GameManager
    │
    ├── Game 1
    ├── Game 2
    ├── Game 3
    └── ...

For a distributed system, persistence and concurrency become important.

Follow-up 8 — Game Events
Requirement

Notify UI and scoreboard whenever a move happens.

Possible event:

MoveMade

Subscribers:

UI
Scoreboard
GameHistory
Analytics

Observer/event-driven design may be appropriate.

Follow-up 9 — Thread Safety
Requirement

Two requests can attempt to make a move simultaneously.

Potential problem:

Request A → Cell (0,0)
Request B → Cell (0,0)

Both may see the cell as empty before one updates it.

Possible solutions:

Locking
Serialized processing
Optimistic concurrency
Version checking

The correct approach depends on whether the game is local or distributed.

Follow-up 10 — Game History
Requirement

Store every move.

Example:

Move 1 → Player X → (0,0)
Move 2 → Player O → (1,1)
Move 3 → Player X → (0,1)

Create a dedicated history component.

Do not put history logic into Cell.

Common Interview Questions
Q1. Why is Game responsible for turns?

Because turn management is part of the overall game flow.

Q2. Why doesn't Player validate moves?

Because move validation depends on the Board state.

Q3. Why doesn't Board switch turns?

Because turn management is a game-level responsibility.

Q4. Why don't we use XPlayer and OPlayer subclasses?

Because X/O represents a symbol, not a different player type.

Q5. Why don't we use Factory?

Object creation is simple and does not require a separate abstraction.

Q6. Why don't we use Singleton?

There is no requirement that only one Game instance can exist.

Q7. When would you introduce Strategy?

When the winning behavior becomes variable.

Q8. When would you introduce Command?

When operations such as Undo/Redo/Replay are required.

Q9. When would you introduce Observer?

When multiple independent components need to react to game events.

Q10. How would you make the design extensible?

Keep responsibilities separated and introduce abstractions around behavior that is actually expected to vary.

Final Interview Mental Model

When the interviewer changes the requirement:

New Requirement
      ↓
Which responsibility changes?
      ↓
Which class owns that responsibility?
      ↓
Does the existing design handle it cleanly?
      ↓
If behavior varies:
      ↓
Introduce abstraction
      ↓
Choose appropriate pattern
      ↓
Keep existing behavior stable
Key LLD Lesson From Tic-Tac-Toe

Do not start with:

"Which design pattern should I use?"

Start with:

"What are the requirements?"
        ↓
"What are the responsibilities?"
        ↓
"What objects own those responsibilities?"
        ↓
"How are they related?"
        ↓
"What behavior can change?"
        ↓
"What abstraction is justified?"
        ↓
"Which pattern, if any, solves that problem?"

That is the LLD thought process we should carry into the next problems.


### Final Problem 1 structure

```text
TicTacToe/
│
├── 01-Requirements.md
├── 02-Core-Objects.md
├── 03-Relationships.md
├── 04-OOP.md
├── 05-SOLID.md
├── 06-Design-Patterns.md
├── 07-Class-Interface-Design.md
├── 08-CSharp-Implementation.md
├── 09-Runtime-Walkthrough.md
├── 10-Review-and-Improvement.md
└── 11-Interview-Followups.md

One important point: for Tic-Tac-Toe, I deliberately did not force Strategy/Factory/Singleton/etc. into the initial implementation. In LLD interviews, demonstrating why a pattern is not needed is also an important design skill.
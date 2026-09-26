# `03-Relationships.md`

```markdown
# Tic-Tac-Toe — Step 3: Relationships

## Objective

Determine how the identified objects are related.

---

# 1. Game → Player

A Game has two Players.

```text
Game
 ├── Player X
 └── Player O

The Game needs Players to:

Identify the current player
Know the player's symbol
Determine the winner

This is a has-a relationship.

2. Game → Board

A Game has a Board.

Game
 └── Board

The Game uses the Board to:

Place moves
Check whether a move is valid
Check whether the board is full
Check winning conditions
3. Board → Cell

A Board contains Cells.

Board
 ├── Cell
 ├── Cell
 ├── Cell
 ├── ...
 └── Cell

There are exactly 9 cells in the current requirement.

This is a composition relationship because the cells are part of the board.

4. Cell → Position

A Cell has a Position.

Cell
 └── Position

Position represents:

Row
Column
5. Player → PlayerSymbol

A Player has one PlayerSymbol.

Player
 └── PlayerSymbol

Example:

Player 1 → X
Player 2 → O
6. Game → GameStatus

Game maintains its current status.

Game
 └── GameStatus

Possible values:

InProgress
Won
Draw
Relationship Diagram
                   ┌──────────────┐
                   │    Game      │
                   └──────┬───────┘
                          │
              ┌───────────┼───────────┐
              │           │           │
              ▼           ▼           ▼
          Player X    Player O      Board
                                      │
                                      │
                                      ▼
                                   Cell[]
                                      │
                                      ▼
                                  Position
Inheritance

We do NOT need inheritance.

For example, we should NOT create:

Player
 ├── XPlayer
 └── OPlayer

Why?

Because X and O do not represent different types of players.

The difference is only the symbol.

Therefore:

Player
{
    Name
    Symbol
}

is simpler.

Dependency Direction

The main flow is:

Game
  ↓
Board
  ↓
Cell

The Game coordinates the use case.

The Board manages board state.

The Cell manages the state of one position.

Important Design Principle

Prefer:

Composition

over unnecessary:

Inheritance

when there is no genuine "is-a" relationship.
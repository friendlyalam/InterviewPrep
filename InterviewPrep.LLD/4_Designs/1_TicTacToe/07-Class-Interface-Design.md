# `07-Class-Interface-Design.md`

```markdown
# Tic-Tac-Toe — Step 7: Class and Interface Design

## Objective

Convert the conceptual design into concrete C# classes.

---

# 1. PlayerSymbol

```csharp
public enum PlayerSymbol
{
    X,
    O
}

Purpose:

Represents X/O.
2. GameStatus
public enum GameStatus
{
    InProgress,
    Won,
    Draw
}

Purpose:

Represents the lifecycle of the game.
3. Position
public sealed record Position(int Row, int Column);

Purpose:

Represents a board coordinate.

Example:

Position(0, 1)
4. Player
Player
-------------------
Name
Symbol

Responsibilities:

Store player identity.
Store player symbol.
5. Cell
Cell
-------------------
Position
Symbol
-------------------
Mark()
IsEmpty

Responsibilities:

Represent one board position.
Protect cell state.
6. Board
Board
-------------------
Cells
-------------------
Place()
IsFull()
HasWinningLine()
GetSymbol()

Responsibilities:

Maintain board state.
Place moves.
Check winning condition.
Check whether board is full.
7. Game
Game
-------------------
Board
Players
CurrentPlayer
Status
-------------------
MakeMove()

Responsibilities:

Control game flow.
Manage turns.
Detect win/draw.
Stop moves after game completion.
Class Relationship
                  Game
                   │
          ┌────────┴────────┐
          │                 │
       Players             Board
          │                 │
       Player             Cells
                            │
                         Position
Interfaces

No mandatory interface is required for the first implementation.

This is intentional.

Potential future interfaces:

IWinCondition
IMoveStrategy
IGameRepository

should only be introduced when requirements justify them.

Public API

The main operation is:

game.MakeMove(position);

The caller does not need to know:

How cells are stored
How winning lines are checked
How turns are switched
Encapsulation Rules

The following should NOT be publicly mutable:

Board internal cells
Cell symbol
Current player index
Game status

The objects should control their own state.

Final Design
Player
 ├── Name
 └── Symbol

Position
 ├── Row
 └── Column

Cell
 ├── Position
 └── Symbol

Board
 └── Cell[3,3]

Game
 ├── Player[]
 ├── Board
 ├── CurrentPlayer
 └── GameStatus
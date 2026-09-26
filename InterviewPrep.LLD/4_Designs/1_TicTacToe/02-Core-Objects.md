# Tic-Tac-Toe — Step 2: Core Objects

## Objective

Identify the core objects/classes from the requirements.

We should derive objects from responsibilities rather than immediately thinking about design patterns.

---

## 1. Player

### Responsibility

Represents a player participating in the game.

### Data

- Name
- Symbol

Example:

```text
Player 1 → X
Player 2 → O
Why do we need it?

The game needs to know:

Who is playing?
Which symbol belongs to that player?
Should Player handle the board?

No.

A player should not know:

How the board is stored
How winning is calculated
Whose turn comes next
2. Board
Responsibility

Represents the 3 × 3 game board.

Data
9 cells
Behavior
Place a symbol
Check whether a cell is empty
Check whether board is full
Check winning condition
Why do we need it?

The board is responsible for maintaining the state of the playing area.

3. Cell
Responsibility

Represents one position inside the board.

Data
Row
Column
Symbol

A cell can be:

Empty
X
O
Why separate Cell from Board?

The board contains multiple cells.

Board
 ├── Cell
 ├── Cell
 ├── Cell
 ├── ...
 └── Cell

This gives each object a clear responsibility.

4. Position
Responsibility

Represents a location on the board.

Row
Column

Example:

Position(0, 1)

means:

[ ] [X] [ ]
Why not simply pass two integers?

Instead of:

MakeMove(0, 1);

we can conceptually use:

MakeMove(new Position(0, 1));

This makes the method intention clearer.

5. Game
Responsibility

Controls the overall game flow.

The Game is responsible for:

Players
Current player
Board
Turn management
Accepting moves
Checking win
Checking draw
Ending the game
Why do we need Game?

Neither Player nor Board should control the complete game lifecycle.

The Game acts as the coordinator.

6. GameStatus

Represents the current state of the game.

Possible values:

InProgress
Won
Draw
7. PlayerSymbol

Represents the symbol used by a player.

Possible values:

X
O
Final Core Objects

The initial design contains:

Player
Cell
Position
Board
Game
GameStatus
PlayerSymbol
Responsibility Summary
| Object       | Responsibility                   |
| ------------ | -------------------------------- |
| Player       | Player information               |
| Position     | Board coordinate                 |
| Cell         | State of one board position      |
| Board        | Board state and board operations |
| Game         | Overall game flow                |
| GameStatus   | Game lifecycle                   |
| PlayerSymbol | X/O representation               |

Important LLD Observation

We should NOT create classes just because they sound like nouns.

For example:

Winner
TurnManager
MoveManager
GameManager
BoardManager

are not automatically required.

We first identify actual responsibilities.

If a responsibility is small and naturally belongs to an existing class, we should not create another class unnecessarily.

Current Object Model
Game
 ├── Player X
 ├── Player O
 └── Board
      └── Cells
           └── Position + Symbol
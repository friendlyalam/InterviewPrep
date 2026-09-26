# `09-Runtime-Walkthrough.md`

```markdown
# Tic-Tac-Toe — Step 9: Runtime Walkthrough

## Initial State

Players:

```text
Player 1 → X
Player 2 → O

Game:

Status = InProgress
CurrentPlayer = Player 1

Board:

[ ] [ ] [ ]
[ ] [ ] [ ]
[ ] [ ] [ ]
Move 1

Player 1 chooses:

(0,0)

Board:

[X] [ ] [ ]
[ ] [ ] [ ]
[ ] [ ] [ ]

Flow:

Game.MakeMove()
      ↓
Board.Place()
      ↓
Cell.Mark(X)
      ↓
Check Win
      ↓
Check Draw
      ↓
Switch Turn

Current player becomes Player 2.

Move 2

Player 2 chooses:

(1,0)

Board:

[X] [ ] [ ]
[O] [ ] [ ]
[ ] [ ] [ ]

Turn:

Player 1
Move 3

Player 1 chooses:

(0,1)

Board:

[X] [X] [ ]
[O] [ ] [ ]
[ ] [ ] [ ]

No winner.

Turn changes to Player 2.

Move 4

Player 2 chooses:

(1,1)

Board:

[X] [X] [ ]
[O] [O] [ ]
[ ] [ ] [ ]

No winner.

Turn changes to Player 1.

Move 5

Player 1 chooses:

(0,2)

Board:

[X] [X] [X]
[O] [O] [ ]
[ ] [ ] [ ]

Winner check:

[X] [X] [X]

Three X symbols are in one horizontal line.

Therefore:

Status = Won

The turn does NOT change.

Final State
Winner = Player 1
Status = Won
Draw Flow

A draw happens when:

Board.IsFull()
        ↓
true
        ↓
No player has won
        ↓
Status = Draw

Example:

X O X
X O O
O X X

All 9 cells are occupied.

No three-symbol winning line exists.

Therefore:

Status = Draw
Invalid Move Flow

Suppose a player attempts:

(0,0)

when that cell already contains X.

Flow:

Game.MakeMove()
      ↓
Board.Place()
      ↓
Cell.Mark()
      ↓
Cell is not empty
      ↓
InvalidOperationException

The move is rejected.

Move After Game Ends

If:

Status = Won

and another move is attempted:

Game.MakeMove()

the Game rejects the operation.

This ensures the game lifecycle is protected.
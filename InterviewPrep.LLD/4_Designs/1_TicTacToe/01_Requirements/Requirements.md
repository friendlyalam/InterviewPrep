# Tic-Tac-Toe — Requirements

## 1. Functional Requirements

### 1.1 Two-Player Game

- The game supports exactly two players.
- One player uses `X`.
- The other player uses `O`.

### 1.2 Game Board

- The game contains a 3 × 3 board.
- The board contains a total of 9 cells.

### 1.3 Player Turns

- Players take turns alternately.
- A player can make one move during their turn.
- After a valid move, the turn changes to the other player.

### 1.4 Valid Move

- A player can select only an empty cell.
- An occupied cell cannot be selected again.

### 1.5 Winning Condition

A player wins when their symbol occupies three cells in a straight line.

A winning line can be:

- Horizontal
- Vertical
- Diagonal

### 1.6 Draw Condition

- If all 9 cells are occupied and neither player has won, the game ends in a draw.

### 1.7 Game Completion

- Once a player wins, the game ends.
- Once the game is a draw, the game ends.
- No additional moves are allowed after the game has ended.

---

## 2. Assumptions

For the first version:

- There are exactly two players.
- Player 1 uses `X`.
- Player 2 uses `O`.
- Player 1 starts the game.
- The board is always 3 × 3.
- A player cannot select an occupied cell.
- The game stops immediately after a win or draw.

---

## 3. In Scope

- Two-player game
- 3 × 3 board
- Player turns
- Move validation
- Win detection
- Draw detection
- Game state
- Game completion

---

## 4. Out of Scope

- Computer/AI player
- Online multiplayer
- Network communication
- GUI
- Database
- Player accounts
- Score persistence
- Tournament management
- Multiple concurrent games

---

## 5. Future Requirements

These are not part of the current implementation but may be considered during interview follow-ups:

- Support different board sizes
- Support a computer player
- Support multiple players
- Maintain player scores
- Restart a completed game
- Undo a move
- Add online multiplayer
- Add time limits
- Persist game history
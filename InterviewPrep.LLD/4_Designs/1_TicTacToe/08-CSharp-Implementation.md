# `08-CSharp-Implementation.md`

```markdown
# Tic-Tac-Toe — Step 8: Complete C# Implementation

## Objective

Implement the design in clean, runnable C#.

The following code can be placed in a .NET console project.

---

## Program.cs

```csharp
using System;

namespace TicTacToe;

public enum PlayerSymbol
{
    X,
    O
}

public enum GameStatus
{
    InProgress,
    Won,
    Draw
}

public sealed record Position(int Row, int Column);

public sealed class Player
{
    public string Name { get; }
    public PlayerSymbol Symbol { get; }

    public Player(string name, PlayerSymbol symbol)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Player name is required.",
                nameof(name));
        }

        Name = name;
        Symbol = symbol;
    }
}

public sealed class Cell
{
    public Position Position { get; }

    public PlayerSymbol? Symbol { get; private set; }

    public bool IsEmpty => Symbol is null;

    public Cell(Position position)
    {
        Position = position
            ?? throw new ArgumentNullException(nameof(position));
    }

    public void Mark(PlayerSymbol symbol)
    {
        if (!IsEmpty)
        {
            throw new InvalidOperationException(
                "Cell is already occupied.");
        }

        Symbol = symbol;
    }
}

public sealed class Board
{
    private const int Size = 3;

    private readonly Cell[,] _cells;

    public Board()
    {
        _cells = new Cell[Size, Size];

        for (var row = 0; row < Size; row++)
        {
            for (var column = 0; column < Size; column++)
            {
                _cells[row, column] =
                    new Cell(new Position(row, column));
            }
        }
    }

    public void Place(
        Position position,
        PlayerSymbol symbol)
    {
        ValidatePosition(position);

        _cells[position.Row, position.Column]
            .Mark(symbol);
    }

    public bool IsFull()
    {
        for (var row = 0; row < Size; row++)
        {
            for (var column = 0; column < Size; column++)
            {
                if (_cells[row, column].IsEmpty)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool HasWinningLine(PlayerSymbol symbol)
    {
        // Horizontal lines
        for (var row = 0; row < Size; row++)
        {
            if (HasRow(symbol, row))
            {
                return true;
            }
        }

        // Vertical lines
        for (var column = 0; column < Size; column++)
        {
            if (HasColumn(symbol, column))
            {
                return true;
            }
        }

        // Main diagonal
        if (_cells[0, 0].Symbol == symbol &&
            _cells[1, 1].Symbol == symbol &&
            _cells[2, 2].Symbol == symbol)
        {
            return true;
        }

        // Other diagonal
        if (_cells[0, 2].Symbol == symbol &&
            _cells[1, 1].Symbol == symbol &&
            _cells[2, 0].Symbol == symbol)
        {
            return true;
        }

        return false;
    }

    public PlayerSymbol? GetSymbol(Position position)
    {
        ValidatePosition(position);

        return _cells[position.Row, position.Column].Symbol;
    }

    private bool HasRow(
        PlayerSymbol symbol,
        int row)
    {
        for (var column = 0; column < Size; column++)
        {
            if (_cells[row, column].Symbol != symbol)
            {
                return false;
            }
        }

        return true;
    }

    private bool HasColumn(
        PlayerSymbol symbol,
        int column)
    {
        for (var row = 0; row < Size; row++)
        {
            if (_cells[row, column].Symbol != symbol)
            {
                return false;
            }
        }

        return true;
    }

    private static void ValidatePosition(Position position)
    {
        if (position is null)
        {
            throw new ArgumentNullException(nameof(position));
        }

        if (position.Row < 0 ||
            position.Row >= Size ||
            position.Column < 0 ||
            position.Column >= Size)
        {
            throw new ArgumentOutOfRangeException(
                nameof(position),
                "Position must be inside the 3 x 3 board.");
        }
    }
}

public sealed class Game
{
    private readonly Board _board;
    private readonly Player[] _players;

    private int _currentPlayerIndex;

    public Game(
        Player player1,
        Player player2)
    {
        if (player1 is null)
        {
            throw new ArgumentNullException(nameof(player1));
        }

        if (player2 is null)
        {
            throw new ArgumentNullException(nameof(player2));
        }

        if (player1.Symbol == player2.Symbol)
        {
            throw new ArgumentException(
                "Players must have different symbols.");
        }

        _board = new Board();

        _players =
        [
            player1,
            player2
        ];

        _currentPlayerIndex = 0;

        Status = GameStatus.InProgress;
    }

    public GameStatus Status { get; private set; }

    public Player CurrentPlayer =>
        _players[_currentPlayerIndex];

    public Board Board => _board;

    public void MakeMove(Position position)
    {
        if (Status != GameStatus.InProgress)
        {
            throw new InvalidOperationException(
                "The game has already ended.");
        }

        _board.Place(
            position,
            CurrentPlayer.Symbol);

        if (_board.HasWinningLine(
                CurrentPlayer.Symbol))
        {
            Status = GameStatus.Won;
            return;
        }

        if (_board.IsFull())
        {
            Status = GameStatus.Draw;
            return;
        }

        SwitchTurn();
    }

    private void SwitchTurn()
    {
        _currentPlayerIndex =
            (_currentPlayerIndex + 1)
            % _players.Length;
    }
}

public static class Program
{
    public static void Main()
    {
        var player1 =
            new Player("Player 1", PlayerSymbol.X);

        var player2 =
            new Player("Player 2", PlayerSymbol.O);

        var game =
            new Game(player1, player2);

        game.MakeMove(new Position(0, 0)); // X
        game.MakeMove(new Position(1, 0)); // O
        game.MakeMove(new Position(0, 1)); // X
        game.MakeMove(new Position(1, 1)); // O
        game.MakeMove(new Position(0, 2)); // X wins

        Console.WriteLine(
            $"Status: {game.Status}");

        Console.WriteLine(
            $"Current Player: {game.CurrentPlayer.Name}");
    }
}
Design Notes
Why Game owns the turn?

Because turn management is part of game flow.

Why Board owns Cells?

Because the Board is responsible for board state.

Why Cell protects Symbol?

Because the Cell should prevent invalid state such as:

X → O

without first validating whether the cell is empty.

Why no interfaces?

There is currently no replaceable dependency that requires one.

Why no Factory?

Object creation is simple.

Why no Singleton?

There is no reason the game should have only one instance.

Why no inheritance?

There is no useful inheritance relationship.

Complexity

For the fixed 3 × 3 board:

Move placement:
O(1)

Win detection:
O(1)

Full-board check:
O(1)

Space:
O(1)

The constants are small because the board always contains 9 cells.
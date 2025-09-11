// -----------------------------------------------------------------------
// <copyright file="Connect4GameTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using MyGames.Connect4.UnitTests.Mocks;
using MyGames.Core;
using MyGames.Core.Extensions;
using MyNet.Utilities.Helpers;
using Xunit;

namespace MyGames.Connect4.UnitTests;

public class Connect4GameTests
{
    private static Connect4Game CreateNewGame(int numberOfPlayers = 2)
    {
        var players = EnumerableHelper.Range(1, numberOfPlayers, 1).Select(_ => new MockPlayer());
        return new Connect4Game(players);
    }

    [Fact]
    public void NewGame_CurrentPlayerIsPlayer1()
    {
        // Arrange
        var game = CreateNewGame();

        // Act
        var currentPlayer = game.CurrentPlayer;

        // Assert
        Assert.Equal(game.Players[0], currentPlayer);
    }

    [Fact]
    public void NewGame_BoardIsEmpty()
    {
        // Arrange
        var game = CreateNewGame();

        // Act & Assert
        Assert.True(game.Board.IsEmpty());
    }

    [Fact]
    public void MakeMove_ValidMove_SetsCorrectPlayerAtPosition()
    {
        // Arrange
        var game = CreateNewGame();
        const int column = 3;

        // Act
        var moveIsValid = game.MakeMove(column);

        var piece = game.Board.TryGetPiece(game.Board.Rows.Count - 1, column);

        // Assert
        Assert.True(moveIsValid);
        Assert.NotNull(piece);
        Assert.Equal(game.Players[0], piece.Player);
    }

    [Fact]
    public void MakeMove_InvalidMove_ReturnFalse()
    {
        // Arrange
        var game = CreateNewGame();
        var column = game.Board.Columns.Count;

        // Act
        var moveIsValid = game.MakeMove(column);

        // Assert
        Assert.False(moveIsValid);
    }

    [Fact]
    public void MakeMove_InvalidMoveInFullColumn_ReturnFalse()
    {
        // Arrange
        var game = CreateNewGame();
        const int column = 3;

        // Act & Assert
        // Fill the column
        for (var row = 0; row < game.Board.Rows.Count; row++)
            game.MakeMove(column);

        var moveIsValid = game.MakeMove(column);

        // Assert
        Assert.False(moveIsValid);
    }

    [Fact]
    public void IsColumnFull_EmptyColumn_ReturnsFalse()
    {
        // Arrange
        var game = CreateNewGame();
        const int column = 0;

        // Act
        var isColumnFull = game.Board.GetColumn(column).IsFull();

        // Assert
        Assert.False(isColumnFull);
    }

    [Fact]
    public void IsColumnFull_FullColumn_ReturnsTrue()
    {
        // Arrange
        var game = CreateNewGame();
        const int column = 3;

        // Fill the column
        for (var row = 0; row < game.Board.Rows.Count; row++)
            game.MakeMove(column);

        // Act
        var isColumnFull = game.Board.GetColumn(column).IsFull();

        // Assert
        Assert.True(isColumnFull);
    }

    [Fact]
    public void IsGameOver_EmptyBoard_ReturnsFalse()
    {
        // Arrange
        var game = CreateNewGame();

        // Act
        var isGameOver = game.IsOver;

        // Assert
        Assert.False(isGameOver);
    }

    [Fact]
    public void IsGameOver_WinningMove_ReturnsTrue()
    {
        // Arrange
        var game = CreateNewGame();

        // Perform winning moves
        game.MakeMove(0); // Player1
        game.MakeMove(1); // Player2
        game.MakeMove(0); // Player1
        game.MakeMove(1); // Player2
        game.MakeMove(0); // Player1
        game.MakeMove(1); // Player2
        game.MakeMove(0); // Player1

        // Act
        var isGameOver = game.IsOver;
        var pieceWinners = game.WinnerSquares.ToList();

        // Assert
        Assert.True(isGameOver);
        Assert.Equal(4, pieceWinners.Count);
        Assert.All(pieceWinners, x => Assert.Equal(game.Players[0], x.Piece.Player));
    }

    [Fact]
    public void IsGameOver_FullBoardNoWin_ReturnsTrue()
    {
        // Arrange
        var game = CreateNewGame();

        // Fill the board without a win
        for (var column = 0; column < game.Board.Columns.Count; column++)
        {
            for (var row = 0; row < game.Board.Rows.Count; row++)
                game.MakeMove(column);
        }

        // Act
        var isGameOver = game.IsOver;

        // Assert
        Assert.True(isGameOver);
    }

    [Fact]
    public void IsGameOver_WinningHorizontalMove_ReturnsSameRow()
    {
        // Arrange
        var game = CreateNewGame();

        // Perform winning moves
        game.MakeMove(0); // Player1
        game.MakeMove(0); // Player2
        game.MakeMove(1); // Player1
        game.MakeMove(1); // Player2
        game.MakeMove(2); // Player1
        game.MakeMove(2); // Player2
        game.MakeMove(3); // Player1

        // Act
        var winnerSquares = game.WinnerSquares.ToList();

        // Assert
        Assert.Equal(game.Players[0], game.Winner);
        Assert.Equal(4, winnerSquares.Count);
        Assert.All(winnerSquares, x => Assert.Equal(game.Board.Rows.Count - 1, x.Row));
    }

    [Fact]
    public void IsGameOver_WinningVerticalMove_ReturnsSameColumn()
    {
        // Arrange
        var game = CreateNewGame();

        // Perform winning moves
        game.MakeMove(3); // Player1
        game.MakeMove(1); // Player2
        game.MakeMove(3); // Player1
        game.MakeMove(1); // Player2
        game.MakeMove(3); // Player1
        game.MakeMove(2); // Player2
        game.MakeMove(3); // Player1

        // Act
        var winnerSquares = game.WinnerSquares.ToList();

        // Assert
        Assert.Equal(game.Players[0], game.Winner);
        Assert.Equal(4, winnerSquares.Count);
        Assert.All(winnerSquares, x => Assert.Equal(3, x.Column));
    }

    [Fact]
    public void IsGameOver_WinningDiagonalBottomToTopMove_ReturnsSameColumn()
    {
        // Arrange
        var game = CreateNewGame();

        // Perform winning moves
        game.MakeMove(1); // Player1
        game.MakeMove(2); // Player2
        game.MakeMove(2); // Player1
        game.MakeMove(3); // Player2
        game.MakeMove(4); // Player1
        game.MakeMove(3); // Player2
        game.MakeMove(3); // Player1
        game.MakeMove(4); // Player2
        game.MakeMove(4); // Player1
        game.MakeMove(5); // Player2
        game.MakeMove(4); // Player1

        // Act
        var winnerSquares = game.WinnerSquares.ToList();

        // Assert
        Assert.Equal(game.Players[0], game.Winner);
        Assert.Equal(4, winnerSquares.Count);

        Assert.Equal(new BoardCoordinates(game.Board.Rows.Count - 1, 1), winnerSquares[0].Coordinates);
        Assert.Equal(new BoardCoordinates(game.Board.Rows.Count - 2, 2), winnerSquares[1].Coordinates);
        Assert.Equal(new BoardCoordinates(game.Board.Rows.Count - 3, 3), winnerSquares[2].Coordinates);
        Assert.Equal(new BoardCoordinates(game.Board.Rows.Count - 4, 4), winnerSquares[3].Coordinates);
    }

    [Fact]
    public void IsGameOver_WinningDiagonalTopToBottomMove_ReturnsSameColumn()
    {
        // Arrange
        var game = CreateNewGame();

        // Perform winning moves
        game.MakeMove(4); // Player1
        game.MakeMove(3); // Player2
        game.MakeMove(3); // Player1
        game.MakeMove(2); // Player2
        game.MakeMove(1); // Player1
        game.MakeMove(2); // Player2
        game.MakeMove(2); // Player1
        game.MakeMove(1); // Player2
        game.MakeMove(1); // Player1
        game.MakeMove(0); // Player2
        game.MakeMove(1); // Player1

        // Act
        var winnerSquares = game.WinnerSquares.ToList();

        // Assert
        Assert.Equal(game.Players[0], game.Winner);
        Assert.Equal(4, winnerSquares.Count);

        Assert.Equal(new BoardCoordinates(game.Board.Rows.Count - 4, 1), winnerSquares[0].Coordinates);
        Assert.Equal(new BoardCoordinates(game.Board.Rows.Count - 3, 2), winnerSquares[1].Coordinates);
        Assert.Equal(new BoardCoordinates(game.Board.Rows.Count - 2, 3), winnerSquares[2].Coordinates);
        Assert.Equal(new BoardCoordinates(game.Board.Rows.Count - 1, 4), winnerSquares[3].Coordinates);
    }

    [Fact]
    public void HistoryMoves_NextMove_AddHistoryMove()
    {
        var player1 = new MockPlayer();
        var player2 = new MockPlayer();
        var game = new Connect4Game([player1, player2]);

        var moveCount = 0;
        while (!game.IsOver)
        {
            if (game.NextMove())
            {
                moveCount++;
                Assert.Equal(moveCount, game.History.Count);
            }
        }
    }
}

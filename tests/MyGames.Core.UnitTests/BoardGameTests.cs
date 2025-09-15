// -----------------------------------------------------------------------
// <copyright file="BoardGameTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using MyGames.Core.UnitTests.Mocks;
using Xunit;

namespace MyGames.Core.UnitTests;

public class BoardGameTests
{
    private static MockBoardGame CreateGame()
    {
        var board = new MockBoard(6, 7);
        var players = new List<MockPlayer> { new(), new() };
        var game = new MockBoardGame(board, players);
        return game;
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenNoPlayersProvided()
    {
        // Arrange
        var board = new MockBoard(3, 3);
        var players = new List<MockPlayer>();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new MockBoardGame(board, players));
        Assert.Equal("No players provided (Parameter 'players')", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldSetCurrentPlayerIndex()
    {
        // Arrange
        var board = new MockBoard(3, 3);
        var players = new List<MockPlayer> { new(), new() };
        const int currentPlayerIndex = 1;

        // Act
        var game = new MockBoardGame(board, players, currentPlayerIndex);

        // Assert
        Assert.Equal(players[currentPlayerIndex], game.CurrentPlayer);
    }

    [Fact]
    public void Constructor_ShouldInitializeBoardGameWithCorrectValues()
    {
        // Arrange
        var board = new MockBoard(6, 7);
        var players = new List<MockPlayer> { new(), new() };

        // Act
        var game = new MockBoardGame(board, players);

        // Assert
        Assert.Equal(board, game.Board);
        Assert.Equal(players, game.Players);
    }

    [Fact]
    public void GetCurrentPlayer_ShouldReturnFirstPlayerInitially()
    {
        // Arrange
        var board = new MockBoard(6, 7);
        var players = new List<MockPlayer> { new(), new() };
        var game = new MockBoardGame(board, players);

        // Act
        var currentPlayer = game.CurrentPlayer;

        // Assert
        Assert.Equal(players[0], currentPlayer);
    }

    [Fact]
    public void GetCurrentPlayer_ShouldThrowInvalidOperationException_WhenIndexIsOutOfRange()
    {
        // Arrange
        var board = new MockBoard(6, 7);
        var players = new List<MockPlayer> { new(), new() };
        var game = new MockBoardGame(board, players, -1);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => game.CurrentPlayer);
    }

    [Fact]
    public void MakeMove_ShouldAddMoveToHistory()
    {
        // Arrange
        var game = CreateGame();
        var move = new MockMove(game.Players[0]);

        // Act
        var result = game.MakeMove(move);

        // Assert
        Assert.True(result);
        Assert.Single(game.History);
    }

    [Fact]
    public void Undo_ShouldThrowException_WhenNoMovesToUndo()
    {
        // Arrange
        var game = CreateGame();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => game.Undo());
    }

    [Fact]
    public void Redo_ShouldThrowException_WhenNoMovesToRedo()
    {
        // Arrange
        var game = CreateGame();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => game.Redo());
    }

    [Fact]
    public void Undo_ShouldRevertLastMove()
    {
        // Arrange
        var game = CreateGame();
        var move = new MockMove(game.Players[0]);

        // Act
        game.MakeMove(move);
        var undoMove = game.Undo();

        // Assert
        Assert.NotNull(undoMove);
        Assert.Empty(game.History);
        Assert.Single(game.UndoHistory);
    }

    [Fact]
    public void Redo_ShouldReapplyLastUndoneMove()
    {
        // Arrange
        var game = CreateGame();
        var move = new MockMove(game.Players[0]);

        // Act
        game.MakeMove(move);
        game.Undo();
        var redoMove = game.Redo();

        // Assert
        Assert.NotNull(redoMove);
        Assert.Single(game.History);
        Assert.Empty(game.UndoHistory);
    }

    [Fact]
    public void CanUndo_NoMoves_ReturnsFalse()
    {
        // Arrange
        var game = CreateGame();

        // Act
        var result = game.CanUndo();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanUndo_AfterMove_ReturnsTrue()
    {
        // Arrange
        var game = CreateGame();
        var move = new MockMove(game.Players[0]);
        game.MakeMove(move);

        // Act
        var result = game.CanUndo();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanRedo_NoUndoneMoves_ReturnsFalse()
    {
        // Arrange
        var game = CreateGame();

        // Act
        var result = game.CanRedo();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanRedo_AfterUndo_ReturnsTrue()
    {
        // Arrange
        var game = CreateGame();
        var move = new MockMove(game.Players[0]);
        game.MakeMove(move);
        game.Undo();

        // Act
        var result = game.CanRedo();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Clone_ShouldCreateExactCopyOfGame()
    {
        // Arrange
        var game = CreateGame();
        var move = new MockMove(game.Players[0]);
        game.MakeMove(move);

        // Act
        var clone = game.Clone();

        // Assert
        Assert.NotSame(game, clone);
        Assert.Equal(game.Board.ToString(), clone.Board.ToString());
        Assert.Equal(game.Players, clone.Players);
    }
}

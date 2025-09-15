// -----------------------------------------------------------------------
// <copyright file="Connect4PieceTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core;
using Xunit;

namespace MyGames.Connect4.UnitTests;

public class Connect4PieceTests
{
    private sealed class TestPlayer : IConnect4Player
    {
        public Connect4Move NextMove(Connect4Game game) => new(0);
    }

    [Fact]
    public void IsSimilar_ShouldReturnTrueForSamePlayer()
    {
        // Arrange
        var player = new TestPlayer();
        var piece1 = new Connect4Piece(player);
        var piece2 = new Connect4Piece(player);

        // Act
        var result = piece1.IsSimilar(piece2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsSimilar_ShouldReturnFalseForDifferentPlayers()
    {
        // Arrange
        var player1 = new TestPlayer();
        var player2 = new TestPlayer();
        var piece1 = new Connect4Piece(player1);
        var piece2 = new Connect4Piece(player2);

        // Act
        var result = piece1.IsSimilar(piece2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSimilar_ShouldReturnFalseForNull()
    {
        // Arrange
        var player = new TestPlayer();
        var piece = new Connect4Piece(player);

        // Act
        var result = piece.IsSimilar(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSimilar_ShouldReturnFalseForDifferentType()
    {
        // Arrange
        var player = new TestPlayer();
        var piece = new Connect4Piece(player);
        var differentPiece = new TestPiece();

        // Act
        var result = piece.IsSimilar(differentPiece);

        // Assert
        Assert.False(result);
    }

    private sealed class TestPiece : IPiece
    {
        public bool IsSimilar(IPiece? obj) => obj is TestPiece;
    }
}

// -----------------------------------------------------------------------
// <copyright file="SquareTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyGames.Core.UnitTests.Mocks;
using Xunit;

namespace MyGames.Core.UnitTests;

public class SquareTests
{
    [Fact]
    public void GetPiece_ShouldReturnException()
    {
        // Act
        var coordinates = new BoardCoordinates(1, 1);
        var square = new Square<MockPiece>(coordinates);

        // Assert
        Assert.Throws<InvalidOperationException>(() => square.Piece);
    }

    [Fact]
    public void IsSimilar_ShouldReturnTrueForSimilarSquares()
    {
        // Arrange
        var coordinates = new BoardCoordinates(1, 1);
        var piece = new MockPiece();
        var square1 = new Square<MockPiece>(coordinates, piece);
        var square2 = new Square<MockPiece>(coordinates, piece);

        // Act
        var result = square1.IsSimilar(square2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsSimilar_ShouldReturnFalseForDifferentCoordinates()
    {
        // Arrange
        var coordinates1 = new BoardCoordinates(1, 1);
        var coordinates2 = new BoardCoordinates(2, 2);
        var piece = new MockPiece();
        var square1 = new Square<MockPiece>(coordinates1, piece);
        var square2 = new Square<MockPiece>(coordinates2, piece);

        // Act
        var result = square1.IsSimilar(square2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSimilar_ShouldReturnFalseForDifferentPieces()
    {
        // Arrange
        var coordinates = new BoardCoordinates(1, 1);
        var piece1 = new MockPiece();
        var piece2 = new MockPiece();
        var square1 = new Square<MockPiece>(coordinates, piece1);
        var square2 = new Square<MockPiece>(coordinates, piece2);

        // Act
        var result = square1.IsSimilar(square2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSimilar_ShouldReturnTrueForBothEmptySquares()
    {
        // Arrange
        var coordinates = new BoardCoordinates(1, 1);
        var square1 = new Square<MockPiece>(coordinates);
        var square2 = new Square<MockPiece>(coordinates);

        // Act
        var result = square1.IsSimilar(square2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsSimilar_ShouldReturnFalseForOneEmptySquare()
    {
        // Arrange
        var coordinates = new BoardCoordinates(1, 1);
        var piece = new MockPiece();
        var square1 = new Square<MockPiece>(coordinates, piece);
        var square2 = new Square<MockPiece>(coordinates);

        // Act
        var result = square1.IsSimilar(square2);

        // Assert
        Assert.False(result);
    }
}

// -----------------------------------------------------------------------
// <copyright file="SquaresCollectionTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using MyGames.Core.UnitTests.Mocks;
using Xunit;

namespace MyGames.Core.UnitTests;

public class SquaresCollectionTests
{
    [Fact]
    public void Indexer_ShouldReturnCorrectSquare()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0)),
            new(new BoardCoordinates(0, 1))
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var square = collection[1];

        // Assert
        Assert.Equal(squares[1], square);
    }

    [Fact]
    public void Count_ShouldReturnCorrectNumberOfSquares()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0)),
            new(new BoardCoordinates(0, 1))
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var count = collection.Count;

        // Assert
        Assert.Equal(2, count);
    }

    [Fact]
    public void IsValid_ShouldReturnTrueForValidIndex()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0)),
            new(new BoardCoordinates(0, 1))
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var isValid = collection.IsValid(1);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void IsValid_ShouldReturnFalseForInvalidIndex()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0)),
            new(new BoardCoordinates(0, 1))
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var isValid = collection.IsValid(2);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_ShouldReturnFalseForNegativeIndex()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0)),
            new(new BoardCoordinates(0, 1))
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var isValid = collection.IsValid(-2);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void IsEmpty_ShouldReturnTrueIfAllSquaresAreEmpty()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0)),
            new(new BoardCoordinates(0, 1))
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var isEmpty = collection.IsEmpty();

        // Assert
        Assert.True(isEmpty);
    }

    [Fact]
    public void IsFull_ShouldReturnTrueIfAllSquaresAreFull()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0), new MockPiece()),
            new(new BoardCoordinates(0, 1), new MockPiece())
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var isFull = collection.IsFull();

        // Assert
        Assert.True(isFull);
    }

    [Fact]
    public void GetSquare_ShouldReturnSquareForValidIndex()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0)),
            new(new BoardCoordinates(0, 1))
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var square = collection.GetSquare(1);

        // Assert
        Assert.Equal(squares[1], square);
    }

    [Fact]
    public void GetSquare_ShouldReturnNullForInvalidIndex()
    {
        // Arrange
        var squares = new List<Square<MockPiece>>
        {
            new(new BoardCoordinates(0, 0)),
            new(new BoardCoordinates(0, 1))
        };
        var collection = new SquaresCollection<MockPiece>(squares);

        // Act
        var square = collection.GetSquare(2);

        // Assert
        Assert.Null(square);
    }
}

// -----------------------------------------------------------------------
// <copyright file="SquaresExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using MyGames.Core.Extensions;
using MyGames.Core.UnitTests.Mocks;
using Xunit;

namespace MyGames.Core.UnitTests;

public class SquaresExtensionsTests
{
    private static SquaresCollection<MockPiece> CreateBoardSquaresCollection(params Square<MockPiece>[] squares) => new(squares);

    [Fact]
    public void GetNotEmptySquares_ShouldReturnOnlyNotEmptySquares()
    {
        // Arrange
        var piece = new MockPiece();
        var squares = CreateBoardSquaresCollection(
            new Square<MockPiece>(new BoardCoordinates(0, 0), piece),
            new Square<MockPiece>(new BoardCoordinates(0, 1)),
            new Square<MockPiece>(new BoardCoordinates(0, 2), piece));

        // Act
        var notEmptySquares = squares.GetNotEmptySquares().ToList();

        // Assert
        Assert.Equal(2, notEmptySquares.Count);
        Assert.All(notEmptySquares, square => Assert.False(square.IsEmpty));
    }

    [Fact]
    public void GetEmptySquares_ShouldReturnOnlyEmptySquares()
    {
        // Arrange
        var piece = new MockPiece();
        var squares = CreateBoardSquaresCollection(
            new Square<MockPiece>(new BoardCoordinates(0, 0), piece),
            new Square<MockPiece>(new BoardCoordinates(0, 1)),
            new Square<MockPiece>(new BoardCoordinates(0, 2), piece));

        // Act
        var emptySquares = squares.GetEmptySquares().ToList();

        // Assert
        Assert.Single(emptySquares);
        Assert.All(emptySquares, square => Assert.True(square.IsEmpty));
    }

    [Fact]
    public void GetNotEmptyConsecutives_ByPiece_ShouldReturnConsecutiveSquares()
    {
        // Arrange
        var piece1 = new MockPiece();
        var piece2 = new MockPiece();
        var squares = CreateBoardSquaresCollection(
            new Square<MockPiece>(new BoardCoordinates(0, 0), piece1),
            new Square<MockPiece>(new BoardCoordinates(0, 1), piece1),
            new Square<MockPiece>(new BoardCoordinates(0, 2), piece2),
            new Square<MockPiece>(new BoardCoordinates(0, 3), piece1));

        // Act
        var consecutives = squares.GetNotEmptyConsecutives((p1, p2) => p1 == p2).ToList();

        // Assert
        Assert.Equal(3, consecutives.Count);
        Assert.Equal(2, consecutives[0].Count);
        Assert.Single(consecutives[1]);
        Assert.Single(consecutives[2]);
    }

    [Fact]
    public void GetNotEmptyConsecutives_EmptyList_ShouldReturnEmptySquares()
    {
        // Arrange
        var squares = CreateBoardSquaresCollection();

        // Act
        var consecutives = squares.GetNotEmptyConsecutives((p1, p2) => p1 == p2).ToList();

        // Assert
        Assert.Empty(consecutives);
    }

    [Fact]
    public void GetConsecutives_BySquare_ShouldReturnConsecutiveSquares()
    {
        // Arrange
        var piece1 = new MockPiece();
        var piece2 = new MockPiece();
        var squares = CreateBoardSquaresCollection(
            new Square<MockPiece>(new BoardCoordinates(0, 0), piece1),
            new Square<MockPiece>(new BoardCoordinates(0, 1), piece1),
            new Square<MockPiece>(new BoardCoordinates(0, 2), piece2),
            new Square<MockPiece>(new BoardCoordinates(0, 3), piece1));

        // Act
        var consecutives = squares.GetConsecutives((c1, c2) => c1.Piece == c2.Piece).ToList();

        // Assert
        Assert.Equal(3, consecutives.Count);
        Assert.Equal(2, consecutives[0].Count);
        Assert.Single(consecutives[1]);
        Assert.Single(consecutives[2]);
    }

    [Fact]
    public void GetConsecutives_EmptyList_ShouldReturnEmptySquares()
    {
        // Arrange
        var squares = CreateBoardSquaresCollection();

        // Act
        var consecutives = squares.GetConsecutives((c1, c2) => c1.Piece == c2.Piece).ToList();

        // Assert
        Assert.Empty(consecutives);
    }
}

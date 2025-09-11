// -----------------------------------------------------------------------
// <copyright file="BoardDirectionTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Xunit;

namespace MyGames.Core.UnitTests;

public class BoardDirectionTests
{
    [Fact]
    public void Operator_Addition_ShouldReturnCorrectResult()
    {
        // Arrange
        var direction1 = new BoardDirection(1, 2);
        var direction2 = new BoardDirection(3, 4);

        // Act
        var result = direction1 + direction2;

        // Assert
        Assert.Equal(new BoardDirection(4, 6), result);
    }

    [Fact]
    public void Operator_Subtraction_ShouldReturnCorrectResult()
    {
        // Arrange
        var direction1 = new BoardDirection(5, 7);
        var direction2 = new BoardDirection(2, 3);

        // Act
        var result = direction1 - direction2;

        // Assert
        Assert.Equal(new BoardDirection(3, 4), result);
    }

    [Fact]
    public void Operator_Multiplication_ShouldReturnCorrectResult()
    {
        // Arrange
        var direction = new BoardDirection(2, 3);
        const int multiplier = 2;

        // Act
        var result = direction * multiplier;

        // Assert
        Assert.Equal(new BoardDirection(4, 6), result);
    }

    [Fact]
    public void Operator_Division_ShouldReturnCorrectResult()
    {
        // Arrange
        var direction = new BoardDirection(4, 6);
        const int divisor = 2;

        // Act
        var result = direction / divisor;

        // Assert
        Assert.Equal(new BoardDirection(2, 3), result);
    }

    [Fact]
    public void Operator_Equality_ShouldReturnTrueForEqualDirections()
    {
        // Arrange
        var direction1 = new BoardDirection(1, 1);
        var direction2 = new BoardDirection(1, 1);

        // Act & Assert
        Assert.True(direction1 == direction2);
    }

    [Fact]
    public void Operator_Inequality_ShouldReturnTrueForDifferentDirections()
    {
        // Arrange
        var direction1 = new BoardDirection(1, 1);
        var direction2 = new BoardDirection(2, 2);

        // Act & Assert
        Assert.True(direction1 != direction2);
    }

    [Fact]
    public void Operator_Inequality_ShouldReturnTrueForDifferentRow()
    {
        // Arrange
        var direction1 = new BoardDirection(1, 1);
        var direction2 = new BoardDirection(2, 1);

        // Act & Assert
        Assert.True(direction1 != direction2);
    }

    [Fact]
    public void Operator_Inequality_ShouldReturnTrueForDifferentColumn()
    {
        // Arrange
        var direction1 = new BoardDirection(1, 1);
        var direction2 = new BoardDirection(1, 2);

        // Act & Assert
        Assert.True(direction1 != direction2);
    }

    [Fact]
    public void Equals_ShouldReturnTrueForEqualDirections()
    {
        // Arrange
        var direction1 = new BoardDirection(1, 1);
        var direction2 = new BoardDirection(1, 1);

        // Act & Assert
        Assert.True(direction1.Equals(direction2));
    }

    [Fact]
    public void Equals_ShouldReturnFalseForDifferentDirections()
    {
        // Arrange
        var direction1 = new BoardDirection(1, 1);
        var direction2 = new BoardDirection(2, 2);

        // Act & Assert
        Assert.False(direction1.Equals(direction2));
    }

    [Fact]
    public void Equals_ShouldReturnFalseForNullDirections()
    {
        // Arrange
        var direction1 = new BoardDirection(1, 1);

        // Act & Assert
        Assert.False(direction1.Equals(null));
    }
}

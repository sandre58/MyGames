// -----------------------------------------------------------------------
// <copyright file="BoardCoordinatesExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core.Extensions;
using Xunit;

namespace MyGames.Core.UnitTests;

public class BoardCoordinatesExtensionsTests
{
    [Fact]
    public void GetDirection_ShouldReturnUp()
    {
        var from = new BoardCoordinates(2, 2);
        var to = new BoardCoordinates(1, 2);
        var direction = from.GetDirection(to);

        Assert.Equal(BoardDirection.Up, direction);
    }

    [Fact]
    public void GetDirection_ShouldReturnDown()
    {
        var from = new BoardCoordinates(2, 2);
        var to = new BoardCoordinates(3, 2);
        var direction = from.GetDirection(to);

        Assert.Equal(BoardDirection.Down, direction);
    }

    [Fact]
    public void GetDirection_ShouldReturnLeft()
    {
        var from = new BoardCoordinates(2, 2);
        var to = new BoardCoordinates(2, 1);
        var direction = from.GetDirection(to);

        Assert.Equal(BoardDirection.Left, direction);
    }

    [Fact]
    public void GetDirection_ShouldReturnRight()
    {
        var from = new BoardCoordinates(2, 2);
        var to = new BoardCoordinates(2, 3);
        var direction = from.GetDirection(to);

        Assert.Equal(BoardDirection.Right, direction);
    }

    [Fact]
    public void GetDirection_ShouldReturnUpLeft()
    {
        var from = new BoardCoordinates(2, 2);
        var to = new BoardCoordinates(1, 1);
        var direction = from.GetDirection(to);

        Assert.Equal(BoardDirection.UpLeft, direction);
    }

    [Fact]
    public void GetDirection_ShouldReturnUpRight()
    {
        var from = new BoardCoordinates(2, 2);
        var to = new BoardCoordinates(1, 3);
        var direction = from.GetDirection(to);

        Assert.Equal(BoardDirection.UpRight, direction);
    }

    [Fact]
    public void GetDirection_ShouldReturnDownLeft()
    {
        var from = new BoardCoordinates(2, 2);
        var to = new BoardCoordinates(3, 1);
        var direction = from.GetDirection(to);

        Assert.Equal(BoardDirection.DownLeft, direction);
    }

    [Fact]
    public void GetDirection_ShouldReturnDownRight()
    {
        var from = new BoardCoordinates(2, 2);
        var to = new BoardCoordinates(3, 3);
        var direction = from.GetDirection(to);

        Assert.Equal(BoardDirection.DownRight, direction);
    }
}

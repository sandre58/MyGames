// -----------------------------------------------------------------------
// <copyright file="Connect4MoveTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Connect4.UnitTests.Mocks;
using MyGames.Core.Exceptions;
using Xunit;

namespace MyGames.Connect4.UnitTests;

public class Connect4MoveTests
{
    [Fact]
    public void Apply_ValidMove_AddsPieceToBoard()
    {
        // Arrange
        var board = new Connect4Board();
        var player = new MockPlayer();
        var move = new Connect4Move(0);

        // Act
        var result = move.Apply(board, player);

        // Assert
        Assert.Equal(move, result);
        Assert.False(board.IsEmpty());
    }

    [Fact]
    public void Apply_InvalidMove_ThrowsInvalidMoveException()
    {
        // Arrange
        var board = new Connect4Board();
        var player = new MockPlayer();
        var move = new Connect4Move(0);

        // Fill the column to make the move invalid
        for (var i = 0; i < board.Rows.Count; i++)
        {
            board.Insert(new Connect4Piece(player), 0);
        }

        // Act & Assert
        var exception = Assert.Throws<InvalidMoveException>(() => move.Apply(board, player));
        Assert.Equal(player, exception.Player);
        Assert.Equal(move, exception.Move);
    }
}

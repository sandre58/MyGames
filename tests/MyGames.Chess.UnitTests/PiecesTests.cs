// -----------------------------------------------------------------------
// <copyright file="PiecesTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyGames.Core;
using Xunit;

namespace MyGames.Chess.UnitTests;

public class PiecesTests
{
    [Fact]
    public void GetPossibleMoves_ShouldReturnCorrectMoves_ForWhitePawnAtStartingPosition()
    {
        // Arrange
        var board = new ChessBoard();
        var pawn = board.Whites.GetPawn(0);
        var from = new BoardCoordinates(board.Rows.Count - 2, 0); // Starting position for white pawn

        // Act
        var possibleMoves = pawn.GetPossibleMoves(board);

        // Assert
        Assert.Contains(new BoardCoordinates(from.Row - 1, from.Column), possibleMoves);
        Assert.Contains(new BoardCoordinates(from.Row - 2, from.Column), possibleMoves);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnCorrectMoves_ForBlackPawnAtStartingPosition()
    {
        // Arrange
        var board = new ChessBoard();
        var pawn = board.Blacks.GetPawn(0);
        var from = new BoardCoordinates(1, 0); // Starting position for black pawn

        // Act
        var possibleMoves = pawn.GetPossibleMoves(board);

        // Assert
        Assert.Contains(new BoardCoordinates(from.Row + 1, from.Column), possibleMoves);
        Assert.Contains(new BoardCoordinates(from.Row + 2, from.Column), possibleMoves);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnCorrectMoves_ForWhitePawnCapturing()
    {
        // Arrange
        var board = new ChessBoard();
        var whitePawn = board.Whites.GetPawn(0);
        var blackPawn = board.Blacks.GetPawn(0);
        var from = new BoardCoordinates(board.Rows.Count - 3, 1);
        var capturePosition = new BoardCoordinates(from.Row - 1, from.Column - 1);
        board.Move(whitePawn, from);
        board.Move(blackPawn, capturePosition);

        // Act
        var possibleMoves = whitePawn.GetPossibleMoves(board);

        // Assert
        Assert.Contains(capturePosition, possibleMoves);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnCorrectMoves_ForBlackPawnCapturing()
    {
        // Arrange
        var board = new ChessBoard();
        var whitePawn = board.Whites.GetPawn(0);
        var blackPawn = board.Blacks.GetPawn(0);
        var from = new BoardCoordinates(2, 1);
        var capturePosition = new BoardCoordinates(from.Row + 1, from.Column - 1);
        board.Move(blackPawn, from);
        board.Move(whitePawn, capturePosition);

        // Act
        var possibleMoves = blackPawn.GetPossibleMoves(board);

        // Assert
        Assert.Contains(capturePosition, possibleMoves);
    }

    [Fact]
    public void GetPossibleMoves_ShouldNotReturnInvalidMoves()
    {
        // Arrange
        var board = new ChessBoard();
        var pawn = board.Blacks.GetPawn(0);
        var from = new BoardCoordinates(board.Rows.Count - 2, 0); // Starting position for white pawn
        board.Move(pawn, from);

        // Act
        var possibleMoves = pawn.GetPossibleMoves(board);

        // Assert
        Assert.DoesNotContain(new BoardCoordinates(from.Row - 1, from.Column + 1), possibleMoves);
        Assert.DoesNotContain(new BoardCoordinates(from.Row - 2, from.Column + 1), possibleMoves);
    }
}

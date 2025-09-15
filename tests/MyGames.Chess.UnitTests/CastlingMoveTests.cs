// -----------------------------------------------------------------------
// <copyright file="CastlingMoveTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using MyGames.Chess.Exceptions;
using MyGames.Chess.Extensions;
using MyGames.Chess.Factories;
using MyGames.Chess.UnitTests.Mocks;
using MyGames.Core;
using MyNet.Utilities;
using Xunit;

namespace MyGames.Chess.UnitTests;

public class CastlingMoveTests
{
    private static ChessGame CreateGame(Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int Row, int Column)>>? newPiecesCreation = null)
    {
        var whitePlayer = new MockPlayer();
        var blackPlayer = new MockPlayer();
        var board = newPiecesCreation is not null ? ChessBoardFactory.Create(newPiecesCreation) : ChessBoardFactory.Create();
        return new ChessGame(board, whitePlayer, blackPlayer);
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenRookIsNull()
    {
        // Arrange
        var game = CreateGame();
        var castlingMove = CastlingMove.Short(game.Whites.King);
        game.Board.Remove(game.Whites.King);

        // Act
        var result = castlingMove.IsValid(game);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenKingHasMoved()
    {
        // Arrange
        var game = CreateGame();
        var castlingMove = CastlingMove.Short(game.Whites.King);
        game.Board.Move(game.Whites.King, new BoardCoordinates(4, 4));

        // Act
        var result = castlingMove.IsValid(game);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenRookHasMoved()
    {
        // Arrange
        var game = CreateGame();
        var castlingMove = CastlingMove.Short(game.Whites.King);
        game.Board.Move(game.Whites.RightRook, new BoardCoordinates(4, 4));

        // Act
        var result = castlingMove.IsValid(game);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenPiecesBetweenKingAndRook()
    {
        // Arrange
        var game = CreateGame();
        var castlingMove = CastlingMove.Short(game.Whites.King);

        // Act
        var result = castlingMove.IsValid(game);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenKingPassesThroughAttackedSquare()
    {
        // Arrange
        var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
        {
            { whites.King, (7, ChessBoardPiecesCollection.KingColumn) },
            { whites.RightRook, (7, ChessBoardPiecesCollection.RightRookColumn) },
            { blacks.GetPawn(0), (6, ChessBoardPiecesCollection.RightKnightColumn) },
        });
        var castlingMove = CastlingMove.Short(game.Whites.King);

        // Act
        var result = castlingMove.IsValid(game);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenAllConditionsMet()
    {
        // Arrange
        var game = CreateGame((whites, _) => new Dictionary<ChessPiece, (int, int)>()
        {
            { whites.King, (7, ChessBoardPiecesCollection.KingColumn) },
            { whites.RightRook, (7, ChessBoardPiecesCollection.RightRookColumn) },
        });
        var castlingMove = CastlingMove.Short(game.Whites.King);

        // Act
        var result = castlingMove.IsValid(game);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ShortCastlingMove_ShouldBeValid()
    {
        // Arrange
        var game = CreateGame();
        var castlingMove = CastlingMove.Short(game.Whites.King);
        var blackPawn = game.Blacks.GetPawn(0);
        game.Move(game.Whites.RightKnight, new BoardDirection(-2, -1)); // Move right knight
        game.Advance(blackPawn);
        game.Advance(game.Whites.GetPawn(6)); // Move pawn
        game.Advance(blackPawn);
        game.Move(game.Whites.RightBishop, BoardDirection.UpRight); // Move right bishop

        // Act
        var isValid = castlingMove.IsValid(game);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void LongCastlingMove_ShouldBeValid()
    {
        // Arrange
        var game = CreateGame();
        var castlingMove = CastlingMove.Long(game.Whites.King);
        var blackPawn = game.Blacks.GetPawn(0);
        game.Move(game.Whites.LeftKnight, new BoardDirection(-2, 1)); // Move left knight
        game.Advance(blackPawn);
        game.Advance(game.Whites.GetPawn(1)!.CastIn<Pawn>()); // Move pawn
        game.Advance(blackPawn);
        game.Move(game.Whites.LeftBishop, BoardDirection.UpLeft); // Move left bishop
        game.Advance(blackPawn);
        game.Advance(game.Whites.GetPawn(3)!.CastIn<Pawn>()); // Move pawn
        game.Advance(blackPawn);
        game.Move(game.Whites.Queen, BoardDirection.Up); // Move queen

        // Act
        var isValid = castlingMove.IsValid(game);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Apply_ShouldThrowException_WhenRookIsNull()
    {
        // Arrange
        var game = CreateGame((whites, _) => new Dictionary<ChessPiece, (int, int)>()
        {
            { whites.King, (7, ChessBoardPiecesCollection.KingColumn) },
        });
        var castlingMove = CastlingMove.Short(game.Whites.King);

        // Act & Assert
        Assert.Throws<ChessInvalidMoveException>(() => castlingMove.Apply(game.Board, game.WhitePlayer));
    }

    [Fact]
    public void ShortCastlingMove_ShouldApplyCorrectly()
    {
        // Arrange
        var game = CreateGame((whites, _) => new Dictionary<ChessPiece, (int, int)>()
        {
            { whites.King, (7, ChessBoardPiecesCollection.KingColumn) },
            { whites.RightRook, (7, ChessBoardPiecesCollection.RightRookColumn) },
        });
        var castlingMove = CastlingMove.Short(game.Whites.King);

        // Act
        castlingMove.Apply(game.Board, game.GetPlayer(ChessColor.White));

        // Assert
        Assert.Equal(game.Whites.King, game.Board.GetSquare(castlingMove.KingEndCoordinates).Piece);
        Assert.Equal(game.Whites.RightRook, game.Board.GetSquare(castlingMove.RookEndCoordinates).Piece);
    }

    [Fact]
    public void LongCastlingMove_ShouldApplyCorrectly()
    {
        // Arrange
        var game = CreateGame((whites, _) => new Dictionary<ChessPiece, (int, int)>()
        {
            { whites.King, (7, ChessBoardPiecesCollection.KingColumn) },
            { whites.LeftRook, (7, ChessBoardPiecesCollection.LeftRookColumn) },
        });
        var castlingMove = CastlingMove.Long(game.Whites.King);

        // Act
        castlingMove.Apply(game.Board, game.GetPlayer(ChessColor.White));

        // Assert
        Assert.Equal(game.Whites.King, game.Board.GetSquare(castlingMove.KingEndCoordinates).Piece);
        Assert.Equal(game.Whites.LeftRook, game.Board.GetSquare(castlingMove.RookEndCoordinates).Piece);
    }
}

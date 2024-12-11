// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using MyGames.Chess.Exceptions;
using MyGames.Chess.Extensions;
using MyGames.Chess.Factories;
using MyGames.Chess.UnitTests.Mocks;
using MyGames.Domain;
using Xunit;

namespace MyGames.Chess.UnitTests
{
    public class EnPassantCaptureMoveTests
    {
        private static ChessGame CreateGame(Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int row, int column)>>? newPiecesCreation = null)
        {
            var whitePlayer = new MockPlayer();
            var blackPlayer = new MockPlayer();
            var board = newPiecesCreation is not null ? ChessBoardFactory.Create(newPiecesCreation) : ChessBoardFactory.Create();
            return new ChessGame(board, whitePlayer, blackPlayer);
        }

        [Fact]
        public void IsValid_ShouldReturnFalseIfCaptureCoordinatesDoNotExist()
        {
            // Arrange
            var game = CreateGame();
            var pawn = game.Whites.GetPawn(0);
            var destination = new BoardCoordinates(3, 7);
            var move = new EnPassantCaptureMove(pawn, destination);

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalseIfCapturedPieceIsNotPawn()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
            {
                { whites.GetPawn(0), (3, 3) },
                { blacks.RightRook, (3, 4) },
            });
            var pawn = game.Whites.GetPawn(0);
            var destination = new BoardCoordinates(2, 4);
            var move = new EnPassantCaptureMove(pawn, destination);

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalseIfCapturedPieceIsSameColor()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
            {
                { whites.GetPawn(0), (3, 3) },
                { whites.GetPawn(1), (3, 4) },
            });
            var pawn = game.Whites.GetPawn(0);
            var destination = new BoardCoordinates(2, 4);
            var move = new EnPassantCaptureMove(pawn, destination);

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalseIfLastMoveWasNotDoublePawnMove()
        {
            // Arrange
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
            {
                { whites.GetPawn(0), (3, 3) },
                { blacks.GetPawn(1), (3, 4) },
            });
            var pawn = game.Whites.GetPawn(0);
            var destination = new BoardCoordinates(2, 4);
            var move = new EnPassantCaptureMove(pawn, destination);

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnTrueForValidEnPassantCaptureWhite()
        {
            // Arrange
            var game = CreateGame();
            var whitePawn = game.Whites.GetPawn(0);
            var blackPawn = game.Blacks.GetPawn(7);
            var destination = new BoardCoordinates(2, 1);
            var move = new EnPassantCaptureMove(whitePawn, destination);

            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Advance(whitePawn);
            game.Move(game.Blacks.GetPawn(1), new BoardDirection(2, 0));

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_ShouldReturnTrueForValidEnPassantCaptureBlack()
        {
            // Arrange
            var game = CreateGame();
            var blackPawn = game.Blacks.GetPawn(0);
            var whitePawn = game.Whites.GetPawn(7);
            var destination = new BoardCoordinates(5, 1);
            var move = new EnPassantCaptureMove(blackPawn, destination);

            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Move(game.Whites.GetPawn(1), new BoardDirection(-2, 0));

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Apply_ShouldThrowExceptionIfCaptureCoordinatesDoNotExist()
        {
            // Arrange
            var game = CreateGame();
            var pawn = game.Whites.GetPawn(0);
            var destination = new BoardCoordinates(3, 7);
            var move = new EnPassantCaptureMove(pawn, destination);

            // Act & Assert
            Assert.Throws<ChessInvalidMoveException>(() => move.Apply(game.Board, game.WhitePlayer));
        }

        [Fact]
        public void Apply_ShouldThrowExceptionIfCapturedPieceIsNotPawn()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
            {
                { whites.GetPawn(0), (3, 3) },
                { blacks.RightRook, (3, 4) },
            });
            var pawn = game.Whites.GetPawn(0);
            var destination = new BoardCoordinates(2, 4);
            var move = new EnPassantCaptureMove(pawn, destination);

            // Act & Assert
            Assert.Throws<ChessInvalidMoveException>(() => move.Apply(game.Board, game.WhitePlayer));
        }

        [Fact]
        public void Apply_ShouldReturnChessPlayedMoveForValidEnPassantCaptureWhite()
        {
            // Arrange
            var game = CreateGame();
            var whitePawn = game.Whites.GetPawn(0);
            var blackPawn = game.Blacks.GetPawn(7);
            var destination = new BoardCoordinates(2, 1);
            var move = new EnPassantCaptureMove(whitePawn, destination);

            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Advance(whitePawn);
            game.Move(game.Blacks.GetPawn(1), new BoardDirection(2, 0));

            // Act
            var result = move.Apply(game.Board, game.WhitePlayer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(whitePawn, result.Piece);
            Assert.Equal(new BoardCoordinates(3, 0), result.Start);
            Assert.Equal(destination, result.Destination);
            Assert.True(result.EnPassant);
            Assert.Equal(game.Blacks.GetPawn(1), result.TakenPiece);
        }

        [Fact]
        public void Apply_ShouldReturnChessPlayedMoveForValidEnPassantCapturBlacks()
        {
            // Arrange
            var game = CreateGame();
            var blackPawn = game.Blacks.GetPawn(0);
            var whitePawn = game.Whites.GetPawn(7);
            var destination = new BoardCoordinates(5, 1);
            var move = new EnPassantCaptureMove(blackPawn, destination);

            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Advance(whitePawn);
            game.Advance(blackPawn);
            game.Move(game.Whites.GetPawn(1), new BoardDirection(-2, 0));

            // Act
            var result = move.Apply(game.Board, game.BlackPlayer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(blackPawn, result.Piece);
            Assert.Equal(new BoardCoordinates(4, 0), result.Start);
            Assert.Equal(destination, result.Destination);
            Assert.True(result.EnPassant);
            Assert.Equal(game.Whites.GetPawn(1), result.TakenPiece);
        }
    }
}

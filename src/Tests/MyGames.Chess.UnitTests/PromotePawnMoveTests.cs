// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Chess.Factories;
using System.Collections.Generic;
using System;
using MyGames.Chess.UnitTests.Mocks;
using MyGames.Chess.Exceptions;
using MyGames.Domain;
using Xunit;

namespace MyGames.Chess.UnitTests
{
    public class PromotePawnMoveTests
    {
        private static ChessGame CreateGame(Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int row, int column)>>? newPiecesCreation = null)
        {
            var whitePlayer = new MockPlayer();
            var blackPlayer = new MockPlayer();
            var board = newPiecesCreation is not null ? ChessBoardFactory.Create(newPiecesCreation) : ChessBoardFactory.Create();
            return new ChessGame(board, whitePlayer, blackPlayer);
        }

        [Fact]
        public void Apply_ShouldReturnChessPlayedMoveForValidPromotion()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
            {
                { whites.GetPawn(0), (1, 3) },
            });
            var destination = new BoardCoordinates(0, 3);
            var move = new PromotePawnMove(game.Whites.GetPawn(0), destination, ExchangePiece.Queen);

            // Act
            var result = move.Apply(game.Board, game.WhitePlayer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(game.Whites.GetPawn(0), result.Piece);
            Assert.Equal(new BoardCoordinates(1, 3), result.Start);
            Assert.Equal(destination, result.Destination);
            Assert.True(result.IsPromotion);
            Assert.Equal(ExchangePiece.Queen, result.ExchangePiece);
        }

        [Fact]
        public void IsValid_ShouldReturnFalseIfNotEndOfBoard()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
            {
                { whites.GetPawn(0), (2, 0) },
            });
            var destination = new BoardCoordinates(1, 0);
            var move = new PromotePawnMove(game.Whites.GetPawn(0), destination, ExchangePiece.Queen);

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnFalseIfMoveResultsInCheck()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
            {
                { whites.GetPawn(0), (1, 0) },
                { whites.King, (7, 0) },
                { blacks.Queen, (0, 0) },
                { blacks.LeftRook, (0, 1) },
            });
            var destination = new BoardCoordinates(0, 3);
            var move = new PromotePawnMove(game.Whites.GetPawn(0), destination, ExchangePiece.Queen);

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ShouldReturnTrueForValidPromotionMove()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>()
            {
                { whites.GetPawn(0), (1, 3) },
            });
            var destination = new BoardCoordinates(0, 3);
            var move = new PromotePawnMove(game.Whites.GetPawn(0), destination, ExchangePiece.Queen);

            // Act
            var result = move.IsValid(game);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(ExchangePiece.Queen, typeof(Queen))]
        [InlineData(ExchangePiece.Rook, typeof(Rook))]
        [InlineData(ExchangePiece.Bishop, typeof(Bishop))]
        [InlineData(ExchangePiece.Knight, typeof(Knight))]
        public void CreateReplacementPiece_ShouldReturnCorrectPieceType(ExchangePiece exchangePiece, Type expectedType)
        {
            // Arrange
            var color = ChessColor.White;

            // Act
            var result = PromotePawnMove.CreateReplacementPiece(exchangePiece, color);

            // Assert
            Assert.IsType(expectedType, result);
            Assert.Equal(color, result.Color);
        }

        [Fact]
        public void CreateReplacementPiece_ShouldThrowNotSupportedExceptionForInvalidPiece()
        {
            // Arrange
            var invalidPiece = (ExchangePiece)999;
            var color = ChessColor.White;

            // Act & Assert
            Assert.Throws<NotSupportedException>(() => PromotePawnMove.CreateReplacementPiece(invalidPiece, color));
        }
    }
}

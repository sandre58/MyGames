// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using MyGames.Chess.Extensions;
using MyGames.Chess.Factories;
using MyGames.Chess.UnitTests.Mocks;
using MyGames.Domain;
using MyGames.Domain.Extensions;
using Xunit;

namespace MyGames.Chess.UnitTests
{
    public class MovesExtensionsTests
    {
        private static ChessGame CreateGame(Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int row, int column)>>? newPiecesCreation = null)
        {
            var whitePlayer = new MockPlayer();
            var blackPlayer = new MockPlayer();
            var board = newPiecesCreation is not null ? ChessBoardFactory.Create(newPiecesCreation) : ChessBoardFactory.Create();
            return new ChessGame(board, whitePlayer, blackPlayer);
        }

        [Fact]
        public void Pawn_Advance_ShouldMovePawnOneStepForward()
        {
            // Arrange
            var game = CreateGame();
            var pawn = game.Whites.GetPawn(0);

            // Act
            var result = game.Advance(pawn);

            // Assert
            var expectedPosition = new BoardCoordinates(5, 0);
            Assert.True(result);
            Assert.Equal(expectedPosition, game.Board.GetCoordinates(pawn));
        }

        [Fact]
        public void Pawn_Promote_ShouldPromotePawn()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.GetPawn(0), (1, 0) },
            });
            var exchangePiece = ExchangePiece.Queen;

            // Act
            var result = game.Promote(game.Whites.GetPawn(0), new BoardDirection(-1, 0), exchangePiece);

            // Assert
            var expectedPosition = new BoardCoordinates(0, 0);
            Assert.True(result);
            var promotedPiece = game.Board.GetPiece(expectedPosition);
            Assert.IsType<Queen>(promotedPiece);
            Assert.Equal(ChessColor.White, promotedPiece.Color);
        }

        [Fact]
        public void ChessPiece_Move_ShouldMovePieceInGivenDirection()
        {
            // Arrange
            var game = CreateGame((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.LeftRook, (7, 0) },
            });
            var rook = game.Whites.LeftRook;

            // Act
            var result = game.Move(rook, BoardDirection.Right);

            // Assert
            var expectedPosition = new BoardCoordinates(7, 1);
            Assert.True(result);
            Assert.Equal(expectedPosition, game.Board.GetCoordinates(rook));
        }
    }
}

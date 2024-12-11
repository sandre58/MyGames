// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using MyGames.Chess.Factories;
using MyGames.Chess.UnitTests.Mocks;
using MyGames.Domain;
using Xunit;

namespace MyGames.Chess.UnitTests
{
    public class ChessMoveTests
    {
        private static ChessGame CreateGame(Func<ChessBoardPiecesCollection, ChessBoardPiecesCollection, IDictionary<ChessPiece, (int row, int column)>>? newPiecesCreation = null)
        {
            var whitePlayer = new MockPlayer();
            var blackPlayer = new MockPlayer();
            var board = newPiecesCreation is not null ? ChessBoardFactory.Create(newPiecesCreation) : ChessBoardFactory.Create();
            return new ChessGame(board, whitePlayer, blackPlayer);
        }

        [Fact]
        public void Apply_ShouldMovePiece()
        {
            // Arrange
            var game = CreateGame();
            var player = new MockPlayer();
            var pawn = game.Whites.GetPawn(0);
            var destination = new BoardCoordinates(5, 0);
            var chessMove = new ChessMove(pawn, destination);

            // Act
            var playedMove = chessMove.Apply(game.Board, player);

            // Assert
            Assert.Equal(pawn, game.Board.GetSquare(destination).Piece);
            Assert.Equal(destination, playedMove.Destination);
        }

        [Fact]
        public void IsValid_ShouldReturnTrue_WhenMoveIsValid()
        {
            // Act
            var game = CreateGame();
            var pawn = game.Whites.GetPawn(0);
            var destination = new BoardCoordinates(5, 0);
            var chessMove = new ChessMove(pawn, destination);
            var isValid = chessMove.IsValid(game);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_ShouldReturnFalse_WhenMoveIsInvalid()
        {
            // Arrange
            var game = CreateGame();
            var pawn = game.Whites.GetPawn(0);
            var invalidDestination = new BoardCoordinates(7, 7);
            var invalidMove = new ChessMove(pawn, invalidDestination);

            // Act
            var isValid = invalidMove.IsValid(game);

            // Assert
            Assert.False(isValid);
        }
    }
}

// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Chess.Extensions;
using MyGames.Domain;
using Xunit;

namespace MyGames.Chess.UnitTests
{
    public class ChessBoardExtensionsTests
    {
        [Fact]
        public void GetAllPossibleMoves_ValidInput_ReturnsExpectedMoves()
        {
            // Arrange
            var board = new ChessBoard();
            var piece = board.Whites[0];
            var from = new BoardCoordinates(5, 5);
            board.Move(piece, from);
            var directions = new BoardDirectionOffset[]
            {
                new(BoardDirection.Up, 2),
                new(BoardDirection.Right, 2)
            };

            // Act
            var possibleMoves = board.GetAllPossibleMoves(from, piece.Color, directions);

            // Assert
            Assert.Contains(new BoardCoordinates(4, 5), possibleMoves);
            Assert.Contains(new BoardCoordinates(3, 5), possibleMoves);
            Assert.Contains(new BoardCoordinates(5, 6), possibleMoves);
            Assert.Contains(new BoardCoordinates(5, 7), possibleMoves);
        }

        [Fact]
        public void GetPieces_ValidInput_ReturnsExpectedPieces()
        {
            // Arrange
            var board = new ChessBoard();
            var piece1 = board.Whites[0];
            var piece2 = board.Whites[1];
            var piece3 = board.Whites[2];
            board.Move(piece1, new BoardCoordinates(0, 0));
            board.Move(piece2, new BoardCoordinates(1, 0));
            board.Move(piece3, new BoardCoordinates(2, 0));

            // Act
            var pieces = board.Whites;

            // Assert
            Assert.Contains(piece1, pieces);
            Assert.Contains(piece2, pieces);
            Assert.Contains(piece3, pieces);
        }

        [Fact]
        public void GetKing_ValidInput_ReturnsKing()
        {
            // Arrange
            var board = new ChessBoard();
            var king = new King(ChessColor.White);

            // Act
            var result = board.Whites.King;

            // Assert
            Assert.True(king.IsSimilar(result));
        }

        [Fact]
        public void CanMove_ValidMove_ReturnsTrue()
        {
            // Arrange
            var board = new ChessBoard();
            var piece = board.Whites.GetPawn(0);
            var from = new BoardCoordinates(5, 5);
            var to = new BoardCoordinates(4, 5);
            board.Move(piece, from);

            // Act
            var result = board.CanMove(piece, to);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void MakeCheckAfterMove_ValidMove_ReturnsExpectedResult()
        {
            // Arrange
            var board = new ChessBoard();
            var king = board.Whites.King;
            var opponentPiece = board.Blacks.GetPawn(0);
            board.Move(king, new BoardCoordinates(5, 5));
            board.Move(opponentPiece, new BoardCoordinates(3, 4));
            var move = new ChessMove(opponentPiece, new BoardCoordinates(4, 4));

            // Act
            var result = board.MakeCheckAfterMove(move);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCheckAfterMove_ValidMove_ReturnsExpectedResult()
        {
            // Arrange
            var board = new ChessBoard();
            var king = board.Whites.King;
            var opponentPiece = board.Blacks.GetPawn(0);
            board.Move(king, new BoardCoordinates(5, 5));
            board.Move(opponentPiece, new BoardCoordinates(3, 4));
            var move = new ChessMove(king, new BoardCoordinates(4, 5));

            // Act
            var result = board.IsCheckAfterMove(move);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanMove_ShouldReturnTrue_WhenPieceCanMoveToDirection()
        {
            // Arrange
            var board = new ChessBoard();
            var direction = BoardDirection.Up;

            // Act
            var result = ChessBoardExtensions.CanMove(board, board.Whites.GetPawn(0), direction);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanMove_ShouldReturnFalse_WhenPieceCannotMoveToDirection()
        {
            // Arrange
            var board = new ChessBoard();
            var direction = BoardDirection.Right;

            // Act
            var result = ChessBoardExtensions.CanMove(board, board.Whites.GetPawn(0), direction);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanMove_ShouldReturnFalse_WhenPieceCoordinatesNotFound()
        {
            // Arrange
            var board = new ChessBoard();
            var direction = new BoardDirection(-10, 0);

            // Act
            var result = ChessBoardExtensions.CanMove(board, board.Whites.GetPawn(0), direction);

            // Assert
            Assert.False(result);
        }
    }
}

// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyGames.Domain;
using Xunit;

namespace MyGames.Chess.UnitTests
{
    public class ChessBoardTests
    {
        [Fact]
        public void Constructor_InitializesBoardWithCorrectPieces()
        {
            // Arrange & Act
            var board = new ChessBoard();

            // Assert
            Assert.IsType<Rook>(board.GetSquare(new BoardCoordinates(0, 0)).Piece);
            Assert.IsType<Knight>(board.GetSquare(new BoardCoordinates(0, 1)).Piece);
            Assert.IsType<Bishop>(board.GetSquare(new BoardCoordinates(0, 2)).Piece);
            Assert.IsType<Queen>(board.GetSquare(new BoardCoordinates(0, 3)).Piece);
            Assert.IsType<King>(board.GetSquare(new BoardCoordinates(0, 4)).Piece);
            Assert.IsType<Bishop>(board.GetSquare(new BoardCoordinates(0, 5)).Piece);
            Assert.IsType<Knight>(board.GetSquare(new BoardCoordinates(0, 6)).Piece);
            Assert.IsType<Rook>(board.GetSquare(new BoardCoordinates(0, 7)).Piece);
            Assert.All(Enumerable.Range(0, 8), x => Assert.IsType<Pawn>(board.GetSquare(new BoardCoordinates(1, x)).Piece));

            Assert.IsType<Rook>(board.GetSquare(new BoardCoordinates(7, 0)).Piece);
            Assert.IsType<Knight>(board.GetSquare(new BoardCoordinates(7, 1)).Piece);
            Assert.IsType<Bishop>(board.GetSquare(new BoardCoordinates(7, 2)).Piece);
            Assert.IsType<Queen>(board.GetSquare(new BoardCoordinates(7, 3)).Piece);
            Assert.IsType<King>(board.GetSquare(new BoardCoordinates(7, 4)).Piece);
            Assert.IsType<Bishop>(board.GetSquare(new BoardCoordinates(7, 5)).Piece);
            Assert.IsType<Knight>(board.GetSquare(new BoardCoordinates(7, 6)).Piece);
            Assert.IsType<Rook>(board.GetSquare(new BoardCoordinates(7, 7)).Piece);
            Assert.All(Enumerable.Range(0, 8), x => Assert.IsType<Pawn>(board.GetSquare(new BoardCoordinates(6, x)).Piece));
        }

        [Fact]
        public void ChessBoard_GetPieces_ShouldReturnCorrectPieces()
        {
            // Arrange
            var chessBoard = new ChessBoard();

            // Act
            var whitePieces = chessBoard.GetPieces(ChessColor.White);
            var blackPieces = chessBoard.GetPieces(ChessColor.Black);

            // Assert
            Assert.NotNull(whitePieces);
            Assert.NotNull(blackPieces);
            Assert.Equal(16, whitePieces.Count); // Assuming standard chess setup
            Assert.Equal(16, blackPieces.Count); // Assuming standard chess setup
        }

        [Fact]
        public void ChessBoard_IsCheck_ShouldReturnFalseForNewBoard()
        {
            // Arrange
            var chessBoard = new ChessBoard();

            // Act
            var isWhiteInCheck = chessBoard.IsCheck(ChessColor.White);
            var isBlackInCheck = chessBoard.IsCheck(ChessColor.Black);

            // Assert
            Assert.False(isWhiteInCheck);
            Assert.False(isBlackInCheck);
        }

        [Fact]
        public void GetStartRowOf_ReturnsCorrectRow()
        {
            // Arrange & Act
            var whiteStartRow = ChessBoard.GetStartRowOf(ChessColor.White);
            var blackStartRow = ChessBoard.GetStartRowOf(ChessColor.Black);

            // Assert
            Assert.Equal(7, whiteStartRow);
            Assert.Equal(0, blackStartRow);
        }

        [Fact]
        public void Replace_ShouldReplaceOldPieceWithNewPiece()
        {
            // Arrange
            var chessBoard = new ChessBoard();
            var oldPiece = chessBoard.Whites.GetPawn(0);
            var newPiece = new Queen(ChessColor.White);
            var coordinates = chessBoard.GetCoordinates(oldPiece);

            // Act
            var result = chessBoard.Replace(oldPiece, newPiece);

            // Assert
            Assert.True(result);
            Assert.Equal(newPiece, chessBoard.GetSquare(coordinates).Piece);
            Assert.DoesNotContain(oldPiece, chessBoard.Whites);
            Assert.Contains(newPiece, chessBoard.Whites);
        }

        [Fact]
        public void Replace_ShouldReturnFalseIfOldPieceNotFound()
        {
            // Arrange
            var chessBoard = new ChessBoard();
            var oldPiece = new Pawn(ChessColor.White);
            var newPiece = new Queen(ChessColor.White);

            // Act
            var result = chessBoard.Replace(oldPiece, newPiece);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Replace_ShouldReturnFalseIfNewPieceCannotBeInserted()
        {
            // Arrange
            var chessBoard = new ChessBoard();
            var oldPiece = chessBoard.Whites.GetPawn(0);
            var newPiece = new Queen(ChessColor.White);

            // Simulate that the new piece cannot be inserted by removing the old piece first
            chessBoard.Remove(oldPiece);

            // Act
            var result = chessBoard.Replace(oldPiece, newPiece);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Replace_ShouldMaintainBoardStateIfReplacementFails()
        {
            // Arrange
            var chessBoard = new ChessBoard();
            var oldPiece = chessBoard.Whites.GetPawn(0);
            var newPiece = new Queen(ChessColor.White);
            var coordinates = new BoardCoordinates(6, 0);

            // Simulate that the new piece cannot be inserted by removing the old piece first
            chessBoard.Remove(oldPiece);

            // Act
            var result = chessBoard.Replace(oldPiece, newPiece);

            // Assert
            Assert.False(result);
            Assert.Throws<InvalidOperationException>(() => chessBoard.GetSquare(coordinates).Piece);
            Assert.DoesNotContain(oldPiece, chessBoard.Whites);
            Assert.DoesNotContain(newPiece, chessBoard.Whites);
        }

        [Fact]
        public void Move_MovesPieceCorrectly()
        {
            // Arrange
            var board = new ChessBoard();
            var piece = board.GetSquare(new BoardCoordinates(1, 0)).Piece;
            var targetCoordinates = new BoardCoordinates(2, 0);

            // Act
            var result = board.Move(piece, targetCoordinates);

            // Assert
            Assert.True(result);
            Assert.Equal(piece, board.GetSquare(targetCoordinates).Piece);
        }

        [Fact]
        public void Remove_RemovesPieceCorrectly()
        {
            // Arrange
            var board = new ChessBoard();
            var piece = board.GetSquare(new BoardCoordinates(1, 0)).Piece;

            // Act
            var result = board.Remove(piece);

            // Assert
            Assert.True(result);
            Assert.True(board.GetSquare(new BoardCoordinates(1, 0)).IsEmpty);
        }

        [Fact]
        public void IsCheck_ReturnsCorrectResult()
        {
            // Arrange
            var board = new ChessBoard();

            // Act
            var result = board.IsCheck(ChessColor.Black);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsCheckmate_ShouldReturnTrue_WhenKingIsInCheckmate()
        {
            // Arrange
            var chessBoard = new ChessBoard((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.Queen, (1, 1) },
                { blacks.LeftRook, (0, 1) }
            });

            // Act
            var result = chessBoard.IsCheckmate(ChessColor.White);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsStalemate_ShouldReturnTrue_WhenKingIsInStalemate()
        {
            // Arrange
            var chessBoard = new ChessBoard((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.King, (2, 1) },
                { blacks.Queen, (1, 2) }
            });

            // Act
            var result = chessBoard.IsStalemate(ChessColor.White);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsStalemate_ShouldReturnFalse_WhenKingIsNotInStalemate()
        {
            // Arrange
            var chessBoard = new ChessBoard((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.King, (7, 7) },
                { blacks.LeftRook, (1, 1) }
            });

            // Act
            var result = chessBoard.IsStalemate(ChessColor.White);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsStalemate_ShouldReturnFalse_WhenKingIsInCheck()
        {
            // Arrange
            var chessBoard = new ChessBoard((whites, blacks) => new Dictionary<ChessPiece, (int, int)>
            {
                { whites.King, (0, 0) },
                { blacks.King, (7, 7) },
                { blacks.LeftRook, (0, 1) }
            });

            // Act
            var result = chessBoard.IsStalemate(ChessColor.White);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEnd_ReturnsCorrectResult()
        {
            // Arrange
            var coordinates = new BoardCoordinates(0, 0);

            // Act
            var result = ChessBoard.IsEnd(coordinates, ChessColor.White);

            // Assert
            Assert.True(result);
        }
    }
}

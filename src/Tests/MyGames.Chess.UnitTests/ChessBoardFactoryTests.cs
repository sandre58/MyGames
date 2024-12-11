// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using MyGames.Chess.Factories;
using MyGames.Domain;
using Xunit;

namespace MyGames.Chess.UnitTests
{
    public class ChessBoardFactoryTests
    {
        [Fact]
        public void Empty_ShouldReturnEmptyChessBoard()
        {
            // Act
            var chessBoard = ChessBoardFactory.Empty();

            // Assert
            Assert.NotNull(chessBoard);
            Assert.Empty(chessBoard.GetPieces(ChessColor.White));
            Assert.Empty(chessBoard.GetPieces(ChessColor.Black));
        }

        [Fact]
        public void Create_ShouldReturnDefaultChessBoard()
        {
            // Act
            var chessBoard = ChessBoardFactory.Create();

            // Assert
            Assert.NotNull(chessBoard);
            Assert.NotEmpty(chessBoard.GetPieces(ChessColor.White));
            Assert.NotEmpty(chessBoard.GetPieces(ChessColor.Black));
        }

        [Fact]
        public void Create_WithCustomPieces_ShouldReturnChessBoardWithCustomPieces()
        {
            // Arrange
            static IDictionary<ChessPiece, (int row, int column)> customPiecesCreation(ChessBoardPiecesCollection whites, ChessBoardPiecesCollection blacks)
            {
                var pieces = new Dictionary<ChessPiece, (int row, int column)>
                {
                    { whites.King, (7, 4) },
                    { blacks.King, (0, 4) }
                };
                return pieces;
            }

            // Act
            var chessBoard = ChessBoardFactory.Create(customPiecesCreation);

            // Assert
            Assert.NotNull(chessBoard);
            var whiteKing = chessBoard.GetSquare(new BoardCoordinates(7, 4)).Piece;
            var blackKing = chessBoard.GetSquare(new BoardCoordinates(0, 4)).Piece;
            Assert.IsType<King>(whiteKing);
            Assert.IsType<King>(blackKing);
        }

        [Fact]
        public void Create_WithMissingKings_ShouldAddKingsToDefaultPositions()
        {
            // Arrange
            static IDictionary<ChessPiece, (int row, int column)> customPiecesCreation(ChessBoardPiecesCollection whites, ChessBoardPiecesCollection blacks)
            {
                return new Dictionary<ChessPiece, (int row, int column)>();
            }

            // Act
            var chessBoard = ChessBoardFactory.Create(customPiecesCreation);

            // Assert
            Assert.NotNull(chessBoard);
            var whiteKing = chessBoard.GetSquare(new BoardCoordinates(7, ChessBoardPiecesCollection.KingColumn)).Piece;
            var blackKing = chessBoard.GetSquare(new BoardCoordinates(0, ChessBoardPiecesCollection.KingColumn)).Piece;
            Assert.IsType<King>(whiteKing);
            Assert.IsType<King>(blackKing);
        }
    }
}

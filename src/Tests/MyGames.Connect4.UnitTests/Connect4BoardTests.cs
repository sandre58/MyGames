// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;
using MyGames.Connect4.UnitTests.Mocks;
using Xunit;

namespace MyGames.Connect4.UnitTests
{
    public class Connect4BoardTests
    {
        [Fact]
        public void AddPiece_ShouldAddPieceToColumn()
        {
            // Arrange
            var board = new Connect4Board();
            var player = new MockPlayer();
            var piece = new Connect4Piece(player);

            // Act
            var result = board.Insert(piece, 0);

            // Assert
            Assert.True(result);
            Assert.Equal(piece, board.GetColumn(0)[board.GetColumn(0).Count - 1].Piece);
        }

        [Fact]
        public void RemovePiece_ShouldRemovePieceFromColumn()
        {
            // Arrange
            var board = new Connect4Board();
            var player = new MockPlayer();
            var piece = new Connect4Piece(player);
            board.Insert(piece, 0);

            // Act
            var result = board.Remove(0);

            // Assert
            Assert.True(result);
            Assert.True(board.GetColumn(0).GetSquare(0)?.IsEmpty);
        }

        [Fact]
        public void Remove_ShouldReturnFalseIfColumnIsEmpty()
        {
            // Arrange
            var board = new Connect4Board();
            var columnIndex = 0;

            // Act
            var result = board.Remove(columnIndex);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Remove_ShouldReturnTrueAndRemovePieceIfColumnIsNotEmpty()
        {
            // Arrange
            var board = new Connect4Board();
            var player = new MockPlayer();
            var piece = new Connect4Piece(player);
            var columnIndex = 0;
            board.Insert(piece, columnIndex);

            // Act
            var result = board.Remove(columnIndex);

            // Assert
            Assert.True(result);
            Assert.True(board.GetColumn(columnIndex).GetSquare(5)!.IsEmpty);
        }

        [Fact]
        public void Remove_ShouldRemoveTopPieceFromColumn()
        {
            // Arrange
            var board = new Connect4Board();
            var player = new MockPlayer();
            var piece1 = new Connect4Piece(player);
            var piece2 = new Connect4Piece(player);
            var columnIndex = 0;
            board.Insert(piece1, columnIndex);
            board.Insert(piece2, columnIndex);

            // Act
            var result = board.Remove(columnIndex);

            // Assert
            Assert.True(result);
            Assert.True(board.GetColumn(columnIndex).GetSquare(4)!.IsEmpty);
            Assert.False(board.GetColumn(columnIndex).GetSquare(5)!.IsEmpty);
        }

        [Fact]
        public void GetValidColumns_ShouldReturnNonFullColumns()
        {
            // Arrange
            var board = new Connect4Board();
            var player = new MockPlayer();
            var piece = new Connect4Piece(player);

            // Fill one column
            for (var i = 0; i < Connect4Board.DefaultRows; i++)
                board.Insert(piece, 0);

            // Act
            var validColumns = board.GetValidColumns().ToList();

            // Assert
            Assert.DoesNotContain(validColumns, col => col.Index == 0);
            Assert.Contains(validColumns, col => col.Index == 1);
        }
    }
}

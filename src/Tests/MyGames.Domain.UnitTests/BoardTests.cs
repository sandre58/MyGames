// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using MyGames.Domain.Extensions;
using MyGames.Domain.UnitTests.Mocks;
using Xunit;

namespace MyGames.Domain.UnitTests
{
    public class BoardTests
    {
        [Fact]
        public void Constructor_ShouldInitializeBoardWithCorrectDimensions()
        {
            // Arrange & Act
            var board = new MockBoard(6, 7);

            // Assert
            Assert.Equal(6, board.Rows.Count);
            Assert.Equal(7, board.Columns.Count);
        }

        [Fact]
        public void GetSquare_ShouldReturnCorrectSquare()
        {
            // Arrange
            var board = new MockBoard(6, 7);
            var coordinates = new BoardCoordinates(0, 0);

            // Act
            var square = board.GetSquare(coordinates);

            // Assert
            Assert.NotNull(square);
            Assert.Equal(coordinates, square.Coordinates);
        }

        [Fact]
        public void GetRow_ShouldReturnCorrectRow()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);
            board.InsertPiece(piece, coordinates);

            // Act
            var row = board.GetRow(0);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(8, row.Count);
            Assert.Equal(piece, row[0].Piece);
        }


        [Fact]
        public void GetRow_ShouldReturnEmptyRowIfNoPieces()
        {
            // Arrange
            var board = new MockBoard(8, 8);

            // Act
            var row = board.GetRow(0);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(8, row.Count);
            Assert.All(row, square => Assert.True(square.IsEmpty));
        }

        [Fact]
        public void GetRow_ShouldReturnNewRowIfRowDoesNotExist()
        {
            // Arrange
            var board = new MockBoard(8, 8);

            // Act
            var row = board.GetRow(10);

            // Assert
            Assert.NotNull(row);
            Assert.Empty(row);
        }

        [Fact]
        public void TryGetSquare_ShouldReturnSquareIfPieceExists()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);
            board.InsertPiece(piece, coordinates);

            // Act
            var square = board.TryGetSquare(piece);

            // Assert
            Assert.NotNull(square);
            Assert.Equal(piece, square?.Piece);
        }

        [Fact]
        public void TryGetSquare_ShouldReturnNullIfPieceDoesNotExist()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece = new MockPiece();

            // Act
            var square = board.TryGetSquare(piece);

            // Assert
            Assert.Null(square);
        }

        [Fact]
        public void TryGetSquare_ShouldReturnNullForInvalidCoordinates()
        {
            var board = new MockBoard(8, 8);
            var coordinates = new BoardCoordinates(8, 8);
            var square = board.TryGetSquare(coordinates);

            Assert.Null(square);
        }


        [Fact]
        public void TryGetCoordinates_ShouldReturnCoordinatesIfPieceExists()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);
            board.InsertPiece(piece, coordinates);

            // Act
            var result = board.TryGetCoordinates(piece);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(coordinates, result);
        }

        [Fact]
        public void TryGetCoordinates_ShouldReturnNullIfPieceDoesNotExist()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece = new MockPiece();

            // Act
            var result = board.TryGetCoordinates(piece);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Exists_ShouldReturnTrueIfPieceExists()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);
            board.InsertPiece(piece, coordinates);

            // Act
            var result = board.Exists(piece);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Exists_ShouldReturnFalseIfPieceDoesNotExist()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece = new MockPiece();

            // Act
            var result = board.Exists(piece);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Exists_ShouldReturnTrueForValidCoordinates()
        {
            var board = new MockBoard(8, 8);
            var coordinates = new BoardCoordinates(0, 0);
            var exists = board.Exists(coordinates);

            Assert.True(exists);
        }

        [Fact]
        public void Exists_ShouldReturnFalseForInvalidCoordinates()
        {
            var board = new MockBoard(8, 8);
            var coordinates = new BoardCoordinates(8, 8);
            var exists = board.Exists(coordinates);

            Assert.False(exists);
        }

        [Fact]
        public void IsEmpty_ShouldReturnTrueForEmptySquare()
        {
            var board = new MockBoard(8, 8);
            var coordinates = new BoardCoordinates(0, 0);
            var isEmpty = board.IsEmpty(coordinates);

            Assert.True(isEmpty);
        }

        [Fact]
        public void IsEmpty_ShouldReturnFalseForNonEmptySquare()
        {
            var board = new MockBoard(8, 8);
            var coordinates = new BoardCoordinates(0, 0);
            var piece = new MockPiece();
            board.InsertPiece(piece, coordinates);

            var isEmpty = board.IsEmpty(coordinates);

            Assert.False(isEmpty);
        }

        [Fact]
        public void IsEmpty_ShouldReturnFalse_WhenCoordinatesDoNotExist()
        {
            // Arrange
            var board = new MockBoard(3, 3);
            var coordinates = new BoardCoordinates(5, 5);

            // Act
            var result = board.IsEmpty(coordinates);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsFull_ShouldReturnFalseIfNotAllSquaresAreFull()
        {
            // Arrange
            var board = new MockBoard(6, 7);

            // Act
            var isFull = board.IsFull();

            // Assert
            Assert.False(isFull);
        }

        [Fact]
        public void InsertPiece_ShouldPlacePieceAtCorrectCoordinates()
        {
            // Arrange
            var board = new MockBoard(6, 7);
            var piece = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);

            // Act
            var result = board.InsertPiece(piece, coordinates);

            // Assert
            Assert.True(result);
            Assert.Equal(piece, board.GetPiece(coordinates.Row, coordinates.Column));
        }


        [Fact]
        public void InsertPiece_ShouldNotReplacePieceIfReplaceIfTakenIsFalse()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece1 = new MockPiece();
            var piece2 = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);
            board.InsertPiece(piece1, coordinates);

            // Act
            var result = board.InsertPiece(piece2, coordinates, replaceIfTaken: false);

            // Assert
            Assert.False(result);
            Assert.Equal(piece1, board.GetSquare(coordinates).Piece);
        }

        [Fact]
        public void RemovePiece_ShouldClearPieceFromCoordinates()
        {
            // Arrange
            var board = new MockBoard(6, 7);
            var piece = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);
            board.InsertPiece(piece, coordinates);

            // Act
            var result = board.RemovePiece(coordinates);

            // Assert
            Assert.True(result);
            Assert.Null(board.TryGetPiece(coordinates));
        }

        [Fact]
        public void RemovePiece_ShouldReturnFalseIfCoordinatesAreEmpty()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var coordinates = new BoardCoordinates(0, 0);

            // Act
            var result = board.RemovePiece(coordinates);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemovePiece_ShouldRemovePieceByPieceInstance()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var piece = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);
            board.InsertPiece(piece, coordinates);

            // Act
            var result = board.RemovePiece(piece);

            // Assert
            Assert.True(result);
            Assert.True(board.GetSquare(coordinates).IsEmpty);
        }

        [Fact]
        public void MovePiece_ShouldMovePieceToNewCoordinates()
        {
            // Arrange
            var board = new MockBoard(6, 7);
            var piece = new MockPiece();
            var initialCoordinates = new BoardCoordinates(0, 0);
            var newCoordinates = new BoardCoordinates(1, 1);
            board.InsertPiece(piece, initialCoordinates);

            // Act
            var result = board.MovePiece(piece, newCoordinates);

            // Assert
            Assert.True(result);
            Assert.Null(board.TryGetPiece(initialCoordinates));
            Assert.Equal(piece, board.GetPiece(newCoordinates));
        }

        [Fact]
        public void Clone_ShouldCreateExactCopyOfBoard()
        {
            // Arrange
            var board = new MockBoard(6, 7);
            var piece = new MockPiece();
            var coordinates = new BoardCoordinates(0, 0);
            board.InsertPiece(piece, coordinates);

            // Act
            var clone = board.Clone();

            // Assert
            Assert.NotSame(board, clone);
            Assert.Equal(piece, clone.GetPiece(coordinates));
        }

        [Fact]
        public void IsSimilar_SameBoard_ReturnsTrue()
        {
            // Arrange
            var board1 = new MockBoard(6, 7);
            var board2 = new MockBoard(6, 7);

            // Act
            var result = board1.IsSimilar(board2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsSimilar_DifferentBoard_ReturnsFalse()
        {
            // Arrange
            var board1 = new MockBoard(8, 9);
            var board2 = new MockBoard(6, 7);

            // Act
            var result = board1.IsSimilar(board2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsSimilar_DifferentBoard2_ReturnsFalse()
        {
            // Arrange
            var board1 = new MockBoard(4, 5);
            var board2 = new MockBoard(6, 7);

            // Act
            var result = board1.IsSimilar(board2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsSimilar_InsertPiece_ReturnsFalse()
        {
            // Arrange
            var board1 = new MockBoard(6, 7);
            var board2 = new MockBoard(6, 7);
            board2.InsertPiece(new MockPiece(), new BoardCoordinates(0, 0));

            // Act
            var result = board1.IsSimilar(board2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsSimilar_NullBoard_ReturnsFalse()
        {
            // Arrange
            var board = new MockBoard(6, 7);

            // Act
            var result = board.IsSimilar(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsSimilar_NewBoard_ReturnsTrue()
        {
            // Arrange
            var board = new MockBoard(6, 7);
            var newBoard = new MockBoard(board);

            // Act
            var result = newBoard.IsSimilar(board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void SetFrom_ShouldCopyPiecesFromAnotherBoard()
        {
            // Arrange
            var board1 = new MockBoard(3, 3);
            var piece1 = new MockPiece();
            var piece2 = new MockPiece();
            board1.InsertPiece(piece1, new BoardCoordinates(0, 0));
            board1.InsertPiece(piece2, new BoardCoordinates(1, 1));

            var board2 = new MockBoard(3, 3);

            // Act
            board2.SetFrom(board1);

            // Assert
            Assert.True(board2.Exists(piece1));
            Assert.True(board2.Exists(piece2));
            Assert.Equal(new BoardCoordinates(0, 0), board2.GetCoordinates(piece1));
            Assert.Equal(new BoardCoordinates(1, 1), board2.GetCoordinates(piece2));
        }

        [Fact]
        public void SetFrom_ShouldClearExistingPiecesBeforeCopying()
        {
            // Arrange
            var board1 = new MockBoard(3, 3);
            var piece1 = new MockPiece();
            board1.InsertPiece(piece1, new BoardCoordinates(0, 0));

            var board2 = new MockBoard(3, 3);
            var piece2 = new MockPiece();
            board2.InsertPiece(piece2, new BoardCoordinates(1, 1));

            // Act
            board2.SetFrom(board1);

            // Assert
            Assert.True(board2.Exists(piece1));
            Assert.False(board2.Exists(piece2));
            Assert.Equal(new BoardCoordinates(0, 0), board2.GetCoordinates(piece1));
        }
    }
}

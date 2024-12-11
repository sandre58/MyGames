// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;
using MyGames.Domain.Extensions;
using MyGames.Domain.UnitTests.Mocks;
using Xunit;

namespace MyGames.Domain.UnitTests
{
    public class BoardExtensionsTests
    {
        [Fact]
        public void GetPiece_ShouldReturnCorrectPiece()
        {
            // Arrange
            var board = new MockBoard(3, 3);
            var piece = new MockPiece();
            board.InsertPiece(piece, new(1, 1));

            // Act
            var result = board.TryGetPiece(1, 1);

            // Assert
            Assert.Equal(piece, result);
        }

        [Fact]
        public void GetNotEmptySquares_ShouldReturnOnlyNotEmptySquares()
        {
            // Arrange
            var board = new MockBoard(3, 3);
            var piece = new MockPiece();
            board.InsertPiece(piece, new(0, 0));
            board.InsertPiece(piece, new(1, 1));

            // Act
            var notEmptySquares = board.GetNotEmptySquares().ToList();

            // Assert
            Assert.Equal(2, notEmptySquares.Count);
            Assert.All(notEmptySquares, square => Assert.False(square.IsEmpty));
        }

        [Fact]
        public void GetEmptySquares_ShouldReturnOnlyEmptySquares()
        {
            // Arrange
            var board = new MockBoard(3, 3);
            var piece = new MockPiece();
            board.InsertPiece(piece, new(0, 0));
            board.InsertPiece(piece, new(1, 1));

            // Act
            var emptySquares = board.GetEmptySquares().ToList();

            // Assert
            Assert.Equal(7, emptySquares.Count);
            Assert.All(emptySquares, square => Assert.True(square.IsEmpty));
        }

        [Fact]
        public void GetSquaresBetween_IncludesStartAndEnd()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var start = new BoardCoordinates(0, 0);
            var end = new BoardCoordinates(0, 3);

            // Act
            var result = board.GetSquaresBetween(start, end, includeStart: true, includeEnd: true);

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Equal(start, result[0].Coordinates);
            Assert.Equal(end, result[result.Count - 1].Coordinates);
        }

        [Fact]
        public void GetSquaresBetween_ExcludesStartAndEnd()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var start = new BoardCoordinates(0, 0);
            var end = new BoardCoordinates(0, 3);

            // Act
            var result = board.GetSquaresBetween(start, end, includeStart: false, includeEnd: false);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(new BoardCoordinates(0, 1), result[0].Coordinates);
            Assert.Equal(new BoardCoordinates(0, 2), result[result.Count - 1].Coordinates);
        }

        [Fact]
        public void GetSquaresBetween_EndUpperThanStart()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var start = new BoardCoordinates(0, 3);
            var end = new BoardCoordinates(0, 0);

            // Act
            var result = board.GetSquaresBetween(start, end, includeStart: true, includeEnd: true);

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Equal(start, result[0].Coordinates);
            Assert.Equal(end, result[result.Count - 1].Coordinates);
        }

        [Fact]
        public void GetSquaresBetween_DiagonalPath()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var start = new BoardCoordinates(0, 0);
            var end = new BoardCoordinates(3, 3);

            // Act
            var result = board.GetSquaresBetween(start, end, includeStart: true, includeEnd: true);

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Equal(start, result[0].Coordinates);
            Assert.Equal(end, result[result.Count - 1].Coordinates);
        }

        [Fact]
        public void GetSquaresBetween_DiagonalInversePath()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var end = new BoardCoordinates(0, 0);
            var start = new BoardCoordinates(3, 3);

            // Act
            var result = board.GetSquaresBetween(start, end, includeStart: true, includeEnd: true);

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Equal(start, result[0].Coordinates);
            Assert.Equal(end, result[result.Count - 1].Coordinates);
        }

        [Fact]
        public void GetSquaresBetween_CalculatesCorrectRowDirection()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var start = new BoardCoordinates(1, 1);
            var end = new BoardCoordinates(3, 1);

            // Act
            var result = board.GetSquaresBetween(start, end, includeStart: true, includeEnd: true);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(start, result[0].Coordinates);
            Assert.Equal(end, result[result.Count - 1].Coordinates);
        }

        [Fact]
        public void GetSquaresBetween_CalculatesCorrectColumnDirection()
        {
            // Arrange
            var board = new MockBoard(8, 8);
            var start = new BoardCoordinates(1, 1);
            var end = new BoardCoordinates(1, 3);

            // Act
            var result = board.GetSquaresBetween(start, end, includeStart: true, includeEnd: true);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(start, result[0].Coordinates);
            Assert.Equal(end, result[result.Count - 1].Coordinates);
        }
    }
}

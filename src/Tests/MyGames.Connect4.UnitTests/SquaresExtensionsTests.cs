// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyGames.Connect4.Extensions;
using MyGames.Connect4.UnitTests.Mocks;
using MyGames.Domain;
using Xunit;

namespace MyGames.Connect4.UnitTests
{
    public class SquaresExtensionsTests
    {
        private class TestPiece : IPiece
        {
            public bool IsSimilar(IPiece? obj) => obj is TestPiece;
        }

        [Fact]
        public void GetNextRow_ShouldReturnLastEmptyRow()
        {
            // Arrange
            var board = new Connect4Board();

            // Act
            var result = board.GetColumn(0).GetNextRow();

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void GetNextRow_ShouldReturnMinusOneIfColumnIsFull()
        {
            // Arrange
            var board = new Connect4Board();
            board.Insert(new Connect4Piece(new MockPlayer()), 0);
            board.Insert(new Connect4Piece(new MockPlayer()), 0);
            board.Insert(new Connect4Piece(new MockPlayer()), 0);
            board.Insert(new Connect4Piece(new MockPlayer()), 0);
            board.Insert(new Connect4Piece(new MockPlayer()), 0);
            board.Insert(new Connect4Piece(new MockPlayer()), 0);

            // Act
            var result = board.GetColumn(0).GetNextRow();

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void GetNextRow_ShouldReturnCorrectRowWhenPartiallyFilled()
        {
            // Arrange
            var board = new Connect4Board();
            board.Insert(new Connect4Piece(new MockPlayer()), 0);
            board.Insert(new Connect4Piece(new MockPlayer()), 0);

            // Act
            var result = board.GetColumn(0).GetNextRow();

            // Assert
            Assert.Equal(3, result);
        }
    }
}

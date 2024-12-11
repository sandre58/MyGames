// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace MyGames.Domain.UnitTests
{
    public class BoardCoordinatesTests
    {
        [Fact]
        public void Operator_Equality_ShouldReturnTrueForEqualCoordinates()
        {
            // Arrange
            var coordinates1 = new BoardCoordinates(1, 1);
            var coordinates2 = new BoardCoordinates(1, 1);

            // Act & Assert
            Assert.True(coordinates1 == coordinates2);
        }

        [Fact]
        public void Operator_Inequality_ShouldReturnTrueForDifferentCoordinates()
        {
            // Arrange
            var coordinates1 = new BoardCoordinates(1, 1);
            var coordinates2 = new BoardCoordinates(2, 2);

            // Act & Assert
            Assert.True(coordinates1 != coordinates2);
        }

        [Fact]
        public void Operator_Inequality_ShouldReturnTrueForDifferentRow()
        {
            // Arrange
            var coordinates1 = new BoardCoordinates(1, 1);
            var coordinates2 = new BoardCoordinates(2, 1);

            // Act & Assert
            Assert.True(coordinates1 != coordinates2);
        }

        [Fact]
        public void Operator_Inequality_ShouldReturnTrueForDifferentColumn()
        {
            // Arrange
            var coordinates1 = new BoardCoordinates(1, 1);
            var coordinates2 = new BoardCoordinates(1, 2);

            // Act & Assert
            Assert.True(coordinates1 != coordinates2);
        }

        [Fact]
        public void Operator_Subtraction_ShouldReturnCorrectDirection()
        {
            // Arrange
            var coordinates1 = new BoardCoordinates(3, 3);
            var coordinates2 = new BoardCoordinates(1, 1);

            // Act
            var result = coordinates1 - coordinates2;

            // Assert
            Assert.Equal(new BoardDirection(2, 2), result);
        }

        [Fact]
        public void Operator_AdditionWithDirection_ShouldReturnCorrectCoordinates()
        {
            // Arrange
            var coordinates = new BoardCoordinates(1, 1);
            var direction = new BoardDirection(2, 2);

            // Act
            var result = coordinates + direction;

            // Assert
            Assert.Equal(new BoardCoordinates(3, 3), result);
        }

        [Fact]
        public void Operator_SubtractionWithDirection_ShouldReturnCorrectCoordinates()
        {
            // Arrange
            var coordinates = new BoardCoordinates(3, 3);
            var direction = new BoardDirection(1, 1);

            // Act
            var result = coordinates - direction;

            // Assert
            Assert.Equal(new BoardCoordinates(2, 2), result);
        }

        [Fact]
        public void Equals_ShouldReturnTrueForEqualCoordinates()
        {
            // Arrange
            var coordinates1 = new BoardCoordinates(1, 1);
            var coordinates2 = new BoardCoordinates(1, 1);

            // Act & Assert
            Assert.True(coordinates1.Equals(coordinates2));
        }

        [Fact]
        public void Equals_ShouldReturnFalseForDifferentCoordinates()
        {
            // Arrange
            var coordinates1 = new BoardCoordinates(1, 1);
            var coordinates2 = new BoardCoordinates(2, 2);

            // Act & Assert
            Assert.False(coordinates1.Equals(coordinates2));
        }

        [Fact]
        public void Equals_ShouldReturnFalseForNullCoordinates()
        {
            // Arrange
            var coordinates1 = new BoardCoordinates(1, 1);

            // Act & Assert
            Assert.False(coordinates1.Equals(null));
        }
    }
}

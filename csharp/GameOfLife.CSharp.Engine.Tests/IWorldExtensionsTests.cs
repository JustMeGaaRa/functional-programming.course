using System;
using Xunit;

namespace GameOfLife.CSharp.Engine.Tests
{
    public class IWorldExtensionsTests
    {
        [Fact]
        public void IsValidIndex_WithNullWorld_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => IWorldExtensions.IsValidIndex(null, 0, 0));
        }

        [Theory]
        [InlineData(10, 10, 3, 3, true)]
        [InlineData(7, 9, 3, 3, true)]
        [InlineData(5, 5, 10, 3, false)]
        [InlineData(5, 5, 3, 10, false)]
        public void IsValidIndex_WithWorldSize10_ShouldReturnTrue(int width, int height, int column, int row, bool expected)
        {
            // Arrange
            IWorld world = World.FromSize(width, height);

            // Act
            bool actual = IWorldExtensions.IsValidIndex(world, row, column);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsInBounds_WithNullWorld_ShouldThrowArgumentNullException()
        {
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => IWorldExtensions.IsInBounds(null, 0, 0));
        }

        [Theory]
        [InlineData(10, 10, 3, 3, false)]
        [InlineData(7, 9, 3, 3, false)]
        [InlineData(7, 9, 10, 10, true)]
        [InlineData(5, 5, 15, 10, false)]
        [InlineData(5, 5, 10, 15, false)]
        public void IsInBounds_WithWorldSize10_ShouldReturnTrue(int width, int height, int column, int row, bool expected)
        {
            // Arrange
            IWorld world = World.FromSize(width, height).Move(10, 5);

            // Act
            bool actual = IWorldExtensions.IsInBounds(world, row, column);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

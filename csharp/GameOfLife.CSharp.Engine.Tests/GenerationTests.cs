using GameOfLife.CSharp.Engine;
using Xunit;

namespace GameOfLife.Engine.Tests
{
    public class GenerationTests
    {
        [Fact]
        public void Next_FromBlinkerPeriod1Pattern_ReturnsGeneration1()
        {
            // Arrange, Act
            Generation generation0 = Generation.Zero(PopulationPatterns.Blinker);
            Generation generation1 = generation0.Next();

            // Assert
            Assert.NotNull(generation0);
            Assert.NotNull(generation1);
            Assert.NotEqual(generation0, generation1);
            Assert.Equal(0m, generation0.Number);
            Assert.Equal(1m, generation1.Number);
        }
    }
}

using GameOfLife.Engine;
using System;
using System.Threading.Tasks;

namespace GameOfLife.App
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            Generation generation0 = Generation.Zero(PopulationPatterns.Toad);
            Generation generationN = ObserveCommands(generation0);
            Console.WriteLine(generationN);
        }

        private static Generation ObserveCommands(Generation generation)
        {
            var keyInfo = Console.ReadKey();
            return keyInfo.Key == ConsoleKey.Escape
                ? generation
                : ObserveCommands(generation.Next());
        }
    }
}

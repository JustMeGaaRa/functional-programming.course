using GameOfLife.Engine;
using System;
using System.Threading.Tasks;

namespace GameOfLife.App
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            Generation generation0 = Generation.Zero(PopulationPatterns.Pulsar);
            Generation generationN = ObserveCommands(generation0);

            Console.WriteLine(generationN);
            Console.Clear();

            Time time = new Time();
            IObservable<Generation> obserable = time.Start(PopulationPatterns.Pulsar);
            using IDisposable disposable = obserable.Subscribe(Render);
            await Task.Delay(10000);
            await time.End();
        }

        private static Generation ObserveCommands(Generation generation)
        {
            var keyInfo = Console.ReadKey();
            Render(generation);
            return keyInfo.Key == ConsoleKey.Escape
                ? generation
                : ObserveCommands(generation.Next());
        }

        private static void Render(Generation generation)
        {
            Console.Clear();

            for (int row = 0; row < generation.Size.Height; row++)
            {
                for (int column = 0; column < generation.Size.Width; column++)
                {
                    string cell = generation[row, column].Population.IsAlive() ? "+" : " ";
                    Console.Write(cell);
                }

                Console.WriteLine();
            }
        }
    }
}

using GameOfLife.CSharp.Engine;
using GameOfLife.Engine;
using System;
using System.Threading.Tasks;

namespace GameOfLife.App
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            Generation zero = Generation.Zero(PopulationPatterns.Pulsar);
            Time time = new Time(zero);
            using (var disposable = time.Subscribe(Render))
            {
                time.Start();
                await Task.Delay(10000);
            }
        }

        private static void Render(Generation generation)
        {
            Console.Clear();

            for (int row = 0; row < generation.Size.Height; row++)
            {
                for (int column = 0; column < generation.Size.Width; column++)
                {
                    string cell = generation[row, column].IsAlive() ? "+" : " ";
                    Console.Write(cell);
                }

                Console.WriteLine();
            }
        }
    }
}

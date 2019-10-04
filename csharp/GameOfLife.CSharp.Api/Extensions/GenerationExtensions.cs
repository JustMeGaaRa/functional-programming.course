using GameOfLife.CSharp.Api.Models;
using GameOfLife.Engine;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Extensions
{
    public static class GenerationExtensions
    {
        public static WorldVM ToWorldVM(this Generation generation)
        {
            ICollection<WorldRowVM> rows = new List<WorldRowVM>();

            for (int row = 0; row < generation.Size.Height; row++)
            {
                ICollection<WorldColumnVM> columns = new List<WorldColumnVM>();

                for (int column = 0; column < generation.Size.Width; column++)
                {
                    bool isAlive = generation[row, column].Population.IsAlive();
                    columns.Add(new WorldColumnVM { IsAlive = isAlive });
                }

                rows.Add(new WorldRowVM { Columns = columns });
            }

            return new WorldVM
            {
                Generation = generation.Number,
                Height = generation.Size.Height,
                Width = generation.Size.Width,
                Rows = rows
            };
        }
    }
}

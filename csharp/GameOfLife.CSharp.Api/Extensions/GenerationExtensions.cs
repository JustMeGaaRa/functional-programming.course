using GameOfLife.CSharp.Api.ViewModels;
using GameOfLife.CSharp.Engine;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Extensions
{
    public static class GenerationExtensions
    {
        public static PopulationPatternViewVM ToPatternViewVM(this Generation generation)
        {
            ICollection<PopulationPatternRowVM> rows = new List<PopulationPatternRowVM>();

            for (int row = 0; row < generation.Size.Height; row++)
            {
                ICollection<PopulationPatternCellVM> columns = new List<PopulationPatternCellVM>();

                for (int column = 0; column < generation.Size.Width; column++)
                {
                    bool isAlive = generation[row, column].IsAlive();
                    columns.Add(new PopulationPatternCellVM { Row = row, Column = column, IsAlive = isAlive });
                }

                rows.Add(new PopulationPatternRowVM { Number = row, Columns = columns });
            }

            return new PopulationPatternViewVM
            {
                Generation = generation.Number,
                Height = generation.Size.Height,
                Width = generation.Size.Width,
                Rows = rows
            };
        }
    }
}

using GameOfLife.CSharp.Api.Models;
using GameOfLife.CSharp.Engine;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Extensions
{
    public static class PopulationPatternExtensions
    {
        public static PopulationPatternInfoVM? ToPatternInfoVM(this PopulationPattern pattern)
        {
            return pattern == null
                ? null
                : new PopulationPatternInfoVM
                {
                    PatternId = pattern.PatternId,
                    Name = pattern.Name,
                    Height = pattern.Height,
                    Width = pattern.Width
                };
        }

        public static PopulationPatternViewVM ToPatternViewVM(this PopulationPattern pattern)
        {
            ICollection<PopulationPatternRowVM> rows = new List<PopulationPatternRowVM>();

            for (int row = 0; row < pattern.Height; row++)
            {
                ICollection<PopulationPatternCellVM> columns = new List<PopulationPatternCellVM>();

                for (int column = 0; column < pattern.Width; column++)
                {
                    bool isAlive = pattern[row, column];
                    columns.Add(new PopulationPatternCellVM { Row = row, Column = column, IsAlive = isAlive });
                }

                rows.Add(new PopulationPatternRowVM { Number = row, Columns = columns });
            }

            return new PopulationPatternViewVM
            {
                PatternId = pattern.PatternId,
                Name = pattern.Name,
                Height = pattern.Height,
                Width = pattern.Width,
                Rows = rows
            };
        }
    }
}

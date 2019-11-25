using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Models
{
    public class PopulationPatternRowVM
    {
        public int Number { get; set; }

        public ICollection<PopulationPatternCellVM>? Columns { get; set; }
    }
}

using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Models
{
    public class PopulationPatternViewVM
    {
        public uint Generation { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public ICollection<PopulationPatternRowVM>? Rows { get; set; }
    }
}

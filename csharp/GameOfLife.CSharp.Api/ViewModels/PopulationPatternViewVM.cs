using System;
using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Models
{
    public class PopulationPatternViewVM
    {
        public int PatternId { get; set; }

        public Guid InstanceId { get; set; }

        public string? Name { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public uint Generation { get; set; }

        public ICollection<PopulationPatternRowVM>? Rows { get; set; }
    }
}

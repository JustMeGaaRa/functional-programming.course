using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Models
{
    public class GenerationVM
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public ICollection<PositionVM>? AliveCells { get; set; }
    }
}

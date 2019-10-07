using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Models
{
    public class WorldRowVM
    {
        public int Number { get; set; }

        public ICollection<WorldColumnVM>? Columns { get; set; }
    }
}

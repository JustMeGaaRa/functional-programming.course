using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Models
{
    public class WorldRowVM
    {
        public ICollection<WorldColumnVM>? Columns { get; set; }
    }
}

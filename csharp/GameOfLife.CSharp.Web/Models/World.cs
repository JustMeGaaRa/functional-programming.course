using System.Collections.Generic;

namespace GameOfLife.CSharp.Web.Models
{
    public class World
    {
        public int PatternId { get; set; }

        public string Name { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public uint Generation { get; set; }

        public ICollection<WorldRow> Rows { get; set; }
    }
}

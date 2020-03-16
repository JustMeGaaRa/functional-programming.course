using System.Collections.Generic;

namespace GameOfLife.CSharp.Web.Models
{
    public class WorldRow
    {
        public int Number { get; set; }

        public ICollection<WorldColumn> Columns { get; set; }
    }
}

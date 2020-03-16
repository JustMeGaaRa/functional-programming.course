using System.Collections.Generic;

namespace GameOfLife.CSharp.Web.Models
{
    public class World
    {
        public ICollection<WorldRow> Rows { get; set; }
    }
}

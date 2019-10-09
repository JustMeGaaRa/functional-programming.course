using System.ComponentModel.DataAnnotations;

namespace GameOfLife.CSharp.Api.Models
{
    public class PopulationPatternCellVM
    {
        [Required]
        public int Row { get; set; }

        [Required]
        public int Column { get; set; }

        [Required]
        public bool IsAlive { get; set; }
    }
}

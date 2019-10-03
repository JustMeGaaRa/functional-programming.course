using System.ComponentModel.DataAnnotations;

namespace GameOfLife.CSharp.Api.Models
{
    public class WorldCellVM
    {
        [Required]
        public int Row { get; set; }

        [Required]
        public int Column { get; set; }

        [Required]
        public bool Alive { get; set; }
    }
}

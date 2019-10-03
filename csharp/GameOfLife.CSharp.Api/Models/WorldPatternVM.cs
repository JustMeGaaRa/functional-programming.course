using System.ComponentModel.DataAnnotations;

namespace GameOfLife.CSharp.Api.Models
{
    public class WorldPatternVM
    {
        public string Name { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }
    }
}

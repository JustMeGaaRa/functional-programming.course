using System.ComponentModel.DataAnnotations;

namespace GameOfLife.CSharp.Api.Models
{
    public class WorldPatternVM
    {
        public int PatternId { get; set; }

        public string? Name { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }
    }
}

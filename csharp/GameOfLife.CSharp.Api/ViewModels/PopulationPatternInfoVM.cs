using System.ComponentModel.DataAnnotations;

namespace GameOfLife.CSharp.Api.ViewModels
{
    public class PopulationPatternInfoVM
    {
        public int PatternId { get; set; }

        public string? Name { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }
    }
}

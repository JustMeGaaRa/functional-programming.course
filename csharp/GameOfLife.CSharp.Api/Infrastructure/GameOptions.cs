namespace GameOfLife.CSharp.Api.Infrastructure
{
    public class GameOptions
    {
        public GameOptions()
        {
            PatternsDirectory = "patterns";
        }

        public string PatternsDirectory { get; set; }
    }
}

namespace GameOfLife.CSharp.Engine
{
    public record Size(int Width, int Height)
    {
        public static Size None => new (0, 0);
    }
}

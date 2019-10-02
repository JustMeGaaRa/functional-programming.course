namespace GameOfLife.Engine
{
    public class Size
    {
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static Size None => new Size(0, 0);

        public int Width { get; }

        public int Height { get; }

        public override bool Equals(object obj) => 
            obj is Size size
                && size.Width == Width
                && size.Height == Height;

        public override int GetHashCode() => Height ^ Width;
    }
}

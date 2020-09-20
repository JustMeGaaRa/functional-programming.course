namespace GameOfLife.CSharp.Engine
{
    public record Offset(int Left, int Top)
    {
        public static Offset None => new (0, 0);

        public Offset Shift(int shiftLeft, int shiftTop) => this with { Left = Left + shiftLeft, Top = Top + shiftTop };
    }
}

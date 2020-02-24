namespace GameOfLife.CSharp.Engine
{
    public class Offset
    {
        public Offset(int left, int top)
        {
            Left = left;
            Top = top;
        }

        public static Offset None => new Offset(0, 0);

        public int Left { get; }

        public int Top { get; }

        public override bool Equals(object obj) =>
            obj is Offset offset
                && offset.Left == Left
                && offset.Top == Top;

        public override int GetHashCode() => Left ^ Top;

        public Offset Shift(int shiftLeft, int shiftTop) => new Offset(Left + shiftLeft, Top + shiftTop);
    }
}

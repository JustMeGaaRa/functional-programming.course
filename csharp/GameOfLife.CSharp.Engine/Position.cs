namespace GameOfLife.Engine
{
    public class Position
    {
        private Position(int x, int y)
        {
            Row = x;
            Column = y;
        }

        public int Row { get; }

        public int Column { get; }

        public static Position None => new Position(0, 0);

        public static Position From(int row, int column) => new Position(row, column);
    }
}

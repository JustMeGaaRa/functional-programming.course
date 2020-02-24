namespace GameOfLife.CSharp.Engine
{
    public class World : IWorld
    {
        private readonly Cell[,] _cells;

        private World(Cell[,] cells, Offset topLeft, Size size)
        {
            _cells = cells ?? throw new System.ArgumentNullException(nameof(cells));
            Size = size ?? throw new System.ArgumentNullException(nameof(size));
            TopLeft = topLeft ?? throw new System.ArgumentNullException(nameof(topLeft));
            BottomRight = new Offset(topLeft.Left + size.Width, topLeft.Top + size.Height);
        }

        public static World Empty => new World(new Cell[0, 0], Offset.None, Size.None);

        public Cell this[int relativeRow, int relativeColumn]
        {
            get
            {
                return this.IsValidIndex(relativeRow, relativeColumn)
                    ? _cells[relativeRow, relativeColumn] : Cell.Empty;
            }
        }

        public Offset TopLeft { get; }

        public Offset BottomRight { get; }

        public Size Size { get; }

        public static World FromState(Cell[,] cells, Offset topLeft, Size size)
        {
            return new World(cells, topLeft, size);
        }

        public static World FromSize(int width, int height)
        {
            Size size = new Size(width, height);
            Cell[,] cells = new Cell[width, height];
            return new World(cells, Offset.None, size);
        }

        public static World FromPattern(PopulationPattern pattern)
        {
            Size size = new Size(pattern.Width, pattern.Height);
            Cell[,] cells = pattern.Select(alive => Cell.Create(alive ? Population.Alive : Population.Dead));
            return new World(cells, Offset.None, size);
        }

        public IWorld Move(int shiftLeft, int shiftTop) => new World(_cells, TopLeft.Shift(shiftLeft, shiftTop), Size);

        public bool IsCellAliveBySelfOffset(int relativeRow, int relativeColumn) => this[relativeRow, relativeColumn].IsAlive();

        public bool IsCellAliveByAbsoluteOffset(int absoluteRow, int absoluteColumn) => this[absoluteRow - TopLeft.Top, absoluteColumn - TopLeft.Left].IsAlive();
    }
}

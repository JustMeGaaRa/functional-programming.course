namespace GameOfLife.CSharp.Engine
{
    public static class ImmutableGridExtensions
    {
        public static bool IsValidIndex(Size size, int relativeRow, int relativeColumn)
        {
            if (size is null) throw new System.ArgumentNullException(nameof(size));

            return relativeRow >= 0
                && relativeColumn >= 0
                && relativeRow < size.Height
                && relativeColumn < size.Width;
        }
    }
}

using System;

namespace GameOfLife.CSharp.Engine
{
    public record Cell(Population Population)
    {
        public Guid Identity { get; } = Guid.NewGuid();

        public static Cell Empty => new(Population.None);

        public static Cell Alive => new(Population.Alive);

        public static Cell Dead => new(Population.Dead);

        public static Cell Create(Population state) => new(state);
    }
}

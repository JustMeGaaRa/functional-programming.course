namespace GameOfLife.Engine
{
    public class Time
    {
        private readonly Generation _generation;

        private Time(Generation generation) => _generation = generation;

        public static Time None => new Time(Generation.Zero(0, 0));

        public Time Start() => this;

        public Time Stop() => this;
    }
}

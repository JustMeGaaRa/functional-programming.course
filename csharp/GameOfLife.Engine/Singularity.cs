namespace GameOfLife.Engine
{
    public class Singularity
    {
        public static Time BigBang() => Time.None.Start();

        public static Time BigCrunch(Time time) => time.Stop();
    }
}

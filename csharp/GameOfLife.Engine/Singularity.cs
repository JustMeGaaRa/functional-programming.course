using System.Threading.Tasks;

namespace GameOfLife.Engine
{
    public class Singularity
    {
        public static Task<Time> BigBang() => Time.None.Start();

        public static Task<Time> BigCrunch(Time time) => time.Stop();
    }
}

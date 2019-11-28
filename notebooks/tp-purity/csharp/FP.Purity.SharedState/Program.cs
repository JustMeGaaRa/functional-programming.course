namespace FP.Purity.SharedState
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string AlicesName = "Alice";
            const string BobsName = "Bob";
            const string ChargingCableName = "Charging Cable";

            var alice = new Person(AlicesName);
            var bob = new Person(BobsName);
            // shared state
            var drawer = new Drawer();
            alice.Store(ChargingCableName, drawer);

            bob.Get(ChargingCableName, drawer).Print();
            alice.Get(ChargingCableName, drawer).Print();

            // different states
            var alicesDrawer = new Drawer();
            var bobsDrawer = new Drawer();
            alice.Store(ChargingCableName, alicesDrawer);

            bob.Get(ChargingCableName, bobsDrawer).Print();
            alice.Get(ChargingCableName, alicesDrawer).Print();
        }
    }
}

namespace FP.Purity.MutableState
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var alice = new Alice();
            var bob = new Bob();

            // mutable state
            var device = new Phone()
                .Install(Apps.InstagramAppName)
                .Install(Apps.FacebookAppName)
                .Install(Apps.TwitterAppName);

            alice.UseDevice(device);
            bob.UseDevice(device);

            // different states
            var alicesDevice = new Phone()
                .Install(Apps.InstagramAppName)
                .Install(Apps.FacebookAppName)
                .Install(Apps.TwitterAppName);

            alice.UseDevice(alicesDevice);

            var bobsDevice = new Phone()
                .Install(Apps.InstagramAppName)
                .Install(Apps.FacebookAppName)
                .Install(Apps.TwitterAppName);

            bob.UseDevice(bobsDevice);
        }
    }
}

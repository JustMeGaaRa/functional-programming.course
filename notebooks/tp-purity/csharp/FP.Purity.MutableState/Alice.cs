namespace FP.Purity.MutableState
{
    public class Alice : Person
    {
        public Alice() : base("Alice") { }

        public override void UseDevice(Phone phone)
        {
            phone
                .Run(Apps.InstagramAppName)
                .Run(Apps.TwitterAppName)
                .Remove(Apps.TwitterAppName);
        }
    }
}

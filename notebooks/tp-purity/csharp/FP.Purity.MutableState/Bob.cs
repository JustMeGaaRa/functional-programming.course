namespace FP.Purity.MutableState
{
    public class Bob : Person
    {
        public Bob() : base("Bob") { }

        public override void UseDevice(Phone phone)
        {
            phone
                .Run(Apps.FacebookAppName)
                .Run(Apps.TwitterAppName);
        }
    }
}

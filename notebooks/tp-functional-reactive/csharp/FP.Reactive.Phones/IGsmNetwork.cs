namespace FP.Reactive.Phones
{
    public interface IGsmNetwork
    {
        IChannel EstablishConnection(string from, string to);
    }
}

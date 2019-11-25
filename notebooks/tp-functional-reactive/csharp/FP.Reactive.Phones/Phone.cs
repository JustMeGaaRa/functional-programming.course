namespace FP.Reactive.Phones
{
    public class Phone
    {
        private IGsmNetwork _gsmOperator;

        public Phone(string owner) => Owner = owner;

        public string Owner { get; }

        public IGsmNetwork UseGsmNetwork(IGsmNetwork gsmOperator) => _gsmOperator = gsmOperator;

        public IChannel Call(string contact) => _gsmOperator.EstablishConnection(Owner, contact);

        public IChannel Receive(string contact) => _gsmOperator.EstablishConnection(Owner, contact);
    }
}

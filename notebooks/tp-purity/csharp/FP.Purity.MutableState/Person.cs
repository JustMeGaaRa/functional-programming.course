namespace FP.Purity.MutableState
{
    public abstract class Person
    {
        protected Person(string name) => Name = name;

        public string Name { get; }

        public abstract void UseDevice(Phone phone);
    }
}

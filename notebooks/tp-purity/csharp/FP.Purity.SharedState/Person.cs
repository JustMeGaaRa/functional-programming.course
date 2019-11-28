namespace FP.Purity.SharedState
{
    public class Person
    {
        public Person(string name) => Name = name;

        public string Name { get; }

        public void Store(string item, Drawer drawer) => drawer.Store(item);

        public string Get(string name, Drawer drawer) => drawer.Retreive(name);
    }
}

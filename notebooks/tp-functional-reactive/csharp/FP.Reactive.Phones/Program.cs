using System;
using System.Threading.Tasks;

namespace FP.Reactive.Phones
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string Alice = "Alice";
            const string Bob = "Bob";
            var alicesPhone = new Phone(Alice);
            var bobsPhone = new Phone(Bob);

            // proactive example
            var proactiveGsm = new GsmNetwork<ProactiveChannel>();
            alicesPhone.UseGsmNetwork(proactiveGsm);
            bobsPhone.UseGsmNetwork(proactiveGsm);
            ExecuteSequence(Alice, Bob, alicesPhone, bobsPhone);

            // reactive example
            var reactiveGsm = new GsmNetwork<ReactiveChannel>();
            alicesPhone.UseGsmNetwork(reactiveGsm);
            bobsPhone.UseGsmNetwork(reactiveGsm);
            ExecuteSequence(Alice, Bob, alicesPhone, bobsPhone);

            Console.ReadKey();
        }

        private static void ExecuteSequence(string Alice, string Bob, Phone alicesPhone, Phone bobsPhone)
        {
            alicesPhone
                .Receive(Bob)
                .Listen(Console.WriteLine);

            Task.Delay(5000).Wait();

            bobsPhone
                .Call(Alice)
                .Send("Hi, Alice!")
                .Send("It's good that I've been able to call you.")
                .Send("Well, bye now!")
                .Close();
        }
    }
}

# Functional Reactive

So, Alice has a phone. And Bob has a phone. Bob can call Alice at any given time just by dialing the number. Now imagine if Alice had to have the phone picked up in order for Bob to reach her. She can wait besides the phone for hours, days before Bob calls. Not very convinient, right? It's easier to have the phone ringing when someone is calling, so that she can pick up the phone. Since someone calling Alice does not actually depend on Alice herself, it's only logical that she has more important stuff to do than sit and wait for a call. Remember the "Hollywood Principle"? "Don't call us, we will call you!" - they say. And they are right. Our example is exactly the same.

In other words, Bob calling Alice is like establishing a channel between point A and point B (haha, what a pun - A for Alice and B for Bob). Messages going between those two points are descrete signals in form of concrete values. So a channel can be represented as a queue of descrete values of int, for example.

If the first case, Alice waiting for a call is the same as reading an queue for incoming values. If the values are not there, then we have to wait for somme time and try again. You can see where the problem is.

"Let's see the code already" - you would say. Oh, okay then.

## Proactive Network

First, we have to define what a channel is.


```C#
using System;

public interface IChannel
{
    IChannel Send(string message);

    IChannel Listen(Action<string> handle);

    void Close();
}
```

Now in order to make calls, let's assume that we have to be on the same network. Here's what a network could look like in code.


```C#
public interface IGsmNetwork
{
    IChannel EstablishConnection(string from, string to);
}

public class GsmNetwork<TChannel> : IGsmNetwork where TChannel : IChannel, new()
{
    private readonly Dictionary<(string, string), IChannel> _channels = new Dictionary<(string, string), IChannel>();

    public IChannel EstablishConnection(string from, string to)
    {
        IChannel channel = _channels.ContainsKey((to, from))
            ? _channels[(to, from)]
            : _channels[(from, to)] = new TChannel();
        return _channels.ContainsKey((from, to))
            ? _channels[(from, to)]
            : channel;
    }
}
```

Network just establishes a connection between to recipients. Alice calling Bob should be the same channel as Bob calling Alice. Ther can be no more channels between same two people at the same time.

But to make a acall we need a phone. And phone it is.


```C#
public class Phone
{
    private IGsmNetwork _gsmOperator;

    public Phone(string owner) => Owner = owner;

    public string Owner { get; }

    public IGsmNetwork UseGsmNetwork(IGsmNetwork gsmOperator) => _gsmOperator = gsmOperator;

    public IChannel Call(string contact) => _gsmOperator.EstablishConnection(Owner, contact);

    public IChannel Receive(string contact) => _gsmOperator.EstablishConnection(Owner, contact);
}
```

A phone can use one network at a time. It can also call someone or receive a call, or pick up the phone if you prefer.

And we are ready to see the first case in action. So a channel implementation would look like:


```C#
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class ProactiveChannel : IChannel
{
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly ConcurrentQueue<string> _messages = new ConcurrentQueue<string>();
    private Task _processingHandle;

    public ProactiveChannel()
    {
        Console.WriteLine();
        Console.WriteLine("Starting Proactive GSM Line...");
        Console.WriteLine("-------------------------------------");
    }

    public void Close()
    {
        _cts.Cancel();
        _processingHandle.Wait();
    }

    public IChannel Listen(Action<string> handle)
    {
        _processingHandle = Task.Run(() => ProcessMessages(handle));
        return this;
    }

    public IChannel Send(string message)
    {
        _messages.Enqueue(message);
        return this;
    }

    private void ProcessMessages(Action<string> handle)
    {
        while (!_cts.IsCancellationRequested || !_messages.IsEmpty)
        {
            string text = _messages.TryDequeue(out string message)
                ? message
                : "...";
            handle(text);
            Task.Delay(1000).Wait();
        }
    }
}
```

There are all the pieces to run the code and see how it goes.


```C#
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

const string Alice = "Alice";
const string Bob = "Bob";
var alicesPhone = new Phone(Alice);
var bobsPhone = new Phone(Bob);

var proactiveGsm = new GsmNetwork<ProactiveChannel>();
alicesPhone.UseGsmNetwork(proactiveGsm);
bobsPhone.UseGsmNetwork(proactiveGsm);
ExecuteSequence(Alice, Bob, alicesPhone, bobsPhone);
```

    
    Starting Proactive GSM Line...
    -------------------------------------
    ...
    ...
    ...
    ...
    ...
    Hi, Alice!
    It's good that I've been able to call you.
    Well, bye now!
    

As you can see, s is way to inefficient to wait an undefined amount of time.

## Reactive Network

Refactoring the previous code into a version where the code gets called is a reactive way of doing things. Another implementation of a `IChannel` type will do just fine.


```C#
#r "nuget:System.Reactive, 4.2.0"

using System;
using System.Reactive.Subjects;

public class ReactiveChannel : IChannel
{
    private readonly ISubject<string> _subject = new Subject<string>();
    private IDisposable _disposable;

    public ReactiveChannel()
    {
        Console.WriteLine();
        Console.WriteLine("Starting Reactive GSM Line...");
        Console.WriteLine("-------------------------------------");
    }

    public void Close()
    {
        _disposable.Dispose();
        _subject.OnCompleted();
    }

    public IChannel Listen(Action<string> handle)
    {
        _disposable = _subject.Subscribe(handle);
        return this;
    }

    public IChannel Send(string message)
    {
        _subject.OnNext(message);
        return this;
    }
}
```


Installing package System.Reactive, version 4.2.0..........done!



Successfully added reference to package System.Reactive, version 4.2.0


Now to execute some code and see how it works:


```C#
var reactiveGsm = new GsmNetwork<ReactiveChannel>();
alicesPhone.UseGsmNetwork(reactiveGsm);
bobsPhone.UseGsmNetwork(reactiveGsm);
ExecuteSequence(Alice, Bob, alicesPhone, bobsPhone);
```

    
    Starting Reactive GSM Line...
    -------------------------------------
    Hi, Alice!
    It's good that I've been able to call you.
    Well, bye now!
    

Yay! No waiting on the line. Whenever Bob called Alice, she just received messages! Reactive can be that new kid on the block that turn out to be cool!

## Resources

- [Reactivity in F#](https://medium.com/@dagbrattli/reactivity-in-f-4540377d02fa)
- [Expert to Expert: Brian Beckman and Erik Meijer - Inside the .NET Reactive Framework (Rx)](https://channel9.msdn.com/Shows/Going+Deep/Expert-to-Expert-Brian-Beckman-and-Erik-Meijer-Inside-the-NET-Reactive-Framework-Rx)
- [Duality and the End of Reactive](https://channel9.msdn.com/Events/Lang-NEXT/Lang-NEXT-2014/Keynote-Duality)
- [Demystifying Functional Reactive Programming](https://itnext.io/demystifying-functional-reactive-programming-67767dbe520b)


```C#

```

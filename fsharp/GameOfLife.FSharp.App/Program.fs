open GameOfLife.FSharp.Engine

open System
open System.Threading

[<EntryPoint>]
let main argv =
    let render (generation: Generation) =
        Console.Clear()
        let height = generation.world.size.height - 1
        let width = generation.world.size.width - 1
        let cells = generation.world.cells

        let toChar = function
            | Dead -> " "
            | Alive -> "+"

        for row in 0..height do
            for column in 0..width do
                Console.Write(toChar cells.[row, column])
            Console.WriteLine()

    let cts = new CancellationTokenSource()
    let observable = Time.start PopulationPatterns.pulsar cts
    use disposable = observable.Subscribe render
    Async.Sleep(10000) |> Async.RunSynchronously
    0

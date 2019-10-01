open GameOfLife.FSharp.Engine

[<EntryPoint>]
let main argv =
    let world = World.fromPattern PopulationPatterns.pulsar
    0 // return an integer exit code

open GameOfLife.FSharp.Engine

[<EntryPoint>]
let main argv =
    let world = Generation.zero PopulationPatterns.pulsar
    0 // return an integer exit code

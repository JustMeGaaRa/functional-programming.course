namespace GameOfLife.FSharp.Engine

module Cell = 

    type Cell = 
        | Dead
        | Alive

    let empty() = Dead
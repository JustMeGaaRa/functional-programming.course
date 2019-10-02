namespace GameOfLife.FSharp.Engine

type Cell = 
    | Dead
    | Alive

module Cell = 

    let empty() = Dead
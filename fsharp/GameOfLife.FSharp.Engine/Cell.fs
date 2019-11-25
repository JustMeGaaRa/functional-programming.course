namespace GameOfLife.FSharp.Engine

type Cell = 
    | Dead
    | Alive

    with

    static member empty = Dead

    member this.isAlive = 
        match this with
        | Dead -> false
        | Alive -> true
namespace GameOfLife.FSharp.Engine

type Cell = 
    | Dead
    | Alive

    with

    static member empty = Dead

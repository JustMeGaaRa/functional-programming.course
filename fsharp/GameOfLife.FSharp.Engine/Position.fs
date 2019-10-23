namespace GameOfLife.FSharp.Engine
    
type Position = {
        row: int;
        column: int;
    } with

    static member none = {
        row = 0;
        column = 0;
    }

module Position =

    let create row column = {
        row = row;
        column = column;
    }


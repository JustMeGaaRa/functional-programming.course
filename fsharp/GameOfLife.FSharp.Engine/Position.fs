namespace GameOfLife.FSharp.Engine
    
type Position = {
    row: int;
    column: int;
}

module Position =

    let create row column = {
        row = row;
        column = column;
    }

    let none() = {
        row = 0;
        column = 0;
    }

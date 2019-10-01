namespace GameOfLife.FSharp.Engine

module Position =
    
    type Position = {
        row: int;
        column: int;
    }

    let create row column = {
        row = row;
        column = column;
    }

    let none() = {
        row = 0;
        column = 0;
    }

namespace GameOfLife.FSharp.Engine

module Size =
    
    type Size = {
        width: int;
        height: int;
    }

    let create width height = {
        width = width;
        height = height;
    }

    let none() = {
        width = 0;
        height = 0;
    }

namespace GameOfLife.FSharp.Engine
    
type Size = {
    width: int;
    height: int;
}

module Size =

    let create width height = {
        width = width;
        height = height;
    }

    let none() = {
        width = 0;
        height = 0;
    }

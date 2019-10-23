namespace GameOfLife.FSharp.Engine
    
type Size = {
        width: int;
        height: int;
    } with

    static member none = {
        width = 0;
        height = 0;
    }

module Size =

    let create width height = {
        width = width;
        height = height;
    }

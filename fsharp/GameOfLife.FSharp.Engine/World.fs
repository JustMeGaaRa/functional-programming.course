namespace GameOfLife.FSharp.Engine

open Cell
open Size

module World =

    type World = {
        cells: Cell[,];
        size: Size;
    }

    let empty() = {
        cells = Array2D.create 0 0 Dead;
        size = Size.none();
    }

    let fromSize width height = {
        cells = Array2D.create height width Dead;
        size = Size.create width height;
    }

    let fromPattern pattern = {
        cells = pattern;
        size = Size.create (Array2D.length2 pattern) (Array2D.length1 pattern)
    }

    let evolve world =
        let countAliveNeighbours world row column =
            let isAlive = function
                | Dead -> false
                | Alive -> true

            let checkPopulationSafely (row, column) =
                row >= 0 && column >= 0
                && row < world.size.height && column < world.size.width
                && isAlive world.cells.[row, column]

            let indicies = [|
                (row - 1, column - 1);
                (row - 1, column);
                (row - 1, column + 1);
                (row, column - 1);
                (row, column + 1);
                (row + 1, column - 1);
                (row + 1, column);
                (row + 1, column + 1);
            |]

            indicies |> Array.filter checkPopulationSafely |> Array.length
        
        let getNextCellState world row column cell =
            let aliveNeighbours = countAliveNeighbours world row column
            match (cell, aliveNeighbours) with
            | (Alive, x) when x < 2 -> Dead
            | (Alive, x) when x < 4 -> Alive
            | (Alive, x) when x > 3 -> Dead
            | (Dead, 3) -> Alive
            | _ -> Dead

        let cells = Array2D.mapi (getNextCellState world) world.cells
        { world with cells = cells }
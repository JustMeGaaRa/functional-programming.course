namespace GameOfLife.FSharp.Engine

type PopulationPattern = {
    cells: Cell[,];
    name: string;
    width: int;
    height: int;
}

module PopulationPattern =

    let create name cells = {
        name = name;
        cells = cells;
        width = Array2D.length2 cells;
        height = Array2D.length1 cells;
    }

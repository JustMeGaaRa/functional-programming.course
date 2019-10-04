namespace GameOfLife.FSharp.Engine

type Generation = {
    world: World;
    number: int;
}

module Generation =

    let create width height = {
        world = World.fromSize width height;
        number = 0;
    }

    let zero pattern = {
        world = World.fromPattern pattern.cells;
        number = 0;
    }

    let next generation = {
        world = World.evolve generation.world;
        number = generation.number + 1;
    }

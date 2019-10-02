namespace GameOfLife.FSharp.Engine

type Generation = {
    world: World;
    number: int;
}

module Generation =

    let zero states = {
        world = World.fromPattern states;
        number = 0;
    }

    let next generation = {
        world = World.evolve generation.world;
        number = generation.number + 1;
    }

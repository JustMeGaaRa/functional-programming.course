namespace GameOfLife.FSharp.Engine

open Cell

module PopulationPatterns =

    /// <summary>
    /// Blinker Pattern - Period 2
    /// </summary>
    let blinker = array2D [|
        [| Dead; Alive; Dead |];
        [| Dead; Alive; Dead |];
        [| Dead; Alive; Dead |];
    |]

    /// <summary>
    /// Toad Pattern - Period 2
    /// </summary>v
    let toad = array2D [|
        [| Dead; Dead; Dead; Dead |];
        [| Dead; Alive; Alive; Alive |];
        [| Alive; Alive; Alive; Dead |];
        [| Dead; Dead; Dead; Dead |];
    |]

    /// <summary>
    /// Beacon Pattern - Period 2
    /// </summary>
    let beacon = array2D [|
        [| Alive; Alive; Dead; Dead |];
        [| Alive; Alive; Dead; Dead |];
        [| Dead; Dead; Alive; Alive |];
        [| Dead; Dead; Alive; Alive |];
    |]

    /// <summary>
    /// Pulsar Pattern - Period 3
    /// </summary>
    let pulsar = array2D [|
        [| Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead |];
        [| Dead; Dead; Dead; Alive; Alive; Alive; Dead; Dead; Dead; Alive; Alive; Alive; Dead; Dead; Dead |];
        [| Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead |];
        [| Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead |];
        [| Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead |];
        [| Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead |];
        [| Dead; Dead; Dead; Alive; Alive; Alive; Dead; Dead; Dead; Alive; Alive; Alive; Dead; Dead; Dead |];
        [| Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead |];
        [| Dead; Dead; Dead; Alive; Alive; Alive; Dead; Dead; Dead; Alive; Alive; Alive; Dead; Dead; Dead |];
        [| Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead |];
        [| Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead |];
        [| Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead; Alive; Dead; Dead; Dead; Dead; Alive; Dead |];
        [| Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead |];
        [| Dead; Dead; Dead; Alive; Alive; Alive; Dead; Dead; Dead; Alive; Alive; Alive; Dead; Dead; Dead |];
        [| Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead; Dead |];
    |]
    
    let all = [|
        blinker; toad; beacon; pulsar
    |]
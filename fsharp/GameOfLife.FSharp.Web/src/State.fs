module App.State

open Elmish
open PopulationPatternCell.Types
open PopulationPatternRow.Types
open PopulationPatternGrid.Types
open App.Types

let init() : Model * Cmd<Message> =
    let cells = Pattern [|
        Row [| Dead; Alive; Dead |];
        Row [| Dead; Alive; Dead |];
        Row [| Dead; Alive; Dead |];
    |]
    { Pattern = cells; }, []

let update (msg: Message) (model: Types.Model) =
    model, []

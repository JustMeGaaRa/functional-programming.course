module PopulationPatternCell.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types

let root model =
    let cellState =
        match model with
        | Alive -> "population alive"
        | Dead  -> "population dead"

    td [] [
        div [ ClassName cellState ] []
    ]

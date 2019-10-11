module PopulationPatternGrid.View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open PopulationPatternGrid.Types

let root model =
    let rows =
        match model with
        | Pattern data -> Array.map PopulationPatternRow.View.root data

    div [ ClassName "grid centered" ] [
        table [] [
            tbody [] rows
        ]
    ]

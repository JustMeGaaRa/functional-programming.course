module PopulationPatternRow.View

open Fable.Helpers.React
open PopulationPatternRow.Types

let root model =
    let cells =
        match model with
        | Row data -> Array.map PopulationPatternCell.View.root data

    tr [ ] cells

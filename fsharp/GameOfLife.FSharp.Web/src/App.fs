module App.View

open Elmish
open Elmish.Browser.Navigation
open Fable.Core.JsInterop
open App.State
open App.Types

importAll "../sass/main.sass"

open Fable.Helpers.React
open Fable.Helpers.React.Props

let root model (dispatch: Dispatch<Message>) =
    let grid =
        match model with
        | { Pattern = patten } -> PopulationPatternGrid.View.root patten

    div [ ClassName "layout" ] [
        p [] [
            str "Generation: 0"
        ]
        grid
    ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update root
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReact "elmish-app"
|> Program.run

namespace GameOfLife.FSharp.Api

open Giraffe
open GiraffeViewEngine
open GameOfLife.FSharp.Engine

module Views =

    let layout (content: XmlNode list) =
        html [] [
            head [] [
                title []  [ encodedText "Conway's Game Of Life" ]
                link [
                    _rel "stylesheet"
                    _type "text/css"
                    _href "/main.css" 
                ]
            ]
            body [
                _class "layout"
            ] content
        ]

    let index (genertion : Generation) =
        let getRow row (array: 'a[,]) = array.[row, *] |> Seq.toList
        
        let toCell cell =
            let style = 
                match cell with
                | Dead -> "population dead"
                | Alive -> "population alive"

            td [] [
                div [ _class style ] []
            ]

        let rows = genertion.world.size.height - 1
        let cells = genertion.world.cells
        let trs = [
            for row in 0..rows -> getRow row cells |> List.map toCell |> tr []
        ]

        layout [
            div [
                _class "grid centered"
            ] [
                table [] trs
            ]
        ]
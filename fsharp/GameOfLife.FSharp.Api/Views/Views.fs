namespace GameOfLife.FSharp.Api

open Giraffe
open GiraffeViewEngine

module Views =

    let layout (content: XmlNode list) =
        html [] [
            head [] [
                title []  [ encodedText "fsharp" ]
                link [
                        _type "text/css"
                        _href "/main.css" ]
            ]
            body [] content
        ]

    let partial () =
        h1 [] [ encodedText "fsharp" ]

    let index (model : Message) =
        [
            partial()
            p [] [ encodedText model.Text ]
        ] |> layout
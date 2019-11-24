open System

type Result<'a> =
    | Success of 'a
    | Failure of Exception

let bind func input =
    match input with
    | Success value   -> func value
    | Failure message -> Failure message

let map func input =
    match input with
    | Success value   -> Success (func value)
    | Failure message -> Failure message

let bindSafe func input =
    try func input
    with
    | ex -> Failure ex

let mapSafe func input =
    try Success (func input)
    with
    | ex -> Failure ex

let get input =
    match input with
    | Success value -> value
    | Failure _ -> Unchecked.defaultof<'a>
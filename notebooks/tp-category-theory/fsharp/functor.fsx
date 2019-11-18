// category: Maybe
module Maybe =

    type Maybe<'a> =
        | Some of 'a
        | None

    // functor
    let bind a =
        Some a
        
    // morphisms
    let map func a = 
        match a with
        | Some value -> Some (func value)
        | None -> None

// identity rule
let id a = a
printfn "Identity of 4.07 is %A" (Maybe.map id (Maybe.bind 4.07))
printfn "Identity of \"Hello World!\" is %A" (Maybe.map id (Maybe.bind "Hello World!"))

// composition rule
let value5 = Maybe.bind 5
let add2 = fun a -> a + 2
let add4 = fun b -> b + 4

let left = Maybe.map (add2 >> add4)
let right = Maybe.map add2 >> Maybe.map add4

printfn "Left: %A" (left value5)
printfn "Right: %A" (right value5)
// category of sets: string, float, int, bool
// morphisms
let fromStringToFloat value = float value
let fromtFloatToInt value = int value
let fromoIntToBool value = value % 2 = 0

// identity rule
let id a = a
printfn "Identity of 4.07 is %A" (id 4.07)
printfn "Identity of \"Hello World!\" is %A" (id "Hello World!")

// composition rule
let fromoStringToBool = fromStringToFloat >> fromtFloatToInt >> fromoIntToBool
printfn "Is Even: %b" (fromoStringToBool "4.07")
printfn "Is Even: %b" (fromoStringToBool "17.01")

// associativity rule
let left = fromStringToFloat >> (fromtFloatToInt >> fromoIntToBool)
let right = (fromStringToFloat >> fromtFloatToInt) >> fromoIntToBool

printfn "Left: %b" (left "4.07")
printfn "Right: %b" (right "4.07")

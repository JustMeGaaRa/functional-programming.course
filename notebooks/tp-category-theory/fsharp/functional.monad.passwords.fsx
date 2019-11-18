open System

let check condition errorFormat password =
    if condition password then Ok password else Error <| sprintf errorFormat password

let charIsSpecial x = Array.contains x [|'$';'#';'@'|]

let checkPassword = 
    (check (String.exists Char.IsUpper)) "No upper case characters: %s."
    >> Result.bind (check (String.exists Char.IsLower) "No lower case characters: %s.")
    >> Result.bind (check (String.exists Char.IsDigit) "No digits: %s.")
    >> Result.bind (check (String.exists charIsSpecial) "No special characters: %s.")
    >> Result.bind (check (fun x -> String.length x >= 6) "Password is shorter than 6 characters: %s.")
    >> Result.bind (check (fun x -> String.length x <= 12) "Password is longer than 12 characters: %s.")

let printResult = function
    | Ok password -> printfn "%s" password
    | Error error -> printfn "%s" error

Console.ReadLine()
|> (fun passwords -> passwords.Split ',')
|> Array.map checkPassword
|> Array.iter printResult
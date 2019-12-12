open Money
open BankAccount
open StockMarket
open REIT

module Investment =
    
    type Investment<'a> = Investment of 'a

    let ofType item = Investment item

[<EntryPoint>]
let main argv =
    let cash = 400.
    let currency = "UAH"
    let account = BankAccount.debit currency |> BankAccount.add cash
    printfn "Account: %A" account

    let withdrawn300 = BankAccount.withdraw 300. account
    let withdrawn500 = BankAccount.withdraw 500. account
    printfn "Withdrawing 300: %A" withdrawn300
    printfn "Withdrawing 500: %A" withdrawn500

    let money = Money.from cash currency
    let investment = Investment.ofType money
    printfn "Investment: %A" investment

    let stock = "GOOG"
    
    0

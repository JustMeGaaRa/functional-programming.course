module BankAccount

open Money

type Account =
    | Credit of amount: Money * limit: float
    | Debit of amount: Money
    | Deposit of amount: Money * period: int * rate: float

let credit currency limit = Credit ((Money.from 0. currency), limit)

let debit currency = Debit (Money.from 0. currency)

let deposit currency amount period rate = Deposit ((Money.from amount currency), period, rate)

let add cash account = 
    match account with
    | Credit (money, limit) -> Credit ({ money with amount = money.amount + cash }, limit)
    | Debit money -> Debit ({ money with amount = money.amount + cash })
    | Deposit (money, period, rate) -> Deposit ({ money with amount = money.amount + cash }, period, rate)

let withdraw cash account =
    let withdrawFromCredit money limit cash =
        if cash > limit
        then Result.Error "Credit limit reached"
        else Credit ({ money with amount = money.amount - cash }, limit) |> Result.Ok

    let withdrawFromDebit money cash =
        if cash > money.amount
        then Result.Error "Not enough money on the balance"
        else { money with amount = money.amount - cash } |> Debit |> Result.Ok

    let withdrawDromDeposit money period rate cash =
        if cash > money.amount
        then Result.Error "Not enough money on deposit balance"
        else Deposit ({ money with amount = money.amount - cash }, period, rate) |> Result.Ok

    match account with
    | Credit (money, limit) -> withdrawFromCredit money limit cash
    | Debit money -> withdrawFromDebit money cash
    | Deposit (money, period, rate) -> withdrawDromDeposit money period rate cash

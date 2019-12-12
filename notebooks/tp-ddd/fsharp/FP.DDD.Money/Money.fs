module Money

type Money = {
    amount: float;
    currency: string;
}

let from amount currency = {
    amount = amount;
    currency = currency;
}

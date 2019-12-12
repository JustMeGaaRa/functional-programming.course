module StockMarket

type Share = {
    amount: int;
    name: string;
    price: float;
}

type StockMarket = { shares: Share list }

let openMarket() = {
    shares = [
        { amount = 1000; name = "GOOG"; price = 1304.96 };
        { amount = 1000; name = "AAPL"; price = 267.25 };
        { amount = 1000; name = "AMZN"; price = 1800.80 };
    ]
}

let updatePrice name price market =
    market

let buyShares name amount account market =
    let shares = market.shares
    (account, market, shares)

let sellShares shares account market =
    (account, market)

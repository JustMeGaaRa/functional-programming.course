module REIT

type RealEstate = {
    name: string;
    price: float;
}

type REIT = { realEstate: RealEstate list }

let openMarket() = {
    realEstate = [
        { name = "World Traing Center"; price = 35000000. };
        { name = "Empire State Building"; price = 50000000. };
    ]
}

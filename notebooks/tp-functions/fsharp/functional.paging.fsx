let mapToStrings = List.map (string)

// total < pages to show
// 1
// 1 2 3
// 1 2 3 4 5 6 7
let generateAllRange total = mapToStrings [1..total]

// current in between
// 1 .. 4 5 6 .. 10
let generateMidRange current total pagesToShow = 
    let start = current - pagesToShow / 2 + 2
    let finish = current + pagesToShow / 2 - 2
    let range = mapToStrings [start..finish]
    ["1";".."] @ range @ [".."; string total]

// current close to start
// 1 2 3 4 5 .. 10
let generateStartRange total pagesToShow =
    let finish = pagesToShow - 2
    let range = mapToStrings [1..finish]
    range @ [".."; string total]

// current close to finish
// 1 .. 6 7 8 9 10
let generateEndRange total pagesToShow =
    let start = total - pagesToShow + 3
    let range = mapToStrings [start..total]
    ["1";".."] @ range

let generatePageNumbers current total visible = 
    if total <= visible then generateAllRange total
    else if current - visible / 2 <= 0 then generateStartRange total visible
    else if current + visible / 2 >= total then generateEndRange total visible
    else generateMidRange current total visible


generatePageNumbers 1 51 7
generatePageNumbers 3 51 7
generatePageNumbers 4 51 7
generatePageNumbers 5 51 7
generatePageNumbers 30 51 7
generatePageNumbers 49 51 7
generatePageNumbers 51 51 7
generatePageNumbers 1 5 7
generatePageNumbers 4 6 7
generatePageNumbers 1 51 9
generatePageNumbers 3 51 9
generatePageNumbers 4 51 9
generatePageNumbers 5 51 9
generatePageNumbers 3 51 10
generatePageNumbers 30 51 10
generatePageNumbers 51 51 10
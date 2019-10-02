namespace GameOfLife.FSharp.Engine

open System.Threading

module Time =
    
    let start pattern (cts: CancellationTokenSource) = 
        let rec flow generation (cts: CancellationTokenSource) = async {
            do! Async.Sleep 1000
            if cts.IsCancellationRequested then return generation
            else return! flow (Generation.next generation) cts
        }

        flow (Generation.zero pattern) cts

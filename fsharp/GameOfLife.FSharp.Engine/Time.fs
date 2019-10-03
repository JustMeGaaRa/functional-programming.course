namespace GameOfLife.FSharp.Engine

open System
open System.Reactive.Subjects
open System.Threading

module Time =
    
    let start pattern (cts: CancellationTokenSource) = 
        let subject = new Subject<Generation>()
        let generation0 = Generation.zero pattern

        let rec flow (observer: IObserver<Generation>) generation (cts: CancellationTokenSource) = async {
            observer.OnNext(generation)
            do! Async.Sleep 1000
            if cts.IsCancellationRequested then return ()
            else return! flow observer (Generation.next generation) cts
        }

        flow subject generation0 cts |> Async.Start
        subject

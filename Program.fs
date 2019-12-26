// Learn more about F# at http://fsharp.org

open System
open JsonModule
open FSharp.Data
open XPlot.GoogleCharts
open Deedle

[<EntryPoint>]
let main argv =

    let res = classAmountData
    printfn "%A" res
    printfn "the end..." 
    // let names = JsonModule.passengers
    //             |> Array.map (fun passenger -> passenger.Fields.Name)
    // printfn "%A" names           

    // let avgAgeBySex =
    //     passengers
    //     |> Array.groupBy (fun p -> p.Fields.Sex)
    //     |> Array.map (fun (s,ps) -> s,ps |> Array.averageBy (fun p -> p.Fields.Age |> Option.defaultValue 0m))
    // printfn "%A" avgAgeBySex

    // let res = total
    // printfn "%A" total

    0 // return an integer exit code

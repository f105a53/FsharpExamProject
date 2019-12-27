module JsonModule

open System
open FSharp.Data
open Deedle
open XPlot.GoogleCharts

// Average age by class
type TitanicPassengers = JsonProvider<"./files/titanic-passengers.json">

let passengers = TitanicPassengers.GetSamples()

let showChart =
    let classAmountData =
        passengers
        |> Array.countBy (fun p -> p.Fields.Pclass)
        |> List.ofArray
        |> List.map (fun (x, y) -> x, y :> value)

    let classAgeSummaryData summaryFunc =
        passengers
        |> Array.groupBy (fun p -> p.Fields.Pclass)
        |> Array.map (fun (s, ps) ->
            s,
            ps
            |> Array.choose (fun p -> p.Fields.Age)
            |> summaryFunc)
        |> List.ofArray
        |> List.map (fun (x, y) -> x, y :> value)

    [ classAmountData
      classAgeSummaryData Array.min
      classAgeSummaryData Array.max
      classAgeSummaryData Array.average ]
    |> Chart.Table
    |> Chart.WithLabels [ "Amount"; "Min"; "Max"; "Avg" ]
    |> Chart.Show

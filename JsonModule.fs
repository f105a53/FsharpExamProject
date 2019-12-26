module JsonModule

open System
open FSharp.Data
open Deedle
open XPlot.GoogleCharts

// Average age by class
type TitanicPassengers = JsonProvider<"./files/titanic-passengers.json">

let passengers = TitanicPassengers.GetSamples()

let classAgeData =
    passengers
    |> Array.groupBy (fun p -> p.Fields.Pclass)
    |> Array.map (fun (s, ps) -> s, ps |> (Array.averageBy (fun p -> p.Fields.Age |> Option.defaultValue 0m)))
    |> List.ofArray

// let classSurvivedData =
//     passengers
//     |> Array.groupBy (fun p -> p.Fields.Pclass)
//     |> Array.map (fun (s, ps) -> s, ps |> (Array.countBy (fun p -> p.Fields.Survived)))
//     |> List.ofArray

let classAmountData =
    passengers
    |> Array.groupBy (fun p -> p.Fields.Pclass)
    |> Array.map (fun (s, ps) -> s, ps |> Array.countBy (fun p -> p.Fields.Pclass))



// let chartData =
//     passengers
//     |> Array.groupBy (fun p -> p.Fields.Pclass)
//     |> Array.map (fun (s, ps) -> s, ps |> (Array.averageBy (fun p -> p.Fields.Age |> Option.defaultValue 0m)))
//     |> List.ofArray
// |> Chart.Table
// |> Chart.WithOptions(Options(showRowNumber = true))
// |> Chart.WithLabels [ "Class"; "Age" ]
// |> Chart.Show




// let printArr =
//     passengers
//     |> Seq.map (fun p ->
//         [ p.Fields.JsonValue.TryGetProperty "age"
//           p.Fields.JsonValue.TryGetProperty "pclass" ])

// let testMatch p: obj =
//     match (p.Fields.JsonValue.TryGetProperty "age", p.Fields.JsonValue.TryGetProperty "pclass") with
//     | (Some a, Some c) -> (a, c)
//     | (_, _) -> ([], [])

// let printArr =
//     passengers
//     |> Seq.map (fun p ->
//         match (p.Fields.JsonValue.TryGetProperty "age", p.Fields.JsonValue.TryGetProperty "pclass") with
//         | (None, Some c) -> (JsonValue.Parse("""{ "age":-1 }""").GetProperty "age", c)
//         | (Some a, None) -> (a, JsonValue.Parse("""{ "pclass":-1 }""").GetProperty "pclass")
//         | (None, None) ->
//             (JsonValue.Parse("""{ "age":-1 }""").GetProperty "age",
//              JsonValue.Parse("""{ "pclass":-1 }""").GetProperty "pclass")
//         | (Some a, Some c) -> (a, c))
//     |> Seq.iter (fun p -> printfn "%A" p)

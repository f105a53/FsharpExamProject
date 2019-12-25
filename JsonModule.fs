module JsonModule

open System
open FSharp.Data
open Deedle

// Show correlation between age and class
type TitanicPassengers = JsonProvider<"./files/titanic-passengers.json">

let passengers = TitanicPassengers.GetSamples()

// let printArr =
//     passengers
//     |> Seq.map (fun p ->
//         [ p.Fields.JsonValue.TryGetProperty "age"
//           p.Fields.JsonValue.TryGetProperty "pclass" ])

// let testMatch p: obj =
//     match (p.Fields.JsonValue.TryGetProperty "age", p.Fields.JsonValue.TryGetProperty "pclass") with
//     | (Some a, Some c) -> (a, c)
//     | (_, _) -> ([], [])

let printArr =
    passengers
    |> Seq.map (fun p ->
        match (p.Fields.JsonValue.TryGetProperty "age", p.Fields.JsonValue.TryGetProperty "pclass") with
        | (None, Some c) -> (JsonValue.Parse("""{ "age":-1 }""").GetProperty "age", c)
        | (Some a, None) -> (a, JsonValue.Parse("""{ "pclass":-1 }""").GetProperty "pclass")
        | (None, None) ->
            (JsonValue.Parse("""{ "age":-1 }""").GetProperty "age",
             JsonValue.Parse("""{ "pclass":-1 }""").GetProperty "pclass")
        | (Some a, Some c) -> (a, c))
    |> Seq.iter (fun p -> printfn "%A" p)

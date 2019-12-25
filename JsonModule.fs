module JsonModule

open System
open FSharp.Data
open Deedle

// Show correlation between age and class

// type BookOrder = JsonProvider<"""{   "asks": [   [   "0.00001605",   14636.67789781   ],   [   "0.00001606",   73739.31116785   ],   [   "0.00001607",   1342158.7102721   ]   ],   "bids": [   [   "0.00001593",   17805.17982312   ],   [   "0.00001591",   71659.725   ],   [   "0.00001590",   67600.19748428   ]   ],   "isFrozen": "0",   "seq": 29534867  }""">
let sales =
    [ "2013", 1000
      "2014", 1170 ]
// let BookOrderNow = BookOrder.GetSample()

// let printArr =
//     BookOrderNow.JsonValue.Properties()
//     |> Seq.map fst
//     |> printfn "%A"


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
        | (None, _)
        | (_, None)
        | (None, None) -> (JsonValue.Parse("""{}"""), JsonValue.Parse("""{}"""))
        | (Some a, Some c) -> (a, c))
    |> Seq.iter (fun p -> printfn "%A" p)



// let printArr =
//     let arr = []
//     for passenger in passengers do
//         let mutable pair =
//             (passenger.Fields.JsonValue.GetProperty "age", passenger.Fields.JsonValue.GetProperty "pclass")
//         printfn "%A" pair



// let create


// let df4 =
//     [ ("Monday", "Tomas", 1.0)
//       ("Tuesday", "Adam", 2.1)
//       ("Tuesday", "Tomas", 4.0)
//       ("Wednesday", "Tomas", -5.4) ]
// type TitanicPassengers = JsonProvider<"./files/titanic-passengers.json">
// type AgeClassPair = {
//     Age: int;
//     PClass: int
// }

// let passengers = TitanicPassengers.Parse

// let parse props =
//     seq {
//         for (k, v) in props ->
//         {Age = v?Age.AsInteger(); PClass = v?PClass.AsInteger()}
//     }

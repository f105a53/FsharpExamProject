module CSV

open FSharp.Data
open Deedle
open XPlot.GoogleCharts

type TitanicData = CsvProvider<"data/titanic.csv">

let data = TitanicData.Load "data/titanic.csv"

let count matching data =
    let all = data |> Seq.length

    let matching =
        data
        |> Seq.where matching
        |> Seq.length
    float matching / float all

let survivalByAgeClass =
    data.Rows
    |> Seq.groupBy (fun p ->
        p.Pclass,
        ((p.Age / 10.0)
         |> floor
         |> int)
        * 10)
    |> Seq.map (fun (groups, data) -> groups, data |> count (fun p -> p.Survived))
    |> Seq.groupBy (fst >> fst)
    |> Seq.sortBy fst
    |> Seq.map (fun (age, data) -> data |> Seq.map (fun (t, avg) -> snd t, avg) |> Seq.sort)
    |> Chart.Line

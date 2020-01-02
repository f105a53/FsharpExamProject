module CSV

open FSharp.Data
open Deedle
open XPlot.GoogleCharts

type TitanicData = CsvProvider<"data/csv/titanic.csv">

let data = TitanicData.Load "data/csv/titanic.csv"

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
    |> Seq.map (fun (age, data) ->
        data
        |> Seq.map (fun (t, avg) -> snd t, avg)
        |> Seq.sort)
    |> Chart.Line

let survivedByClass =
    data.Rows
    |> Seq.groupBy (fun p -> p.Pclass, p.Survived)
    |> Seq.map (fun (group, data) ->
        let (clas, survived) = group
        string clas,
        (if survived then "Survived" else "Died"), data |> Seq.length)
    |> Chart.Sankey

    
let age =
    data.Rows
    |> Seq.map (fun p -> p.Name.Replace("\"",""), p.Age)
    |> Chart.Histogram
    |> Chart.WithLabel "Age"

let ageToFare =
    data.Rows
    |> Seq.map (fun p -> p.Fare, p.Age)
    |> Chart.Scatter

let passangersInClasses =
    data.Rows
    |> Seq.countBy (fun p -> p.Pclass)
    |> Seq.sort
    |> Chart.Table
    |> Chart.WithLabels ["Class";"Passanger Count"]
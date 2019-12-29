module XML

open System
open FSharp.Data
open XPlot.GoogleCharts

type MissingPersons = XmlProvider<"./data/xml/namus-missings.xml">

let missingByYearColumnsChart (chartHeight: int) =
    let data =
        MissingPersons.GetSample().Rows
        |> Array.choose (fun p -> p.DateOfLastContact.DateTime)
        |> Array.groupBy (fun t -> t.Year)
        |> Array.map (fun (y, t) -> (y |> string, Array.length t))
        |> Array.sortDescending

    data
    |> Chart.Column
    |> Chart.WithLabels [ "Missing Amount" ]
    |> Chart.WithOptions(Options(title = "Missing Persons per Year", hAxis = Axis(title = "Year")))
    |> Chart.WithHeight chartHeight
    |> Chart.Show

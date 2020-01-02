module XML

open System
open FSharp.Data
open XPlot.GoogleCharts

type MissingPersons = XmlProvider<"./data/xml/namus-missings.xml">

type AirQual = FSharp.Data.XmlProvider<"./data/xml/pollution.xml">

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
    |> Chart.WithOptions(Options(title = "Missing Persons per Year in the US", hAxis = Axis(title = "Year")))
    |> Chart.WithHeight chartHeight
    |> Chart.Show

let airQualityByCountry =
    let data =
        AirQual.GetSample().Rows
        |> Array.groupBy (fun d -> d.Country)
        |> Array.map (fun (c, d) -> c, d |> Array.averageBy (fun a -> a.Value |> double))
        |> Array.choose (fun (c, d) ->
            match c with
            | Some cc -> Some(cc, d)
            | None -> None)

    data
    |> Chart.Geo
 
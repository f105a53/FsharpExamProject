// Learn more about F# at http://fsharp.org

open System
open XPlot.GoogleCharts

type AirQual = FSharp.Data.XmlProvider<"./data/pollution.xml">

[<EntryPoint>]
let main argv =
    AirQual.GetSample().Rows
    |> Array.groupBy (fun d -> d.Country)
    |> Array.map (fun (c,d) -> c, d |> Array.averageBy (fun a -> a.Value |> double))
    |> Array.choose (fun (c,d) -> match c with | Some cc -> Some (cc,d) | None -> None )
    |> Chart.Geo
    |> Chart.Show
    
    // Json.showTitanicAmountChart 700 700
    // Json.showTitanicAgeChart 700 700
    // Json.missileSuccessRate 900 900
    // Json.showCommodityValueLineChart (["Copper"; "Lead"; "Aluminum"; "Nickel"; "Tin"] |> Array.ofList) 900

    // Json.showTitanicAmountChart 700 700
    // Json.showTitanicAgeChart 700 700
    // Json.missileSuccessRate 900 900
    // Json.showCommodityValueLineChart (["Copper"; "Lead"; "Aluminum"; "Nickel"; "Tin"] |> Array.ofList) 900

    // CSV.ageToFare
    // |> Chart.WithHeight 1000    
    // |> Chart.Show

    XML.missingByYearColumnsChart 1000
    // CSV.ageToFare
    // |> Chart.WithHeight 1000    
    // |> Chart.Show

    

    0 // return an integer exit code

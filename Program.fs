// Learn more about F# at http://fsharp.org

open System
open XPlot.GoogleCharts

[<EntryPoint>]
let main argv =

    
    // Json.showTitanicAmountChart 700 700
    // Json.showTitanicAgeChart 700 700
    Json.missileSuccessRate 900 900
    // Json.showCommodityValueLineChart (["Copper"; "Lead"; "Aluminum"; "Nickel"; "Tin"] |> Array.ofList) 900

    // // CSV.ageToFare
    // // |> Chart.WithHeight 1000    
    // // |> Chart.Show

    // XML.missingByYearColumnsChart 1000
    // //XML.airQualityByCountry
    // CSV.passangersInClasses |> Chart.Show

    0 // return an integer exit code

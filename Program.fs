// Learn more about F# at http://fsharp.org

open System
open Json

[<EntryPoint>]
let main argv =

    showTitanicAmountChart 700 700
    showTitanicAgeChart 700 700
    missileSuccessRate 900 900
    showCommodityValueLineChart (["Copper"; "Lead"; "Aluminum"; "Nickel"; "Tin"] |> Array.ofList) 900

    CSV.ageToFare
    |> Chart.WithHeight 1000    
    |> Chart.Show

    0 // return an integer exit code

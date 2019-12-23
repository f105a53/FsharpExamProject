// Learn more about F# at http://fsharp.org

open System
open FSharp.Data
open XPlot.GoogleCharts

[<EntryPoint>]
let main argv =
    let data = WorldBankData.GetDataContext()
    data.Countries.Denmark.Indicators.``Gross capital formation (% of GDP)`` |> Chart.Line |> Chart.Show
    0 // return an integer exit code

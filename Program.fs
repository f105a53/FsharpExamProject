// Learn more about F# at http://fsharp.org

open System
open FSharp.Data
open XPlot.GoogleCharts

[<EntryPoint>]
let main argv =
    CSV.survivalByAgeClass |> Chart.Show
    0 // return an integer exit code

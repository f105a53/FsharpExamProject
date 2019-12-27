﻿// Learn more about F# at http://fsharp.org

open System
open FSharp.Data
open XPlot.GoogleCharts

[<EntryPoint>]
let main argv =
    CSV.survivedByClass 
    |> Chart.WithHeight 1000    
    |> Chart.Show
    0 // return an integer exit code

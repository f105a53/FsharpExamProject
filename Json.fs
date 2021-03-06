module Json

open System
open FSharp.Data
open XPlot.GoogleCharts
open System.Collections.Generic

type TitanicPassengers = JsonProvider<"./data/json/titanic-passengers.json">

type CommodityPrices = JsonProvider<"./data/json/commodity-prices.json"> // http://pubdocs.worldbank.org/en/678281575404408706/CMO-Pink-Sheet-December-2019.pdf

type MissileTests = JsonProvider<"./data/json/north-korea-missile-test.json">


let missileSuccessRate (chartHeight: int) (chartWidth: int) =
    let missiles = MissileTests.GetSamples()

    let checkUnknown (t: string * string * int) =
        match t with
        | (n, s, i) when (n = "Unknown" && s = "Unknown") -> ("UNKNOWN_NAME", "UNKNOWN_SUCCESS_RATE", i)
        | (n, s, i) when n = "Unknown" -> ("UNKNOWN_NAME", s, i)
        | (n, s, i) when s = "Unknown" -> (n, "UNKNOWN_SUCCESS_RATE", i)
        | _ -> t

    let data =
        missiles
        |> Array.map (fun r -> (r.Fields.MissileName, r.Fields.Success))
        |> Array.groupBy (fun (n, s) -> n, s)
        |> Array.map (fun (summary, tests) -> (summary |> fst, summary |> snd, Array.length tests) |> checkUnknown)
        |> Array.sort

    data
    |> Chart.Sankey
    |> Chart.WithLabels [ "Missile"; "Result"; "Count" ]
    |> Chart.WithWidth chartWidth
    |> Chart.WithHeight chartHeight
    |> Chart.Show

let showCommodityValueLineChart (commodityList: string []) (chartHeight: int) =
    let commodities = CommodityPrices.GetSamples() |> Array.groupBy (fun c -> c.Fields.Commodity)

    let commodityValueData (commodity: string) =
        commodities
        |> Array.where (fst >> (=) commodity)
        |> Array.exactlyOne
        |> snd
        |> Array.map (fun r -> (r.Fields.Date, r.Fields.PriceIndex))
        |> Array.sortDescending

    commodityList
    |> Array.map (commodityValueData)
    |> Chart.Line
    |> Chart.WithLabels commodityList
    |> Chart.WithHeight chartHeight
    |> Chart.Show

let showTitanicAmountChart chartWidth chartHeight =
    let passengers = TitanicPassengers.GetSamples()

    let classAmountData =
        passengers
        |> Array.countBy (fun p -> p.Fields.Pclass)
        |> Array.map (fun (x, y) -> x, y :> value)

    [ classAmountData ]
    |> Chart.Column
    |> Chart.WithOptions(Options(title = "Titanic Amount per Class"))
    |> Chart.WithLabels [ "Amount" ]
    |> Chart.WithWidth chartWidth
    |> Chart.WithHeight chartHeight
    |> Chart.Show

let showTitanicAgeChart (chartWidth: int) (chartHeight: int) =
    let passengers = TitanicPassengers.Load("./data/json/titanic-passengers.json")

    let classAgeSummaryData summaryFunc =
        passengers
        |> Array.groupBy (fun p -> p.Fields.Pclass)
        |> Array.map (fun (s, ps) ->
            s,
            ps
            |> Array.choose (fun p -> p.Fields.Age)
            |> summaryFunc :> value)

    [ classAgeSummaryData Array.min
      classAgeSummaryData Array.max
      classAgeSummaryData Array.average ]
    |> Chart.Column
    |> Chart.WithOptions(Options(title = "Titanic Age per Class"))
    |> Chart.WithLabels [ "Min"; "Max"; "Avg" ]
    |> Chart.WithWidth chartWidth
    |> Chart.WithHeight chartHeight
    |> Chart.Show

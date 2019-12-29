module Json

open System
open FSharp.Data
open XPlot.GoogleCharts
open System.Collections.Generic

type TitanicPassengers = JsonProvider<"./files/titanic-passengers.json">

type CommodityPrices = JsonProvider<"./files/commodity-prices.json"> // http://pubdocs.worldbank.org/en/678281575404408706/CMO-Pink-Sheet-December-2019.pdf

// type RomanEmperors = JsonProvider<"./files/roman-emperors.json"> // https://fslab.org/XPlot/chart/google-timeline-chart.html

// let birthDeathEmperors (chartHeight: int) =
//     let emperors = RomanEmperors.GetSamples()

//     let data =
//         emperors
//         |> Array.map (fun r ->
//             (r.Fields.Name, r.Fields.ReignStart |> DateTime.Parse, r.Fields.ReignEnd |> DateTime.Parse))

//     data
//     |> Chart.Timeline
//     |> Chart.WithLabels [ "Start"; "End" ]
//     |> Chart.WithHeight chartHeight
//     |> Chart.Show

let showCommodityValueAnnotationChart (commodityList: string []) (chartHeight: int) =
    let commodities = CommodityPrices.GetSamples()

    let getCommodityIndex (arr: (string * CommodityPrices.Root []) []) (element: string) =
        arr
        |> Array.map (fun (s, r) -> s)
        |> try
            Array.findIndex ((=) element)
           with :? KeyNotFoundException -> (fun ex -> -1)

    let commodityValueData (commodity: string) =
        commodities
        |> Array.groupBy (fun c -> c.Fields.Commodity)
        |> Array.item (getCommodityIndex (commodities |> Array.groupBy (fun c -> c.Fields.Commodity)) commodity)
        |> snd
        |> Array.map (fun r -> (r.Fields.Date, r.Fields.PriceIndex))
        |> Array.sortDescending

    commodityList
    |> Array.map (commodityValueData)
    |> Chart.Line
    |> Chart.WithOptions(Options(title = "Commodity Prices"))
    |> Chart.WithLabels commodityList
    |> Chart.WithHeight chartHeight
    |> Chart.Show

let showTitanicAmountChart chartWidth chartHeight =
    let passengers = TitanicPassengers.GetSamples()

    let classAmountData =
        passengers
        |> Array.countBy (fun p -> p.Fields.Pclass)
        |> List.ofArray
        |> List.map (fun (x, y) -> x, y :> value)

    [ classAmountData ]
    |> Chart.Column
    |> Chart.WithOptions(Options(title = "Titanic Amount per Class"))
    |> Chart.WithLabels [ "Amount" ]
    |> Chart.WithWidth chartWidth
    |> Chart.WithHeight chartHeight
    |> Chart.Show

let showTitanicAgeChart chartWidth chartHeight =
    let passengers = TitanicPassengers.GetSamples()

    let classAgeSummaryData summaryFunc =
        passengers
        |> Array.groupBy (fun p -> p.Fields.Pclass)
        |> Array.map (fun (s, ps) ->
            s,
            ps
            |> Array.choose (fun p -> p.Fields.Age)
            |> summaryFunc)
        |> List.ofArray
        |> List.map (fun (x, y) -> x, y :> value)

    [ classAgeSummaryData Array.min
      classAgeSummaryData Array.max
      classAgeSummaryData Array.average ]
    |> Chart.Column
    |> Chart.WithOptions(Options(title = "Titanic Age per Class"))
    |> Chart.WithLabels [ "Min"; "Max"; "Avg" ]
    |> Chart.WithWidth chartWidth
    |> Chart.WithHeight chartHeight
    |> Chart.Show

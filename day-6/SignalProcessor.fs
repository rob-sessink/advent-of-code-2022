module SignalProcessor

open System.IO

let uniques arr = arr |> Array.distinct |> Array.length

let findMarker window datastream =
    datastream
    |> Seq.windowed window
    |> Seq.indexed
    |> Seq.pick (fun (i, segment) ->
        match uniques segment = window with
        | true -> Some(i + window, segment)
        | false -> None)

let toList file = File.ReadAllLines file |> Seq.toList

let processSignal window signals =
    signals
    |> toList
    |> List.map (findMarker window)
    |> List.head
    |> fst

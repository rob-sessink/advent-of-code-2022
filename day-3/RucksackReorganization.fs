module RucksackReorganization

open System
open System.IO

let priority c =
    match Char.IsLower(c) with
    | true -> int c - 96
    | false -> int c - 38

let toList file = File.ReadLines(file) |> Seq.toList

let toGroup content = content |> Seq.toList

let toCompartments i content = content |> toGroup |> List.splitInto i

let matchingItems (c1: char list) (c2: char list) =
    List.allPairs c1 c2
    |> List.choose (fun (t1, t2) ->
        match t1 = t2 with
        | true -> Some t1
        | false -> None)
    |> List.distinct

let matching (items: char list list) = List.reduce matchingItems items

let reorganizeGroups contentlist =
    contentlist
    |> toList
    |> List.map toGroup
    |> List.chunkBySize 3
    |> List.map (fun l3 -> l3 |> matching |> List.sumBy priority)
    |> List.sum

let reorganize contentlist =
    contentlist
    |> toList
    |> List.map (fun l -> l |> toCompartments 2 |> matching |> List.sumBy priority)
    |> List.sum

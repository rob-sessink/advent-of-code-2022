module CaloryCounting

open System.IO

type Elf = { items: int list }

let itemsByElf (previous: Elf list) item =
    let newElf = { items = List.Empty }
    let addItem elf ci = { elf with items = ci :: elf.items }

    match item with
    | "" -> newElf :: previous
    | _ ->
        match previous with
        | elf :: otherElves -> addItem elf (int item) :: otherElves
        | [] -> [ addItem newElf (int item) ]

let top3 elves =
    elves |> List.sortDescending |> List.take 3 |> List.sum

let sum elves =
    List.map (fun e -> (List.sum e.items)) elves

let max elves = List.max elves

let toList file = File.ReadLines(file) |> Seq.toList

let maxCalories inventory =
    toList inventory |> List.fold itemsByElf [] |> sum |> max

let top3Elves inventory =
    toList inventory |> List.fold itemsByElf [] |> sum |> top3

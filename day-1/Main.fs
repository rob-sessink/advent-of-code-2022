module Main

open CaloryCounting

let usage () =
    printfn $"countcalories <inventory>"
    -1

[<EntryPoint>]
let main args =
    if args = [||] || args.Length < 1 then
        usage ()
    else
        let inventory = args.[0]
        let calories = maxCalories inventory
        printfn $"Maximum of %i{calories} calories carried by one elf for inventory: %s{inventory}"
        0

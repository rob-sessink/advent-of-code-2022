module Main

open RockPaperScissors

let usage () =
    printfn $"rockpaperscissors <strategy>"
    -1

[<EntryPoint>]
let main args =
    if args = [||] || args.Length < 1 then
        usage ()
    else
        let strategy = args.[0]
        let score = calculateScore Variant.Hand strategy
        printfn $"RPS score of %i{score} for strategy: %s{strategy}"
        0

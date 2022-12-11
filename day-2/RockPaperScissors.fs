module RockPaperScissors

open System.IO

type Hand =
    | Rock
    | Paper
    | Scissors

    static member As hand =
        match hand with
        | "A"
        | "X" -> Rock
        | "B"
        | "Y" -> Paper
        | "C"
        | "Z" -> Scissors
        | _ -> invalidArg hand "Unknown shape"

    member me.Value =
        match me with
        | Rock -> 1
        | Paper -> 2
        | Scissors -> 3

type Outcome =
    | Draw
    | Loss
    | Win

    static member As outcome =
        match outcome with
        | "X" -> Loss
        | "Y" -> Draw
        | "Z" -> Win
        | _ -> invalidArg outcome "Unknown outcome"

type Score =
    | Tie of Hand * Hand
    | First of Hand * Hand
    | Second of Hand * Hand

    member private me.TieScore = 3
    member private me.WinScore = 6

    member me.Value =
        match me with
        | Tie (f, s) -> (f.Value + me.TieScore, s.Value + me.TieScore)
        | First (f, s) -> (f.Value + me.WinScore, s.Value)
        | Second (f, s) -> (f.Value, s.Value + me.WinScore)

    member me.Snd = snd me.Value

type Round =
    | ByHand of Hand * Hand
    | ByOutcome of Outcome * Hand

    member me.Play =
        match me with
        | ByHand (Rock, Rock) -> Tie(Rock, Rock)
        | ByHand (Rock, Paper) -> Second(Rock, Paper)
        | ByHand (Rock, Scissors) -> First(Rock, Scissors)
        | ByHand (Paper, Rock) -> First(Paper, Rock)
        | ByHand (Paper, Paper) -> Tie(Paper, Paper)
        | ByHand (Paper, Scissors) -> Second(Paper, Scissors)
        | ByHand (Scissors, Rock) -> Second(Scissors, Rock)
        | ByHand (Scissors, Paper) -> First(Scissors, Paper)
        | ByHand (Scissors, Scissors) -> Tie(Scissors, Scissors)
        | ByOutcome (Draw, Rock) -> Tie(Rock, Rock)
        | ByOutcome (Draw, Paper) -> Tie(Paper, Paper)
        | ByOutcome (Draw, Scissors) -> Tie(Scissors, Scissors)
        | ByOutcome (Loss, Rock) -> First(Rock, Scissors)
        | ByOutcome (Loss, Paper) -> First(Paper, Rock)
        | ByOutcome (Loss, Scissors) -> First(Scissors, Paper)
        | ByOutcome (Win, Rock) -> Second(Rock, Paper)
        | ByOutcome (Win, Paper) -> Second(Paper, Scissors)
        | ByOutcome (Win, Scissors) -> Second(Scissors, Rock)

type Variant =
    | Hand
    | Outcome

let toList file = File.ReadLines(file) |> Seq.toList

let playBy variant (round: string) =
    match round.Split(" ") with
    | [| fh; sh |] ->
        match variant with
        | Variant.Hand -> ByHand(Hand.As fh, Hand.As sh)
        | Variant.Outcome -> ByOutcome(Outcome.As sh, Hand.As fh)
    | _ -> invalidArg round "Invalid round"

let calculateScore variant strategy =
    toList strategy
    |> List.map (playBy variant)
    |> List.map (fun r -> r.Play)
    |> List.sumBy (fun s -> s.Snd)

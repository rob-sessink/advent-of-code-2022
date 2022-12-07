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
    | Loose
    | Win

    static member As outcome =
        match outcome with
        | "X" -> Loose
        | "Y" -> Draw
        | "Z" -> Win
        | _ -> invalidArg outcome "Unknown outcome"

type Score =
    | Both of Hand * Hand
    | First of Hand * Hand
    | Second of Hand * Hand

    member me.Value =
        match me with
        | Both (f, s) -> (f.Value + 3, s.Value + 3)
        | First (f, s) -> (f.Value + 6, s.Value)
        | Second (f, s) -> (f.Value, s.Value + 6)

    member me.Snd = snd me.Value

type Round =
    | ByHand of Hand * Hand
    | ByOutcome of Outcome * Hand

    member me.Play =
        match me with
        | ByHand (Rock, Rock) -> Both(Rock, Rock)
        | ByHand (Rock, Paper) -> Second(Rock, Paper)
        | ByHand (Rock, Scissors) -> First(Rock, Scissors)
        | ByHand (Paper, Rock) -> First(Paper, Rock)
        | ByHand (Paper, Paper) -> Both(Paper, Paper)
        | ByHand (Paper, Scissors) -> Second(Paper, Scissors)
        | ByHand (Scissors, Rock) -> Second(Scissors, Rock)
        | ByHand (Scissors, Paper) -> First(Scissors, Paper)
        | ByHand (Scissors, Scissors) -> Both(Scissors, Scissors)
        | ByOutcome (Draw, Rock) -> Both(Rock, Rock)
        | ByOutcome (Draw, Paper) -> Both(Paper, Paper)
        | ByOutcome (Draw, Scissors) -> Both(Scissors, Scissors)
        | ByOutcome (Loose, Rock) -> First(Rock, Scissors)
        | ByOutcome (Loose, Paper) -> First(Paper, Rock)
        | ByOutcome (Loose, Scissors) -> First(Scissors, Paper)
        | ByOutcome (Win, Rock) -> Second(Rock, Paper)
        | ByOutcome (Win, Paper) -> Second(Paper, Scissors)
        | ByOutcome (Win, Scissors) -> Second(Scissors, Rock)

type Variant =
    | Hand
    | Outcome

let toList file = File.ReadLines(file) |> Seq.toList

let playByType variant (round: string) =
    match round.Split(" ") with
    | [| fh; sh |] ->
        match variant with
        | Variant.Hand -> ByHand(Hand.As fh, Hand.As sh)
        | Variant.Outcome -> ByOutcome(Outcome.As sh, Hand.As fh)
    | _ -> invalidArg round "Invalid round"

let calculateScore variant strategy =
    toList strategy
    |> List.map (playByType variant)
    |> List.map (fun r -> r.Play)
    |> List.sumBy (fun s -> s.Snd)

module CampCleanup

open System.IO

type Section =
    { Start: int
      End: int }

    static member As(s: string) =
        match s.Split("-") with
        | [| is; ie |] -> { Start = int is; End = int ie }
        | _ -> invalidArg s "invalid section range"

    static member containedIn me other =
        match me, other with
        | me, other when me.Start >= other.Start && me.End <= other.End -> Some me
        | _ -> None

    static member overLapping me other =
        match me, other with
        | me, other when me.Start <= other.End && me.End >= other.Start -> Some me
        | _ -> None

type Assignment =
    { First: Section
      Second: Section }

    static member As(a: string) =
        match a.Split(",") with
        | [| fs; ss |] ->
            { First = Section.As fs
              Second = Section.As ss }
        | _ -> invalidArg a "invalid section assignment"

    member me.CompareBy f =
        match (f me.First me.Second), (f me.Second me.First) with
        | Some first, _ -> Some first
        | None, Some second -> Some second
        | None, None -> None

let toList sections = File.ReadAllLines sections |> Seq.toList

let countAssignmentPairsBy f sections =
    sections
    |> toList
    |> List.map Assignment.As
    |> List.choose (fun a -> a.CompareBy f)
    |> List.length

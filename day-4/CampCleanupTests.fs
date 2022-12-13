module CampCleanupTests

open CampCleanup

open Xunit
open FsUnit.Xunit

[<Fact>]
let ``Part 1: `Count containing assignment pairs using assignments-test.txt`` () =
    countAssignmentPairsBy Section.ContainedIn "assignments-test.txt"
    |> should equal 2

[<Fact>]
let ``Part 1: `Count containing assignment pairs using assignments.txt`` () =
    countAssignmentPairsBy Section.ContainedIn "assignments.txt"
    |> should equal 466

[<Fact>]
let ``Part 2 `Count overlapping assignment pairs using assignments-test.txt`` () =
    countAssignmentPairsBy Section.OverLapping "assignments-test.txt"
    |> should equal 4

[<Fact>]
let ``Part 2 `Count overlapping assignment pairs using assignments.txt`` () =
    countAssignmentPairsBy Section.OverLapping "assignments.txt"
    |> should equal 865

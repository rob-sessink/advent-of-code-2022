module RucksackReorganizationTests

open Xunit
open FsUnit.Xunit

open RucksackReorganization

[<Fact>]
let ``Part 1: Reorganize rucksuck using contentlist-test.txt`` () =
    reorganize "contentlist-test.txt" |> should equal 157

[<Fact>]
let ``Part 1: Reorganize rucksuck using contentlist.txt`` () =
    reorganize "contentlist.txt" |> should equal 7727

[<Fact>]
let ``Part 2: Reorganize rucksuck across groups using contentlist-test.txt`` () =
    reorganizeGroups "contentlist-test.txt" |> should equal 70

[<Fact>]
let ``Part 2: Reorganize rucksuck across groups using contentlist.txt`` () =
    reorganizeGroups "contentlist.txt" |> should equal 2609

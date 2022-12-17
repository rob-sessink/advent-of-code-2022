module SupplyStacksTests

open Xunit
open FsUnit.Xunit

open SupplyStacks

[<Fact>]
let ``Part 1: `Rearranging supply stacks using configuration-test.txt`` () =
    rearrange "configuration-test.txt" CraneType.CrateMover_9000 |> should equal "CMZ"

[<Fact>]
let ``Part 1: `Rearranging supply stacks using configuration.txt`` () =
    rearrange "configuration.txt" CraneType.CrateMover_9000 |> should equal "LJSVLTWQM"

[<Fact>]
let ``Part 2: `Rearranging supply stacks with CrateMover_9001 using configuration-test.txt`` () =
    rearrange "configuration-test.txt" CraneType.CrateMover_9001 |> should equal "MCD"

[<Fact>]
let ``Part 2: `Rearranging supply stacks with CrateMover_9001 using configuration.txt`` () =
    rearrange "configuration.txt" CraneType.CrateMover_9001 |> should equal "BRQWDBBJM"

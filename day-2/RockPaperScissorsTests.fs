module RockPaperScissorsTests

open FsUnit.Xunit
open Xunit

open RockPaperScissors

[<Fact>]
let ``Part 1: Calculate RPS score by Hand variant using strategy-test.txt`` () =
    calculateScore Variant.Hand "strategy-test.txt"
    |> should equal 15

[<Fact>]
let ``Part 1: Calculate RPS score by Hand variant using strategy.txt`` () =
    calculateScore Variant.Hand "strategy.txt"
    |> should equal 11666

[<Fact>]
let ``Part 2: Calculate RPS score by Outcome variant using strategy-test.txt`` () =
    calculateScore Variant.Outcome "strategy-test.txt"
    |> should equal 12

[<Fact>]
let ``Part 2: Calculate RPS score by Outcome variant using strategy.txt`` () =
    calculateScore Variant.Outcome "strategy.txt"
    |> should equal 12767

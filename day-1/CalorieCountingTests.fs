module CalorieCountingTests

open FsUnit.Xunit
open Xunit
open CaloryCounting

[<Fact>]
let ``Part 1: Start the journey, max calories for Elf via inventory-test.txt`` () =
    maxCalories "inventory-test.txt" |> should equal 24000

[<Fact>]
let ``Part 1: Start the journey, max calories for Elf via inventory.txt`` () =
    maxCalories "inventory.txt" |> should equal 69836

[<Fact>]
let ``Part 2: Start the journey, max calories for top3 Elves via inventory-test.txt`` () =
    top3Elves "inventory-test.txt" |> should equal 45000

[<Fact>]
let ``Part 2: Start the journey, max calories for top3 Elves via inventory.txt`` () =
    top3Elves "inventory.txt" |> should equal 207968

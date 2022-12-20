module SignalProcessorTests

open FsUnit.Xunit
open Xunit

open SignalProcessor

[<Fact>]
let ``Part 1: Processing datastream to find start-of-packet marker for signals-test.txt`` () =
    processSignal 4 "signals-test.txt" |> should equal 7

[<Fact>]
let ``Part 1: Processing datastream to find start-of-packet marker for signals.txt`` () =
    processSignal 4 "signals.txt" |> should equal 1658

[<Fact>]
let ``Part 2: Processing datastream to find start-of-message marker for signals-test.txt`` () =
    processSignal 14 "signals-test.txt" |> should equal 19

[<Fact>]
let ``Part 2: Processing datastream to find start-of-message marker for signals.txt`` () =
    processSignal 14 "signals.txt" |> should equal 2260

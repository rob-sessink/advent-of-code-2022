module SupplyStacks

open System
open System.Collections.Generic
open System.IO
open System.Text.RegularExpressions

type CraneType =
    | CrateMover_9000
    | CrateMover_9001
    
let (|Crate|_|) (c: char array) =
    match c[1] with
    | d when Char.IsLetter(d) -> Some d
    | _ -> None

let (|Instruction|_|) (r: string) =
    let m = Regex.Match(r, "move (\d+) from (\d+) to (\d+)")
    if m.Success then
        Some (int m.Groups[1].Value, int m.Groups[2].Value, int m.Groups[3].Value)  
    else None

type Crane =
    { stacks: Map<int, Stack<char>>; craneType: CraneType }  

    static member Init(stacks, craneType) =
        let map = seq { for i in 1 .. stacks do (i, Stack<char>()) } |> Map.ofSeq
        { stacks = map; craneType = craneType }            
    
    member me.Push (stack, crate) =
        (me.stacks.Item stack).Push(crate)
        me
        
    member me.Pop stack = (me.stacks.Item stack).Pop()        
        
    member me.Move (from, onto) = me.Push(onto, me.Pop(from))

    member me.RearrangeBy instruction  =
        match instruction, me.craneType with
        | Instruction (c, f, t), CrateMover_9000 -> me.Rearrange(c, f, t)
        | Instruction (c, f, t), CrateMover_9001 -> me.RearrangeBatch(c, f, t)
        | _ -> me
    
    member me.RearrangeBatch(crates, from, target) =
        seq { 1 .. crates }
            |> Seq.fold (fun s _ -> me.Pop(from) :: s) []
            |> Seq.iter (fun c -> me.Push(target, c) |> ignore) 
        me                                
    
    member me.Rearrange(crates, from, target) =
        for _ in 1 .. crates do me.Move(from, target) |> ignore 
        me                                

    member me.Tops =
        (me.stacks.Values, [])
        ||> Seq.foldBack (fun stack s -> stack.Peek() :: s) 
        |> Seq.toArray
        |> String

let initCrane craneType (configuration: string list) =
    let stacks, crates = configuration |> List.rev |> List.splitAt 1 
    let nst =
        stacks.Head
        |> Seq.chunkBySize 4
        |> Seq.length  
        
    let crane = Crane.Init(nst, craneType)

    let stackCrate (crane: Crane) i c = 
        match c with
        | Crate c -> crane.Push(i, c)
        | _ -> crane
            
    (crane, crates)
    ||> List.fold(fun crane crate ->
        crate 
        |> Seq.chunkBySize 4
        |> Seq.iteri (fun i crate -> stackCrate crane (i+1) crate |> ignore) 
        crane) 

let parseConfiguration craneType config  =
    let i = config |> List.findIndex (fun l -> l = "")
    let craneconfig, procedures = List.splitAt i config
    (initCrane craneType craneconfig), procedures[1..]   
    
let toList file = File.ReadAllLines file |> Seq.toList

let rearrange configuration craneType =
    let crane = 
        toList configuration
        |> parseConfiguration craneType
        ||> List.fold (fun c -> c.RearrangeBy)    
    crane.Tops
// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO
open Shared

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let rawInput = File.ReadAllText("input")
    let input = ParseRaw rawInput
    printfn "task1: %d" (task1.run input)
    task2.run "task2 result"
    0 // return an integer exit code
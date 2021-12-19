// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let rawInput = File.ReadAllText("input")
    task1.run "task1 result"
    task2.run "task2 result"
    0 // return an integer exit code
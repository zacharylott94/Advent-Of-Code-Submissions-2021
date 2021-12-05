// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO
open FSharp.Core
open Board


let splitIntoNumbersAndBoardStrings (str:string) = 
    str.Split("\n\n")
    |> fun x -> (Array.head x, Array.tail x)
    |> fun (x,y) -> (x.Split(","), y)
    |> fun (x,y) -> (Array.map (int) x, y)

let tupleMapSecond func (x,y) = (x, func y)
let tms = tupleMapSecond

[<EntryPoint>]
let main argv =
    let file = File.ReadAllText("input")
    file
    |> splitIntoNumbersAndBoardStrings
    |> tms (Array.map Board.parseString)
    |> ignore
    0 // return an integer exit code
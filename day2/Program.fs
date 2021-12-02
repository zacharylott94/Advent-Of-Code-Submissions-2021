// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO

type Command =
    | Forward of int
    | Up of int
    | Down of int

type Position = int * int


let parse (str:string) =
    str.Split()
    |> fun x -> (x.[0], x.[1])
    |> fun (command, value) -> 
        match command with
        | "down" -> Down (int value)
        | "up" -> Up (int value)
        | "forward" -> Forward (int value)
        | _ -> failwith "Invalid command"

let doCommand (h, d) command =
    match command with
    | Up x -> (h, d - x)
    | Down x -> (h, d + x)
    | Forward x -> (h + x, d)

[<EntryPoint>]
let main argv =
    let lines = File.ReadAllLines("input")
    lines
    |> Array.map parse
    |> Array.fold (doCommand) (0,0)
    // |> fun (h, d) -> h * d
    |> fun (h,d) -> printfn "Horizontal:%d * Depth:%d = %d" h d (h*d)
    

    0 // return an integer exit code
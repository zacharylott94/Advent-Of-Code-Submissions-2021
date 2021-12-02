// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO

type Command =
    | Forward of int
    | Up of int
    | Down of int

type Position = int * int * int


let parse (str:string) =
    str.Split()
    |> fun x -> (x.[0], x.[1])
    |> fun (command, value) -> 
        match command with
        | "down" -> Down (int value)
        | "up" -> Up (int value)
        | "forward" -> Forward (int value)
        | _ -> failwith "Invalid command"

let doCommand ((h, d, a):Position) (command:Command) : Position =
    match command with
    | Up x -> (h, d, a - x)
    | Down x -> (h, d, a + x)
    | Forward x -> (h + x, d + (a*x), a)

[<EntryPoint>]
let main argv =
    let lines = File.ReadAllLines("input")
    lines
    |> Array.map parse
    |> Array.fold (doCommand) (0,0,0)
    // |> fun (h, d) -> h * d
    |> fun (h,d,a) -> printfn "Horizontal:%d * Depth:%d = %d" h d (h*d)
    

    0 // return an integer exit code
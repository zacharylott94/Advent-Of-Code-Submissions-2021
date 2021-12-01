// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System



[<EntryPoint>]
let main argv =
    let lines = IO.File.ReadAllLines("input")
    lines
    |> Array.map int
    |> Array.pairwise
    |> Array.filter (fun (x,y) -> x < y) 
    |> fun x -> x.Length
    |> printfn "number of measurements larger than previous: %d" 
    0 // return an integer exit code
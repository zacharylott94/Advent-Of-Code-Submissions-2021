// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System


let front array = 
    array
    |> Array.rev
    |> Array.tail
    |> Array.rev

let triad array = 
    let t = Array.tail
    let f = front
    Array.zip3 ((f >> f) array) ((t >> f) array) ((t >> t) array)



[<EntryPoint>]
let main argv =
    let lines = IO.File.ReadAllLines("input")
    lines
    |> Array.map int
    |> triad
    |> Array.map (fun (x,y,z) ->  x + y + z)
    |> Array.pairwise
    |> Array.filter (fun (x,y) -> x < y) 
    |> fun x -> x.Length
    |> printfn "number of measurements larger than previous: %d" 
    0 // return an integer exit code
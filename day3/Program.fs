// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO
open System
open FSharp.Core

type Bit = 
    |One 
    |Zero
type BinaryNumber = Bit[]

let parse (str:string) : BinaryNumber =
    str.ToCharArray()
    |> Array.map (function
        | '1' -> One
        | '0' -> Zero
        | _ -> failwith "Not 1 or 0"
            )
let decideBit (bits:Bit[]): Bit =
    bits
    |> Array.fold (fun (ones, zeros) bit -> 
        match bit with
        | One -> (ones + 1, zeros)
        | Zero -> (ones, zeros + 1)
        ) (0,0)
    |> fun (ones, zeros) -> if (ones > zeros) then One else Zero     
 
let parseStringToNumber (str:string) =
    str.ToCharArray()
    |> Array.rev
    |> Array.indexed
    |> Array.map (fun (i,b) -> ((string >> int) b)<<<i)
    |> Array.sum

let flipBits (bits:Bit[]): Bit[] =
    Array.map (function
        | One -> Zero
        | Zero -> One) bits
let bitsToString bits =
    Array.fold (fun str bit ->
        match bit with
        | One -> str+"1"
        | Zero -> str+"0"
    ) ""  bits  

[<EntryPoint>]
let main argv =
    File.ReadAllLines("input")
    |> Array.map parse
    |> Array.transpose
    |> Array.map decideBit
    |> fun x -> (x, flipBits x)
    |> fun (x,y) -> Array.map bitsToString [|x;y|]
    |> Array.map parseStringToNumber
    |> Array.reduce (*)
    |> printfn "final result: %d"

    0 // return an integer exit code
// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.IO
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
let mostCommonBitAt index (nums:BinaryNumber[]): Bit =
    nums
    |> Array.fold (fun (ones, zeros) num -> 
        match num.[index] with
        | One -> (ones + 1, zeros)
        | Zero -> (ones, zeros + 1)
        ) (0,0)
    |> fun (ones, zeros) -> if (ones >= zeros) then One else Zero     

let flipBit =
    function
    | One -> Zero
    | Zero -> One

let leastCommonBitAt index = mostCommonBitAt index >> flipBit
 
let parseStringToNumber (str:string) =
    str.ToCharArray()
    |> Array.rev
    |> Array.indexed
    |> Array.map (fun (i,b) -> ((string >> int) b)<<<i)
    |> Array.sum


let bitsToString bits =
    Array.fold (fun str bit ->
        match bit with
        | One -> str+"1"
        | Zero -> str+"0"
    ) ""  bits  

let binaryToDecimal = bitsToString >> parseStringToNumber

let rec filterAndRepeat (index:int) (criteria:int -> BinaryNumber[] -> Bit) (nums:BinaryNumber[]) =
    let criteriaBit = criteria index nums
    nums
    |> Array.filter (fun x -> x.[index] = criteriaBit)
    |> function
        | [|num|] -> num
        | [||] -> failwith "Empty Array"
        | x -> filterAndRepeat (index+1) criteria x

let oxygenRating = filterAndRepeat 0 mostCommonBitAt
let co2ScrubbingRating = filterAndRepeat 0 leastCommonBitAt 

[<EntryPoint>]
let main argv =
    File.ReadAllLines("input")
    |> Array.map parse
    |> fun x -> [|oxygenRating x; co2ScrubbingRating x|]
    |> Array.map (binaryToDecimal)
    |> Array.reduce (*)
    |> printfn "final result: %d"

    0 // return an integer exit code
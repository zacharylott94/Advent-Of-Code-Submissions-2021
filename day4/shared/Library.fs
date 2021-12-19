module Shared
open System
type Spot = 
    | Marked of int
    | Unmarked of int

type Board = Spot[][]

module Board =
    let toString (board:Board) = //currently only used to equate boards
        Array.concat board
        |> Array.map (function 
                        | Marked x -> string x + "*"
                        | Unmarked x -> string x
                        )
        |> String.concat " "
    let Equals (a:Board) (b:Board) =
        toString a = toString b

    let fromString (str:string) : Board =
        str.Split('\n') //to row Strings
        |> Array.map (fun rowString -> 
                        rowString.Split(' ', System.StringSplitOptions.RemoveEmptyEntries) 
                            |>  Array.map int 
                            |> Array.map (fun num -> Unmarked num)
                        )

let (<.) (map: 'a -> 'c) ((first, second): 'a * 'b) = (map first, second)
let (.>) (map: 'b -> 'c) ((first, second): 'a * 'b) = (first, map second)


let ParseRaw (str:string) : int[]* Board[] = 
    str.Split("\n\n")
    |> fun x -> (Array.head x, Array.tail x)
    |> (<.) (fun first -> first.Split(","))
    |> (<.) (Array.map int)
    |> (.>) (fun x -> Array.map Board.fromString x)

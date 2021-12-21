module Shared
open System
type Spot = 
    | Marked of int
    | Unmarked of int

type Board = Spot[][]

module Row =
    let mark (num: int) (row: Spot[]) :Spot[] =
        Array.map (function 
                    | Unmarked x when x = num -> Marked x
                    | x -> x) row

module Board =
    let fromString (str:string) : Board =
        str.Split('\n',System.StringSplitOptions.RemoveEmptyEntries) //to row Strings
        |> Array.map (fun rowString -> 
                        rowString.Split(' ', System.StringSplitOptions.RemoveEmptyEntries) 
                            |>  Array.map int 
                            |> Array.map (fun num -> Unmarked num)
                        )
    let mark (num: int) (board: Board) : Board =
        board |> Array.map (fun row -> (Row.mark num row))
    
    let isWinner (board: Board) : bool =
        Array.concat [|board; Array.transpose board|] //transform board into a list of Rows and Columns
        |> Array.exists 
            (Array.forall 
                (function 
                    | Marked _ -> true
                    | _ -> false
                )
            )

    let score (lastCalled: int) (winningBoard: Board) : int =
        winningBoard
        |> Array.concat
        |> Array.map (function
                        | Marked _ -> 0
                        | Unmarked x -> x)
        |> Array.sum
        |> (*) lastCalled

let (<.) (map: 'a -> 'c) ((first, second): 'a * 'b) = (map first, second)
let (.>) (map: 'b -> 'c) ((first, second): 'a * 'b) = (first, map second)


let ParseRaw (str:string) : int[]* Board[] = 
    str.Split("\n\n")
    |> fun x -> (Array.head x, Array.tail x)
    |> (<.) (fun first -> first.Split(","))
    |> (<.) (Array.map int)
    |> (.>) (fun x -> Array.map Board.fromString x)

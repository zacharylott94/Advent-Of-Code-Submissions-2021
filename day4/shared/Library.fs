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
        |> Array.map (fun row -> 
            row
            |> Array.map (function 
                            | Marked x -> true
                            | Unmarked x -> false) //transform marked or unmarked spots to true or false
            |> Array.reduce (&&) //true if every spot in row marked
        )
        |> Array.reduce (||) //true if at least one row or column is complete
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

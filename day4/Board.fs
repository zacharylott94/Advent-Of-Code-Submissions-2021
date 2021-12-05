namespace Board
type Spot =
    | Marked of int
    | Unmarked of int

type Board = Spot[,]

module Board =
  open System.Text.RegularExpressions



  let parseString (str:string):Board =
      str.Split("\n") //Into Rows
      |> (Array.rev >> Array.tail >> Array.rev) //remove empty string at the end of the array
      |> Array.map (fun row -> row.Trim())
      |> Array.map (fun row -> Regex.Split(row, "[\t\r\n\s]+"))
      |> array2D
      |> Array2D.map (int >> Unmarked)

  let markNumber (num:int) (board:Board) : Board =
    let matchNumber num spot =
      match spot with
      | Unmarked x -> num = x
      | Marked x -> num = x

    let markSpot = function
                    | Unmarked x -> Marked x
                    | x -> x
    board
    |> Array2D.map (fun x -> (if matchNumber num x then markSpot x else x))

  let countMarked board : int =
    board
    |> Array2d.fold2 (fun x y -> match y with 
                                  | Marked n -> x + 1
                                  | _ -> x) 0
  let toNestedArray (arr: 'T [,]) = 
    arr 
    |> Seq.cast<'T> 
    |> Seq.toArray
    |> Array.chunkBySize 5

  let SpotsToBool board =
    board |> Array2D.map (function
                          | Marked _-> true
                          | _ -> false)

  let isWinner (board:Board) : bool =
    board
    |> SpotsToBool
    |> toNestedArray 
    |> fun x -> Array.concat [x; (Array.transpose x)] //list of rows and columns
    |> Array.map (fun rc -> Array.fold (&&) true rc)
    |> Array.fold (||) false


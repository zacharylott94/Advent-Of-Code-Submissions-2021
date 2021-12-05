module Board
open System.Text.RegularExpressions

type Spot =
    | Marked of int
    | Unmarked of int

type Board = Spot[,]

let parseString (str:string):Board =
    str.Split("\n") //into Rows
    |> Array.map (fun row -> row.Trim())
    |> Array.map (fun row -> Regex.Split(row, "[\t\r\n\s]+"))
    |> array2D
    |> Array2D.map (int >> Unmarked)


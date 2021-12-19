module task1
open Shared

let rec run ((nums, boards): int[] * Board[]) : int =
    let next = Array.head nums
    let markNext: Board -> Board = Board.mark next
    let markedBoards = boards |> Array.map markNext

    markedBoards
    |> Array.filter Board.isWinner
    |> function 
        | [|winner|] -> Board.score next winner
        | [||] -> run (Array.tail nums, markedBoards)
        | _ -> failwith "No idea what happened"
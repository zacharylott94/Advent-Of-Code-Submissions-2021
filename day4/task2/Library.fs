module task2
open Shared

let rec playLast ((nums, board): int[] * Board): int =
    let next = Array.head nums
    let markedBoard = board |> Board.mark next

    if (Board.isWinner markedBoard) 
        then (Board.score next markedBoard)
        else (playLast (Array.tail nums, markedBoard))


let rec run ((nums, boards): int[] * Board[]) : int =
    let next = Array.head nums
    let markNext: Board -> Board = Board.mark next
    let markedBoards = boards |> Array.map markNext

    markedBoards
    |> Array.filter (not << Board.isWinner)
    |> function 
        | [|last|] -> playLast (Array.tail nums, last)
        | xs -> run (Array.tail nums, xs)



module Array2d
let fold2 (folder:'U -> 'T -> 'U) (state:'U) (arr2d:'T[,]) : 'U =
  let mutable result = state
  Array2D.iter (fun t -> (result <- (folder result t)) ) arr2d

  result


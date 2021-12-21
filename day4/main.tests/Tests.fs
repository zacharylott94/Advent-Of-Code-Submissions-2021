namespace main.tests

open System.IO
open Microsoft.VisualStudio.TestTools.UnitTesting
open task1
open Shared

[<TestClass>]
type SharedTests () =

    [<TestMethod>]
    member this.ParseRaw () =
        let rawInput = File.ReadAllText("truncatedInput")
        let firstBoard = 
            [|
                [|3; 55; 15; 54; 81|]
                [|56; 77; 20; 99; 25|]
                [|90; 57; 67; 0; 97|]
                [|28; 45; 69; 84; 14|]
                [|91; 94; 39; 36; 85|]
            |] 
            |> Array.map (fun row -> Array.map (fun num -> Unmarked num) row)
        
        let secondBoard = 
            [|
                [|52; 60; 30; 7; 36|]
                [|71; 97; 77; 19; 46|]
                [|6; 3; 75; 82; 24|]
                [|4; 57; 2; 11; 91|]
                [|56; 84; 23; 43; 48|]
            |] 
            |> Array.map (fun row -> Array.map (fun num -> Unmarked num) row)

        let expectedNums = [|87;7;82;21;47;88;12;71;24;35;10;90;4;97;30;55;36;74;19;50;23;46;13;44;69;27;2;0;37;33;99;49;77;15;89;98;31;51;22;96;73;94;95;18;52;78;32;83;85;54;75;84;59;25;76;45;20;48;9;28;39;70;63;56;5;68;61;26;58;92;67;53;43;62;17;81;80;66;91;93;41;64;14;8;57;38;34;16;42;11;86;72;40;65;79;6;3;29;60;1|]

        let parsed = Shared.ParseRaw rawInput

        //check to see if our first tuple value is the expected array of int
        let compareNums = 
            Array.zip expectedNums (fst parsed) 
            |> Array.map (fun (x,y) -> x = y)
            |> Array.reduce (&&)
        Assert.IsTrue(compareNums)

        let parsedFirst = (snd parsed).[0]
        let parsedSecond = (snd parsed).[1]
        //check to see if the boards were parsed correctly
        Assert.IsTrue((parsedFirst = firstBoard))
        Assert.IsTrue((parsedSecond = secondBoard))

    [<TestMethod>]
    member this.markBoard () =
        let unmarkedBoard = 
            [|
                [|3; 55; 15; 54; 81|]
                [|56; 77; 20; 99; 25|]
                [|90; 57; 67; 0; 97|]
                [|28; 45; 69; 84; 14|]
                [|91; 94; 39; 36; 85|]
            |] 
            |> Array.map (fun row -> Array.map (fun num -> Unmarked num) row)
        
        let expectedBoard =
            [|
                [|Unmarked 3; Marked 55; Unmarked 15; Unmarked 54; Unmarked 81|]
                [|Unmarked 56; Unmarked 77; Unmarked 20; Unmarked 99; Unmarked 25|]
                [|Unmarked 90; Unmarked 57; Marked 67; Unmarked 0; Unmarked 97|]
                [|Unmarked 28; Unmarked 45; Unmarked 69; Unmarked 84; Unmarked 14|]
                [|Unmarked 91; Unmarked 94; Unmarked 39; Marked 36; Unmarked 85|]
            |] 
        
        let test = 
            unmarkedBoard
            |> Board.mark 55
            |> Board.mark 67
            |> Board.mark 36
            |> Board.mark 1 //this number doesn't exist on the board and should not change the board
            |> (=) expectedBoard
        Assert.IsTrue(test)
    
    [<TestMethod>]
    member this.checkBoardForBingo () =
        let winningRowBoard =
            [|
                [|Marked 3; Marked 55; Marked 15; Marked 54; Marked 81|]
                [|Unmarked 56; Unmarked 77; Unmarked 20; Unmarked 99; Unmarked 25|]
                [|Unmarked 90; Unmarked 57; Marked 67; Unmarked 0; Unmarked 97|]
                [|Unmarked 28; Unmarked 45; Unmarked 69; Unmarked 84; Unmarked 14|]
                [|Unmarked 91; Unmarked 94; Unmarked 39; Marked 36; Unmarked 85|]
            |]          
        let winningColumnBoard =
            [|
                [|Unmarked 3; Marked 55; Unmarked 15; Unmarked 54; Unmarked 81|]
                [|Unmarked 56; Marked 77; Unmarked 20; Unmarked 99; Unmarked 25|]
                [|Unmarked 90; Marked 57; Marked 67; Unmarked 0; Unmarked 97|]
                [|Unmarked 28; Marked 45; Unmarked 69; Unmarked 84; Unmarked 14|]
                [|Unmarked 91; Marked 94; Unmarked 39; Marked 36; Unmarked 85|]
            |] 
        let nonWinningBoard =
            [|
                [|Marked 3; Unmarked 55; Unmarked 15; Unmarked 54; Unmarked 81|]
                [|Unmarked 56; Marked 77; Unmarked 20; Marked 99; Marked 25|]
                [|Marked 90; Marked 57; Marked 67; Unmarked 0; Marked 97|]
                [|Marked 28; Marked 45; Marked 69; Marked 84; Unmarked 14|]
                [|Unmarked 91; Unmarked 94; Unmarked 39; Unmarked 36; Marked 85|]
            |] 
        
        Assert.IsTrue(Board.isWinner winningRowBoard) //complete Row can win
        Assert.IsTrue(Board.isWinner winningColumnBoard) //complete Column can win
        Assert.IsFalse(Board.isWinner nonWinningBoard) //Incomplete board is not a winner

    [<TestMethod>]
    member this.ScoreBoard () =
        let winningRowBoard =
            [|
                [|Marked 3; Marked 55; Marked 15; Marked 54; Marked 81|]
                [|Unmarked 56; Unmarked 77; Unmarked 20; Unmarked 99; Unmarked 25|]
                [|Unmarked 90; Unmarked 57; Marked 67; Unmarked 0; Unmarked 97|]
                [|Unmarked 28; Unmarked 45; Unmarked 69; Unmarked 84; Unmarked 14|]
                [|Unmarked 91; Unmarked 94; Unmarked 39; Marked 36; Unmarked 85|]
            |]
        let lastCalled = 36
        let sumOfUnmarked = 1070
        let expectedScore = sumOfUnmarked * lastCalled

        Assert.AreEqual(Board.score lastCalled winningRowBoard, expectedScore)

[<TestClass>]
type Task1Tests () =
    
    [<TestMethod>]
    member this.task1 () =
        let inputs: (int[] * Board[]) = ([|52; 60; 3; 55; 71; 6; 4; 56; 11; 13; 14|],
            [|
                [|
                    [|3; 55; 15; 54; 81|]
                    [|56; 77; 20; 99; 25|]
                    [|90; 57; 67; 0; 97|]
                    [|28; 45; 69; 84; 14|]
                    [|91; 94; 39; 36; 85|]
                |] 
                |> Array.map (fun row -> Array.map (fun num -> Unmarked num) row)
            
                [|
                    [|52; 60; 30; 7; 36|]
                    [|71; 97; 77; 19; 46|]
                    [|6; 3; 75; 82; 24|]
                    [|4; 57; 2; 11; 91|]
                    [|56; 84; 23; 43; 48|]
                |] 
                |> Array.map (fun row -> Array.map (fun num -> Unmarked num) row)
            |])
        
        let expectedOutput = 56 * 852

        Assert.AreEqual(task1.run inputs, expectedOutput)
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

        //check to see if our first tuple value is an array of int and check to see if the first element of that array is 87
        let compareNums = 
            Array.zip expectedNums (fst parsed) 
            |> Array.map (fun (x,y) -> x = y)
            |> Array.reduce (&&)
        Assert.IsTrue(compareNums)

        let parsedFirst = (snd parsed).[0]
        let parsedSecond = (snd parsed).[1]
        printfn "parsed first board: %s" (Board.toString parsedFirst)
        printfn "expected first board: %s" (Board.toString firstBoard)
        Assert.IsTrue(Board.Equals parsedFirst firstBoard)
        Assert.IsTrue(Board.Equals parsedSecond secondBoard)

namespace main.tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open task1

[<TestClass>]
type Task1 () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        Assert.IsTrue(true);

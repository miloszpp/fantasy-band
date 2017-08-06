module BillboardIntegrationTests

open Microsoft.VisualStudio.TestTools.UnitTesting

open BillboardIntegration
open CommonLibrary
open Result

[<TestClass>]
type TestSpotifyIntegration() =
  [<TestMethod>]
  member this.``getBandNames does not crash``() =
    let bandNames = getBandNames()
    ()

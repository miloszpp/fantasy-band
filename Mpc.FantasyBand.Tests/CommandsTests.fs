module CommandsTests

open Microsoft.VisualStudio.TestTools.UnitTesting
open CommonLibrary
open Commands
open Result

[<TestClass>]
type TestCommands() =
  [<TestMethod>]
  member this.``importSpotifyBandQuotes does not crash``() =
    result {
      let context = Repository.getContext()
      let! quotes = importSpotifyBandQuotes context 50
      return ()
    } |> ignore
  
  [<TestMethod>]
  member this.``importBillboardBands does not crash``() =
    result {
      let context = Repository.getContext()
      let! quotes = importBillboardBands context
      return ()
    } |> ignore
module SpotifyIntegrationTests

open Microsoft.VisualStudio.TestTools.UnitTesting

open SpotifyIntegration
open CommonLibrary
open Result

[<TestClass>]
type TestSpotifyIntegration() =
  [<TestMethod>]
  member this.``getManyArtists does not crash``() =
    let artists = result {
      let! token = getToken()
      let! response = getManyArtists ["2d0hyoQ5ynDBnkvAbJKORj"; "1fSzW5cXBmquli5laFnoGY"] token.AccessToken
      return response.Artists
    }
    ()

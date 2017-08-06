module Commands

open RepositoryTypes
open Repository
open Validation
open CommonLibrary
open CommandTypes
open SpotifyIntegration
open Result
open System.Threading

let createBand (ctx: DbContext) =
  validateBand
  >> bind (createBand ctx)

let artistToCreateBandCommand (artist: SpotifyIntegration.Artist) =
  {
    Name = artist.Name
    SpotifyId = artist.Id
    Image = artist.Images |> List.tryHead |> Option.map (fun image -> image.Url)
    SpotifyUrl = artist.Href
    Genres = artist.Genres
  }

let importSpotifyBand (ctx: DbContext) (cmd: ImportSpotifyBandCommand) =
  let token = SpotifyIntegration.getToken()
  let getSpotifyId cmd = cmd.SpotifyId
  result {
    let! token = SpotifyIntegration.getToken()
    let! artist = SpotifyIntegration.getArtist cmd.SpotifyId token.AccessToken
    let createBandCmd = artistToCreateBandCommand artist
    return createBand ctx createBandCmd
  }

let importSpotifyBandQuotes (ctx: DbContext) batchSize =
  let bands = getBands ctx
  let bandsChunks = Seq.split batchSize bands
  let now = System.DateTime.Now
  let getQuotes token (bandsChunk: Band list) = result {
    let bandSpotifyIds = bandsChunk |> Seq.map (fun band -> band.SpotifyId) |> Seq.toList
    let! response = SpotifyIntegration.getManyArtists bandSpotifyIds token.AccessToken
    return! List.zip bandsChunk response.Artists
      |> List.map (fun (band, artist) -> { BandId = band.Id; Popularity = artist.Popularity; Followers = artist.Followers.Total; Date = now }) 
      |> List.map (createQuote ctx)
      |> Result.flattenConcatErrors
  }
  result {
    let! token = SpotifyIntegration.getToken()
    let! resultChunks = bandsChunks |> Seq.map (getQuotes token) |> Result.flattenConcatErrors
    return resultChunks |> Seq.concat
  }
  
let importBillboardBands (ctx: DbContext) =
  let bandNames = BillboardIntegration.getBandNames()
  result {
    let! token = SpotifyIntegration.getToken()
    let bands = 
      bandNames 
      |> Seq.map (fun name -> Thread.Sleep(500); SpotifyIntegration.searchArtists name token.AccessToken)
      |> Seq.choose (function | Ok(value) -> Some(value) | Error(_) -> None)
      |> Seq.map (fun response -> Seq.head response.Artists.Items)
      |> Seq.map (fun artist -> artistToCreateBandCommand artist |> createBand ctx)
    return! Result.flattenConcatErrors bands
  }
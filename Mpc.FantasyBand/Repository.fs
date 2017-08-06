module Repository

open System
open CommonLibrary
open CommandTypes
open RepositoryTypes
open Result

let getBands (ctx: DbContext): Band list =
  ctx.Dbo.Band |> Seq.toList

let createBand (ctx: DbContext) (cmd: CreateBandCommand): Result<Band, string> =
  let row = ctx.Dbo.Band.Create()
  row.Id <- Guid.NewGuid()
  row.Name <- cmd.Name
  row.SpotifyId <- cmd.SpotifyId
  row.SpotifyUrl <- cmd.SpotifyUrl
  row.Created <- DateTime.Now
  row.Modified <- DateTime.Now
  row.Image <- cmd.Image
  let safeUpdate = 
    tryCatch (fun () -> ctx.SubmitUpdates(); row) (fun ex -> ex.Message)
  safeUpdate()

let createQuote (ctx: DbContext) (cmd : CreateQuoteCommand): Result<BandPopularityQuote, string> =
  let row = ctx.Dbo.BandPopularityQuote.Create()
  row.BandId <- cmd.BandId
  row.Popularity <- cmd.Popularity
  row.Followers <- cmd.Followers
  row.Date <- cmd.Date
  let safeUpdate = 
    tryCatch (fun () -> ctx.SubmitUpdates(); row) (fun ex -> ex.Message)
  safeUpdate()

let getBandQuotes (bandId: Guid) (ctx: DbContext): BandPopularityQuote list =
  ctx.Dbo.BandPopularityQuote |> Seq.where (fun q -> q.BandId = bandId) |> Seq.toList

let getContext() = Sql.GetDataContext()

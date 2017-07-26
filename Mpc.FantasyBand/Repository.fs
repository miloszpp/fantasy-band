module Repository

open System
open CommonLibrary
open CommandTypes
open RepositoryTypes

let getBands (ctx: DbContext): Band list =
  ctx.Dbo.Band |> Seq.toList

let createBand (ctx: DbContext) (cmd: CreateBandCommand): Result<Band, string> =
  let row = ctx.Dbo.Band.Create()
  row.Id <- Guid.NewGuid()
  row.Name <- cmd.Name
  row.ShortDescription <- cmd.ShortDescription
  row.Created <- DateTime.Now
  row.Modified <- DateTime.Now
  let safeUpdate = 
    tryCatch (fun () -> ctx.SubmitUpdates(); row) (fun ex -> ex.Message)
  safeUpdate()

let getBandQuotes (bandId: Guid) (ctx: DbContext): BandPopularityQuote list =
  ctx.Dbo.BandPopularityQuote |> Seq.where (fun q -> q.BandId = bandId) |> Seq.toList

let getContext() = Sql.GetDataContext()

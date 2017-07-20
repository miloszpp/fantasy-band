module Dal

open FSharp.Data.Sql
open System

let [<Literal>] connectionString = 
  "Server=.\SQLEXPRESS01;Database=Mpc_FantasyBand;Trusted_Connection=True;MultipleActiveResultSets=true"

type Sql = 
    SqlDataProvider<ConnectionString = connectionString, DatabaseVendor=Common.DatabaseProviderTypes.MSSQLSERVER, UseOptionTypes = true>
  
type DbContext = Sql.dataContext

type Band = DbContext.``dbo.BandEntity``

type BandPopularityQuote = DbContext.``dbo.BandPopularityQuoteEntity``

let getBands (ctx: DbContext): Band list =
  ctx.Dbo.Band |> Seq.toList

let getBandQuotes (bandId: Guid) (ctx: DbContext): BandPopularityQuote list =
  ctx.Dbo.BandPopularityQuote |> Seq.where (fun q -> q.BandId = bandId) |> Seq.toList

let getContext() = Sql.GetDataContext()

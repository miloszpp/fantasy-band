module RepositoryTypes

open FSharp.Data.Sql

let [<Literal>] connectionString = 
  "Server=.\SQLEXPRESS01;Database =Mpc_FantasyBand;Trusted_Connection=True;MultipleActiveResultSets=true"

type Sql = 
    SqlDataProvider<ConnectionString = connectionString, DatabaseVendor=Common.DatabaseProviderTypes.MSSQLSERVER, UseOptionTypes = true>
  
type DbContext = Sql.dataContext

type Band = DbContext.``dbo.BandEntity``

type BandPopularityQuote = DbContext.``dbo.BandPopularityQuoteEntity``


module Queries

open System
open RepositoryTypes
open SpotifyIntegration
open Result

type BandListItem = {
  Id: Guid;
  Name: string
}

let getBands (ctx: DbContext) =
  Repository.getBands ctx |> Seq.map (fun l -> l.MapTo<BandListItem>())

let searchBandsSpotify searchString =
  let token = SpotifyIntegration.getToken()
  bind (fun t -> SpotifyIntegration.searchArtists searchString t.AccessToken) token
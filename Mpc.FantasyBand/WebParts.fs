module WebParts

open Suave
open Suave.Json
open WebPartUtils

let bandList = warbler (fun _ ->
  Repository.getContext()
  |> Queries.getBands
  |> JSON
)

let bandSearch searchStr = request (fun r ->
  Queries.searchBandsSpotify searchStr
  |> JSON
)

let bandCreate = wrapCommand Commands.createBand

let bandImport = wrapCommand Commands.importSpotifyBand
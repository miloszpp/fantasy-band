module WebParts

open Suave
open Suave.Json
open WebPartUtils

let bandList = warbler (fun _ ->
  Repository.getContext()
  |> Queries.getBands
  |> JSON
)

let bandCreate = wrapCommand Commands.createBand
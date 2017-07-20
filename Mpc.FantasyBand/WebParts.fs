module WebParts

open Suave
open Suave.Json
open WebPartCombinators

let bandList = warbler (fun _ ->
  Dal.getContext()
  |> Queries.getBands
  |> JSON
)
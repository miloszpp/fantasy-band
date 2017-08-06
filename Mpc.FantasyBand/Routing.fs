module Routing

open Suave
open Suave.Filters
open Suave.Operators

let app =
  choose 
    [
    path "/band" >=> GET >=> WebParts.bandList
    pathScan "/band-search/%s" WebParts.bandSearch >=> GET 
    path "/band" >=> POST >=> WebParts.bandCreate
    path "/band-import" >=> POST >=> WebParts.bandImport
    ]
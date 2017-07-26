module Routing

open Suave
open Suave.Filters
open Suave.Operators

let app =
  choose 
    [
    path "/band" >=> GET >=> WebParts.bandList
    path "/band" >=> POST >=> WebParts.bandCreate
    ]
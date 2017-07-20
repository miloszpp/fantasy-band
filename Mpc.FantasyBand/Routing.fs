module Routing

open Suave
open Suave.Filters
open Suave.Operators

let app =
  choose 
    [
    path "/bands" >=> GET >=> WebParts.bandList
    ]
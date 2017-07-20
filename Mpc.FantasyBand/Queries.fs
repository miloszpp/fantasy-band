module Queries

open Dal
open Dtos

let getBands (ctx: DbContext) =
  Dal.getBands ctx |> Seq.map (fun l -> l.MapTo<BandListItem>())
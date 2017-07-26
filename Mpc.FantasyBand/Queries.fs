module Queries

open System
open RepositoryTypes

type BandListItem = {
  Id: Guid;
  Name: string
}

let getBands (ctx: DbContext) =
  Repository.getBands ctx |> Seq.map (fun l -> l.MapTo<BandListItem>())
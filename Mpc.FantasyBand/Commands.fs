module Commands

open RepositoryTypes
open Repository
open Validation
open CommonLibrary

let createBand (ctx: DbContext) =
  validateBand
  >> bind (createBand ctx)
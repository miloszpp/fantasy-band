module Validation

open CommonLibrary
open CommandTypes
open Result

let private validateName item =
  if not <| String.isEmpty item.Name then Ok item
  else Error "Band name must not be empty"
 
let private (&&&) v1 v2 = 
  let addSuccess r1 r2 = r1 // return first
  let addFailure s1 s2 = s1 + "; " + s2  // concat
  plus addSuccess addFailure v1 v2 

let validateBand: CreateBandCommand -> Result<CreateBandCommand, string>
  = validateName
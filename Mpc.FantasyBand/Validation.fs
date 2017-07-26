module Validation

open CommonLibrary
open CommandTypes

let private validateName item =
  if not <| String.isEmpty item.Name then Ok item
  else Error "Band name must not be empty"

let private validateShortDescription item =
  if not <| String.isEmpty(item.ShortDescription) then Ok item
  else Error "Band short description must not be empty"

let validateBand: CreateBandCommand -> Result<CreateBandCommand, string>
  = validateName &&& validateShortDescription
module WebPartCombinators

open Suave
open Suave.Successful

open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open Suave.Operators

let JSON v =
  let jsonSerializerSettings = new JsonSerializerSettings()
  jsonSerializerSettings.ContractResolver <- new CamelCasePropertyNamesContractResolver()

  JsonConvert.SerializeObject(v, jsonSerializerSettings)
  |> OK
  >=> Writers.setMimeType "application/json; charset=utf-8"
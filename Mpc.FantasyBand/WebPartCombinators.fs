module WebPartCombinators

open Suave
open Suave.Successful

open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open Suave.Operators

open RepositoryTypes
open Repository
open CommandTypes

let JSON v: WebPart =
  let jsonSerializerSettings = new JsonSerializerSettings()
  jsonSerializerSettings.ContractResolver <- new CamelCasePropertyNamesContractResolver()

  JsonConvert.SerializeObject(v, jsonSerializerSettings)
  |> OK
  >=> Writers.setMimeType "application/json; charset=utf-8"

let private fromJson<'a> json =
  JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a

let private getResourceFromReq<'a> (req : HttpRequest) =
  let getString rawForm =
    System.Text.Encoding.UTF8.GetString(rawForm)
  req.rawForm |> getString |> fromJson<'a>

let wrapCommand<'input, 'output> (cmd: CommandProcessor<'input, 'output>): WebPart =
  let cmdWithCtx = cmd <| getContext()
  let parse req = req.rawForm |> System.Text.Encoding.UTF8.GetString |> fromJson<'a>
  request (fun req -> parse req |> cmdWithCtx |> JSON)
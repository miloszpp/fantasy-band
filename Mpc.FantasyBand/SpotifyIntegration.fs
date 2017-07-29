﻿module SpotifyIntegration

open FSharp.Data
open Newtonsoft.Json
open Newtonsoft.Json.FSharp
open Newtonsoft.Json.Serialization
open System

type Token = {
  AccessToken: string
  TokenType: string
  ExiresIn: int
}

type Image = {
  Height: int
  Width: int
  Url: string
}

type Artist = {
  Id: string
  Name: string
  Popularity: int
  Images: Image list
  Href: string
}

type SearchArtistsResponseInner = {
  Items: Artist list
}

type SearchArtistsResponse = {
  Artists: SearchArtistsResponseInner
}

let private serializerSettings = lazy (
  let jsonSerializerSettings = new JsonSerializerSettings()
  let jsonContractResolver = new DefaultContractResolver()
  jsonContractResolver.NamingStrategy <- new SnakeCaseNamingStrategy()
  jsonSerializerSettings.ContractResolver <- jsonContractResolver
  //Serialisation.extend jsonSerializerSettings
  jsonSerializerSettings
)

let private fromJson<'t> data = JsonConvert.DeserializeObject<'t>(data, serializerSettings.Value)

let getToken() = 
  let authToken = 
    ConfigSecret.spotifyClientId + ":" + ConfigSecret.spotifyClientSecret
    |> System.Text.Encoding.UTF8.GetBytes
    |> Convert.ToBase64String
  let headers = [
    ("Authorization", "Basic " + authToken)
    ("Content-Type", "application/x-www-form-urlencoded")
  ]
  let query = [("grant_type", "client_credentials")]
  let response = Http.Request("https://accounts.spotify.com/api/token", headers = headers, query = query, httpMethod = "POST")
  if response.StatusCode = 200 then
    match response.Body with
    | Text text -> text |> fromJson<Token> |> Ok
    | _ -> Error "Invalid response from Spotify"
  else
    Error "Could not get authentication token"

let searchArtists searchString token =
  let headers = [
    ("Authorization", "Bearer " + token)
    ("Content-Type", "application/x-www-form-urlencoded")
  ]
  let query: (string * string) list = [("q", searchString); ("type", "artist")]
  let response = Http.Request("https://api.spotify.com/v1/search", headers = headers, query = query, httpMethod = "GET")
  if response.StatusCode = 200 then
    match response.Body with
    | Text text -> text |> fromJson<SearchArtistsResponse> |> Ok
    | _ -> Error "Invalid response from Spotify"
  else
    Error "Could not get authentication token"
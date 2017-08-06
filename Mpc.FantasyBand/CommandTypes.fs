module CommandTypes

open RepositoryTypes
open System

type CommandProcessor<'input, 'output> = DbContext -> 'input -> Result<'output, string>

type CreateBandCommand = {
  Name: string
  SpotifyId: string
  Image: string option
  SpotifyUrl: string
  Genres: string list
}

type CreateQuoteCommand = {
  BandId: Guid
  Popularity: int
  Followers: int
  Date: DateTime
}

type ImportSpotifyBandCommand = {
  SpotifyId: string
}
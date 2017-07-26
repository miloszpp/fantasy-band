module CommandTypes

open RepositoryTypes

type CommandProcessor<'input, 'output> = DbContext -> 'input -> Result<'output, string>

type CreateBandCommand = {
  Name: string;
  ShortDescription: string
}

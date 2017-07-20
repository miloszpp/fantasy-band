// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open Suave
open System.Threading
open System

[<EntryPoint>]
let main argv = 
  let cts = new CancellationTokenSource()
  let conf = { defaultConfig with cancellationToken = cts.Token }

  let listening, server = startWebServerAsync conf Routing.app
    
  Async.Start(server, cts.Token)
  printfn "Make requests now"
  Console.ReadKey true |> ignore
    
  cts.Cancel()

  0 // return an integer exit code

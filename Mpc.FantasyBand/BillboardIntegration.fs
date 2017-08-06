module BillboardIntegration

open FSharp.Data

let private url = "http://www.billboard.com/charts/artist-100"

let getBandNames() =
  let results = HtmlDocument.Load(url)
  results.CssSelect(".chart-row__song") |> Seq.map (fun el -> el.InnerText())

module SuaveAzure 

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful

open Suave.Writers
open Suave.Web
open DAL.Db
open JsonWrapper.NewtonsoftJsonMapper


let setCORSHeaders =
    setHeader "Access-Control-Allow-Origin" "*"     
    >=> setHeader "Access-Control-Allow-Headers" "content-type"     

let setCORS context = 
    context |> (
        setCORSHeaders
        >=> OK "CORS approved" )

let allowCors : WebPart =
    choose [
        OPTIONS >=> setCORS
        //GET >=> setCORS            
    ]

// let JSON v =
//   let jsonSerializerSettings = new JsonSerializerSettings()
//   jsonSerializerSettings.ContractResolver <- new CamelCasePropertyNamesContractResolver()

//   JsonConvert.SerializeObject(v, jsonSerializerSettings)
//   |> OK
//   >=> setMimeType "application/json; charset=utf-8"

[<EntryPoint>]
let main argv =
    let webPart = 
        choose [            
            GET >=> path "/" >=> (OK "Votes")
            GET >=> path "/votes" >=>warbler (fun _ -> ToJson ok (getVotes()))
            POST >=> path "/votes/add" >=> MapJson created addVote
        ]  
    
//    let config =
//        { defaultConfig with
//              bindings = [ HttpBinding.create (uint16 port) ]
//              listenTimeout = TimeSpan.FromMilliseconds 3000. }

    //let localConfig = 
    //    {defaultConfig with 
    //        bindings = 
    //            [ HttpBinding.createSimple HTTP "127.0.0.1" (int port)]}
        
    startWebServer defaultConfig webPart
    0 // return an integer exit code

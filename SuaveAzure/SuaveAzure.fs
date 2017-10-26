module SuaveAzure 

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open Suave.Writers
open Suave.Web
open System

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


[<EntryPoint>]
let main argv = 
    let webPart = 
        choose [            
            GET >=> path "/" >=> OK "Votes"
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

namespace DAL
open System.Collections.Generic

module Db =
    type Vote = {        
        name: string;
        version:int
    }    

    // type VoteInfo = {
    //     vote: Vote
    //     count: int
    // }
    type VotesResult = {        
        // name: string   
        vote: Vote     
        mutable count: int
    }

    let  mutable private votes = Dictionary<string, VotesResult>() 
    votes.Add ("default", {vote = {name="default"; version = 0}; count = 1})
    
    let getVotes ()  =  
        printfn "Votes to get: %A" votes    
        votes
        |> Seq.map( fun (KeyValue(_, voteInfo)) -> 
            voteInfo
        ) 
        |> Seq.sortByDescending (fun ({count = c; vote = {version = ver}}) -> c, ver)
        |> List.ofSeq

    let addVote (vote: Vote) = 
        // let voteResult = {}
        if votes.ContainsKey vote.name then
            votes.[vote.name].count <- votes.[vote.name].count + 1
        else 
            votes.Add (vote.name, {vote = vote; count = 1})    
        printfn "%A" votes    


    // addVote ({name="newVote"})

    // getVotes()
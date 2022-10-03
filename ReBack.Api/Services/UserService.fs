namespace ReBack.Services

open MongoDB.Bson
open MongoDB.Driver
open ReBack.Database
open ReBack.Entities.User

type UserService(db: IMongoDatabase) =
    let userCollection = db.GetCollection<User> USER_COLLECTION_NAME
    
    member _.CreateAsync user =
        task { do! userCollection.InsertOneAsync user } |> Async.AwaitTask
    
    member _.GetAsync () =
        task { return! userCollection.Find(fun _ -> true).ToListAsync() } |> Async.AwaitTask
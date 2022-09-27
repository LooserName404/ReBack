namespace ReBack.Services

open Microsoft.Extensions.Options
open MongoDB.Driver
open ReBack.DatabaseSettings
open ReBack.Entities
open ReBack.Entities.User

type UserService(dbSettings: IOptions<DatabaseSettings>) =
    let mongoClient = MongoClient dbSettings.Value.ConnectionString
    let mongoDatabase = mongoClient.GetDatabase dbSettings.Value.DatabaseName
    let userCollection = mongoDatabase.GetCollection<User> dbSettings.Value.Collections[USER_COLLECTION_NAME]
    
    member _.CreateAsync user =
        task {
            do! userCollection.InsertOneAsync user
        } |> Async.AwaitTask
    
    member _.GetAsync () =
        task {
            return! userCollection.Find(fun _ -> true).ToListAsync()
        } |> Async.AwaitTask
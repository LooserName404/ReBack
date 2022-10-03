module ReBack.Database

open System.Collections.Generic
open MongoDB.Driver
open ReBack.Entities.User

[<Literal>]
let WORK_COLLECTION_NAME = "Works"

[<Literal>]
let USER_COLLECTION_NAME = "Users"

[<Literal>]
let ADMIN_COLLECTION_NAME = "Admins"

[<Literal>]
let PERSON_COLLECTION_NAME = "People"

type DatabaseConfigurationSection = {
    ConnectionString: string
    DatabaseName: string
}

type DatabaseProvider(dbConfig: DatabaseConfigurationSection) =
    let settings = MongoClientSettings.FromUrl(MongoUrl(dbConfig.ConnectionString))
    let mongoClient = MongoClient(settings)
    let database = mongoClient.GetDatabase dbConfig.DatabaseName
    
    member val Database = database

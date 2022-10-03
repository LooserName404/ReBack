module ReBack.Api.Tests.Integration.SetupMongoDb

open EphemeralMongo
open MongoDB.Bson.FSharp
open MongoDB.Driver
open MongoDB.FSharp
open ReBack.Database

let withDatabase test () =
    use runner = MongoRunner.Run()
    Serializers.Register()
    FSharpSerializer.Register()
    let db = MongoClient(runner.ConnectionString).GetDatabase("Tests")
    db.DropCollection USER_COLLECTION_NAME
    db.DropCollection WORK_COLLECTION_NAME
    db.DropCollection ADMIN_COLLECTION_NAME
    db.DropCollection PERSON_COLLECTION_NAME
    db.CreateCollection USER_COLLECTION_NAME
    db.CreateCollection WORK_COLLECTION_NAME
    db.CreateCollection ADMIN_COLLECTION_NAME
    db.CreateCollection PERSON_COLLECTION_NAME
    test db

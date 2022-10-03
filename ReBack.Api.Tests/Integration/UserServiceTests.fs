module ReBack.Api.Tests.Integration.UserServiceTests

open Bogus
open Expecto
open ReBack.Api.Tests.Fakers.UserFakers
open ReBack.Database
open ReBack.Entities.User
open ReBack.Services
open ReBack.Api.Tests.Integration.SetupMongoDb
open MongoDB.Driver

[<Tests>]
let tests =
    testList "User Service tests" [
        yield! testFixture withDatabase [
            "Given a call to CreateAsync, then the database should contain the correct user",
            fun db ->
                let collection = db.GetCollection<User> USER_COLLECTION_NAME
                let sut = UserService(db)
                let expected = generateUser(Faker())
                                        
                sut.CreateAsync expected |> Async.RunSynchronously
                        
                let users = collection.Find(fun _ -> true).ToList()
                        
                Expect.contains users expected "User should have been inserted in the database"
                
            "Given a call to GetAsync, then returns an User list",
            fun db ->
                let collection = db.GetCollection USER_COLLECTION_NAME
                let sut = UserService(db)
                let expected = [for _ in 0..3 do generateUser(Faker())] |> Seq.toList
                    
                collection.InsertMany expected
                                
                let actual = sut.GetAsync () |> Async.RunSynchronously |> Seq.toList
                                
                Expect.sequenceEqual actual expected "Should bring the users from the database"
        ]
        
    ]
open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open MongoDB.Bson.FSharp
open MongoDB.Driver
open MongoDB.FSharp

open ReBack.Database
open ReBack.Controllers
open ReBack.JsonDefaultConverter
open ReBack.Services

let private configureSerializers (builder: WebApplicationBuilder) =
    Serializers.Register()
    FSharpSerializer.Register()
    builder.Services.Configure<JsonOptions>(fun (options: JsonOptions) ->
            JsonDefaultConverter()
            |> options.JsonSerializerOptions.Converters.Add) |> ignore

let private configureMongoDB (builder: WebApplicationBuilder) =
    let configSection key = builder.Configuration.GetSection($"MongoDbConfiguration").GetSection(key).Value
    let dbConfig = {
        ConnectionString = configSection "ConnectionString"
        DatabaseName = configSection "DatabaseName"
    }
    builder.Services.AddSingleton<IMongoDatabase>(fun c -> DatabaseProvider(dbConfig).Database) |> ignore

let private configureServices (builder: WebApplicationBuilder) =
    configureSerializers builder
    configureMongoDB builder

let private addServices (services: IServiceCollection) =
    services.AddSingleton<UserService>() |> ignore

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    configureServices builder
    addServices builder.Services
    let app = builder.Build()

    app.MapGet("/users", Func<_,_>(UserController.getAll >> Async.RunSynchronously)) |> ignore
    app.MapPost("/users", Func<_,_>(UserController.create >> Async.RunSynchronously)) |> ignore

    app.Run()

    0 // Exit code


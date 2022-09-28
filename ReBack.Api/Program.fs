open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open MongoDB.Bson.FSharp
open MongoDB.FSharp

open ReBack.DatabaseSettings
open ReBack.Controllers
open ReBack.JsonDefaultConverter
open ReBack.Services

let private configureServices (builder: WebApplicationBuilder) =
    Serializers.Register()
    FSharpSerializer.Register()
    builder.Services.Configure<JsonOptions>(fun (options: JsonOptions) ->
            JsonDefaultConverter()
            |> options.JsonSerializerOptions.Converters.Add) |> ignore
    builder.Configuration.GetSection "MongoDbConfiguration" |> builder.Services.Configure<DatabaseSettings> |> ignore

let private addServices (services: IServiceCollection) =
    services.AddSingleton<UserService>() |> ignore

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    configureServices builder
    addServices builder.Services
    let app = builder.Build()

    app.MapGet("/users", Func<_,_>(UserController.getAll)) |> ignore
    app.MapPost("/users", Func<_,_>(UserController.create)) |> ignore

    app.Run()

    0 // Exit code


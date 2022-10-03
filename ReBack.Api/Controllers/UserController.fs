[<RequireQualifiedAccess>]
module ReBack.Controllers.UserController

open System
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open MongoDB.Bson
open ReBack.Services
open ReBack.Entities.User
open ReBack.Entities.Work

let create ([<FromServices>]userService: UserService) =
    async {
        let user = {
            Id = ObjectId.GenerateNewId().ToString()
            Name = "Thyago"
            Email = "thyago.vccunha@gmail.com"
            Password = "salve"
            Cover = Choose "salve"
            Description = Some "HI"
            Photo = Some "photo"
            Works = [
                {
                    Work = {
                        Id = ObjectId.GenerateNewId().ToString()
                        Cover = "FOO"
                        Name = "A volta dos Que Não Foram"
                        ReleaseYear = 2000
                        Type = Book
                        Tags = ["Ação"]
                    }
                    Score = 3
                    Status = OnHold (BookProgress 15)
                }
                {
                    Work = {
                        Id = ObjectId.GenerateNewId().ToString()
                        Cover = "FOO2BAR"
                        Name = "A volta dos Que Não volta"
                        ReleaseYear = 2001
                        Type = Movie
                        Tags = ["Fantasia"]
                    }
                    Score = 4
                    Status = Ongoing MovieProgress
                }
            ]
        }
        do! userService.CreateAsync user
        return Results.NoContent()
    }

let getAll ([<FromServices>]userService: UserService) =
    async { return! userService.GetAsync() }
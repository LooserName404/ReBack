[<RequireQualifiedAccess>]
module ReBack.Controllers.UserController

open Microsoft.AspNetCore.Http
open Giraffe
open ReBack.Services
open ReBack.Entities.User

let create : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let userService = ctx.GetService<UserService>()
                let! user = ctx.BindJsonAsync<User>()
                let! inserted = userService.CreateAsync user
                return! Successful.created (json inserted) next ctx
            }        

let getAll : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let userService = ctx.GetService<UserService>()
            let! users = userService.GetAsync()
            return! Successful.ok (json users) next ctx
        }
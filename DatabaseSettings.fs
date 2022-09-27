module ReBack.DatabaseSettings

open System.Collections.Generic
open Microsoft.FSharp.Collections

type DatabaseSettings() = 
    member val ConnectionString: string = "" with get, set
    member val DatabaseName: string = "" with get, set
    member val Collections: IDictionary<string, string> = Dictionary() with get, set


[<Literal>]
let WORK_COLLECTION_NAME = "Work"

[<Literal>]
let USER_COLLECTION_NAME = "User"

[<Literal>]
let ADMIN_COLLECTION_NAME = "Admin"

[<Literal>]
let PERSON_COLLECTION_NAME = "Person"

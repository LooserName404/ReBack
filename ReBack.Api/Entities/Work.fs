module ReBack.Entities.Work

open System.Diagnostics.CodeAnalysis
open MongoDB.Bson.Serialization.Attributes
open ReBack.JsonDefaultConverter

[<CLIMutable>]
[<JsonCvt>]
type Work = {
    WorkInfo: WorkInfo
}

and [<CLIMutable>][<JsonCvt>]WorkInfo = {
    [<BsonId>]
    [<BsonIgnoreIfNull>]
    [<AllowNull>]mutable Id: string
    
    Name: string
    Cover: string
    Type: WorkType
    ReleaseYear: int
    Tags: string list
}
and [<JsonCvt>]WorkType =
    | Book
    | Movie
    | Game
    | Series

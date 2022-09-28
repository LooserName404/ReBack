module ReBack.Entities.User

open System.Diagnostics.CodeAnalysis
open MongoDB.Bson.Serialization.Attributes
open ReBack.Entities.Work
open ReBack.JsonDefaultConverter

[<CLIMutable>]
[<JsonCvt>]
type User = {
    [<BsonId>]
    [<BsonIgnoreIfNull>]
    [<AllowNull>]mutable Id: string
    
    Name: string
    Password: string
    Email: string
    Description: string option
    Photo: string option
    Cover: CoverOption
    Works: WorkUser seq
}

and [<JsonCvt>]CoverOption =
    | Choose of photo: string
    | CycleFavorites
and [<JsonCvt>]WorkUser = {
    Work: WorkInfo
    Score: int
    Status: WorkUserStatus
}
and [<JsonCvt>]WorkUserStatus =
    | Completed
    | Dropped of progress: WorkProgress
    | OnHold of progress: WorkProgress
    | Ongoing of progress: WorkProgress
    | Planning
and [<JsonCvt>]WorkProgress =
    | MovieProgress
    | GameProgress
    | BookProgress of page: int
    | SeriesProgress of episode: int
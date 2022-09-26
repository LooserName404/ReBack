module ReBack.Entities.User

open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes
open ReBack.Entities.Work

type User = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    
    Name: string
    Password: string
    Email: string
    Description: string option
    Photo: string option
    Cover: CoverOption
    Works: WorkUser seq
}
and CoverOption =
    | Choose of photo: string
    | CycleFavorites
and WorkUser = {
    Work: WorkInfo
    Score: int
    Status: WorkUserStatus
}
and WorkUserStatus =
    | Completed
    | Dropped of progress: WorkProgress
    | OnHold of progress: WorkProgress
    | Ongoing of progress: WorkProgress
    | Planning
and WorkProgress =
    | MovieProgress
    | GameProgress
    | BookProgress of page: int
    | SeriesProgress of episode: int
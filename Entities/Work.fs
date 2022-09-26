module ReBack.Entities.Work

open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

type Work = {
    WorkInfo: WorkInfo
}
and WorkInfo = {
    [<BsonId>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    Id: string
    
    Name: string
    Cover: string
    Type: WorkType
    ReleaseYear: int
    Tags: string seq
}
and WorkType =
    | Book
    | Movie
    | Game
    | Series

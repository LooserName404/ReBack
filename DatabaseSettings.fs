module ReBack.DatabaseSettings

type DatabaseSettings = {
    ConnectionString: string
    DatabaseName: string
    Collections: Map<string, string>
}

[<Literal>]
let WORK_COLLECTION_NAME = "Work"

[<Literal>]
let USER_COLLECTION_NAME = "User"

[<Literal>]
let ADMIN_COLLECTION_NAME = "Admin"

[<Literal>]
let PERSON_COLLECTION_NAME = "Person"

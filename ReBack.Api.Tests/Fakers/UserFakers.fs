module ReBack.Api.Tests.Fakers.UserFakers

open Bogus
open ReBack.Entities.User
open ReBack.Entities.Work

let generateWorkInfo (faker: Faker): WorkInfo = {
    Id = faker.Random.Uuid().ToString()
    Name = faker.Lorem.Word()
    Cover = faker.Internet.Url()
    Type = faker.PickRandom [Book ; Movie; Game; Series]
    ReleaseYear = faker.Date.Past().Year
    Tags = [ for _ in 0..(faker.Random.Int(0, 10)) do faker.Lorem.Word() ]
}

let generateWorkProgress(faker: Faker): WorkProgress =
    faker.PickRandom [
        MovieProgress
        GameProgress
        BookProgress (faker.Random.Int(0, 800))
        SeriesProgress (faker.Random.Int(0, 50))
    ]

let generateWorkUser(faker: Faker): WorkUser = {
    Work = generateWorkInfo(faker)
    Score = faker.Random.Int(1, 5)
    Status = faker.PickRandom [
        Completed
        Planning
        Dropped (generateWorkProgress(faker))
        OnHold (generateWorkProgress(faker))
        Ongoing (generateWorkProgress(faker))
    ]
}

let generateUser(faker: Faker): User = {
    Id = faker.Random.Uuid().ToString()
    Name = faker.Person.FullName
    Password = faker.Internet.Password()
    Email = faker.Internet.Email()
    Description = faker.PickRandom [ Some (faker.Lorem.Paragraph()); None ]
    Photo = faker.PickRandom [ Some (faker.Random.Uuid().ToString()); None ]
    Cover = faker.PickRandom [ CycleFavorites; Choose (faker.Lorem.Sentence()) ]
    Works = [ for _ = 0 to faker.Random.Int(0, 10) do generateWorkUser(faker) ]
}

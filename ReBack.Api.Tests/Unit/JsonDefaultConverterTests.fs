module ReBack.Api.Tests.Unit.JsonDefaultConverterTests

open System.Text.Json
open Expecto
open ReBack.JsonDefaultConverter

type private UnionSample = A of int | B

let private createJsonOptions () =
    let options = JsonSerializerOptions()
    JsonDefaultConverter() |> options.Converters.Add
    options

[<Tests>]
let tests =
    testList "Default JSON Converter tests" [
        testCase "Should have 'value' as union field name" <| fun _ ->
            let sut = createJsonOptions()
            let fake = {| Union = A 1 |}
            let expected = @"""value"":1"
            
            let actual = JsonSerializer.Serialize(fake, sut)
            Expect.stringContains
                actual
                expected
                "JSON must serialize union field name as 'value'"
            
        testCase "Should have 'type' as union tag name" <| fun _ ->
            let sut = createJsonOptions()
            let fake = {| Union = A 1 |}
            let expected = @"""type"":""A"""
            
            let actual = JsonSerializer.Serialize(fake, sut)
            
            Expect.stringContains
                actual
                expected
                "JSON must serialize union tag name as 'type'"
    ]
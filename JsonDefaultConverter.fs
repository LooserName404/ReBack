module ReBack.JsonDefaultConverter

open System.Text.Json.Serialization

type JsonCvtAttribute() =
    inherit JsonFSharpConverterAttribute(
        unionEncoding = (JsonUnionEncoding.AdjacentTag ||| JsonUnionEncoding.UnwrapSingleFieldCases),
        unionFieldsName = "value",
        unionTagName = "type"
    )

type JsonDefaultConverter() =
    inherit JsonFSharpConverter(
         unionEncoding = (JsonUnionEncoding.AdjacentTag ||| JsonUnionEncoding.UnwrapSingleFieldCases),
         unionFieldsName = "value",
         unionTagName = "type"
    )
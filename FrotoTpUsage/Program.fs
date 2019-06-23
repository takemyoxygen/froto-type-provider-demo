// Learn more about F# at http://fsharp.org

open Froto.TypeProvider
open Froto.Serialization

type Books = ProtocolBuffersTypeProvider<"books.proto">


[<EntryPoint>]
let main argv =
    let b = new Books.BookAuthor()
    b.Name <- Some "foo"

    let bb = new Books.BookDetails()
    bb.Author <- Some b

    printfn "Total size - %i" bb.SerializedLength

    let zcb = new ZeroCopyBuffer(100)

    bb.Serialize(zcb);
    let buffer' = new ZeroCopyBuffer(zcb.AsArraySegment)

    let bb' = Books.BookDetails.Deserialize buffer'

    printf "Author name - %A" (bb'.Author |> Option.map(fun a -> a.Name))

    printfn "Hello World from F#!"
    0 // return an integer exit code

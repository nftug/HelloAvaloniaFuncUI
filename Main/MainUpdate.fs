namespace HelloAvaloniaFuncUI.Main

open Elmish
open System.Threading.Tasks

module MainUpdate =
    let private fetchDelayedCount (model, delay, ret) =
        task {
            match model.IsSetting with
            | true -> return failwith "Already resetting"
            | false ->
                do! Task.Delay(int delay)
                return int ret
        }

    let private sendDelayedResponse model delay number =
        { model with IsSetting = true },
        Cmd.OfTask.either fetchDelayedCount (model, delay, number) Completed Error

    let update msg model =
        match msg with
        | Increment -> sendDelayedResponse model 100 (model.Count + 1)
        | Decrement -> sendDelayedResponse model 100 (model.Count - 1)
        | Reset -> sendDelayedResponse model 500 0
        | Completed count ->
            printfn $"Completed called with {count}"
            { Count = count; IsSetting = false }, Cmd.none
        | Error ex ->
            printfn $"Error: {ex.Message}"
            { model with IsSetting = false }, Cmd.none

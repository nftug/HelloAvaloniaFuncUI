namespace HelloAvaloniaFuncUI.Main.Clock

open System
open Elmish
open R3

type Model = { Time: DateTime }

type Msg = SetTime of DateTime

module ClockModel =
    let private interval =
        Observable.Interval(TimeSpan.FromSeconds 1.0)
        |> fun src -> ObservableExtensions.Select(src, (fun _ -> DateTime.Now))
        |> ObservableExtensions.Publish
        |> ObservableExtensions.RefCount

    let init () : Model * Cmd<Msg> =
        { Time = DateTime.Now },
        Cmd.ofEffect (fun dispatch ->
            ObservableSubscribeExtensions.Subscribe(interval, (fun t -> dispatch (SetTime t)))
            |> ignore)

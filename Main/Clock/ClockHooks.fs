namespace HelloAvaloniaFuncUI.Main.Clock

open System
open Avalonia.FuncUI
open R3

type ClockModel = { Now: DateTime }

type ClockHooks = { Now: IReadable<DateTime> }

[<AutoOpen>]
module ClockHooks =
    // Model logic
    let private interval =
        Observable.Interval(TimeSpan.FromSeconds 1.0)
        |> fun src -> ObservableExtensions.Select(src, (fun _ -> DateTime.Now))
        |> ObservableExtensions.Publish
        |> ObservableExtensions.RefCount

    let useClockHooks (ctx: IComponentContext) : ClockHooks =
        let model =
            ctx.useState { ClockModel.Now = DateTime.Now }

        let now = model.Map _.Now
        ctx.useEffect (
            fun () -> ObservableSubscribeExtensions.Subscribe(interval, (fun t -> model.Set { Now = t }))
            , [ EffectTrigger.AfterInit ]
        )

        { Now = now }

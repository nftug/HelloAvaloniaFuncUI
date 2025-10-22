namespace HelloAvaloniaFuncUI.Main.Clock

open System
open Avalonia.FuncUI
open R3

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
        let now = ctx.useState DateTime.Now

        ctx.useEffect (
            fun () -> ObservableSubscribeExtensions.Subscribe(interval, (fun t -> now.Set t))
            , [ EffectTrigger.AfterInit ]
        )

        { Now = now }

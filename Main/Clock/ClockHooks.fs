namespace HelloAvaloniaFuncUI.Main.Clock

open System
open Avalonia.FuncUI
open R3

module ClockHooks =
    // Model logic
    let private interval =
        Observable.Interval(TimeSpan.FromSeconds 1.0)
        |> fun src -> ObservableExtensions.Select(src, (fun _ -> DateTime.Now))
        |> ObservableExtensions.Publish
        |> ObservableExtensions.RefCount

    type ClockHooks = { now: IReadable<DateTime> }

    let useClockHooks (ctx: IComponentContext) : ClockHooks =
        let now = ctx.useState DateTime.Now

        ctx.useEffect (
            fun () -> ObservableSubscribeExtensions.Subscribe(interval, (fun t -> now.Set t))
            , [ EffectTrigger.AfterInit ]
        )

        { now = ctx.usePassedRead now }

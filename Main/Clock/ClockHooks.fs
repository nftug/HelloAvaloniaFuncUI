namespace HelloAvaloniaFuncUI.Main.Clock

open System
open Avalonia.FuncUI
open R3

type ClockHooks = { Now: IReadable<DateTime> }

[<AutoOpen>]
module ClockHooks =
    // Model logic
    let private interval =
        Observable
            .Interval(TimeSpan.FromSeconds 1.0)
            .Select(fun _ -> DateTime.Now)
            .Publish()
            .RefCount()

    let useClockHooks (ctx: IComponentContext) : ClockHooks =
        let now = ctx.useState DateTime.Now

        ctx.useEffect ((fun () -> interval.Subscribe now.Set), [ EffectTrigger.AfterInit ])

        { Now = now }

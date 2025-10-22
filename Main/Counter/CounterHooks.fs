namespace HelloAvaloniaFuncUI.Main.Counter

open System
open System.Threading.Tasks
open Avalonia.FuncUI

type CounterHooks =
    { Count: IReadable<int>
      IsSetting: IReadable<bool>
      SetCountWithDelay: TimeSpan -> int -> unit }

[<AutoOpen>]
module CounterHooks =
    // Model logic
    let private fetchDelayedCount (delay: TimeSpan) (newCount: int) : Task<int> =
        task {
            do! Task.Delay delay
            return newCount
        }

    let useCounterHooks (ctx: IComponentContext) : CounterHooks =
        let count = ctx.useState 0
        let isSetting = ctx.useState false

        let setCountWithDelay delay newCount =
            task {
                isSetting.Set true

                let! result = fetchDelayedCount delay newCount

                count.Set result
                isSetting.Set false
            }
            |> ignore

        { Count = count
          IsSetting = isSetting
          SetCountWithDelay = setCountWithDelay }

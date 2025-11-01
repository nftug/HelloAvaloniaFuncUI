namespace HelloAvaloniaFuncUI.Main.Counter

open System
open System.Threading.Tasks
open Avalonia.FuncUI

type CounterHooks =
    { Count: IReadable<int>
      IsSetting: IReadable<bool>
      SetCountWithDelay: (TimeSpan * int) -> unit }

[<AutoOpen>]
module CounterHooks =
    // Model logic
    let private fetchDelayedCount (delay: TimeSpan, newCount: int) : Task<int> =
        task {
            do! Task.Delay delay
            return newCount
        }

    let useCounterHooks (ctx: IComponentContext) : CounterHooks =
        let count = ctx.useState 0

        let mutation = ctx.useMutation fetchDelayedCount

        let setCountWithDelay (args: TimeSpan * int) =
            task {
                let! newCount = mutation.MutateTask args
                count.Set newCount
            }

        { Count = count
          IsSetting = mutation.IsPending
          SetCountWithDelay = setCountWithDelay >> ignore }

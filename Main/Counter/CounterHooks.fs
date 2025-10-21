namespace HelloAvaloniaFuncUI.Main.Counter

open System
open System.Threading.Tasks
open Avalonia.FuncUI

type CounterHooks =
    { count: IReadable<int>
      isSettingWithDelay: IReadable<bool>
      increment: unit -> unit
      decrement: unit -> unit
      reset: unit -> unit
      canIncrement: IReadable<bool>
      canDecrement: IReadable<bool>
      canReset: IReadable<bool> }

module CounterHooks =
    // Model logic
    let private fetchDelayedCount (delay: TimeSpan) (newCount: int) : Task<int> =
        task {
            do! Task.Delay delay
            return newCount
        }

    let useCounterHooks (ctx: IComponentContext) : CounterHooks =
        let count = ctx.useState 0
        let isSettingWithDelay = ctx.useState false

        let startSettingWithDelay delay newCount =
            task {
                isSettingWithDelay.Set true
                let! result = fetchDelayedCount delay newCount
                count.Set result
                isSettingWithDelay.Set false
            }

        let canIncrement = isSettingWithDelay.Map(fun isSetting -> not isSetting)

        let canDecrement =
            isSettingWithDelay.Map(fun isSetting -> count.Current > 0 && not isSetting)

        let canReset =
            isSettingWithDelay.Map(fun isSetting -> count.Current <> 0 && not isSetting)

        let increment () =
            if canIncrement.Current then
                startSettingWithDelay (TimeSpan.FromMilliseconds 100.0) (count.Current + 1)
                |> ignore

        let decrement () =
            if canDecrement.Current then
                startSettingWithDelay (TimeSpan.FromMilliseconds 100.0) (count.Current - 1)
                |> ignore

        let reset () =
            if canReset.Current then
                startSettingWithDelay (TimeSpan.FromMilliseconds 500.0) 0 |> ignore

        { count = count
          isSettingWithDelay = isSettingWithDelay
          increment = increment
          decrement = decrement
          reset = reset
          canIncrement = canIncrement
          canDecrement = canDecrement
          canReset = canReset }

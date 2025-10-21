namespace HelloAvaloniaFuncUI.Main

open System
open System.Threading.Tasks
open Avalonia.FuncUI

module MainHooks =
    // Model logic
    let private fetchDelayedCount (delay: TimeSpan) (newCount: int) : Task<int> =
        task {
            do! Task.Delay delay
            return newCount
        }

    type MainHooks =
        { count: IReadable<int>
          isSettingWithDelay: IReadable<bool>
          increment: unit -> unit
          decrement: unit -> unit
          reset: unit -> unit
          canIncrement: bool
          canDecrement: bool
          canReset: bool }

    let useMainHooks (ctx: IComponentContext) : MainHooks =
        let count = ctx.useState 0
        let isSettingWithDelay = ctx.useState false

        let startSettingWithDelay delay newCount =
            task {
                isSettingWithDelay.Set true
                let! result = fetchDelayedCount delay newCount
                count.Set result
                isSettingWithDelay.Set false
            }

        let increment () =
            startSettingWithDelay (TimeSpan.FromMilliseconds 100.0) (count.Current + 1)
            |> ignore

        let decrement () =
            startSettingWithDelay (TimeSpan.FromMilliseconds 100.0) (count.Current - 1)
            |> ignore

        let reset () =
            startSettingWithDelay (TimeSpan.FromMilliseconds 500.0) 0 |> ignore

        let canIncrement = not isSettingWithDelay.Current
        let canDecrement = not isSettingWithDelay.Current && count.Current > 0
        let canReset = not isSettingWithDelay.Current && count.Current <> 0

        { count = ctx.usePassedRead count
          isSettingWithDelay = ctx.usePassedRead isSettingWithDelay
          increment = increment
          decrement = decrement
          reset = reset
          canIncrement = canIncrement
          canDecrement = canDecrement
          canReset = canReset }

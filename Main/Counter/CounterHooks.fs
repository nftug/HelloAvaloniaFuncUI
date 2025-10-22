namespace HelloAvaloniaFuncUI.Main.Counter

open System
open System.Threading.Tasks
open Avalonia.FuncUI
open HelloAvaloniaFuncUI.Common.Utils

type CounterModel =
    { Count: int; IsSettingWithDelay: bool }

type CounterHooks =
    { Count: IWritable<int>
      IsSettingWithDelay: IReadable<bool>
      CanIncrement: IReadable<bool>
      CanDecrement: IReadable<bool>
      CanReset: IReadable<bool>
      Increment: unit -> unit
      Decrement: unit -> unit
      Reset: unit -> unit }

module CounterHooks =
    // Model logic
    let private fetchDelayedCount (delay: TimeSpan) (newCount: int) : Task<int> =
        task {
            do! Task.Delay delay
            return newCount
        }

    let useCounterHooks (ctx: IComponentContext) : CounterHooks =
        let model =
            ctx.useState
                { Count = 0
                  IsSettingWithDelay = false }

        let count =
            ctx.useBinding (model, (fun m -> m.Count), (fun c -> { model.Current with Count = c }))

        let isSettingWithDelay = model.Map(fun m -> m.IsSettingWithDelay)
        let canIncrement = model.Map(fun m -> not m.IsSettingWithDelay)
        let canDecrement = model.Map(fun m -> not m.IsSettingWithDelay && m.Count > 0)
        let canReset = model.Map(fun m -> not m.IsSettingWithDelay && m.Count <> 0)

        let startSettingWithDelay delay newCount =
            task {
                model.Set
                    { model.Current with
                        IsSettingWithDelay = true }

                let! result = fetchDelayedCount delay newCount

                model.Set
                    { Count = result
                      IsSettingWithDelay = false }
            }

        let increment () =
            if canIncrement.Current then
                startSettingWithDelay (TimeSpan.FromMilliseconds 100.0) (model.Current.Count + 1)
                |> ignore

        let decrement () =
            if canDecrement.Current then
                startSettingWithDelay (TimeSpan.FromMilliseconds 100.0) (model.Current.Count - 1)
                |> ignore

        let reset () =
            if canReset.Current then
                startSettingWithDelay (TimeSpan.FromMilliseconds 500.0) 0 |> ignore

        { Count = count
          IsSettingWithDelay = isSettingWithDelay
          Increment = increment
          Decrement = decrement
          Reset = reset
          CanIncrement = canIncrement
          CanDecrement = canDecrement
          CanReset = canReset }

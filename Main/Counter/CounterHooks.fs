namespace HelloAvaloniaFuncUI.Main.Counter

open System
open System.Threading.Tasks
open Avalonia.FuncUI
open HelloAvaloniaFuncUI.Common.Utils

type CounterModel =
    { CountResult: int;
      CountInput: int;
      IsSetting: bool }

type CounterHooks =
    { Model: IReadable<CounterModel>
      CountResult: IReadable<int>
      CountInput: IWritable<int>
      IsSetting: IReadable<bool>
      CanIncrement: IReadable<bool>
      CanDecrement: IReadable<bool>
      CanReset: IReadable<bool>
      CanSetInput: IReadable<bool>
      Increment: unit -> unit
      Decrement: unit -> unit
      Reset: unit -> unit
      SetFromInput: unit -> unit }

[<AutoOpen>]
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
                { CountResult = 0
                  CountInput = 0
                  IsSetting = false }

        let countResult = model.Map _.CountResult
        let isSetting = model.Map _.IsSetting

        let countInput =
            ctx.useBinding (model, (fun m -> m.CountInput), (fun c -> { model.Current with CountInput = c }))

        let canIncrement = model.Map(fun m -> not m.IsSetting)
        let canDecrement = model.Map(fun m -> not m.IsSetting && m.CountResult > 0)
        let canReset = model.Map(fun m -> not m.IsSetting && m.CountResult <> 0)
        let canSetInput = model.Map(fun m -> not m.IsSetting && m.CountInput <> m.CountResult)

        let setCountWithDelay delay newCount =
            task {
                model.Set
                    { model.Current with IsSetting = true }

                let! result = fetchDelayedCount delay newCount

                model.Set
                    { model.Current with
                        CountResult = result
                        CountInput = result
                        IsSetting = false }
            } |> ignore

        let increment () =
            if canIncrement.Current then
                setCountWithDelay (TimeSpan.FromMilliseconds 100.0) (countResult.Current + 1)

        let decrement () =
            if canDecrement.Current then
                setCountWithDelay (TimeSpan.FromMilliseconds 100.0) (countResult.Current - 1)

        let reset () =
            if canReset.Current then
                setCountWithDelay (TimeSpan.FromMilliseconds 500.0) 0

        let setFromInput () =
            if canSetInput.Current then
                setCountWithDelay (TimeSpan.FromMilliseconds 300.0) countInput.Current

        { Model = model
          CountResult = countResult
          CountInput = countInput
          IsSetting = isSetting
          Increment = increment
          Decrement = decrement
          Reset = reset
          CanIncrement = canIncrement
          CanDecrement = canDecrement
          CanReset = canReset
          CanSetInput = canSetInput
          SetFromInput = setFromInput }

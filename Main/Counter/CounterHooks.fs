namespace HelloAvaloniaFuncUI.Main.Counter

open System
open System.Threading.Tasks
open Avalonia.FuncUI

type CounterModel = { CountResult: int; IsSetting: bool }

type CounterHooks =
    { Model: IReadable<CounterModel>
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
        let model = ctx.useState { CountResult = 0; IsSetting = false }

        let setCountWithDelay delay newCount =
            task {
                model.Set { model.Current with IsSetting = true }

                let! result = fetchDelayedCount delay newCount

                model.Set
                    { model.Current with
                        CountResult = result
                        IsSetting = false }
            }
            |> ignore

        { Model = model
          SetCountWithDelay = setCountWithDelay }

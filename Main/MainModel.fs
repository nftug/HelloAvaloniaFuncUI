namespace HelloAvaloniaFuncUI.Main

open Elmish
open HelloAvaloniaFuncUI.Common
open System.Threading.Tasks
open System

type Model =
    { Count: int
      SetWithDelayState: AsyncState<int>
      Clock: Clock.Model }

type SetArgs = { Delay: TimeSpan; NewCount: int }

type Msg =
    | ClockMsg of Clock.Msg
    | Increment
    | Decrement
    | Reset
    | SetWithDelay of AsyncEvent<SetArgs, int>

module MainModel =
    // Initialization
    let init () : Model * Cmd<Msg> =
        let clockModel, clockCmd = Clock.ClockModel.init ()

        { Count = 0
          SetWithDelayState = Idle
          Clock = clockModel },
        Cmd.map ClockMsg clockCmd

    // Model logic
    let fetchDelayedCount (args: SetArgs) : Task<int> =
        task {
            do! Task.Delay args.Delay
            return args.NewCount
        }

    // Helpers for creating messages
    let makeIncrementTrigger (model: Model) : Msg =
        Trigger
            { Delay = TimeSpan.FromMilliseconds 100
              NewCount = model.Count + 1 }
        |> SetWithDelay

    let makeDecrementTrigger (model: Model) : Msg =
        Trigger
            { Delay = TimeSpan.FromMilliseconds 100
              NewCount = model.Count - 1 }
        |> SetWithDelay

    let makeResetTrigger () : Msg =
        Trigger
            { Delay = TimeSpan.FromMilliseconds 500
              NewCount = 0 }
        |> SetWithDelay

    // Helpers for states
    let canIncrement (model: Model) : bool = model.SetWithDelayState <> InProgress

    let canDecrement (model: Model) : bool =
        model.SetWithDelayState <> InProgress && model.Count > 0

    let canReset (model: Model) : bool =
        model.SetWithDelayState <> InProgress && model.Count <> 0

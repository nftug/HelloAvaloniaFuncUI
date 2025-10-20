namespace HelloAvaloniaFuncUI.Main

open Elmish

type Model =
    { Count: int
      IsSetting: bool
      Clock: Clock.Model }

type Msg =
    | ClockMsg of Clock.Msg
    | Increment
    | Decrement
    | Reset
    | Completed of int
    | Error of exn

module MainModel =
    let init () =
        let clockModel, clockCmd = Clock.ClockModel.init ()

        { Count = 0
          IsSetting = false
          Clock = clockModel },
        Cmd.map ClockMsg clockCmd

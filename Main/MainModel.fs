namespace HelloAvaloniaFuncUI.Main

open Elmish

type Model = { Count: int; IsSetting: bool }

type Msg =
    | Increment
    | Decrement
    | Reset
    | Completed of int
    | Error of exn

module MainModel =

    let init () =
        { Count = 0; IsSetting = false }, Cmd.none

namespace HelloAvaloniaFuncUI.Main.Clock

open Elmish

module ClockUpdate =
    let update msg model =
        match msg with
        | SetTime time -> { model with Time = time }, Cmd.none

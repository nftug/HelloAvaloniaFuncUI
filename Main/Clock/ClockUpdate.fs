namespace HelloAvaloniaFuncUI.Main.Clock

open Elmish

module ClockUpdate =
    let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
        match msg with
        | SetTime time -> { model with Time = time }, Cmd.none

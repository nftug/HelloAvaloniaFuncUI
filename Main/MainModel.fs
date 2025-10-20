namespace HelloAvaloniaFuncUI.Main

type Model = { Count: int }

type Msg =
    | Increment
    | Decrement

module MainModel =
    let init () = { Count = 0 }

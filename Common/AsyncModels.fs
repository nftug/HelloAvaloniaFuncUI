namespace HelloAvaloniaFuncUI.Common

type AsyncState<'a> =
    | Idle
    | InProgress
    | Success of 'a
    | Failure of exn

type AsyncEvent<'input, 'output> =
    | Trigger of 'input
    | Completed of 'output
    | Failed of exn

module AsyncState =
    let evolve (event: AsyncEvent<'input, 'output>) (state: AsyncState<'output>) =
        match event, state with
        | Trigger _, _ -> InProgress
        | Completed result, InProgress -> Success result
        | Failed ex, InProgress -> Failure ex
        | _, _ -> state

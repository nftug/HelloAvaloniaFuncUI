namespace HelloAvaloniaFuncUI.Main

open Elmish
open HelloAvaloniaFuncUI.Common

module MainUpdate =
    let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
        match msg with
        | ClockMsg clockMsg ->
            let clockModel, clockCmd = Clock.ClockUpdate.update clockMsg model.Clock
            { model with Clock = clockModel }, Cmd.map ClockMsg clockCmd

        | Increment -> model, Cmd.ofMsg (MainModel.makeIncrementTrigger model)

        | Decrement -> model, Cmd.ofMsg (MainModel.makeDecrementTrigger model)

        | Reset -> model, Cmd.ofMsg (MainModel.makeResetTrigger ())

        | SetWithDelay event ->
            let newModel =
                { model with
                    SetWithDelayState = AsyncState.evolve event model.SetWithDelayState }

            match event with
            | Trigger args ->
                newModel,
                Cmd.OfTask.perform MainModel.fetchDelayedCount args (Completed >> SetWithDelay)
            | Completed newCount -> { newModel with Count = newCount }, Cmd.none
            | Failed _ -> newModel, Cmd.none

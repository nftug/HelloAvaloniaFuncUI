namespace HelloAvaloniaFuncUI.Main.Counter

open System
open Avalonia
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

type private MergedModel =
    { Model: CounterModel; InputValue: int }

module CounterInputView =
    let private mergeModel m prev =
        match prev with
        | Some p when m.IsSetting -> { Model = m; InputValue = p.InputValue }
        | _ ->
            { Model = m
              InputValue = m.CountResult }

    let inputDelay = TimeSpan.FromMilliseconds 300.0

    let create (hooks: CounterHooks) =
        Component.create (
            "CounterInputView",
            fun ctx ->
                let model = ctx.useMerged (hooks.Model, mergeModel)

                let inputValue =
                    ctx.useBinding (
                        model,
                        (fun m -> m.InputValue),
                        fun v prev -> { prev with InputValue = v }
                    )

                let canSetInput =
                    model.Map(fun c -> c.Model.CountResult <> c.InputValue && not c.Model.IsSetting)
                    |> ctx.usePassedRead

                let isSetting = model.Map(fun c -> c.Model.IsSetting) |> ctx.usePassedRead

                let setCountFromInput () =
                    if canSetInput.Current then
                        hooks.SetCountWithDelay inputDelay inputValue.Current

                Grid.create
                    [ Grid.columnDefinitions "1*, Auto"
                      Grid.children
                          [ NumericUpDown.create
                                [ NumericUpDown.minimum 0
                                  NumericUpDown.maximum Int32.MaxValue
                                  NumericUpDown.value inputValue.Current
                                  NumericUpDown.formatString "0"
                                  NumericUpDown.onValueChanged (fun value ->
                                      if value.HasValue then
                                          inputValue.Set(int value.Value))
                                  NumericUpDown.isEnabled (not isSetting.Current)
                                  NumericUpDown.verticalContentAlignment VerticalAlignment.Center
                                  NumericUpDown.margin (Thickness(5.0, 0.0)) ]
                            Button.create
                                [ Button.content "Set Count"
                                  Button.onClick (fun _ -> setCountFromInput ())
                                  Button.isEnabled canSetInput.Current
                                  Button.margin (Thickness(5.0, 0.0))
                                  Grid.column 1 ] ] ]
        )

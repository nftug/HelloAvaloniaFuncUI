namespace HelloAvaloniaFuncUI.Main.Counter

open System
open Avalonia
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module CounterInputView =
    let inputDelay = TimeSpan.FromMilliseconds 300.0

    let create (hooks: CounterHooks) =
        Component.create (
            "CounterInputView",
            fun ctx ->
                let count = hooks.Count |> ctx.usePassedRead
                let isSetting = hooks.IsSetting |> ctx.usePassedRead
                let inputValue = ctx.useState count.Current

                ctx.useEffect (
                    fun () -> inputValue.Set count.Current
                    , [ EffectTrigger.AfterChange count ]
                )

                let canSetInput =
                    ctx.useDerived3 (
                        (isSetting, count, inputValue),
                        fun (s, c, i) -> not s && i <> c
                    )

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

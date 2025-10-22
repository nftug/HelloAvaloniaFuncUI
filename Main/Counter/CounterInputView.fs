namespace HelloAvaloniaFuncUI.Main.Counter

open System
open Avalonia
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module CounterInputView =
    let create (hooks: CounterHooks) =
        Component.create (
            "CounterInputView",
            fun ctx ->
                let inputValue = ctx.usePassed hooks.CountInput
                let canSetInput = ctx.usePassedRead hooks.CanSetInput
                let isSetting = ctx.usePassedRead hooks.IsSetting

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
                                  Button.onClick (fun _ -> hooks.SetFromInput())
                                  Button.isEnabled canSetInput.Current
                                  Button.margin (Thickness(5.0, 0.0))
                                  Grid.column 1 ] ] ]
        )

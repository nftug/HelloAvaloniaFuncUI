namespace HelloAvaloniaFuncUI.Main.Counter

open Avalonia
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module CounterView =
    let create () =
        Component.create (
            "CounterView",
            fun ctx ->
                let hooks = useCounterHooks ctx
                let model = ctx.usePassedRead hooks.Model
                let countResult = model.Map(fun m -> m.CountResult) |> ctx.usePassedRead

                StackPanel.create
                    [ StackPanel.margin 20.0
                      StackPanel.spacing 10.0
                      StackPanel.horizontalAlignment HorizontalAlignment.Center
                      StackPanel.verticalAlignment VerticalAlignment.Center
                      StackPanel.children
                          [ TextBlock.create
                                [ TextBlock.text $"Count: {countResult.Current}"
                                  TextBlock.fontSize 24.0
                                  TextBlock.margin (Thickness(0.0, 0.0, 0.0, 20.0))
                                  TextBlock.horizontalAlignment HorizontalAlignment.Center ]
                            CounterInputView.create hooks
                            CounterActionButtonView.create hooks ] ]
        )

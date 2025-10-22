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
                let countResult = ctx.usePassedRead hooks.CountResult

                StackPanel.create
                    [ StackPanel.margin 10.0
                      StackPanel.horizontalAlignment HorizontalAlignment.Stretch
                      StackPanel.verticalAlignment VerticalAlignment.Stretch
                      StackPanel.children
                          [ TextBlock.create
                                [ TextBlock.text $"Count: {countResult.Current}"
                                  TextBlock.fontSize 24.0
                                  TextBlock.margin (Thickness(0.0, 0.0, 0.0, 20.0))
                                  TextBlock.horizontalAlignment HorizontalAlignment.Center ]
                            CounterActionButtonView.create hooks ]
                      StackPanel.horizontalAlignment HorizontalAlignment.Center
                      StackPanel.verticalAlignment VerticalAlignment.Center ]
        )

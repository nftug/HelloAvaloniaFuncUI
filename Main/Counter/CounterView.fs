namespace HelloAvaloniaFuncUI.Main.Counter

open Avalonia
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module CounterView =
    let view () =
        Component.create (
            "CounterView",
            fun ctx ->
                let hooks = CounterHooks.useCounterHooks ctx

                StackPanel.create
                    [ StackPanel.margin 10.0
                      StackPanel.horizontalAlignment HorizontalAlignment.Stretch
                      StackPanel.verticalAlignment VerticalAlignment.Stretch
                      StackPanel.children
                          [ TextBlock.create
                                [ TextBlock.text $"Count: {hooks.Count.Current}"
                                  TextBlock.fontSize 24.0
                                  TextBlock.margin (Thickness(0.0, 0.0, 0.0, 20.0))
                                  TextBlock.horizontalAlignment HorizontalAlignment.Center ]
                            CounterActionButtonView.view hooks ]
                      StackPanel.horizontalAlignment HorizontalAlignment.Center
                      StackPanel.verticalAlignment VerticalAlignment.Center ]
        )

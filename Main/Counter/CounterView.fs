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
                let count = ctx.usePassedRead hooks.Count
                let isSetting = ctx.usePassedRead hooks.IsSetting
                let setCountWithDelay = hooks.SetCountWithDelay

                StackPanel.create
                    [ StackPanel.margin 20.0
                      StackPanel.spacing 10.0
                      StackPanel.horizontalAlignment HorizontalAlignment.Center
                      StackPanel.verticalAlignment VerticalAlignment.Center
                      StackPanel.children
                          [ TextBlock.create
                                [ TextBlock.text $"Count: {count.Current}"
                                  TextBlock.fontSize 24.0
                                  TextBlock.margin (Thickness(0.0, 0.0, 0.0, 20.0))
                                  TextBlock.horizontalAlignment HorizontalAlignment.Center ]
                            CounterInputView.create (count, isSetting, setCountWithDelay)
                            CounterActionButtonView.create (count, isSetting, setCountWithDelay) ] ]
        )

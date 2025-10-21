namespace HelloAvaloniaFuncUI.Main.Counter

open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module CounterActionButtonView =
    let view (hooks: CounterHooks) =
        Component.create (
            "CounterActionButtonView",
            fun ctx ->
                let canIncrement = ctx.usePassedRead hooks.canIncrement
                let canDecrement = ctx.usePassedRead hooks.canDecrement
                let canReset = ctx.usePassedRead hooks.canReset

                StackPanel.create
                    [ StackPanel.spacing 10.0
                      StackPanel.horizontalAlignment HorizontalAlignment.Center
                      StackPanel.verticalAlignment VerticalAlignment.Center
                      StackPanel.children
                          [ StackPanel.create
                                [ StackPanel.spacing 10.0
                                  StackPanel.orientation Orientation.Horizontal
                                  StackPanel.children
                                      [ Button.create
                                            [ Button.content "Increment"
                                              Button.onClick (fun _ -> hooks.increment ())
                                              Button.isEnabled canIncrement.Current ]
                                        Button.create
                                            [ Button.content "Decrement"
                                              Button.onClick (fun _ -> hooks.decrement ())
                                              Button.isEnabled canDecrement.Current ] ] ]
                            Button.create
                                [ Button.content "Reset"
                                  Button.onClick (fun _ -> hooks.reset ())
                                  Button.isEnabled canReset.Current
                                  Button.horizontalAlignment HorizontalAlignment.Center ] ] ]
        )

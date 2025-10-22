namespace HelloAvaloniaFuncUI.Main.Counter

open Avalonia
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module CounterActionButtonView =
    let create (hooks: CounterHooks) =
        Component.create (
            "CounterActionButtonView",
            fun ctx ->
                let canIncrement = ctx.usePassedRead hooks.CanIncrement
                let canDecrement = ctx.usePassedRead hooks.CanDecrement
                let canReset = ctx.usePassedRead hooks.CanReset

                Grid.create
                    [ Grid.width 300.0
                      Grid.columnDefinitions "1*,1*"
                      Grid.rowDefinitions "Auto,Auto"
                      Grid.children
                          [ Button.create
                                [ Button.content "Increment"
                                  Button.onClick (fun _ -> hooks.Increment())
                                  Button.isEnabled canIncrement.Current
                                  Button.horizontalAlignment HorizontalAlignment.Stretch
                                  Button.horizontalContentAlignment HorizontalAlignment.Center
                                  Button.margin (Thickness(5.0, 0.0))
                                  Grid.column 0 ]
                            Button.create
                                [ Button.content "Decrement"
                                  Button.onClick (fun _ -> hooks.Decrement())
                                  Button.isEnabled canDecrement.Current
                                  Button.horizontalAlignment HorizontalAlignment.Stretch
                                  Button.horizontalContentAlignment HorizontalAlignment.Center
                                  Button.margin (Thickness(5.0, 0.0))
                                  Grid.column 1 ]
                            Button.create
                                [ Button.content "Reset"
                                  Button.onClick (fun _ -> hooks.Reset())
                                  Button.isEnabled canReset.Current
                                  Button.horizontalAlignment HorizontalAlignment.Stretch
                                  Button.horizontalContentAlignment HorizontalAlignment.Center
                                  Button.margin (Thickness(5.0, 10.0))
                                  Grid.columnSpan 2
                                  Grid.row 1 ] ] ]
        )

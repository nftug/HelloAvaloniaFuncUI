namespace HelloAvaloniaFuncUI.Main.Counter

open System
open Avalonia
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module CounterActionButtonView =
    let private incAndDecDelay = TimeSpan.FromMilliseconds 100.0
    let private resetDelay = TimeSpan.FromMilliseconds 500.0

    let create
        (count: IReadable<int>)
        (isSetting: IReadable<bool>)
        (setCountWithDelay: TimeSpan -> int -> unit)
        =
        Component.create (
            "CounterActionButtonView",
            fun ctx ->
                let count = ctx.usePassedRead count
                let isSetting = ctx.usePassedRead isSetting

                let canIncrement = ctx.useDerived1 (isSetting, not)

                let canDecrement =
                    ctx.useDerived2 ((isSetting, count), (fun (s, c) -> not s && c > 0))

                let canReset = ctx.useDerived2 ((isSetting, count), (fun (s, c) -> not s && c <> 0))

                let increment () =
                    if canIncrement.Current then
                        setCountWithDelay incAndDecDelay (count.Current + 1)

                let decrement () =
                    if canDecrement.Current then
                        setCountWithDelay incAndDecDelay (count.Current - 1)

                let reset () =
                    if canReset.Current then
                        setCountWithDelay resetDelay 0

                Grid.create
                    [ Grid.width 300.0
                      Grid.columnDefinitions "1*,1*"
                      Grid.rowDefinitions "Auto,Auto"
                      Grid.children
                          [ Button.create
                                [ Button.content "Increment"
                                  Button.onClick (fun _ -> increment ())
                                  Button.isEnabled canIncrement.Current
                                  Button.horizontalAlignment HorizontalAlignment.Stretch
                                  Button.horizontalContentAlignment HorizontalAlignment.Center
                                  Button.margin (Thickness(5.0, 0.0))
                                  Grid.column 0 ]
                            Button.create
                                [ Button.content "Decrement"
                                  Button.onClick (fun _ -> decrement ())
                                  Button.isEnabled canDecrement.Current
                                  Button.horizontalAlignment HorizontalAlignment.Stretch
                                  Button.horizontalContentAlignment HorizontalAlignment.Center
                                  Button.margin (Thickness(5.0, 0.0))
                                  Grid.column 1 ]
                            Button.create
                                [ Button.content "Reset"
                                  Button.onClick (fun _ -> reset ())
                                  Button.isEnabled canReset.Current
                                  Button.horizontalAlignment HorizontalAlignment.Stretch
                                  Button.horizontalContentAlignment HorizontalAlignment.Center
                                  Button.margin (Thickness(5.0, 10.0))
                                  Grid.columnSpan 2
                                  Grid.row 1 ] ] ]
        )

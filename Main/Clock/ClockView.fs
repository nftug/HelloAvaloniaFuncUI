namespace HelloAvaloniaFuncUI.Main.Clock

open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module ClockView =
    let create () =
        Component.create (
            "ClockView",
            fun ctx ->
                let hooks = useClockHooks ctx

                StackPanel.create
                    [ StackPanel.margin 15.0
                      StackPanel.horizontalAlignment HorizontalAlignment.Stretch
                      StackPanel.verticalAlignment VerticalAlignment.Stretch
                      StackPanel.children
                          [ TextBlock.create
                                [ TextBlock.text (hooks.Now.Current.ToString "HH:mm:ss")
                                  TextBlock.fontSize 48.0
                                  TextBlock.horizontalAlignment HorizontalAlignment.Center ] ]
                      StackPanel.horizontalAlignment HorizontalAlignment.Center
                      StackPanel.verticalAlignment VerticalAlignment.Center ]
        )

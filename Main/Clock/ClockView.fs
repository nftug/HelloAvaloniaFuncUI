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
                let now = ctx.usePassedRead hooks.Now

                TextBlock.create
                    [ TextBlock.text (now.Current.ToString "HH:mm:ss")
                      TextBlock.fontSize 48.0
                      TextBlock.horizontalAlignment HorizontalAlignment.Center
                      TextBlock.verticalAlignment VerticalAlignment.Center ]
        )

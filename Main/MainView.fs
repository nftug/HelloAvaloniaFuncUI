namespace HelloAvaloniaFuncUI.Main

open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module MainView =
    let create () =
        Component(fun _ ->
            StackPanel.create
                [ StackPanel.spacing 20.0
                  StackPanel.margin 20.0
                  StackPanel.horizontalAlignment HorizontalAlignment.Center
                  StackPanel.verticalAlignment VerticalAlignment.Center
                  StackPanel.children [ Clock.ClockView.create (); Counter.CounterView.create () ] ])

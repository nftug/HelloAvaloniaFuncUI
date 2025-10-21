namespace HelloAvaloniaFuncUI.Main

open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module MainView =
    let view =
        Component(fun _ ->
            StackPanel.create
                [ StackPanel.spacing 10.0
                  StackPanel.horizontalAlignment HorizontalAlignment.Center
                  StackPanel.verticalAlignment VerticalAlignment.Center
                  StackPanel.children [ Clock.ClockView.view (); Counter.CounterView.view () ]
                  StackPanel.horizontalAlignment HorizontalAlignment.Center
                  StackPanel.verticalAlignment VerticalAlignment.Center ])

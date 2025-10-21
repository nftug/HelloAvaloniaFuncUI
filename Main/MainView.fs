namespace HelloAvaloniaFuncUI.Main

open Avalonia
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module MainView =
    // View
    let view =
        Component(fun ctx ->
            let hooks = MainHooks.useMainHooks ctx

            let actionButtons =
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
                                              Button.isEnabled hooks.canIncrement ]
                                        Button.create
                                            [ Button.content "Decrement"
                                              Button.onClick (fun _ -> hooks.decrement ())
                                              Button.isEnabled hooks.canDecrement ] ] ]
                            Button.create
                                [ Button.content "Reset"
                                  Button.onClick (fun _ -> hooks.reset ())
                                  Button.isEnabled hooks.canReset
                                  Button.horizontalAlignment HorizontalAlignment.Center ] ] ]

            StackPanel.create
                [ StackPanel.spacing 10.0
                  StackPanel.horizontalAlignment HorizontalAlignment.Center
                  StackPanel.verticalAlignment VerticalAlignment.Center
                  StackPanel.children
                      [ Clock.ClockView.view ()
                        TextBlock.create
                            [ TextBlock.text $"Count: {hooks.count.Current}"
                              TextBlock.fontSize 24.0
                              TextBlock.margin (Thickness(0.0, 0.0, 0.0, 20.0))
                              TextBlock.horizontalAlignment HorizontalAlignment.Center ]
                        actionButtons ]
                  StackPanel.horizontalAlignment HorizontalAlignment.Center
                  StackPanel.verticalAlignment VerticalAlignment.Center ])

namespace HelloAvaloniaFuncUI.Main

open Avalonia
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout

module MainView =
    let view (model: Model) (dispatch: Msg -> unit) =
        let actionButtons =
            StackPanel.create
                [ StackPanel.orientation Orientation.Horizontal
                  StackPanel.spacing 10.0
                  StackPanel.children
                      [ Button.create [ Button.content "Increment"; Button.onClick (fun _ -> dispatch Increment) ]
                        Button.create
                            [ Button.content "Decrement"
                              Button.onClick (fun _ -> dispatch Decrement)
                              Button.isEnabled (model.Count > 0) ] ] ]

        StackPanel.create
            [ StackPanel.children
                  [ TextBlock.create
                        [ TextBlock.text $"Count: {model.Count}"
                          TextBlock.fontSize 24.0
                          TextBlock.margin (Thickness(0.0, 0.0, 0.0, 20.0))
                          TextBlock.horizontalAlignment HorizontalAlignment.Center ]
                    actionButtons ]
              StackPanel.horizontalAlignment HorizontalAlignment.Center
              StackPanel.verticalAlignment VerticalAlignment.Center ]

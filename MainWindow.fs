namespace HelloAvaloniaFuncUI

open Avalonia.FuncUI.Hosts
open Avalonia.FuncUI.Elmish
open Elmish
open HelloAvaloniaFuncUI.Main

type MainWindow() as this =
    inherit HostWindow()

    do
        this.Title <- "Hello Avalonia FuncUI"
        this.Width <- 400.0
        this.Height <- 300.0
        this.WindowStartupLocation <- Avalonia.Controls.WindowStartupLocation.CenterScreen

        Program.mkSimple MainModel.init MainUpdate.update MainView.view
        |> Program.withHost this
        |> Program.run

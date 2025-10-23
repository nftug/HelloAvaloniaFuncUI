namespace HelloAvaloniaFuncUI

open Avalonia.FuncUI.Hosts
open HelloAvaloniaFuncUI.Main

type MainWindow() as this =
    inherit HostWindow()

    do
        this.Title <- "Hello Avalonia FuncUI"
        this.Width <- 800.0
        this.Height <- 600.0
        this.WindowStartupLocation <- Avalonia.Controls.WindowStartupLocation.CenterScreen
        this.Content <- MainView.create ()

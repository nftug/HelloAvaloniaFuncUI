namespace HelloAvaloniaFuncUI

open System
open Avalonia.FuncUI.Hosts
open Avalonia.FuncUI.Elmish
open Elmish
open HelloAvaloniaFuncUI.Main

type MainWindow() as this =
    inherit HostWindow()

    do
        this.Title <- "Hello Avalonia FuncUI"
        this.Width <- 800.0
        this.Height <- 600.0
        this.WindowStartupLocation <- Avalonia.Controls.WindowStartupLocation.CenterScreen

        // Workaround to bring window to front on startup on macOS
        if OperatingSystem.IsMacOS() then
            this.Opened.Add(fun _ ->
                Avalonia.Threading.Dispatcher.UIThread.Post(fun _ ->
                    this.Topmost <- true
                    this.Activate()
                    this.Topmost <- false))

        Program.mkProgram MainModel.init MainUpdate.update MainView.view
        |> Program.withHost this
        |> Program.runWithAvaloniaSyncDispatch ()

namespace HelloAvaloniaFuncUI

open Avalonia
open Avalonia.Themes.Fluent
open Avalonia.Controls.ApplicationLifetimes

module Program =
    type App() =
        inherit Application()

        override this.OnFrameworkInitializationCompleted() : unit =
            this.Styles.Add(FluentTheme())

            match this.ApplicationLifetime with
            | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
                desktopLifetime.MainWindow <- MainWindow()
            | _ -> ()

    [<EntryPoint>]
    let main argv =
        AppBuilder.Configure<App>().UsePlatformDetect().StartWithClassicDesktopLifetime argv

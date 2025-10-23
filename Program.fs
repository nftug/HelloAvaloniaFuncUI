namespace HelloAvaloniaFuncUI

open Avalonia
open Avalonia.Themes.Fluent
open Avalonia.Controls.ApplicationLifetimes

type App() =
    inherit Application()

    override this.Initialize() : unit = this.Styles.Add(FluentTheme())

    override this.OnFrameworkInitializationCompleted() : unit =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

module Program =
    [<EntryPoint>]
    let main argv =
        AppBuilder.Configure<App>().UsePlatformDetect().StartWithClassicDesktopLifetime argv

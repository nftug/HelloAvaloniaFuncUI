namespace HelloAvaloniaFuncUI.Common.Utils

open System.Runtime.CompilerServices
open Avalonia.FuncUI

[<Extension>]
type __ComponentUtilsExtension =
    [<Extension>]
    static member useBinding
        (
            ctx: IComponentContext,
            model: IWritable<'T>,
            toBinding: 'T -> 'U,
            toModel: 'U -> 'T
        ) : IWritable<'U> =
        let binding = ctx.useState (toBinding model.Current)

        ctx.useEffect (
            fun () ->
                if binding.Current <> toBinding model.Current then
                    binding.Set(toBinding model.Current)
            , [ EffectTrigger.AfterChange model ]
        )

        ctx.useEffect (
            fun () ->
                if model.Current |> toBinding <> binding.Current then
                    model.Set(toModel binding.Current)
            , [ EffectTrigger.AfterChange binding ]
        )

        binding

namespace Avalonia.FuncUI

open System.Runtime.CompilerServices

[<Extension>]
type __ComponentContextExtensions =
    [<Extension>]
    static member useBinding
        (
            ctx: IComponentContext,
            model: IWritable<'M>,
            toBinding: 'M -> 'B,
            toModel: 'B -> 'M -> 'M
        ) : IWritable<'B> =
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
                    model.Set(toModel binding.Current model.Current)
            , [ EffectTrigger.AfterChange binding ]
        )

        binding

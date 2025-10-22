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

    [<Extension>]
    static member useMerged
        (
            ctx: IComponentContext,
            model: IReadable<'T>,
            mergeFunc: 'T -> 'U option -> 'U
        ) : IWritable<'U> =
        let model = ctx.usePassedRead model
        let merged = ctx.useState (mergeFunc model.Current None)

        ctx.useEffect (
            fun () ->
                let next = mergeFunc model.Current (Some merged.Current)

                if next <> merged.Current then
                    merged.Set next
            , [ EffectTrigger.AfterChange model ]
        )

        merged

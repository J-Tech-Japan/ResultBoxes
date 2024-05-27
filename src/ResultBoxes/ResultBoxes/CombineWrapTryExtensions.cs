namespace ResultBoxes;

public static class CombineWrapTryExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await current.RemapResultAsync(
            async first =>
                (await ResultBox<TValue2>.WrapTry(secondValueFunc)).Remap(first.Append));
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<TValue1, Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await current.RemapResultAsync(
            async first =>
                (await ResultBox<TValue2>.WrapTry(async () => await secondValueFunc(first.GetValue()))).Remap(first.Append));
    
    public static ResultBox<TwoValues<TValue, TValue2>>
        CombineWrapTry<TValue, TValue2>(
            this ResultBox<TValue> current,
            Func<TValue2> secondValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current.RemapResult(
            c => ResultBox<TValue2>.WrapTry(secondValueFunc).Remap(current.Append));
    
    public static ResultBox<TwoValues<TValue1, TValue2>>
        CombineWrapTry<TValue1, TValue2>(
            this ResultBox<TValue1> current,
            Func<TValue1,TValue2> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current.RemapResult(
            c => ResultBox<TValue2>.WrapTry(() => secondValueFunc(c.GetValue())).Remap(current.Append));

    
    
    // public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
    //     TValue1, TValue2, TValue3>(
    //     this ResultBox<TwoValues<TValue1, TValue2>> current,
    //     Func<Task<TValue3>> secondValueFunc)
    //     where TValue1 : notnull
    //     where TValue2 : notnull
    //     where TValue3 : notnull
    //     => await current.RemapResultAsync(
    //         async first =>
    //             (await ResultBox<TValue3>.WrapTry(secondValueFunc)).Remap(first.GetValue().Append));
    // public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
    //     TValue1, TValue2, TValue3>(
    //     this ResultBox<TwoValues<TValue1, TValue2>> current,
    //     Func<TValue1, TValue2, Task<TValue3>> secondValueFunc)
    //     where TValue1 : notnull
    //     where TValue2 : notnull
    //     where TValue3 : notnull
    //     => await current.RemapAsync(
    //         async first =>
    //             (await ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.WrapTry(async () => await first.Call(secondValueFunc).Remap(first.Append));
    //
    // public static ResultBox<TwoValues<TValue, TValue2>>
    //     CombineWrapTry<TValue, TValue2>(
    //         this ResultBox<TValue> current,
    //         Func<TValue2> secondValueFunc)
    //     where TValue : notnull
    //     where TValue2 : notnull
    //     => current.RemapResult(
    //         c => ResultBox<TValue2>.WrapTry(secondValueFunc).Remap(current.Append));
    //
    // public static ResultBox<TwoValues<TValue1, TValue2>>
    //     CombineWrapTry<TValue1, TValue2>(
    //         this ResultBox<TValue1> current,
    //         Func<TValue1,TValue2> secondValueFunc)
    //     where TValue1 : notnull
    //     where TValue2 : notnull
    //     => current.RemapResult(
    //         c => ResultBox<TValue2>.WrapTry(() => secondValueFunc(c.GetValue())).Remap(current.Append));

    
    
}

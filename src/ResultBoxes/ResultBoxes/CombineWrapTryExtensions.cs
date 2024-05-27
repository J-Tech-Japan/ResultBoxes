namespace ResultBoxes;

public static class CombineWrapTryExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await current.ConveyorResult(
            async first =>
                (await ResultBox.WrapTry(secondValueFunc)).Conveyor(first.Append));
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<TValue1, Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await current.ConveyorResult(
            async first =>
                (await ResultBox.WrapTry(async () => await secondValueFunc(first.GetValue())))
                .Conveyor(first.Append));

    public static ResultBox<TwoValues<TValue, TValue2>>
        CombineWrapTry<TValue, TValue2>(
            this ResultBox<TValue> current,
            Func<TValue2> secondValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current.ConveyorResult(
            c => ResultBox.WrapTry(secondValueFunc).Conveyor(current.Append));

    public static ResultBox<TwoValues<TValue1, TValue2>>
        CombineWrapTry<TValue1, TValue2>(
            this ResultBox<TValue1> current,
            Func<TValue1, TValue2> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current.ConveyorResult(
            c => ResultBox.WrapTry(() => secondValueFunc(c.GetValue())).Conveyor(current.Append));



    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
        TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<Task<TValue3>> lastValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await current.ConveyorResult(
            async first =>
                (await ResultBox.WrapTry(lastValueFunc)).Remap(first.GetValue().Append));
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
        TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, Task<TValue3>> lastValueFuncAsync)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await current.Conveyor(
            async first =>
                await ResultBox.WrapTry(
                    async () => first.Append(await first.Call(lastValueFuncAsync))));

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>>
        CombineWrapTry<TValue1, TValue2, TValue3>(
            this ResultBox<TwoValues<TValue1, TValue2>> current,
            Func<TValue3> lastValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => current.Conveyor(
            values => ResultBox.WrapTry(() => values.Append(lastValueFunc())));

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>>
        CombineWrapTry<TValue1, TValue2, TValue3>(
            this ResultBox<TwoValues<TValue1, TValue2>> current,
            Func<TValue1, TValue2, TValue3> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => current.Conveyor(
            values => ResultBox.WrapTry(() => values.Append(values.Call(secondValueFunc))));
}

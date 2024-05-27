namespace ResultBoxes;

public static class ConveyorWrapTryTaskExtensions
{
    public static async Task<ResultBox<TValue3>> ConveyorWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox<TValue3>
                .WrapTry(() => handleValueFunc(values.Value1, values.Value2)));

    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, Task<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValue).Conveyor(
            async value => await ResultBox<TValue2>.WrapTry(
                () => handleValueFunc(value)));

    public static async Task<ResultBox<TValue3>> ConveyorWrapTry<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox<TValue3>.WrapTry(async () => await values.Call(handleValueFunc)));
}

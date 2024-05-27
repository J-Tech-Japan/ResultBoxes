namespace ResultBoxes;

public static class ConveyorWrapTryTaskExtensions
{
    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, Task<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValue).Conveyor(
            async value => await ResultBox.WrapTry(
                () => handleValueFunc(value)));
    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, TValue2> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await firstValue).Conveyor(
            value => ResultBox.WrapTry(() => handleValueFunc(value)));

    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<Task<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValue).Conveyor(
            async value => await ResultBox.WrapTry(
                handleValueFunc));
    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue2> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await firstValue).Conveyor(
            value => ResultBox.WrapTry(handleValueFunc));

    
    public static async Task<ResultBox<TValue3>> ConveyorWrapTry<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await values.Call(handleValueFunc)));
}

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

    
    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValueResult>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await values.Call(handleValueFunc)));
    
    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValueResult>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValueResult>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<Task<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await handleValueFunc()));
    
    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValueResult>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(_ => ResultBox.WrapTry(handleValueFunc));

}

namespace ResultBoxes;

public static class ConveyorWrapTryExtensions
{

    public static ResultBox<TValue2> ConveyorWrapTry<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, TValue2> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        =>
            current.Conveyor(
                value => ResultBox.WrapTry(
                    () => handleValueFunc(value)));
    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, Task<TValue2>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        =>
            await current.Conveyor(
                value => ResultBox.WrapTry(
                    async () => await handleValueFunc(value)));

    public static ResultBox<TValue2> ConveyorWrapTry<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue2> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        =>
            current.Conveyor(
                _ => ResultBox.WrapTry(handleValueFunc));
    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<Task<TValue2>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        =>
            await current.Conveyor(
                _ => ResultBox.WrapTry(
                    async () => await handleValueFunc()));

    
    
    public static ResultBox<TValue3> ConveyorWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, TValue3> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValue3>> ConveyorWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task< TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue.Conveyor(
             async values => await ResultBox.WrapTry(async () => await values.Call(handleValueFunc)));

    public static ResultBox<TValue3> ConveyorWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func< TValue3> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue.Conveyor(
            _ => ResultBox.WrapTry(handleValueFunc));

    public static async Task<ResultBox<TValue3>> ConveyorWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<Task< TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await handleValueFunc()));

    
    public static ResultBox<TValue4> ConveyorWrapTry<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValue4>> ConveyorWrapTry<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> firstValue,
        Func<TValue1, TValue2, TValue3, Task< TValue4>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await values.Call(handleValueFunc)));

    public static ResultBox<TValue4> ConveyorWrapTry<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> firstValue,
        Func< TValue4> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(handleValueFunc));

    public static async Task<ResultBox<TValue4>> ConveyorWrapTry<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> firstValue,
        Func<Task< TValue4>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await handleValueFunc()));

    
    
    
}

namespace ResultBoxes;

public static class ConveyorWrapTryExtensions
{

    public static ResultBox<TValueResult> ConveyorWrapTry<TValue, TValueResult>(
        this ResultBox<TValue> current,
        Func<TValue, TValueResult> handleValueFunc)
        where TValue : notnull
        where TValueResult : notnull
        =>
            current.Conveyor(
                value => ResultBox.WrapTry(
                    () => handleValueFunc(value)));
    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue, TValueResult>(
        this ResultBox<TValue> current,
        Func<TValue, Task<TValueResult>> handleValueFunc)
        where TValue : notnull
        where TValueResult : notnull
        =>
            await current.Conveyor(
                value => ResultBox.WrapTry(
                    async () => await handleValueFunc(value)));

    public static ResultBox<TValueResult> ConveyorWrapTry<TValue, TValueResult>(
        this ResultBox<TValue> current,
        Func<TValueResult> handleValueFunc)
        where TValue : notnull
        where TValueResult : notnull
        =>
            current.Conveyor(
                _ => ResultBox.WrapTry(handleValueFunc));
    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue, TValueResult>(
        this ResultBox<TValue> current,
        Func<Task<TValueResult>> handleValueFunc)
        where TValue : notnull
        where TValueResult : notnull
        =>
            await current.Conveyor(
                _ => ResultBox.WrapTry(
                    async () => await handleValueFunc()));

    
    
    public static ResultBox<TValueResult> ConveyorWrapTry<TValue1, TValue2, TValueResult>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValueResult>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task< TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => await firstValue.Conveyor(
             async values => await ResultBox.WrapTry(async () => await values.Call(handleValueFunc)));

    public static ResultBox<TValueResult> ConveyorWrapTry<TValue1, TValue2, TValueResult>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func< TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => firstValue.Conveyor(
            _ => ResultBox.WrapTry(handleValueFunc));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValueResult>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<Task< TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await handleValueFunc()));

    
    public static ResultBox<TValueResult> ConveyorWrapTry<TValue1, TValue2, TValue3, TValueResult>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> firstValue,
        Func<TValue1, TValue2, TValue3, TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3, TValueResult>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> firstValue,
        Func<TValue1, TValue2, TValue3, Task< TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await values.Call(handleValueFunc)));

    public static ResultBox<TValueResult> ConveyorWrapTry<TValue1, TValue2, TValue3, TValueResult>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> firstValue,
        Func< TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(handleValueFunc));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3, TValueResult>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> firstValue,
        Func<Task< TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await handleValueFunc()));

    

    public static ResultBox<TValueResult> ConveyorWrapTry<TValue1, TValue2, TValue3,TValue4, TValueResult>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3,TValue4>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,TValue4, TValueResult>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, Task< TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await values.Call(handleValueFunc)));

    public static ResultBox<TValueResult> ConveyorWrapTry<TValue1, TValue2, TValue3,TValue4, TValueResult>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> firstValue,
        Func< TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(handleValueFunc));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,TValue4, TValueResult>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> firstValue,
        Func<Task< TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await handleValueFunc()));


        public static ResultBox<TValueResult> ConveyorWrapTry<TValue1, TValue2, TValue3,TValue4,TValue5, TValueResult>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3,TValue4,TValue5>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,TValue4,TValue5, TValueResult>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4,TValue5>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task< TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await values.Call(handleValueFunc)));

    public static ResultBox<TValueResult> ConveyorWrapTry<TValue1, TValue2, TValue3,TValue4,TValue5, TValueResult>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4,TValue5>> firstValue,
        Func< TValueResult> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => firstValue.Conveyor(
            values => ResultBox.WrapTry(handleValueFunc));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,TValue4,TValue5, TValueResult>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4,TValue5>> firstValue,
        Func<Task< TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => await firstValue.Conveyor(
            async values => await ResultBox.WrapTry(async () => await handleValueFunc()));

    
}

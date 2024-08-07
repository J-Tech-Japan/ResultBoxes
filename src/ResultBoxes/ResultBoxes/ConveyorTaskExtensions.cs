namespace ResultBoxes;

public static class ConveyorTaskExtensions
{
    #region single value
    public static async Task<ResultBox<TValue2>> Conveyor<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, ResultBox<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue.Conveyor(value => Task.FromResult(handleValueFunc(value)));

    public static async Task<ResultBox<TValue2>> Conveyor<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, Task<ResultBox<TValue2>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValue).Conveyor(handleValueFunc);
    public static async Task<ResultBox<TValue2>> Conveyor<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<ResultBox<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue.Conveyor(value => Task.FromResult(handleValueFunc()));

    public static async Task<ResultBox<TValue2>> Conveyor<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<Task<ResultBox<TValue2>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValue).Conveyor(handleValueFunc);
    #endregion



    #region two values
    public static async Task<ResultBox<TValue3>> Conveyor<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<ResultBox<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await firstValue).Conveyor(async values => await values.Call(handleValueFunc));

    public static async Task<ResultBox<TValue3>> Conveyor<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, ResultBox<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await firstValue).Conveyor(
            values => Task.FromResult(values.Call(handleValueFunc)));
    #endregion


    #region three values
    public static async Task<ResultBox<TValueReturn>> Conveyor<TValue1, TValue2, TValue3,
        TValueReturn>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> current,
        Func<TValue1, TValue2, TValue3, ResultBox<TValueReturn>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueReturn : notnull
        => await current.Conveyor(values => Task.FromResult(values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValue4>>
        Conveyor<TValue1, TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
            Func<TValue1, TValue2, TValue3, Task<ResultBox<TValue4>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await firstValue).Conveyor(async values => await values.Call(handleValueFunc));
    #endregion


    #region four values
    public static async Task<ResultBox<TValue5>> Conveyor<TValue1, TValue2, TValue3, TValue4,
        TValue5>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, Task<ResultBox<TValue5>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await firstValue).Conveyor(async values => await values.Call(handleValueFunc));

    public static async Task<ResultBox<TValue5>> Conveyor<TValue1, TValue2, TValue3, TValue4,
        TValue5>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, ResultBox<TValue5>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await firstValue).Conveyor(values => values.Call(handleValueFunc));
    #endregion

    #region five values
    public static async Task<ResultBox<TValue6>> Conveyor<TValue1, TValue2, TValue3, TValue4,
        TValue5, TValue6>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<ResultBox<TValue6>>>
            handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValue6 : notnull
        => await (await firstValue).Conveyor(async values => await values.Call(handleValueFunc));
    public static async Task<ResultBox<TValue6>> Conveyor<TValue1, TValue2, TValue3, TValue4,
        TValue5, TValue6>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, ResultBox<TValue6>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValue6 : notnull
        => (await firstValue).Conveyor(values => values.Call(handleValueFunc));
    #endregion
}

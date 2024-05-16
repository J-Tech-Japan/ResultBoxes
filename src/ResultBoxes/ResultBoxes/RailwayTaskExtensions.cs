namespace ResultBoxes;

public static class RailwayTaskExtensions
{
    public static async Task<ResultBox<TValue2>> Railway<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, Task<ResultBox<TValue2>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue.HandleAsync(async value => await handleValueFunc(value));

    public static async Task<ResultBox<TValue2>> Railway<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, ResultBox<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue.HandleAsync(value => Task.FromResult(handleValueFunc(value)));


    public static async Task<ResultBox<TValueReturn>> Railway<TValue1, TValue2, TValue3,
        TValueReturn>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> current,
        Func<TValue1, TValue2, TValue3, ResultBox<TValueReturn>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueReturn : notnull
        => await current.HandleAsync(values => Task.FromResult(values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, Task<ResultBox<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await current.HandleAsync(async values => await values.Call(handleValueFunc));

    public static async Task<ResultBox<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<ResultBox<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await firstValue).HandleAsync(async values => await values.Call(handleValueFunc));

    public static async Task<ResultBox<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, ResultBox<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await firstValue).HandleAsync(
            values => Task.FromResult(values.Call(handleValueFunc)));

    public static async Task<ResultBox<TValue4>>
        Railway<TValue1, TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
            Func<TValue1, TValue2, TValue3, Task<ResultBox<TValue4>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await firstValue).HandleAsync(async values => await values.Call(handleValueFunc));

    public static async Task<ResultBox<TValue5>> Railway<TValue1, TValue2, TValue3, TValue4,
        TValue5>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, Task<ResultBox<TValue5>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await firstValue).HandleAsync(async values => await values.Call(handleValueFunc));

    public static async Task<ResultBox<TValue6>> Railway<TValue1, TValue2, TValue3, TValue4,
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
        => await (await firstValue).HandleAsync(async values => await values.Call(handleValueFunc));
}

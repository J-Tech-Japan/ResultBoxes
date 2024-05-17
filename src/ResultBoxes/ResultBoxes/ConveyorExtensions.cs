namespace ResultBoxes;

public static class ConveyorExtensions
{
    public static ResultBox<TValueResult> Conveyor<TValue1, TValue2, TValue3, TValue4, TValue5,
        TValueResult>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> current,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, ResultBox<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => current.Handle(value => value.Call(handleValueFunc));

    public static ResultBox<TValueResult> Conveyor<TValue1, TValue2, TValue3, TValue4, TValueResult>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
        Func<TValue1, TValue2, TValue3, TValue4, ResultBox<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => current.Handle(value => value.Call(handleValueFunc));

    public static ResultBox<TValueResult> Conveyor<TValue1, TValue2, TValue3, TValueResult>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<TValue1, TValue2, TValue3, ResultBox<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => current.Handle(value => value.Call(handleValueFunc));

    public static ResultBox<TValue3> Conveyor<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, ResultBox<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => current.Handle(value => value.Call(handleValueFunc));

    public static ResultBox<TValue2> Conveyor<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, ResultBox<TValue2>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current.Handle(handleValueFunc);

    public static async Task<ResultBox<TValue2>> Conveyor<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, Task<ResultBox<TValue2>>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => await current.HandleAsync(handleValueFunc);
}

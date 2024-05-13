namespace SingleResults;

public static class TwoValueExtensions
{
    public static SingleValueResult<TValue3> Railway<TValue1, TValue2, TValue3>(
        this SingleValueResult<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, SingleValueResult<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { Value1: { } value1, Value2: { } value2 } } => handleValueFunc(
                    value1,
                    value2),
                _ => SingleValueResult<TValue3>.OutOfRange
            };
    public static Func<TValue1, TValue2, SingleValueResult<TValueResult>> ToFunc<TValue1, TValue2, TValueResult>(this Func<TwoValues<TValue1, TValue2>, SingleValueResult<TValueResult>> func) where TValue1 : notnull where TValue2 : notnull where TValueResult : notnull
        => (value1, value2) => func(new TwoValues<TValue1, TValue2>(value1, value2));

    public static async Task<SingleValueResult<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this SingleValueResult<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<SingleValueResult<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        =>
            firstValue
                switch
                {
                    { Exception: not null } e => e.Exception,
                    { Value: { } values } => await handleValueFunc(
                        values.Value1,
                        values.Value2),
                    _ => SingleValueResult<TValue3>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<SingleValueResult<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<SingleValueResult<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: not null } e => e.Exception,
                    { Value: { } values } => await handleValueFunc(
                        values.Value1,
                        values.Value2),
                    _ => SingleValueResult<TValue3>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue3>> RailwayWrapTry<TValue1, TValue2, TValue3>(
        this SingleValueResult<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } values } => await SingleValueResult<TValue3>
                    .WrapTry(() => handleValueFunc(values.Value1, values.Value2)),
                _ => SingleValueResult<TValue3>.OutOfRange
            };

    public static SingleValueResult<TValue3> RailwayWrapTry<TValue1, TValue2, TValue3>(
        this SingleValueResult<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, TValue3> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } values } => SingleValueResult<TValue3>
                    .WrapTry(
                        () => handleValueFunc(values.Value1, values.Value2)),
                _ => SingleValueResult<TValue3>.OutOfRange
            };

    public static async Task<SingleValueResult<TValue3>> RailwayWrapTry<TValue1, TValue2, TValue3>(
        this Task<SingleValueResult<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } values } => await SingleValueResult<TValue3>
                    .WrapTry(
                        () => handleValueFunc(values.Value1, values.Value2)),
                _ => SingleValueResult<TValue3>.OutOfRange
            };


}
namespace SingleResults;

public static class RailwayWrapTryTaskExtensions
{
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

    public static async Task<SingleValueResult<TValue2>> RailwayWrapTry<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValue,
        Func<TValue1, Task<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => await SingleValueResult<TValue2>.WrapTry(
                    () => handleValueFunc(value)),
                _ => SingleValueResult<TValue2>.OutOfRange
            };
}

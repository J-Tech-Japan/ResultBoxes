namespace ResultBoxes;

public static class RailwayWrapTryTaskExtensions
{
    public static async Task<ResultBox<TValue3>> RailwayWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } values } => await ResultBox<TValue3>
                    .WrapTry(() => handleValueFunc(values.Value1, values.Value2)),
                _ => ResultBox<TValue3>.OutOfRange
            };

    public static async Task<ResultBox<TValue2>> RailwayWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, Task<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => await ResultBox<TValue2>.WrapTry(
                    () => handleValueFunc(value)),
                _ => ResultBox<TValue2>.OutOfRange
            };
}

namespace SingleResults;

public static class RailwayExtensions
{
    public static SingleValueResult<TValue2> Railway<TValue, TValue2>(
        this SingleValueResult<TValue> current,
        Func<TValue, SingleValueResult<TValue2>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => handleValueFunc(value),
                _ => SingleValueResult<TValue2>.OutOfRange
            };

    public static async Task<SingleValueResult<TValue2>> Railway<TValue, TValue2>(
        this SingleValueResult<TValue> current,
        Func<TValue, Task<SingleValueResult<TValue2>>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => await handleValueFunc(value),
                _ => SingleValueResult<TValue2>.OutOfRange
            };
}

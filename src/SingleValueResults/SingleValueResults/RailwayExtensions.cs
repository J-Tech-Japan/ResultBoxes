namespace SingleResults;

public static class RailwayExtensions
{
    public static SingleValueResult<TValueResult> Railway<TValue1, TValue2, TValue3, TValueResult>(
        this SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<TValue1, TValue2, TValue3,SingleValueResult<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => current
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => handleValueFunc( value.Value1, value.Value2, value.Value3),
                _ => SingleValueResult<TValueResult>.OutOfRange
            };

    
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

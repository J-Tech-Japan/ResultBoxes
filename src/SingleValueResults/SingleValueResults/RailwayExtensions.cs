namespace SingleResults;

public static class RailwayExtensions
{
    public static SingleValueResult<TValueResult> Railway<TValue1, TValue2, TValue3, TValue4, TValue5, TValueResult>(
        this SingleValueResult<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> current,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, SingleValueResult<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => current
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => handleValueFunc( value.Value1, value.Value2, value.Value3, value.Value4, value.Value5),
                _ => SingleValueResult<TValueResult>.OutOfRange
            };

    public static SingleValueResult<TValueResult> Railway<TValue1, TValue2, TValue3, TValue4, TValueResult>(
        this SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
        Func<TValue1, TValue2, TValue3, TValue4, SingleValueResult<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => current
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => handleValueFunc( value.Value1, value.Value2, value.Value3, value.Value4),
                _ => SingleValueResult<TValueResult>.OutOfRange
            };

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

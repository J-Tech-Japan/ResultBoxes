namespace SingleResults;

public static class RailwayWrapTryExtensions
{
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

    public static SingleValueResult<TValue2> RailwayWrapTry<TValue, TValue2>(
        this SingleValueResult<TValue> current,
        Func<TValue, TValue2> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        =>
            current switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => SingleValueResult<TValue2>.WrapTry(
                    () => handleValueFunc(value)),
                _ => SingleValueResult<TValue2>.OutOfRange
            };
}

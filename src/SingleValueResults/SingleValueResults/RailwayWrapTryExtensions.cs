namespace SingleResults;

public static class RailwayWrapTryExtensions
{
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

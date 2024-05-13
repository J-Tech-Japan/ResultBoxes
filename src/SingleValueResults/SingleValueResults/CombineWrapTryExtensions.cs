namespace SingleResults;

public static class CombineWrapTryExtensions
{
    public static async Task<SingleValueResult<TwoValues<TValue1, TValue2>>> CombineValueWrapTry<
        TValue1, TValue2>(
        this SingleValueResult<TValue1> current,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: not null } => await SingleValueResult<TValue2>.WrapTry(secondValueFunc)
                switch
                {
                    { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                        default,
                        e.Exception),
                    { Value: { } secondValue } => new
                        SingleValueResult<TwoValues<TValue1, TValue2>>(
                            new TwoValues<TValue1, TValue2>(current.Value, secondValue),
                            null),
                    _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(default, null)
                },
            _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue1)} combine to {nameof(TValue2)}"))
        };
    public static SingleValueResult<TwoValues<TValue, TValue2>>
        CombineValueWrapTry<TValue, TValue2>(
            this SingleValueResult<TValue> current,
            Func<TValue2> secondValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: not null } e => new SingleValueResult<TwoValues<TValue, TValue2>>(
                default,
                e.Exception),
            { Value: not null } => SingleValueResult<TValue2>.WrapTry(secondValueFunc) switch
            {
                { Exception: not null } e => new SingleValueResult<TwoValues<TValue, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new SingleValueResult<TwoValues<TValue, TValue2>>(
                    new TwoValues<TValue, TValue2>(current.Value, secondValue),
                    null),
                _ => new SingleValueResult<TwoValues<TValue, TValue2>>(default, null)
            },
            _ => new SingleValueResult<TwoValues<TValue, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };
}

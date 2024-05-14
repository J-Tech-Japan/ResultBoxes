namespace ResultBoxes;

public static class CombineWrapTryExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineValueWrapTry<
        TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: { } error }  => error,
            { Value: not null } => await ResultBox<TValue2>.WrapTry(secondValueFunc)
                switch
                {
                    { Exception: { } error }  => error,
                    { Value: { } secondValue } => new
                        ResultBox<TwoValues<TValue1, TValue2>>(
                            new TwoValues<TValue1, TValue2>(current.Value, secondValue),
                            null),
                    _ => new ResultBox<TwoValues<TValue1, TValue2>>(default, null)
                },
            _ => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue1)} combine to {nameof(TValue2)}"))
        };
    public static ResultBox<TwoValues<TValue, TValue2>>
        CombineValueWrapTry<TValue, TValue2>(
            this ResultBox<TValue> current,
            Func<TValue2> secondValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: { } error }  => error,
            { Value: not null } => ResultBox<TValue2>.WrapTry(secondValueFunc) switch
            {
                { Exception: { } error }  => error,
                { Value: { } secondValue } => new ResultBox<TwoValues<TValue, TValue2>>(
                    new TwoValues<TValue, TValue2>(current.Value, secondValue),
                    null),
                _ => new ResultBox<TwoValues<TValue, TValue2>>(default, null)
            },
            _ => new ResultBox<TwoValues<TValue, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };
}

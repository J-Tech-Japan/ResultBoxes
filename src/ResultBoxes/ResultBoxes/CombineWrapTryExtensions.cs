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
            { Exception: { } error } => error,
            { Value: not null } => (await ResultBox<TValue2>.WrapTry(secondValueFunc)).Handle(current.Append),
            _ => new ResultValueNullException()
        };
    public static ResultBox<TwoValues<TValue, TValue2>>
        CombineValueWrapTry<TValue, TValue2>(
            this ResultBox<TValue> current,
            Func<TValue2> secondValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: { } error } => error,
            { Value: not null } => ResultBox<TValue2>.WrapTry(secondValueFunc) switch
            {
                { Exception: { } error } => error,
                { Value: { } secondValue } => current.Append(secondValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };
}

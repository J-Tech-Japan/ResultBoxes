namespace SingleResults;

public static class CombineWrapTryTaskExtension
{
    public static async Task<SingleValueResult<TwoValues<TValue1, TValue2>>> CombineValueWrapTry<
        TValue1,
        TValue2>(
        this Task<SingleValueResult<TValue1>> firstValueTask,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: { } firstValue } => await SingleValueResult<TValue2>.WrapTry(
                    secondValueFunc)
                switch
                {
                    { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                        default,
                        e.Exception),
                    { Value: { } secondValue } => new
                        SingleValueResult<TwoValues<TValue1, TValue2>>(
                            new TwoValues<TValue1, TValue2>(firstValue, secondValue),
                            null),
                    _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(default, null)
                },
            _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };
}

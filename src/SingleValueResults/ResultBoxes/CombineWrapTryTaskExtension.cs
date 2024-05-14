namespace ResultBoxes;

public static class CombineWrapTryTaskExtension
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineValueWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: { } firstValue } => await ResultBox<TValue2>.WrapTry(
                    secondValueFunc)
                switch
                {
                    { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                        default,
                        e.Exception),
                    { Value: { } secondValue } => new
                        ResultBox<TwoValues<TValue1, TValue2>>(
                            new TwoValues<TValue1, TValue2>(firstValue, secondValue),
                            null),
                    _ => new ResultBox<TwoValues<TValue1, TValue2>>(default, null)
                },
            _ => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };
}

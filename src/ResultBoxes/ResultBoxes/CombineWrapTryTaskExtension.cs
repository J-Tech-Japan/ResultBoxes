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
            { Exception: { } error } => error,
            { Value: not null } current => await ResultBox<TValue2>.WrapTry(secondValueFunc)
                switch
                {
                    { Exception: { } error } => error,
                    { Value: { } secondValue } => current.Append(secondValue),
                    _ => new ResultValueNullException()
                },
            _ => new ResultValueNullException()
        };
}

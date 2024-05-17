namespace ResultBoxes;

public static class CombineWrapTryTaskExtension
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).HandleResultAsync(
            async current => await (await ResultBox<TValue2>.WrapTry(secondValueFunc)).HandleAsync(
                addingValue => Task.FromResult(current.Append(addingValue))));
}

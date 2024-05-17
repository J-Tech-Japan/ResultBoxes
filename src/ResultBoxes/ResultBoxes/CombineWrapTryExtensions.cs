namespace ResultBoxes;

public static class CombineWrapTryExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await current.HandleResultAsync(
            async first =>
                (await ResultBox<TValue2>.WrapTry(secondValueFunc)).Handle(first.Append));

    public static ResultBox<TwoValues<TValue, TValue2>>
        CombineWrapTry<TValue, TValue2>(
            this ResultBox<TValue> current,
            Func<TValue2> secondValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current.HandleResult(
            c => ResultBox<TValue2>.WrapTry(secondValueFunc).Handle(current.Append));
}

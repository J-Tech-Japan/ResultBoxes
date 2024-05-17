namespace ResultBoxes;

public static class VerifyExtension
{
    public static ResultBox<TValue> Verify<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, ExceptionOrNone> predicate)
        where TValue : notnull
        => result.Remap(
            value => predicate(value) switch
            {
                { Exception: { } error } => ResultBox<TValue>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TValue>> Verify<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, ExceptionOrNone> predicate)
        where TValue : notnull
        => await (await result).RemapAsync(
            value => Task.FromResult(
                predicate(value) switch
                {
                    { Exception: { } error } => ResultBox<TValue>.FromException(error),
                    _ => value
                }));
}

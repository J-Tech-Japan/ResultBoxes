namespace ResultBoxes;

public record UnitValue
{
    public static UnitValue None => new();
}
public record ExceptionOrNone(Exception? Exception = null)
{
    public bool HasException => Exception is not null;
    public static ExceptionOrNone None => new();
    public static ExceptionOrNone FromException(Exception exception) => new(exception);

    public static implicit operator ExceptionOrNone(Exception exception) => new(exception);
    public static implicit operator ExceptionOrNone(UnitValue _) => new();
}
public static class EnsureExtension
{
    public static ResultBox<TValue> Ensure<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, ExceptionOrNone> predicate)
        where TValue : notnull
        => result.Handle(
            value => predicate(value) switch
            {
                { Exception: { } error } => ResultBox<TValue>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TValue>> Ensure<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, ExceptionOrNone> predicate)
        where TValue : notnull
        => await (await result).HandleAsync(
            value => Task.FromResult(
                predicate(value) switch
                {
                    { Exception: { } error } => ResultBox<TValue>.FromException(error),
                    _ => value
                }));
}

namespace ResultBoxes;

public static class UnwrapExtensions
{
    public static TValue UnwrapBox<TValue>(this ResultBox<TValue> result) where TValue : notnull =>
        result.Value ?? throw (result.Exception ?? new ResultsInvalidOperationException());
    public static async Task<TValue> UnwrapBox<TValue>(this Task<ResultBox<TValue>> result)
        where TValue : notnull => await result switch
    {
        { IsSuccess: false } error => throw error.GetException(),
        { IsSuccess: true } value => value.GetValue(),
        _ => throw new ResultsInvalidOperationException()
    };
    public static TValueReturn UnwrapBox<TValue, TValueReturn>(
        this ResultBox<TValue> result,
        Func<TValue, TValueReturn> returnFunc) where TValue : notnull =>
        result switch
        {
            { IsSuccess: false } error => throw error.GetException(),
            { IsSuccess: true } value => returnFunc(value.GetValue()),
            _ => throw new ResultsInvalidOperationException()
        };
    public static async Task<TValueReturn> UnwrapBox<TValue, TValueReturn>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, TValueReturn> returnFunc) where TValue : notnull =>
        await result switch
        {
            { IsSuccess: false } error => throw error.GetException(),
            { IsSuccess: true } value => returnFunc(value.GetValue()),
            _ => throw new ResultsInvalidOperationException()
        };
    public static async Task<TValueReturn> UnwrapBox<TValue, TValueReturn>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task<TValueReturn>> returnFunc) where TValue : notnull =>
        await result switch
        {
            { IsSuccess: false } error => throw error.GetException(),
            { IsSuccess: true } value => await returnFunc(value.GetValue()),
            _ => throw new ResultsInvalidOperationException()
        };
}

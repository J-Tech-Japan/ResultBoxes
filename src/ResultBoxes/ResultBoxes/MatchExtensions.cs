namespace ResultBoxes;

public static class MatchExtensions
{
    public static TUnionResult Match<TValue, TUnionResult>(this ResultBox<TValue> result,
        Func<TValue, TUnionResult> successFunc, Func<Exception, TUnionResult> errorFunc)
        where TValue : notnull
        => result.IsSuccess ? successFunc(result.GetValue()) : errorFunc(result.GetException());

    public static async Task<TUnionResult> Match<TValue, TUnionResult>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, TUnionResult> successFunc,
        Func<Exception, TUnionResult> errorFunc)
        where TValue : notnull
        => (await result).Match(successFunc, errorFunc);
}
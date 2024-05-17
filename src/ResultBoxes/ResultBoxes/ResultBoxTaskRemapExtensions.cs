namespace ResultBoxes;

public static class ResultBoxTaskRemapExtensions
{
    public static async Task<ResultBox<TValueResult>> Remap<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, TValueResult> valueFunc) where TValue : notnull where TValueResult : notnull =>
        (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, ResultBox<TValueResult>> valueFunc)
        where TValue : notnull where TValueResult : notnull =>
        (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> RemapAsync<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, Task<ResultBox<TValueResult>>> valueFunc)
        where TValue : notnull where TValueResult : notnull =>
        await (await task).RemapAsync(valueFunc);
    public static async Task<ResultBox<TValueResult>> RemapResultAsync<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<ResultBox<TValue>, Task<ResultBox<TValueResult>>> valueFunc) where TValue : notnull
        where TValueResult : notnull => await (await task).RemapResultAsync(valueFunc);
    public static async Task<ResultBox<TValueResult>> RemapAsync<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, Task<TValueResult>> valueFunc)
        where TValue : notnull where TValueResult : notnull =>
        await (await task).RemapAsync(valueFunc);
    public static Task<TValue> UnwrapAsync<TValue>(this Task<ResultBox<TValue>> task)
        where TValue : notnull =>
        task.ContinueWith(t => t.Result.UnwrapBox());
}

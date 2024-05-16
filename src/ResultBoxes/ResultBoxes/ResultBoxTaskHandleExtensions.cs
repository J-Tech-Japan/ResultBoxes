namespace ResultBoxes;

public static class ResultBoxTaskHandleExtensions
{
    public static async Task<ResultBox<TValueResult>> Handle<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, TValueResult> valueFunc) where TValue : notnull where TValueResult : notnull =>
        (await task).Handle(valueFunc);

    public static async Task<ResultBox<TValueResult>> Handle<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, ResultBox<TValueResult>> valueFunc)
        where TValue : notnull where TValueResult : notnull =>
        (await task).Handle(valueFunc);

    public static async Task<ResultBox<TValueResult>> HandleAsync<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, Task<ResultBox<TValueResult>>> valueFunc)
        where TValue : notnull where TValueResult : notnull =>
        await (await task).HandleAsync(valueFunc);
    public static async Task<ResultBox<TValueResult>> HandleResultAsync<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<ResultBox<TValue>, Task<ResultBox<TValueResult>>> valueFunc) where TValue : notnull
        where TValueResult : notnull => await (await task).HandleResultAsync(valueFunc);
    public static async Task<ResultBox<TValueResult>> HandleAsync<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, Task<TValueResult>> valueFunc)
        where TValue : notnull where TValueResult : notnull =>
        await (await task).HandleAsync(valueFunc);
    public static Task<TValue> UnwrapAsync<TValue>(this Task<ResultBox<TValue>> task) where TValue : notnull =>
        task.ContinueWith(t => t.Result.UnwrapBox());
}
namespace ResultBoxes;

public static class RemapTaskExtensions
{
    public static async Task<ResultBox<TValueResult>> Remap<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, TValueResult> valueFunc) where TValue : notnull where TValueResult : notnull =>
        (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2, TValueResult>(
        this Task<ResultBox<TwoValues<TValueOriginal1, TValueOriginal2>>> task,
        Func<TValueOriginal1, TValueOriginal2, TValueResult> valueFunc) 
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueResult : notnull =>
        (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueResult>(
        this Task<ResultBox<ThreeValues<TValueOriginal1, TValueOriginal2, TValueOriginal3>>> task,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueResult> valueFunc) 
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueResult : notnull =>
        (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueResult>(
        this Task<ResultBox<FourValues<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4>>> task,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueResult> valueFunc) 
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueResult : notnull =>
        (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5, TValueResult>(
        this Task<ResultBox<FiveValues<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5>>> task,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5, TValueResult> valueFunc) 
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueOriginal5 : notnull
        where TValueResult : notnull =>
        (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValue, TValueResult>(
        this Task<ResultBox<TValue>> task,
        Func<TValue, Task<TValueResult>> valueFunc)
        where TValue : notnull where TValueResult : notnull =>
        await (await task).Remap(valueFunc);
    
    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2, TValueResult>(
        this Task<ResultBox<TwoValues<TValueOriginal1, TValueOriginal2>>> task,
        Func<TValueOriginal1, TValueOriginal2, Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueResult : notnull =>
        await (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueResult>(
        this Task<ResultBox<ThreeValues<TValueOriginal1, TValueOriginal2, TValueOriginal3>>> task,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueResult : notnull =>
        await (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueResult>(
        this Task<ResultBox<FourValues<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4>>> task,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueResult : notnull =>
        await (await task).Remap(valueFunc);

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5, TValueResult>(
        this Task<ResultBox<FiveValues<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5>>> task,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5, Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueOriginal5 : notnull
        where TValueResult : notnull =>
        await (await task).Remap(valueFunc);
}

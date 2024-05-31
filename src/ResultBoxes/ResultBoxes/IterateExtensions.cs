namespace ResultBoxes;

public static class IterateExtensions
{
    public static ResultBox<List<TValue>> ScanEach<TValue>(
        this ResultBox<List<TValue>> current,
        Func<TValue, ScanEachControlFlow> action)
        where TValue : notnull
    {
        if (!current.IsSuccess)
        {
            return current;
        }
        foreach (var value in current.GetValue())
        {
            if (action(value) == ScanEachControlFlow.Break)
            {
                break;
            }
        }
        return current;
    }

    public static ResultBox<List<TValue>> ScanEach<TValue>(
        this ResultBox<List<TValue>> current,
        Action<TValue> action)
        where TValue : notnull
    {
        if (!current.IsSuccess)
        {
            return current;
        }
        foreach (var value in current.GetValue())
        {
            action(value);
        }
        return current;
    }

    public static async Task<ResultBox<List<TValue>>> ScanEach<TValue>(
        this ResultBox<List<TValue>> current,
        Func<TValue, Task<ScanEachControlFlow>> action)
        where TValue : notnull
    {
        if (!current.IsSuccess)
        {
            return current;
        }
        foreach (var value in current.GetValue())
        {
            if (await action(value) == ScanEachControlFlow.Break)
            {
                break;
            }
        }

        return current;
    }

    public static async Task<ResultBox<List<TValue>>> ScanEach<TValue>(
        this ResultBox<List<TValue>> current,
        Func<TValue, Task> action)
        where TValue : notnull
    {
        if (!current.IsSuccess)
        {
            return current;
        }
        foreach (var value in current.GetValue())
        {
            await action(value);
        }
        return current;
    }

    public static async Task<ResultBox<List<TValue>>> ScanEach<TValue>(
        this Task<ResultBox<List<TValue>>> currentTask,
        Func<TValue, Task<ScanEachControlFlow>> action)
        where TValue : notnull
    {
        var current = await currentTask;
        if (!current.IsSuccess)
        {
            return current;
        }
        foreach (var value in current.GetValue())
        {
            if (await action(value) == ScanEachControlFlow.Break)
            {
                break;
            }
        }

        return current;
    }

    public static async Task<ResultBox<List<TValue>>> ScanEach<TValue>(
        this Task<ResultBox<List<TValue>>> currentTask,
        Func<TValue, Task> action)
        where TValue : notnull
    {
        var current = await currentTask;
        if (!current.IsSuccess)
        {
            return current;
        }
        foreach (var value in current.GetValue())
        {
            await action(value);
        }

        return current;
    }

    public static async Task<ResultBox<List<TValue>>> ScanEach<TValue>(
        this Task<ResultBox<List<TValue>>> currentTask,
        Action<TValue> action)
        where TValue : notnull
    {
        var current = await currentTask;
        if (!current.IsSuccess)
        {
            return current;
        }
        foreach (var value in current.GetValue())
        {
            action(value);
        }
        return current;
    }
}

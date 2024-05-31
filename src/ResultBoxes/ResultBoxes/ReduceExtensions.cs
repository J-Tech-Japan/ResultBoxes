namespace ResultBoxes;

public static class ReduceExtensions
{
    public static ResultBox<TValueResult> ReduceEach<TValue, TValueResult>(
        this ResultBox<List<TValue>> current,
        TValueResult initialValue,
        Func<TValue, TValueResult, ResultBox<ReduceResultValue<TValueResult>>> reduceFunc)
        where TValue : notnull
        where TValueResult : notnull
    {
        if (!current.IsSuccess)
        {
            return ResultBox.FromException<TValueResult>(current.GetException());
        }
        var result = ResultBox.FromValue(ReduceResultValue.Start(initialValue));
        foreach (var value in current.GetValue())
        {
            result = reduceFunc(value, result.GetValue().Value);
            if (!result.IsSuccess || result.GetValue().ControlFlow == ReduceControlFlow.Break)
            {
                break;
            }
        }
        return result.IsSuccess ? ResultBox.FromValue(result.GetValue().Value) : result.GetException();
    }

    public static ResultBox<TValueResult> ReduceEach<TValue, TValueResult>(
        this ResultBox<List<TValue>> current,
        TValueResult initialValue,
        Func<TValue, TValueResult, ResultBox<TValueResult>> reduceFunc)
        where TValue : notnull
        where TValueResult : notnull
    {
        if (!current.IsSuccess)
        {
            return ResultBox.FromException<TValueResult>(current.GetException());
        }
        var result = ResultBox.FromValue(initialValue);
        foreach (var value in current.GetValue())
        {
            result = reduceFunc(value, result.GetValue());
            if (!result.IsSuccess)
            {
                break;
            }
        }
        return result.IsSuccess ? ResultBox.FromValue(result.GetValue()) : result.GetException();
    }

    
    
    public static async Task<ResultBox<TValueResult>> ReduceEach<TValue, TValueResult>(
        this Task<ResultBox<List<TValue>>> currentTask,
        TValueResult initialValue,
        Func<TValue, TValueResult, ResultBox<ReduceResultValue<TValueResult>>> reduceFunc)
        where TValue : notnull
        where TValueResult : notnull
    {
        var current = await currentTask;
        if (!current.IsSuccess)
        {
            return ResultBox.FromException<TValueResult>(current.GetException());
        }
        var result = ResultBox.FromValue(ReduceResultValue.Start(initialValue));
        foreach (var value in current.GetValue())
        {
            result = reduceFunc(value, result.GetValue().Value);
            if (!result.IsSuccess || result.GetValue().ControlFlow == ReduceControlFlow.Break)
            {
                break;
            }
        }
        return result.IsSuccess ? ResultBox.FromValue(result.GetValue().Value) : result.GetException();
    }

    public static async Task<ResultBox<TValueResult>> ReduceEach<TValue, TValueResult>(
        this Task<ResultBox<List<TValue>>> currentTask,
        TValueResult initialValue,
        Func<TValue, TValueResult, ResultBox<TValueResult>> reduceFunc)
        where TValue : notnull
        where TValueResult : notnull
    {
        var current = await currentTask;
        if (!current.IsSuccess)
        {
            return ResultBox.FromException<TValueResult>(current.GetException());
        }
        var result = ResultBox.FromValue(initialValue);
        foreach (var value in current.GetValue())
        {
            result = reduceFunc(value, result.GetValue());
            if (!result.IsSuccess)
            {
                break;
            }
        }
        return result.IsSuccess ? ResultBox.FromValue(result.GetValue()) : result.GetException();
    }


    public static async Task<ResultBox<TValueResult>> ReduceEach<TValue, TValueResult>(
        this Task<ResultBox<List<TValue>>> currentTask,
        TValueResult initialValue,
        Func<TValue, TValueResult, Task<ResultBox<ReduceResultValue<TValueResult>>>> reduceFunc)
        where TValue : notnull
        where TValueResult : notnull
    {
        var current = await currentTask;
        if (!current.IsSuccess)
        {
            return ResultBox.FromException<TValueResult>(current.GetException());
        }
        var result = ResultBox.FromValue(ReduceResultValue.Start(initialValue));
        foreach (var value in current.GetValue())
        {
            result = await reduceFunc(value, result.GetValue().Value);
            if (!result.IsSuccess || result.GetValue().ControlFlow == ReduceControlFlow.Break)
            {
                break;
            }
        }
        return result.IsSuccess ? ResultBox.FromValue(result.GetValue().Value) : result.GetException();
    }

    public static async Task<ResultBox<TValueResult>> ReduceEach<TValue, TValueResult>(
        this Task<ResultBox<List<TValue>>> currentTask,
        TValueResult initialValue,
        Func<TValue, TValueResult, Task<ResultBox<TValueResult>>> reduceFunc)
        where TValue : notnull
        where TValueResult : notnull
    {
        var current = await currentTask;
        if (!current.IsSuccess)
        {
            return ResultBox.FromException<TValueResult>(current.GetException());
        }
        var result = ResultBox.FromValue(initialValue);
        foreach (var value in current.GetValue())
        {
            result = await reduceFunc(value, result.GetValue());
            if (!result.IsSuccess)
            {
                break;
            }
        }
        return result.IsSuccess ? ResultBox.FromValue(result.GetValue()) : result.GetException();
    }

    public static async Task<ResultBox<TValueResult>> ReduceEach<TValue, TValueResult>(
        this ResultBox<List<TValue>> current,
        TValueResult initialValue,
        Func<TValue, TValueResult, Task<ResultBox<ReduceResultValue<TValueResult>>>> reduceFunc)
        where TValue : notnull
        where TValueResult : notnull
    {
        if (!current.IsSuccess)
        {
            return ResultBox.FromException<TValueResult>(current.GetException());
        }
        var result = ResultBox.FromValue(ReduceResultValue.Start(initialValue));
        foreach (var value in current.GetValue())
        {
            result = await reduceFunc(value, result.GetValue().Value);
            if (!result.IsSuccess || result.GetValue().ControlFlow == ReduceControlFlow.Break)
            {
                break;
            }
        }
        return result.IsSuccess ? ResultBox.FromValue(result.GetValue().Value) : result.GetException();
    }

    public static async Task<ResultBox<TValueResult>> ReduceEach<TValue, TValueResult>(
        this ResultBox<List<TValue>> current,
        TValueResult initialValue,
        Func<TValue, TValueResult, Task<ResultBox<TValueResult>>> reduceFunc)
        where TValue : notnull
        where TValueResult : notnull
    {
        if (!current.IsSuccess)
        {
            return ResultBox.FromException<TValueResult>(current.GetException());
        }
        var result = ResultBox.FromValue(initialValue);
        foreach (var value in current.GetValue())
        {
            result = await reduceFunc(value, result.GetValue());
            if (!result.IsSuccess)
            {
                break;
            }
        }
        return result.IsSuccess ? ResultBox.FromValue(result.GetValue()) : result.GetException();
    }
}
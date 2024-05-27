namespace ResultBoxes;

public static class ScanExtensions
{
    public static ResultBox<TValue> Scan<TValue>(
        this ResultBox<TValue> result,
        Action<TValue> action,
        Action<Exception>? actionError = null)
        where TValue : notnull
    {
        switch (result)
        {
            case { IsSuccess: false }:
                actionError?.Invoke(result.GetException());
                break;
            case { IsSuccess: true }:
                action(result.GetValue());
                break;
        }
        return result;
    }
    public static ResultBox<TValue> ScanResult<TValue>(
        this ResultBox<TValue> result,
        Action<ResultBox<TValue>> action)
        where TValue : notnull
    {
        action(result);
        return result;
    }
    public static ResultBox<TwoValues<TValue1, TValue2>> Scan<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Action<TValue1, TValue2> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => Scan(result, values => values.CallAction(action), actionError);

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Scan<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Action<TValue1, TValue2, TValue3> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => Scan(result, values => values.CallAction(action), actionError);

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> Scan<TValue1, TValue2,
        TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Action<TValue1, TValue2, TValue3, TValue4> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => Scan(result, values => values.CallAction(action), actionError);
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> Scan<TValue1,
        TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Action<TValue1, TValue2, TValue3, TValue4, TValue5> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => Scan(result, values => values.CallAction(action), actionError);

    public static async Task<ResultBox<TValue>> Scan<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, Task> actionAsync,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue : notnull
    {
        switch (result)
        {
            case { IsSuccess: false }:
                if (actionErrorAsync is not null)
                {
                    await actionErrorAsync(result.GetException());
                }
                break;
            case { IsSuccess: true }:
                await actionAsync(result.GetValue());
                break;
        }
        return result;
    }
    public static async Task<ResultBox<TValue>> ScanResult<TValue>(
        this ResultBox<TValue> result,
        Func<ResultBox<TValue>, Task> actionAsync)
        where TValue : notnull
    {
        await actionAsync(result);
        return result;
    }
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, Task> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await Scan(result, async values => await values.CallAction(action), actionErrorAsync);

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Scan<TValue1,
        TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, Task> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await Scan(result, async values => await values.CallAction(action), actionErrorAsync);

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Scan<
        TValue1,
        TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await Scan(result, async values => await values.CallAction(action), actionErrorAsync);
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task> action,
            Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await Scan(result, async values => await values.CallAction(action), actionErrorAsync);

    public static async Task<ResultBox<TValue>> Scan<TValue>(
        this Task<ResultBox<TValue>> result,
        Action<TValue> action,
        Action<Exception>? actionError = null)
        where TValue : notnull
    {
        var res = await result;
        switch (res)
        {
            case { IsSuccess: false }:
                actionError?.Invoke(res.GetException());
                break;
            case { IsSuccess: true }:
                action(res.GetValue());
                break;
        }
        return res;
    }
    public static async Task<ResultBox<TValue>> ScanResult<TValue>(
        this Task<ResultBox<TValue>> result,
        Action<ResultBox<TValue>> action)
        where TValue : notnull
    {
        var res = await result;
        action(res);
        return res;
    }

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Action<TValue1, TValue2> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await result).Scan(values => values.CallAction(action), actionError);
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Scan<TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Action<TValue1, TValue2, TValue3> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => (await result).Scan(values => values.CallAction(action), actionError);

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Scan<
        TValue1,
        TValue2, TValue3, TValue4>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Action<TValue1, TValue2, TValue3, TValue4> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => (await result).Scan(values => values.CallAction(action), actionError);
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
            Action<TValue1, TValue2, TValue3, TValue4, TValue5> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await result).Scan(values => values.CallAction(action));

    public static async Task<ResultBox<TValue>> Scan<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task> actionAsync,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue : notnull
    {
        var res = await result;
        switch (res)
        {
            case { IsSuccess: false }:
                if (actionErrorAsync is not null)
                {
                    await actionErrorAsync(res.GetException());
                }
                break;
            case { IsSuccess: true }:
                await actionAsync(res.GetValue());
                break;
        }
        return res;
    }
    public static async Task<ResultBox<TValue>> ScanResult<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<ResultBox<TValue>, Task> actionAsync)
        where TValue : notnull
    {
        var res = await result;
        await actionAsync(res);
        return res;
    }
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, Task> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await result).Scan(
            async values => await values.CallAction(action),
            actionErrorAsync);
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Scan<TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Func<TValue1, TValue2, TValue3, Task> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await result).Scan(
            async values => await values.CallAction(action),
            actionErrorAsync);
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Scan<
        TValue1,
        TValue2, TValue3, TValue4>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await result).Scan(
            async values => await values.CallAction(action),
            actionErrorAsync);
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task> action,
            Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await result).Scan(
            async values => await values.CallAction(action),
            actionErrorAsync);
}

namespace ResultBoxes;

public static class ScanExtensions
{
    #region Scan Nothing

    public static ResultBox<TValue> Scan<TValue>(
        this ResultBox<TValue> result,
        Action action)
        where TValue : notnull
    {
        action();
        return result;
    }

    public static async Task<ResultBox<TValue>> Scan<TValue>(
        this ResultBox<TValue> result,
        Func<Task> actionAsync)
        where TValue : notnull
    {
        await actionAsync();
        return result;
    }

    public static ResultBox<TValue> Scan<TValue, TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<TValueToIgnore> actionAsync)
        where TValue : notnull
    {
        actionAsync();
        return result;
    }

    public static async Task<ResultBox<TValue>> Scan<TValue, TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<Task<TValueToIgnore>> actionAsync)
        where TValue : notnull
    {
        await actionAsync();
        return result;
    }

    public static async Task<ResultBox<TValue>> Scan<TValue>(
        this Task<ResultBox<TValue>> resultTask,
        Action action)
        where TValue : notnull => (await resultTask).Scan(action);

    public static async Task<ResultBox<TValue>> Scan<TValue>(
        this Task<ResultBox<TValue>> resultTask,
        Func<Task> actionAsync)
        where TValue : notnull => await (await resultTask).Scan(actionAsync);

    public static async Task<ResultBox<TValue>> Scan<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> resultTask,
        Func<TValueToIgnore> actionAsync)
        where TValue : notnull => (await resultTask).Scan(actionAsync);

    public static async Task<ResultBox<TValue>> Scan<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> resultTask,
        Func<Task<TValueToIgnore>> actionAsync)
        where TValue : notnull => await (await resultTask).Scan(actionAsync);

    #endregion


    #region Scan Result

    public static ResultBox<TValue> ScanResult<TValue>(
        this ResultBox<TValue> result,
        Action<ResultBox<TValue>> action)
        where TValue : notnull
    {
        action(result);
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

    public static ResultBox<TValue> ScanResult<TValue, TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<ResultBox<TValue>, TValueToIgnore> actionAsync)
        where TValue : notnull
    {
        actionAsync(result);
        return result;
    }

    public static async Task<ResultBox<TValue>> ScanResult<TValue, TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<ResultBox<TValue>, Task<TValueToIgnore>> actionAsync)
        where TValue : notnull
    {
        await actionAsync(result);
        return result;
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

    public static async Task<ResultBox<TValue>> ScanResult<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<ResultBox<TValue>, Task> actionAsync)
        where TValue : notnull
    {
        var res = await result;
        await actionAsync(res);
        return res;
    }

    public static async Task<ResultBox<TValue>> ScanResult<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<ResultBox<TValue>, TValueToIgnore> actionAsync)
        where TValue : notnull
    {
        var res = await result;
        actionAsync(res);
        return res;
    }

    public static async Task<ResultBox<TValue>> ScanResult<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<ResultBox<TValue>, Task<TValueToIgnore>> actionAsync)
        where TValue : notnull
    {
        var res = await result;
        await actionAsync(res);
        return res;
    }

    #endregion

    #region Scan Value and Error

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

    public static async Task<ResultBox<TValue>> Scan<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, Task> actionAsync,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue : notnull
    {
        switch (result)
        {
            case { IsSuccess: false }:
                if (actionErrorAsync is not null) await actionErrorAsync(result.GetException());
                break;
            case { IsSuccess: true }:
                await actionAsync(result.GetValue());
                break;
        }

        return result;
    }

    public static ResultBox<TValue> Scan<TValue, TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<TValue, TValueToIgnore> actionAsync,
        Action<Exception>? actionError = null)
        where TValue : notnull
    {
        switch (result)
        {
            case { IsSuccess: false }:
                if (actionError is not null) actionError(result.GetException());
                break;
            case { IsSuccess: true }:
                actionAsync(result.GetValue());
                break;
        }

        return result;
    }

    public static async Task<ResultBox<TValue>> Scan<TValue, TValueToVoid>(
        this ResultBox<TValue> result,
        Func<TValue, Task<TValueToVoid>> actionAsync,
        Action<Exception>? actionError = null)
        where TValue : notnull
    {
        switch (result)
        {
            case { IsSuccess: false }:
                if (actionError is not null) actionError(result.GetException());
                break;
            case { IsSuccess: true }:
                await actionAsync(result.GetValue());
                break;
        }

        return result;
    }

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

    public static async Task<ResultBox<TValue>> Scan<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task> actionAsync,
        Action<Exception>? actionError = null)
        where TValue : notnull
    {
        var res = await result;
        switch (res)
        {
            case { IsSuccess: false }:
                if (actionError is not null) actionError(res.GetException());
                break;
            case { IsSuccess: true }:
                await actionAsync(res.GetValue());
                break;
        }

        return res;
    }

    public static async Task<ResultBox<TValue>> Scan<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, TValueToIgnore> actionAsync,
        Action<Exception>? actionError = null)
        where TValue : notnull
    {
        var res = await result;
        switch (res)
        {
            case { IsSuccess: false }:
                if (actionError is not null) actionError(res.GetException());
                break;
            case { IsSuccess: true }:
                actionAsync(res.GetValue());
                break;
        }

        return res;
    }

    public static async Task<ResultBox<TValue>> Scan<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task<TValueToIgnore>> actionAsync,
        Action<Exception>? actionError = null)
        where TValue : notnull
    {
        var res = await result;
        switch (res)
        {
            case { IsSuccess: false }:
                if (actionError is not null) actionError(res.GetException());
                break;
            case { IsSuccess: true }:
                await actionAsync(res.GetValue());
                break;
        }

        return res;
    }

    #endregion


    #region Scan TwoValues and Error

    public static ResultBox<TwoValues<TValue1, TValue2>> Scan<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Action<TValue1, TValue2> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => Scan(result, values => values.CallAction(action), actionError);

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, Task> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
    {
        switch (result)
        {
            case { IsSuccess: false }:
                if (actionError is not null) actionError(result.GetException());
                break;
            case { IsSuccess: true }:
                await result.GetValue().CallAction(action);
                break;
        }

        return result;
    }

    public static ResultBox<TwoValues<TValue1, TValue2>> Scan<TValue1, TValue2, TValueIgnore>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, TValueIgnore> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
    {
        switch (result)
        {
            case { IsSuccess: false }:
                if (actionError is not null) actionError(result.GetException());
                break;
            case { IsSuccess: true }:
                result.GetValue().CallAction((value1, value2) => { action(value1, value2); });
                break;
        }

        return result;
    }

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2, TValueToIgnore>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, Task<TValueToIgnore>> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
    {
        switch (result)
        {
            case { IsSuccess: false }:
                if (actionError is not null) actionError(result.GetException());
                break;
            case { IsSuccess: true }:
                await result.GetValue().CallAction(action);
                break;
        }

        return result;
    }

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Action<TValue1, TValue2> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await result).Scan(values => values.CallAction(action), actionError);

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, Task> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await result).Scan(
            async values => await values.CallAction(action),
            actionErrorAsync);

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2, TValueToIgnore>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, TValueToIgnore> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await result).Scan(
            async values =>
            {
                await Task.CompletedTask;
                values.CallAction(
                    (value1, value2) => { action(value1, value2); });
            },
            actionErrorAsync);

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Scan<TValue1, TValue2, TValueToIgnore>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, Task<TValueToIgnore>> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await result).Scan(
            async values => await values.CallAction(action),
            actionErrorAsync);

    #endregion

    #region Scan TheeValues and Error

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Scan<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Action<TValue1, TValue2, TValue3> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => Scan(result, values => values.CallAction(action), actionError);

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Scan<TValue1,
        TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, Task> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await Scan(result, async values => await values.CallAction(action), actionErrorAsync);

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Scan<TValue1, TValue2, TValue3, TValueToIgnore>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, TValueToIgnore> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => Scan(result, values => values.CallAction(
            (value1, value2, value3) => { action(value1, value2, value3); }), actionError);

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Scan<TValue1,
        TValue2, TValue3, TValueToIgnore>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, Task<TValueToIgnore>> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await Scan(result, async values => await values.CallAction(action), actionErrorAsync);

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Scan<TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Action<TValue1, TValue2, TValue3> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => (await result).Scan(values => values.CallAction(action), actionError);

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


    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Scan<TValue1,
        TValue2, TValue3, TValueToIgnore>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Func<TValue1, TValue2, TValue3, TValueToIgnore> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => (await result).Scan(values => values.CallAction((v1, v2, v3) => { action(v1, v2, v3); }), actionError);

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Scan<TValue1,
        TValue2, TValue3, TValueToIgnore>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Func<TValue1, TValue2, TValue3, Task<TValueToIgnore>> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await result).Scan(
            async values => await values.CallAction(action),
            actionErrorAsync);

    #endregion

    #region Scan FourValues and Error

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


    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> Scan<TValue1, TValue2,
        TValue3, TValue4, TValueToIgnore>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValueToIgnore> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => Scan(result, values => values.CallAction((v1, v2, v3, v4) => { action(v1, v2, v3, v4); }), actionError);

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Scan<
        TValue1,
        TValue2, TValue3, TValue4, TValueToIgnore>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task<TValueToIgnore>> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await Scan(result, async values => await values.CallAction(action), actionErrorAsync);


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


    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Scan<
        TValue1,
        TValue2, TValue3, TValue4, TValueToIgnore>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValueToIgnore> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => (await result).Scan(values => values.CallAction((v1, v2, v3, v4) => { action(v1, v2, v3, v4); }),
            actionError);

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Scan<
        TValue1,
        TValue2, TValue3, TValue4, TValueToIgnore>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task<TValueToIgnore>> action,
        Func<Exception, Task>? actionErrorAsync = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await result).Scan(
            async values => await values.CallAction(action),
            actionErrorAsync);

    #endregion

    #region Scan FiveValues and Error

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

    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task> action,
            Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => Scan(result, async values => await values.CallAction(action), actionError);

    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> Scan<TValue1,
        TValue2, TValue3, TValue4, TValue5, TValueToIgnore>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValueToIgnore> action,
        Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => Scan(result, values => values.CallAction((v1, v2, v3, v4, v5) => { action(v1, v2, v3, v4, v5); }),
            actionError);

    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5, TValueToIgnore>(
            this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<TValueToIgnore>> action,
            Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => Scan(result, async values => await values.CallAction(action), actionError);


    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
            Action<TValue1, TValue2, TValue3, TValue4, TValue5> action, Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await result).Scan(values => values.CallAction(action), actionError);

    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task> action,
            Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
    {
        var res = await result;

        switch (res)
        {
            case { IsSuccess: false }:
                actionError?.Invoke(res.GetException());
                break;
            case { IsSuccess: true }:
                await res.GetValue().CallAction(action);
                break;
        }

        return res;
    }


    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5, TValueToIgnore>(
            this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValueToIgnore> action,
            Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await result).Scan(values => values.CallAction((v1, v2, v3, v4, v5) => { action(v1, v2, v3, v4, v5); }),
            actionError);

    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Scan<TValue1, TValue2, TValue3, TValue4, TValue5, TValueToIgnore>(
            this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<TValueToIgnore>> action,
            Action<Exception>? actionError = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
    {
        var res = await result;

        switch (res)
        {
            case { IsSuccess: false }:
                actionError?.Invoke(res.GetException());
                break;
            case { IsSuccess: true }:
                await res.GetValue().CallAction(action);
                break;
        }

        return res;
    }

    #endregion
}
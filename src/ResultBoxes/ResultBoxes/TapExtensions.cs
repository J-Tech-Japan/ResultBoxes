namespace ResultBoxes;

public static class TapExtensions
{
    public static ResultBox<TValue> Tap<TValue>(
        this ResultBox<TValue> result,
        Action<TValue> action)
        where TValue : notnull
    {
        if (result is { Value: { } value })
        {
            action(value);
        }
        return result;
    }
    public static ResultBox<TwoValues<TValue1, TValue2>> Tap<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Action<TValue1, TValue2> action)
        where TValue1 : notnull
        where TValue2 : notnull
        => Tap(result, values => values.CallAction(action));
    
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Tap<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Action<TValue1, TValue2, TValue3> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => Tap(result, values => values.CallAction(action));

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> Tap<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Action<TValue1, TValue2, TValue3, TValue4> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => Tap(result, values => values.CallAction(action));
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> Tap<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Action<TValue1, TValue2, TValue3, TValue4, TValue5> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => Tap(result, values => values.CallAction(action));

    public static async Task<ResultBox<TValue>> Tap<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, Task> actionAsync)
        where TValue : notnull
    {
        if (result is { Value: { } value })
        {
            await actionAsync(value);
        }
        return result;
    }
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Tap<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull
        => await Tap(result, async values => await values.CallAction(action));

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2,TValue3>>> Tap<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2,TValue3>> result,
        Func<TValue1, TValue2,TValue3, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await Tap(result, async values => await values.CallAction(action));

    public static async Task<ResultBox<FourValues<TValue1, TValue2,TValue3, TValue4>>> Tap<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2,TValue3, TValue4>> result,
        Func<TValue1, TValue2,TValue3, TValue4, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await Tap(result, async values => await values.CallAction(action));
    public static async Task<ResultBox<FiveValues<TValue1, TValue2,TValue3, TValue4, TValue5>>> Tap<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2,TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2,TValue3, TValue4, TValue5, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await Tap(result, async values => await values.CallAction(action));

    public static async Task<ResultBox<TValue>> Tap<TValue>(
        this Task<ResultBox<TValue>> result,
        Action<TValue> action)
        where TValue : notnull
    {
        var res = await result;
        if (res is { Value: { } value })
        {
            action(value);
        }
        return res;
    }
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Tap<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Action<TValue1, TValue2> action)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await result).Tap(values => values.CallAction(action));
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Tap<TValue1, TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Action<TValue1, TValue2, TValue3> action)
        where TValue1 : notnull
        where TValue2 : notnull
    where TValue3 : notnull
        => (await result).Tap(values => values.CallAction(action));

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Tap<TValue1, TValue2, TValue3, TValue4>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Action<TValue1, TValue2, TValue3, TValue4> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => (await result).Tap(values => values.CallAction(action));
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Tap<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Action<TValue1, TValue2, TValue3, TValue4, TValue5> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await result).Tap(values => values.CallAction(action));

    public static async Task<ResultBox<TValue>> Tap<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task> actionAsync)
        where TValue : notnull
    {
        var res = await result;
        if (res is { Value: { } value })
        {
            await actionAsync(value);
        }
        return res;
    }
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Tap<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await result).Tap(async values => await values.CallAction(action));
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Tap<TValue1, TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Func<TValue1, TValue2, TValue3, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await result).Tap(async values => await values.CallAction(action));
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Tap<TValue1, TValue2, TValue3, TValue4>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await result).Tap(async values => await values.CallAction(action));
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Tap<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await result).Tap(async values => await values.CallAction(action));
}

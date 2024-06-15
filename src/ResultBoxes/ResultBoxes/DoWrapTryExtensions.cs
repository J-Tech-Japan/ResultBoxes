namespace ResultBoxes;

public static class DoWrapTryExtensions 
{
    #region DoWrapTry Nothing 
    public static ResultBox<TValue> DoWrapTry<TValue>(
        this ResultBox<TValue> result,
        Action action)
        where TValue : notnull =>
        result.ConveyorWrapTry(value =>
        {
            action();
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue>(
        this ResultBox<TValue> result,
        Func<Task> actionAsync)
        where TValue : notnull =>
        await result.ConveyorWrapTry(async value =>
        {
            await actionAsync();
            return value;
        });
    public static ResultBox<TValue> DoWrapTry<TValue, TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<TValueToIgnore> action)
        where TValue : notnull =>
        result.ConveyorWrapTry( value =>
        {
            action();
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue,TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<Task<TValueToIgnore>> actionAsync)
        where TValue : notnull =>
        await result.ConveyorWrapTry(async value =>
        {
            await actionAsync();
            return value;
        });

    public static async Task< ResultBox<TValue>> DoWrapTry<TValue>(
        this Task<ResultBox<TValue>> result,
        Action action)
        where TValue : notnull =>
        (await result).ConveyorWrapTry(value =>
        {
            action();
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<Task> actionAsync)
        where TValue : notnull =>
        await (await result).ConveyorWrapTry(async value =>
        {
            await actionAsync();
            return value;
        });
    public static async Task< ResultBox<TValue>> DoWrapTry<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<TValueToIgnore> actionAsync)
        where TValue : notnull=>
        (await result).ConveyorWrapTry(value =>
        {
            actionAsync();
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue,TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<Task<TValueToIgnore>> actionAsync)
        where TValue : notnull =>
        await (await result).ConveyorWrapTry(async value =>
        {
            await actionAsync();
            return value;
        });
    #endregion

    #region Do WrapTry Result
    public static ResultBox<TValue> DoResultWrapTry<TValue>(
        this ResultBox<TValue> result,
        Action<ResultBox<TValue>> action)
        where TValue : notnull =>
        result.ConveyorResult(value =>
            ResultBox.WrapTry(() =>
                {
                    action(value);
                    return UnitValue.Unit;
                }) switch
                {
                    {IsSuccess: true} => value,
                    {IsSuccess: false} failed => ResultBox.FromException<TValue>(failed.GetException())
                });
    public static async Task<ResultBox<TValue>> DoResultWrapTry<TValue>(
        this ResultBox<TValue> result,
        Func<ResultBox<TValue>, Task> actionAsync)
        where TValue : notnull  =>
        await result.ConveyorResult(async value =>
            (await ResultBox.WrapTry(async () =>
                {
                    await actionAsync(value);
                    return UnitValue.Unit;
                })) switch
                {
                    {IsSuccess: true} => value,
                    {IsSuccess: false} failed => ResultBox.FromException<TValue>(failed.GetException())
                });
    public static ResultBox<TValue> DoResultWrapTry<TValue, TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<ResultBox<TValue>, TValueToIgnore> action)
        where TValue : notnull =>
        result.ConveyorResult(value =>
            ResultBox.WrapTry(() =>
                {
                    action(value);
                    return UnitValue.Unit;
                }) switch
                {
                    {IsSuccess: true} => value,
                    {IsSuccess: false} failed => ResultBox.FromException<TValue>(failed.GetException())
                });
    public static async Task<ResultBox<TValue>> DoResultWrapTry<TValue,TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<ResultBox<TValue>, Task<TValueToIgnore>> actionAsync)
        where TValue : notnull =>
        await result.ConveyorResult(async value =>
            (await ResultBox.WrapTry(async () =>
                {
                    await actionAsync(value);
                    return UnitValue.Unit;
                })) switch
                {
                    {IsSuccess: true} => value,
                    {IsSuccess: false} failed => ResultBox.FromException<TValue>(failed.GetException())
                });

    public static async Task<ResultBox<TValue>> DoResultWrapTry<TValue>(
        this Task<ResultBox<TValue>> result,
        Action<ResultBox<TValue>> action)
        where TValue : notnull =>
        await result.ConveyorResult(value =>
            ResultBox.WrapTry(() =>
                {
                    action(value);
                    return UnitValue.Unit;
                }) switch
                {
                    {IsSuccess: true} => value,
                    {IsSuccess: false} failed => ResultBox.FromException<TValue>(failed.GetException())
                });
    public static async Task<ResultBox<TValue>> DoResultWrapTry<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<ResultBox<TValue>, Task> actionAsync)
        where TValue : notnull=>
        await result.ConveyorResult(async value =>
            (await ResultBox.WrapTry(async () =>
                {
                    await actionAsync(value);
                    return UnitValue.Unit;
                })) switch
                {
                    {IsSuccess: true} => value,
                    {IsSuccess: false} failed => ResultBox.FromException<TValue>(failed.GetException())
                });
    public static async Task<ResultBox<TValue>> DoResultWrapTry<TValue,TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<ResultBox<TValue>, TValueToIgnore> action)
        where TValue : notnull=>
        await result.ConveyorResult(value =>
            ResultBox.WrapTry(() =>
                {
                    action(value);
                    return UnitValue.Unit;
                }) switch
                {
                    {IsSuccess: true} => value,
                    {IsSuccess: false} failed => ResultBox.FromException<TValue>(failed.GetException())
                });
    public static async Task<ResultBox<TValue>> DoResultWrapTry<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<ResultBox<TValue>, Task<TValueToIgnore>> actionAsync)
        where TValue : notnull=>
        await result.ConveyorResult(async value =>
            (await ResultBox.WrapTry(async () =>
                {
                    await actionAsync(value);
                    return UnitValue.Unit;
                })) switch
                {
                    {IsSuccess: true} => value,
                    {IsSuccess: false} failed => ResultBox.FromException<TValue>(failed.GetException())
                });
    #endregion
    
    #region DoWrapTry from Value
    public static ResultBox<TValue> DoWrapTry<TValue>(
        this ResultBox<TValue> result,
        Action<TValue> action)
        where TValue : notnull =>
        result.ConveyorWrapTry(value =>
        {
            action(value);
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, Task> actionAsync)
        where TValue : notnull =>
        await result.ConveyorWrapTry(async value =>
        {
            await actionAsync(value);
            return value;
        });
    public static ResultBox<TValue> DoWrapTry<TValue, TValueToIgnore>(
        this ResultBox<TValue> result,
        Func<TValue, TValueToIgnore> action)
        where TValue : notnull =>
        result.ConveyorWrapTry(value =>
        {
            action(value);
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue, TValueToVoid>(
        this ResultBox<TValue> result,
        Func<TValue, Task<TValueToVoid>> actionAsync)
        where TValue : notnull =>
        await result.ConveyorWrapTry(async value =>
        {
            await actionAsync(value);
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue>(
        this Task<ResultBox<TValue>> result,
        Action<TValue> action)
        where TValue : notnull =>
        await result.ConveyorWrapTry(value =>
        {
            action(value);
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task> actionAsync)
        where TValue : notnull =>
        await result.ConveyorWrapTry(async value =>
        {
            await actionAsync(value);
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, TValueToIgnore> action)
        where TValue : notnull=>
        await result.ConveyorWrapTry(value =>
        {
            action(value);
            return value;
        });
    public static async Task<ResultBox<TValue>> DoWrapTry<TValue, TValueToIgnore>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task<TValueToIgnore>> actionAsync)
        where TValue : notnull=>
        await result.ConveyorWrapTry(async value =>
        {
            await actionAsync(value);
            return value;
        });
    #endregion
    
    #region DoWrapTry TwoValues
    public static ResultBox<TwoValues<TValue1, TValue2>> DoWrapTry<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Action<TValue1, TValue2> action)
        where TValue1 : notnull
        where TValue2 : notnull =>
        result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2);
            return values;
        });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> DoWrapTry<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2);
            return values;
        });
    
    public static ResultBox<TwoValues<TValue1, TValue2>> DoWrapTry<TValue1, TValue2, TValueIgnore>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, TValueIgnore> action)
        where TValue1 : notnull
        where TValue2 : notnull =>
        result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2);
            return values;
        });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> DoWrapTry<TValue1, TValue2, TValueToIgnore>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, Task<TValueToIgnore>> action)
        where TValue1 : notnull
        where TValue2 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2);
            return values;
        });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> DoWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Action<TValue1, TValue2> action)
        where TValue1 : notnull
        where TValue2 : notnull =>
        await result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2);
            return values;
        });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Do<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2);
            return values;
        });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> DoWrapTry<TValue1, TValue2, TValueToIgnore>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, TValueToIgnore> action)
        where TValue1 : notnull
        where TValue2 : notnull =>
        await result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2);
            return values;
        });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> DoWrapTry<TValue1, TValue2,TValueToIgnore>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, Task<TValueToIgnore>> action)
        where TValue1 : notnull
        where TValue2 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2);
            return values;
        });
    #endregion

    #region DoWrapTry ThreeValues
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> DoWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Action<TValue1, TValue2, TValue3> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull =>
        result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3);
            return values;
        });
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> DoWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2,TValue3, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3);
            return values;
        });
    
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> DoWrapTry<TValue1, TValue2,TValue3, TValueIgnore>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, TValueIgnore> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull =>
        result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3);
            return values;
        });
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> DoWrapTry<TValue1, TValue2,TValue3, TValueToIgnore>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, Task<TValueToIgnore>> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3);
            return values;
        });
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> DoWrapTry<TValue1, TValue2,TValue3>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Action<TValue1, TValue2, TValue3> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull =>
        await result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3);
            return values;
        });
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Do<TValue1, TValue2,TValue3>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Func<TValue1, TValue2, TValue3, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3);
            return values;
        });
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> DoWrapTry<TValue1, TValue2,TValue3, TValueToIgnore>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Func<TValue1, TValue2, TValue3, TValueToIgnore> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull =>
        await result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3);
            return values;
        });
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> DoWrapTry<TValue1, TValue2,TValue3,TValueToIgnore>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> result,
        Func<TValue1, TValue2, TValue3, Task<TValueToIgnore>> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3);
            return values;
        });
    #endregion

    #region DoWrapTry FourValues
    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> DoWrapTry<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Action<TValue1, TValue2, TValue3, TValue4> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull =>
        result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3, values.Value4);
            return values;
        });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> DoWrapTry<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2,TValue3, TValue4, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3, values.Value4);
            return values;
        });
    
    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> DoWrapTry<TValue1, TValue2,TValue3, TValue4, TValueIgnore>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValueIgnore> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull =>
        result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3, values.Value4);
            return values;
        });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> DoWrapTry<TValue1, TValue2,TValue3, TValue4, TValueToIgnore>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task<TValueToIgnore>> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3, values.Value4);
            return values;
        });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> DoWrapTry<TValue1, TValue2,TValue3, TValue4>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Action<TValue1, TValue2, TValue3, TValue4> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull =>
        await result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3, values.Value4);
            return values;
        });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Do<TValue1, TValue2,TValue3, TValue4>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3, values.Value4);
            return values;
        });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> DoWrapTry<TValue1, TValue2,TValue3, TValue4, TValueToIgnore>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValueToIgnore> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull =>
        await result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3, values.Value4);
            return values;
        });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> DoWrapTry<TValue1, TValue2,TValue3, TValue4,TValueToIgnore>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task<TValueToIgnore>> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3, values.Value4);
            return values;
        });
    #endregion

    #region DoWrapTry FiveValues
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> DoWrapTry<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Action<TValue1, TValue2, TValue3, TValue4, TValue5> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull 
        where TValue5 : notnull =>
        result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3, values.Value4, values.Value5);
            return values;
        });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> DoWrapTry<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2,TValue3, TValue4, TValue5, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull 
        where TValue5 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3, values.Value4, values.Value5);
            return values;
        });
    
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> DoWrapTry<TValue1, TValue2,TValue3, TValue4, TValue5, TValueIgnore>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValueIgnore> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull 
        where TValue5 : notnull =>
        result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3, values.Value4, values.Value5);
            return values;
        });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> DoWrapTry<TValue1, TValue2,TValue3, TValue4, TValue5, TValueToIgnore>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<TValueToIgnore>> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull 
        where TValue5 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3, values.Value4, values.Value5);
            return values;
        });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> DoWrapTry<TValue1, TValue2,TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Action<TValue1, TValue2, TValue3, TValue4, TValue5> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull 
        where TValue5 : notnull =>
        await result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3, values.Value4, values.Value5);
            return values;
        });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Do<TValue1, TValue2,TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull 
        where TValue5 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3, values.Value4, values.Value5);
            return values;
        });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> DoWrapTry<TValue1, TValue2,TValue3, TValue4, TValue5, TValueToIgnore>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValueToIgnore> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull 
        where TValue5 : notnull =>
        await result.ConveyorWrapTry(values =>
        {
            action(values.Value1,values.Value2, values.Value3, values.Value4, values.Value5);
            return values;
        });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> DoWrapTry<TValue1, TValue2,TValue3, TValue4, TValue5,TValueToIgnore>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<TValueToIgnore>> action)
        where TValue1 : notnull
        where TValue2 : notnull 
        where TValue3 : notnull
        where TValue4: notnull 
        where TValue5 : notnull =>
        await result.ConveyorWrapTry(async values =>
        {
            await action(values.Value1,values.Value2, values.Value3, values.Value4, values.Value5);
            return values;
        });
    #endregion

}
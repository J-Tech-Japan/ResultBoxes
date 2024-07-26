namespace ResultBoxes;

public static class ConveyorExtensions
{
    #region FiveValues

    public static ResultBox<TValueResult> Conveyor<TValue1, TValue2, TValue3, TValue4, TValue5,
        TValueResult>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> current,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, ResultBox<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
    {
        return current.Conveyor(value => value.Call(handleValueFunc));
    }

    public static async Task<ResultBox<TValueResult>> Conveyor<TValue1, TValue2, TValue3, TValue4,
        TValue5,
        TValueResult>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> current,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<ResultBox<TValueResult>>>
            handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
    {
        return await current.Conveyor(async value => await value.Call(handleValueFunc));
    }

    #endregion

    #region FourValues

    public static ResultBox<TValueResult>
        Conveyor<TValue1, TValue2, TValue3, TValue4, TValueResult>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
            Func<TValue1, TValue2, TValue3, TValue4, ResultBox<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
    {
        return current.Conveyor(value => value.Call(handleValueFunc));
    }

    public static async Task<ResultBox<TValueResult>>
        Conveyor<TValue1, TValue2, TValue3, TValue4, TValueResult>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
            Func<TValue1, TValue2, TValue3, TValue4, Task<ResultBox<TValueResult>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
    {
        return await current.Conveyor(async value => await value.Call(handleValueFunc));
    }

    #endregion

    #region ThreeValues

    public static ResultBox<TValueResult> Conveyor<TValue1, TValue2, TValue3, TValueResult>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<TValue1, TValue2, TValue3, ResultBox<TValueResult>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
    {
        return current.Conveyor(value => value.Call(handleValueFunc));
    }

    public static async Task<ResultBox<TValueResult>> Conveyor<TValue1, TValue2, TValue3,
        TValueResult>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<TValue1, TValue2, TValue3, Task<ResultBox<TValueResult>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
    {
        return await current.Conveyor(async value => await value.Call(handleValueFunc));
    }

    #endregion
    
    #region TwoValues

    public static ResultBox<TValue3> Conveyor<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, ResultBox<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
    {
        return current.Conveyor(value => value.Call(handleValueFunc));
    }

    public static async Task<ResultBox<TValue3>> Conveyor<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, Task<ResultBox<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
    {
        return await current.Conveyor(async values => await values.Call(handleValueFunc));
    }
    #endregion
    
    #region SingleValue

    public static ResultBox<TValue2> Conveyor<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, ResultBox<TValue2>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
    {
        return current switch
        {
            { IsSuccess: true } => handleValueFunc(current.GetValue()),
            _ => ResultBox<TValue2>.FromException(current.GetException())
        };
    }

    public static ResultBox<TValue2> Conveyor<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<ResultBox<TValue2>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
    {
        return current switch
        {
            { IsSuccess: true } => handleValueFunc(),
            _ => ResultBox<TValue2>.FromException(current.GetException())
        };
    }

    public static async Task<ResultBox<TValue2>> Conveyor<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, Task<ResultBox<TValue2>>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
    {
        return current switch
        {
            { IsSuccess: true } => await handleValueFunc(current.GetValue()),
            _ => ResultBox<TValue2>.FromException(current.GetException())
        };
    }

    public static async Task<ResultBox<TValue2>> Conveyor<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<Task<ResultBox<TValue2>>> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
    {
        return current switch
        {
            { IsSuccess: true } => await handleValueFunc(),
            _ => ResultBox<TValue2>.FromException(current.GetException())
        };
    }

    #endregion
}
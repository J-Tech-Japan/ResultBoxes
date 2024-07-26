namespace ResultBoxes;

public static class VerifyExtension
{
    #region SingleValue

    public static ResultBox<TValue> Verify<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, ExceptionOrNone> predicate)
        where TValue : notnull
        => result.Conveyor(
            value => predicate(value) switch
            {
                { Exception: { } error } => ResultBox<TValue>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TValue>> Verify<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, ExceptionOrNone> predicate)
        where TValue : notnull
        => await (await result).Conveyor(
            value => Task.FromResult(
                predicate(value) switch
                {
                    { Exception: { } error } => ResultBox<TValue>.FromException(error),
                    _ => value
                }));

    public static Task<ResultBox<TValue>> Verify<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, Task<ExceptionOrNone>> predicate)
        where TValue : notnull
        => result.Conveyor(
            async value => await predicate(value) switch
            {
                { Exception: { } error } => ResultBox<TValue>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TValue>> Verify<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task<ExceptionOrNone>> predicate)
        where TValue : notnull
        => await (await result).Conveyor(
            async value => 
                await predicate(value) switch
                {
                    { Exception: { } error } => ResultBox<TValue>.FromException(error),
                    _ => value
                });
    #endregion

    #region SingleValueWithResult

    public static ResultBox<TValue> Verify<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, ResultBox<ExceptionOrNone>> predicate)
        where TValue : notnull
        => result.Conveyor(predicate)
            .Conveyor(exceptionOrNone => exceptionOrNone switch
            {
                { Exception: { } error } => ResultBox<TValue>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TValue>> Verify<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, ResultBox<ExceptionOrNone>> predicate)
        where TValue : notnull
        => await (await result)
            .Combine(predicate)
            .Conveyor(
            (value, exceptionOrNone) => Task.FromResult(
                exceptionOrNone switch
                {
                    { Exception: { } error } => ResultBox<TValue>.FromException(error),
                    _ => value
                }));

    public static Task<ResultBox<TValue>> Verify<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, Task<ResultBox<ExceptionOrNone>>> predicate)
        where TValue : notnull
        => result
            .Conveyor(predicate)
            .Conveyor(
            exceptionOrNone => exceptionOrNone switch
            {
                { Exception: { } error } => ResultBox<TValue>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TValue>> Verify<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, Task<ResultBox<ExceptionOrNone>>> predicate)
        where TValue : notnull
        => await (await result)
            .Combine(predicate)
            .Conveyor(
                (value, exceptionOrNone) => Task.FromResult(
                    exceptionOrNone switch
                    {
                        { Exception: { } error } => ResultBox<TValue>.FromException(error),
                        _ => value
                    }));
    #endregion

    #region TwoValues
    
    public static ResultBox<TwoValues<TValue1, TValue2>> Verify<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => result.Conveyor(
            values => predicate(values.Value1, values.Value2) switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Verify<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await result).Conveyor(
            values => predicate(values.Value1, values.Value2) switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => values
            });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Verify<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, Task<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => await result.Conveyor(
            async values => await predicate(values.Value1, values.Value2) switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Verify<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, Task<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await result).Conveyor(
            async values => await predicate(values.Value1, values.Value2) switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => values
            });
    #endregion
    #region TwoValuesWithResult
    
    public static ResultBox<TwoValues<TValue1, TValue2>> Verify<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, ResultBox<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => result
            .Conveyor(predicate)
            .Conveyor(
            exceptionOrNone => exceptionOrNone switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => result
            });
    public static Task<ResultBox<TwoValues<TValue1, TValue2>>> Verify<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, ResultBox<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => result
            .Combine(predicate)
            .Conveyor(
            (value1, value2, errorOrNone) => errorOrNone switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => TwoValues.FromValues(value1, value2)
            });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Verify<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, Task<ResultBox<ExceptionOrNone>>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => await result
            .Conveyor(predicate)
            .Conveyor(
            errorOrNone => errorOrNone switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => result
            });
    public static Task<ResultBox<TwoValues<TValue1, TValue2>>> Verify<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, Task<ResultBox<ExceptionOrNone>>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => result
            .Combine(predicate)
            .Conveyor(
                (value1, value2, errorOrNone) => errorOrNone switch
                {
                    { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                    _ => TwoValues.FromValues(value1, value2)
                });
    #endregion

    #region ThreeValues
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Verify<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => result.Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3) switch
            {
                { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> Verify<TValue1, TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> result,
        Func<TValue1, TValue2,TValue3, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => (await result).Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3) switch
            {
                { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                _ => values
            });
    
    public static Task< ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Verify<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, Task<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => result.Conveyor(
            async values => await predicate(values.Value1, values.Value2, values.Value3) switch
            {
                { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> Verify<TValue1, TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> result,
        Func<TValue1, TValue2,TValue3, Task<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await result).Conveyor(
            async values => await predicate(values.Value1, values.Value2, values.Value3) switch
            {
                { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                _ => values
            });

#endregion

    #region ThreeValuesWithResult
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Verify<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, ResultBox<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => result
            .Combine(predicate)
            .Conveyor(
            values => values.Value4 switch
            {
                { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                _ => ThreeValues.FromValues(values.Value1,values.Value2,values.Value3)
            });
    public static Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> Verify<TValue1, TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> result,
        Func<TValue1, TValue2,TValue3, ResultBox<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => result
            .Combine(predicate)
            .Conveyor(
                values => values.Value4 switch
                {
                    { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                    _ => ThreeValues.FromValues(values.Value1,values.Value2,values.Value3)
                });
    
    public static Task< ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Verify<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, Task<ResultBox<ExceptionOrNone>>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => result
            .Combine(async value => await predicate(value.Value1, value.Value2, value.Value3))
            .Conveyor(
                values => values.Value2 switch
                {
                    { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                    _ => values.Value1
                });
    public static Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> Verify<TValue1, TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> result,
        Func<TValue1, TValue2,TValue3, Task<ResultBox<ExceptionOrNone>>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => result
            .Combine(async value => await predicate(value.Value1, value.Value2, value.Value3))
            .Conveyor(
                values => values.Value2 switch
                {
                    { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                    _ => values.Value1
                });

#endregion

#region FourValues
    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> Verify<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => result.Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3, values.Value4) switch
            {
                { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Verify<TValue1, TValue2, TValue3, TValue4, T>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => (await result).Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3, values.Value4) switch
            {
                { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                _ => values
            });
    
    public static async Task< ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Verify<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await result.Conveyor(
            async values => await predicate(values.Value1, values.Value2, values.Value3, values.Value4) switch
            {
                { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Verify<TValue1, TValue2, TValue3, TValue4, T>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, Task<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await result).Conveyor(
            async values => await predicate(values.Value1, values.Value2, values.Value3, values.Value4) switch
            {
                { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                _ => values
            });

#endregion

#region FourValuesWithResult
    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> Verify<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, ResultBox<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => result
            .Combine(predicate)
            .Conveyor(
            values => values.Value5 switch
            {
                { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                _ => FourValues.FromValues(values.Value1, values.Value2, values.Value3, values.Value4)
            });
    public static Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Verify<TValue1, TValue2, TValue3, TValue4, T>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2,TValue3, TValue4,  ResultBox<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => result
            .Combine(predicate)
            .Conveyor(
                values => values.Value5 switch
                {
                    { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                    _ => FourValues.FromValues(values.Value1, values.Value2, values.Value3, values.Value4)
                });
    
    public static Task< ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Verify<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, Task< ResultBox<ExceptionOrNone>>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => result
            .Combine(values => predicate(values.Value1, values.Value2, values.Value3, values.Value4))
            .Conveyor(
                values => values.Value2 switch
                {
                    { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                    _ => values.Value1
                });
    public static Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Verify<TValue1, TValue2, TValue3, TValue4, T>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, Task< ResultBox<ExceptionOrNone>>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => result
            .Combine(values => predicate(values.Value1, values.Value2, values.Value3, values.Value4))
            .Conveyor(
                values => values.Value2 switch
                {
                    { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                    _ => values.Value1
                });
#endregion



#region FiveValues
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => result.Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3, values.Value4, values.Value5) switch
            {
                { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, TValue5, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await result).Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3, values.Value4, values.Value5) switch
            {
                { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                _ => values
            });
    
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await result.Conveyor(
            async values => await predicate(values.Value1, values.Value2, values.Value3, values.Value4, values.Value5) switch
            {
                { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, TValue5, Task<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await result).Conveyor(
            async values => await predicate(values.Value1, values.Value2, values.Value3, values.Value4, values.Value5) switch
            {
                { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                _ => values
            });

#endregion
#region FiveValuesWithResult
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, ResultBox<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => result
            .Combine(values => predicate(values.Value1, values.Value2, values.Value3,values.Value4, values.Value5 ))
            .Conveyor(
            values => values.Value2 switch
            {
                { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                _ => values.Value1
            });
    public static Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, TValue5, ResultBox<ExceptionOrNone>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => result
            .Combine(values => predicate(values.Value1, values.Value2, values.Value3,values.Value4, values.Value5 ))
            .Conveyor(
                values => values.Value2 switch
                {
                    { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                    _ => values.Value1
                });
    
    public static Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<ResultBox<ExceptionOrNone>>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => result
            .Combine(values => predicate(values.Value1, values.Value2, values.Value3,values.Value4, values.Value5 ))
            .Conveyor(
                values => values.Value2 switch
                {
                    { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                    _ => values.Value1
                });
    public static Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, TValue5, Task<ResultBox<ExceptionOrNone>>> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => result
            .Combine(values => predicate(values.Value1, values.Value2, values.Value3,values.Value4, values.Value5 ))
            .Conveyor(
                values => values.Value2 switch
                {
                    { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                    _ => values.Value1
                });

#endregion
}

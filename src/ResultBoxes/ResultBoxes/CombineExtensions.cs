namespace ResultBoxes;

public static class CombineExtensions
{

    #region Combine with ResultBox<> Value returns ResultBox<>
        public static ResultBox<FiveValues<TValue1, TValue2, TValue3,TValue4, TValue5>> CombineValue<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> values,
        ResultBox<TValue5> addingValue)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => values switch
        {
            { Exception: { } error } e => error,
            { Value: { } value } => addingValue switch
            {
                { Exception: { } error }  => error,
                { Value: { } value5 } => value.Append(value5),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };

    public static ResultBox<FourValues< TValue1, TValue2, TValue3,TValue4>> CombineValue<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> values,
        ResultBox<TValue4> fourthValue)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => values switch
        {
            { Exception: { } error }  => error,
            { Value: { } value } => fourthValue switch
            {
                { Exception: { } error } => error,
                { Value: { } toAppend } => value.Append(toAppend),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> CombineValue<TValue1, TValue2,
        TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> values,
        ResultBox<TValue3> thirdValue)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => values switch
        {
            { Exception: { } error } => error,
            { Value: { } value } => thirdValue switch
            {
                { Exception: { } error }  => error,
                { Value: { } addingValue } => value.Append(addingValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };


    public static ResultBox<TwoValues<TValue1, TValue2>> CombineValue<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        ResultBox<TValue2> secondValue)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: { } error }  => error,
            { Value: not null } => secondValue switch
            {
                { Exception: { } error }  => error,
                { Value: { } addingValue } => current.Append(addingValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };

    #endregion
    
    #region Combine with values func returns ResultBox<>

    public static ResultBox<FiveValues<TValue1, TValue2, TValue3,TValue4, TValue5>> CombineValue<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
        Func<TValue1, TValue2, TValue3, TValue4, ResultBox<TValue5>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => current switch
        {
            { Exception: { } error }  => error,
            { Value: { } value } => value.Call(addingFunc) switch
            {
                { Exception: { } error } => error,
                { Value: { } value5 } => value.Append(value5),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };

        public static ResultBox<FourValues< TValue1, TValue2, TValue3,TValue4>> CombineValue<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<TValue1, TValue2, TValue3, ResultBox<TValue4>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => current switch
        {
            { Exception: { } error }  => error,
            { Value: { } value } => value.Call(addingFunc) switch
            {
                { Exception: { } error }  => error,
                { Value: {} addingValue } => value.Append(addingValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> CombineValue<TValue1, TValue2,
        TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, ResultBox<TValue3>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => current switch
        {
            { Exception: { } error } => error,
            { Value: { } value } => addingFunc(value.Value1,value.Value2) switch
            {
                { Exception: { } error }  => error,
                { Value: { } addingValue } => value.Append(addingValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };
    public static ResultBox<TwoValues<TValue1, TValue2>> CombineValue<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<TValue1, ResultBox<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: { } error }  => error,
            { Value: { } value } => secondValueFunc(value) switch
            {
                { Exception: { } error }  => error,
                { Value: { } secondValue } => current.Append(secondValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };

    #endregion

    #region Combine with async no param func returns ResultBox<> 

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineValue<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<Task<ResultBox<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: { } error }  => error,
            { Value: not null } => await secondValueFunc() switch
            {
                { Exception: { } error }  => error,
                { Value: { } secondValue } => current.Append(secondValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };

    #endregion
    
    #region Combine with async values returns Task<ResultBox<>>
    public static async Task<ResultBox<TwoValues<TValue, TValue2>>> CombineValue<TValue,
        TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, Task<ResultBox<TValue2>>> secondValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: { } error }  => error,
            { Value: { } value } => await secondValueFunc(value) switch
            {
                { Exception: { } error }  => error,
                { Value: { } secondValue } => current.Append(secondValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };

    #endregion
}

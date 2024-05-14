namespace ResultBoxes;

public static class CombineExtensions
{

    #region Combine with async no param func returns ResultBox<>
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineValue<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<Task<ResultBox<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: { } error } => error,
            { Value: not null } => (await secondValueFunc()).Handle(current.Append),
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
            { Exception: { } error } => error,
            { Value: { } value } => (await secondValueFunc(value)).Handle(current.Append),
            _ => new ResultValueNullException()
        };
    #endregion

    #region Combine with ResultBox<> Value returns ResultBox<>
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> CombineValue<
        TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> values,
        ResultBox<TValue5> addingResult)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => values.Handle(value => addingResult.Handle(value.Append));

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> CombineValue<TValue1,
        TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> values,
        ResultBox<TValue4> addingResult)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => values.Handle(value => addingResult.Handle(value.Append));
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> CombineValue<TValue1, TValue2,
        TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> values,
        ResultBox<TValue3> addingResult)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => values.Handle(value => addingResult.Handle(value.Append));


    public static ResultBox<TwoValues<TValue1, TValue2>> CombineValue<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        ResultBox<TValue2> secondValue)
        where TValue1 : notnull
        where TValue2 : notnull
        => current.Handle(_ => secondValue.Handle(current.Append));
    #endregion

    #region Combine with values func returns ResultBox<>
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> CombineValue<
        TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
        Func<TValue1, TValue2, TValue3, TValue4, ResultBox<TValue5>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => current.Handle(value => value.Call(addingFunc).Handle(value.Append));

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> CombineValue<TValue1,
        TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<TValue1, TValue2, TValue3, ResultBox<TValue4>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => current.Handle(value => value.Call(addingFunc).Handle(value.Append));
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> CombineValue<TValue1, TValue2,
        TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, ResultBox<TValue3>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => current.Handle(value => value.Call(addingFunc).Handle(value.Append));
    public static ResultBox<TwoValues<TValue1, TValue2>> CombineValue<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<TValue1, ResultBox<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current.Handle(value => secondValueFunc(value).Handle(current.Append));
    #endregion
}

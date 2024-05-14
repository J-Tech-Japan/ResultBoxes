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
                { Value: { } value5 } => new FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>(value.Value1,
                        value.Value2,
                        value.Value3,
                        value.Value4,
                        addingValue.Value),
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
                { Value: not null } => new FourValues<TValue1, TValue2, TValue3, TValue4>(
                        value.Value1,
                        value.Value2,
                        value.Value3,
                        fourthValue.Value),
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
            { Exception: not null } e => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                default,
                e.Exception),
            { Value: { } value } => thirdValue switch
            {
                { Exception: not null } e => e.Exception,
                { Value: not null } => new ThreeValues<TValue1,TValue2,TValue3>(value.Value1, value.Value2, thirdValue.Value),
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
            { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: not null } => secondValue switch
            {
                { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                    default,
                    e.Exception),
                { Value: not null } => new ResultBox<TwoValues<TValue1, TValue2>>(
                    new TwoValues<TValue1, TValue2>(current.Value, secondValue.Value),
                    null),
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
            { Exception: not null } e => ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.FromException(
                e.Exception),
            { Value: { } value } => addingFunc(current.Value.Value1, current.Value.Value2,current.Value.Value3,current.Value.Value4) switch
            {
                { Exception: not null } e => ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.FromException(
                    e.Exception),
                { Value: { } value5 } => new ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>(
                    new (value.Value1,
                        value.Value2,
                        value.Value3,
                        value.Value4,
                        value5),
                    null),
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
            { Exception: not null } e => ResultBox<FourValues< TValue1, TValue2, TValue3,TValue4>>.FromException(
                e.Exception),
            { Value: { } value } => addingFunc(value.Value1,value.Value2,value.Value3) switch
            {
                { Exception: not null } e => ResultBox<FourValues< TValue1, TValue2, TValue3,TValue4>>.FromException(
                    e.Exception),
                { Value: {} addingValue } => new ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>(
                        new (value.Value1,
                        value.Value2,
                        value.Value3,
                        addingValue),
                    null),
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
            { Exception: not null } e => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                default,
                e.Exception),
            { Value: { } value } => addingFunc(value.Value1,value.Value2) switch
            {
                { Exception: not null } e => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                    default,
                    e.Exception),
                { Value: { } addingValue } => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                    new(value.Value1,
                    value.Value2,
                    addingValue),
                    null),
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
            { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: { } value } => secondValueFunc(value) switch
            {
                { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new ResultBox<TwoValues<TValue1, TValue2>>(
                    new TwoValues<TValue1, TValue2>(current.Value, secondValue),
                    null),
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
            { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: not null } => await secondValueFunc() switch
            {
                { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new ResultBox<TwoValues<TValue1, TValue2>>(
                    new TwoValues<TValue1, TValue2>(
                        current.Value,
                        secondValue),
                    null),
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
            { Exception: not null } e => new ResultBox<TwoValues<TValue, TValue2>>(
                default,
                e.Exception),
            { Value: { } value } => await secondValueFunc(value) switch
            {
                { Exception: not null } e => new ResultBox<TwoValues<TValue, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new ResultBox<TwoValues<TValue, TValue2>>(
                    new TwoValues<TValue, TValue2>(current.Value, secondValue),
                    null),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
        };

    #endregion
}

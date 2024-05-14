namespace ResultBoxes;

public static class CombineExtensions
{
    public static ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4, TValue5>> CombineValue<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> values,
        ResultBox<TValue5> addingValue)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => values switch
        {
            { Exception: not null } e => ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.FromException(
                e.Exception),
            { Value: { } value } => addingValue switch
            {
                { Exception: not null } e => ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.FromException(
                    e.Exception),
                { Value: { } value5 } => new ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>(
                    new (value.Value1,
                        value.Value2,
                        value.Value3,
                        value.Value4,
                        addingValue.Value),
                    null),
                _ => ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.OutOfRange
            },
            _ => ResultBox<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.OutOfRange
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
            { Exception: not null } e => ResultBox<FourValues< TValue1, TValue2, TValue3,TValue4>>.FromException(
                e.Exception),
            { Value: { } value } => fourthValue switch
            {
                { Exception: not null } e => ResultBox<FourValues< TValue1, TValue2, TValue3,TValue4>>.FromException(
                    e.Exception),
                { Value: not null } => new ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>(
                        new (value.Value1,
                        value.Value2,
                        value.Value3,
                        fourthValue.Value),
                    null),
                _ => ResultBox<FourValues< TValue1, TValue2, TValue3,TValue4>>.OutOfRange
            },
            _ => ResultBox<FourValues< TValue1, TValue2, TValue3,TValue4>>.OutOfRange
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
                { Exception: not null } e => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                    default,
                    e.Exception),
                { Value: not null } => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                    new(value.Value1,
                    value.Value2,
                    thirdValue.Value),
                    null),
                _ => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                default,
                new ResultValueNullException("out of range"))
        };


    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineValue<TValue1,
        TValue2>(
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
                _ => new ResultBox<TwoValues<TValue1, TValue2>>(default, null)
            },
            _ => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue1)} combine to {nameof(TValue2)}"))
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
                _ => new ResultBox<TwoValues<TValue1, TValue2>>(default, null)
            },
            _ => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue1)} combine to {nameof(TValue2)}"))
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
                _ => new ResultBox<TwoValues<TValue1, TValue2>>(
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };
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
                _ => new ResultBox<TwoValues<TValue, TValue2>>(
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new ResultBox<TwoValues<TValue, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };
}

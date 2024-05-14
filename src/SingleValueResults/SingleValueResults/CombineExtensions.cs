namespace SingleResults;

public static class CombineExtensions
{
    public static SingleValueResult<FiveValues< TValue1, TValue2, TValue3,TValue4, TValue5>> CombineValue<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>> values,
        SingleValueResult<TValue5> addingValue)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => values switch
        {
            { Exception: not null } e => SingleValueResult<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.FromException(
                e.Exception),
            { Value: { } value } => addingValue switch
            {
                { Exception: not null } e => SingleValueResult<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.FromException(
                    e.Exception),
                { Value: { } value5 } => new SingleValueResult<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>(
                    new (value.Value1,
                        value.Value2,
                        value.Value3,
                        value.Value4,
                        addingValue.Value),
                    null),
                _ => SingleValueResult<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.OutOfRange
            },
            _ => SingleValueResult<FiveValues< TValue1, TValue2, TValue3,TValue4,TValue5>>.OutOfRange
        };

    public static SingleValueResult<FourValues< TValue1, TValue2, TValue3,TValue4>> CombineValue<TValue1, TValue2, TValue3, TValue4>(
        this SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>> values,
        SingleValueResult<TValue4> fourthValue)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => values switch
        {
            { Exception: not null } e => SingleValueResult<FourValues< TValue1, TValue2, TValue3,TValue4>>.FromException(
                e.Exception),
            { Value: { } value } => fourthValue switch
            {
                { Exception: not null } e => SingleValueResult<FourValues< TValue1, TValue2, TValue3,TValue4>>.FromException(
                    e.Exception),
                { Value: not null } => new SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>>(
                        new (value.Value1,
                        value.Value2,
                        value.Value3,
                        fourthValue.Value),
                    null),
                _ => SingleValueResult<FourValues< TValue1, TValue2, TValue3,TValue4>>.OutOfRange
            },
            _ => SingleValueResult<FourValues< TValue1, TValue2, TValue3,TValue4>>.OutOfRange
        };
    public static SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>> CombineValue<TValue1, TValue2,
        TValue3>(
        this SingleValueResult<TwoValues<TValue1, TValue2>> values,
        SingleValueResult<TValue3> thirdValue)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => values switch
        {
            { Exception: not null } e => new SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>(
                default,
                e.Exception),
            { Value: { } value } => thirdValue switch
            {
                { Exception: not null } e => new SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>(
                    default,
                    e.Exception),
                { Value: not null } => new SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>(
                    new(value.Value1,
                    value.Value2,
                    thirdValue.Value),
                    null),
                _ => new SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>(
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>(
                default,
                new ResultValueNullException("out of range"))
        };


    public static async Task<SingleValueResult<TwoValues<TValue1, TValue2>>> CombineValue<TValue1,
        TValue2>(
        this SingleValueResult<TValue1> current,
        Func<Task<SingleValueResult<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: not null } => await secondValueFunc() switch
            {
                { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    new TwoValues<TValue1, TValue2>(
                        current.Value,
                        secondValue),
                    null),
                _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(default, null)
            },
            _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue1)} combine to {nameof(TValue2)}"))
        };
    public static SingleValueResult<TwoValues<TValue1, TValue2>> CombineValue<TValue1, TValue2>(
        this SingleValueResult<TValue1> current,
        SingleValueResult<TValue2> secondValue)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: not null } => secondValue switch
            {
                { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    default,
                    e.Exception),
                { Value: not null } => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    new TwoValues<TValue1, TValue2>(current.Value, secondValue.Value),
                    null),
                _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(default, null)
            },
            _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue1)} combine to {nameof(TValue2)}"))
        };

    public static SingleValueResult<TwoValues<TValue1, TValue2>> CombineValue<TValue1, TValue2>(
        this SingleValueResult<TValue1> current,
        Func<TValue1, SingleValueResult<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: { } value } => secondValueFunc(value) switch
            {
                { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    new TwoValues<TValue1, TValue2>(current.Value, secondValue),
                    null),
                _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };
    public static async Task<SingleValueResult<TwoValues<TValue, TValue2>>> CombineValue<TValue,
        TValue2>(
        this SingleValueResult<TValue> current,
        Func<TValue, Task<SingleValueResult<TValue2>>> secondValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        => current switch
        {
            { Exception: not null } e => new SingleValueResult<TwoValues<TValue, TValue2>>(
                default,
                e.Exception),
            { Value: { } value } => await secondValueFunc(value) switch
            {
                { Exception: not null } e => new SingleValueResult<TwoValues<TValue, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new SingleValueResult<TwoValues<TValue, TValue2>>(
                    new TwoValues<TValue, TValue2>(current.Value, secondValue),
                    null),
                _ => new SingleValueResult<TwoValues<TValue, TValue2>>(
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new SingleValueResult<TwoValues<TValue, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };
}
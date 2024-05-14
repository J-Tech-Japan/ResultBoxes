namespace SingleResults;

public static class CombineTaskExtensions
{
    public static async Task<SingleValueResult<TwoValues<TValue1, TValue2>>> CombineValue<TValue1,
        TValue2>(
        this Task<SingleValueResult<TValue1>> firstValueTask,
        Func<Task<SingleValueResult<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: { } firstValue } => await secondValueFunc() switch
            {
                { Exception: not null } e => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    new TwoValues<TValue1, TValue2>(firstValue, secondValue),
                    null),
                _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new SingleValueResult<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };

    public static async Task<SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>> CombineValue<TValue1,
        TValue2, TValue3>(
        this Task<SingleValueResult<TwoValues<TValue1, TValue2>>> firstValueTask,
        Func<Task<SingleValueResult<TValue3>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>.FromException(e.Exception),
            { Value: { } values } => await secondValueFunc() switch
            {
                { Exception: not null } e => SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>.FromException(e.Exception),
                { Value: { } thirdValue } => new SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>(
                    new( values.Value1, values.Value2, thirdValue),
                    null),
                _ => SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>.OutOfRange
            },
            var first => SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>.OutOfRange
        };

    public static async Task<SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>>> CombineValue<
        TValue1, TValue2, TValue3, TValue4>(
        this Task<SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>> firstValueTask,
        Func<Task<SingleValueResult<TValue4>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(e.Exception),
            { Value: { } values } =>
                await secondValueFunc() switch
                {
                    { Exception: not null } e => 
                        SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(e.Exception),
                    { Value: { } fourthValue } => new
                        SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>>(
                            new(values.Value1,
                            values.Value2,
                            values.Value3,
                            fourthValue),
                            null),
                    _ => SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>>.OutOfRange
                },
            _ => SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>>.OutOfRange
        };

    public static async Task<SingleValueResult<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        CombineValue<
            TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<SingleValueResult<FourValues<TValue1, TValue2, TValue3, TValue4>>> currentValuesTask,
            Func<Task<SingleValueResult<TValue5>>> fifthValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await currentValuesTask switch
        {
            { Exception: not null } e => 
                SingleValueResult<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(
                    e.Exception),
            { Value: { } values } =>
                await fifthValueFunc() switch
                {
                    { Exception: not null } e => 
                        SingleValueResult<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(
                            e.Exception),
                    { Value: { } fifthValue } => new
                        SingleValueResult<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>(
                            new(values.Value1,
                            values.Value2,
                            values.Value3,
                            values.Value4,
                            fifthValue),
                            null),
                    _ => SingleValueResult<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.OutOfRange
                },
            var first => SingleValueResult<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.OutOfRange
        };
}

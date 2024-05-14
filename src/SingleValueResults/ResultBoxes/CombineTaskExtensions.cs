namespace ResultBoxes;

public static class CombineTaskExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineValue<TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<Task<ResultBox<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                e.Exception),
            { Value: { } firstValue } => await secondValueFunc() switch
            {
                { Exception: not null } e => new ResultBox<TwoValues<TValue1, TValue2>>(
                    default,
                    e.Exception),
                { Value: { } secondValue } => new ResultBox<TwoValues<TValue1, TValue2>>(
                    new TwoValues<TValue1, TValue2>(firstValue, secondValue),
                    null),
                _ => new ResultBox<TwoValues<TValue1, TValue2>>(
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new ResultBox<TwoValues<TValue1, TValue2>>(
                default,
                new ResultValueNullException("out of range"))
        };

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineValue<TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValueTask,
        Func<Task<ResultBox<TValue3>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.FromException(e.Exception),
            { Value: { } values } => await secondValueFunc() switch
            {
                { Exception: not null } e => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.FromException(e.Exception),
                { Value: { } thirdValue } => new ResultBox<ThreeValues<TValue1, TValue2, TValue3>>(
                    new( values.Value1, values.Value2, thirdValue),
                    null),
                _ => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.OutOfRange
            },
            var first => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.OutOfRange
        };

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> CombineValue<
        TValue1, TValue2, TValue3, TValue4>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValueTask,
        Func<Task<ResultBox<TValue4>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(e.Exception),
            { Value: { } values } =>
                await secondValueFunc() switch
                {
                    { Exception: not null } e => 
                        ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(e.Exception),
                    { Value: { } fourthValue } => new
                        ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>(
                            new(values.Value1,
                            values.Value2,
                            values.Value3,
                            fourthValue),
                            null),
                    _ => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.OutOfRange
                },
            _ => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.OutOfRange
        };

    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        CombineValue<
            TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> currentValuesTask,
            Func<Task<ResultBox<TValue5>>> fifthValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await currentValuesTask switch
        {
            { Exception: not null } e => 
                ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(
                    e.Exception),
            { Value: { } values } =>
                await fifthValueFunc() switch
                {
                    { Exception: not null } e => 
                        ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(
                            e.Exception),
                    { Value: { } fifthValue } => new
                        ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>(
                            new(values.Value1,
                            values.Value2,
                            values.Value3,
                            values.Value4,
                            fifthValue),
                            null),
                    _ => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.OutOfRange
                },
            var first => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.OutOfRange
        };
}

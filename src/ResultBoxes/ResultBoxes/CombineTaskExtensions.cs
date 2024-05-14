namespace ResultBoxes;

public static class CombineTaskExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineValue<TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> currentTask,
        Func<Task<ResultBox<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await currentTask switch
        {
            { Exception: { } error }  => error,
            { Value: { } } current => await secondValueFunc() switch
            {
                { Exception: { } error }  => error,
                { Value: { } secondValue } => current.Append(secondValue),
                _ => new ResultValueNullException()
            },
            _ => new ResultValueNullException()
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
            { Exception: { } error }  => error,
            { Value: { } values } => await secondValueFunc() switch
            {
                { Exception: { } error }  => error,
                { Value: { } thirdValue } => values.Append(thirdValue),
                _ => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.OutOfRange
            },
            _ => new ResultValueNullException()
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
            { Exception: { } error }  => error,
            { Value: { } values } =>
                await secondValueFunc() switch
                {
                    { Exception: { } error }  => error,
                    { Value: { } fourthValue } => new
                        ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>(
                            new(values.Value1,
                            values.Value2,
                            values.Value3,
                            fourthValue),
                            null),
                    _ => new ResultValueNullException()
                },
            _ => new ResultValueNullException()
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
            { Exception: { } error }  => error,
            { Value: { } values } =>
                await fifthValueFunc() switch
                {
                    { Exception: { } error }  => error,
                    { Value: { } fifthValue } => values.Append(fifthValue),
                    _ => new ResultValueNullException()
                },
            _ => new ResultValueNullException()
        };
}

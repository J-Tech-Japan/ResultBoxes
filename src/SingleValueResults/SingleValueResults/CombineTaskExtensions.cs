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

    public static async Task<ThreeValuesResult<TValue1, TValue2, TValue3>> CombineValue<TValue1,
        TValue2, TValue3>(
        this Task<SingleValueResult<TwoValues<TValue1, TValue2>>> firstValueTask,
        Func<Task<SingleValueResult<TValue3>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                default,
                default,
                default,
                e.Exception),
            { Value: { } values } => await secondValueFunc() switch
            {
                { Exception: not null } e => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                    default,
                    default,
                    default,
                    e.Exception),
                { Value: { } thirdValue } => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                    values.Value1,
                    values.Value2,
                    thirdValue,
                    null),
                _ => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                    default,
                    default,
                    default,
                    new ResultValueNullException("out of range"))
            },
            var first => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                default,
                default,
                default,
                new ResultValueNullException("out of range"))
        };

    public static async Task<FourValuesResult<TValue1, TValue2, TValue3, TValue4>> CombineValue<
        TValue1, TValue2, TValue3, TValue4>(
        this Task<ThreeValuesResult<TValue1, TValue2, TValue3>> firstValueTask,
        Func<Task<SingleValueResult<TValue4>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                e.Value1,
                e.Value2,
                e.Value3,
                default,
                e.Exception),
            { Value1: { } firstValue, Value2: { } secondValue, Value3: { } thirdValue } =>
                await secondValueFunc() switch
                {
                    { Exception: not null } e => new
                        FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                            firstValue,
                            secondValue,
                            thirdValue,
                            default,
                            e.Exception),
                    { Value: { } fourthValue } => new
                        FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                            firstValue,
                            secondValue,
                            thirdValue,
                            fourthValue,
                            null),
                    _ => new FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                        firstValue,
                        secondValue,
                        thirdValue,
                        default,
                        new ResultValueNullException("out of range"))
                },
            var first => new FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                first.Value1,
                first.Value2,
                first.Value3,
                default,
                new ResultValueNullException("out of range"))
        };

    public static async Task<FiveValuesResult<TValue1, TValue2, TValue3, TValue4, TValue5>>
        CombineValue<
            TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<FourValuesResult<TValue1, TValue2, TValue3, TValue4>> currentValuesTask,
            Func<Task<SingleValueResult<TValue5>>> fifthValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await currentValuesTask switch
        {
            { Exception: not null } e => new
                FiveValuesResult<TValue1, TValue2, TValue3, TValue4, TValue5>(
                    e.Value1,
                    e.Value2,
                    e.Value3,
                    e.Value4,
                    default,
                    e.Exception),
            {
                    Value1: { } firstValue, Value2: { } secondValue, Value3: { } thirdValue,
                    Value4: { } fourthValue
                } =>
                await fifthValueFunc() switch
                {
                    { Exception: not null } e => new
                        FiveValuesResult<TValue1, TValue2, TValue3, TValue4, TValue5>(
                            firstValue,
                            secondValue,
                            thirdValue,
                            fourthValue,
                            default,
                            e.Exception),
                    { Value: { } fifthValue } => new
                        FiveValuesResult<TValue1, TValue2, TValue3, TValue4, TValue5>(
                            firstValue,
                            secondValue,
                            thirdValue,
                            fourthValue,
                            fifthValue,
                            null),
                    _ => new FiveValuesResult<TValue1, TValue2, TValue3, TValue4, TValue5>(
                        firstValue,
                        secondValue,
                        thirdValue,
                        fourthValue,
                        default,
                        new ResultValueNullException("out of range"))
                },
            var first => new FiveValuesResult<TValue1, TValue2, TValue3, TValue4, TValue5>(
                first.Value1,
                first.Value2,
                first.Value3,
                first.Value4,
                default,
                new ResultValueNullException("out of range"))
        };
}

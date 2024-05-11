namespace SingleResults;

public static class SingleValueResultExtension
{
    public static SingleValueResult<TValue3> Railway<TValue1, TValue2, TValue3>(
        this SingleValueResult<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, SingleValueResult<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { Value1: {} value1, Value2:{} value2 }  } => handleValueFunc(value1, value2),
                _ => SingleValueResult<TValue3>.OutOfRange
            };

    
    
    public static async Task<SingleValueResult<TValue2>> Railway<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValue,
        Func<TValue1, Task<SingleValueResult<TValue2>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => await handleValueFunc(value),
                _ => SingleValueResult<TValue2>.OutOfRange
            };
    public static async Task<SingleValueResult<TValue2>> RailwayWrapTry<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValue,
        Func<TValue1, Task<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => await SingleValueResult<TValue2>.WrapTry(
                    () => handleValueFunc(value)),
                _ => SingleValueResult<TValue2>.OutOfRange
            };
    public static async Task<SingleValueResult<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<TwoValuesResult<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<SingleValueResult<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: not null } e => e.Exception,
                    { Value1: { } value1, Value2: { } value2 } => await handleValueFunc(
                        value1,
                        value2),
                    _ => SingleValueResult<TValue3>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue4>>
        Railway<TValue1, TValue2, TValue3, TValue4>(
            this Task<ThreeValuesResult<TValue1, TValue2, TValue3>> firstValue,
            Func<TValue1, TValue2, TValue3, Task<SingleValueResult<TValue4>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: not null } e => e.Exception,
                    { Value1: { } value1, Value2: { } value2, Value3: { } value3 } => await
                        handleValueFunc(
                            value1,
                            value2,
                            value3),
                    _ => SingleValueResult<TValue4>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue5>> Railway<TValue1, TValue2, TValue3, TValue4,
        TValue5>(
        this Task<FourValuesResult<TValue1, TValue2, TValue3, TValue4>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, Task<SingleValueResult<TValue5>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: not null } e => e.Exception,
                    {
                        Value1: { } value1, Value2: { } value2, Value3: { } value3,
                        Value4: { } value4
                    } => await handleValueFunc(
                        value1,
                        value2,
                        value3,
                        value4),
                    _ => SingleValueResult<TValue5>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue6>> Railway<TValue1, TValue2, TValue3, TValue4,
        TValue5, TValue6>(
        this Task<FiveValuesResult<TValue1, TValue2, TValue3, TValue4, TValue5>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<SingleValueResult<TValue6>>>
            handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValue6 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: not null } e => e.Exception,
                    {
                        Value1: { } value1, Value2: { } value2, Value3: { } value3,
                        Value4: { } value4, Value5: { } value5
                    } => await handleValueFunc(
                        value1,
                        value2,
                        value3,
                        value4,
                        value5),
                    _ => SingleValueResult<TValue6>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue3>> RailwayWrapTry<TValue1, TValue2,
        TValue3>(
        this Task<TwoValuesResult<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value1: { } value1, Value2: { } value2 } => await SingleValueResult<TValue3>
                    .WrapTry(
                        () => handleValueFunc(value1, value2)),
                _ => SingleValueResult<TValue3>.OutOfRange
            };
    public static async Task<SingleValueResult<TValue2>> Railway<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValue,
        Func<TValue1, SingleValueResult<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => handleValueFunc(value),
                _ => SingleValueResult<TValue2>.OutOfRange
            };
    public static async Task<SingleValueResult<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<TwoValuesResult<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, SingleValueResult<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value1: { } value1, Value2: { } value2 } => handleValueFunc(value1, value2),
                _ => SingleValueResult<TValue3>.OutOfRange
            };

    public static async Task<TwoValuesResult<TValue1, TValue2>> CombineValue<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValueTask,
        Func<Task<SingleValueResult<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new TwoValuesResult<TValue1, TValue2>(
                e.Value,
                default,
                e.Exception),
            { Value: { } firstValue } => await secondValueFunc() switch
            {
                { Exception: not null } e => new TwoValuesResult<TValue1, TValue2>(
                    firstValue,
                    default,
                    e.Exception),
                { Value: { } secondValue } => new TwoValuesResult<TValue1, TValue2>(
                    firstValue,
                    secondValue,
                    null),
                _ => new TwoValuesResult<TValue1, TValue2>(
                    firstValue,
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new TwoValuesResult<TValue1, TValue2>(
                default,
                default,
                new ResultValueNullException("out of range"))
        };

    public static async Task<ThreeValuesResult<TValue1, TValue2, TValue3>> CombineValue<TValue1,
        TValue2, TValue3>(
        this Task<TwoValuesResult<TValue1, TValue2>> firstValueTask,
        Func<Task<SingleValueResult<TValue3>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                e.Value1,
                e.Value2,
                default,
                e.Exception),
            { Value1: { } firstValue, Value2: { } secondValue } => await secondValueFunc() switch
            {
                { Exception: not null } e => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                    firstValue,
                    secondValue,
                    default,
                    e.Exception),
                { Value: { } thirdValue } => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                    firstValue,
                    secondValue,
                    thirdValue,
                    null),
                _ => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                    firstValue,
                    secondValue,
                    default,
                    new ResultValueNullException("out of range"))
            },
            var first => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                first.Value1,
                first.Value2,
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

    public static async Task<TwoValuesResult<TValue1, TValue2>> CombineValueWrapTry<TValue1,
        TValue2>(
        this Task<SingleValueResult<TValue1>> firstValueTask,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValueTask switch
        {
            { Exception: not null } e => new TwoValuesResult<TValue1, TValue2>(
                e.Value,
                default,
                e.Exception),
            { Value: { } firstValue } => await SingleValueResult<TValue2>.WrapTry(
                    secondValueFunc)
                switch
                {
                    { Exception: not null } e => new TwoValuesResult<TValue1, TValue2>(
                        firstValue,
                        default,
                        e.Exception),
                    { Value: { } secondValue } => new TwoValuesResult<TValue1, TValue2>(
                        firstValue,
                        secondValue,
                        null),
                    _ => new TwoValuesResult<TValue1, TValue2>(firstValue, default, null)
                },
            _ => new TwoValuesResult<TValue1, TValue2>(
                default,
                default,
                new ResultValueNullException("out of range"))
        };
}

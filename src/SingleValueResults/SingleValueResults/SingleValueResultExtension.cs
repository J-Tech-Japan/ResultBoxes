namespace SingleResults;

public static class SingleValueResultExtension
{
    public static async Task<SingleValueResult<TValue2>> RailwayAsync<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValue,
        Func<TValue1, Task<SingleValueResult<TValue2>>> handleValueFunc) => await firstValue
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value: { } value } => await handleValueFunc(value),
            _ => SingleValueResult<TValue2>.OutOfRange
        };
    public static async Task<SingleValueResult<TValue2>> RailwayAsyncWrapTry<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValue,
        Func<TValue1, Task<TValue2>> handleValueFunc) => await firstValue
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value: { } value } => await SingleValueResult<TValue2>.WrapTryAsync(
                () => handleValueFunc(value)),
            _ => SingleValueResult<TValue2>.OutOfRange
        };
    public static async Task<SingleValueResult<TValue3>> RailwayAsync<TValue1, TValue2, TValue3>(
        this Task<TwoValuesResult<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<SingleValueResult<TValue3>>> handleValueFunc) =>
        await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value1: { } value1, Value2: { } value2 } => await handleValueFunc(value1, value2),
                _ => SingleValueResult<TValue3>.OutOfRange
            };
    public static async Task<SingleValueResult<TValue3>> RailwayAsyncWrapTry<TValue1, TValue2,
        TValue3>(
        this Task<TwoValuesResult<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc) => await firstValue
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value1: { } value1, Value2: { } value2 } => await SingleValueResult<TValue3>
                .WrapTryAsync(
                    () => handleValueFunc(value1, value2)),
            _ => SingleValueResult<TValue3>.OutOfRange
        };
    public static async Task<SingleValueResult<TValue2>> Railway<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValue,
        Func<TValue1, SingleValueResult<TValue2>> handleValueFunc) => await firstValue
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value: { } value } => handleValueFunc(value),
            _ => SingleValueResult<TValue2>.OutOfRange
        };
    public static async Task<SingleValueResult<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<TwoValuesResult<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, SingleValueResult<TValue3>> handleValueFunc) => await firstValue
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value1: { } value1, Value2: { } value2 } => handleValueFunc(value1, value2),
            _ => SingleValueResult<TValue3>.OutOfRange
        };

    public static async Task<TwoValuesResult<TValue1, TValue2>> CombineValueAsync<TValue1, TValue2>(
        this Task<SingleValueResult<TValue1>> firstValueTask,
        Func<Task<SingleValueResult<TValue2>>> secondValueFunc) => await firstValueTask switch
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
            _ => new TwoValuesResult<TValue1, TValue2>(firstValue, default, null)
        },
        _ => new TwoValuesResult<TValue1, TValue2>(
            default,
            default,
            new ResultValueNullException("out of range"))
    };
    public static async Task<TwoValuesResult<TValue1, TValue2>> CombineValueAsyncWrapTry<TValue1,
        TValue2>(
        this Task<SingleValueResult<TValue1>> firstValueTask,
        Func<Task<TValue2>> secondValueFunc) => await firstValueTask switch
    {
        { Exception: not null } e => new TwoValuesResult<TValue1, TValue2>(
            e.Value,
            default,
            e.Exception),
        { Value: { } firstValue } => await SingleValueResult<TValue2>.WrapTryAsync(secondValueFunc)
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

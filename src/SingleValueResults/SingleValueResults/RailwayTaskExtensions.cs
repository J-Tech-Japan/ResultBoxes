namespace SingleResults;

public static class RailwayTaskExtensions
{
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
    
    public static async Task<SingleValueResult<TValue3>> RailwayWrapTry<TValue1, TValue2, TValue3>(
        this Task<SingleValueResult<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } values } => await SingleValueResult<TValue3>
                    .WrapTry(
                        () => handleValueFunc(values.Value1, values.Value2)),
                _ => SingleValueResult<TValue3>.OutOfRange
            };

    public static async Task<SingleValueResult<TValueReturn>> Railway<TValue1, TValue2, TValue3, TValueReturn>(
        this Task<SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
        Func<TValue1, TValue2, SingleValueResult<TValueReturn>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
    where TValueReturn : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } values } => handleValueFunc(values.Value1, values.Value2),
                _ => SingleValueResult<TValueReturn>.OutOfRange
            };

    
    public static async Task<SingleValueResult<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this SingleValueResult<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<SingleValueResult<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        =>
            firstValue
                switch
                {
                    { Exception: not null } e => e.Exception,
                    { Value: { } values } => await handleValueFunc(
                        values.Value1,
                        values.Value2),
                    _ => SingleValueResult<TValue3>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<SingleValueResult<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<SingleValueResult<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: not null } e => e.Exception,
                    { Value: { } values } => await handleValueFunc(
                        values.Value1,
                        values.Value2),
                    _ => SingleValueResult<TValue3>.OutOfRange
                };

    
    public static async Task<SingleValueResult<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<SingleValueResult<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, SingleValueResult<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } values } => handleValueFunc(values.Value1, values.Value2),
                _ => SingleValueResult<TValue3>.OutOfRange
            };
    public static async Task<SingleValueResult<TValue4>>
        Railway<TValue1, TValue2, TValue3, TValue4>(
            this Task<SingleValueResult<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
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
                    { Value: { } values } => await
                        handleValueFunc(
                            values.Value1,
                            values.Value2,
                            values.Value3),
                    _ => SingleValueResult<TValue4>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue5>> Railway<TValue1, TValue2, TValue3, TValue4,
        TValue5>(
        this Task<SingleValueResult<FourValues< TValue1, TValue2, TValue3, TValue4>>> firstValue,
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
                        Value: { } values
                    } => await handleValueFunc(
                        values.Value1,
                        values.Value2,
                        values.Value3,
                        values.Value4),
                    _ => SingleValueResult<TValue5>.OutOfRange
                };

    public static async Task<SingleValueResult<TValue6>> Railway<TValue1, TValue2, TValue3, TValue4,
        TValue5, TValue6>(
        this Task<SingleValueResult<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> firstValue,
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
                        Value: { } values
                    } => await handleValueFunc(
                        values.Value1,
                        values.Value2,
                        values.Value3,
                        values.Value4,
                        values.Value5),
                    _ => SingleValueResult<TValue6>.OutOfRange
                };
}

namespace ResultBoxes;

public static class RailwayTaskExtensions
{
    public static async Task<ResultBox<TValue2>> Railway<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, Task<ResultBox<TValue2>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue
            switch
            {
                { Exception: { } error }  => error,
                { Value: { } value } => await handleValueFunc(value),
                _ => ResultBox<TValue2>.OutOfRange
            };
    public static async Task<ResultBox<TValue2>> Railway<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, ResultBox<TValue2>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await firstValue
            switch
            {
                { Exception: { } error }  => error,
                { Value: { } value } => handleValueFunc(value),
                _ => ResultBox<TValue2>.OutOfRange
            };
    
    public static async Task<ResultBox<TValue3>> RailwayWrapTry<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue
            switch
            {
                { Exception: { } error }  => error,
                { Value: { } values } => await ResultBox<TValue3>
                    .WrapTry(
                        () => handleValueFunc(values.Value1, values.Value2)),
                _ => ResultBox<TValue3>.OutOfRange
            };

    public static async Task<ResultBox<TValueReturn>> Railway<TValue1, TValue2, TValue3, TValueReturn>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
        Func<TValue1, TValue2, ResultBox<TValueReturn>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
    where TValueReturn : notnull
        => await firstValue
            switch
            {
                { Exception: { } error }  => error,
                { Value: { } values } => handleValueFunc(values.Value1, values.Value2),
                _ => ResultBox<TValueReturn>.OutOfRange
            };

    
    public static async Task<ResultBox<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, Task<ResultBox<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        =>
            firstValue
                switch
                {
                    { Exception: { } error }  => error,
                    { Value: { } values } => await handleValueFunc(
                        values.Value1,
                        values.Value2),
                    _ => ResultBox<TValue3>.OutOfRange
                };

    public static async Task<ResultBox<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<ResultBox<TValue3>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: { } error }  => error,
                    { Value: { } values } => await handleValueFunc(
                        values.Value1,
                        values.Value2),
                    _ => ResultBox<TValue3>.OutOfRange
                };

    
    public static async Task<ResultBox<TValue3>> Railway<TValue1, TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, ResultBox<TValue3>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await firstValue
            switch
            {
                { Exception: { } error }  => error,
                { Value: { } values } => handleValueFunc(values.Value1, values.Value2),
                _ => ResultBox<TValue3>.OutOfRange
            };
    public static async Task<ResultBox<TValue4>>
        Railway<TValue1, TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
            Func<TValue1, TValue2, TValue3, Task<ResultBox<TValue4>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: { } error }  => error,
                    { Value: { } values } => await
                        handleValueFunc(
                            values.Value1,
                            values.Value2,
                            values.Value3),
                    _ => ResultBox<TValue4>.OutOfRange
                };

    public static async Task<ResultBox<TValue5>> Railway<TValue1, TValue2, TValue3, TValue4,
        TValue5>(
        this Task<ResultBox<FourValues< TValue1, TValue2, TValue3, TValue4>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, Task<ResultBox<TValue5>>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        =>
            await firstValue
                switch
                {
                    { Exception: { } error }  => error,
                    { Value: { } values } => await handleValueFunc(
                        values.Value1,
                        values.Value2,
                        values.Value3,
                        values.Value4),
                    _ => ResultBox<TValue5>.OutOfRange
                };

    public static async Task<ResultBox<TValue6>> Railway<TValue1, TValue2, TValue3, TValue4,
        TValue5, TValue6>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<ResultBox<TValue6>>>
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
                    { Exception: { } error }  => error,
                    {
                        Value: { } values
                    } => await handleValueFunc(
                        values.Value1,
                        values.Value2,
                        values.Value3,
                        values.Value4,
                        values.Value5),
                    _ => ResultBox<TValue6>.OutOfRange
                };
}

namespace ResultBoxes;

public static class VerifyExtension
{
    public static ResultBox<TValue> Verify<TValue>(
        this ResultBox<TValue> result,
        Func<TValue, ExceptionOrNone> predicate)
        where TValue : notnull
        => result.Conveyor(
            value => predicate(value) switch
            {
                { Exception: { } error } => ResultBox<TValue>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TValue>> Verify<TValue>(
        this Task<ResultBox<TValue>> result,
        Func<TValue, ExceptionOrNone> predicate)
        where TValue : notnull
        => await (await result).Conveyor(
            value => Task.FromResult(
                predicate(value) switch
                {
                    { Exception: { } error } => ResultBox<TValue>.FromException(error),
                    _ => value
                }));
    
    
    public static ResultBox<TwoValues<TValue1, TValue2>> Verify<TValue1, TValue2>(
        this ResultBox<TwoValues<TValue1, TValue2>> result,
        Func<TValue1, TValue2, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => result.Conveyor(
            values => predicate(values.Value1, values.Value2) switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Verify<TValue1, TValue2>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> result,
        Func<TValue1, TValue2, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await result).Conveyor(
            values => predicate(values.Value1, values.Value2) switch
            {
                { Exception: { } error } => ResultBox<TwoValues<TValue1,TValue2>>.FromException(error),
                _ => values
            });

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Verify<TValue1, TValue2, TValue3>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> result,
        Func<TValue1, TValue2, TValue3, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => result.Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3) switch
            {
                { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> Verify<TValue1, TValue2, TValue3>(
        this Task<ResultBox<ThreeValues<TValue1,TValue2, TValue3>>> result,
        Func<TValue1, TValue2,TValue3, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => (await result).Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3) switch
            {
                { Exception: { } error } => ResultBox<ThreeValues<TValue1,TValue2, TValue3>>.FromException(error),
                _ => values
            });

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> Verify<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> result,
        Func<TValue1, TValue2, TValue3, TValue4, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => result.Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3, values.Value4) switch
            {
                { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> Verify<TValue1, TValue2, TValue3, TValue4, T>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => (await result).Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3, values.Value4) switch
            {
                { Exception: { } error } => ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>.FromException(error),
                _ => values
            });

    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>> result,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => result.Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3, values.Value4, values.Value5) switch
            {
                { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                _ => result
            });
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> Verify<TValue1, TValue2, TValue3, TValue4, TValue5>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> result,
        Func<TValue1, TValue2,TValue3, TValue4, TValue5, ExceptionOrNone> predicate)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await result).Conveyor(
            values => predicate(values.Value1, values.Value2, values.Value3, values.Value4, values.Value5) switch
            {
                { Exception: { } error } => ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>.FromException(error),
                _ => values
            });

}

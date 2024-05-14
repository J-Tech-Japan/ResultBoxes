namespace ResultBoxes;

public static class RailwayWrapTryExtensions
{
    public static ResultBox<TValue3> RailwayWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, TValue3> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue
            switch
            {
                { Exception: { } error }  => error,
                { Value: { } values } => ResultBox<TValue3>
                    .WrapTry(
                        () => handleValueFunc(values.Value1, values.Value2)),
                _ => ResultBox<TValue3>.OutOfRange
            };

    public static ResultBox<TValue2> RailwayWrapTry<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, TValue2> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        =>
            current switch
            {
                { Exception: { } error }  => error,
                { Value: { } value } => ResultBox<TValue2>.WrapTry(
                    () => handleValueFunc(value)),
                _ => ResultBox<TValue2>.OutOfRange
            };
}

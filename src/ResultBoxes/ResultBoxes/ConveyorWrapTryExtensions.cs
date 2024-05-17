namespace ResultBoxes;

public static class ConveyorWrapTryExtensions
{
    public static ResultBox<TValue3> ConveyorWrapTry<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> firstValue,
        Func<TValue1, TValue2, TValue3> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => firstValue.Remap(
            values => ResultBox<TValue3>
                .WrapTry(() => values.Call(handleValueFunc)));

    public static ResultBox<TValue2> ConveyorWrapTry<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, TValue2> handleValueFunc)
        where TValue : notnull
        where TValue2 : notnull
        =>
            current.Remap(
                value => ResultBox<TValue2>.WrapTry(
                    () => handleValueFunc(value)));
}

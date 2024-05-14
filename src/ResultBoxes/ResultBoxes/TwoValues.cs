namespace ResultBoxes;

public record TwoValues<TValue1, TValue2>(TValue1 Value1, TValue2 Value2)
    where TValue1 : notnull where TValue2 : notnull
{
    public static Func<TwoValues<TValue1, TValue2>, ResultBox<TValueResult>>
        ToFunc<TValueResult>(Func<TValue1, TValue2, ResultBox<TValueResult>> valueFunc)
        where TValueResult : notnull
        => values => valueFunc(values.Value1, values.Value2);
    public ThreeValues<TValue1, TValue2, TValue3> Append<TValue3>(TValue3 value3)
        where TValue3 : notnull
        => new(Value1, Value2, value3);
}
public static class TwoValues
{
    public static Func<TwoValues<TValue1, TValue2>, ResultBox<TValueResult>>
        ToFunc<TValue1, TValue2, TValueResult>(
            Func<TValue1, TValue2, ResultBox<TValueResult>> valueFunc)
        where TValue1 : notnull where TValue2 : notnull where TValueResult : notnull
        => values => valueFunc(values.Value1, values.Value2);
}
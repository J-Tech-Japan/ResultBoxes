namespace ResultBoxes;

public static class TwoValueExtensions
{
    public static Func<TValue1, TValue2, ResultBox<TValueResult>>
        ToFunc<TValue1, TValue2, TValueResult>(
            this Func<TwoValues<TValue1, TValue2>, ResultBox<TValueResult>> func)
        where TValue1 : notnull where TValue2 : notnull where TValueResult : notnull
        => (value1, value2) => func(new TwoValues<TValue1, TValue2>(value1, value2));




}

namespace SingleResults;

public static class TwoValueExtensions
{
    public static Func<TValue1, TValue2, SingleValueResult<TValueResult>>
        ToFunc<TValue1, TValue2, TValueResult>(
            this Func<TwoValues<TValue1, TValue2>, SingleValueResult<TValueResult>> func)
        where TValue1 : notnull where TValue2 : notnull where TValueResult : notnull
        => (value1, value2) => func(new TwoValues<TValue1, TValue2>(value1, value2));




}

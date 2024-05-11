namespace SingleResults;

public record TwoValues<TValue1, TValue2>(TValue1 Value1, TValue2 Value2)
    where TValue1 : notnull where TValue2 : notnull
{
}

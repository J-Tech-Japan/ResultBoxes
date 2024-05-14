namespace SingleResults;

public record FourValues<TValue1, TValue2, TValue3, TValue4>(TValue1 Value1, TValue2 Value2, TValue3 Value3, TValue4 Value4)
    where TValue1 : notnull where TValue2 : notnull where TValue3 : notnull where TValue4 : notnull;
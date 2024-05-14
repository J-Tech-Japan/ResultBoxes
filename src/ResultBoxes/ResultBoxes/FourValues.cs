namespace ResultBoxes;

public record FourValues<TValue1, TValue2, TValue3, TValue4>(
    TValue1 Value1,
    TValue2 Value2,
    TValue3 Value3,
    TValue4 Value4)
    where TValue1 : notnull where TValue2 : notnull where TValue3 : notnull where TValue4 : notnull
{
    public FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5> Append<TValue5>(TValue5 value5)
        where TValue5 : notnull
        => new(Value1, Value2, Value3, Value4, value5);
    public ResultBox<TValue5> Call<TValue5>(
        Func<TValue1, TValue2, TValue3, TValue4, ResultBox<TValue5>> addingFunc)
        where TValue5 : notnull
        => addingFunc(Value1, Value2, Value3, Value4);
    public Task<ResultBox<TValue5>> Call<TValue5>(
        Func<TValue1, TValue2, TValue3, TValue4, Task<ResultBox<TValue5>>> addingFunc)
        where TValue5 : notnull
        => addingFunc(Value1, Value2, Value3, Value4);
    public TValue5 Call<TValue5>(Func<TValue1, TValue2, TValue3, TValue4, TValue5> addingFunc)
        where TValue5 : notnull
        => addingFunc(Value1, Value2, Value3, Value4);
    public Task<TValue5> Call<TValue5>(
        Func<TValue1, TValue2, TValue3, TValue4, Task<TValue5>> addingFunc) where TValue5 : notnull
        => addingFunc(Value1, Value2, Value3, Value4);
}

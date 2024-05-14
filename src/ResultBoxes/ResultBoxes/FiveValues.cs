namespace ResultBoxes;

public record FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>(
    TValue1 Value1,
    TValue2 Value2,
    TValue3 Value3,
    TValue4 Value4,
    TValue5 Value5)
    where TValue1 : notnull
    where TValue2 : notnull
    where TValue3 : notnull
    where TValue4 : notnull
    where TValue5 : notnull
{
    public ResultBox<TValue6> Call<TValue6>(
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, ResultBox<TValue6>> addingFunc)
        where TValue6 : notnull
        => addingFunc(Value1, Value2, Value3, Value4, Value5);

    public Task<ResultBox<TValue6>> Call<TValue6>(
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<ResultBox<TValue6>>> addingFunc)
        where TValue6 : notnull
        => addingFunc(Value1, Value2, Value3, Value4, Value5);
    public TValue6 Call<TValue6>(
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6> addingFunc)
        where TValue6 : notnull
        => addingFunc(Value1, Value2, Value3, Value4, Value5);

    public Task<TValue6> Call<TValue6>(
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<TValue6>> addingFunc)
        where TValue6 : notnull
        => addingFunc(Value1, Value2, Value3, Value4, Value5);
}

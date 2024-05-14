namespace ResultBoxes;

public record ThreeValues<TValue1, TValue2, TValue3>(TValue1 Value1, TValue2 Value2, TValue3 Value3)
    where TValue1 : notnull where TValue2 : notnull where TValue3 : notnull
{
    public FourValues<TValue1, TValue2, TValue3, TValue4> Append<TValue4>(TValue4 value4)
        where TValue4 : notnull
        => new(Value1, Value2, Value3, value4);

    public ResultBox<TValue4> Call<TValue4>(
        Func<TValue1, TValue2, TValue3, ResultBox<TValue4>> addingFunc) where TValue4 : notnull
        => addingFunc(Value1, Value2, Value3);
    public Task<ResultBox<TValue4>> Call<TValue4>(
        Func<TValue1, TValue2, TValue3, Task<ResultBox<TValue4>>> addingFunc)
        where TValue4 : notnull
        => addingFunc(Value1, Value2, Value3);
    public TValue4 Call<TValue4>(Func<TValue1, TValue2, TValue3, TValue4> addingFunc)
        where TValue4 : notnull
        => addingFunc(Value1, Value2, Value3);
    public Task<TValue4> Call<TValue4>(Func<TValue1, TValue2, TValue3, Task<TValue4>> addingFunc)
        where TValue4 : notnull
        => addingFunc(Value1, Value2, Value3);
}

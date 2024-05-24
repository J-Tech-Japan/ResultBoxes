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
    public ResultBox<TValue3> Call<TValue3>(Func<TValue1, TValue2, ResultBox<TValue3>> addingFunc)
        where TValue3 : notnull
        => addingFunc(Value1, Value2);
    public void CallAction(Action<TValue1, TValue2> action)
        => action(Value1, Value2);
    public async Task CallAction(Func<TValue1, TValue2, Task> action)
        => await action(Value1, Value2);
    public Task<ResultBox<TValue3>> Call<TValue3>(
        Func<TValue1, TValue2, Task<ResultBox<TValue3>>> addingFunc) where TValue3 : notnull
        => addingFunc(Value1, Value2);
    public Task<TValue3> Call<TValue3>(Func<TValue1, TValue2, Task<TValue3>> addingFunc)
        where TValue3 : notnull
        => addingFunc(Value1, Value2);
    public TValue3 Call<TValue3>(Func<TValue1, TValue2, TValue3> addingFunc) where TValue3 : notnull
        => addingFunc(Value1, Value2);
    public static ResultBox<TwoValues<TValue1, TValue2>> FromResults(
        ResultBox<TValue1> box1,
        ResultBox<TValue2> box2)
        => TwoValues.FromResults(box1, box2);
}
public static class TwoValues
{
    public static Func<TwoValues<TValue1, TValue2>, ResultBox<TValueResult>>
        ToFunc<TValue1, TValue2, TValueResult>(
            Func<TValue1, TValue2, ResultBox<TValueResult>> valueFunc)
        where TValue1 : notnull where TValue2 : notnull where TValueResult : notnull
        => values => valueFunc(values.Value1, values.Value2);

    public static TwoValues<TValue1, TValue2> FromValues<TValue1, TValue2>(
        TValue1 value1,
        TValue2 value2)
        where TValue1 : notnull where TValue2 : notnull
        => new(value1, value2);
    public static ResultBox<TwoValues<TValue1, TValue2>> FromResults<TValue1, TValue2>(
        ResultBox<TValue1> box1,
        ResultBox<TValue2> box2)
        where TValue1 : notnull where TValue2 : notnull
        => box1.IsSuccess switch
        {
            false => ResultBox<TwoValues<TValue1, TValue2>>.Error(box1.GetException()),
            _ => box2.IsSuccess switch
            {
                true => new TwoValues<TValue1, TValue2>(box1.GetValue(), box2.GetValue()),
                _ => ResultBox<TwoValues<TValue1, TValue2>>.Error(box2.GetException())
            }
        };
}

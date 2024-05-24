namespace ResultBoxes;

public record ThreeValues<TValue1, TValue2, TValue3>(TValue1 Value1, TValue2 Value2, TValue3 Value3)
    where TValue1 : notnull where TValue2 : notnull where TValue3 : notnull
{
    public FourValues<TValue1, TValue2, TValue3, TValue4> Append<TValue4>(TValue4 value4)
        where TValue4 : notnull
        => new(Value1, Value2, Value3, value4);
    public ResultBox<ThreeValues<TValue1, TValue2, TValue3>> FromResults(
        ResultBox<TValue1> box1,
        ResultBox<TValue2> box2,
        ResultBox<TValue3> box3)
        => box1.IsSuccess switch
        {
            false => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.Error(box1.GetException()),
            _ => box2.IsSuccess switch
            {
                false => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.Error(
                    box2.GetException()),
                _ => box3.IsSuccess switch
                {
                    true => new ThreeValues<TValue1, TValue2, TValue3>(
                        box1.GetValue(),
                        box2.GetValue(),
                        box3.GetValue()),
                    _ => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.Error(
                        box3.GetException())
                }
            }
        };
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

    public void CallAction(Action<TValue1, TValue2, TValue3> action)
        => action(Value1, Value2, Value3);
    public async Task CallAction(Func<TValue1, TValue2, TValue3, Task> action)
        => await action(Value1, Value2, Value3);
}
public static class ThreeValues
{
    public static ThreeValues<TValue1, TValue2, TValue3> FromValues<TValue1, TValue2, TValue3>(
        TValue1 value1,
        TValue2 value2,
        TValue3 value3)
        where TValue1 : notnull where TValue2 : notnull where TValue3 : notnull
        => new(value1, value2, value3);
    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> FromResults<TValue1, TValue2,
        TValue3>(
        ResultBox<TValue1> box1,
        ResultBox<TValue2> box2,
        ResultBox<TValue3> box3)
        where TValue1 : notnull where TValue2 : notnull where TValue3 : notnull
        => box1.IsSuccess switch
        {
            false => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.Error(box1.GetException()),
            _ => box2.IsSuccess switch
            {
                false => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.Error(
                    box2.GetException()),
                _ => box3.IsSuccess switch
                {
                    true => new ThreeValues<TValue1, TValue2, TValue3>(
                        box1.GetValue(),
                        box2.GetValue(),
                        box3.GetValue()),
                    _ => ResultBox<ThreeValues<TValue1, TValue2, TValue3>>.Error(
                        box3.GetException())
                }
            }
        };
}

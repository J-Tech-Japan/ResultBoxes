namespace ResultBoxes;

public record ReduceResultValue<TValue>(TValue Value, ReduceControlFlow ControlFlow)
{
    public static ReduceResultValue<TValue> Continue(TValue value) => new(value, ReduceControlFlow.Continue);
    public static ReduceResultValue<TValue> Break(TValue value) => new(value, ReduceControlFlow.Break);
}

public static class ReduceResultValue
{
    public static ReduceResultValue<TValue> Continue<TValue>(TValue value) => new(value, ReduceControlFlow.Continue);
    public static ReduceResultValue<TValue> Start<TValue>(TValue value) => new(value, ReduceControlFlow.Continue);
    public static ReduceResultValue<TValue> Break<TValue>(TValue value) => new(value, ReduceControlFlow.Break);
}
namespace ResultBoxes;

public static class ConveyorResultExtensions
{
    public static ResultBox<TValueResult> ConveyorResult<TValue, TValueResult>(
        this ResultBox<TValue> current,
        Func<ResultBox<TValue>, ResultBox<TValueResult>> valueFunc)
        where TValue : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => valueFunc(current),
            _ => new ResultValueNullException()
        };
    public static async Task<ResultBox<TValueResult>> ConveyorResult<TValue, TValueResult>(
        this ResultBox<TValue> current,
        Func<ResultBox<TValue>, Task<ResultBox<TValueResult>>> valueFunc)
        where TValue : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await valueFunc(current),
            _ => new ResultValueNullException()
        };
}

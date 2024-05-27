namespace ResultBoxes;

public static class ConveyorResultTaskExtensions
{

    public static async Task<ResultBox<TValueResult>> ConveyorResult<TValue, TValueResult>(
        this Task<ResultBox<TValue>> currentTask,
        Func<ResultBox<TValue>, ResultBox<TValueResult>> valueFunc)
        where TValue : notnull
        where TValueResult : notnull =>
        (await currentTask) switch
        {
            { IsSuccess: false } current => current.GetException(),
            { IsSuccess: true } current => valueFunc(current),
            _ => new ResultValueNullException()
        };
    public static async Task<ResultBox<TValueResult>> ConveyorResult<TValue, TValueResult>(
        this Task<ResultBox<TValue>> currentTask,
        Func<ResultBox<TValue>, Task<ResultBox<TValueResult>>> valueFunc)
        where TValue : notnull
        where TValueResult : notnull =>
        (await currentTask) switch
        {
            { IsSuccess: false } current => current.GetException(),
            { IsSuccess: true } current => await valueFunc(current),
            _ => new ResultValueNullException()
        };
}
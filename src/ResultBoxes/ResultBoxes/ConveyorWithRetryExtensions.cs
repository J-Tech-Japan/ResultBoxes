namespace ResultBoxes;

public static class ConveyorWithRetryExtensions
{
    public static async Task<ResultBox<TValue>> ConveyorWithRetry<TValue>(
        this ResultBox<TValue> current,
        IRetryPolicy retryPolicy,
        Func<TValue, Task<ResultBox<TValue>>> conveyorFunc)
        where TValue : notnull
        => await current.Conveyor(
            async value => await Retry(value,retryPolicy, conveyorFunc));
    public static async Task<ResultBox<TValue>> ConveyorWithRetry<TValue>(
        this ResultBox<TValue> current,
        IRetryPolicy retryPolicy,
        Func<TValue, ResultBox<TValue>> conveyorFunc)
        where TValue : notnull
        => await current.Conveyor(async value => await Retry(value, retryPolicy, conveyorFunc));
    public static async Task<ResultBox<TValue>> ConveyorWithRetry<TValue>(
        this Task<ResultBox<TValue>> current,
        Func<TValue, Task<ResultBox<TValue>>> conveyorFunc,
        IRetryPolicy retryPolicy)
        where TValue : notnull
        => await current.Conveyor(async value => await Retry(value,retryPolicy, conveyorFunc));

    public static async Task<ResultBox<TValue>> ConveyorWithRetry<TValue>(
        this Task<ResultBox<TValue>> current,
        IRetryPolicy retryPolicy,
        Func<TValue, ResultBox<TValue>> conveyorFunc)
        where TValue : notnull
        => await current.Conveyor(async value => await Retry(value,retryPolicy, conveyorFunc));

    #region private methods 
    private static async Task<ResultBox<TValue>> Retry<TValue>(
        TValue value,
        IRetryPolicy retryPolicy,
        Func<TValue, Task<ResultBox<TValue>>> conveyorFunc)
        where TValue : notnull
    {
        var exceptionCount = 0;
        while (true)
        {
            var result = await conveyorFunc(value);
            if (result.IsSuccess)
            {
                return result;
            }
            exceptionCount++;
            var shouldRetry = await retryPolicy.ShouldRetryWithDelay(
                result.GetException(),
                exceptionCount);
            if (!shouldRetry.IsSuccess || !shouldRetry.GetValue())
            {
                return result.RemapException(retryPolicy.RemapLastException);
            }
        }
    }
    private static async Task<ResultBox<TValue>> Retry<TValue>(
        TValue value,
        IRetryPolicy retryPolicy,
        Func<TValue, ResultBox<TValue>> conveyorFunc)
        where TValue : notnull
        => await Retry(value,retryPolicy, async v => await Task.FromResult(conveyorFunc(v)));
    #endregion
    
}
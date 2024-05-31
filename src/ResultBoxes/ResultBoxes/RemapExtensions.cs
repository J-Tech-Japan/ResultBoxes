using System.Collections.Immutable;
namespace ResultBoxes;

public static class RemapExtensions
{

    public static ResultBox<TValueResult> Remap<TValueOriginal, TValueResult>(
        this ResultBox<TValueOriginal> current,
        Func<TValueOriginal, TValueResult> valueFunc)
        where TValueOriginal : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => valueFunc(current.GetValue()),
            { IsSuccess: false } => current.GetException()
        };

    public static ResultBox<TValueResult> Remap<TValueOriginal1, TValueOriginal2, TValueResult>(
        this ResultBox<TwoValues<TValueOriginal1, TValueOriginal2>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueResult> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => current.GetValue().Call(valueFunc),
            { IsSuccess: false } => current.GetException()
        };

    public static ResultBox<TValueResult> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3,
        TValueResult>(
        this ResultBox<ThreeValues<TValueOriginal1, TValueOriginal2, TValueOriginal3>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueResult> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => current.GetValue().Call(valueFunc),
            { IsSuccess: false } => current.GetException()
        };
    public static ResultBox<TValueResult> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3,
        TValueOriginal4, TValueResult>(
        this ResultBox<FourValues<TValueOriginal1, TValueOriginal2, TValueOriginal3,
            TValueOriginal4>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueResult>
            valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => current.GetValue().Call(valueFunc),
            { IsSuccess: false } => current.GetException()
        };
    public static ResultBox<TValueResult> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3,
        TValueOriginal4, TValueOriginal5, TValueResult>(
        this ResultBox<FiveValues<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4
            , TValueOriginal5>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5,
            TValueResult> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueOriginal5 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => current.GetValue().Call(valueFunc),
            { IsSuccess: false } => current.GetException()
        };


    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal, TValueResult>(
        this ResultBox<TValueOriginal> current,
        Func<TValueOriginal, Task<TValueResult>> valueFunc)
        where TValueOriginal : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await valueFunc(current.GetValue()),
            _ => new ResultValueNullException()
        };

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2,
        TValueResult>(
        this ResultBox<TwoValues<TValueOriginal1, TValueOriginal2>> current,
        Func<TValueOriginal1, TValueOriginal2, Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await current.GetValue().Call(valueFunc),
            _ => new ResultValueNullException()
        };
    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2,
        TValueOriginal3, TValueResult>(
        this ResultBox<ThreeValues<TValueOriginal1, TValueOriginal2, TValueOriginal3>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await current.GetValue().Call(valueFunc),
            _ => new ResultValueNullException()
        };

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2,
        TValueOriginal3, TValueOriginal4, TValueResult>(
        this ResultBox<FourValues<TValueOriginal1, TValueOriginal2, TValueOriginal3,
            TValueOriginal4>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, Task<TValueResult>>
            valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await current.GetValue().Call(valueFunc),
            _ => new ResultValueNullException()
        };

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2,
        TValueOriginal3, TValueOriginal4, TValueOriginal5, TValueResult>(
        this ResultBox<FiveValues<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4
            , TValueOriginal5>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5,
            Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueOriginal5 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await current.GetValue().Call(valueFunc),
            _ => new ResultValueNullException()
        };
}
public interface IRetryPolicy
{
    public Task<ResultBox<bool>> ShouldRetryWithDelay(Exception exception, int exceptionCount);
    public Func<Exception, Exception> RemapLastException { get; }
}
public record RetryPolicy(int MaxRetries, TimeSpan Delay) : IRetryPolicy
{
    protected readonly List<Exception> exceptions  = new();
    public ImmutableList<Exception> Exceptions => exceptions.ToImmutableList();
    public Task<ResultBox<bool>> ShouldRetryWithDelay(Exception exception, int exceptionCount) =>
        exceptionCount < MaxRetries
            ? ResultBox.Start.Scan(_ => Delay == TimeSpan.Zero ? Task.CompletedTask : Task.Delay(Delay))
                .Scan(_ => exceptions.Add(exception))
                .Remap(_ => Task.FromResult(true))
            : ResultBox.Start
                .Scan(_ => exceptions.Add(exception))
                .Remap(_ => Task.FromResult(false));
    public Func<Exception, Exception> RemapLastException { get; init; } = ex => ex;
}

public static class ConveyorWithRetryExtensions
{
    public static async Task<ResultBox<TValue>> ConveyorWithRetry<TValue>(
        this ResultBox<TValue> current,
        Func<TValue, Task<ResultBox<TValue>>> conveyorFunc,
        IRetryPolicy retryPolicy)
        where TValue : notnull
        => await current.Conveyor(
            async value => await Retry(value, conveyorFunc, retryPolicy));
    public static async Task<ResultBox<TValue>> ConveyorWithRetry<TValue>(
        this ResultBox<TValue> current,
        Func<TValue, ResultBox<TValue>> conveyorFunc,
        IRetryPolicy retryPolicy)
        where TValue : notnull
        => await current.Conveyor(async value => await Retry(value, conveyorFunc, retryPolicy));
    public static async Task<ResultBox<TValue>> ConveyorWithRetry<TValue>(
        this Task<ResultBox<TValue>> current,
        Func<TValue, Task<ResultBox<TValue>>> conveyorFunc,
        IRetryPolicy retryPolicy)
        where TValue : notnull
        => await current.Conveyor(async value => await Retry(value, conveyorFunc, retryPolicy));

    public static async Task<ResultBox<TValue>> ConveyorWithRetry<TValue>(
        this Task<ResultBox<TValue>> current,
        Func<TValue, ResultBox<TValue>> conveyorFunc,
        IRetryPolicy retryPolicy)
        where TValue : notnull
        => await current.Conveyor(async value => await Retry(value, conveyorFunc, retryPolicy));

    #region private methods 
    private static async Task<ResultBox<TValue>> Retry<TValue>(
        TValue value,
        Func<TValue, Task<ResultBox<TValue>>> conveyorFunc, IRetryPolicy retryPolicy)
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
        Func<TValue, ResultBox<TValue>> conveyorFunc, IRetryPolicy retryPolicy)
        where TValue : notnull
        => await Retry(value, async v => await Task.FromResult(conveyorFunc(v)), retryPolicy);
    #endregion
    
}
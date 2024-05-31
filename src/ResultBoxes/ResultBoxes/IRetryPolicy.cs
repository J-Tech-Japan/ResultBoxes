namespace ResultBoxes;

public interface IRetryPolicy
{
    public Task<ResultBox<bool>> ShouldRetryWithDelay(Exception exception, int exceptionCount);
    public Func<Exception, Exception> RemapLastException { get; }
}
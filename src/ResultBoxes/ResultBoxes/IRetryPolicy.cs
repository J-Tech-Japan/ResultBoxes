namespace ResultBoxes;

public interface IRetryPolicy
{
    public Func<Exception, Exception> RemapLastException { get; }
    public Task<ResultBox<bool>> ShouldRetryWithDelay(Exception exception, int exceptionCount);
}

using System.Collections.Immutable;
namespace ResultBoxes;

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
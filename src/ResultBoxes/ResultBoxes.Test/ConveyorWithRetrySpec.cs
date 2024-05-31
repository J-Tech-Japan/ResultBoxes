using Xunit.Abstractions;
namespace ResultBoxes.Test;

public class ConveyorWithRetrySpec(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task ConveyorWithRetryTest1()
    {
        var result = await ResultBox.FromValue(1)
            .ConveyorWithRetry(new RetryPolicy(3, TimeSpan.Zero),
                async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine($"{i} can not use");
                return new ApplicationException($"can not use {i}");
            });
        
        Assert.False(result.IsSuccess); 
    }
    [Fact]
    public async Task ConveyorWithRetryTest2()
    {
        var result = await ResultBox.FromValue(1)
            .ConveyorWithRetry(
                new RetryPolicy(3, TimeSpan.FromSeconds(1)),async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine($"{i} can not use");
                return new ApplicationException($"can not use {i}");
            });
        
        Assert.False(result.IsSuccess); 
    }

    [Fact]
    public async Task ConveyorWithRetryTest3()
    {
        var localCount = 0;
        var result = await ResultBox.FromValue(1)
            .ConveyorWithRetry(
                new RetryPolicy(5, TimeSpan.FromMilliseconds(100)),
                async i =>
            {
                localCount++;
                await Task.CompletedTask;
                testOutputHelper.WriteLine($"{i} can not use");
                return localCount < 2 ? new ApplicationException($"can not use {i}, {localCount}") : localCount;
            });
        
        Assert.True(result.IsSuccess); 
        Assert.Equal(2, result.GetValue()); 
    }

    [Fact]
    public async Task ConveyorWithRetryTest4()
    {
        var localCount = 0;
        var result = await ResultBox.FromValue(1)
            .ConveyorWithRetry(
                new RetryPolicy(5, TimeSpan.FromMilliseconds(100)),
                i =>
                // ReSharper disable once AccessToModifiedClosure
                ResultBox.FromValue(i)
                    .Scan(_ => localCount++)
                    .Combine(ResultBox.FromValue(localCount))
                    .Scan(values => testOutputHelper.WriteLine($"{values.Value1} can not use"))
                    .Conveyor(values => values.Value2 < 2 ? ResultBox<int>.FromException(new ApplicationException($"can not use {values.Value1}, {values.Value2}")) : values.Value2)
            );
        
        Assert.True(result.IsSuccess); 
        Assert.Equal(2, result.GetValue()); 
    }

}

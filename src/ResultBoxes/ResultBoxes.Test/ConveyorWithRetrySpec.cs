using Xunit.Abstractions;
namespace ResultBoxes.Test;

public class ConveyorWithRetrySpec(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task ConveyorWithRetryTest1()
    {
        var result = await ResultBox.FromValue(1)
            .ConveyorWithRetry(async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine($"{i} can not use");
                return new ApplicationException($"can not use {i}");
            }, new RetryPolicy(3, TimeSpan.Zero));
        
        Assert.False(result.IsSuccess); 
    }
    [Fact]
    public async Task ConveyorWithRetryTest2()
    {
        var result = await ResultBox.FromValue(1)
            .ConveyorWithRetry(async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine($"{i} can not use");
                return new ApplicationException($"can not use {i}");
            }, new RetryPolicy(3, TimeSpan.FromSeconds(1)));
        
        Assert.False(result.IsSuccess); 
    }

    [Fact]
    public async Task ConveyorWithRetryTest3()
    {
        var localCount = 0;
        var result = await ResultBox.FromValue(1)
            .ConveyorWithRetry(async i =>
            {
                localCount++;
                await Task.CompletedTask;
                testOutputHelper.WriteLine($"{i} can not use");
                return localCount < 2 ? new ApplicationException($"can not use {i}, {localCount}") : localCount;
            }, new RetryPolicy(5, TimeSpan.FromMilliseconds(100)));
        
        Assert.True(result.IsSuccess); 
        Assert.Equal(2, result.GetValue()); 
    }

}

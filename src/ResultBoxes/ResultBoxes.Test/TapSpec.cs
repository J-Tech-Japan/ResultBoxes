using ResultBoxes;
namespace SngleResults.Test;

public class TapSpec
{
    [Fact]
    public void CanTapResult()
    {
        var result = ResultBox.FromValue(100);
        var result2 = result.Tap(Console.WriteLine);
        Assert.Equal(result, result2);
    }

    [Fact]
    public void CanTapResult2()
    {
        var result = ResultBox.FromValue(TwoValues.FromValues(100, 2));
        var result2 = result.Tap((value1, value2) => Console.WriteLine($"{value1} {value2}"));
        Assert.Equal(result, result2);
    }
    
    
    [Fact]
    public async Task CanTapResultSyncAndAsync()
    {
        var result = ResultBox.FromValue(100);
        var result2 = await result.Tap(async (x) => await Task.Run(() => Console.WriteLine(x)));
        Assert.Equal(result, result2);
    }

    [Fact]
    public async Task CanTapResult2SyncAndAsync()
    {
        var result = ResultBox.FromValue(TwoValues.FromValues(100, 2));
        var result2 = await result.Tap(async (x,y) => await Task.Run(() => Console.WriteLine($"{x} {y}")));
        Assert.Equal(result, result2);
    }

    [Fact]
    public async Task CanTapResultAsyncAndSync()
    {
        var result = await ResultBox.FromValue(Task.FromResult(100)).Tap(Console.WriteLine);
        Assert.Equal(100, result.GetValue());
    }
    [Fact]
    public async Task CanTapResult2AsyncAndSync()
    {
        var result = await ResultBox.FromValue(Task.FromResult( TwoValues.FromValues(100, 2))).Tap((value1, value2) => Console.WriteLine($"{value1} {value2}"));
        Assert.Equal(TwoValues.FromValues(100,2), result.GetValue());
    }
    
    [Fact]
    public async Task CanTapResultAsyncAndAsync()
    {
        var result = await ResultBox.FromValue(Task.FromResult(100)).Tap(async (x) => await Task.Run(() => Console.WriteLine(x)));
        Assert.Equal(100, result.GetValue());
    }
    [Fact]
    public async Task CanTapResult2AsyncAndAsync()
    {
        var result = await ResultBox.FromValue(Task.FromResult(TwoValues.FromValues(100, 2))).Tap(async (x) => await Task.Run(() => Console.WriteLine(x)));
        Assert.Equal( TwoValues.FromValues(100, 2), result.GetValue());
    }
    
}

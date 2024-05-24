namespace ResultBoxes.Test;

public class ScanSpec
{
    [Fact]
    public void CanScanResult()
    {
        var result = ResultBox.FromValue(100);
        var result2 = result.Scan(Console.WriteLine);
        Assert.Equal(result, result2);
    }
    [Fact]
    public void CanScanResultError()
    {
        var result = ResultBox<int>.FromException(new ApplicationException("Error"));
        var result2 = result.Scan(Console.WriteLine, Console.WriteLine);
        Assert.Equal(result, result2);
    }

    [Fact]
    public void CanScanResult2()
    {
        var result = ResultBox.FromValue(TwoValues.FromValues(100, 2));
        var result2 = result.Scan((value1, value2) => Console.WriteLine($"{value1} {value2}"));
        Assert.Equal(result, result2);
    }


    [Fact]
    public async Task CanScanResultSyncAndAsync()
    {
        var result = ResultBox.FromValue(100);
        var result2 = await result.Scan(async x => await Task.Run(() => Console.WriteLine(x)));
        Assert.Equal(result, result2);
    }

    [Fact]
    public async Task CanScanResult2SyncAndAsync()
    {
        var result = ResultBox.FromValue(TwoValues.FromValues(100, 2));
        var result2 = await result.Scan(
            async (x, y) => await Task.Run(() => Console.WriteLine($"{x} {y}")));
        Assert.Equal(result, result2);
    }

    [Fact]
    public async Task CanScanResultAsyncAndSync()
    {
        var result = await ResultBox.FromValue(Task.FromResult(100)).Scan(Console.WriteLine);
        Assert.Equal(100, result.GetValue());
    }
    [Fact]
    public async Task CanScanResult2AsyncAndSync()
    {
        var result = await ResultBox.FromValue(Task.FromResult(TwoValues.FromValues(100, 2)))
            .Scan((value1, value2) => Console.WriteLine($"{value1} {value2}"));
        Assert.Equal(TwoValues.FromValues(100, 2), result.GetValue());
    }

    [Fact]
    public async Task CanScanResultAsyncAndAsync()
    {
        var result = await ResultBox.FromValue(Task.FromResult(100))
            .Scan(async x => await Task.Run(() => Console.WriteLine(x)));
        Assert.Equal(100, result.GetValue());
    }
    [Fact]
    public async Task CanScanResult2AsyncAndAsync()
    {
        var result = await ResultBox.FromValue(Task.FromResult(TwoValues.FromValues(100, 2)))
            .Scan(async x => await Task.Run(() => Console.WriteLine(x)));
        Assert.Equal(TwoValues.FromValues(100, 2), result.GetValue());
    }
}

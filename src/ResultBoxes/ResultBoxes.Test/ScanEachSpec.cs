using Xunit.Abstractions;
namespace ResultBoxes.Test;

public class ScanEachSpec(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ScanEachTest1()
    {
        var result = ResultBox.FromValue(Enumerable.Range(0, 10).ToList())
            .ScanEach(i =>
            {
                testOutputHelper.WriteLine(i.ToString());
                return ScanEachControlFlow.Continue;
            });
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public void ScanEachTest2()
    {
        var result = ResultBox.FromValue(Enumerable.Range(0, 10).ToList())
            .ScanEach(i =>
            {
                testOutputHelper.WriteLine(i.ToString());
                return i < 5 ? ScanEachControlFlow.Continue : ScanEachControlFlow.Break;
            });
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public void ScanEachTest3()
    {
        var result = ResultBox.FromValue(Enumerable.Range(0, 10).ToList())
            .ScanEach(i => testOutputHelper.WriteLine(i.ToString()));
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public async Task ScanEachTest1Async()
    {
        var result = await ResultBox.FromValue(Enumerable.Range(0, 10).ToList())
            .ScanEach(async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine(i.ToString());
            });
        Assert.True(result.IsSuccess);
        Assert.IsType<List<int>>(result.GetValue());
    }

    [Fact]
    public async Task ScanEachTest2Async()
    {
        var result = await ResultBox.FromValue(Enumerable.Range(0, 10).ToList())
            .ScanEach(async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine(i.ToString());
                return i < 5 ? ScanEachControlFlow.Continue : ScanEachControlFlow.Break;
            });
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ScanEachTest3Async()
    {
        var result = await ResultBox.FromValue(Task.FromResult(Enumerable.Range(0, 10).ToList()))
            .ScanEach(async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine(i.ToString());
            });
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ScanEachTest4Async()
    {
        var result = await ResultBox.FromValue(Task.FromResult(Enumerable.Range(0, 10).ToList()))
            .ScanEach(async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine(i.ToString());
                return i < 5 ? ScanEachControlFlow.Continue : ScanEachControlFlow.Break;
            });
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ScanEachTest5Async()
    {
        var result = await ResultBox.FromValue(Task.FromResult(Enumerable.Range(0, 10).ToList()))
            .ScanEach(async i =>
            {
                await Task.CompletedTask;
                testOutputHelper.WriteLine(i.ToString());
            });
        Assert.True(result.IsSuccess);
    }

}


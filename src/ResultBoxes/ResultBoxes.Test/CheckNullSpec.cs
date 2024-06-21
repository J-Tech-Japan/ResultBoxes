namespace ResultBoxes.Test;

public class CheckNullSpec
{
    [Fact]
    public void CheckNullTest1()
    {
        var result = ResultBox.CheckNull((string?)"test");
        
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public void CheckNullTest2()
    {
        var result = ResultBox.CheckNull((string?)null);
        
        Assert.False(result.IsSuccess);
        Assert.True(result.GetException() is ResultValueNullException);
    }
    [Fact]
    public void CheckNullTest3()
    {
        var result = ResultBox.CheckNull((string?)null, new ApplicationException("test"));
        
        Assert.False(result.IsSuccess);
        Assert.True(result.GetException() is ApplicationException);
    }
    
    [Fact]
    public void CheckNullTest4()
    {
        var result = ResultBox.CheckNull(() =>(string?)"test");
        
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CheckNullTest5()
    {
        var result = await ResultBox.CheckNull(() =>Task.FromResult((string?)"test"));
        
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public async Task CheckNullWrapTryTest1()
    {
        await Task.CompletedTask;
        var result = ResultBox.CheckNullWrapTry(() =>(string?)"test");
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public async Task CheckNullWrapTryTest2()
    {
        await Task.CompletedTask;
        var result = await ResultBox.CheckNullWrapTry(() =>Task.FromResult((string?)"test"));
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public async Task CheckNullWrapTryTest3()
    {
        await Task.CompletedTask;
        var result = ResultBox.CheckNullWrapTry(() =>(string?)null);
        Assert.False(result.IsSuccess);
    }
    [Fact]
    public async Task CheckNullWrapTryTest4()
    {
        await Task.CompletedTask;
        var result = await ResultBox.CheckNullWrapTry(() =>Task.FromResult((string?)null));
        Assert.False(result.IsSuccess);
    }
    [Fact]
    public async Task CheckNullWrapTryTest5()
    {
        await Task.CompletedTask;
        var result = ResultBox.CheckNullWrapTry(() => ThrowException(true));
        Assert.False(result.IsSuccess);
    }
    private string? ThrowException(bool whenTrue) => whenTrue ? throw new ApplicationException("test") : "test";
    [Fact]
    public async Task CheckNullWrapTryTest6()
    {
        await Task.CompletedTask;
        var result = await ResultBox.CheckNullWrapTry(() =>Task.FromResult(ThrowException(true)));
        Assert.False(result.IsSuccess);
    }

}

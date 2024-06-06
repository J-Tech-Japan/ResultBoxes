namespace ResultBoxes.Test;

public class CombineWrapTrySpec
{
    [Fact]
    public void CombineWrapTryTest1()
    {
        var result = ResultBox<int>.FromValue(1)
            .CombineWrapTry(value => value + 1);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }
    [Fact]
    public void CombineWrapTryTest2()
    {
        var result = ResultBox<int>.FromValue(1)
            .CombineWrapTry(() => 2);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }
    [Fact]
    public async Task CombineWrapTryAsyncTest1()
    {
        var result = await ResultBox<int>.FromValue(1)
            .CombineWrapTry(value => Task.FromResult(value + 1));
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }
    [Fact]
    public async Task CombineWrapTryAsyncTest2()
    {
        var result = await ResultBox<int>.FromValue(1)
            .CombineWrapTry(() => Task.FromResult(2));
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }



    [Fact]
    public async Task CombineWrapTryTest3()
    {
        var result = await ResultBox<int>.FromValue(Task.FromResult(1))
            .CombineWrapTry(value => value + 1);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }
    [Fact]
    public async Task CombineWrapTryTest4()
    {
        var result = await ResultBox<int>.FromValue(Task.FromResult(1))
            .CombineWrapTry(() => 2);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }
    [Fact]
    public async Task CombineWrapTryAsyncTest3()
    {
        var result = await ResultBox<int>.FromValue(Task.FromResult(1))
            .CombineWrapTry(value => Task.FromResult(value + 1));
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }
    [Fact]
    public async Task CombineWrapTryAsyncTest4()
    {
        var result = await ResultBox<int>.FromValue(Task.FromResult(1))
            .CombineWrapTry(() => Task.FromResult(2));
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }
    
    [Fact]
    public void CombineWrapTryTestThrowAndRemap()
    {
        var result = ResultBox<int>.FromValue(1)
            .CombineWrapTry(value => value / (value - 1), exception => new ApplicationException("can not divide by 0"));
        Assert.False(result.IsSuccess);
        Assert.IsType<ApplicationException>(result.GetException());
    }
    [Fact]
    public void CombineWrapTryTestThrowAndRemap2()
    {
        var result = ResultBox<int>.FromValue(1)
            .CombineWrapTry(value => value / (value - 1));
        Assert.False(result.IsSuccess);
        var exception = result.GetException();
        Assert.IsType<DivideByZeroException>(exception);
    }

}

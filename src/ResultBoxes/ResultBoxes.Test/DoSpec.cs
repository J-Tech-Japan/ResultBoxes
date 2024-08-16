namespace ResultBoxes.Test;

public class DoSpec
{
    [Fact]
    public void DoTest1()
    {
        var result = ResultBox.Start.Do(() => 1 + 1);
        Assert.True(result.IsSuccess);
        Assert.Equal(UnitValue.Unit, result.GetValue());
    }
    
    [Fact]
    public async Task DoTest2()
    {
        var result = await ResultBox.FromValue(1).Do(async (i) => await Task.FromResult( i + 1));
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue());
    }
    [Fact]
    public void DoTest3()
    {
        var result = ResultBox.FromValue(1).Do((i) => i + 1);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue());
    }
    
    [Fact]
    public async Task DoTest4()
    {
        var result = await ResultBox.Start.Do(async () => await Task.FromResult( 1 + 1));
        Assert.True(result.IsSuccess);
        Assert.Equal(UnitValue.Unit, result.GetValue());
    }

    [Fact]
    public void DoWrapTryTest1()
    {
        var result = ResultBox.Start.DoWrapTry(() => 1 + 1);
        Assert.True(result.IsSuccess);
        Assert.Equal(UnitValue.Unit, result.GetValue());
    }
    
    [Fact]
    public async Task DoWrapTryTest2()
    {
        var result = await ResultBox.Start.DoWrapTry(async () => await Task.FromResult( 1 + 1));
        Assert.True(result.IsSuccess);
        Assert.Equal(UnitValue.Unit, result.GetValue());
    }

    [Fact]
    public void DoWrapTryTest3()
    {
        var result = ResultBox.Start.DoWrapTry(() => ThrowableFunction(0));
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public async Task DoWrapTryTest4()
    {
        var result = await ResultBox.Start.DoWrapTry(async () => await Task.FromResult( ThrowableFunction(0)));
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void DoWrapTryTest5()
    {
        var result = ResultBox.Start.DoWrapTry(() => ThrowableFunction(1));
        Assert.True(result.IsSuccess);
        Assert.Equal(UnitValue.Unit, result.GetValue());
    }
    
    [Fact]
    public async Task DoWrapTryTest6()
    {
        var result = await ResultBox.Start.DoWrapTry(async () => await Task.FromResult( ThrowableFunction(1)));
        Assert.True(result.IsSuccess);
        Assert.Equal(UnitValue.Unit, result.GetValue());
    }

    [Fact]
    public async Task DoAfterSpec()
    {
        var sut = await GetAdditionAsync(1, 3)
            .Do(async () =>
            {
                await Task.CompletedTask;
                Console.WriteLine("done");
            });
        Assert.True(sut.IsSuccess);
        Assert.Equal(4, sut.GetValue());
    }
    
    private Task<ResultBox<int>> GetAdditionAsync(int i, int j)
    {
        return Task.FromResult(ResultBox.FromValue(i + j));
    }

    private int ThrowableFunction(int i)
    {
        if (i == 0)
            throw new Exception("Error");
        return i + 1;
    }

}

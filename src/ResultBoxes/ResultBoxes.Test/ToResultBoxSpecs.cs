namespace ResultBoxes.Test;

public class ToResultBoxSpecs
{
    [Fact]
    public void ToResultBoxFromPrimitive()
    {
        var result = 1.ToResultBox();
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue());
    }
    [Fact]
    public void ToResultBoxFromString()
    {
        var result = "Hello".ToResultBox();
        Assert.True(result.IsSuccess);
        Assert.Equal("Hello", result.GetValue());
    }
    [Fact]
    public async Task ToResultBoxFromPrimitiveTask()
    {
        var result = Task.FromResult(1).ToResultBox();
        Assert.True((await result).IsSuccess);
        Assert.Equal(1, (await result).GetValue());
    }
    [Fact]
    public async Task ToResultBoxFromStringTask()
    {
        var result = Task.FromResult("Hello").ToResultBox();
        Assert.True((await result).IsSuccess);
        Assert.Equal("Hello", (await result).GetValue());
    }
    [Fact]
    public void ToResultBoxFromException()
    {
        var exception = new ApplicationException("Can not execute");
        var result = exception.ToResultBoxFromException<int>();
        Assert.False(result.IsSuccess);
        Assert.Equal(exception, result.GetException());
    }
}

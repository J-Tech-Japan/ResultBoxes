namespace ResultBoxes.Test;

public class FromValueTest
{
    [Fact]
    public void Test()
    {
        var value = 1;
        var result = ResultBox.FromValue(value);
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    [Fact]
    public async Task TestAsync()
    {
        var value = 1;
        var result = await ResultBox.FromValue(Task.FromResult(value));
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    [Fact]
    public async Task TestAsyncFunc()
    {
        var value = 1;
        var result = await ResultBox.FromValue(() => Task.FromResult(value));
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
}

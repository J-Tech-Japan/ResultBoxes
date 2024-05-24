using ResultBoxes;
namespace SngleResults.Test;

public class CombineTaskSpec
{
    [Fact]
    public async Task CombineTwoValuesTaskTest()
    {
        var result = await Task.FromResult(ResultBox.FromValue("Value1"))
            .Combine((value1) => Task.FromResult(ResultBox.FromValue(value1 + "Value2")));

        Assert.True(result.IsSuccess);
        Assert.Equal("Value1", result.GetValue().Value1);
        Assert.Equal("Value1Value2", result.GetValue().Value2);
    }
    [Fact]
    public async Task CombineTwoValuesTest()
    {
        var result = await Task.FromResult(ResultBox.FromValue("Value1"))
            .Combine((value1) => ResultBox.FromValue(value1 + "Value2"));

        Assert.True(result.IsSuccess);
        Assert.Equal("Value1", result.GetValue().Value1);
        Assert.Equal("Value1Value2", result.GetValue().Value2);
    }
    
    [Fact]
    public async Task CombineTwoValuesTaskNotPassingTest()
    {
        var result = await Task.FromResult(ResultBox.FromValue("Value1"))
            .Combine(() => Task.FromResult(ResultBox.FromValue("Value2")));

        Assert.True(result.IsSuccess);
        Assert.Equal("Value1", result.GetValue().Value1);
        Assert.Equal("Value2", result.GetValue().Value2);
    }
    [Fact]
    public async Task CombineTwoValuesNotPassingTest()
    {
        var result = await Task.FromResult(ResultBox.Ok("Value1"))
            .Combine(() => ResultBox.Ok("Value2"));

        Assert.True(result.IsSuccess);
        Assert.Equal("Value1", result.GetValue().Value1);
        Assert.Equal("Value2", result.GetValue().Value2);
    }

}

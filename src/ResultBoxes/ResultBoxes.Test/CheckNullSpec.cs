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

}

namespace ResultBoxes.Test;

public class ToDtoSpec
{
    [Fact]
    public void TestDto1()
    {
        var dto = ResultBox.Start.ToDto();
        
        Assert.True(dto.IsSuccess);
    }
    [Fact]
    public void TestDto2()
    {
        var dto = ResultBox.FromValue(2).Verify(_ => new ApplicationException("test")).ToDto();
        
        Assert.False(dto.IsSuccess);
        Assert.Equal("test", dto.Exception?.Message);
    }
    
    
    [Fact]
    public async Task TestDto3()
    {
        var dto = await ResultBox.Start.ToTask().ToDto();
        
        Assert.True(dto.IsSuccess);
    }
    [Fact]
    public async Task TestDto4()
    {
        var dto = await ResultBox.FromValue(2).Verify(_ => new ApplicationException("test")).ToTask().ToDto();
        
        Assert.False(dto.IsSuccess);
        Assert.Equal("test", dto.Exception?.Message);
    }

}

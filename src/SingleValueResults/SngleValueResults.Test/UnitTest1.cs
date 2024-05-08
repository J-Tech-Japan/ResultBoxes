using SingleResults.Usage;
namespace SngleResults.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }

    [Fact]
    public async Task RailwayAsyncSpec()
    {
        var sut = await FunctionDeclarations.RailwayAsync(1);

        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.Value);
    }
    [Fact]
    public async Task Railway2AsyncSpec()
    {
        var sut = await FunctionDeclarations.Railway2Async(1);

        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.Value);
    }
    [Fact]
    public void RailwaySpec()
    {
        var sut = FunctionDeclarations.RailwayInstance(1);

        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.Value);
    }
    [Fact]
    public void RailwayInstance2Spec()
    {
        var sut = FunctionDeclarations.RailwayInstance2(1);

        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.Value);
    }
    [Fact]
    public void RailwayInstance2WithErrorSpec()
    {
        var sut = FunctionDeclarations.RailwayInstance2(4);

        Assert.False(sut.IsSuccess);
        Assert.True(sut.Exception is ApplicationException);
    }
    [Fact]
    public void RailwayInstance2WithError2Spec()
    {
        var sut = FunctionDeclarations.RailwayInstance2(3);

        Assert.False(sut.IsSuccess);
        Assert.True(sut.Exception is ApplicationException);
    }

    [Fact]
    public void Railway2Calc3Spec()
    {
        var sut = FunctionDeclarations.Railway2Calc3(9, 2, 3);
        Assert.True(sut.IsSuccess);
        Assert.Equal(2, sut.Value);
    }
    [Fact]
    public async Task Railway2Calc3AsyncSpec()
    {
        var sut = await FunctionDeclarations.RailwayCalc3Async(9, 2, 3);
        Assert.True(sut.IsSuccess);
        Assert.Equal(2, sut.Value);
    }
    [Fact]
    public async Task RailwayCalc3Async2Spec()
    {
        var sut = await FunctionDeclarations.RailwayCalc3Async2(9, 2, 3);
        Assert.True(sut.IsSuccess);
        Assert.Equal(2, sut.Value);
    }
    [Fact]
    public void Railway2Calc4Spec()
    {
        var sut = FunctionDeclarations.Railway2Calc4(9, 2, 3);
        Assert.True(sut.IsSuccess);
        Assert.Equal(2, sut.Value);
    }
    [Fact]
    public void Railway2Calc4SpecThrowing()
    {
        var sut = FunctionDeclarations.Railway2Calc4(9, 101, 3);
        Assert.False(sut.IsSuccess);
        Assert.True(sut.Exception is ApplicationException);
    }
    [Fact]
    public async Task Railway3AsyncSpec()
    {
        var sut = await FunctionDeclarations.Railway3Async(1);
        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.Value);
    }
}

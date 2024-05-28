using SingleResults.Usage;
namespace ResultBoxes.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }

    [Fact]
    public async Task RailroadAsyncSpec()
    {
        var sut = await FunctionDeclarations.RailroadWithAsync(1);

        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.GetValue());
    }
    [Fact]
    public async Task Railroad2AsyncSpec()
    {
        var sut = await FunctionDeclarations.Railroad2Async(1);

        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.GetValue());
    }
    [Fact]
    public void RailroadSpec()
    {
        var sut = FunctionDeclarations.RailroadInstance(1);

        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.GetValue());
    }
    [Fact]
    public void RailroadInstance2Spec()
    {
        var sut = FunctionDeclarations.RailroadInstance2(1);

        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.GetValue());
    }
    [Fact]
    public void RailroadInstance2WithErrorSpec()
    {
        var sut = FunctionDeclarations.RailroadInstance2(4);

        Assert.False(sut.IsSuccess);
        Assert.True(sut.Exception is ApplicationException);
    }
    [Fact]
    public void RailroadInstance2WithError2Spec()
    {
        var sut = FunctionDeclarations.RailroadInstance2(3);

        Assert.False(sut.IsSuccess);
        Assert.True(sut.Exception is ApplicationException);
    }

    [Fact]
    public void Railroad2Calc3Spec()
    {
        var sut = FunctionDeclarations.Railroad2Calc3(9, 2, 3);
        Assert.True(sut.IsSuccess);
        Assert.Equal(2, sut.GetValue());
    }
    [Fact]
    public async Task Railroad2Calc3AsyncSpec()
    {
        var sut = await FunctionDeclarations.RailroadCalc3Async(9, 2, 3);
        Assert.True(sut.IsSuccess);
        Assert.Equal(2, sut.GetValue());
    }
    [Fact]
    public async Task RailroadCalc3Async2Spec()
    {
        var sut = await FunctionDeclarations.RailroadCalc3Async2(9, 2, 3);
        Assert.True(sut.IsSuccess);
        Assert.Equal(2, sut.GetValue());
    }
    [Fact]
    public void Railroad2Calc4Spec()
    {
        var sut = FunctionDeclarations.Railroad2Calc4(9, 2, 3);
        Assert.True(sut.IsSuccess);
        Assert.Equal(2, sut.GetValue());
    }
    [Fact]
    public void Railroad2Calc4SpecThrowing()
    {
        var sut = FunctionDeclarations.Railroad2Calc4(9, 101, 3);
        Assert.False(sut.IsSuccess);
        Assert.True(sut.Exception is ApplicationException);
    }
    [Fact]
    public async Task Railroad3AsyncSpec()
    {
        var sut = await FunctionDeclarations.Railroad3Async(1);
        Assert.True(sut.IsSuccess);
        Assert.Equal(12, sut.GetValue());
    }
}

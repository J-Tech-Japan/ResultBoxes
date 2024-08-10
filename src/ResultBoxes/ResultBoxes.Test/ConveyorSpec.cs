using Xunit.Abstractions;

namespace ResultBoxes.Test;

public class ConveyorSpec()
{
    [Fact]
    public void ConveyorWithNoParam()
    {
        var sut = ResultBox.Start
            .Conveyor(() => ResultBox.FromValue("Hello"));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task ConveyorWithNoParamTask1()
    {
        var sut = await ResultBox.Start
            .Conveyor(() => ResultBox.FromValue(Task.FromResult("Hello")));
        Assert.True(sut.IsSuccess);
    }
    
    [Fact]
    public  async Task  ConveyorWithNoParamTask2()
    {
        var sut = await ResultBox.Start.ToTask()
            .Conveyor(() => ResultBox.FromValue("Hello"));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task ConveyorWithNoParamTask3()
    {
        var sut = await ResultBox.Start.ToTask()
            .Conveyor(() => ResultBox.FromValue(Task.FromResult("Hello")));
        Assert.True(sut.IsSuccess);
    }

}
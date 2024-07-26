using Xunit.Abstractions;

namespace ResultBoxes.Test;

public class ConveyorSpec(ITestOutputHelper testOutputHelper)
{


    [Fact]
    public void ConveyorWithNoParam()
    {
        var sut = ResultBox.Start
            .Conveyor(() => ResultBox.FromValue("Hello"));
        Assert.True(sut.IsSuccess);
    }
}
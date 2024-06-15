namespace ResultBoxes.Test;

public class ToTaskSpec
{
    [Fact]
    public async Task TestTask()
    {
        var taskResult = await ResultBox.Start.ToTask();
        
        Assert.True(taskResult.IsSuccess);
    }
}

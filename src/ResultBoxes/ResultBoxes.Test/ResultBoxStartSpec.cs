namespace ResultBoxes.Test;

public class ResultBoxStartSpec
{
    [Fact]
    public void UseResultBoxStart()
    {
        var result = ResultBox.Start;
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void UseResultBoxStart2()
    {
        var result = ResultBox.Start
            .Conveyor(_ => ResultBox.Ok(1));
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue());
    }

}

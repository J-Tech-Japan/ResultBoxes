namespace ResultBoxes.Test;

public class MatchSpec
{
    [Fact]
    public void MatchTest1()
    {
        var result = ResultBox.FromValue(2)
            .Match(value => value.ToString(), exception => exception.Message);
        Assert.Equal("2", result);
    }

    [Fact]
    public async Task MatchTest2()
    {
        var result = await ResultBox.FromValue(2).ToTask()
            .Match(value => value.ToString(), exception => exception.Message);
        Assert.Equal("2", result);
    }

    
    [Fact]
    public void MatchTest3()
    {
        var result = ResultBox.FromException<int>(new ApplicationException("err 2"))
            .Match(value => value.ToString(), exception => exception.Message);
        Assert.Equal("err 2", result);
    }

    [Fact]
    public async Task MatchTest4()
    {
        var result = await ResultBox.FromException<int>(new ApplicationException("err 2")).ToTask()
            .Match(value => value.ToString(), exception => exception.Message);
        Assert.Equal("err 2", result);
    }

}

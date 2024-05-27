namespace ResultBoxes.Test;

public class FromResultsSpec
{
    [Fact]
    public void FromResultsTest()
    {
        var result1 = ResultBox.FromValue(1);
        var result2 = ResultBox.FromValue(2);

        var result = TwoValues.FromResults(result1, result2);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.GetValue().Value1);
        Assert.Equal(2, result.GetValue().Value2);
    }

    [Fact]
    public void FromResultsTestWithError()
    {
        var result1 = ResultBox<int>.Error(new ApplicationException("result 1 failed"));
        var result2 = ResultBox.FromValue(2);

        var result = TwoValues.FromResults(result1, result2);

        Assert.False(result.IsSuccess);
        Assert.True(result.GetException() is ApplicationException);
        Assert.Equal("result 1 failed", result.GetException().Message);
    }
    [Fact]
    public void FromResultsTestWithError2()
    {
        var result1 = ResultBox.FromValue(1);
        var result2 = ResultBox<int>.Error(new ApplicationException("result 2 failed"));

        var result = TwoValues.FromResults(result1, result2);

        Assert.False(result.IsSuccess);
        Assert.True(result.GetException() is ApplicationException);
        Assert.Equal("result 2 failed", result.GetException().Message);
    }
}

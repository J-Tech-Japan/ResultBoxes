using SingleResults.Usage;
namespace ResultBoxes.Test;

public class ThreeValueTest
{
    [Fact]
    public void ThreeValueCaseSpec()
    {
        // ((8+1) + (2+1)) / (3+1) = 3 
        var sut = ThreeValueCase.Calc3Value(8, 2, 3);

        Assert.True(sut.IsSuccess);
        Assert.Equal(3, sut.GetValue());
    }
    [Fact]
    public async Task ThreeValueCaseAsyncSpec()
    {
        // ((8+1) + (2+1)) / (3+1) = 3 
        var sut = await ThreeValueCase.Calc3ValueAsync(8, 2, 3);

        Assert.True(sut.IsSuccess);
        Assert.Equal(3, sut.GetValue());
    }
}

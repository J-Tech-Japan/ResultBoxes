using ResultBoxes;
namespace SngleResults.Test;

public class IsSuccessSpec
{
    [Fact]
    public void IsSuccess_ReturnsTrue_WhenResultIsSuccess()
    {
        // Arrange
        var result = ResultBox<string>.FromValue("Success");

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void IsSuccess_ReturnsFalse_WhenValueAndExceptionAreNull()
    {
        // Arrange
        var result = ResultBox<string>.FromException(null);

        // Assert
        Assert.False(result.IsSuccess);
    }

}

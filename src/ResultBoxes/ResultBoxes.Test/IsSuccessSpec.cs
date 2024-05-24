namespace ResultBoxes.Test;

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
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var result = ResultBox<string>.FromException(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Assert
        Assert.False(result.IsSuccess);
    }

}

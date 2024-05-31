namespace ResultBoxes.Test;

public class RemapExceptionSpec
{
    [Fact]
    public void RemapExceptionTest1()
    {
        var result = ResultBox.Error(new InvalidDataException("test"))
            .RemapException(
                ex => ex is ArgumentNullException ? new ApplicationException("おかしなデータとなりました。", ex)
                    : ex);
        Assert.False(result.IsSuccess);
        Assert.IsType<InvalidDataException>(result.GetException());
        Assert.Equal("test", result.GetException().Message);
    }

    [Fact]
    public void RemapExceptionTest2()
    {
        var result = ResultBox.Error(new InvalidDataException("test"))
            .RemapException(
                ex => ex is InvalidDataException ? new ApplicationException("おかしなデータとなりました。", ex)
                    : ex);
        Assert.False(result.IsSuccess);
        Assert.IsType<ApplicationException>(result.GetException());
        Assert.Equal("おかしなデータとなりました。", result.GetException().Message);
    }



    [Fact]
    public async Task RemapExceptionTest1Async()
    {
        var result = await ResultBox.FromValue(Task.FromResult(1))
            .Verify(_ => new InvalidDataException("test"))
            .RemapException(
                ex => ex is ArgumentNullException ? new ApplicationException("おかしなデータとなりました。", ex)
                    : ex);
        Assert.False(result.IsSuccess);
        Assert.IsType<InvalidDataException>(result.GetException());
        Assert.Equal("test", result.GetException().Message);
    }
    [Fact]
    public async Task RemapExceptionTest2Async()
    {
        var result = await ResultBox.FromValue(Task.FromResult(1))
            .Verify(_ => new InvalidDataException("test"))
            .RemapException(
                ex => ex is InvalidDataException ? new ApplicationException("おかしなデータとなりました。", ex)
                    : ex);
        Assert.False(result.IsSuccess);
        Assert.IsType<ApplicationException>(result.GetException());
        Assert.Equal("おかしなデータとなりました。", result.GetException().Message);
    }
}

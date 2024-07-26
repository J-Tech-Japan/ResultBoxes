namespace ResultBoxes.Test;

public class VerifySpec
{
    [Fact]
    public void VerifyTest1()
    {
        var result = ResultBox.FromValue(1);
        var result2 = result.Verify(
            x => x switch
            {
                > 10 => new InvalidDataException("error1"),
                0 => new DivideByZeroException("error2"),
                _ => ExceptionOrNone.None
            });
        Assert.Equal(result, result2);
    }
    [Fact]
    public void VerifyTest2()
    {
        var result = ResultBox.FromValue(20)
            .Verify(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.True(result.Exception is InvalidDataException);
    }
    [Fact]
    public void VerifyTest3()
    {
        var result = ResultBox.FromValue(0)
            .Verify(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.True(result.Exception is DivideByZeroException);
    }

    [Fact]
    public async Task VerifyTest1Async()
    {
        var result1 = await Task.FromResult(ResultBox.FromValue(1))
            .Verify(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.Equal(1, result1.GetValue());
    }
    [Fact]
    public async Task VerifyTest2Async()
    {
        var result = await Task.FromResult(ResultBox.FromValue(20))
            .Verify(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.True(result.Exception is InvalidDataException);
    }
    [Fact]
    public async Task VerifyTest3Async()
    {
        var result = await Task.FromResult(ResultBox.FromValue(0))
            .Verify(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.True(result.Exception is DivideByZeroException);
    }

    [Fact]
    public async Task VerifyWithTask1()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(v => Task.FromResult(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error")));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithTask2()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(v => Task.FromResult(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error")));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithTask3()
    {
        var sut = await ResultBox.FromValue(2)
            .Verify(v => Task.FromResult(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error")));
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithTask4()
    {
        var sut = await ResultBox.FromValue(2).ToTask()
            .Verify(v => Task.FromResult(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error")));
        Assert.False(sut.IsSuccess);
    }
}

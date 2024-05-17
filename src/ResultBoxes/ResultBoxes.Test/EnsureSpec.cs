using ResultBoxes;
namespace SngleResults.Test;

public class EnsureSpec
{
    [Fact]
    public void EnsureTest1()
    {
        var result = ResultBox.FromValue(1);
        var result2 = result.Ensure(
            x => x switch
            {
                > 10 => new InvalidDataException("error1"),
                0 => new DivideByZeroException("error2"),
                _ => ExceptionOrNone.None
            });
        Assert.Equal(result, result2);
    }
    [Fact]
    public void EnsureTest2()
    {
        var result = ResultBox.FromValue(20)
            .Ensure(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.True(result.Exception is InvalidDataException);
    }
    [Fact]
    public void EnsureTest3()
    {
        var result = ResultBox.FromValue(0)
            .Ensure(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.True(result.Exception is DivideByZeroException);
    }

    [Fact]
    public async Task EnsureTest1Async()
    {
        var result1 = await Task.FromResult(ResultBox.FromValue(1))
            .Ensure(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.Equal(1, result1.GetValue());
    }
    [Fact]
    public async Task EnsureTest2Async()
    {
        var result = await Task.FromResult(ResultBox.FromValue(20))
            .Ensure(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.True(result.Exception is InvalidDataException);
    }
    [Fact]
    public async Task EnsureTest3Async()
    {
        var result = await Task.FromResult(ResultBox.FromValue(0))
            .Ensure(
                x => x switch
                {
                    > 10 => new InvalidDataException("error1"),
                    0 => new DivideByZeroException("error2"),
                    _ => ExceptionOrNone.None
                });
        Assert.True(result.Exception is DivideByZeroException);
    }
}

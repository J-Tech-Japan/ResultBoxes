using ResultBoxes;
using SingleResults.Usage;
namespace SngleResults.Test;

public class UnwrapTest
{
    [Fact]
    public void UnwrapSpec()
    {
        var sut = FunctionDeclarations.Increment(1)
            .UnwrapBox(v => v + 1);

        Assert.Equal(3, sut);
    }
    [Fact]
    public void UnwrapSpecThrows()
    {
        Assert.Throws<DivideByZeroException>(
            () =>
            {
                var sut = FunctionDeclarations.Increment(1)
                    // ReSharper disable once IntDivisionByZero
                    .UnwrapBox(v => v / 0);
            }
        );
    }

    [Fact]
    public async Task UnwrapTaskSpec1()
    {
        var sut = await FunctionDeclarations.IncrementAsync(1).UnwrapBox(v => v + 1);

        Assert.Equal(3, sut);
    }
    [Fact]
    public async Task UnwrapTaskSpec1Throws()
    {
        await Assert.ThrowsAsync<DivideByZeroException>(
            async () =>
            {
                var sut = await FunctionDeclarations.IncrementAsync(1)
                    // ReSharper disable once IntDivisionByZero
                    .UnwrapBox(v => v / 0);
            }
        );
    }

    [Fact]
    public async Task UnwrapTaskSpec2()
    {
        var sut = await FunctionDeclarations.IncrementAsync(1)
            .UnwrapBox(async v => await Task.FromResult(v + 1));

        Assert.Equal(3, sut);
    }
    [Fact]
    public async Task UnwrapTaskSpec2Throws()
    {
        await Assert.ThrowsAsync<DivideByZeroException>(
            async () =>
            {
                var sut = await FunctionDeclarations.IncrementAsync(1)
                    // ReSharper disable once IntDivisionByZero
                    .UnwrapBox(async v => await Task.FromResult(v / 0));
            }
        );
    }

    [Fact]
    public async Task UnwrapTaskSpec3()
    {
        var sut = await FunctionDeclarations.IncrementAsync(1).UnwrapBox();

        Assert.Equal(2, sut);
    }
    [Fact]
    public async Task UnwrapTaskSpec3Throws()
    {
        await Assert.ThrowsAsync<ApplicationException>(
            async () =>
            {
                // ReSharper disable once IntDivisionByZero
                var sut = await Task
                    .FromResult(ResultBox<int>.FromException(new ApplicationException("test")))
                    .UnwrapBox();
            }
        );
    }
}

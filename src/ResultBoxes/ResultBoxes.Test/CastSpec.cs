namespace ResultBoxes.Test;

public class CastSpec
{
    [Fact]
    public async Task TestCode()
    {
        await Task.CompletedTask;
        var castedResultBox = ResultBox<ITest>.FromValue(new Test()).Cast<ITest, Test>();
        Assert.IsType<ResultBox<Test>>(castedResultBox);

        var resultBoxTask = Task.FromResult(ResultBox<ITest>.FromValue(new Test()));
        var castedResultBox2 = await resultBoxTask.Cast<ITest, Test>();
        var castedResultBoxTask = resultBoxTask.Cast<ITest, Test>();
        Assert.IsType<ResultBox<Test>>(castedResultBox2);
        await Assert.IsType<Task<ResultBox<Test>>>(castedResultBoxTask);
    }
    [Fact]
    public async Task TestCode2()
    {
        await Task.CompletedTask;
        var resultBox = ResultBox<ITest>.FromValue(new Test());
        var castedResultBox = resultBox.Cast<Test>();
        Assert.IsType<ResultBox<Test>>(castedResultBox);

        var resultBoxTask = Task.FromResult(resultBox).Conveyor(val => val.ToResultBox().Cast<Test>());
        await Assert.IsType<Task<ResultBox<Test>>>(resultBoxTask);

        var resultBoxTask2 = Task.FromResult(resultBox).ConveyorResult(result => result.Cast<Test>());
        await Assert.IsType<Task<ResultBox<Test>>>(resultBoxTask2);

        var resultBoxTask3 = Task.FromResult(resultBox).ConveyorResult(ResultBox.Cast<ITest, Test>);
        await Assert.IsType<Task<ResultBox<Test>>>(resultBoxTask2);
    }
    public interface ITest
    {
    }
    public record Test : ITest
    {
    }
}

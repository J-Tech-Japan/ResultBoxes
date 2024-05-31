using Xunit.Sdk;
namespace ResultBoxes.Test;

public class ReduceSpec
{
    [Fact]
    public void ReduceWithControlFlow1()
    {
        var result = ResultBox.FromValue(Enumerable.Range(1, 10).ToList())
            .ReduceEach(0, (coming, aggregate) => ReduceResultValue.Continue(coming + aggregate));
        Assert.True(result.IsSuccess);
        Assert.Equal(55, result.GetValue());
    } 
    [Fact]
    public void ReduceWithControlFlow2Break()
    {
        var result = ResultBox.FromValue(Enumerable.Range(1, 10).ToList())
            .ReduceEach(0, (coming, aggregate) => coming < 5 ? ReduceResultValue.Continue(coming + aggregate) : ReduceResultValue<int>.Break(aggregate));
        Assert.True(result.IsSuccess);
        Assert.Equal(10, result.GetValue());
    } 
    [Fact]
    public void ReduceWithControlFlow3Error()
    {
        var result = ResultBox.FromValue(Enumerable.Range(1, 10).ToList())
            .ReduceEach(0, (coming, aggregate) => coming < 5 ? ReduceResultValue.Continue(coming + aggregate) : new ApplicationException("Can not execute"));
        Assert.False(result.IsSuccess);
    } 
    [Fact]
    public void ReduceWithControlFlow4Unit()
    {
        var result = ResultBox.FromValue(Enumerable.Range(1, 10).ToList())
            .ReduceEach(UnitValue.Unit, (coming, aggregate) => coming < 5 ? ReduceResultValue.Continue(aggregate) : ReduceResultValue.Break(aggregate));
        Assert.True(result.IsSuccess);
        Assert.Equal(UnitValue.Unit, result.GetValue());
    } 

    [Fact]
    public void ReduceWithoutControl1()
    {
        var result = ResultBox.FromValue(Enumerable.Range(1, 10).ToList())
            .ReduceEach(0, (coming, aggregate) => coming + aggregate);
        Assert.True(result.IsSuccess);
        Assert.Equal(55, result.GetValue());
    } 
    [Fact]
    public void ReduceWithoutControlError()
    {
        var result = ResultBox.FromValue(Enumerable.Range(1, 10).ToList())
            .ReduceEach(0, (coming, aggregate) => coming < 5 ? coming + aggregate : new ApplicationException("Can not execute"));
        Assert.False(result.IsSuccess);
    } 
    

}

namespace ResultBoxes.Test;

public class OptionalValueTest
{
    [Fact]
    public void OptionalRemapTest()
    {
        var sut = OptionalValue.FromValue(2).Remap(value => value.ToString());
        Assert.Equal("2", sut.GetValue());
    }
    
    [Fact]
    public void OptionalRemapNullTest()
    {
        var sut = OptionalValue<int>.Empty.Remap(value => value.ToString());
        Assert.False(sut.HasValue);
    }

    [Fact]
    public async Task OptionalTaskTest()
    {
        var sut = await OptionalValue.FromValue(Task.FromResult(2));
        Assert.True(sut.HasValue);
        Assert.Equal(2, sut.GetValue());
    }
    [Fact]
    public async Task OptionalTaskTest2()
    {
        var sut = await OptionalValue.FromNullableValue<int>(Task.FromResult((int?)null));
        Assert.False(sut.HasValue);
    }

}
namespace ResultBoxes.Test;

public class OptionalValueTests
{
    [Fact]
    public void OptionalValueEmptyTest()
    {
        var sut = OptionalValue<string>.Empty;

        Assert.False(sut.HasValue);
        Assert.Throws<ResultsInvalidOperationException>(() => sut.GetValue());
    }

    [Fact]
    public void OptionalValueCreateSpec()
    {
        var value = (Guid?) Guid.NewGuid();
        var sut = OptionalValue.FromNullableValue(value);
        Assert.True(sut.HasValue);
    }
    [Fact]
    public void OptionalValueCreateSpec2()
    {
        var value = (Guid?) null;
        var sut = OptionalValue.FromNullableValue(value);
        Assert.False(sut.HasValue);
    }

    [Fact]
    public void OptionalValueMatchSpec()
    {
        var sut = OptionalValue.FromValue(1);
        var result = sut.Match(
            value => value.ToString(),
            () => "empty");

        Assert.Equal("1", result);
    }
    [Fact]
    public void OptionalValueMatchEmptySpec()
    {
        var sut = OptionalValue<int>.Empty;
        var result = sut.Match(
            value => value.ToString(),
            () => "empty");

        Assert.Equal("empty", result);
    }
    [Fact]
    public async Task OptionalValueMatchTaskEmptySpec()
    {
        var sut = OptionalValue<int>.Empty;
        var result = await sut.Match(
            value => Task.FromResult( value.ToString()),
            () => "empty");

        Assert.Equal("empty", result);
    }
    [Fact]
    public async Task OptionalValueMatchTaskEmpty2Spec()
    {
        var sut = OptionalValue<int>.Empty;
        var result = await sut.Match(
            value => Task.FromResult( value.ToString()),
            () => Task.FromResult("empty"));

        Assert.Equal("empty", result);
    }
    [Fact]
    public async Task OptionalValueMatchTaskEmpty3Spec()
    {
        var sut = OptionalValue<int>.Empty;
        var result = await sut.Match(
            value => value.ToString(),
            () => Task.FromResult("empty"));

        Assert.Equal("empty", result);
    }
}
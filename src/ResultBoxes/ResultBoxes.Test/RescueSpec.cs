using System.Net.Mime;
namespace ResultBoxes.Test;

public class RescueSpec
{
    [Fact]
    public void RescueTest1()
    {
        var result = ResultBox.FromValue(1)
            .Verify(_ =>new InvalidDataException("Invalid data"))
            .Rescue(ex => ex is ArgumentNullException ? 2 : ValueOrException.Exception);
        
        Assert.False(result.IsSuccess);
        Assert.IsType<InvalidDataException>( result.GetException());
    }
    [Fact]
    public void RescueTest2()
    {
        var result = ResultBox.FromValue(1)
            .Verify(_ =>new InvalidDataException("Invalid data"))
            .Rescue(ex => ex is InvalidDataException ? 2 : ValueOrException.Exception);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.GetValue());
    }
    [Fact]
    public void RescueTest3()
    {
        var result = ResultBox.FromValue(1)
            .Verify(_ =>new InvalidDataException("Invalid data"))
            .Rescue(ex => ex is ArgumentNullException ? 2 : ValueOrException<int>.Exception);
        
        Assert.False(result.IsSuccess);
        Assert.IsType<InvalidDataException>( result.GetException());
    }
    [Fact]
    public void RescueTest4()
    {
        var result = ResultBox.FromValue(1)
            .Verify(_ =>new InvalidDataException("Invalid data"))
            .Rescue(ex => ex is InvalidDataException ? ValueOrException<int>.FromValue(2) : ValueOrException.Exception);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.GetValue());
    }
    [Fact]
    public void RescueTest5()
    {
        var result = ResultBox.FromValue(1)
            .Verify(_ =>new InvalidDataException("Invalid data"))
            .Rescue(ex => ex is InvalidDataException ? ValueOrException.FromValue(2) : ValueOrException.Exception);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.GetValue());
    }
    
    
    [Fact]
    public async Task RescueTest1Async()
    {
        var result = await ResultBox.FromValue(Task.FromResult(1))
            .Verify(_ =>new InvalidDataException("Invalid data"))
            .Rescue(ex => ex is ArgumentNullException ? 2 : ValueOrException.Exception);
        
        Assert.False(result.IsSuccess);
        Assert.IsType<InvalidDataException>( result.GetException());
    }
    [Fact]
    public async Task RescueTest2Async()
    {
        var result = await ResultBox.FromValue(Task.FromResult(1))
            .Verify(_ =>new InvalidDataException("Invalid data"))
            .Rescue(ex => ex is InvalidDataException ? 2 : ValueOrException.Exception);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.GetValue());
    }

}

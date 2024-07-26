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
    
    [Fact]
    public async Task VerifyWithResultBox1()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(v => ResultBox.FromValue(Task.FromResult(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error"))));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox2()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(v => ResultBox.FromValue(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error"))).ToTask();
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox3()
    {
        var sut = await ResultBox.FromValue(2)
            .Verify(v => ResultBox.FromValue(Task.FromResult(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error"))));
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox4()
    {
        var sut = await ResultBox.FromValue(2)
            .Verify(v => ResultBox.FromValue(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error"))).ToTask();
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox5()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(v => ResultBox.FromValue(Task.FromResult(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error"))));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox6()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(v => ResultBox.FromValue(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error")));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox7()
    {
        var sut = await ResultBox.FromValue(2).ToTask()
            .Verify(v => ResultBox.FromValue(Task.FromResult(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error"))));
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox8()
    {
        var sut = await ResultBox.FromValue(2).ToTask()
            .Verify(v => ResultBox.FromValue(v == 1 ? ExceptionOrNone.None : new ApplicationException("test error")));
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox9()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(v => ResultBox.FromException<ExceptionOrNone>(new ApplicationException("test error")));
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithResultBox10()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(v => ResultBox.FromException<ExceptionOrNone>(new ApplicationException("test error"))).ToTask();
        Assert.False(sut.IsSuccess);
    }
    
    [Fact]
    public async Task VerifyWithNoParam1()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(() => ExceptionOrNone.None).ToTask();
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParam2()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(() => new ApplicationException("error")).ToTask();
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParam3()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(() => ExceptionOrNone.None);
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParam4()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(() => new ApplicationException("error"));
        Assert.False(sut.IsSuccess);
    }

    
    [Fact]
    public async Task VerifyWithNoParam5()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(() => Task.FromResult(ExceptionOrNone.None));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParam6()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(() => Task.FromResult(ExceptionOrNone.FromException(new ApplicationException("error"))));
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParam7()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(() => Task.FromResult(ExceptionOrNone.None));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParam8()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(() => Task.FromResult(ExceptionOrNone.FromException(new ApplicationException("error"))));
        Assert.False(sut.IsSuccess);
    }

    
    
    
    [Fact]
    public async Task VerifyWithNoParamWithResult1()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(() => ResultBox.FromValue(ExceptionOrNone.None)).ToTask();
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParamWithResult2()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(() => ResultBox.FromException<ExceptionOrNone>(new ApplicationException("error"))).ToTask();
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParamWithResult3()
    {
        var sut = await ResultBox.FromValue(1)
            .Verify(() => ResultBox.FromValue<ExceptionOrNone>(new ApplicationException("error"))).ToTask();
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParamWithResult4()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(() => ResultBox.FromValue(ExceptionOrNone.None));
        Assert.True(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParamWithResult5()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(() => ResultBox.FromException<ExceptionOrNone>(new ApplicationException("error")));
        Assert.False(sut.IsSuccess);
    }
    [Fact]
    public async Task VerifyWithNoParamWithResult6()
    {
        var sut = await ResultBox.FromValue(1).ToTask()
            .Verify(() => ResultBox.FromValue<ExceptionOrNone>(new ApplicationException("error")));
        Assert.False(sut.IsSuccess);
    }

    
}

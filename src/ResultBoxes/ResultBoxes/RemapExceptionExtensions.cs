using System.Diagnostics;
namespace ResultBoxes;

public static class RemapExceptionExtensions
{
    public static ResultBox<TValue> RemapException<TValue>(
        this ResultBox<TValue> current,
        Func<Exception, Exception> remapExceptionFunc)
        where TValue : notnull
        =>
            current switch
            {
                { IsSuccess: false } => remapExceptionFunc(current.GetException()),
                { IsSuccess: true } => current.GetValue()
            };

    public static async Task<ResultBox<TValue>> RemapException<TValue>(
        this ResultBox<TValue> current,
        Func<Exception, Task<Exception>> remapExceptionFuncAsync)
        where TValue : notnull
        =>
            current switch
            {
                { IsSuccess: false } => await remapExceptionFuncAsync(current.GetException()),
                { IsSuccess: true } => ResultBox.FromValue(current.GetValue()) 
            };

    public static async Task<ResultBox<TValue>> RemapException<TValue>(
        this Task<ResultBox<TValue>> current,
        Func<Exception, Exception> remapExceptionFunc)
        where TValue : notnull
        =>
            await current switch
            {
                { IsSuccess: false } valueBox => remapExceptionFunc(valueBox.GetException()),
                { IsSuccess: true } errorBox => ResultBox.FromValue(errorBox.GetValue()) 
            };

    public static async Task<ResultBox<TValue>> RemapException<TValue>(
        this Task<ResultBox<TValue>> current,
        Func<Exception, Task<Exception>> remapExceptionFuncAsync)
        where TValue : notnull
        =>
            await current switch
            {
                { IsSuccess: false } valueBox => await remapExceptionFuncAsync(valueBox.GetException()),
                { IsSuccess: true } errorBox => ResultBox.FromValue(errorBox.GetValue()) 
            };

}

namespace ResultBoxes;

public static class RescueExtensions
{
    public static ResultBox<TValue> Rescue<TValue>(
        this ResultBox<TValue> current,
        Func<Exception, ValueOrException<TValue>> remapExceptionFunc)
        where TValue : notnull
        =>
            current switch
            {
                { IsSuccess: false } => remapExceptionFunc(current.GetException()) switch
                {
                    { IsException: true } =>
                        ResultBox<TValue>.FromException(current.GetException()),
                    { IsException: false } value => ResultBox.FromValue(value.GetValue())
                },
                { IsSuccess: true } => current.GetValue()
            };
    public static async Task<ResultBox<TValue>> Rescue<TValue>(
        this ResultBox<TValue> current,
        Func<Exception, Task<ValueOrException<TValue>>> remapExceptionFunc)
        where TValue : notnull
        =>
            current switch
            {
                { IsSuccess: false } => await remapExceptionFunc(current.GetException()) switch
                {
                    { IsException: true } =>
                        ResultBox<TValue>.FromException(current.GetException()),
                    { IsException: false } value => ResultBox.FromValue(value.GetValue())
                },
                { IsSuccess: true } => current.GetValue()
            };

    public static async Task<ResultBox<TValue>> Rescue<TValue>(
        this Task<ResultBox<TValue>> current,
        Func<Exception, ValueOrException<TValue>> remapExceptionFunc)
        where TValue : notnull
        =>
            await current switch
            {
                { IsSuccess: false } errorBox => remapExceptionFunc(errorBox.GetException()) switch
                {
                    { IsException: true } => ResultBox<TValue>.FromException(
                        errorBox.GetException()),
                    { IsException: false } value => ResultBox.FromValue(value.GetValue())
                },
                { IsSuccess: true } valueBox => valueBox.GetValue()
            };
    public static async Task<ResultBox<TValue>> Rescue<TValue>(
        this Task<ResultBox<TValue>> current,
        Func<Exception, Task<ValueOrException<TValue>>> remapExceptionFunc)
        where TValue : notnull
        =>
            await current switch
            {
                { IsSuccess: false } errorBox => await remapExceptionFunc(errorBox.GetException())
                    switch
                    {
                        { IsException: true } => ResultBox<TValue>.FromException(
                            errorBox.GetException()),
                        { IsException: false } value => ResultBox.FromValue(value.GetValue())
                    },
                { IsSuccess: true } valueBox => valueBox.GetValue()
            };
}

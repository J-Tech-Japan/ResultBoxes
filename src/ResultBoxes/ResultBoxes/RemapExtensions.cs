namespace ResultBoxes;

public static class RemapExtensions
{

    public static ResultBox<TValueResult> Remap<TValueOriginal, TValueResult>(
        this ResultBox<TValueOriginal> current,
        Func<TValueOriginal, TValueResult> valueFunc)
        where TValueOriginal : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => valueFunc(current.GetValue()),
            { IsSuccess: false } => current.GetException()
        };

    public static ResultBox<TValueResult> Remap<TValueOriginal1, TValueOriginal2, TValueResult>(
        this ResultBox<TwoValues<TValueOriginal1, TValueOriginal2>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueResult> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => current.GetValue().Call(valueFunc),
            { IsSuccess: false } => current.GetException()
        };

    public static ResultBox<TValueResult> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3,
        TValueResult>(
        this ResultBox<ThreeValues<TValueOriginal1, TValueOriginal2, TValueOriginal3>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueResult> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => current.GetValue().Call(valueFunc),
            { IsSuccess: false } => current.GetException()
        };
    public static ResultBox<TValueResult> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3,
        TValueOriginal4, TValueResult>(
        this ResultBox<FourValues<TValueOriginal1, TValueOriginal2, TValueOriginal3,
            TValueOriginal4>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueResult>
            valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => current.GetValue().Call(valueFunc),
            { IsSuccess: false } => current.GetException()
        };
    public static ResultBox<TValueResult> Remap<TValueOriginal1, TValueOriginal2, TValueOriginal3,
        TValueOriginal4, TValueOriginal5, TValueResult>(
        this ResultBox<FiveValues<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4
            , TValueOriginal5>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5,
            TValueResult> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueOriginal5 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: true } => current.GetValue().Call(valueFunc),
            { IsSuccess: false } => current.GetException()
        };


    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal, TValueResult>(
        this ResultBox<TValueOriginal> current,
        Func<TValueOriginal, Task<TValueResult>> valueFunc)
        where TValueOriginal : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await valueFunc(current.GetValue()),
            _ => new ResultValueNullException()
        };

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2,
        TValueResult>(
        this ResultBox<TwoValues<TValueOriginal1, TValueOriginal2>> current,
        Func<TValueOriginal1, TValueOriginal2, Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await current.GetValue().Call(valueFunc),
            _ => new ResultValueNullException()
        };
    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2,
        TValueOriginal3, TValueResult>(
        this ResultBox<ThreeValues<TValueOriginal1, TValueOriginal2, TValueOriginal3>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await current.GetValue().Call(valueFunc),
            _ => new ResultValueNullException()
        };

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2,
        TValueOriginal3, TValueOriginal4, TValueResult>(
        this ResultBox<FourValues<TValueOriginal1, TValueOriginal2, TValueOriginal3,
            TValueOriginal4>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, Task<TValueResult>>
            valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await current.GetValue().Call(valueFunc),
            _ => new ResultValueNullException()
        };

    public static async Task<ResultBox<TValueResult>> Remap<TValueOriginal1, TValueOriginal2,
        TValueOriginal3, TValueOriginal4, TValueOriginal5, TValueResult>(
        this ResultBox<FiveValues<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4
            , TValueOriginal5>> current,
        Func<TValueOriginal1, TValueOriginal2, TValueOriginal3, TValueOriginal4, TValueOriginal5,
            Task<TValueResult>> valueFunc)
        where TValueOriginal1 : notnull
        where TValueOriginal2 : notnull
        where TValueOriginal3 : notnull
        where TValueOriginal4 : notnull
        where TValueOriginal5 : notnull
        where TValueResult : notnull =>
        current switch
        {
            { IsSuccess: false } => current.GetException(),
            { IsSuccess: true } => await current.GetValue().Call(valueFunc),
            _ => new ResultValueNullException()
        };
}

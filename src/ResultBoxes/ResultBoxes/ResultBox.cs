using System.Text.Json.Serialization;
namespace ResultBoxes;

public record ResultBox<TValue> where TValue : notnull
{
    internal ResultBox(TValue? value, Exception? exception) =>
        (Value, Exception) = (value, exception);

    [JsonIgnore]
    public bool IsSuccess => Exception is null;
    public static ResultBox<TValue> OutOfRange => new(new ResultValueNullException());
    internal TValue? Value { get; }
    public Exception? Exception { get; }
    public Exception GetException() =>
        Exception ?? throw new ResultsInvalidOperationException("no exception");
    public TValue GetValue() =>
        (IsSuccess ? Value : throw new ResultsInvalidOperationException("no value")) ??
        throw new ResultsInvalidOperationException();
    public static ResultBox<TValue> FromValue(TValue value) => new(value, null);
    public static ResultBox<TValue> FromValue(Func<TValue> value) => new(value(), null);
    public static async Task<ResultBox<TValue>> FromValueAsync(Func<Task<TValue>> value) =>
        new(await value(), null);
    public static ResultBox<TValue> FromException(Exception exception) =>
        new(default, exception);

    public ResultBox<TValueResult> Handle<TValueResult>(Func<TValue, TValueResult> valueFunc)
        where TValueResult : notnull =>
        this switch
        {
            { IsSuccess: true } => valueFunc(GetValue()),
            { IsSuccess: false } => GetException()
        };
    public ResultBox<TValueResult> Handle<TValueResult>(
        Func<TValue, ResultBox<TValueResult>> valueFunc) where TValueResult : notnull =>
        this switch
        {
            { IsSuccess: false } error => error.GetException(),
            { IsSuccess: true } value => valueFunc(value.GetValue()),
            _ => new ResultValueNullException()
        };
    public async Task<ResultBox<TValueResult>> HandleAsync<TValueResult>(
        Func<TValue, Task<ResultBox<TValueResult>>> valueFunc) where TValueResult : notnull =>
        this switch
        {
            { IsSuccess: false } error => error.GetException(),
            { IsSuccess: true } value => await valueFunc(value.GetValue()),
            _ => new ResultValueNullException()
        };
    public ResultBox<TValueResult> HandleResult<TValueResult>(
        Func<ResultBox<TValue>, ResultBox<TValueResult>> valueFunc)
        where TValueResult : notnull =>
        this switch
        {
            { IsSuccess: false } error => error.GetException(),
            { Value: not null } value => valueFunc(value),
            _ => new ResultValueNullException()
        };
    public async Task<ResultBox<TValueResult>> HandleResultAsync<TValueResult>(
        Func<ResultBox<TValue>, Task<ResultBox<TValueResult>>> valueFunc)
        where TValueResult : notnull =>
        this switch
        {
            { IsSuccess: false } error => error.GetException(),
            { Value: not null } value => await valueFunc(value),
            _ => new ResultValueNullException()
        };
    public async Task<ResultBox<TValueResult>> HandleAsync<TValueResult>(
        Func<TValue, Task<TValueResult>> valueFunc) where TValueResult : notnull =>
        this switch
        {
            { IsSuccess: false } error => error.GetException(),
            { IsSuccess: true } value => await valueFunc(value.GetValue()),
            _ => new ResultValueNullException()
        };

    public static implicit operator ResultBox<TValue>(TValue value) => new(value, null);
    public static implicit operator ResultBox<TValue>(Exception exception) =>
        new(default, exception);

    public ResultBox<TwoValues<TValue, TValue2>> Append<TValue2>(TValue2 value)
        where TValue2 : notnull =>
        this switch
        {
            { IsSuccess: false } error => error.GetException(),
            { Value: { } addingValue } => new ResultBox<TwoValues<TValue, TValue2>>(
                new TwoValues<TValue, TValue2>(Value, value),
                null),
            _ => new ResultValueNullException()
        };

    public static ResultBox<TValue> WrapTry(Func<TValue> func)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static ResultBox<UnitValue> WrapTry(Action action)
    {
        try
        {
            action();
            return new UnitValue();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<ResultBox<TValue>> WrapTry(Func<Task<TValue>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception e)
        {
            return e;
        }
    }
}
public static class ResultBox
{
    public static ResultBox<TValue> FromValue<TValue>(TValue value) where TValue : notnull =>
        new(value, null);
    public static async Task<ResultBox<TValue>> FromValue<TValue>(Task<TValue> value)
        where TValue : notnull =>
        new(await value, null);
    public static async Task<ResultBox<TValue>> FromValue<TValue>(Func<Task<TValue>> value)
        where TValue : notnull =>
        new(await value(), null);
}

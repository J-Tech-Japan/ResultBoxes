using System.Text.Json.Serialization;
namespace SingleResults;

public record SingleValueResult<TValue>(TValue? Value, Exception? Exception) where TValue : notnull
{

    [JsonIgnore]
    public bool IsSuccess => Exception is null;
    public static SingleValueResult<TValue> OutOfRange => new(new ResultValueNullException());
    public static SingleValueResult<TValue> FromValue(TValue value) => new(value, null);
    public static SingleValueResult<TValue> FromException(Exception exception) =>
        new(default, exception);
    public Exception GetException() =>
        Exception ?? throw new ResultsInvalidOperationException("no exception");
    public TValue GetValue() =>
        (IsSuccess ? Value : throw new ResultsInvalidOperationException("no value")) ??
        throw new ResultsInvalidOperationException();
    public static implicit operator SingleValueResult<TValue>(TValue value) => new(value, null);
    public static implicit operator SingleValueResult<TValue>(Exception exception) =>
        new(default, exception);

    public static SingleValueResult<TValue> WrapTry(Func<TValue> func)
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
    public static SingleValueResult<UnitValue> WrapTry(Action action)
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

    public static async Task<SingleValueResult<TValue>> WrapTry(Func<Task<TValue>> func)
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

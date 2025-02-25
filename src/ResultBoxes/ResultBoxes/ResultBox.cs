using System.Text.Json.Serialization;
namespace ResultBoxes;

public record ResultBox<TValue> where TValue : notnull
{
    private readonly Exception? exception;

    [JsonIgnore]
    public bool IsSuccess => Exception is null && Value is not null;

    public static ResultBox<TValue> OutOfRange => new(new ResultValueNullException());
    internal TValue? Value { get; }

    public Exception? Exception =>
        exception ?? (Value is null ? new ResultValueNullException() : null);

    internal ResultBox(TValue? value, Exception? exception) =>
        (Value, this.exception) = (value, exception);

    public Exception GetException() =>
        Exception ?? throw new ResultsInvalidOperationException("no exception");

    public TValue GetValue() =>
        (IsSuccess ? Value : throw new ResultsInvalidOperationException("no value")) ??
        throw new ResultsInvalidOperationException();

    public static ResultBox<TValue> Ok(TValue value) => new(value, null);
    public static ResultBox<TValue> FromValue(TValue value) => new(value, null);
    public static ResultBox<TValue> FromValue(Func<TValue> value) => new(value(), null);

    public static async Task<ResultBox<TValue>> FromValue(Func<Task<TValue>> value) =>
        new(await value(), null);

    public static async Task<ResultBox<TValue>> FromValue(Task<TValue> value) =>
        new(await value, null);

    public static ResultBox<TValue> FromException(Exception exception) =>
        new(default, exception);

    public static ResultBox<TValue> Error(Exception exception) =>
        new(default, exception);

    public ResultBox<TCasted> Cast<TCasted>() where TCasted : notnull =>
        this switch
        {
            { IsSuccess: false } => GetException(),
            { IsSuccess: true } => new ResultBox<TCasted>((TCasted)(object)GetValue(), null),
            _ => new ResultValueNullException()
        };

    public static implicit operator ResultBox<TValue>(TValue value) => new(value, null);

    public static implicit operator ResultBox<TValue>(Exception exception) =>
        new(default, exception);

    public ResultBox<TwoValues<TValue, TValue2>> Append<TValue2>(TValue2 value2) where TValue2 : notnull =>
        this switch
        {
            { IsSuccess: false } => GetException(),
            { IsSuccess: true } => new ResultBox<TwoValues<TValue, TValue2>>(
                new TwoValues<TValue, TValue2>(GetValue(), value2),
                null),
            _ => new ResultValueNullException()
        };

    public static ResultBox<TValue> WrapTry(Func<TValue> func, Func<Exception, Exception>? exceptionMapper)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            return exceptionMapper?.Invoke(e) ?? e;
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

    public static async Task<ResultBox<UnitValue>> WrapTry(Func<Task> action)
    {
        try
        {
            await action();
            return new UnitValue();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public static async Task<ResultBox<TValue>> WrapTry(
        Func<Task<TValue>> func,
        Func<Exception, Exception>? exceptionMapper)
    {
        try
        {
            return await func();
        }
        catch (Exception e)
        {
            return exceptionMapper?.Invoke(e) ?? e;
        }
    }
}
public static class ResultBox
{
    public static ResultBox<UnitValue> Start => Ok(ResultBoxes.UnitValue.None);
    public static ResultBox<UnitValue> UnitValue => Start;

    public static ResultBox<TValue> FromValue<TValue>(TValue value) where TValue : notnull =>
        new(value, null);

    public static ResultBox<TValue> Ok<TValue>(TValue value) where TValue : notnull =>
        new(value, null);

    public static async Task<ResultBox<TValue>> FromValue<TValue>(Task<TValue> value) where TValue : notnull =>
        new(await value, null);

    public static async Task<ResultBox<TValue>> FromValue<TValue>(Func<Task<TValue>> value) where TValue : notnull =>
        new(await value(), null);

    public static ResultBox<UnitValue> Error(Exception exception) =>
        new(default, exception);

    public static ResultBox<TValue> Error<TValue>(Exception exception) where TValue : notnull =>
        new(default, exception);

    public static ResultBox<TValue> FromException<TValue>(Exception exception) where TValue : notnull =>
        new(default, exception);
    public static async Task<ResultBox<TValue>> FromException<TValue>(Task<Exception> exception)
        where TValue : notnull =>
        new(default, await exception);

    public static Task<ResultBox<TValueResult>> WrapTry<TValueResult>(
        Func<Task<TValueResult>> func,
        Func<Exception, Exception>? exceptionMapper = null) where TValueResult : notnull =>
        ResultBox<TValueResult>.WrapTry(func, exceptionMapper);

    public static ResultBox<TValueResult> WrapTry<TValueResult>(
        Func<TValueResult> func,
        Func<Exception, Exception>? exceptionMapper = null) where TValueResult : notnull =>
        ResultBox<TValueResult>.WrapTry(func, exceptionMapper);

    public static void LogResult<TValue>(ResultBox<TValue> result) where TValue : notnull => LogResult(result, "");

    public static void LogResult<TValue>(ResultBox<TValue> result, string marking) where TValue : notnull
    {
        switch (result)
        {
            case { IsSuccess: true }:
                Console.WriteLine(SpaceWithMarking(marking) + "Value: " + result.GetValue());
                break;
            case { IsSuccess: false }:
                Console.WriteLine(SpaceWithMarking(marking) + "Error: " + result.GetException().Message);
                break;
        }
    }

    private static string SpaceWithMarking(string marking) =>
        string.IsNullOrWhiteSpace(marking) ? "" : marking + " ";

    public static ResultBox<TValue> CheckNull<TValue>(TValue? value, Exception? exception = null)
        where TValue : notnull => value switch
    {
        not null => FromValue(value),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static ResultBox<TValue> CheckNull<TValue>(TValue? value, Exception? exception = null)
        where TValue : struct => value switch
    {
        { } v => FromValue(v),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static async Task<ResultBox<TValue>> CheckNull<TValue>(Task<TValue?> value, Exception? exception = null)
        where TValue : notnull => await value switch
    {
        { } v => FromValue(v),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static async Task<ResultBox<TValue>> CheckNull<TValue>(Task<TValue?> value, Exception? exception = null)
        where TValue : struct => await value switch
    {
        { } v => FromValue(v),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static ResultBox<TValue> CheckNull<TValue>(Func<TValue?> valueFunc, Exception? exception = null)
        where TValue : notnull => valueFunc() switch
    {
        { } value => FromValue(value),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static ResultBox<TValue> CheckNull<TValue>(Func<TValue?> valueFunc, Exception? exception = null)
        where TValue : struct => valueFunc() switch
    {
        { } value => FromValue(value),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static async Task<ResultBox<TValue>> CheckNull<TValue>(
        Func<Task<TValue?>> valueFunc,
        Exception? exception = null) where TValue : notnull => await valueFunc() switch
    {
        { } value => FromValue(value),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static async Task<ResultBox<TValue>> CheckNull<TValue>(
        Func<Task<TValue?>> valueFunc,
        Exception? exception = null) where TValue : struct => await valueFunc() switch
    {
        { } value => FromValue(value),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static ResultBox<TValue> CheckNullWrapTry<TValue>(
        Func<TValue?> func,
        Func<Exception, Exception>? exceptionMapper = null) where TValue : notnull
    {
        try
        {
            return func() switch
            {
                { } value => FromValue(value),
                _ => ResultBox<TValue>
                    .FromException(new ResultValueNullException(typeof(TValue).Name))
                    .RemapException(e => exceptionMapper?.Invoke(e) ?? e)
            };
        }
        catch (Exception e)
        {
            return exceptionMapper?.Invoke(e) ?? e;
        }
    }

    public static ResultBox<TValue> CheckNullWrapTry<TValue>(
        Func<TValue?> func,
        Func<Exception, Exception>? exceptionMapper = null) where TValue : struct
    {
        try
        {
            var result = func();
            return result.HasValue switch
            {
                true => ResultBox<TValue>.FromValue(result.Value),
                _ => ResultBox<TValue>
                    .FromException(new ResultValueNullException(typeof(TValue).Name))
                    .RemapException(e => exceptionMapper?.Invoke(e) ?? e)
            };
        }
        catch (Exception e)
        {
            return exceptionMapper?.Invoke(e) ?? e;
        }
    }

    public static async Task<ResultBox<TValue>> CheckNullWrapTry<TValue>(
        Func<Task<TValue?>> funcAsync,
        Func<Exception, Exception>? exceptionMapper = null) where TValue : notnull
    {
        try
        {
            return await funcAsync() switch
            {
                { } value => FromValue(value),
                _ => ResultBox<TValue>
                    .FromException(new ResultValueNullException(typeof(TValue).Name))
                    .RemapException(e => exceptionMapper?.Invoke(e) ?? e)
            };
        }
        catch (Exception e)
        {
            return exceptionMapper?.Invoke(e) ?? e;
        }
    }

    public static async Task<ResultBox<TValue>> CheckNullWrapTry<TValue>(
        Func<Task<TValue?>> funcAsync,
        Func<Exception, Exception>? exceptionMapper = null) where TValue : struct
    {
        try
        {
            return await funcAsync() switch
            {
                { } value => FromValue(value),
                _ => ResultBox<TValue>
                    .FromException(new ResultValueNullException(typeof(TValue).Name))
                    .RemapException(e => exceptionMapper?.Invoke(e) ?? e)
            };
        }
        catch (Exception e)
        {
            return exceptionMapper?.Invoke(e) ?? e;
        }
    }

    public static ResultBox<TValue> CheckOptionalEmpty<TValue>(
        OptionalValue<TValue> optionalValue,
        Exception? exception) where TValue : notnull => optionalValue.HasValue switch
    {
        true => FromValue(optionalValue.GetValue()),
        _ => ResultBox<TValue>.FromException(exception ?? new ResultValueNullException(typeof(TValue).Name))
    };

    public static ResultBox<TValue> CheckOptionalEmpty<TValue>(OptionalValue<TValue> optionalValue)
        where TValue : notnull => CheckOptionalEmpty(optionalValue, null);

    public static ResultBox<TCasted> Cast<TOriginal, TCasted>(ResultBox<TOriginal> resultBox)
        where TCasted : notnull where TOriginal : notnull
    {
        if (resultBox.IsSuccess)
        {
            return resultBox.GetValue() is TCasted castedValue
                ? ResultBox<TCasted>.FromValue(castedValue)
                : ResultBox<TCasted>.Error(new InvalidCastException($"Cannot cast value to {typeof(TCasted).Name}"));
        }
        return ResultBox<TCasted>.FromException(resultBox.GetException());
    }
}

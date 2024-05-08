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

    public TwoValuesResult<TValue, TValue2> CombineValue<TValue2>(
        SingleValueResult<TValue2> secondValue)
        where TValue2 : notnull
        => this switch
        {
            { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                e.Exception),
            { Value: not null } => secondValue switch
            {
                { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    default,
                    e.Exception),
                { Value: not null } => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    secondValue.Value,
                    null),
                _ => new TwoValuesResult<TValue, TValue2>(Value, default, null)
            },
            _ => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue)} combine to {nameof(TValue2)}"))
        };
    public async Task<TwoValuesResult<TValue, TValue2>> CombineValue<TValue2>(
        Func<Task<SingleValueResult<TValue2>>> secondValueFunc)
        where TValue2 : notnull
        => this switch
        {
            { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                e.Exception),
            { Value: not null } => await secondValueFunc() switch
            {
                { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    default,
                    e.Exception),
                { Value: { } secondValue } => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    secondValue,
                    null),
                _ => new TwoValuesResult<TValue, TValue2>(Value, default, null)
            },
            _ => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue)} combine to {nameof(TValue2)}"))
        };
    public async Task<TwoValuesResult<TValue, TValue2>> CombineValueWrapTry<TValue2>(
        Func<Task<TValue2>> secondValueFunc)
        where TValue2 : notnull
        => this switch
        {
            { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                e.Exception),
            { Value: not null } => await SingleValueResult<TValue2>.WrapTry(secondValueFunc)
                switch
                {
                    { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                        Value,
                        default,
                        e.Exception),
                    { Value: { } secondValue } => new TwoValuesResult<TValue, TValue2>(
                        Value,
                        secondValue,
                        null),
                    _ => new TwoValuesResult<TValue, TValue2>(Value, default, null)
                },
            _ => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                new ResultValueNullException(
                    $"out of range for {nameof(TValue)} combine to {nameof(TValue2)}"))
        };
    public TwoValuesResult<TValue, TValue2> CombineValueWrapTry<TValue2>(
        Func<TValue2> secondValueFunc)
        where TValue2 : notnull
        => this switch
        {
            { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                e.Exception),
            { Value: not null } => SingleValueResult<TValue2>.WrapTry(secondValueFunc) switch
            {
                { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    default,
                    e.Exception),
                { Value: { } secondValue } => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    secondValue,
                    null),
                _ => new TwoValuesResult<TValue, TValue2>(Value, default, null)
            },
            _ => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                new ResultValueNullException("out of range"))
        };
    public TwoValuesResult<TValue, TValue2> CombineValue<TValue2>(
        Func<TValue, SingleValueResult<TValue2>> secondValueFunc)
        where TValue2 : notnull
        => this switch
        {
            { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                e.Exception),
            { Value: { } value } => secondValueFunc(value) switch
            {
                { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    default,
                    e.Exception),
                { Value: { } secondValue } => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    secondValue,
                    null),
                _ => new TwoValuesResult<TValue, TValue2>(Value, default, null)
            },
            _ => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                new ResultValueNullException("out of range"))
        };
    public async Task<TwoValuesResult<TValue, TValue2>> CombineValue<TValue2>(
        Func<TValue, Task<SingleValueResult<TValue2>>> secondValueFunc)
        where TValue2 : notnull
        => this switch
        {
            { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                e.Exception),
            { Value: { } value } => await secondValueFunc(value) switch
            {
                { Exception: not null } e => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    default,
                    e.Exception),
                { Value: { } secondValue } => new TwoValuesResult<TValue, TValue2>(
                    Value,
                    secondValue,
                    null),
                _ => new TwoValuesResult<TValue, TValue2>(Value, default, null)
            },
            _ => new TwoValuesResult<TValue, TValue2>(
                Value,
                default,
                new ResultValueNullException("out of range"))
        };
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
    public static SingleValueResult<TValue> WrapTry(Func<SingleValueResult<TValue>> func)
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

    public SingleValueResult<TValue2> Railway<TValue2>(
        Func<TValue, SingleValueResult<TValue2>> handleValueFunc)
        where TValue2 : notnull
        => this
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => handleValueFunc(value),
                _ => SingleValueResult<TValue2>.OutOfRange
            };

    public async Task<SingleValueResult<TValue2>> Railway<TValue2>(
        Func<TValue, Task<SingleValueResult<TValue2>>> handleValueFunc)
        where TValue2 : notnull
        => this
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => await handleValueFunc(value),
                _ => SingleValueResult<TValue2>.OutOfRange
            };

    public SingleValueResult<TValue2> RailwayWrapTry<TValue2>(Func<TValue, TValue2> handleValueFunc)
        where TValue2 : notnull
        =>
            this switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => SingleValueResult<TValue2>.WrapTry(
                    () => handleValueFunc(value)),
                _ => SingleValueResult<TValue2>.OutOfRange
            };

    public static SingleValueResult<TValue> Railway<TValue2>(
        Func<SingleValueResult<TValue2>> func,
        Func<TValue2, SingleValueResult<TValue>> handleValueFunc)
        where TValue2 : notnull
        => func()
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => handleValueFunc(value),
                _ => OutOfRange
            };
    public static SingleValueResult<TValue> Railway<TValue2>(
        SingleValueResult<TValue2> firstValueResult,
        Func<TValue2, SingleValueResult<TValue>> handleValueFunc)
        where TValue2 : notnull
        => firstValueResult
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value } => handleValueFunc(value),
                _ => OutOfRange
            };

    public static SingleValueResult<TValue> Railway2Combine<TValue1, TValue2>(
        Func<SingleValueResult<TValue1>> func1,
        Func<SingleValueResult<TValue2>> func2,
        Func<TValue1, TValue2, SingleValueResult<TValue>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => func1()
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value1 } => func2() switch
                {
                    { Exception: not null } e => e.Exception,
                    { Value: { } value2 } => handleValueFunc(value1, value2),
                    _ => OutOfRange
                },
                _ => OutOfRange
            };
    public static SingleValueResult<TValue> Railway2Combine<TValue1, TValue2>(
        SingleValueResult<TValue1> firstValue,
        SingleValueResult<TValue2> secondValue,
        Func<TValue1, TValue2, SingleValueResult<TValue>> handleValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => firstValue
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value: { } value1 } => secondValue switch
                {
                    { Exception: not null } e => e.Exception,
                    { Value: { } value2 } => handleValueFunc(value1, value2),
                    _ => OutOfRange
                },
                _ => OutOfRange
            };
}

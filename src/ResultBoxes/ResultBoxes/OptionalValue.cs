using System.Text.Json.Serialization;
namespace ResultBoxes;

public record OptionalValue<TValue>(TValue? Value, bool HasValue = true)
{
    public static OptionalValue<TValue> Empty => new(default, false);
    public static OptionalValue<TValue> Null => new(default, false);
    public TValue GetValue() => HasValue && Value is not null ? Value : throw new ResultsInvalidOperationException("no value");
    public static implicit operator OptionalValue<TValue>(TValue value) => new(value);
    public static OptionalValue<TValue> FromValue(TValue value) => new(value);
}
public static class OptionalValue
{
    public static OptionalValue<TValue> FromValue<TValue>(TValue value) where TValue : notnull => new(value);
    public static async Task<OptionalValue<TValue>> FromValue<TValue>(Task<TValue> value) where TValue : notnull => new(await value);
    public static OptionalValue<TValue> FromNullableValue<TValue>(TValue? value) where TValue : notnull => value is null ? OptionalValue<TValue>.Null : new(value);
    public static async Task<OptionalValue<TValue>> FromNullableValue<TValue>(Task<TValue?> value) where TValue : notnull => OptionalValue.FromNullableValue(await value);
    public static OptionalValue<TValue> FromNullableValue<TValue>(TValue? value) where TValue : struct => value.HasValue ? new(value.Value) : OptionalValue<TValue>.Null;

    public static async Task<OptionalValue<TValue>> FromNullableValue<TValue>(Task<TValue?> value) where TValue : struct => OptionalValue.FromNullableValue(await value);
    public static  ResultBox<TValue> GetValueResult<TValue>(this OptionalValue<TValue> optional) where TValue : notnull => optional.HasValue && optional.Value is not null ? optional.Value : new ResultsInvalidOperationException("no value");
}

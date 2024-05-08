using System.Text.Json.Serialization;
namespace SingleResults;

public record OptionalValue<TValue>(TValue? Value)
{
    [JsonIgnore]
    public bool HasValue => Value is not null;
    public TValue GetValue() => Value ?? throw new ResultsInvalidOperationException("no value");
    public static implicit operator OptionalValue<TValue>(TValue value) => new(value);

    public static OptionalValue<TValue> Empty => new OptionalValue<TValue>(default);
    public static OptionalValue<TValue> FromValue(TValue value) => new OptionalValue<TValue>(value);
}
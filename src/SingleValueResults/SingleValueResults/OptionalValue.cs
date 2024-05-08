using System.Text.Json.Serialization;
namespace SingleResults;

public record OptionalValue<TValue>(TValue? Value)
{
    [JsonIgnore]
    public bool HasValue => Value is not null;
    public TValue GetValue() => Value ?? throw new ResultsInvalidOperationException("no value");
}
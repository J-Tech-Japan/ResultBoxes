using System.Text.Json.Serialization;
namespace SingleResults;

public record ThreeValuesResult<TValue1, TValue2, TValue3>(
    TValue1? Value1,
    TValue2? Value2,
    TValue3? Value3,
    Exception? Exception)
{
    [JsonIgnore]
    public bool IsSuccess => Exception is null;
    public Exception GetException() =>
        Exception ?? throw new ResultsInvalidOperationException("no exception");
    public TValue1 GetValue1() =>
        (IsSuccess ? Value1 : throw new ResultsInvalidOperationException("no value")) ??
        throw new ResultsInvalidOperationException();
    public TValue2 GetValue2() =>
        (IsSuccess ? Value2 : throw new ResultsInvalidOperationException("no value")) ??
        throw new ResultsInvalidOperationException();
    public TValue3 GetValue3() =>
        (IsSuccess ? Value3 : throw new ResultsInvalidOperationException("no value")) ??
        throw new ResultsInvalidOperationException();

    public SingleValueResult<TValue4> Railway<TValue4>(
        Func<TValue1, TValue2, TValue3, SingleValueResult<TValue4>> handleValueFunc) => this
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value1: { } value1, Value2: { } value2, Value3: { } value3 } => handleValueFunc(
                value1,
                value2,
                value3),
            _ => SingleValueResult<TValue4>.OutOfRange
        };
}

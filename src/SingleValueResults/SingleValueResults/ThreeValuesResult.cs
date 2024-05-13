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
        Func<TValue1, TValue2, TValue3, SingleValueResult<TValue4>> handleValueFunc)
        where TValue4 : notnull
        => this
            switch
            {
                { Exception: not null } e => e.Exception,
                { Value1: { } value1, Value2: { } value2, Value3: { } value3 } => handleValueFunc(
                    value1,
                    value2,
                    value3),
                _ => SingleValueResult<TValue4>.OutOfRange
            };
    public FourValuesResult<TValue1, TValue2, TValue3, TValue4> CombineValue<TValue4>(
        SingleValueResult<TValue4> fourthValue)
        where TValue4 : notnull
        => this switch
        {
            { Exception: not null } e => new FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                Value1,
                Value2,
                Value3,
                default,
                e.Exception),
            { Value1: not null, Value2: not null, Value3: not null } => fourthValue switch
            {
                { Exception: not null } e => new
                    FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                        Value1,
                        Value2,
                        Value3,
                        default,
                        e.Exception),
                { Value: not null } => new FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                    Value1,
                    Value2,
                    Value3,
                    fourthValue.Value,
                    null),
                _ => new FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                    Value1,
                    Value2,
                    Value3,
                    default,
                    new ResultValueNullException("out of range"))
            },
            _ => new FourValuesResult<TValue1, TValue2, TValue3, TValue4>(
                Value1,
                Value2,
                Value3,
                default,
                new ResultValueNullException("out of range"))
        };
}

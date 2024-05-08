using System.Text.Json.Serialization;
namespace SingleResults;

public record TwoValuesResult<TValue1, TValue2>(
    TValue1? Value1,
    TValue2? Value2,
    Exception? Exception)
{
    [JsonIgnore]
    public bool IsSuccess => Exception is null;
    public Exception GetException() =>
        Exception ?? throw new ResultsInvalidOperationException("no exception");
    public TValue1 GetValue1() =>
        (IsSuccess ? Value1 : throw new ResultsInvalidOperationException("no value")) ??
        throw new ResultsInvalidOperationException();

    public SingleValueResult<TValue3> Railway<TValue3>(
        Func<TValue1, TValue2, SingleValueResult<TValue3>> handleValueFunc) => this
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value1: { } value1, Value2: { } value2 } => handleValueFunc(value1, value2),
            _ => SingleValueResult<TValue3>.OutOfRange
        };
    public async Task<SingleValueResult<TValue3>> RailwayAsync<TValue3>(
        Func<TValue1, TValue2, Task<SingleValueResult<TValue3>>> handleValueFunc) => this
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value1: { } value1, Value2: { } value2 } => await handleValueFunc(value1, value2),
            _ => SingleValueResult<TValue3>.OutOfRange
        };
    public async Task<SingleValueResult<TValue3>> RailwayAsyncWrapTry<TValue3>(
        Func<TValue1, TValue2, Task<TValue3>> handleValueFunc) => this
        switch
        {
            { Exception: not null } e => SingleValueResult<TValue3>.FromException(e.Exception),
            { Value1: { } value1, Value2: { } value2 } => await SingleValueResult<TValue3>
                .WrapTryAsync(
                    () => handleValueFunc(value1, value2)),
            _ => SingleValueResult<TValue3>.OutOfRange
        };

    public SingleValueResult<TValue3> RailwayWrapTry<TValue3>(
        Func<TValue1, TValue2, TValue3> handleValueFunc) => this
        switch
        {
            { Exception: not null } e => e.Exception,
            { Value1: { } value1, Value2: { } value2 } => SingleValueResult<TValue3>.WrapTry(
                () => handleValueFunc(value1, value2)),
            _ => SingleValueResult<TValue3>.OutOfRange
        };

    public ThreeValuesResult<TValue1, TValue2, TValue3> CombineValue<TValue3>(
        SingleValueResult<TValue3> thirdValue) => this switch
    {
        { Exception: not null } e => new ThreeValuesResult<TValue1, TValue2, TValue3>(
            Value1,
            Value2,
            default,
            e.Exception),
        { Value1: not null, Value2: not null } => thirdValue switch
        {
            { Exception: not null } e => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                Value1,
                Value2,
                default,
                e.Exception),
            { Value: not null } => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                Value1,
                Value2,
                thirdValue.Value,
                null),
            _ => new ThreeValuesResult<TValue1, TValue2, TValue3>(Value1, Value2, default, null)
        },
        _ => new ThreeValuesResult<TValue1, TValue2, TValue3>(
            Value1,
            Value2,
            default,
            new ResultValueNullException("out of range"))
    };

    public ThreeValuesResult<TValue1, TValue2, TValue3> CombineValue<TValue3>(
        Func<TValue1, TValue2, SingleValueResult<TValue3>> thirdValueFunc) => this switch
    {
        { Exception: not null } e => new ThreeValuesResult<TValue1, TValue2, TValue3>(
            Value1,
            Value2,
            default,
            e.Exception),
        { Value1: { } value1, Value2: { } value2 } => thirdValueFunc(value1, value2) switch
        {
            { Exception: not null } e => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                Value1,
                Value2,
                default,
                e.Exception),
            { Value: { } value3 } => new ThreeValuesResult<TValue1, TValue2, TValue3>(
                Value1,
                Value2,
                value3,
                null),
            _ => new ThreeValuesResult<TValue1, TValue2, TValue3>(Value1, Value2, default, null)
        },
        _ => new ThreeValuesResult<TValue1, TValue2, TValue3>(
            Value1,
            Value2,
            default,
            new ResultValueNullException("out of range"))
    };
}

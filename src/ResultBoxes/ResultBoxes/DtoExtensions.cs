namespace ResultBoxes;

public static class DtoExtensions
{
    public static ResultDto<TValue> ToDto<TValue>(this ResultBox<TValue> result) where TValue : notnull =>
        new(result.Value, result.Exception, result.IsSuccess);
    public static async Task<ResultDto<TValue>> ToDto<TValue>(this Task<ResultBox<TValue>> result) where TValue : notnull =>
        (await result).ToDto();
    public static ResultBox<TValue> ToBox<TValue>(this ResultDto<TValue> result)
        where TValue : notnull =>
        result is { IsSuccess: true, Value: not null } ? ResultBox.FromValue(result.Value)
            : ResultBox.FromException<TValue>(result.Exception ?? new ResultValueNullException());
    public static async Task<ResultBox<TValue>> ToBox<TValue>(this Task<ResultDto<TValue>> result)
        where TValue : notnull =>
        (await result).ToBox();
}
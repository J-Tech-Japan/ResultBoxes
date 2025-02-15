namespace ResultBoxes;

public static class CastExtensions
{
    public static ResultBox<TCasted> Cast<TOriginal, TCasted>(this ResultBox<TOriginal> resultBox)
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
    public static async Task<ResultBox<TCasted>> Cast<TOriginal, TCasted>(this Task<ResultBox<TOriginal>> resultBoxTask)
        where TCasted : notnull where TOriginal : notnull
    {
        var resultBox = await resultBoxTask;
        if (resultBox.IsSuccess)
        {
            return resultBox.GetValue() is TCasted castedValue
                ? ResultBox<TCasted>.FromValue(castedValue)
                : ResultBox<TCasted>.Error(new InvalidCastException($"Cannot cast value to {typeof(TCasted).Name}"));
        }
        return ResultBox<TCasted>.FromException(resultBox.GetException());
    }
}

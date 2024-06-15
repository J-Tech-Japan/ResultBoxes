namespace ResultBoxes;

public static class ToTaskExtensions
{
    public static Task<ResultBox<TValue>> ToTask<TValue>(this ResultBox<TValue> result) where TValue : notnull =>
        Task.FromResult(result);
}
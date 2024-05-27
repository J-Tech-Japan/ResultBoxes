namespace ResultBoxes;

public static class LogExtensions
{
    public static ResultBox<TValue> Log<TValue>(
        this ResultBox<TValue> result,
        string marking = "")
        where TValue : notnull => result.ScanResult(_ => ResultBox.LogResult(result, marking));

    public static async Task<ResultBox<TValue>> Log<TValue>(
        this Task<ResultBox<TValue>> resultAsync,
        string marking = "")
        where TValue : notnull =>
        (await resultAsync).ScanResult(result => ResultBox.LogResult(result, marking));
}
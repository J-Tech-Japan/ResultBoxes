using ResultBoxes;
namespace SingleResults.Usage;

public static class FourValueCase
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        1000 => new ApplicationException($"{target} is too large."),
        _ => target + 1
    };
    public static Task<ResultBox<int>> IncrementAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                1000 => new ApplicationException($"{target} is too large."),
                _ => target + 1
            });

    public static ResultBox<int> AddAndDivide4(
        int numerator1,
        int numerator2,
        int denominator1,
        int denominator2) => (denominator1 + denominator2) switch
    {
        0 => new ApplicationException("Denominator is zero."),
        _ => (numerator1 + numerator2) / (denominator1 + denominator2)
    };

    public static Task<ResultBox<int>> AddAndDivide4Async(
        int numerator1,
        int numerator2,
        int denominator1,
        int denominator2) =>
        Task.FromResult<ResultBox<int>>(
            (denominator1 + denominator2) switch
            {
                0 => new ApplicationException("Denominator is zero."),
                _ => (numerator1 + numerator2) / (denominator1 + denominator2)
            });


    public static ResultBox<int> Calc3Value(int v1, int v2, int v3, int v4) =>
        Increment(v1)
            .Combine(Increment(v2))
            .Combine(Increment(v3))
            .Combine(Increment(v4))
            .Conveyor(AddAndDivide4);

    public static async Task<ResultBox<int>>
        Calc3ValueAsync(int v1, int v2, int v3, int v4) =>
        await IncrementAsync(v1)
            .Combine(() => IncrementAsync(v2))
            .Combine(() => IncrementAsync(v3))
            .Combine(() => IncrementAsync(v4))
            .Conveyor(AddAndDivide4Async);
}

using ResultBoxes;
namespace SingleResults.Usage;

public static class FiveValueCase
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

    public static ResultBox<int> AddAndDivide5(
        int numerator1,
        int numerator2,
        int numerator3,
        int denominator1,
        int denominator2) => (denominator1 + denominator2) switch
    {
        0 => new ApplicationException("Denominator is zero."),
        _ => (numerator1 + numerator2 + numerator3) / (denominator1 + denominator2)
    };

    public static Task<ResultBox<int>> AddAndDivide5Async(
        int numerator1,
        int numerator2,
        int numerator3,
        int denominator1,
        int denominator2) =>
        Task.FromResult<ResultBox<int>>(
            (denominator1 + denominator2) switch
            {
                0 => new ApplicationException("Denominator is zero."),
                _ => (numerator1 + numerator2 + numerator3) / (denominator1 + denominator2)
            });


    public static ResultBox<int> Calc3Value(int v1, int v2, int v3, int v4, int v5) =>
        Increment(v1)
            .Combine(Increment(v2))
            .Combine(Increment(v3))
            .Combine(Increment(v4))
            .Combine(Increment(v5))
            .Conveyor(AddAndDivide5);

    public static async Task<ResultBox<int>>
        Calc3ValueAsync(int v1, int v2, int v3, int v4, int v5) =>
        await IncrementAsync(v1)
            .Combine(() => IncrementAsync(v2))
            .Combine(() => IncrementAsync(v3))
            .Combine(() => IncrementAsync(v4))
            .Combine(() => IncrementAsync(v5))
            .Conveyor(AddAndDivide5Async);
}

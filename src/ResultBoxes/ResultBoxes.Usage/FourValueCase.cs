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
            .CombineValue(Increment(v2))
            .CombineValue(Increment(v3))
            .CombineValue(Increment(v4))
            .Conveyor(AddAndDivide4);

    public static async Task<ResultBox<int>>
        Calc3ValueAsync(int v1, int v2, int v3, int v4) =>
        await IncrementAsync(v1)
            .CombineValue(() => IncrementAsync(v2))
            .CombineValue(() => IncrementAsync(v3))
            .CombineValue(() => IncrementAsync(v4))
            .Conveyor(AddAndDivide4Async);
}

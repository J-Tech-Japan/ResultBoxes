namespace SingleResults.Usage;

public static class FiveValueCase
{
    public static SingleValueResult<int> Increment(int target) => target switch
    {
        1000 => new ApplicationException($"{target} is too large."),
        _ => target + 1
    };
    public static Task<SingleValueResult<int>> IncrementAsync(int target) =>
        Task.FromResult<SingleValueResult<int>>(
            target switch
            {
                1000 => new ApplicationException($"{target} is too large."),
                _ => target + 1
            });

    public static SingleValueResult<int> AddAndDivide5(
        int numerator1,
        int numerator2,
        int numerator3,
        int denominator1,
        int denominator2) => (denominator1 + denominator2) switch
    {
        0 => new ApplicationException("Denominator is zero."),
        _ => (numerator1 + numerator2 + numerator3) / (denominator1 + denominator2)
    };

    public static Task<SingleValueResult<int>> AddAndDivide5Async(
        int numerator1,
        int numerator2,
        int numerator3,
        int denominator1,
        int denominator2) =>
        Task.FromResult<SingleValueResult<int>>(
            (denominator1 + denominator2) switch
            {
                0 => new ApplicationException("Denominator is zero."),
                _ => (numerator1 + numerator2 + numerator3) / (denominator1 + denominator2)
            });


    public static SingleValueResult<int> Calc3Value(int v1, int v2, int v3, int v4, int v5) =>
        Increment(v1)
            .CombineValue(Increment(v2))
            .CombineValue(Increment(v3))
            .CombineValue(Increment(v4))
            .CombineValue(Increment(v5))
            .Railway(AddAndDivide5);

    public static async Task<SingleValueResult<int>>
        Calc3ValueAsync(int v1, int v2, int v3, int v4, int v5) =>
        await IncrementAsync(v1)
            .CombineValue(() => IncrementAsync(v2))
            .CombineValue(() => IncrementAsync(v3))
            .CombineValue(() => IncrementAsync(v4))
            .CombineValue(() => IncrementAsync(v5))
            .Railway(AddAndDivide5Async);
}

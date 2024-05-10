namespace SingleResults.Usage;

public static class ThreeValueCase
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

    public static SingleValueResult<int> AddAndDivide(
        int numerator1,
        int numerator2,
        int denominator) => denominator switch
    {
        0 => new ApplicationException("Denominator is zero."),
        _ => (numerator1 + numerator2) / denominator
    };

    public static Task<SingleValueResult<int>> AddAndDivideAsync(
        int numerator1,
        int numerator2,
        int denominator) =>
        Task.FromResult<SingleValueResult<int>>(
            denominator switch
            {
                0 => new ApplicationException("Denominator is zero."),
                _ => (numerator1 + numerator2) / denominator
            });


    public static SingleValueResult<int> Calc3Value(int v1, int v2, int v3) =>
        Increment(v1)
            .CombineValue(Increment(v2))
            .CombineValue(Increment(v3))
            .Railway(AddAndDivide);

    public static async Task<SingleValueResult<int>> Calc3ValueAsync(int v1, int v2, int v3) =>
        await IncrementAsync(v1)
            .CombineValue(() => IncrementAsync(v2))
            .CombineValue(() => IncrementAsync(v3))
            .Railway(AddAndDivideAsync);
}

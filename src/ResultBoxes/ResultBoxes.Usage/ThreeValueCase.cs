using ResultBoxes;
namespace SingleResults.Usage;

public static class ThreeValueCase
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

    public static ResultBox<int> AddAndDivide(
        int numerator1,
        int numerator2,
        int denominator) => denominator switch
    {
        0 => new ApplicationException("Denominator is zero."),
        _ => (numerator1 + numerator2) / denominator
    };

    public static Task<ResultBox<int>> AddAndDivideAsync(
        int numerator1,
        int numerator2,
        int denominator) =>
        Task.FromResult<ResultBox<int>>(
            denominator switch
            {
                0 => new ApplicationException("Denominator is zero."),
                _ => (numerator1 + numerator2) / denominator
            });


    public static ResultBox<int> Calc3Value(int v1, int v2, int v3) =>
        Increment(v1)
            .Combine(Increment(v2))
            .Combine(Increment(v3))
            .Conveyor(AddAndDivide);

    public static async Task<ResultBox<int>> Calc3ValueAsync(int v1, int v2, int v3) =>
        await IncrementAsync(v1)
            .Combine(() => IncrementAsync(v2))
            .Combine(() => IncrementAsync(v3))
            .Conveyor(AddAndDivideAsync);
}

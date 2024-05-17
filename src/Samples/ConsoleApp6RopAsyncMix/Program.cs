using ResultBoxes;
namespace ConsoleApp6RopAsyncMix;

internal class Program
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        > 1000 => new ApplicationException($"{target} is not allowed for {nameof(Increment)}"),
        _ => target + 1
    };
    public static ResultBox<int> Double(int target) => target switch
    {
        > 1000 => new ApplicationException($"{target} is not allowed for {nameof(Double)}"),
        _ => target * 2
    };
    public static ResultBox<int> Triple(int target) => target switch
    {
        > 1000 => new ApplicationException($"{target} is not allowed for {nameof(Triple)}"),
        _ => target * 3
    };

    public static Task<ResultBox<int>> IncrementAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(IncrementAsync)}"),
                _ => target + 1
            });
    public static Task<ResultBox<int>> DoubleAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(DoubleAsync)}"),
                _ => target * 2
            });
    public static Task<ResultBox<int>> TripleAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(TripleAsync)}"),
                _ => target * 3
            });

    private static async Task Main(string[] args)
    {
        // Error: System.ApplicationException: 1001 is not allowed for IncrementAsync
        await Increment(1001)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();

        // Error: System.ApplicationException: 1001 is not allowed for DoubleAsync
        await IncrementAsync(1000)
            .Conveyor(Double)
            .Conveyor(TripleAsync)
            .Log();
        // Error: System.ApplicationException: 1202 is not allowed for TripleAsync
        await IncrementAsync(600)
            .Conveyor(DoubleAsync)
            .Conveyor(Triple)
            .Log();
        // Value: 24
        await IncrementAsync(3)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();
    }
}

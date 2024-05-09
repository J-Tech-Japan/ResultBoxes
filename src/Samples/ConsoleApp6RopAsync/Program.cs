using SingleResults;
namespace ConsoleApp6RopAsync;

class Program
{
    public static Task<SingleValueResult<int>> IncrementAsync(int target) =>
        Task.FromResult<SingleValueResult<int>>(target switch
            {
                > 1000 => new ApplicationException($"{target} is not allowed for {nameof(IncrementAsync)}"),
                _ => target + 1
            });
    public static Task<SingleValueResult<int>> DoubleAsync(int target) =>
        Task.FromResult<SingleValueResult<int>>(target switch
        {
            > 1000 => new ApplicationException($"{target} is not allowed for {nameof(DoubleAsync)}"),
            _ => target * 2
        });
    public static Task<SingleValueResult<int>> TripleAsync(int target) =>
        Task.FromResult<SingleValueResult<int>>(target switch
        {
            > 1000 => new ApplicationException($"{target} is not allowed for {nameof(TripleAsync)}"),
            _ => target * 3
        });

    static async Task Main(string[] args)
    {
        // Error: System.ApplicationException: 1001 is not allowed for IncrementAsync
        switch (await IncrementAsync(1001).Railway(DoubleAsync).Railway(TripleAsync))
        {
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }
        // Error: System.ApplicationException: 1001 is not allowed for DoubleAsync
        switch (await IncrementAsync(1000).Railway(DoubleAsync).Railway(TripleAsync))
        {
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }
        // Error: System.ApplicationException: 1202 is not allowed for TripleAsync
        switch (await IncrementAsync(600).Railway(DoubleAsync).Railway(TripleAsync))
        {
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }
        // Value: 24
        switch (await IncrementAsync(3).Railway(DoubleAsync).Railway(TripleAsync))
        {
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }
    }
}

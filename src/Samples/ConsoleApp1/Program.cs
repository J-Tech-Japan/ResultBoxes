using SingleResults;
namespace ConsoleApp1;

internal class Program
{
    public static SingleValueResult<int> Increment(int target) => target switch
    {
        > 1000 => new ArgumentOutOfRangeException(nameof(target)),
        _ => target + 1
    };

    private static void Main(string[] args)
    {
        // use switch case to handle Result
        switch (Increment(100))
        {
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            // This will return value result
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }
        switch (Increment(1001))
        {
            // This will return exception result
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }

        Console.WriteLine(RunIncrement(100));
        Console.WriteLine(RunIncrement(1001));
    }

    // use switch expression to handle Result
    private static string RunIncrement(int target) =>
        Increment(target) switch
        {
            { Exception: { } error } => $"Error: {error}",
            { Value: { } value } => $"Value: {value}",
            _ => "Unknown"
        };
}

using ResultBoxes;
namespace ConsoleApp5ROP1;

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


    private static void Main(string[] args)
    {
        // Error: System.ApplicationException: 1001 is not allowed for Increment
        switch (Increment(1001).Railway(Double).Railway(Triple))
        {
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }

        // Error: System.ApplicationException: 1001 is not allowed for Double
        switch (Increment(1000).Railway(Double).Railway(Triple))
        {
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }

        // Error: System.ApplicationException: 1202 is not allowed for Triple
        switch (Increment(600).Railway(Double).Railway(Triple))
        {
            case { Exception: { } error }:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }

        // Value: 24
        switch (Increment(3).Railway(Double).Railway(Triple))
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

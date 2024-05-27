using ResultBoxes;
namespace ConsoleApp1;

internal class Program
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        > 1000 => new ArgumentOutOfRangeException(nameof(target)),
        _ => target + 1
    };

    private static void Main(string[] args)
    {
        // Handle ResultBox<int> with switch expression
        // Value: 101
        var result = Increment(100);
        switch (result)
        {
            case { IsSuccess: false }:
                Console.WriteLine($"Error: {result.GetException().Message}");
                break;
            case { IsSuccess: true }:
                Console.WriteLine($"Value: {result.GetValue()}");
                break;
        }

        // Log() displays value.ToString() if IsSuccess is true, otherwise displays exception.Message
        // case2 Error: Specified argument was out of the range of valid values. (Parameter 'target')
        Increment(1001).Log("case2");

        // RunIncrement() is a method that handles ResultBox<int> with switch expression
        // Value: 101
        Console.WriteLine(RunIncrement(100));

        // Handle ResultBox with if statement
        // Error: Specified argument was out of the range of valid values. (Parameter 'target')
        var result4 = Increment(1001);
        if (result4.IsSuccess)
        {
            Console.WriteLine($"Value: {result4.GetValue()}");
        }
        else
        {
            Console.WriteLine($"Error: {result4.GetException().Message}");
        }

        // Handle ResultBox with ternary operator ?:
        // Value: 2
        var result5 = Increment(1);
        Console.WriteLine(
            result5.IsSuccess ? $"Value: {result5.GetValue()}"
                : $"Error: {result5.GetException().Message}");

    }
    // Handle ResultBox<int> with switch expression
    private static string RunIncrement(int target) =>
        Increment(target) switch
        {
            { IsSuccess: false } error => $"Error: {error.GetException().Message}",
            { IsSuccess: true } success => $"Value: {success.GetValue()}"
        };
}

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
        // use switch case to handle Result
        switch (Increment(100))
        {
            case { IsSuccess: false } error:
                Console.WriteLine("Exception: " + error.GetException().Message);
                break;
            case { IsSuccess: true } value:
                Console.WriteLine("Value: " + value.GetValue());
                break;
        }
        switch (Increment(1001))
        {
            // This will return exception result
            case { IsSuccess: false } error:
                Console.WriteLine("Exception: " + error.GetException().Message);
                break;
            case { IsSuccess: true } value:
                Console.WriteLine("Value: " + value.GetValue());
                break;
        }

        Console.WriteLine(RunIncrement(100));
        Console.WriteLine(RunIncrement(1001));
    }

    // use switch expression to handle Result
    private static string RunIncrement(int target) =>
        Increment(target) switch
        {
            { IsSuccess: false } error => $"Error: {error.GetException()}",
            { IsSuccess: true } success => $"Value: {success.GetValue()}"
        };
}

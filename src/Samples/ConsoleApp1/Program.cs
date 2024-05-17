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
        Increment(100)
            .ScanResult(HandleResult);
        Increment(1001)
            .ScanResult(HandleResult);

        Console.WriteLine(RunIncrement(100));
        Console.WriteLine(RunIncrement(1001));
    }
    private static void HandleResult(ResultBox<int> result)
    {
        switch (result)
        {
            case { IsSuccess: true } success: Console.WriteLine("Value: " + success.GetValue());
                break;
            case { IsSuccess: false } failure: Console.WriteLine("Error: " + failure.GetException().Message);
                break;
        } 
    }

    // use switch expression to handle Result
    private static string RunIncrement(int target) =>
        Increment(target) switch
        {
            { IsSuccess: false } error => $"Error: {error.GetException().Message}",
            { IsSuccess: true } success => $"Value: {success.GetValue()}"
        };
}

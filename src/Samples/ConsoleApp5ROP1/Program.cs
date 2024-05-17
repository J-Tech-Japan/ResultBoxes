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
        Increment(1001)
            .Conveyor(Double)
            .Conveyor(Triple)
            .ScanResult(HandleResult);

        // Error: System.ApplicationException: 1001 is not allowed for Double
        Increment(1000)
            .Conveyor(Double)
            .Conveyor(Triple)
            .ScanResult(HandleResult);

        // Error: System.ApplicationException: 1202 is not allowed for Triple
        Increment(600)
            .Conveyor(Double)
            .Conveyor(Triple)
            .ScanResult(HandleResult);

        // Value: 24
        Increment(3)
            .Conveyor(Double)
            .Conveyor(Triple)
            .ScanResult(HandleResult);
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
}

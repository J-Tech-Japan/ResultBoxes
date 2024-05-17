using ResultBoxes;
namespace ConsoleApp4Unit;

internal class Program
{
    private static void Print(string message)
    {
        switch (message)
        {
            case not null when string.IsNullOrEmpty(message):
                throw new ApplicationException("message is empty");
            default:
                Console.WriteLine(message);
                break;
        }
    }
    private static void Main(string[] args)
    {
        // This will return value (UnitValue) result
        ResultBox<UnitValue>.WrapTry(() => Print("Hello, World!"))
            .ScanResult(HandleResult);

        // This will return exception result
        ResultBox<UnitValue>.WrapTry(() => Print(string.Empty))
            .ScanResult(HandleResult);
    }
    
    public static void HandleResult(ResultBox<UnitValue> result)
    {
        switch (result)
        {
            case { IsSuccess: true } success: Console.WriteLine("Succeed! ");
                break;
            case { IsSuccess: false } failure: Console.WriteLine("Error: " + failure.GetException().Message);
                break;
        }
    }
}

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
        switch (ResultBox<UnitValue>.WrapTry(() => Print("Hello, World!")))
        {
            case { IsSuccess: false } error:
                Console.WriteLine("Exception: " + error.GetException().Message);
                break;
            case { IsSuccess:true }:
                Console.WriteLine("No Exception");
                break;
        }
        // This will return exception result
        switch (ResultBox<UnitValue>.WrapTry(() => Print(string.Empty)))
        {
            case { Exception: { } error }:
                Console.WriteLine("Exception: " + error.Message);
                break;
            default: 
                Console.WriteLine("No Exception");
                break;
        }
    }
}

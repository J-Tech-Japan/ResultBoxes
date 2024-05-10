using SingleResults;
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
        switch (SingleValueResult<UnitValue>.WrapTry(() => Print("Hello, World!")))
        {
            case { Exception: { } error }:
                Console.WriteLine("Exception: " + error.Message);
                break;
            case { Value: not null }:
                Console.WriteLine("No Exception");
                break;
        }

        // This will return exception result
        switch (SingleValueResult<UnitValue>.WrapTry(() => Print(string.Empty)))
        {
            case { Exception: { } error }:
                Console.WriteLine("Exception: " + error.Message);
                break;
            case { Value: not null }:
                Console.WriteLine("No Exception");
                break;
        }
    }
}

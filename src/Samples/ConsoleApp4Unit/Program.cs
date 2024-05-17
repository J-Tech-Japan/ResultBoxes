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
            .Scan(
                value => Console.WriteLine("Value: " + value),
                exception => Console.WriteLine("Exception: " + exception.Message));

        // This will return exception result
        ResultBox<UnitValue>.WrapTry(() => Print(string.Empty))
            .Scan(
                value => Console.WriteLine("Value: " + value),
                exception => Console.WriteLine("Exception: " + exception.Message));
    }
}

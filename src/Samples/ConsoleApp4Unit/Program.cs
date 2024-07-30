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
        UnitValue.WrapTry(() => Print("Hello, World!")).Log();

        // This will return exception result
        UnitValue.WrapTry(() => Print(string.Empty)).Log();
    }
}

using SingleResults;
namespace ConsoleApp3WrapTry;

internal class Program
{
    public static int Divide(int numerator, int denominator) =>
        denominator == 0
            ? throw new ApplicationException("can not divide by 0")
            : numerator / denominator;

    private static void Main(string[] args)
    {
        // This will return exception result
        switch (SingleValueResult<int>.WrapTry(() => Divide(10, 0)))
        {
            case { Exception: not null } exception:
                Console.WriteLine("Exception: " + exception.Exception.Message);
                break;
            case { Value: { } value }:
                Console.WriteLine("Value: " + value);
                break;
        }

        // This will return value result
        switch (SingleValueResult<int>.WrapTry(() => Divide(10, 2)))
        {
            case { Exception: not null } exception:
                Console.WriteLine("Exception: " + exception.Exception.Message);
                break;
            case { Value: { } value }:
                Console.WriteLine("Value: " + value);
                break;
        }

    }
}

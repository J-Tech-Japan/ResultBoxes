using ResultBoxes;
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
        switch (ResultBox<int>.WrapTry(() => Divide(10, 0)))
        {
            case { IsSuccess: false } error:
                Console.WriteLine("Exception: " + error.GetException().Message);
                break;
            case { IsSuccess: true } value:
                Console.WriteLine("Value: " + value.GetValue());
                break;
        }

        // This will return value result
        switch (ResultBox<int>.WrapTry(() => Divide(10, 2)))
        {
            case { IsSuccess: false } error:
                Console.WriteLine("Exception: " + error.GetException().Message);
                break;
            case { IsSuccess: true } value:
                Console.WriteLine("Value: " + value.GetValue());
                break;
        }

    }
}

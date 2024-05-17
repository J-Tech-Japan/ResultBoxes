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
        ResultBox<int>.WrapTry(() => Divide(10, 0))
            .ScanResult(HandleResult);

        // This will return value result
        ResultBox<int>.WrapTry(() => Divide(10, 2))
            .ScanResult(HandleResult);
    }
    public static void HandleResult(ResultBox<int> result)
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

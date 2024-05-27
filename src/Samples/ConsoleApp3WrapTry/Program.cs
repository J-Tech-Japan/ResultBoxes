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
        // Error: can not divide by 0
        ResultBox.WrapTry(() => Divide(10, 0)).Log();

        // This will return value result
        // Value: 5
        ResultBox.WrapTry(() => Divide(10, 2)).Log();
    }
}

using ResultBoxes;
namespace ConsoleApp8RopWrap;

internal class Program
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        > 1000 => new ApplicationException($"{target} is not allowed for {nameof(Increment)}"),
        _ => target + 1
    };
    public static int IncrementWithThrowing(int target) => target switch
    {
        > 1000 => throw new ApplicationException(
            $"{target} is not allowed for {nameof(Increment)}"),
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
    public static int TripleWithThrowing(int target) => target switch
    {
        > 1000 => throw new ApplicationException($"{target} is not allowed for {nameof(Triple)}"),
        _ => target * 3
    };

    private static void Main(string[] args)
    {
        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Calculate (1 + 1) * 2 * 3 = 12
        // Value1: 12
        ResultBox<int>.WrapTry(() => IncrementWithThrowing(1))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing)
            .Tap(
                value => Console.WriteLine("Value: " + value),
                exception => Console.WriteLine("Exception: " + exception.Message));

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error2: System.ApplicationException: 2000 is not allowed for Increment
        ResultBox<int>.WrapTry(() => IncrementWithThrowing(2000))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing)
            .Tap(
                value => Console.WriteLine("Value: " + value),
                exception => Console.WriteLine("Exception: " + exception.Message));

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error3: System.ApplicationException: 1001 is not allowed for Double
        ResultBox<int>.WrapTry(() => IncrementWithThrowing(1000))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing)
            .Tap(
                value => Console.WriteLine("Value: " + value),
                exception => Console.WriteLine("Exception: " + exception.Message));

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error4: System.ApplicationException: 1202 is not allowed for Triple
        ResultBox<int>.WrapTry(() => IncrementWithThrowing(600))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing)
            .Tap(
                value => Console.WriteLine("Value: " + value),
                exception => Console.WriteLine("Exception: " + exception.Message));
    }
}

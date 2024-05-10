using SingleResults;
namespace ConsoleApp8RopWrap;

internal class Program
{
    public static SingleValueResult<int> Increment(int target) => target switch
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
    public static SingleValueResult<int> Double(int target) => target switch
    {
        > 1000 => new ApplicationException($"{target} is not allowed for {nameof(Double)}"),
        _ => target * 2
    };
    public static SingleValueResult<int> Triple(int target) => target switch
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
        switch (SingleValueResult<int>.WrapTry(() => IncrementWithThrowing(1))
            .Railway(Double)
            .RailwayWrapTry(TripleWithThrowing))
        {
            case { Exception: { } error1 }:
                Console.WriteLine($"Error1: {error1}");
                break;
            case { Value: { } value1 }:
                Console.WriteLine($"Value1: {value1}");
                break;
        }

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error2: System.ApplicationException: 2000 is not allowed for Increment
        switch (SingleValueResult<int>.WrapTry(() => IncrementWithThrowing(2000))
            .Railway(Double)
            .RailwayWrapTry(TripleWithThrowing))
        {
            case { Exception: { } error2 }:
                Console.WriteLine($"Error2: {error2}");
                break;
            case { Value: { } value2 }:
                Console.WriteLine($"Value2: {value2}");
                break;
        }

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error3: System.ApplicationException: 1001 is not allowed for Double
        switch (SingleValueResult<int>.WrapTry(() => IncrementWithThrowing(1000))
            .Railway(Double)
            .RailwayWrapTry(TripleWithThrowing))
        {
            case { Exception: { } error3 }:
                Console.WriteLine($"Error3: {error3}");
                break;
            case { Value: { } value3 }:
                Console.WriteLine($"Value3: {value3}");
                break;
        }

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error4: System.ApplicationException: 1202 is not allowed for Triple
        switch (SingleValueResult<int>.WrapTry(() => IncrementWithThrowing(600))
            .Railway(Double)
            .RailwayWrapTry(TripleWithThrowing))
        {
            case { Exception: { } error4 }:
                Console.WriteLine($"Error4: {error4}");
                break;
            case { Value: { } value4 }:
                Console.WriteLine($"Value4: {value4}");
                break;
        }
    }
}

using ResultBoxes;
namespace ConsoleApp7RopCombine;

internal class Program
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        > 1000 => new ApplicationException(
            $"{target} can not use for the {nameof(Increment)}. It should be under or equal 1000"),
        _ => target + 1
    };
    public static ResultBox<int> Add(int target1, int target2) => target1 switch
    {
        > 100 => new ApplicationException($"over 100 is not allowed for {nameof(Add)}"),
        _ => target1 + target2
    };
    public static ResultBox<int> Divide(int numerator, int denominator) =>
        (numerator, denominator) switch
        {
            (_, 0) => new ApplicationException("can not divide by 0"),
            _ => numerator / denominator
        };

    private static void Main(string[] args)
    {
        // Pattern 1 : Use CombineValue method chain
        // calculate answer = (29 + 1) / (1 + 9) = 3
        // Value: 3
        switch (Increment(29)
            .CombineValue(Add(1, 9))
            .Railway(Divide))
        {
            case { Exception: { } error }:
                Console.WriteLine("Exception2: " + error.Message);
                break;
            case { Value: var value }:
                Console.WriteLine("Value2: " + value);
                break;
        }

        // Pattern 2 : Error in Increment method (target > 1000)
        // Exception3: 2000 can not use for the Increment. It should be under or equal 1000
        switch (Increment(2000)
            .CombineValue(Add(1, 9))
            .Railway(Divide))
        {
            case { Exception: { } error }:
                Console.WriteLine("Exception3: " + error.Message);
                break;
            case { Value: var value }:
                Console.WriteLine("Value3: " + value);
                break;
        }

        // Pattern 4 : Error in Add method (target1 > 100)
        // Exception4: over 100 is not allowed for Add
        switch (Increment(19)
            .CombineValue(Add(1000, 9))
            .Railway(Divide))
        {
            case { Exception: { } error }:
                Console.WriteLine("Exception4: " + error.Message);
                break;
            case { Value: var value }:
                Console.WriteLine("Value4: " + value);
                break;
        }

        // Pattern 5 : Error in Divide method (denominator <> 0)
        // Exception5: can not divide by 0
        switch (Increment(19)
            .CombineValue(Add(0, 0))
            .Railway(Divide))
        {
            case { Exception: { } error }:
                Console.WriteLine("Exception5: " + error.Message);
                break;
            case { Value: var value }:
                Console.WriteLine("Value5: " + value);
                break;
        }
    }
}

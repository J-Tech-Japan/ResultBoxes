using SingleResults;
namespace ConsoleApp2Optional;

internal class Program
{
    public static SingleValueResult<OptionalValue<string>> ConvertStringToHalfLength(string input)
        => input.Length switch
        {
            0 => new ApplicationException("Input string is empty"), // Exception
            1 => OptionalValue<string>.Empty, // Not error but Empty 
            _ => OptionalValue<string>.FromValue(input[..^(input.Length / 2)]) // has value 
        };

    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please input a string.");
            return;
        }
        var result = ConvertStringToHalfLength(args[0]);
        switch (result)
        {
            case { Exception: { } error }:
                Console.WriteLine("Exception: " + error.Message);
                break;
            case { Value : { HasValue: true } value }: // When OptionalValue has value
                Console.WriteLine("Value: " + value.Value);
                break;
            case { Value : { HasValue: false } }: // When OptionalValue is empty
                Console.WriteLine("No value");
                break;
        }
    }
}

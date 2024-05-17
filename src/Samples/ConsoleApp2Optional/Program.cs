using ResultBoxes;
namespace ConsoleApp2Optional;

internal class Program
{
    public static ResultBox<OptionalValue<string>> ConvertStringToHalfLength(string input)
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
            case { IsSuccess: false} error:
                Console.WriteLine("Exception: " + error.GetException().Message);
                break;
            case { IsSuccess: true } value when value.GetValue().HasValue: // When OptionalValue has value
                Console.WriteLine("Value: " + value.GetValue().Value);
                break;
            case { IsSuccess: true } value when !value.GetValue().HasValue: // When OptionalValue has value
                Console.WriteLine("No value");
                break;
        }
    }
}

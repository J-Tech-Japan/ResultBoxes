using ResultBoxes;
namespace ConsoleApp2Optional;

internal class Program
{
    public static ResultBox<OptionalValue<string>> ConvertStringToHalfLength(string input)
        => input.Length switch
        {
            0 => new ApplicationException("Input string is empty"), // Exception
            1 => OptionalValue<string>.Empty, // Not error but Empty 
            _ => OptionalValue.FromValue(input[..^(input.Length / 2)]) // has value 
        };
    private static void Main(string[] args)
    {
        // Error: Input string is empty
        ConvertStringToHalfLength("").Log();

        // Value: OptionalValue { Value = , HasValue = False }
        ConvertStringToHalfLength("H").Log();

        // Value: OptionalValue { Value = Hel, HasValue = True }
        ConvertStringToHalfLength("Hello").Log();
    }
}

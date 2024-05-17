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
        ConvertStringToHalfLength("")
            .ScanResult(HandleResult);
        ConvertStringToHalfLength("H")
            .ScanResult(HandleResult);
        ConvertStringToHalfLength("Hello")
            .ScanResult(HandleResult);
    }
    private static void HandleResult(ResultBox<OptionalValue<string>> result)
    {
        switch (result)
        {
            case { IsSuccess: true } success when success.GetValue().HasValue: Console.WriteLine("Value: " + success.GetValue().Value);
                break;
            case { IsSuccess: true } success when !success.GetValue().HasValue: Console.WriteLine("No Value");
                break;
            case { IsSuccess: false } failure: Console.WriteLine("Error: " + failure.GetException().Message);
                break;
        } 
    }
}

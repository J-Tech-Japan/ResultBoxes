# SingleValueResults

C# Results Library that focus on Railway Oriented Programming.

# How can you install?

```sh
dotnet add package SingleValueResults --version 0.1.1-alpha
```

# Why Result type?

Some language, especially functional language has custom to use `Result` type, that express return type of the function
is either return value or Error.

There is of course pros and cons of result type.

**Pros**

- Readability and Explicit Error Handling.
- Slow "throw and catch" speed.
- Functional Style programming.
- Fully use of power of pattern matching.

**Cons**

- Not build in language feature.
- C# is not pure functional language.
- Complex for someone not used to.

This SingleValueResult try to be simple Result type, that fully use lately introduced pattern matching feature. And
first class support of the `Railway Oriented Programming` that introduced with Scott Wlaschin with following article.

[Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)

# Usage

## 1. Simple Function and Use Result Function

Basic use for this library is use `SingleValueResult<T>` for the return type of the functions.

Then you can return value when success, and when you have any issue, you can **return** exception. (not throw.)

Like example below, you can either return **Value itself** or **Exception**, and implicit operation can convert it
to `SingleValueResult<T>` class in code.

```csharp

internal class Program
{
    public static SingleValueResult<int> Increment(int target) => target switch
    {
        > 1000 => new ArgumentOutOfRangeException(nameof(target)),
        _ => target + 1
    };

    private static void Main(string[] args)
    {
        // use switch case to handle Result
        switch (Increment(100))
        {
            case { Exception: { } error } :
                Console.WriteLine($"Error: {error}");
                break;
            // This will return value result
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }
        switch (Increment(1001))
        {
            // This will return exception result
            case { Exception: not null } error:
                Console.WriteLine($"Error: {error}");
                break;
            case { Value: { } value }:
                Console.WriteLine($"Value: {value}");
                break;
        }

        Console.WriteLine(RunIncrement(100));
        Console.WriteLine(RunIncrement(1001));
    }

    // use switch expression to handle Result
    private static string RunIncrement(int target) =>
        Increment(target) switch
        {
            { Exception: { } error } => $"Error: {error}",
            { Value: { } value } => $"Value: {value}",
            _ => "Unknown"
        };
}
```

**Notes**

`SingleValueResult<T>` does have `IsSuccess` property, which returns if it have error or not. But we recommend you to
use `Exception` property and `Value` property for the inspecting result. It is because of the C# feature of
the `pattern matching` can get exception or value without null checking and easily use after.

After `case { Exception: { } error } :`, error is sure of not null, because it checked with pattern matching.
After `case { Value: { } value }:`, value is not null, because it was checked not null with pattern matching as well.

## 2. Don't use nullable value as Type of the Value

`SingleValueResult<TValue>` has `where TValue: notnull` constraint. This is because if it allows null type, it will
allow Value is null and Exception is null Result class. Many feature assume those value as irregular case and not
working with it. How can you write value is null in some case? you can use **OptionalValue** type.

```csharp
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
```

## 3. Wrapping throwing function that returns value.

When I use `Result` type in C# project, often need in mixing with non-result functions which can be throw exception any
time. When this happens, we need to write try/catch and convert throwable functions to `Result`
type. `SingleValueResult` has `WrapTry` function to do this conversion.
When you use `WrapTry`, you need to pass `Func` as the argument.

```csharp

internal class Program
{
    public static int Divide(int numerator, int denominator) =>
        denominator == 0
            ? throw new ApplicationException("can not divide by 0")
            : numerator / denominator;

    private static void Main(string[] args)
    {
        // This will return exception result
        switch (SingleValueResult<int>.WrapTry(() => Divide(10, 0)))
        {
            case { Exception: { } error } :
                Console.WriteLine("Exception: " + error.Message);
                break;
            case { Value: { } value }:
                Console.WriteLine("Value: " + value);
                break;
        }

        // This will return value result
        switch (SingleValueResult<int>.WrapTry(() => Divide(10, 2)))
        {
            case { Exception: { } error } :
                Console.WriteLine("Exception: " + error.Message);
                break;
            case { Value: { } value }:
                Console.WriteLine("Value: " + value);
                break;
        }

    }
}
```

## 4. Wrapping void function.

When a function does not return value, C# can use void as a return (type). But you can not use `SingleValueResult<void>`
due to C# language definition. Instead, we made `UnitValue` type, which means nothing inside but as a data class.
UnitValue does not have any properties. You can wrap try with `WrapTry` void action, and it will
return `SingleValueResult<UnitValue>` type.

```csharp

internal class Program
{
    private static void Print(string message)
    {
        switch (message)
        {
            case not null when string.IsNullOrEmpty(message):
                throw new ApplicationException("message is empty");
                break;
            default:
                Console.WriteLine(message);
                break;
        }
    }

    private static void Main(string[] args)
    {
        // This will return value (UnitValue) result
        switch (SingleValueResult<UnitValue>.WrapTry(() => Print("Hello, World!")))
        {
            case { Exception: { } error } :
                Console.WriteLine("Exception: " + error.Message);
                break;
            case { Value: not null }:
                Console.WriteLine("No Exception");
                break;
        }

        // This will return exception result
        switch (SingleValueResult<UnitValue>.WrapTry(() => Print(string.Empty)))
        {
            case { Exception: { } error } :
                Console.WriteLine("Exception: " + error.Message);
                break;
            case { Value: not null }:
                Console.WriteLine("No Exception");
                break;
        }
    }
}
```

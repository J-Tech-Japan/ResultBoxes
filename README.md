# ResultBoxes

C# Results Library that focus on Railway Oriented Programming.

# How can you install?

```sh
dotnet add package ResultBoxes
```

# Why Result type?

Some language, especially functional language has custom to use `Result` type, that express return type of the function is either return value or Error.

There is of course pros and cons of result type.

**Pros**

- Readability and Explicit Error Handling.
- It is slow to "throw and catch", faster to return as Result.
- Functional Style programming.
- Fully use of power of pattern matching.
- Error handling with simpler code.
- We can focus smaller scopes and focus implement each smaller function.

**Cons**

- Not build in language feature.
- C# is not pure functional language.
- It seems complex for someone not used to the functional programming.

This ResultBoxes try to be simple Result type, that fully use lately introduced pattern matching feature. And first class support of the `Railway Oriented Programming` that introduced with Scott Wlaschin with following article.

[Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)

# Usage

1. Simple Function and Use Result Function
2. Don't use nullable value as Type of the Value
3. Wrapping throwing function that returns value.
4. Wrapping void function.
5. Railway Oriented Programming - Method Chain
6. Railway Oriented Programming - Async Task Functions.
7. Railway Oriented Programming - Combine Value
8. Railway Oriented Programming with Wrapping Function with Try.

## 1. Simple Function and Use Result Function

Basic use for this library is use [ResultBox<T>](https://github.com/J-Tech-Japan/ResultBoxes/blob/main/src/ResultBoxes/ResultBoxes/ResultBox.cs)
for the return type of the functions.

Then you can return value when success, and when you have any issue, you can **return** exception. (not throw.)

Like example below, you can either return **Value itself** or **Exception
**, and implicit operation can convert it to `ResultBox<T>` class in code.

```csharp
internal class Program
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        > 1000 => new ArgumentOutOfRangeException(nameof(target)),
        _ => target + 1
    };

    private static void Main(string[] args)
    {
        // Handle ResultBox<int> with switch expression
        // Value: 101
        var result = Increment(100);
        switch (result)
        {
            case { IsSuccess: false }:
                Console.WriteLine($"Error: {result.GetException().Message}");
                break;
            case { IsSuccess: true }:
                Console.WriteLine($"Value: {result.GetValue()}");
                break;
        }
        
        // Log() displays value.ToString() if IsSuccess is true, otherwise displays exception.Message
        // case2 Error: Specified argument was out of the range of valid values. (Parameter 'target')
        Increment(1001).Log("case2");

        // RunIncrement() is a method that handles ResultBox<int> with switch expression
        // Value: 101
        Console.WriteLine(RunIncrement(100));
        
        // Handle ResultBox with if statement
        // Error: Specified argument was out of the range of valid values. (Parameter 'target')
        var result4 = Increment(1001);
        if (result4.IsSuccess)
        {
            Console.WriteLine($"Value: {result4.GetValue()}");
        }
        else
        {
            Console.WriteLine($"Error: {result4.GetException().Message}");
        }
        
        // Handle ResultBox with ternary operator ?:
        // Value: 2
        var result5 = Increment(1);
        Console.WriteLine(
            result5.IsSuccess ? $"Value: {result5.GetValue()}"
                : $"Error: {result5.GetException().Message}");

    }
    // Handle ResultBox<int> with switch expression
    private static string RunIncrement(int target) =>
        Increment(target) switch
        {
            { IsSuccess: false } error => $"Error: {error.GetException().Message}",
            { IsSuccess: true } success => $"Value: {success.GetValue()}"
        };
}
```

**Notes**

`ResultBox<T>` does have `IsSuccess` property, which returns if it have error or not. We recommend you to use `IsSuccess` for the Pattern Matching handler, and using to check if it was success or not. And like code above, you can get Value with `result.GetValue()` or `result.GetException()` to get Exception.

Keep in mind,

`result.GetValue()` can only get when `IsSuccess: true` otherwise, it throws `ResultsInvalidOperationException`.

`result.GetException()` can only get when  `IsSuccess: false` otherwise, it throws `ResultsInvalidOperationException`.

We use this because in some cases like primitive value `int`, value will be initiated with default value 0. In this case, when Success is false, Exception is not null, and also value is not null. This is why we hide accessing `Value`
property directory from user, and provide `GetValue()` method. Hopefully C# typing system improve and allow us to open up value object to the public...

## 2. Don't use nullable value as Type of the Value

C# has two different "nullable"
types. [Nullable Value Types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types)
and [Nullable Reference Types](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references).

`ResultBox<TValue>` has `where TValue: notnull` constraint. This is because if it allows null type, it will allow Value is null and Exception is null Result class. notnull prevent to use both
**Nullable Value Types** and **Nullable Reference Types**. But for the **Nullable Value Types
** only show warnings because it is wrapped with **Nullable
** generic type. ResultBox designed when Value is null, Value is Empty.

How can you write value is null in some case? You can use [OptionalValue](https://github.com/J-Tech-Japan/ResultBoxes/blob/main/src/ResultBoxes/ResultBoxes/OptionalValue.cs)
type.

```csharp
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
        // Error: Input string is empty
        ConvertStringToHalfLength("").Log();
        
        // Value: OptionalValue { Value = , HasValue = False }
        ConvertStringToHalfLength("H").Log();
        
        // Value: OptionalValue { Value = Hel, HasValue = True }
        ConvertStringToHalfLength("Hello").Log();
    }
}
```

## 3. Wrapping throwing function that returns value.

When I use `Result` type in C# project, often need in mixing with non-result functions which can be throw exception any time. When this happens, we need to write try/catch and convert throwable functions to `Result`
type. `ResultBox` has `WrapTry` function to do this conversion. When you use `WrapTry`, you need to pass `Func` as the argument.

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
        // Error: can not divide by 0
        ResultBox.WrapTry(() => Divide(10, 0)).Log();

        // This will return value result
        // Value: 5
        ResultBox.WrapTry(() => Divide(10, 2)).Log();
    }
}
```

## 4. Wrapping void function.

When a function does not return value, C# can use void as a return (type). But you can not use `ResultBox<void>`
due to C# language definition. Instead, we made [UnitValue](https://github.com/J-Tech-Japan/ResultBoxes/blob/main/src/ResultBoxes/ResultBoxes/UnitValue.cs)
type, which means nothing inside but as a data class. UnitValue does not have any properties. You can wrap try with `WrapTry` void action, and it will return `ResultBox<UnitValue>` type.

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
        // Error: can not divide by 0
        ResultBox<int>.WrapTry(() => Divide(10, 0)).Log();

        // This will return value result
        // Value: 5
        ResultBox<int>.WrapTry(() => Divide(10, 2)).Log();
    }
}
```

## 5. Railway Oriented Programming - Method Chain

Railway Oriented Programming (ROP) is a functional programming pattern that facilitates error handling and is often used in languages that support functional programming concepts, like F#, Haskell, and others. The analogy of a railway is used to describe the flow of data through a series of functions, similar to how a train travels along tracks.

ResultBoxes supports ROP by providing chain method to connect functions and simply write error handling code.

![Simple ROP](https://raw.githubusercontent.com/J-Tech-Japan/ResultBoxes/main/docs/images/SimpleRop.png)

Like example below, you can use `Conveyor(nextFunction)` to method chain continuous functions.

It is like Result"Box" are carrying through Belt Conveyor and moving next checkpoint, at next checkpoint, contents will be adjusted and pack with different form, and also if error happens, it will convert to the Error Result.

![conveyor image](https://raw.githubusercontent.com/J-Tech-Japan/ResultBoxes/main/docs/images/conveyer.jpg)

If first method , in example `Increment` returns Exception, following functions `Double` and `Triple` will not executed, it will be just passing Exception that returned by `Increment`. If first method returns value, second method, in this case `Double` will be execute, and if all three method succeed, `Main` method receive the result value. If any methods returns Exception Result, it will return Exception to the `Main` function.

```csharp
internal class Program
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        > 1000 => new ApplicationException($"{target} is not allowed for {nameof(Increment)}"),
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
    private static void Main(string[] args)
    {
        // Error: System.ApplicationException: 1001 is not allowed for Increment
        Increment(1001)
            .Conveyor(Double)
            .Conveyor(Triple)
            .Log();

        // Error: System.ApplicationException: 1001 is not allowed for Double
        Increment(1000)
            .Conveyor(Double)
            .Conveyor(Triple)
            .Log();

        // Error: System.ApplicationException: 1202 is not allowed for Triple
        Increment(600)
            .Conveyor(Double)
            .Conveyor(Triple)
            .Log();

        // Value: 24
        Increment(3)
            .Conveyor(Double)
            .Conveyor(Triple)
            .Log();
    }
}
```

## 6. Railway Oriented Programming - Async Task Functions.

Async method returns `Task<ResultBox<TValue>>`, but we provide async chaining methods as well.

```csharp
internal class Program
{
    public static Task<ResultBox<int>> IncrementAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(IncrementAsync)}"),
                _ => target + 1
            });
    public static Task<ResultBox<int>> DoubleAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(DoubleAsync)}"),
                _ => target * 2
            });
    public static Task<ResultBox<int>> TripleAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(TripleAsync)}"),
                _ => target * 3
            });

    private static async Task Main(string[] args)
    {
        // Error: System.ApplicationException: 1001 is not allowed for IncrementAsync
        await IncrementAsync(1001)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();
        // Error: System.ApplicationException: 1001 is not allowed for DoubleAsync
        await IncrementAsync(1000)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();

        // Error: System.ApplicationException: 1202 is not allowed for TripleAsync
        await IncrementAsync(600)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();
        // Value: 24
        await IncrementAsync(3)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();
    }
}
```

You can mix async functions with non-async functions as well.

```csharp
internal class Program
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        > 1000 => new ApplicationException($"{target} is not allowed for {nameof(Increment)}"),
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

    public static Task<ResultBox<int>> IncrementAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(IncrementAsync)}"),
                _ => target + 1
            });
    public static Task<ResultBox<int>> DoubleAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(DoubleAsync)}"),
                _ => target * 2
            });
    public static Task<ResultBox<int>> TripleAsync(int target) =>
        Task.FromResult<ResultBox<int>>(
            target switch
            {
                > 1000 => new ApplicationException(
                    $"{target} is not allowed for {nameof(TripleAsync)}"),
                _ => target * 3
            });

    private static async Task Main(string[] args)
    {
        // Error: System.ApplicationException: 1001 is not allowed for IncrementAsync
        await Increment(1001)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();

        // Error: System.ApplicationException: 1001 is not allowed for DoubleAsync
        await IncrementAsync(1000)
            .Conveyor(Double)
            .Conveyor(TripleAsync)
            .Log();
        // Error: System.ApplicationException: 1202 is not allowed for TripleAsync
        await IncrementAsync(600)
            .Conveyor(DoubleAsync)
            .Conveyor(Triple)
            .Log();
        // Value: 24
        await IncrementAsync(3)
            .Conveyor(DoubleAsync)
            .Conveyor(TripleAsync)
            .Log();
    }
}
```

## 7. Railway Oriented Programming - Combine Value

We have cases that need to prepare 2 or more value and pass it to next function. One way to achieve this is, programmer make a wrapping function and gather two value in function and use railway to handle results. But ResultBoxes provide `CombineValue` methods, which follows first Result, run second function and instead of passing only last executed value, but both first value and second value together and pass it to third function.

![CombineValue](https://raw.githubusercontent.com/J-Tech-Japan/ResultBoxes/main/docs/images/CombineValue.png)

We provide
[TwoValues](https://github.com/J-Tech-Japan/ResultBoxes/blob/main/src/ResultBoxes/ResultBoxes/TwoValues.cs),
[ThreeValues](https://github.com/J-Tech-Japan/ResultBoxes/blob/main/src/ResultBoxes/ResultBoxes/ThreeValues.cs),
[FourValuesResult](https://github.com/J-Tech-Japan/ResultBoxes/blob/main/src/ResultBoxes/ResultBoxes/FourValues.cs)
and [FiveValues](https://github.com/J-Tech-Japan/ResultBoxes/blob/main/src/ResultBoxes/ResultBoxes/FiveValues.cs)

Those record class has keep multiple values and can be used in `CombineValue` method. When you use `CombineValue` method, it will still use `ResultBox<T>` class, but value type will be `TwoValues<T1, T2>`, `ThreeValues<T1, T2, T3>`, `FourValues<T1, T2, T3, T4>`, `FiveValues<T1, T2, T3, T4, T5>`.

We can do it with following code.

```csharp
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
        // Pattern 1 : Use Combine method chain
        // calculate answer = (29 + 1) / (1 + 9) = 3
        // Value: 3
        Increment(29)
            .Combine(Add(1, 9))
            .Conveyor(Divide)
            .Log();

        // Pattern 2 : Error in Increment method (target > 1000)
        // Exception3: 2000 can not use for the Increment. It should be under or equal 1000
        Increment(2000)
            .Combine(Add(1, 9))
            .Conveyor(Divide)
            .Log();

        // Pattern 4 : Error in Add method (target1 > 100)
        // Exception4: over 100 is not allowed for Add
        Increment(19)
            .Combine(Add(1000, 9))
            .Conveyor(Divide)
            .Log();

        // Pattern 5 : Error in Divide method (denominator <> 0)
        // Exception5: can not divide by 0
        Increment(19)
            .Combine(Add(0, 0))
            .Conveyor(Divide)
            .Log();
    }
}
```

We can use `RailWay` and `CombineValue` Method in Async as well.

## 8. Railway Oriented Programming with Wrapping Function with Try.

Example above in **3. Wrapping throwing function that returns value.
** can be use in the Railway Oriented Method Chain as well.

```csharp
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
        // Value: 12
        ResultBox.WrapTry(() => IncrementWithThrowing(1))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing)
            .Log();

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error: 2000 is not allowed for Increment
        ResultBox.WrapTry(() => IncrementWithThrowing(2000))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing)
            .Log();

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error: 1001 is not allowed for Double
        ResultBox.WrapTry(() => IncrementWithThrowing(1000))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing)
            .Log();

        // IncrementWithThrowing and TripleWithThrowing can throw exceptions
        // WrapTry is used to catch exceptions and return them as error
        // Error: 1202 is not allowed for Triple
        ResultBox.WrapTry(() => IncrementWithThrowing(600))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing)
            .Log();
    }
}
```

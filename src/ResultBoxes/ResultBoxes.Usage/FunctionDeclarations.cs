using ResultBoxes;
namespace SingleResults.Usage;

public static class FunctionDeclarations
{
    public static ResultBox<int> Increment(int target) => target switch
    {
        > 1000 => new ArgumentOutOfRangeException(nameof(target)),
        _ => target + 1
    };
    public static int IncrementWithThrowing(int target) => target switch
    {
        3 => throw new ApplicationException("3 is not allowed"),
        _ => target + 1
    };
    public static ResultBox<int> Double(int target) => target switch
    {
        > 1000 => new ArgumentOutOfRangeException(nameof(target)),
        _ => target * 2
    };
    public static ResultBox<int> Triple(int target) => target switch
    {
        > 1000 => new ArgumentOutOfRangeException(nameof(target)),
        _ => target * 3
    };
    public static int TripleWithThrowing(int target) => target switch
    {
        10 => throw new ApplicationException("10 is not allowed"),
        _ => target * 3
    };
    public static ResultBox<int> Add(int target1, int target2) => target1 + target2;
    public static int AddWithThrowing(int target1, int target3) => target1 switch
    {
        > 100 => throw new ApplicationException("over 100 is not allowed"),
        _ => target1 + target3
    };
    public static ResultBox<int> Divide(int numerator, int denominator) =>
        (numerator, denominator) switch
        {
            (_, 0) => new ApplicationException("can not divide by 0"),
            _ => numerator / denominator
        };
    public static ResultBox<int> DivideConverter(TwoValues<int, int> values) =>
        Divide(values.Value1, values.Value2);
    public static int DivideWithThrowing(int numerator, int denominator) =>
        denominator == 0
            ? throw new ApplicationException("can not divide by 0")
            : numerator / denominator;

    public static Task<ResultBox<int>> IncrementAsync(int target) =>
        Task.FromResult(ResultBox<int>.FromValue(target + 1));
    public static async Task<int> IncrementAsyncWithThrowing(int target) =>
        await Task.FromResult(
            target switch
            {
                3 => throw new ApplicationException("3 is not allowed"),
                _ => target + 1
            });
    public static Task<ResultBox<int>> DoubleAsync(int target) =>
        Task.FromResult(ResultBox<int>.FromValue(target * 2));
    public static Task<int> DoubleAsyncWithThrowing(int target) =>
        Task.FromResult(
            target switch
            {
                5 => throw new ApplicationException("5 is not allowed"),
                _ => target * 2
            });
    public static Task<ResultBox<int>> TripleAsync(int target) =>
        Task.FromResult(ResultBox<int>.FromValue(target * 3));
    public static Task<ResultBox<int>> AddAsync(int target1, int target2) =>
        Task.FromResult(ResultBox<int>.FromValue(target1 + target2));
    public static Task<int> AddAsyncWithThrowing(int target1, int target2) =>
        Task.FromResult(
            target1 switch
            {
                > 100 => throw new ApplicationException("over 100 is not allowed"),
                _ => target1 + target2
            });
    public static Task<ResultBox<int>> DivideAsync(int numerator, int denominator) =>
        Task.FromResult(
            (numerator, denominator) switch
            {
                (_, 0) => ResultBox<int>.FromException(
                    new ApplicationException("can not divide by 0")),
                _ => ResultBox<int>.FromValue(numerator / denominator)
            });
    public static Task<int> DivideAsyncWithThrowing(int numerator, int denominator) =>
        Task.FromResult(
            denominator == 0
                ? throw new ApplicationException("can not divide by 0")
                : numerator / denominator);

    public static ResultBox<int> CombinedCalc(int target1, int target2, int target3)
        => Increment(target1) switch
        {
            { IsSuccess: false } error => error,
            { IsSuccess: true } value1 => Add(value1.GetValue(), target2) switch
            {
                { IsSuccess: false } error => error,
                { IsSuccess: true } value2 => Divide(value2.GetValue(), target3)
            }
        };
    public static ResultBox<int> Combined2Calc(int target1, int target2, int target3)
        => Increment(target1) switch
        {
            { IsSuccess: false } error => error,
            var value1 => Add(target2, target3) switch
            {
                { IsSuccess: true } value2 => Divide(value1.GetValue(), value2.GetValue()),
                var exception2 => exception2
            }
        };

    public static ResultBox<int> RailwayCalc3(int target1, int target2, int target3)
        => Increment(target1)
            .Conveyor(value1 => Add(value1, target2))
            .Conveyor(value2 => Divide(value2, target3));
    public static ResultBox<int> Railway2Calc3(int target1, int target2, int target3)
        => Increment(target1)
            .Combine(Add(target2, target3))
            .Conveyor(Divide);

    public static ResultBox<int> Railway2CalcG(int target1, int target2, int target3)
        => Increment(target1)
            .Combine(Add(target2, target3))
            .Conveyor(DivideConverter);
    // .Conveyor(TwoValues.ToFunc<int,int,int>(Divide));
    // .Conveyor(TwoValues<int,int>.ToFunc(Divide))
    // .Conveyor(Divide);
    // .Conveyor((values) => Divide(values.Value1, values.Value2));


    public static ResultBox<int> Railway2Calc4(int target1, int target2, int target3)
        => Increment(target1)
            .CombineWrapTry(() => AddWithThrowing(target2, target3))
            .ConveyorWrapTry(DivideWithThrowing);

    public static Task<ResultBox<int>> RailwayCalc3Async(
        int target1,
        int target2,
        int target3)
        => IncrementAsync(target1)
            .Combine(() => AddAsync(target2, target3))
            .Conveyor(DivideAsync);
    public static Task<ResultBox<int>> RailwayCalc3Async2(
        int target1,
        int target2,
        int target3)
        => Increment(target1)
            .Combine(() => AddAsync(target2, target3))
            .Conveyor(Divide);
    public static Task<ResultBox<int>> RailwayCalc3Async4(
        int target1,
        int target2,
        int target3)
        => Increment(target1)
            .CombineWrapTry(() => AddAsyncWithThrowing(target2, target3))
            .ConveyorWrapTry(DivideAsyncWithThrowing);

    public static ResultBox<int> RailwayCalc3Async3(int target1, int target2, int target3)
        => Increment(target1)
            .Combine(Add(target2, target3))
            .ConveyorWrapTry(DivideWithThrowing);
    public static Task<ResultBox<int>> RailwayCalc3Async5(
        int target1,
        int target2,
        int target3)
        => IncrementAsync(target1)
            .CombineWrapTry(() => AddAsyncWithThrowing(target2, target3))
            .Conveyor(DivideAsync);

    public static Task<ResultBox<int>> Railway2Calc3Async6(
        int target1,
        int target2,
        int target3)
        => Increment(target1)
            .Combine(Add(target2, target3))
            .Conveyor(DivideAsync);
    public static Task<ResultBox<int>> Railway2Calc3Async7(
        int target1,
        int target2,
        int target3)
        => Increment(target1)
            .Combine(Add(target2, target3))
            .ConveyorWrapTry(DivideAsyncWithThrowing);

    public static ResultBox<int> RailwayInstance(int target1)
        => Increment(target1)
            .Conveyor(Double)
            .Conveyor(Triple);
    public static ResultBox<int> RailwayInstance2(int target1)
        => ResultBox.WrapTry(() => IncrementWithThrowing(target1))
            .Conveyor(Double)
            .ConveyorWrapTry(TripleWithThrowing);

    public static Task<ResultBox<int>> RailwayWithAsync(int target1)
        => IncrementAsync(target1).Conveyor(DoubleAsync).Conveyor(TripleAsync);
    public static Task<ResultBox<int>> Railway2Async(int target1)
        => Increment(target1).Conveyor(DoubleAsync).Conveyor(TripleAsync);
    public static Task<ResultBox<int>> Railway3Async(int target1)
        => Increment(target1).Conveyor(DoubleAsync).Conveyor(Triple);
    public static Task<ResultBox<int>> Railway4Async(int target1)
        => ResultBox.WrapTry(() => IncrementAsyncWithThrowing(target1))
            .ConveyorWrapTry(DoubleAsyncWithThrowing)
            .Conveyor(TripleAsync);
}

namespace ResultBoxes;

public static class CombineWrapTryTaskExtension
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).ConveyorResult(
            async current => await (await ResultBox<TValue2>.WrapTry(secondValueFunc)).Conveyor(
                addingValue => Task.FromResult(current.Append(addingValue))));
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<TValue1, Task<TValue2>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).ConveyorResult(
            async current => await (await ResultBox<TValue2>.WrapTry(async() => await secondValueFunc(current.GetValue()))).Conveyor(
                addingValue => Task.FromResult(current.Append(addingValue))));
    
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<TValue2> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).ConveyorResult(
            async current => await (ResultBox<TValue2>.WrapTry(secondValueFunc)).Conveyor(
                addingValue => Task.FromResult(current.Append(addingValue))));
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<TValue1, TValue2> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).ConveyorResult(
            async current => await (ResultBox<TValue2>.WrapTry(() => secondValueFunc(current.GetValue()))).Conveyor(
                addingValue => Task.FromResult(current.Append(addingValue))));
}

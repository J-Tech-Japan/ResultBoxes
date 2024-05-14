namespace ResultBoxes;

public static class CombineTaskExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineValue<TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> currentTask,
        Func<Task<ResultBox<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await currentTask).HandleResultAsync(
            async current => await (await secondValueFunc()).HandleAsync(
                addingValue => Task.FromResult(current.Append(addingValue))));

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineValue<
        TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> currentTask,
        Func<Task<ResultBox<TValue3>>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await currentTask).HandleAsync(
            async values => await (await addingFunc()).HandleAsync(
                addingValue => Task.FromResult(values.Append(addingValue))));

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>>
        CombineValue<
            TValue1, TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> currentTask,
            Func<Task<ResultBox<TValue4>>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await currentTask).HandleAsync(
            async values => await (await addingFunc()).HandleAsync(
                addingValue => Task.FromResult(values.Append(addingValue))));

    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        CombineValue<
            TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> currentTask,
            Func<Task<ResultBox<TValue5>>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await currentTask).HandleAsync(
            async values => await (await addingFunc()).HandleAsync(
                addingValue => Task.FromResult(values.Append(addingValue))));
}

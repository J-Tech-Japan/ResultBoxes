namespace ResultBoxes;

public static class CombineTaskExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Combine<TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> currentTask,
        Func<Task<ResultBox<TValue2>>> secondValueFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await currentTask).RemapResultAsync(
            async current => await (await secondValueFunc()).RemapAsync(
                addingValue => Task.FromResult(current.Append(addingValue))));

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Combine<
        TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> currentTask,
        Func<Task<ResultBox<TValue3>>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await currentTask).RemapAsync(
            async values => await (await addingFunc()).RemapAsync(
                addingValue => Task.FromResult(values.Append(addingValue))));

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>>
        Combine<
            TValue1, TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> currentTask,
            Func<Task<ResultBox<TValue4>>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await currentTask).RemapAsync(
            async values => await (await addingFunc()).RemapAsync(
                addingValue => Task.FromResult(values.Append(addingValue))));

    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Combine<
            TValue1, TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> currentTask,
            Func<Task<ResultBox<TValue5>>> addingFunc)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await currentTask).RemapAsync(
            async values => await (await addingFunc()).RemapAsync(
                addingValue => Task.FromResult(values.Append(addingValue))));
}

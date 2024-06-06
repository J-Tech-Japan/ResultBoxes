namespace ResultBoxes;

public static class CombineWrapTryTaskExtension
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<Task<TValue2>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).ConveyorResult(
            async current => await (await ResultBox.WrapTry(secondValueFunc, exceptionMapper)).Conveyor(
                addingValue => Task.FromResult(current.Append(addingValue))));
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<TValue1, Task<TValue2>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).ConveyorResult(
            async current =>
                await (await ResultBox.WrapTry(
                    async () => await secondValueFunc(current.GetValue()),exceptionMapper)).Conveyor(
                    addingValue => Task.FromResult(current.Append(addingValue))));

    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<TValue2> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).ConveyorResult(
            async current => await ResultBox.WrapTry(secondValueFunc,exceptionMapper)
                .Conveyor(
                    addingValue => Task.FromResult(current.Append(addingValue))));
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1,
        TValue2>(
        this Task<ResultBox<TValue1>> firstValueTask,
        Func<TValue1, TValue2> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValueTask).ConveyorResult(
            async current => await ResultBox.WrapTry(() => secondValueFunc(current.GetValue()),exceptionMapper)
                .Conveyor(
                    addingValue => Task.FromResult(current.Append(addingValue))));














    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
        TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValueTask,
        Func<Task<TValue3>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await firstValueTask).Conveyor(
            async current => (await ResultBox.WrapTry(secondValueFunc,exceptionMapper)).Remap(current.Append));
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
        TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValueTask,
        Func<TValue1, TValue2, Task<TValue3>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await (await firstValueTask).Conveyor(
            async current => await ResultBox.WrapTry(() => current.Call(secondValueFunc), exceptionMapper)
                .Remap(current.Append));

    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
        TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValueTask,
        Func<TValue3> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => (await firstValueTask).Conveyor(
            current => ResultBox.WrapTry(secondValueFunc, exceptionMapper).Remap(current.Append));
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
        TValue1,
        TValue2, TValue3>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValueTask,
        Func<TValue1, TValue2, TValue3> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => (await firstValueTask).Conveyor(
            current =>
                ResultBox.WrapTry(() => current.Call(secondValueFunc), exceptionMapper).Remap(current.Append));



    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>>
        CombineWrapTry<
            TValue1,
            TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValueTask,
            Func<Task<TValue4>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await firstValueTask).Conveyor(
            async current => (await ResultBox.WrapTry(secondValueFunc, exceptionMapper)).Remap(current.Append));
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>>
        CombineWrapTry<
            TValue1,
            TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValueTask,
            Func<TValue1, TValue2, TValue3, Task<TValue4>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await (await firstValueTask).Conveyor(
            async current => await ResultBox.WrapTry(() => current.Call(secondValueFunc), exceptionMapper)
                .Remap(current.Append));

    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>>
        CombineWrapTry<
            TValue1,
            TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValueTask,
            Func<TValue4> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => (await firstValueTask).Conveyor(
            current => ResultBox.WrapTry(secondValueFunc, exceptionMapper).Remap(current.Append));
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>>
        CombineWrapTry<
            TValue1,
            TValue2, TValue3, TValue4>(
            this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValueTask,
            Func<TValue1, TValue2, TValue3, TValue4> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => (await firstValueTask).Conveyor(
            current =>
                ResultBox.WrapTry(() => current.Call(secondValueFunc), exceptionMapper).Remap(current.Append));



    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        CombineWrapTry<
            TValue1,
            TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValueTask,
            Func<Task<TValue5>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await firstValueTask).Conveyor(
            async current => (await ResultBox.WrapTry(secondValueFunc, exceptionMapper)).Remap(current.Append));
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        CombineWrapTry<
            TValue1,
            TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValueTask,
            Func<TValue1, TValue2, TValue3, TValue4, Task<TValue5>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await (await firstValueTask).Conveyor(
            async current => await ResultBox.WrapTry(() => current.Call(secondValueFunc), exceptionMapper)
                .Remap(current.Append));

    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        CombineWrapTry<
            TValue1,
            TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValueTask,
            Func<TValue5> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await firstValueTask).Conveyor(
            current => ResultBox.WrapTry(secondValueFunc, exceptionMapper).Remap(current.Append));
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        CombineWrapTry<
            TValue1,
            TValue2, TValue3, TValue4, TValue5>(
            this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValueTask,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => (await firstValueTask).Conveyor(
            current =>
                ResultBox.WrapTry(() => current.Call(secondValueFunc), exceptionMapper).Remap(current.Append));
}

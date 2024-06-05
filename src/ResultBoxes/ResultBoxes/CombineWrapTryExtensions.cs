namespace ResultBoxes;

public static class CombineWrapTryExtensions
{
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<Task<TValue2>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await current.ConveyorResult(
            async first =>
                (await ResultBox.WrapTry(secondValueFunc, exceptionMapper)).Conveyor(first.Append));
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> CombineWrapTry<
        TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<TValue1, Task<TValue2>> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await current.ConveyorResult(
            async first =>
                (await ResultBox.WrapTry(async () => await secondValueFunc(first.GetValue()),exceptionMapper))
                .Conveyor(first.Append));

    public static ResultBox<TwoValues<TValue, TValue2>>
        CombineWrapTry<TValue, TValue2>(
            this ResultBox<TValue> current,
            Func<TValue2> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue : notnull
        where TValue2 : notnull
        => current.ConveyorResult(
            c => ResultBox.WrapTry(secondValueFunc, exceptionMapper).Conveyor(current.Append));

    public static ResultBox<TwoValues<TValue1, TValue2>>
        CombineWrapTry<TValue1, TValue2>(
            this ResultBox<TValue1> current,
            Func<TValue1, TValue2> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => current.ConveyorResult(
            c => ResultBox.WrapTry(() => secondValueFunc(c.GetValue()),exceptionMapper).Conveyor(current.Append));



    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
        TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<Task<TValue3>> lastValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await current.ConveyorResult(
            async first =>
                (await ResultBox.WrapTry(lastValueFunc,exceptionMapper)).Remap(first.GetValue().Append));
    public static async Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> CombineWrapTry<
        TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, Task<TValue3>> lastValueFuncAsync, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => await current.Conveyor(
            async first =>
                await ResultBox.WrapTry(
                    async () => first.Append(await first.Call(lastValueFuncAsync)),exceptionMapper));

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>>
        CombineWrapTry<TValue1, TValue2, TValue3>(
            this ResultBox<TwoValues<TValue1, TValue2>> current,
            Func<TValue3> lastValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => current.Conveyor(
            values => ResultBox.WrapTry(() => values.Append(lastValueFunc()), exceptionMapper));

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>>
        CombineWrapTry<TValue1, TValue2, TValue3>(
            this ResultBox<TwoValues<TValue1, TValue2>> current,
            Func<TValue1, TValue2, TValue3> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        => current.Conveyor(
            values => ResultBox.WrapTry(() => values.Append(values.Call(secondValueFunc)), exceptionMapper));
    
    
    
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> CombineWrapTry<
        TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<Task<TValue4>> lastValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await current.ConveyorResult(
            async first =>
                (await ResultBox.WrapTry(lastValueFunc, exceptionMapper)).Remap(first.GetValue().Append));
    public static async Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> CombineWrapTry<
        TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<TValue1, TValue2, TValue3, Task<TValue4>> lastValueFuncAsync, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => await current.Conveyor(
            async first =>
                await ResultBox.WrapTry(
                    async () => first.Append(await first.Call(lastValueFuncAsync)), exceptionMapper));

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>
        CombineWrapTry<TValue1, TValue2, TValue3, TValue4>(
            this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
            Func<TValue4> lastValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => current.Conveyor(
            values => ResultBox.WrapTry(() => values.Append(lastValueFunc()), exceptionMapper));

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>
        CombineWrapTry<TValue1, TValue2, TValue3, TValue4>(
            this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
            Func<TValue1, TValue2, TValue3, TValue4> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        => current.Conveyor(
            values => ResultBox.WrapTry(() => values.Append(values.Call(secondValueFunc)), exceptionMapper));


    
    
        public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> CombineWrapTry<
        TValue1, TValue2, TValue3, TValue4, TValue5>(
        this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
        Func<Task<TValue5>> lastValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await current.ConveyorResult(
            async first =>
                (await ResultBox.WrapTry(lastValueFunc, exceptionMapper)).Remap(first.GetValue().Append));
    public static async Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> CombineWrapTry<
            TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
            Func<TValue1, TValue2, TValue3, TValue4, Task<TValue5>> lastValueFuncAsync, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => await current.Conveyor(
            async first =>
                await ResultBox.WrapTry(
                    async () => first.Append(await first.Call(lastValueFuncAsync)), exceptionMapper));

    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>
        CombineWrapTry<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
            Func<TValue5> lastValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => current.Conveyor(
            values => ResultBox.WrapTry(() => values.Append(lastValueFunc()),exceptionMapper));

    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>
        CombineWrapTry<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
            Func<TValue1, TValue2, TValue3, TValue4, TValue5> secondValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        => current.Conveyor(
            values => ResultBox.WrapTry(() => values.Append(values.Call(secondValueFunc)),exceptionMapper));
}

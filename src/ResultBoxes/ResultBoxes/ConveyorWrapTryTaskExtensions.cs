namespace ResultBoxes;

public static class ConveyorWrapTryTaskExtensions
{
    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, Task<TValue2>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValue).Conveyor(
            async value => await ResultBox.WrapTry(
                () => handleValueFunc(value), exceptionMapper));
    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue1, TValue2> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await firstValue).Conveyor(
            value => ResultBox.WrapTry(() => handleValueFunc(value), exceptionMapper));

    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<Task<TValue2>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => await (await firstValue).Conveyor(
            async value => await ResultBox.WrapTry(
                handleValueFunc, exceptionMapper));
    public static async Task<ResultBox<TValue2>> ConveyorWrapTry<TValue1, TValue2>(
        this Task<ResultBox<TValue1>> firstValue,
        Func<TValue2> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        => (await firstValue).Conveyor(
            value => ResultBox.WrapTry(handleValueFunc, exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2,
        TValueResult>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, Task<TValueResult>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await values.Call(handleValueFunc), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2,
        TValueResult>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValue1, TValue2, TValueResult> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2,
        TValueResult>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<Task<TValueResult>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await handleValueFunc(), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2,
        TValueResult>(
        this Task<ResultBox<TwoValues<TValue1, TValue2>>> firstValue,
        Func<TValueResult> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(_ => ResultBox.WrapTry(handleValueFunc, exceptionMapper));





    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValueResult>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
        Func<TValue1, TValue2, TValue3, Task<TValueResult>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await values.Call(handleValueFunc), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValueResult>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValueResult> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValueResult>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
        Func<Task<TValueResult>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await handleValueFunc(), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValueResult>(
        this Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> firstValue,
        Func<TValueResult> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(_ => ResultBox.WrapTry(handleValueFunc, exceptionMapper));





    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValue4, TValueResult>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, Task<TValueResult>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await values.Call(handleValueFunc), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValue4, TValueResult>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValueResult> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValue4, TValueResult>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValue,
        Func<Task<TValueResult>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await handleValueFunc(), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValue4, TValueResult>(
        this Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>> firstValue,
        Func<TValueResult> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(_ => ResultBox.WrapTry(handleValueFunc, exceptionMapper));




    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValue4, TValue5, TValueResult>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, Task<TValueResult>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await values.Call(handleValueFunc), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValue4, TValue5, TValueResult>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> firstValue,
        Func<TValue1, TValue2, TValue3, TValue4, TValue5, TValueResult> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(
            values => ResultBox.WrapTry(() => values.Call(handleValueFunc), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValue4, TValue5, TValueResult>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> firstValue,
        Func<Task<TValueResult>> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => await (await firstValue).Conveyor(
            async values =>
                await ResultBox.WrapTry(async () => await handleValueFunc(), exceptionMapper));

    public static async Task<ResultBox<TValueResult>> ConveyorWrapTry<TValue1, TValue2, TValue3,
        TValue4, TValue5, TValueResult>(
        this Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>> firstValue,
        Func<TValueResult> handleValueFunc, Func<Exception, Exception>? exceptionMapper = null)
        where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull
        where TValueResult : notnull
        => (await firstValue).Conveyor(_ => ResultBox.WrapTry(handleValueFunc, exceptionMapper));
}

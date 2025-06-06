namespace ResultBoxes;

public static class CombineExtensions
{
    #region Combine with async no param func returns ResultBox<>
    public static async Task<ResultBox<TwoValues<TValue1, TValue2>>> Combine<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<Task<ResultBox<TValue2>>> secondValueFunc) where TValue1 : notnull where TValue2 : notnull =>
        await current.Conveyor(async _ => (await secondValueFunc()).Conveyor(current.Append));
    public static Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Combine<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<Task<ResultBox<TValue3>>> combiningFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull =>
        current
            .Combine(_ => combiningFunc())
            .Remap(
                values => new ThreeValues<TValue1, TValue2, TValue3>(
                    values.Value1.Value1,
                    values.Value1.Value2,
                    values.Value2));
    public static Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>>
        Combine<TValue1, TValue2, TValue3, TValue4>(
            this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
            Func<Task<ResultBox<TValue4>>> combiningFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull =>
        current
            .Combine(_ => combiningFunc())
            .Remap(
                values => new FourValues<TValue1, TValue2, TValue3, TValue4>(
                    values.Value1.Value1,
                    values.Value1.Value2,
                    values.Value1.Value3,
                    values.Value2));
    public static Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Combine<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
            Func<Task<ResultBox<TValue5>>> combiningFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull =>
        current
            .Combine(_ => combiningFunc())
            .Remap(
                values => FiveValues.FromValues(
                    values.Value1.Value1,
                    values.Value1.Value2,
                    values.Value1.Value3,
                    values.Value1.Value4,
                    values.Value2));
    #endregion

    #region Combine with async values returns Task<ResultBox<>>
    public static async Task<ResultBox<TwoValues<TValue, TValue2>>> Combine<TValue, TValue2>(
        this ResultBox<TValue> current,
        Func<TValue, Task<ResultBox<TValue2>>> secondValueFunc) where TValue : notnull where TValue2 : notnull =>
        await current.Conveyor(async value => (await secondValueFunc(value)).Conveyor(current.Append));

    public static Task<ResultBox<ThreeValues<TValue1, TValue2, TValue3>>> Combine<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, Task<ResultBox<TValue3>>> combiningFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull =>
        current
            .Combine(value => combiningFunc(value.Value1, value.Value2))
            .Remap(values => ThreeValues.FromValues(values.Value1.Value1, values.Value1.Value2, values.Value2));

    public static Task<ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>>>
        Combine<TValue1, TValue2, TValue3, TValue4>(
            this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
            Func<TValue1, TValue2, TValue3, Task<ResultBox<TValue4>>> combiningFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull =>
        current
            .Combine(value => combiningFunc(value.Value1, value.Value2, value.Value3))
            .Remap(
                values => FourValues.FromValues(
                    values.Value1.Value1,
                    values.Value1.Value2,
                    values.Value1.Value3,
                    values.Value2));
    public static Task<ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>>
        Combine<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
            Func<TValue1, TValue2, TValue3, TValue4, Task<ResultBox<TValue5>>> combiningFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull =>
        current
            .Combine(value => combiningFunc(value.Value1, value.Value2, value.Value3, value.Value4))
            .Remap(
                values => FiveValues.FromValues(
                    values.Value1.Value1,
                    values.Value1.Value2,
                    values.Value1.Value3,
                    values.Value1.Value4,
                    values.Value2));
    #endregion

    #region Combine with ResultBox<> Value returns ResultBox<>
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>
        Combine<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> values,
            ResultBox<TValue5> addingResult) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull =>
        values.Conveyor(value => addingResult.Remap(value.Append));

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> Combine<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> values,
        ResultBox<TValue4> addingResult) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull =>
        values.Conveyor(value => addingResult.Remap(value.Append));

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Combine<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> values,
        ResultBox<TValue3> addingResult) where TValue1 : notnull where TValue2 : notnull where TValue3 : notnull =>
        values.Conveyor(value => addingResult.Remap(value.Append));


    public static ResultBox<TwoValues<TValue1, TValue2>> Combine<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        ResultBox<TValue2> secondValue) where TValue1 : notnull where TValue2 : notnull =>
        current.Conveyor(_ => secondValue.Conveyor(current.Append));
    #endregion

    #region Combine with values func returns ResultBox<>
    public static ResultBox<FiveValues<TValue1, TValue2, TValue3, TValue4, TValue5>>
        Combine<TValue1, TValue2, TValue3, TValue4, TValue5>(
            this ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> current,
            Func<TValue1, TValue2, TValue3, TValue4, ResultBox<TValue5>> addingFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull
        where TValue5 : notnull =>
        current.Conveyor(value => value.Call(addingFunc).Remap(value.Append));

    public static ResultBox<FourValues<TValue1, TValue2, TValue3, TValue4>> Combine<TValue1, TValue2, TValue3, TValue4>(
        this ResultBox<ThreeValues<TValue1, TValue2, TValue3>> current,
        Func<TValue1, TValue2, TValue3, ResultBox<TValue4>> addingFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull
        where TValue4 : notnull =>
        current.Conveyor(value => value.Call(addingFunc).Remap(value.Append));

    public static ResultBox<ThreeValues<TValue1, TValue2, TValue3>> Combine<TValue1, TValue2, TValue3>(
        this ResultBox<TwoValues<TValue1, TValue2>> current,
        Func<TValue1, TValue2, ResultBox<TValue3>> addingFunc) where TValue1 : notnull
        where TValue2 : notnull
        where TValue3 : notnull =>
        current.Conveyor(value => value.Call(addingFunc).Remap(value.Append));

    public static ResultBox<TwoValues<TValue1, TValue2>> Combine<TValue1, TValue2>(
        this ResultBox<TValue1> current,
        Func<TValue1, ResultBox<TValue2>> secondValueFunc) where TValue1 : notnull where TValue2 : notnull =>
        current.Conveyor(value => secondValueFunc(value).Conveyor(current.Append));
    #endregion
}
